using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour, CharacterStatus
{
    private bool canSlash = false;
    private bool canCharge = true;
    private bool isCharging = false;
    private bool isSlashing = false;
    private bool canBlink = true;
    private bool isDead = false;
    private bool hasBeenRoar = false;
    private bool canAct = false;
    private bool hasPlayEndScene = false;


    private float chargeCooldown = 10;
    private float chargeTime = 5;
    private float slashTime = 2f;
    private float blinkIdleTime = 1.0f;
    private float blinkCooldown  = 10f;
    private float attackRange = 4.5f;
    private float rangeToStartAttack = 6.5f;
    private float reapersAppearDelay = 1.0f;
    private float roarSlashSpeedFactor = 2;

    private int attack = 40;

    private bool isFacingRight = true;
    public Transform[] reaperSpawnPoints;
    public GameObject[] toDisableWhenEnd;
    public AudioClip castReaperSound;
    public GameObject reaper;
    public GameObject reapermanAppearEffect;
    public GameObject aura;
    public GameObject backgroundMusicPlayer;
    public Transform reapermanAppearEffectSpawnPoint;
    public GameObject endScene;
    public Image endSceneFade;
    private ArrayList reapers;
    public Transform player;
    private Animator animator;
    private PlayerSoundController soundController;
    private Damageable damageable;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        soundController = player.GetComponent<PlayerSoundController>();
        reapers = new ArrayList(3);
        damageable = GetComponent<Damageable>();
        Flip();
    }

    // Update is called once per frame
    void Update()
    {
        if ( !canAct ) return ;
        if( !isDead && (!isSlashing || hasBeenRoar)){
            Vector3 targetDir = player.position - transform.position;
            if ( (targetDir.x > 0 && !isFacingRight) || (targetDir.x < 0 && isFacingRight) ){
                Flip();
            }
            if ( !isSlashing ){
                if ( Mathf.Abs(targetDir.x) < rangeToStartAttack && Mathf.Abs(targetDir.y) < rangeToStartAttack ){
                    Slash();
                }
                else if ( CanCharge() ) {
                    Charge();
                }
                else if ( canBlink ) {
                    Blink();
                }
            }
            
        
            
            if ( damageable.CalHealthRatio() < 0.3 && !hasBeenRoar){
                hasBeenRoar = true;
                Roar();
            }
        }
        if ( isDead ){
            CheckReaper();
        }
        
    }

    public void SetCanAct(bool b){
        canAct = b;
    }

    void Roar(){
        aura.SetActive(true);
        GetComponent<AudioSource>().Play();
        animator.SetFloat("SlashSpeedFactor", roarSlashSpeedFactor);
        slashTime = slashTime / roarSlashSpeedFactor;
    }

    void Blink(){
        canBlink = false;
        Debug.Log("blink");
    }

    IEnumerator CooldownBlink(){
        yield return new WaitForSeconds(blinkCooldown);
        canBlink = true;
    }

    void Slash(){
        isSlashing = true;
        animator.SetBool("Attack", true);
        soundController.PlaySlash();
        StartCoroutine(CooldownSlash());
    }

    IEnumerator CooldownSlash(){
        yield return new WaitForSeconds(slashTime);
        isSlashing = false;
        animator.SetBool("Attack", false);
    }

    public void EndSlashing(){
        Debug.Log("End Slash");
        Vector3 targetDir = player.position - transform.position;
        if ( ((isFacingRight && targetDir.x < attackRange && targetDir.x > -0.5f) ||
             ( !isFacingRight && -targetDir.x < attackRange && targetDir.x < 0.5f)) && Mathf.Abs(targetDir.y) < attackRange){
                player.GetComponent<Damageable>().Attacked(attack);
                soundController.PlaySlashEnemy();
             }
        
    }

    void CastSpawnReaper(){
        GameObject effect3 = GameObject.Instantiate(reapermanAppearEffect, reapermanAppearEffectSpawnPoint.position, Quaternion.identity);
        effect3.SetActive(true);
        GetComponent<AudioSource>().PlayOneShot(castReaperSound);
        Destroy(effect3, chargeTime);
        StartCoroutine(SpawnReapers());
    }

    IEnumerator SpawnReapers(){
        yield return new WaitForSeconds(reapersAppearDelay);
        int i = Mathf.FloorToInt(Random.Range(0f,reaperSpawnPoints.Length-0.1f));
        GameObject newReaper1 = GameObject.Instantiate(reaper, reaperSpawnPoints[i].position, Quaternion.identity);
        GameObject newReaper2 = GameObject.Instantiate(reaper, reaperSpawnPoints[(i+1)%reaperSpawnPoints.Length].position, Quaternion.identity);
        reapers.Clear();
        reapers.Add(newReaper1);
        reapers.Add(newReaper2);

        newReaper1.SetActive(true);
        newReaper2.SetActive(true);
        GameObject effect1 = GameObject.Instantiate(reapermanAppearEffect, newReaper1.transform.position, Quaternion.identity);
        GameObject effect2 = GameObject.Instantiate(reapermanAppearEffect, newReaper2.transform.position, Quaternion.identity);
        effect1.SetActive(true);
        effect2.SetActive(true);
        Destroy(effect1, 1);
        Destroy(effect2, 1);
    }

    bool CanCharge(){
        bool reapersNull = true;
        foreach(GameObject gameObject in reapers){
            if ( gameObject != null){
                reapersNull = false;
            }
        }
        return canCharge && reapersNull;
    }

    void Charge(){
        canCharge = false;
        isCharging = true;
        animator.SetBool("Charge", true);
        CastSpawnReaper();
        StartCoroutine(CooldownCharge());
        StartCoroutine(StartCharging());
    }

    IEnumerator CooldownCharge(){
        yield return new WaitForSeconds(chargeCooldown);
        canCharge = true;
    }

    IEnumerator StartCharging(){
        yield return new WaitForSeconds(chargeTime);
        animator.SetBool("Charge", false);
        isCharging = false;
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
        animator.SetBool("Die", true);
        isDead = true;
    }

    void EndScene(){
        player.GetComponent<UnityStandardAssets._2D.MyCharacterUserControl>().SetCanControl(false);
        foreach(GameObject gameObject in toDisableWhenEnd){
            gameObject.SetActive(false);
        }
        AudioSource audioSource = backgroundMusicPlayer.GetComponent<AudioSource>();
        audioSource.Stop();
        audioSource.clip = backgroundMusicPlayer.GetComponent<BackgroundMusic>().endSong;
        audioSource.Play();
        endScene.SetActive(true);
        endSceneFade.canvasRenderer.SetAlpha(0);
        endSceneFade.CrossFadeAlpha(1, 3, false);
    }

    void CheckReaper(){
        bool hasReaper = false;
        foreach(GameObject reaper in reapers){
            if ( reaper != null){
                hasReaper = true;
            }
        }
        if ( !hasReaper && !hasPlayEndScene ){
            hasPlayEndScene = true;
            EndScene();
        }
    }
}
