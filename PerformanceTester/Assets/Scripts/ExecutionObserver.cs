using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExecutionObserver : MonoBehaviour {

	private PerformanceTester performanceTester;

	private bool running;
	private float progress;
	private string processName;

	private System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();

	void Awake(){
		performanceTester = PerformanceTester.instance;
		running = false;
	}

	public void Run(string processName){
		if (performanceTester.ValidateProcessName (processName)) {
			this.processName = processName;
		} else {
			Debug.LogError ("Process name already taken. Aborting...");
		}
		this.running = true;
		stopwatch.Reset ();
		stopwatch.Start ();
	}

	public void UpdateProgress(float progress){
		this.progress = Mathf.Clamp01 (progress);
	}

	public void Stop(){
		running = false;
		stopwatch.Stop ();
	}

	public bool IsRunning(){
		return running;
	}

	public float GetProgress(){
		return progress;
	}

	public string GetProcessName (){
		return processName;
	}

	public float GetExecutionTime(){
		if (running) {
			Debug.LogError ("Execution time read before process completion.");
			return -1;
		} else {
			return (float)stopwatch.ElapsedMilliseconds / 1000;
		}
	}

}
