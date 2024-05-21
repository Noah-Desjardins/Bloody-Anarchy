using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] GameObject explosion;
    [SerializeField] AudioClip explosionSound;

    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(Exploser());
    }

    void OnEnable()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(Exploser());
    }

    IEnumerator Exploser()
    {
        yield return new WaitForSeconds(1f);
        audioSource.PlayOneShot(explosionSound);
        GameObject explosionTemp = ObjectPool.instance.GetPoolObject(explosion);
        if (explosionTemp != null)
        {
            explosionTemp.transform.position = transform.position;
            explosionTemp.transform.rotation = transform.rotation;
            explosionTemp.SetActive(true);
        }
        gameObject.SetActive(false);
    }
}
