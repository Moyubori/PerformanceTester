using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour {

	public bool showFPS = true;
	public bool showMinFPS = true;
	public bool showMaxFPS = true;
	public bool showDT = true;

	// interval between fps checks given in seconds
	[Range(0.1f, 2)]
	public float updateInterval = 1f;

	// references to UI labels
	public Text fpsLabel;
	public Text minFPSLabel;
	public Text maxFPSLabel;
	public Text dtLabel;

	private int framesSinceLastCheck;
	private float calculatedFPS = -1;
	private float minFPS = -1;
	private float maxFPS = -1;
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
		yield return new WaitForSecondsRealtime (updateInterval);
		while(showFPS){
			calculatedFPS = framesSinceLastCheck / updateInterval;
			framesSinceLastCheck = 0;
			if (calculatedFPS < minFPS || minFPS == -1) {
				minFPS = calculatedFPS;
			}
			if (calculatedFPS > maxFPS || maxFPS == -1) {
				maxFPS = calculatedFPS;
			}

			yield return new WaitForSecondsRealtime (updateInterval);
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
		if (showMinFPS) {
			if (!minFPSLabel.gameObject.activeSelf) {
				minFPSLabel.gameObject.SetActive (true);
			}
			minFPSLabel.text = "Min FPS:" + minFPS.ToString ("0.0");
		} else {
			fpsLabel.gameObject.SetActive (false);
		}
		if (showMaxFPS) {
			if (!maxFPSLabel.gameObject.activeSelf) {
				maxFPSLabel.gameObject.SetActive (true);
			}
			maxFPSLabel.text = "Max FPS:" + maxFPS.ToString ("0.0");
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
