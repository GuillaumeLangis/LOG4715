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

    [SerializeField]
    private string jumpInput = "Jump";

    [SerializeField]
    private string fireInput = "Fire1";

    [SerializeField]
    private string reposInput = "Fire3";

    [SerializeField]
    private string nitroInput = "Fire2";

    bool jump = false;
    bool repos = false;
	
	void Awake ()
	{
		// get the car controller
		car = GetComponent<CarController>();
	}

    void Update()
    {
        if (CrossPlatformInput.GetButtonDown(fireInput))
        {
            car.LaunchProjectile();
        }

        if (!jump)
            jump = CrossPlatformInput.GetButtonDown(jumpInput);

        if (!repos)
            repos = CrossPlatformInput.GetButtonDown(reposInput);
    }

    void FixedUpdate()
	{
		// pass the input to the car!
		#if CROSS_PLATFORM_INPUT
		float h = CrossPlatformInput.GetAxis(horizontal);
		float v = CrossPlatformInput.GetAxis(vertical);
        float r = CrossPlatformInput.GetAxis(roll);
        car.useNitro = CrossPlatformInput.GetButton(nitroInput);
        if (jump)
        {
            car.Jump();
            jump = false;
        }

        if (repos)
        {
            car.Reposition();
            repos = false;
        }
        #else
		float h = Input.GetAxis(horizontal);
		float v = Input.GetAxis(vertical);
        #endif
        car.Move(h,v,r);
	}
}
