using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollows : MonoBehaviour {

	public Transform Target;
	public float Smoothing = 5f;

	Vector3 _offset;

	// Use this for initialization
	void Start () {
		_offset = transform.position - Target.position;
	}

	void FixedUpdate () {
		var targetCamPos = Target.position + _offset;
		transform.position = Vector3.Lerp (transform.position, targetCamPos, Smoothing + Time.deltaTime);
	}
}
