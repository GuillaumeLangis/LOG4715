using UnityEngine;
using System.Collections;

public class RollingBouldersController : MonoBehaviour {

	Vector3 originalPos;
	Quaternion originalRot;

	// Use this for initialization
	void Start () {
		originalPos = transform.position;
		originalRot = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.timeSinceLevelLoad > 1) {
			if (Time.timeSinceLevelLoad % 15 >= -0.5 && Time.timeSinceLevelLoad % 15 <= 0.5) {
				Explode ();
				Hide ();
			}
		}
	}

	void Reset () {
		Show ();
		transform.position = originalPos;
		transform.rotation = originalRot;
		rigidbody.velocity = Vector3.zero;
		rigidbody.angularVelocity = Vector3.zero;
	}

	void Explode () {
		var exp = GetComponent<ParticleSystem> ();
		exp.Play ();
		Invoke ("Reset", 0.75f);
	}

	void Hide () {
		MeshRenderer render = gameObject.GetComponentInChildren<MeshRenderer> ();
		render.enabled = false;
	}
	
	void Show () {
		MeshRenderer render = gameObject.GetComponentInChildren<MeshRenderer> ();
		render.enabled = true;
	}
}
