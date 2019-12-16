using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaperController : MonoBehaviour, CharacterStatus
{
    public Transform player;
    public GameObject asteroid;
    public Transform asteroidSpawnPoint;

    //gameplay value
    public float sightX = 3f;
    public float sightY = 5f;
    public float slashSightX = 25f;
    public float slashSightY = 25f;
    public float throwCoolDown = 4f;
    public float throwObjectSpeedFfactor = 8f;
    private float attackRange = 2.5f;

    public int attack = 10;

    //state
    private bool isThrowing = false;
    private bool isSlashing = false;
    private bool isReadyToThrow = true;
    private bool isFacingRight = true;
    private bool shouldSlash = false;
    private bool isDead = false;

    private Animator animator;
    public AudioClip asteroidChargeSound;
    public AudioClip attackSound;

    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("Dead", isDead);
        animator.SetBool("Attack", isSlashing);
        animator.SetBool("Throw", isThrowing);
        if ( !isDead ){
            CheckPlayer();
        }    
    }

    void CheckPlayer(){
        if ( IsPlayerInSight() ){
            if ( shouldSlash){
                if ( !isSlashing ){
                   Slash(); 
                }
            }
            else if ( isReadyToThrow ) {
                Throw();
            }
        }
    }

    private void Throw(){
        isThrowing = true;
        isReadyToThrow = false;
        Vector2 dir = player.position - transform.position;
        dir.Normalize();
        dir = new Vector2(throwObjectSpeedFfactor*dir.x, throwObjectSpeedFfactor*dir.y);
        GameObject gameObject = Instantiate(asteroid, asteroidSpawnPoint.position, Quaternion.identity);
        gameObject.SetActive(true);
        gameObject.GetComponent<Rigidbody2D>().velocity = dir;
        audioSource.clip = asteroidChargeSound;
        audioSource.Play();
    }

    private void Slash(){
        isSlashing = true;
        audioSource.clip = attackSound;
        audioSource.Play();
    }

    public void EndThrowing(){
        isThrowing = false;
        StartCoroutine(CoolDownThrowing());
    }

    IEnumerator CoolDownThrowing(){
        yield return new WaitForSeconds(throwCoolDown);

        isReadyToThrow = true;
    }

    public void EndSlashing(){
        Debug.Log("End Slashing");
        isSlashing = false;
        Vector3 targetDir = player.position - transform.position;
        if ( ((isFacingRight && targetDir.x < attackRange && targetDir.x > -0.5f) ||
             ( !isFacingRight && -targetDir.x < attackRange && targetDir.x < 0.5f)) && Mathf.Abs(targetDir.y) < attackRange){
                player.GetComponent<Damageable>().Attacked(attack);
             }
    }

    bool IsPlayerInSight(){
        Vector2 dir = player.position - transform.position;
        bool inSight = false;
        if ( Mathf.Abs(dir.x) < sightX && Mathf.Abs(dir.y) < sightY ){
            inSight = true;
            if ( isFacingRight && dir.x < 0 ){
                Flip();
            }
            else if ( !isFacingRight && dir.x > 0 ){
                Flip();
            }
            if ( Mathf.Abs(dir.x) < slashSightX && Mathf.Abs(dir.y) < slashSightY ){
                shouldSlash = true;
            }
            else {
                shouldSlash = false;
            }
        }

        return inSight;
    }

    private void Flip()
        {
            // Switch the way the player is labelled as facing.
            isFacingRight = !isFacingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }

    public void Die(){
        isDead = true;
        Destroy(gameObject.GetComponent<Rigidbody2D>());
        Destroy(gameObject.GetComponent<Collider2D>());
        Destroy(gameObject, 2);
    }
}
