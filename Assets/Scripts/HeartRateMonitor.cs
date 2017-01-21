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
	private static float timeSinceLastHeartbeat = 0;
	//private static Text testText;

	void Start()
	{
		heartRate = 70;

		heartbeatEvent = new UnityEvent();

		//GameObject text = GameObject.FindGameObjectWithTag("TestText");
		//testText = text.GetComponent<Text>();
		//testText.text = heartRate.ToString();
	}
	
	// Counts up from the last heartbeat
	void Update()
	{
		if (Input.GetKeyDown("up"))
			ReceiveChangeOfRate(10);
		if (Input.GetKeyDown("down"))
			ReceiveChangeOfRate(-10);

		timeSinceLastHeartbeat += Time.deltaTime;
		if (timeSinceLastHeartbeat > heartRate/60)
		{
			timeSinceLastHeartbeat = 0;
			BroadcastHeartbeat();
		}
	}

	// Updates the heart rate to the passed value.
	public static void ReceiveRate(int rate)
	{
		heartRate = rate;
		//testText.text = heartRate.ToString();
	}

	// Adds the passed value to the current heart rate.
	public static void ReceiveChangeOfRate(int delta)
	{
		heartRate = (heartRate + delta > 0) ? heartRate + delta : 1;
		//testText.text = heartRate.ToString();
	}

	public static void BroadcastHeartbeat() // this might be better as private
	{
		heartbeatEvent.Invoke();
	}
}
