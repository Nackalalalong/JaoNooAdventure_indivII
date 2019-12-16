using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KunaiController : MonoBehaviour
{
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start(){
        audioSource = GetComponent<AudioSource>();
    }
    void OnTriggerEnter2D(Collider2D other){
        if ( other.gameObject.layer == 11 || other.gameObject.layer == 8 || other.gameObject.tag == "Asteroid" ){
            if ( other.gameObject.layer == 11 ){
                other.gameObject.GetComponent<Damageable>().Attacked(GetComponent<Attackable>().attack);
                audioSource.Play();
                Debug.Log("Play Kunai hit sound");
            }
            Destroy(gameObject.GetComponent<Collider2D>());
            Destroy(gameObject.GetComponent<SpriteRenderer>());
            Destroy(gameObject,2);
        }
        
    }

}
