using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoobEnemyController : MonoBehaviour, CharacterStatus
{
    public int attack = 10;
    public float walkSpeed = 2;
    private Animator animator;
    private bool isFacingRight = true;
    public Transform player;
    private float sightY = 1.5f;
    public float sightX = 6f;
    public float attackRange = 2f;
    private bool isDead = false;
    private PlayerActionable playerAction;
    private AudioSource AudioSource;

    private Rigidbody2D rigidbody2D;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        playerAction = player.GetComponent<PlayerActionable>();
        AudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if ( !isDead ){
            CheckPlayer();
        }
        
    }

    private void Attack(){
        animator.SetBool("Attack", true);
    }

    public void StartAttackPeriod(){
        AudioSource.Play();
    }

    public void EndAttackEvent(){
        player.GetComponent<Damageable>().Attacked(attack);
    }

    private void CheckPlayer(){
        Vector3 targetDir = player.position - transform.position;
        if ( Mathf.Abs(targetDir.x) <= sightX && Mathf.Abs(targetDir.y) <= sightY ){
            if ( (targetDir.x > 0 && !isFacingRight) || (targetDir.x < 0 && isFacingRight) ){
                Flip();
            }
            if ( Mathf.Abs(targetDir.x) < attackRange ){
                StopWalking();
                Attack();
            }
            else {
                WalkToPlayer();
                animator.SetBool("Attack", false);
            }
            
        }
        else {
            StopWalking();
            animator.SetBool("Attack", false);
        }
    }

    private void WalkToPlayer(){
        if ( isFacingRight ){
            rigidbody2D.velocity = new Vector2(walkSpeed, rigidbody2D.velocity.y);
        }
        else {
           rigidbody2D.velocity = new Vector2(-walkSpeed, rigidbody2D.velocity.y); 
        }
        
        animator.SetFloat("Speed", walkSpeed);
    }

    private void StopWalking(){
        rigidbody2D.velocity = new Vector2(0,rigidbody2D.velocity.y);
        animator.SetFloat("Speed", 0);
    }

    public void Die(){
        Destroy(gameObject.GetComponent<Rigidbody2D>());
        Destroy(gameObject.GetComponent<Collider2D>());
        StopWalking();
        animator.SetBool("Dead", true);
        isDead = true;
        Destroy(gameObject, 2.0f);
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
}
