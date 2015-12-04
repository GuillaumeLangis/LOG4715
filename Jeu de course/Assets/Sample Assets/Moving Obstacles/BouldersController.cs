using UnityEngine;
using System.Collections;

public class BouldersController : MonoBehaviour {

	bool movingUp = false;
	bool paused = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!paused) {
			if (movingUp) {
				transform.position = new Vector3 (transform.position.x, transform.position.y + 0.1f, transform.position.z);
				if (transform.position.y >= 115) {
					paused = true;
					Invoke ("Wait", 2);
				}
			}
		}
	}

	void OnCollisionEnter(Collision col) {
		paused = true;
		Invoke ("Wait",2);
	}

	void Wait() {
		if (movingUp) {
			movingUp = false;
		} else {
			movingUp = true;
		}
		paused = false;
	}
}
