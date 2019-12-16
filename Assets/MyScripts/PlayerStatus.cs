using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour, CharacterStatus
{
    private PlayerActionable currentController;
    private NinjaActionController ninjaController;
    private PlayerActionable warriorController;
    private PlayerSoundController playerSoundController;

    private float boyCapsuleColliderYOffSet = -0.1347594f;
    private float boyCapsuleColliderYSize = 4.489583f;
    private float ninjaCapsuleColliderYOffSet = -0.01563072f;
    private float ninjaCapsuleColliderYSize = 4.303131f;

    private Animator animator;
    private CapsuleCollider2D capsuleCollider2D;
    private TransformationEffectSpawner transformationEffectSpawner;

    public float status = 0;
    private float maxStatus = 100;
    public float increaseStatusRate = 2f;
    private int transformation = 0; // 0 normal 1 ninja 2 warrior
    private int currentTransformation = 0;
    private bool hasStatusBar = true;
    private bool canTransformToNinja = true;
    private bool canTransformToWarrior = false;
    private UnityStandardAssets._2D.MyCharacterUserControl myCharacterUserControl;
    public GameObject gameOver;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        if ( hasStatusBar ){
            InvokeRepeating("AutoIncreaseStatus", 1, 1);
        }
        myCharacterUserControl = GetComponent<UnityStandardAssets._2D.MyCharacterUserControl>();
        transformationEffectSpawner = GetComponent<TransformationEffectSpawner>();
        ninjaController = GetComponent<NinjaActionController>();
        playerSoundController = GetComponent<PlayerSoundController>();
    }

    // Update is called once per frame
    void Update()
    {
        ValidateStatus();
    }

    void LateUpdate(){
        CheckTransformation();
    }

    public void Die(){
        animator.SetBool("Dead", true);
        myCharacterUserControl.SetCanControl(false);
        gameOver.SetActive(true);
    }

    private void CheckTransformation(){
        if ( Input.GetKeyUp(KeyCode.T) ){
            if ( transformation != 1){
                transformation = 1;
            }
            else {
                transformation = 0;
            }
        }
        else if ( Input.GetKeyUp(KeyCode.Y) ){
            if ( transformation != 2 ){
                transformation = 2;
            }
            else {
                transformation = 0;
            }
        }
        if ( currentTransformation != transformation ){
            currentTransformation = transformation;
            if ( transformation == 0 ){
                TransformToBoy();
            }
            else if ( transformation == 1 ){
                TransformToNinja();
            }
            else if ( transformation == 2 ){
                TransformToWarrior();
            }
            transformationEffectSpawner.SpawnTransformationEffect();
            playerSoundController.PlayTransform();
        }
    }

    void FixedUpdate(){

    }

    public float CalStatusRatio(){
        return status / maxStatus;
    }

    private void AutoIncreaseStatus(){
        status += increaseStatusRate;
        ValidateStatus();
    }

    private void ValidateStatus(){
        if ( status > maxStatus ){
            status = maxStatus;
        }
    }

    public void TransformToBoy(){
        currentController.SetActive(false);
        animator.runtimeAnimatorController = 
            Resources.Load<RuntimeAnimatorController>("MyAnimations/Player/BoyAnim");
            animator.Play("BoyIdle");
        capsuleCollider2D.offset = new Vector2(capsuleCollider2D.offset.x, boyCapsuleColliderYOffSet);
        capsuleCollider2D.size = new Vector2(capsuleCollider2D.size.x, boyCapsuleColliderYSize);
    }

    public void TransformToNinja(){
        currentController = ninjaController;
        currentController.SetActive(true);
        animator.runtimeAnimatorController = 
            Resources.Load<RuntimeAnimatorController>("MyAnimations/Ninja/NinjaAnim");
            animator.Play("Idle");
        capsuleCollider2D.offset = new Vector2(capsuleCollider2D.offset.x, ninjaCapsuleColliderYOffSet);
        capsuleCollider2D.size = new Vector2(capsuleCollider2D.size.x, ninjaCapsuleColliderYSize);

    }

    public void TransformToWarrior(){

    }
}
