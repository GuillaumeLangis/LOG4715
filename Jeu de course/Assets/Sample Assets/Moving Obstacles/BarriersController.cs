using UnityEngine;
using System.Collections;

public class BarriersController : MonoBehaviour {

	bool movingUp = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (movingUp) {
			if (transform.position.y >= 109) {
				movingUp = false;
			}
			transform.position = new Vector3 (transform.position.x, transform.position.y + 0.01f, transform.position.z);
		} else {
			if (transform.position.y <= 101.5) {
				movingUp = true;
			}
			transform.position = new Vector3 (transform.position.x, transform.position.y - 0.01f, transform.position.z);
		}
	}
}
