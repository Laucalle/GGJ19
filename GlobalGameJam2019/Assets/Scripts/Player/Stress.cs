using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stress : MonoBehaviour {

	private const float MIN_STRESS_VALUE = 0;

	[SerializeField]
	private float maxStressValue;

	[SerializeField]
	private float stressAutoIncrease;

	private float currentStressLevel;


	// Use this for initialization
	void Start () {
		currentStressLevel = MIN_STRESS_VALUE;
	}
	
	// Update is called once per frame
	void Update () {
		increaseStressLevel (stressAutoIncrease * Time.deltaTime);
	}

	public void increaseStressLevel(float stressQuantity){
		float newStressLevel = currentStressLevel + stressQuantity;

		if (newStressLevel > maxStressValue) {
			currentStressLevel = maxStressValue;
		} else {
			currentStressLevel = newStressLevel;
		}
	}

	public void decreaseStressLevel(float stressQuantity){
		float newStressLevel = currentStressLevel - stressQuantity;

		if (newStressLevel < MIN_STRESS_VALUE) {
			currentStressLevel = MIN_STRESS_VALUE;
		} else {
			currentStressLevel = newStressLevel;
		}
	}

}
