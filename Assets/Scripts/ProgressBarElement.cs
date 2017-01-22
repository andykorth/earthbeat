using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ProgressBarElement : Script {

	public Image foreground;
	public Image midground;

	private RectTransform foregroundRT;
	private RectTransform backgroundRT;
	private RectTransform midgroundRT;

	public float minWidth = 32f;
	public Color originalColor;

	private Color animatedColor;
	private float animSpeed = 1.0f;
	private bool animating = false;
	private float current = 100;
	public ParticleSystem particleEffect;

	public Color startColor;

	public void SetAnimatingColor (Color color1, Color color2, float speed)
	{
		animating = true;

		originalColor = color1;
		animatedColor = color2;
		animSpeed = speed;
	}

	public void Awake(){
		animating = false;
		animatedColor = originalColor = foreground.color = startColor;
		backgroundRT = this.GetComponent<RectTransform>();

		foregroundRT = foreground.GetComponent<RectTransform> ();
		if(midground != null){
			midgroundRT = midground.GetComponent<RectTransform> ();
			midgroundRT.gameObject.SetActive(false);
		}

	}

	public void Update(){
		if (animating) {
			Color flashing = Color.Lerp (originalColor, animatedColor, Mathf.Sin (Time.time * animSpeed) * 0.5f + 0.5f);
			foreground.color = flashing;
		}
	}

	public void EnableParticles(bool b){
		//		Debug.Log ("Enable particles: " + b);
		foreach (ParticleSystem ps in GetComponentsInChildren<ParticleSystem>()) {
			var v = ps.emission;
			v.enabled = b;
		}
	}

	public void SetSolidColor(Color c){
		originalColor = c;
		animating = false;
		foreground.color = c;
	}

	public void SetPreviewProgress(float cents){
		if(backgroundRT == null) {
			Awake();
		}
		cents = Mathf.Clamp(cents, 0, 100);

		midgroundRT.gameObject.SetActive(true);

		SetBar(midground, midgroundRT, Color.red, current, backgroundRT.sizeDelta.x);
		SetBar(foreground, foregroundRT, originalColor, cents, backgroundRT.sizeDelta.x);
	}

	public void ClearPreview(){
		midgroundRT.gameObject.SetActive(false);
		SetProgress(current);
	}

	public void SetProgress(float cents){
		if(backgroundRT == null) {
			Awake();
		}
		cents = Mathf.Clamp(cents, 0, 100);

		current = cents;

		float maxWidth = backgroundRT.sizeDelta.x;

		SetBar(foreground, foregroundRT, originalColor, cents, maxWidth);
	}

	public void SetBar(Image image, RectTransform barRT, Color color, float cents, float maxWidth){
		if (cents <= 0f) {
			image.color = Color.clear;
		} else {
			image.color = color;
		}

		float p = cents / 100f;
		Vector2 r = barRT.sizeDelta;
		r.x = maxWidth * p;
		if(r.x < minWidth){
			// scale
			float scale = r.x / minWidth;
			barRT.localScale = Vector3.one * scale;
			r.x = minWidth;
		}else{
			barRT.localScale = Vector3.one;
		}

		barRT.sizeDelta = r;

	}



}
