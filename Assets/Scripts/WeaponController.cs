using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour
{
    private AudioSource audioSource;
    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;
    public float delay;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        InvokeRepeating("Fire", delay, delay * Random.Range(0.8f, 1.2f));
    }

    void Fire()
    {
        Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        audioSource.Play();
    }
}
