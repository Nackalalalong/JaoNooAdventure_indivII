using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateController : MonoBehaviour
{
    public AudioClip crateHit;
    public AudioClip enemyGotHit;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if( other.gameObject.tag == "Zombie" ){
            other.gameObject.GetComponent<EnemyController>().Die();
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), other.gameObject.GetComponent<Collider2D>());
            PlayHitSound();
        }
        if ( other.gameObject.tag == "NoobZombie" ){
            other.gameObject.GetComponent<Damageable>().Die();
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), other.gameObject.GetComponent<Collider2D>());
            PlayHitSound();
        }
    }

    void PlayHitSound(){
        audioSource.PlayOneShot(enemyGotHit, 0.5f);
        audioSource.PlayOneShot(crateHit, 0.8f);
    }
}
