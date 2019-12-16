using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiePatrol : MonoBehaviour
{

    public float delayToFlip = 5.0f;
    public bool walkPatrol = false;
    private EnemyController enemyController;
    public Transform player;
    public float horizontalDistance = 1;
    private float verticalDistance = 1;
    private bool isPatroling = true;
    public float delayToStartPatroling = 2;
    public GameObject gameOverScreen;

    // Start is called before the first frame update
    void Start()
    {   
        enemyController = GetComponent<EnemyController>();
        InvokeRepeating("Flip", delayToStartPatroling, delayToFlip);    
    }

    // Update is called once per frame
    void Update()
    {
        if ( isPatroling && enemyController.IsAlive() ){
            Vector3 targetDir = player.position - transform.position;
            if ( enemyController.IsFacingRight() ){
                if ( targetDir.x > 0 &&  targetDir.x < horizontalDistance && Mathf.Abs(targetDir.y) < verticalDistance ){
                    SeePlayer();
                }
            }
            else {
                if ( Mathf.Abs(targetDir.x) < horizontalDistance && Mathf.Abs(targetDir.y) < verticalDistance ){
                    SeePlayer();
                }
            }
        }
       
    }

    private void Flip()
        {
            if ( enemyController.IsAlive() ){
                enemyController.Flip();
                if ( walkPatrol ){
                    enemyController.Walk();
                }
            }
            
        }

    public void SeePlayer(){
        Debug.Log("see player");
        CancelInvoke("Flip");
        isPatroling = false;
        gameOverScreen.SetActive(true);
        player.GetComponent<UnityStandardAssets._2D.MyCharacterUserControl>().SetCanControl(false);
    }

    public void StopPatroling(){
        isPatroling  = false;
    }
}
