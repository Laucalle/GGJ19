using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	[SerializeField]
	private float movementSpeed;

	[SerializeField]
	private KeyCode 
		upMovementKey,
		downMovementKey,
		leftMovementKey,
		rightMovementKey;

	private KeyCode solveTaskKey;

	[SerializeField]
	private KeyCode stressReliefKey;

	private BoxCollider2D boxCollider;
	private Rigidbody2D rigibody;
	private Animator animator;
	public LayerMask blockingLayer;
	private AudioSource audioSource;

	// Use this for initialization
	void Start () {
		boxCollider = GetComponent<BoxCollider2D>();
		rigibody = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (leftMovementKey)) {
			movePlayerLeft (movementSpeed * Time.deltaTime);
		} else if(Input.GetKey (rightMovementKey)){
			movePlayerRight (movementSpeed * Time.deltaTime);
		}
	}

	private void movePlayerUp(float distance){
		//TODO
	}

	private void movePlayerDown(float distance){
		//TODO
	}

	private void movePlayerLeft(float distance){
		transform.Translate (Vector2.left * distance);
	}

	private void movePlayerRight(float distance){
		transform.Translate (Vector2.right * distance);
	}

	public void IncreaseStress(float quantity){
		
	}
}
