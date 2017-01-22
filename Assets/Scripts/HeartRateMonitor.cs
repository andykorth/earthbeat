using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HeartRateMonitor : MonoBehaviour
{

	private static HeartRateMonitor heartRateMonitor;

	public static UnityEvent heartbeatEvent;
	public static int heartRate;
	private static float timeSinceLastHeartbeat;
	public static GameObject heartbeatSphere;
	private static AudioSource heartbeatAudio;

	void Start()
	{
		heartRate = 60;
		timeSinceLastHeartbeat = 0;

		heartbeatSphere = (GameObject) Resources.Load("HeartbeatSphere");
		heartbeatEvent = new UnityEvent();
		heartbeatAudio = gameObject.GetComponent<AudioSource>();
	}
	
	// Counts up from the last heartbeat
	void Update()
	{
		timeSinceLastHeartbeat += Time.deltaTime;
		if (timeSinceLastHeartbeat > 60.0/heartRate)
		{
			timeSinceLastHeartbeat = (float) (timeSinceLastHeartbeat % (60.0/heartRate));
			BroadcastHeartbeat();
		}
	}

	// Updates the heart rate to the passed value.
	public static void ReceiveRate(int rate)
	{
		heartRate = rate;
	}

	// Adds the passed value to the current heart rate.
	public static void ReceiveChangeOfRate(int delta)
	{
		heartRate = (heartRate + delta > 0) ? heartRate + delta : 1;
	}

	public static void BroadcastHeartbeat() // this might be better as private
	{
		heartbeatEvent.Invoke();
		heartbeatAudio.Play();
		Instantiate(heartbeatSphere); // Instantiating here defeats the purpose
									  // of broadcasting the heartbeat event!
	}
}
