using UnityEngine;
using System;

public class Mathfx
{
	public static float Hermite(float start, float end, float value)
	{
		value = Mathf.Clamp01(value);
		return Mathf.Lerp(start, end, value * value * (3.0f - 2.0f * value));
	}
	
	public static float Sinerp(float start, float end, float value)
	{
		value = Mathf.Clamp01(value);
		return Mathf.Lerp(start, end, Mathf.Sin(value * Mathf.PI * 0.5f));
	}

	public static float Coserp(float start, float end, float value)
	{
		value = Mathf.Clamp01(value);
		return Mathf.Lerp(start, end, 1f - Mathf.Cos(value * Mathf.PI * 0.5f));
	}

	public static float Berp(float start, float end, float value)
	{
		value = Mathf.Clamp01(value);
		value = (Mathf.Sin(value * Mathf.PI * (0.2f + 2.5f * value * value * value)) * Mathf.Pow(1f - value, 2.2f) + value) * (1f + (1.2f * (1f - value)));
		return start + (end - start) * value;
	}
	
	public static float Lerp(float start, float end, float value)
	{
		return ((1.0f - value) * start) + (value * end);
	}
	
	public static float Exponential(float start, float end, float value, float power) {
		//value = Mathf.Clamp01(value);
		return Mathf.Lerp(start, end, Mathf.Pow(value, power));
	}
	
	public static float Exponential(float start, float end, float value) {
		return Exponential(start, end, value, 2f);
	}
	
	public static float DragBounce(float start, float end, float value) {
		value = Mathf.Clamp01(value);
		return Mathf.Lerp(start, end, 6.75f * value * (1f - value) * (1f - value));
	}
	
	public static float Parabolic(float start, float end, float value) {
		return Mathf.Lerp(start, end, (2 * value) - (2 * value * value));
	}
	
	public static float ClampAngle (float angle, float min, float max)
	{
		while (angle < -360f)
			angle += 360f;
		while (angle > 360f)
			angle -= 360f;
		return Mathf.Clamp (angle, min, max);
	}
	
	public static float Parameter(float start, float middle, float end, float value) {
		if (value < 0.5f)
			return Mathf.Lerp(start, middle, (0.5f - value) * 2f);
		else
			return Mathf.Lerp(middle, end, (value - 0.5f) * 2f);
	}
}
