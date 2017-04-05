using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObserverPool : MonoBehaviour {

	public ExecutionObserver executionObserverPrefab;

	private List<ExecutionObserver> observerInstances = new List<ExecutionObserver>();

	void Start(){
		if (executionObserverPrefab == null) {
			Debug.LogError ("ExecutionObserver prefab not set in the ObserverPool.");
		}
	}

	public ExecutionObserver GetInstance(){
		foreach (ExecutionObserver instance in observerInstances) {
			if (!instance.gameObject.activeSelf) {
				instance.gameObject.SetActive (true);
				return instance;
			}
		}
		ExecutionObserver newInstance = Instantiate (executionObserverPrefab);
		observerInstances.Add (newInstance);
		newInstance.transform.parent = transform;
		return newInstance;
	}

	public int InstancesInPool(){
		return observerInstances.Count;
	}

}
