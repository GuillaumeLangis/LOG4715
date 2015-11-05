using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerUI : MonoBehaviour {

    public CarController controller;

	[SerializeField] Image speedFill;
    [SerializeField] Image hpFill;
    [SerializeField] Text speedValue;
    [SerializeField] Image nitroFill;
    [SerializeField] Text projectileInfo;
    [SerializeField] Text stylePointsValue;
    [SerializeField] Text positionInRaceValue;

    // Direction panels are only active one at a time, must be in the right order in the array
    [SerializeField] Text[] directionPanels = new Text[4];
    public enum ESuggestedDirection
    {
        Forward,
        Right,
        Left,
        Back,
        None
    }
    ESuggestedDirection _direction;
    public ESuggestedDirection direction
    {
        get { return _direction; }
        set
        {
            _direction = value;
            for (int i = 0; i < directionPanels.Length; i++)
            {
                directionPanels[i].enabled = i == (int)value;
            }
        }
    }

    void SetProjectileInfo(string info)
    {
        projectileInfo.text = info;
    }
    

    void Awake()
    {
        direction = ESuggestedDirection.None;
    }

    void FixedUpdate()
    {
        if (controller)
        {
            speedFill.fillAmount = controller.CurrentSpeed / controller.MaxSpeed;
            speedValue.text = Mathf.RoundToInt(controller.CurrentSpeed).ToString() + " km/h";
            nitroFill.fillAmount = controller.nitroLeft;
            stylePointsValue.text = controller.stylePoints.ToString() + " points";
            hpFill.fillAmount = controller.health;
            positionInRaceValue.text = (controller.checkpointManager.GetPositionInRace(controller) + 1).ToString();

            SetProjectileInfo(controller.projectilePrefab ? controller.projectilePrefab.name : "No projectile");
        }
    }
}
