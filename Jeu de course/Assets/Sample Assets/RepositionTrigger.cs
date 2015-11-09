using UnityEngine;
using System.Collections;

public class RepositionTrigger : MonoBehaviour {

	void OnTriggerEnter(Collider col)
    {
        if (col.GetComponentInParent<CarController>())
        {
            col.GetComponentInParent<CarController>().Reposition();
        }
    }
}
