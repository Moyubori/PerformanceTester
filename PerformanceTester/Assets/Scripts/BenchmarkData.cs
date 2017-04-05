using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BenchmarkData", menuName = "BenchmarkData", order = 1)]
public class BenchmarkData : ScriptableObject {

	[System.Serializable]
	struct FPSDataStruct{
		public FPSDataStruct(float systemTime, float fps){
			this.applicationTime = systemTime;
			this.fps = fps;
		}

		public float applicationTime { get; private set; }
		public float fps { get; private set; }
	}

	[System.Serializable]
	struct ProcessDataStruct{
		public ProcessDataStruct(string processName, float systemTime, float processTime, float progress){
			this.processName = processName;
			this.applicationTime = systemTime;
			this.processTime = processTime;
			this.progress = progress;
		}

		public string processName { get; private set; }
		public float applicationTime { get; private set; }
		public float processTime { get; private set; }
		public float progress { get; private set; }
	}

	public string logsPath = "Assets/Logs/";
	public int currentBenchmark = -1;

	[SerializeField]
	private List<FPSDataStruct> fpsData;
	[SerializeField]
	private List<ProcessDataStruct> processData;

	public void StartBenchmark(){
		currentBenchmark++;
		fpsData = new List<FPSDataStruct> ();
		processData = new List<ProcessDataStruct> ();
	}

	public void LogFPS(float systemTime, float fps){
		FPSDataStruct log = new FPSDataStruct (systemTime, fps);
		fpsData.Add (log);
	}

	public void LogProcess(string processName, float systemTime, float processTime, float progress){
		ProcessDataStruct log = new ProcessDataStruct (processName, systemTime, processTime, progress);
		processData.Add (log);
	}

	public void SaveDataToFile(){
		System.IO.StreamWriter fpsLogWriter = new System.IO.StreamWriter (logsPath + "/FPSLog" + currentBenchmark.ToString () + ".txt", true);
		foreach (FPSDataStruct fpsLog in fpsData) {
			fpsLogWriter.WriteLine (fpsLog.applicationTime + ";" + fpsLog.fps);
		}
		fpsLogWriter.Close ();
		System.IO.StreamWriter processLogWriter = new System.IO.StreamWriter (logsPath + "/ProcessLog" + currentBenchmark.ToString () + ".txt", true);
		foreach (ProcessDataStruct processLog in processData) {
			processLogWriter.WriteLine (processLog.applicationTime + ";" + processLog.processName + ";" + processLog.processTime + ";" + processLog.progress);
		}
		processLogWriter.Close ();
	}

}
