using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundController : MonoBehaviour
{
    public AudioClip slash;
    public AudioClip slashEnemy;
    public AudioClip throwKunai;
    public AudioClip transformSound;
    public AudioClip endJump;
    private AudioSource audioSource;

    private bool isJumpSoundReady = true;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void PlaySlash(){
        audioSource.clip = slash;
        audioSource.Play();
    }

    public void PlaySlashEnemy(){
        audioSource.clip = slashEnemy;
        audioSource.PlayOneShot(slashEnemy);
    }

    public void PlayThrowKunai(){
        audioSource.clip = throwKunai;
        audioSource.Play();
    }

    public void PlayTransform(){
        audioSource.PlayOneShot(transformSound);  
    }

    public void EndJump(){
        audioSource.PlayOneShot(endJump);
    }
}
