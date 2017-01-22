using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolypController : MonoBehaviour {
	[Header("Controllers")]
	public Transform leftController;
	public Transform rightController;
	public Transform head;

	[Header("Extensions")]
	public Transform leftStickyHand;
	public Transform rightStickyHand;
	public Transform tongue;

	[Header("Rigged model nodes")]
	public Transform tongueNode;
	public Transform headNode;
	public Transform leftArmNode;
	public Transform leftHandNode;
	public Transform rightArmNode;
	public Transform rightHandNode;

	private Transform playerNode;

	// Use this for initialization
	void Start () {
		playerNode = transform;
	}
	
	// Update is called once per frame
	void Update () {
		playerNode.position = new Vector3 (head.position.x, playerNode.position.y, head.position.z);
		playerNode.rotation = Quaternion.Euler (0, head.rotation.eulerAngles.y, 0);

		headNode.rotation = head.transform.rotation;
		tongueNode.position = tongue.position;

		leftArmNode.position = leftController.position;
		leftArmNode.rotation = leftController.rotation;
		leftHandNode.position = leftStickyHand.position;

		rightArmNode.position = rightController.position;
		rightArmNode.rotation = rightController.rotation;
		rightHandNode.position = rightStickyHand.position;

	}
}
