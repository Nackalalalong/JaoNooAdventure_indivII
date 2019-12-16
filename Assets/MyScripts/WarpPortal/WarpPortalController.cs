using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WarpPortalController : MonoBehaviour
{
    public string nextScene;
    private AudioSource audioSource;
    public GameObject player;
    public GameObject backgroundMusicPlayer;
    public bool track = false;
    public EnemyTracker enemyTracker;
    public GameObject warpEffect;

    void Start(){
        audioSource = GetComponent<AudioSource>();
    }
    public void OnTriggerEnter2D(Collider2D other){
        if ( track ){
            if ( !enemyTracker.CanWarp() ){
                return ;
            }
        }
        if ( other.gameObject.tag == "Player" ){
            audioSource.Play();
            player.GetComponent<UnityStandardAssets._2D.MyCharacterUserControl>().SetCanControl(false);
            backgroundMusicPlayer.GetComponent<AudioSource>().Pause();
            GameObject effect = GameObject.Instantiate(warpEffect, transform.position, Quaternion.identity);
            effect.SetActive(true);
            StartCoroutine(CastingWarp());
        }
    }

    IEnumerator CastingWarp(){
        yield return new WaitForSeconds(audioSource.clip.length-1.5f);
        SceneManager.LoadScene(nextScene);
    }
}
