using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[ExecuteInEditMode]
public class MinimapCarWidget : MonoBehaviour {

    [SerializeField] RectTransform rect;
    public Camera cam;
    public CarController car;

    [Header("Debug Data")]
    public Vector3 dist;
    public Vector3 distScaled;
    public Vector3 viewPoint;
	
	// Update is called once per frame
	void Update () {
	    if (car && cam)
        {
            // var matrix = cam.worldToCameraMatrix;
            // Vector4 worldPos = new Vector4(car.transform.position.x, car.transform.position.y, car.transform.position.z, 1);
            // Vector4 screenPos = matrix * worldPos;
            // rect.transform.position = new Vector3(screenPos.x, screenPos.y, screenPos.z);

            dist = car.transform.position - cam.transform.position;
            distScaled = dist / cam.orthographicSize;

            

            viewPoint = cam.WorldToViewportPoint(car.transform.position);

            rect.anchorMin = new Vector2(viewPoint.x, viewPoint.y);
            rect.anchorMax = new Vector2(viewPoint.x, viewPoint.y);
        }
	}
}
