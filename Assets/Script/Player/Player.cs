using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public int vie = 100;
    public int degat = 1;

    // Update is called once per frame
    void Update()
    {
        
        if (vie <= 0)
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
    }

}
