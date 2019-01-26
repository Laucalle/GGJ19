using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

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

	// Use this for initialization
	void Start () {
		boxCollider = GetComponent<BoxCollider2D>();
		rigibody = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();

		currentDoor = null;

		playerStress = GetComponent<Stress>();
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKey (leftMovementKey)) {
			movePlayerLeft (movementSpeed * Time.deltaTime);
		} else if (Input.GetKey (rightMovementKey)) {
			movePlayerRight (movementSpeed * Time.deltaTime);
		} else if (Input.GetKeyDown (crossDoorKey)) {
			crossStairs ();
		}
	}

	private void crossStairs(){
		if (currentDoor != null) {
			transform.position = currentDoor.Cross();
			animator.SetTrigger ("Doors");
		}
	}
		
	private void movePlayerLeft(float distance){
		transform.Translate (Vector2.left * distance);
	}

	private void movePlayerRight(float distance){
		transform.Translate (Vector2.right * distance);
	}

	public void IncreaseStress(float quantity){
		playerStress.increaseStressLevel (quantity);
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
}
