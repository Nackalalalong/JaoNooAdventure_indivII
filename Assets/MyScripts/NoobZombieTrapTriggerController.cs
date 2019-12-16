using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoobZombieTrapTriggerController : MonoBehaviour
{
    public Collider2D[] holders;
    private bool hasPausePlayer = false;
    private bool isEntered = false;
    public GameObject player;
    public GameObject column1;
    public GameObject column2;
    // Start is called before the first frame update
    
    void OnTriggerEnter2D(Collider2D other){
        if ( other.gameObject.tag == "Player" ){
            foreach(Collider2D holder in holders ){
                Destroy(holder);
            }
            isEntered = true;
        }
    }

    void Update(){
        if ( isEntered ){
            if ( !hasPausePlayer ){
                player.GetComponent<UnityStandardAssets._2D.MyCharacterUserControl>().SetCanControl(false);
                hasPausePlayer = true;
                Invoke("EnablePlayer", 2f);
            }
        }
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, new Vector2(30,40), 0);
        bool hasAnyZombie = false;
        foreach(Collider2D collider in colliders ){
            if ( collider.gameObject.tag == "NoobZombie" ){
                hasAnyZombie = true;
            }
        }
        if ( !hasAnyZombie ){
            Destroy(column1);
            Destroy(column2);
            Destroy(gameObject);
        }
    }

    void EnablePlayer(){
        player.GetComponent<UnityStandardAssets._2D.MyCharacterUserControl>().SetCanControl(true);
    }
}
