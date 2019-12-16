using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaActionController : MonoBehaviour, PlayerActionable
{
    public int kunaiCost = 7;
    public int slashDamage = 80;
    public int kunaiDamage = 40;
    public bool isActive = false;
    public GameObject kunai;
    public float kunaiSpeed = 40;
    private PlayerStatus playerStatus;
    private UnityStandardAssets._2D.MyCharacterController myCharacterController;
    private Animator animator;
    private bool isThrowingKunel = false;
    private bool isSlashing = false;
    public GameObject slashChecker;

    private PlayerSoundController playerSoundController;
    // Start is called before the first frame update
    void Start()
    {
        playerStatus = GetComponent<PlayerStatus>();
        animator = GetComponent<Animator>();
        myCharacterController = GetComponent<UnityStandardAssets._2D.MyCharacterController>();
        playerSoundController = GetComponent<PlayerSoundController>();
    }

    // Update is called once per frame
    void Update()
    {
        if ( isActive ){
            if ( Input.GetKeyDown(KeyCode.J) && !isSlashing ){
                isSlashing = true;
                animator.SetBool("Attack", true);
                StartCoroutine(Slash());
            }
            if ( Input.GetKeyDown(KeyCode.K) && !isThrowingKunel && playerStatus.status > kunaiCost){
                playerStatus.status -= kunaiCost;
                isThrowingKunel = true;
                playerSoundController.PlayThrowKunai();
                animator.SetBool("Throw", true);
                StartCoroutine(ThrowKunai());
            }
        }
    }

    public void SetActive(bool b){
        isActive = b;
    }

    public bool IsActive(){
        return isActive;
    }

    IEnumerator Slash(){
        playerSoundController.PlaySlash();
        yield return new WaitForSeconds(0.3f);
        isSlashing = false;
        animator.SetBool("Attack", false);
    }

    public void EndSlashing(){
        slashChecker.GetComponent<SlashController>().CheckSlash();
    }

    public bool IsSlashing(){
        return isSlashing;
    }

    IEnumerator ThrowKunai(){
        GameObject throwedKunai = Instantiate(kunai, gameObject.transform.position, Quaternion.Euler(0,0,270));
        throwedKunai.SetActive(true);
        throwedKunai.GetComponent<Rigidbody2D>().velocity = new Vector2(kunaiSpeed, 0);
        throwedKunai.GetComponent<Attackable>().attack = kunaiDamage;
        if ( !myCharacterController.IsFacingRight() ){
            Vector3 theScale = throwedKunai.transform.localScale;
            theScale.y *= -1;
            throwedKunai.transform.localScale = theScale;
            throwedKunai.GetComponent<Rigidbody2D>().velocity = new Vector2(-kunaiSpeed, 0);
        }
        
        yield return new WaitForSeconds(0.2f);  // throw animation time approximately
        animator.SetBool("Throw", false);
        isThrowingKunel = false;
    }
}
