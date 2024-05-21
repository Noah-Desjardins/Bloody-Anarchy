using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ProjectileGuide : MonoBehaviour
{
    GameObject target;
    Vector3 distance = Vector3.zero;
    float rotation;
    [SerializeField] float vitesse = 3;
    public int degat = 25;
    [SerializeField] AudioClip shootSound;

    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        target = GameObject.FindGameObjectWithTag("player");
        distance = target.transform.position - transform.position;
        transform.transform.rotation = Quaternion.LookRotation(Vector3.forward, distance);
        transform.Rotate(new Vector3(0, 0, 45), Space.World);
        audioSource.PlayOneShot(shootSound);
    }

    void OnEnable()
    {
        audioSource = GetComponent<AudioSource>();
        target = GameObject.FindGameObjectWithTag("player");
        distance = target.transform.position - transform.position;
        transform.transform.rotation = Quaternion.LookRotation(Vector3.forward, distance);
        transform.Rotate(new Vector3(0, 0, 45), Space.World);
        audioSource.PlayOneShot(shootSound);
    }

    void Update()
    {
        transform.Translate(distance.normalized * vitesse * Time.deltaTime, Space.World);
    }
}
