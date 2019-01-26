using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private BoxCollider2D boxCollider;
    private Rigidbody2D rb2D;
    private Animator animator;
    public LayerMask blockingLayer;
    private AudioSource audioSource;

    public int stress;

    // Start is called before the first frame update
    void Start() {
        boxCollider = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        activated = true;
        death = false;

        stress = 0;
    }

    // Update is called once per frame
    void Update() {
        



    }
}
