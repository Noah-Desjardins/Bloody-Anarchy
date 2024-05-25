using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] GameObject explosion;

    void Start()
    {
        StartCoroutine(Exploser());
    }

    void OnEnable()
    {
        StartCoroutine(Exploser());
    }

    IEnumerator Exploser()
    {
        yield return new WaitForSeconds(1f);
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
