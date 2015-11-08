using UnityEngine;

[RequireComponent(typeof(CarController))]
public class CarUserControlMP : MonoBehaviour
{
	private CarController car;  // the car controller we want to use

	[SerializeField]
	private string vertical = "Vertical";

	[SerializeField]
	private string horizontal = "Horizontal";

    [SerializeField]
    private string roll = "Roll";

    bool jump = false;
	
	void Awake ()
	{
		// get the car controller
		car = GetComponent<CarController>();
	}

    void Update()
    {
        if (CrossPlatformInput.GetButtonDown("Fire1"))
        {
            car.LaunchProjectile();
        }

        if (!jump)
            jump = CrossPlatformInput.GetButtonDown("Jump");
    }

    void FixedUpdate()
	{
		// pass the input to the car!
		#if CROSS_PLATFORM_INPUT
		float h = CrossPlatformInput.GetAxis(horizontal);
		float v = CrossPlatformInput.GetAxis(vertical);
        float r = CrossPlatformInput.GetAxis(roll);
        car.useNitro = CrossPlatformInput.GetButton("Fire2");
        if (jump)
        {
            car.Jump();
            jump = false;
        }
        #else
		float h = Input.GetAxis(horizontal);
		float v = Input.GetAxis(vertical);
        #endif
        car.Move(h,v,r);
	}
}
