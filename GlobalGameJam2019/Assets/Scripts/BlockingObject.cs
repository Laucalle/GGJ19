using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockingObject : MonoBehaviour {

    private Rigidbody2D rgbd;
    
	void Start () {
        rgbd = GetComponent<Rigidbody2D>();
    }
	
	void Update () {

	}
}
