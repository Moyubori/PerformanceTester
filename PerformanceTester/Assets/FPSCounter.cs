using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour {

	public bool showFPS = true;
	public bool showDT = true;

	// interval between fps checks given in seconds
	public float updateInterval = 1f;

	// references to UI labels
	public Text fpsLabel;
	public Text dtLabel;

	private int framesSinceLastCheck;
	private float calculatedFPS;
	private bool calculateFPSCoroutineRunning;

	void Awake(){
		framesSinceLastCheck = 0;
		calculatedFPS = 0;
	}

	void Update(){
		if (showFPS) {
			framesSinceLastCheck++;
			if (!calculateFPSCoroutineRunning) {
				StartCoroutine (CalculateFPS ());
			}
		}
		UpdateLabels ();
	}

	IEnumerator CalculateFPS(){
		calculateFPSCoroutineRunning = true;
		while(showFPS){
			calculatedFPS = framesSinceLastCheck / updateInterval;
			framesSinceLastCheck = 0;
			yield return new WaitForSeconds (updateInterval);
		}
		calculateFPSCoroutineRunning = false;
		yield break;
	}

	void UpdateLabels(){
		if (showFPS) {
			if (!fpsLabel.gameObject.activeSelf) {
				fpsLabel.gameObject.SetActive (true);
			}
			fpsLabel.text = "FPS:" + calculatedFPS.ToString ("0.0");
		} else {
			fpsLabel.gameObject.SetActive (false);
		}
		if (showDT) {
			if (!dtLabel.gameObject.activeSelf) {
				dtLabel.gameObject.SetActive (true);
			}
			dtLabel.text = "DT:" + Time.deltaTime.ToString ();
		} else {
			dtLabel.gameObject.SetActive (false);
		}
	}

}
