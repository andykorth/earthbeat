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
	private static AudioSource heartbeatAudio;

	void Start()
	{
		heartRate = 60;
		timeSinceLastHeartbeat = 0;

		heartbeatEvent = new UnityEvent();

		GameObject gameObject = GameObject.FindGameObjectWithTag("HRM");
		heartbeatAudio = gameObject.GetComponent<AudioSource>();
	}
	
	// Counts up from the last heartbeat
	void Update()
	{
		if (Input.GetKeyDown("up"))
			ReceiveChangeOfRate(10);
		if (Input.GetKeyDown("down"))
			ReceiveChangeOfRate(-10);

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
	}
}
