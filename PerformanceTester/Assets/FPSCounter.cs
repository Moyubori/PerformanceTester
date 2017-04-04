using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour {

	// interval between fps checks given in seconds
	public float updateInterval = 1f;

	// reference to UI label
	private Text counterLabel;
	private int framesSinceLastCheck;
	private float calculatedFPS;

	void Awake(){
		framesSinceLastCheck = 0;
		calculatedFPS = 0;
	}

	void Start(){
		counterLabel = transform.GetComponentInChildren<Text> ();
		if (counterLabel == null) {
			Debug.LogError ("FPSCounter label not found");
		} else {
			InvokeRepeating ("CalculateFPS", 0, updateInterval);
		}
	}

	void Update(){
		framesSinceLastCheck++;
		UpdateCounterLabel ();
	}

	void CalculateFPS(){
		calculatedFPS = framesSinceLastCheck / updateInterval;
		framesSinceLastCheck = 0;
	}

	void UpdateCounterLabel(){
		counterLabel.text = "FPS:" + calculatedFPS.ToString() + " dt:" + Time.deltaTime.ToString();
	}

}
