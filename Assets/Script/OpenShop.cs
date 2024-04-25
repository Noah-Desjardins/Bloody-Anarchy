using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OpenShop : MonoBehaviour
{
    [SerializeField] bool isInRange;

    //open shop
    [SerializeField] KeyCode interactKeyOpen;
    [SerializeField] UnityEvent interactActionOpen;
    bool open = false;

    //close shop
    [SerializeField] KeyCode interactKeyClose;
    [SerializeField] UnityEvent interactActionClose;

    // Update is called once per frame
    void Update()
    {
        if(isInRange)
        {
            //s'active seulement si le shop est ouvert
            if (open)
            {
                if (Input.GetKeyDown(interactKeyClose) || Input.GetKeyDown(interactKeyOpen)) //si appuyer sur esc ou 'e'
                {
                    interactActionClose.Invoke(); //ferme le shop
                    open = false;
                }
            }
            else if (Input.GetKeyDown(interactKeyOpen)) //si appuyer sur 'e'
            {
                interactActionOpen.Invoke(); //ouvre le shop
                open = true;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            isInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            isInRange = false;
        }
    }
}
