using UnityEngine;
using System.Collections;

public class EvasiveManeuver : MonoBehaviour
{
    public float dodge;
    public Vector2 startWait;
    public Vector2 maneuverTime;
    public Vector2 maneuverWait;
    public float smoothing;
    public Boundary boundary;
    public float tilt;

    private float targetManeuver;
    private Rigidbody rb;
    private float currentSpeed;
    void Start()
    {
        StartCoroutine(Evade());
        rb = GetComponent<Rigidbody>();
        currentSpeed = rb.velocity.z;
    }

    IEnumerator Evade()
    {
        yield return new WaitForSeconds(Random.Range(startWait.x, startWait.y));
        while (true)
        {
            targetManeuver = Random.Range(1, dodge) * -Mathf.Sign(transform.position.x);
            yield return new WaitForSeconds(Random.Range(maneuverTime.x, maneuverTime.y));
            targetManeuver = 0;
            yield return new WaitForSeconds(Random.Range(maneuverWait.x, maneuverWait.y));
        }
    }

    void FixedUpdate()
    {
        float newManeuver = Mathf.MoveTowards(rb.velocity.x, targetManeuver, Time.deltaTime * smoothing);
        rb.velocity = new Vector3
        {
            x = newManeuver,
            y = 0.0f,
            z = currentSpeed
        };

        var position = new Vector3
        {
            x = Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
            y = 0.0f,
            z = Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)

        };
        rb.position = position;
        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
    }
}
