using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public string spawningProcessName;
	public GameObject cubePrefab;

	public int cubesToSpawn = 1000;

	void Start(){
		if (cubePrefab == null) {
			Debug.LogError ("Cube prefab not found");
		}
		if (spawningProcessName == null) {
			Debug.LogError ("No spawning process name");
		}
		StartCoroutine(SpawnStuff ());
	}

	IEnumerator SpawnStuff(){
		ExecutionObserver observer = PerformanceTester.GetExecutionObserver ();
		Debug.Log (spawningProcessName + observer);
		observer.Run (spawningProcessName);
		for (int i = 0; i < cubesToSpawn; i++) {
			GameObject spawnedCube = Instantiate (cubePrefab);
			spawnedCube.transform.parent = transform;
			spawnedCube.transform.position = new Vector3 (Mathf.Sin(Random.Range(0,6.28f))*10,Mathf.Cos(Random.Range(0,6.28f))*10);
			observer.UpdateProgress ((float)(i + 1) / cubesToSpawn);
			yield return null;
		}
		observer.Stop ();
		yield break;
	}

}
