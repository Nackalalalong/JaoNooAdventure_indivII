using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    private Animator animator;
    private bool isDead = false;
    public float walkSpeed = 1;
    private float speed = 0;
    // Start is called before the first frame update
    private bool facingRight = true;
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if ( facingRight ){
            rigidbody2D.velocity = new Vector2(speed, rigidbody2D.velocity.y);
        }
        else{
            rigidbody2D.velocity = new Vector2(-speed, rigidbody2D.velocity.y);
        }
        
        animator.SetFloat("Speed", speed);
        animator.SetBool("Dead", isDead);
        if ( isDead ){
            Dead();
        }
    }

    public void Attacked(int damage){
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if( other.gameObject.tag == "Wall" ){
            Stop();
        }
    }

    public void Flip()
        {
            facingRight = !facingRight;

            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }

    void  Dead(){
        Destroy(this.gameObject, 3);
    }

    public void Die(){
        CancelInvoke();
        Destroy(transform.GetChild(0).gameObject);
        isDead  = true;
    }

    public void Walk(){
        speed = walkSpeed;
    }

    public void Stop(){
        speed = 0;
    }

    public bool IsFacingRight(){
        return this.facingRight;
    }

    public bool IsDead(){
        return this.isDead;
    }

    public bool IsAlive(){
        return !this.isDead;
    }
}
