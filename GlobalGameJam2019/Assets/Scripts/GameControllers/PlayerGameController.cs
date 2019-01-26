using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGameController : MonoBehaviour {

	[SerializeField]
	private Player player1, player2;

	[SerializeField]
	private float distanceThreshold;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (arePlayersClose ()) {
			player1.autoDecreaseStress ();
			player2.autoDecreaseStress ();
		} else {
			player1.autoIncreaseStress ();
			player2.autoIncreaseStress ();
		}

	}

	private bool arePlayersClose(){
		float distanceBetweenPlayers = Vector3.Distance (player1.transform.position,
			                               player2.transform.position);
		return distanceBetweenPlayers <= distanceThreshold;
	}
}
