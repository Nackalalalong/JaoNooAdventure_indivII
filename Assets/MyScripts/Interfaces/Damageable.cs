using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour 
{
    public int maxHealth = 1;
    private int health;
    public bool bloodWhenAttacked = true;
    private GameObject blood;
    public AudioClip enemyDeadSound;

    void Start(){
        health = maxHealth;
        blood = GameObject.Find("Blood");
    }

    public void Die(){
        GetComponent<CharacterStatus>().Die();
        if ( bloodWhenAttacked ){
            GameObject newBlood = Instantiate(blood,transform.position, Quaternion.identity);
            newBlood.SetActive(true);
            Destroy(newBlood, 0.4f);
        }
        if ( gameObject.layer == 11 ){
            gameObject.GetComponent<AudioSource>().PlayOneShot(enemyDeadSound);
        }
    }
    public void Attacked(int damage){
        health -= damage;
        if ( health < 0 ){
            health = 0;
            Die();
            return ;
        }    
        if ( bloodWhenAttacked ){
            GameObject newBlood = Instantiate(blood,transform.position, Quaternion.identity);
            newBlood.SetActive(true);
            Destroy(newBlood, 0.4f);
        }
    }

    public float CalHealthRatio(){
        return (float)health / (float)maxHealth;
    }
}
