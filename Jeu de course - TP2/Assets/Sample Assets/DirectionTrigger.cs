using UnityEngine;
using System.Collections;

public class DirectionTrigger : MonoBehaviour {

    [SerializeField] PlayerUI ui;
    public PlayerUI.ESuggestedDirection suggestion = PlayerUI.ESuggestedDirection.None;

    void OnTriggerEnter(Collider c)
    {
        CarController player = c.GetComponentInParent<CarController>();
        if (player && player == ui.controller)
        {
            ui.direction = suggestion;
        }
    }
}
