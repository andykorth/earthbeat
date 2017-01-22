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

	private static AudioSource heartbeatAudio1;
	private static AudioSource heartbeatAudio2;
	private static float audioTimer;
	private static bool heartbeatPlaying;

	private static float timeSinceSlowdown;

	void Start()
	{
		heartRate = 60;
		timeSinceLastHeartbeat = 0;

		heartbeatSphere = (GameObject) Resources.Load("HeartbeatSphere");
		heartbeatEvent = new UnityEvent();
		heartbeatAudio1 = gameObject.GetComponents<AudioSource>()[0];
		heartbeatAudio2 = gameObject.GetComponents<AudioSource>()[1];

		audioTimer = 0;
		heartbeatPlaying = false;

		timeSinceSlowdown = 0;
	}
	
	// Counts up from the last heartbeat
	void Update()
	{
		timeSinceLastHeartbeat += Time.deltaTime;
		if (timeSinceLastHeartbeat > 60.0/heartRate)
		{
			timeSinceLastHeartbeat = (float) (timeSinceLastHeartbeat % (60f/heartRate));
			BroadcastHeartbeat();
		}

		if (heartbeatPlaying)
			audioTimer += Time.deltaTime;
		if (audioTimer >= (60f/4f)/heartRate)
		{
			heartbeatAudio2.Play();
			audioTimer = 0;
			heartbeatPlaying = false;
		}

		if (heartRate > 40)
		{
			timeSinceSlowdown += Time.deltaTime;
			if (timeSinceSlowdown >= 3 && timeSinceSlowdown >= 12 - heartRate/10)
			{
				heartRate -= 10;
				timeSinceSlowdown = 0;
			}
		}
	}

	// Updates the heart rate to the passed value.
	public static void ReceiveRate(int rate)
	{
		heartRate = rate;
		heartRate = (heartRate > 0) ? ((heartRate < 200) ? heartRate : 200) : 1;
		timeSinceSlowdown = 0;
	}

	// Adds the passed value to the current heart rate.
	public static void ReceiveChangeOfRate(int delta)
	{
		heartRate = (heartRate + delta > 0) ? ((heartRate + delta < 200) ? heartRate + delta : 200) : 1;
	}

	public static void BroadcastHeartbeat() // this might be better as private
	{
		heartbeatEvent.Invoke();
		heartbeatAudio1.Play();
		heartbeatPlaying = true;
	
		Instantiate(heartbeatSphere); // Instantiating here defeats the purpose
									  // of broadcasting the heartbeat event!
	}
}
