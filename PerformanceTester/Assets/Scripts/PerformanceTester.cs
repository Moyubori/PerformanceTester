using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerformanceTester : MonoBehaviour {

	public static PerformanceTester instance = null;

	public BenchmarkData dataStorage;

	ObserverPool observerPool;
	FPSCounter fpsCounter;
	List<ExecutionObserver> deployedObservers = new List<ExecutionObserver> ();

	void Awake(){
		// make sure that there's only one instance
		if (PerformanceTester.instance == null) {
			PerformanceTester.instance = this;
		} else {
			Destroy (this.gameObject);
		}
	}

	void Start(){
		observerPool = GetComponentInChildren<ObserverPool> ();
		if (observerPool == null) {
			Debug.LogError ("Can't find ObserverPool.");
		}
		fpsCounter = GetComponentInChildren<FPSCounter> ();
		if (fpsCounter == null) {
			Debug.LogError ("Can't find FPSCounter.");
		}
		dataStorage.StartBenchmark ();
		StartCoroutine (RunTester());
	}

	void OnApplicationQuit(){
		dataStorage.SaveDataToFile ();
	}

	IEnumerator RunTester(){
		while (true) {
			// can't use foreach because some elements might get removed form list during the loop
			for (int i = 0; i < deployedObservers.Count; i++) {
				SetMeasurementStatus (deployedObservers [i]);
			}
				
			yield return new WaitForSecondsRealtime (0.01f);
		}
	}

	void SetMeasurementStatus(ExecutionObserver observer){
		if (observer.GetProgress () == 1 && !observer.IsRunning ()) {
			Debug.Log (observer.GetProcessName () + " finished in time:" + observer.GetExecutionTime());
			observer.gameObject.SetActive (false);
			deployedObservers.Remove (observer);
		}
		dataStorage.LogProcess (observer.GetProcessName (), Time.realtimeSinceStartup, observer.GetExecutionTime (), observer.GetProgress ());
		//Debug.Log (observer.GetProcessName() + " progress:" + (observer.GetProgress() * 100) + "%");
	}

	public static ExecutionObserver GetExecutionObserver(){
		ExecutionObserver observer = instance.observerPool.GetInstance ();
		instance.deployedObservers.Add (observer);
		return observer;
	}

	public bool ValidateProcessName(string processName){
		return true;
	}
}
