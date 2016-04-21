using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float tilt;
    public Boundary boundary;

    public float fireRate;
    public GameObject shot;
    public Transform shotSpawn;

    private float nextFire;

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);

            var shootSound = GetComponent<AudioSource>();
            shootSound.Play();
        }
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        var movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        var rigidBody = GetComponent<Rigidbody>();
        rigidBody.velocity = movement * speed;

        var position = new Vector3
        {
            x = Mathf.Clamp(rigidBody.position.x, boundary.xMin, boundary.xMax),
            y = 0.0f,
            z = Mathf.Clamp(rigidBody.position.z, boundary.zMin, boundary.zMax)

        };
        rigidBody.position = position;
        rigidBody.rotation = Quaternion.Euler(0.0f, 0.0f, rigidBody.velocity.x * -tilt);
    }
}
