using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OpenShop : MonoBehaviour
{
    [SerializeField] bool isInRange;
    ShopManagerScript Shop;
    QuitterShopBtn quitter;
    //open shop
    [SerializeField] KeyCode interactKeyOpen;
    bool open = false;

    //close shop
    [SerializeField] KeyCode interactKeyClose;
    private void Start()
    {
        quitter = GameObject.FindAnyObjectByType<QuitterShopBtn>();
        Shop = GameObject.FindGameObjectWithTag("ShopManager").GetComponent<ShopManagerScript>();
    }
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
                    quitter.QuitterShop(); //ferme le shop
                    open = false;
                }
            }
            else if (Input.GetKeyDown(interactKeyOpen)) //si appuyer sur 'e'
            {
                Shop.AfficherShop(); //ouvre le shop
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
