using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashController : MonoBehaviour
{
    // Update is called once per frame
    private int slashDamage;
    private PlayerStatus playerStatus;
    private PlayerSoundController playerSoundController;

    void Start(){
        slashDamage = gameObject.transform.parent.GetComponent<NinjaActionController>().slashDamage;
        playerStatus = gameObject.transform.parent.GetComponent<PlayerStatus>();
        playerSoundController = gameObject.transform.parent.GetComponent<PlayerSoundController>();
    }
    
    public void CheckSlash(){
        bool completedAttack = false;
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, new Vector2(3,4),0);
        foreach(Collider2D collider in colliders ){
            if( collider.gameObject.layer == 11 ){
                collider.gameObject.GetComponent<Damageable>().Attacked(slashDamage);
                if ( collider.gameObject.tag != "Boss"){
                    collider.gameObject.GetComponent<Animator>().SetBool("Attack", false);
                }
                completedAttack = true;
            }
        }
        if ( completedAttack ){
            playerStatus.status += 5;
            playerSoundController.PlaySlashEnemy();
        }
    }
}
