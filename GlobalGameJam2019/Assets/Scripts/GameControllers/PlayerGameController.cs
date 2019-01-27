using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGameController : MonoBehaviour {

	[SerializeField]
	private Player player1, player2;

	[SerializeField]
	private float distanceThreshold;

	[SerializeField]
	private float stressRelieved, stressAdded;

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

	public void relieveStress(int playerID){
		if (playerID != player1.transform.GetInstanceID ()) {
			player1.IncreaseStress (stressAdded);
			player2.DecreaseStress (stressRelieved);
		} else {
			player2.IncreaseStress (stressAdded);
			player1.DecreaseStress (stressRelieved);
		}
	}

	public float getCurrentMaxStressLevel(){
		float player1Stress = player1.getCurrentStressLevel ();
		float player2Sstres = player2.getCurrentStressLevel ();

		if (player1Stress > player2Sstres) {
			return player1Stress;
		}
		return player2Sstres;
	}
}
