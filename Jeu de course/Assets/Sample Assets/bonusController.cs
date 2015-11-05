using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class bonusController : MonoBehaviour {
	private bool carIsIn = false;
	public GameObject interactFX;

	public float multiplier = 4;

	[SerializeField] List<CarController> currentCars = new List<CarController>();

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.CompareTag ("Car_Collider")) {
			CarController car = other.GetComponentInParent<CarController>();
			if (car && !currentCars.Contains(car)) {
				car.boostFactor *= multiplier;
				currentCars.Add(car);

				if (interactFX) {
					GameObject fx = Instantiate(interactFX, other.transform.position, other.transform.rotation) as GameObject;
					fx.transform.SetParent(other.transform);
					Destroy(fx, 20f);
				}
			}
			carIsIn = true;
			//car.gameObject.SetActive(false);
			//car.rigidbody.AddForce(rigidbody.velocity.normalized * 5, ForceMode.Impulse);
			Debug.LogWarning("Enter the bonus area");


		}
	}

	void OnTriggerExit(Collider other){
		if (other.gameObject.CompareTag ("Car_Collider")) {
			CarController car = other.GetComponentInParent<CarController>();
			if (car && currentCars.Contains(car)){
				car.boostFactor /= multiplier;
				currentCars.Remove(car);
			}
			carIsIn = false;
			//car.gameObject.SetActive(true);
			Debug.LogWarning("Exit the bonus area");
		}
	}
}
