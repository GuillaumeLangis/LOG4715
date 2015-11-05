using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    [SerializeField] Rigidbody _body;
    public Rigidbody body
    {
        get
        {
            return _body;
        }
    }
    [SerializeField] WaypointProgressTracker pathfinder;

    public enum ProjType
    {
        Green,
        Red,
        Blue
    }
    [SerializeField] ProjType _projectileType = ProjType.Green;
    public ProjType projectileType { get { return _projectileType; } }

    public CarController source;
    public CarController target;
    [SerializeField] Transform wptTgt;
    public float speed = 20;
    [Range(0, 1)] public float damage = 0.25f;
    public float force = 250f;
    [SerializeField] int nbBounce = 1;

    [SerializeField] float distToWaypoint;
    [SerializeField] float distToTarget;

    public GameObject deathPrefab;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate()
    {
        if (target)
        {
            Vector3 direction = transform.forward;
            if (pathfinder)
            {
                distToWaypoint = Vector3.Distance(wptTgt.position, transform.position);
                distToTarget = Vector3.Distance(target.rigidbody.worldCenterOfMass, transform.position);

                RaycastHit rayhit;
                if (Physics.Raycast(new Ray(transform.position, (target.rigidbody.worldCenterOfMass - transform.position)), out rayhit))
                {
                    Debug.DrawLine(transform.position, rayhit.point);
                    // If straight line to target
                    var car = rayhit.collider.GetComponentInParent<CarController>();
                    if (car && car == target)
                    {
                        direction = (target.rigidbody.worldCenterOfMass - transform.position).normalized;
                    }
                    else
                    {
                        direction = (wptTgt.position - transform.position).normalized;
                    }
                }
                else
                {
                    direction = (wptTgt.position - transform.position).normalized;
                }
            }
            else
            {
                direction = (target.transform.position - transform.position).normalized;
            }
            transform.rotation = Quaternion.LookRotation(direction.normalized);
            body.velocity = direction.normalized * speed;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        CarController car = col.gameObject.GetComponentInParent<CarController>();
        if (car)
        {
            switch (projectileType)
            {
                case ProjType.Green:
                    car.Damage(damage);
                    car.rigidbody.AddForce((Random.onUnitSphere + Vector3.up * 1.5f).normalized * force);
                    Destroy(gameObject);
                    if (deathPrefab)
                    {
                        var fx = Instantiate(deathPrefab, transform.position, transform.rotation);
                        Destroy(fx, 20f);
                    }
                    break;
                case ProjType.Red:
                    if (car != source)
                    {
                        car.Damage(damage);
                        car.rigidbody.AddForce((Random.onUnitSphere + Vector3.up * 1.5f).normalized * force);
                        Destroy(gameObject);
                        if (deathPrefab)
                        {
                            var fx = Instantiate(deathPrefab, transform.position, transform.rotation);
                            Destroy(fx, 20f);
                        }
                    }
                    break;
                case ProjType.Blue:
                    if (car != source)
                    {
                        car.Damage(damage);
                        car.rigidbody.AddForce((Random.onUnitSphere + Vector3.up * 1.5f).normalized * force);
                        if (car == target)
                        {
                            Destroy(gameObject);
                        }
                        if (deathPrefab)
                        {
                            var fx = Instantiate(deathPrefab, transform.position, transform.rotation);
                            Destroy(fx, 20f);
                        }
                    }
                    break;
                default:
                    break;
            }
        }
        else
        {
            nbBounce--;
            if (nbBounce < 0)
            {
                Destroy(gameObject);
                if (deathPrefab)
                {
                    var fx = Instantiate(deathPrefab, transform.position, transform.rotation);
                    Destroy(fx, 20f);
                }
            }
        }
    }
}
