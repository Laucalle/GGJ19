using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	[SerializeField]
	private PlayerGameController playerGameController;

	[SerializeField]
	private GameObject sprite;

	[SerializeField]
	private float movementSpeed;

	[SerializeField]
	private KeyCode 
		leftMovementKey,
		rightMovementKey;

	[SerializeField]
	private KeyCode crossDoorKey;
	private KeyCode solveTaskKey;

	[SerializeField]
	private KeyCode stressReliefKey;

	private BoxCollider2D boxCollider;
	private Rigidbody2D rigibody;
	private Animator animator;
	public LayerMask blockingLayer;
	private AudioSource audioSource;
    private Stress playerStress;

    [SerializeField]
    private Canvas PlayerCanvas;
    private Image StressImage;

    private Door currentDoor;
	private Task currentTask;

	private bool isPlayerStunned;

    private bool currentlyRunning;

    private bool currentlyWorking;
    private float workingDuration;
    private float workingTime;

    [SerializeField]
	private int stunDuration;

    public List<AnimatorOverrideController> animatorOverriders;

	[SerializeField]
	private List<KeyCode> possibleSolveTaskKeycodes;

	[SerializeField]
	private float timeBetweenChangeSolveTaskKey;

	// Use this for initialization
	void Start () {
		boxCollider = GetComponent<BoxCollider2D>();
		rigibody = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		playerStress = GetComponent<Stress>();

        InitializeAnimator();

        currentDoor = null;
        currentTask = null;

		isPlayerStunned = false;

        stunDuration = 3;
        currentlyRunning = false;

        currentlyWorking = false;
        workingDuration = 0.5f;
        
        changeSolveTaskKeyAtRandom ();

        if (PlayerCanvas == null) {
            Debug.Log("FALTA ASIGNAR EL PLAYER_CANVAS");
        } else {
            StressImage = PlayerCanvas.transform.GetChild(0).GetComponent<Image>().transform.GetChild(0).GetComponent<Image>();
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (playerStress.isStressMaxed()) {
            stunPlayer();
        }

        if (!isPlayerStunned) {
            if (currentTask != null && (currentlyWorking || Input.GetKeyDown(solveTaskKey)) ) {
                Working();
            } else if (currentDoor != null && Input.GetKeyDown(crossDoorKey)) {
                crossStairs();
            } else {
                if (Input.GetKey(leftMovementKey)) {
                    movePlayerLeft(movementSpeed * Time.deltaTime);
                } else if (Input.GetKey(rightMovementKey)) {
                    movePlayerRight(movementSpeed * Time.deltaTime);
                } else {
                    animator.SetBool("Running", false);
                    currentlyRunning = false;
                }
            }
        }

        StressImage.fillAmount = playerStress.getCurrentStressNormalized();
        Debug.Log(solveTaskKey);
    }

    private void Working() {
        if (currentlyWorking) {
            workingTime -= Time.deltaTime;
            currentlyWorking = workingTime > 0;
        }
        if (Input.GetKeyDown(solveTaskKey) && currentTask.WorkOn(this)) {
            currentlyWorking = true;
            workingTime = workingDuration;
        }
    
        
        animator.SetBool("Working", currentlyWorking);
    }

	private void crossStairs() {
		if (currentDoor != null) {
			transform.position = currentDoor.Cross();
			animator.SetTrigger ("Doors");
		}
	}
		
	private void movePlayerLeft(float distance){
        RunningLookingRight(false);
        transform.Translate (Vector2.left * distance);
	}

	private void movePlayerRight(float distance){
        RunningLookingRight(true);
        transform.Translate (Vector2.right * distance);
	}

    private void InitializeAnimator() {
        int randomNumber = Random.Range(0, animatorOverriders.Count);
        animator.runtimeAnimatorController = animatorOverriders[randomNumber];
    }

	public void autoIncreaseStress(){
		playerStress.autoIncreaseStressLevel ();
	}

	public void autoDecreaseStress(){
		playerStress.autoDecreaseStressLevel ();
	}

	public void IncreaseStress(float quantity){
		playerStress.increaseStressLevel (quantity);
	}

	public void DecreaseStress(float quantity){
		playerStress.decreaseStressLevel (quantity);
	}

	void OnTriggerEnter2D(Collider2D collisionObject){
		if (collisionObject.gameObject.GetComponent<Door> () != null) {
			currentDoor = collisionObject.gameObject.GetComponent<Door> ();
		} else if (collisionObject.gameObject.GetComponent<Task>() != null){
			currentTask = collisionObject.gameObject.GetComponent<Task>();
		}
	}

	void OnTriggerExit2D(Collider2D collisionObject){
		if (collisionObject.gameObject.GetComponent<Door> () != null) {
			currentDoor = null;
		} else if (collisionObject.gameObject.GetComponent<Task>() != null){
			currentTask = null;
		}
	}

	public void stunPlayer(){
        if (!isPlayerStunned) {
            isPlayerStunned = true;
            animator.SetTrigger("Stun");
            Invoke("unStunPlayer", stunDuration);
        }
    }

	public void unStunPlayer(){
		isPlayerStunned = false;
		animator.SetTrigger ("Unstun");
		playerStress.resetStress();
	}

	public void relieveStress(){
		playerGameController.relieveStress (transform.GetInstanceID ());
	}
    
    private void RunningLookingRight(bool right) {
        if (!currentlyRunning) {
            currentlyRunning = true;
            animator.SetBool("Running", true);
        }

        GetComponent<SpriteRenderer>().flipX = !right;
    }

	private void changeSolveTaskKeyAtRandom(){
		solveTaskKey = possibleSolveTaskKeycodes[Random.Range(0,possibleSolveTaskKeycodes.Count-1)];
		Invoke ("changeSolveTaskKeyAtRandom", timeBetweenChangeSolveTaskKey);
	}

	public string solveTaskKeyCodeString(){
		return solveTaskKey.ToString();
	}

    public float getCurrentStressLevel() {
        return playerStress.getCurrentStressLevel();
    } 

}
