﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
    
    private BoxCollider2D boxCollider;

    // Asignar externamente
    public Door connectedDoor;

    // Use this for initialization
    void Start () {
        boxCollider = GetComponent<BoxCollider2D>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

	public Vector2 Cross() {
		return connectedDoor.transform.position;
    }

    public Door ConnectedDoor() {
        return connectedDoor;
    }
}
