using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ShopManagerScript : MonoBehaviour
{
    public int[,] shopItem = new int[4,2];
    [SerializeField] float pourcentage;
    [SerializeField] TextMeshProUGUI PourcentageTxt;
    [SerializeField] GameObject Shop;

    void Start()
    {
        PourcentageTxt.text = "Vous avec fait " + pourcentage.ToString() + "% du boss";

        //mettre les ID
        shopItem[1, 1] = 1;

        //mettre les prix
        shopItem[2, 1] = 1;

        //quantité acheter
        shopItem[3, 1] = 0;
    }

    // Update is called once per frame
    public void Acheter()
    {
        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;

        if (pourcentage >= shopItem[2, ButtonRef.GetComponent<ItemInfo>().ItemID])
        {
            shopItem[3, ButtonRef.GetComponent<ItemInfo>().ItemID] = 1;
            ButtonRef.GetComponent<ItemInfo>().Desactiver();
        }
    }

    public void AfficherShop()
    {
        Shop.SetActive(true);
    }
}
