using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

	private Door currentDoor;
	private Task currentTask;

	private Stress playerStress;

	private bool isPlayerStunned;
	private float remainingStunTime;
    private bool running;

	[SerializeField]
	private int stunDuration;

    public List<AnimatorOverrideController> animatorOverriders;

	// Use this for initialization
	void Start () {
		boxCollider = GetComponent<BoxCollider2D>();
		rigibody = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		playerStress = GetComponent<Stress>();

		currentDoor = null;
		isPlayerStunned = false;
		remainingStunTime = 0.0f;
        running = false;

        InitializeAnimator();
    }
	
	// Update is called once per frame
	void Update () {
        if (!isPlayerStunned) {
            if (!playerStress.isStressMaxed()) {
                if (Input.GetKey(leftMovementKey)) {
                    movePlayerLeft(movementSpeed * Time.deltaTime);
                } else if (Input.GetKey(rightMovementKey)) {
                    movePlayerRight(movementSpeed * Time.deltaTime);
                } else if (Input.GetKeyDown(crossDoorKey)) {
                    crossStairs();
                } else if (Input.GetKeyDown(stressReliefKey)) {
                    relieveStress();
                } else {
                    animator.SetBool("Running", false);
                    running = false;
                }

            }
        }

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
		isPlayerStunned = true;
		animator.SetTrigger ("Stun");
		Invoke ("unStunPlayer", stunDuration);
	}

	public void unStunPlayer(){
		isPlayerStunned = false;
		animator.SetTrigger ("Unstun");
		playerStress.resetStress();
	}

	public void relieveStress(){
		playerGameController.relieveStress (transform.GetInstanceID ());
	}

    /*
	private void startRunningRightAnimation(){
		sprite.GetComponent<SpriteRenderer> ().flipX = false;
		//animator.Play ("RunningRight");
		animator.SetTrigger ("runRight");
	}

	private void startRunningLeftAnimation(){
		sprite.GetComponent<SpriteRenderer> ().flipX = true;
		//animator.Play ("RunningLeft");
		animator.SetTrigger ("runLeft");
	}
    */

    private void RunningLookingRight(bool right) {
        if (!running) {
            running = true;
            animator.SetBool("Running", true);
        }

        GetComponent<SpriteRenderer>().flipX = !right;
    }
}
