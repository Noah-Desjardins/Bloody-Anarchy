using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPhaseUn : MonoBehaviour
{
    [SerializeField] int vie = 1000;
    int vieRestant;
    Rigidbody2D rb;
    [SerializeField] int attack = 5;
    [SerializeField] GameObject projectile;
    [SerializeField] GameObject target;
    [SerializeField] float vitesse = 4;
    void Start()
    {
        vieRestant = vie;
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(Attack());
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 tagetPos = target.transform.position - transform.position;
        transform.Translate(tagetPos.normalized * vitesse * Time.deltaTime);
    }
    IEnumerator Attack()
    {
        yield return new WaitForSeconds(2);
        int attack = Random.Range(0, 3);
        switch (attack)
        {
            case 0:
                
                break;
            case 1:
                
                break;
            case 2:
                
                break;
        }
        StartCoroutine(Attack());
    }
}
