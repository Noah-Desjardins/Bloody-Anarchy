using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public int vie = 100;
    public int degat = 1;
    [SerializeField] int explosionDegat = 25;
    [SerializeField] bool godMode = false;

    // Update is called once per frame
    void Update()
    {
        
        if (vie <= 0 && godMode == false)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "sword")
        {
            projectileGuide projectile = collision.GetComponent<projectileGuide>();
            vie -= projectile.degat;
        }
        else if(collision.tag == "Explosion")
        {
            vie -= explosionDegat;
        }
        else if (collision.tag == "AttackBoss1")
        {
            print("aouch");
        }
    }

}
