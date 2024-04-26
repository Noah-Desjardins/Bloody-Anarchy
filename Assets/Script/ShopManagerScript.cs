using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ShopManagerScript : MonoBehaviour
{
    [SerializeField] int[] pris;
    public int[,] shopItem ;
    [SerializeField] float pourcentage;
    [SerializeField] TextMeshProUGUI PourcentageTxt;
    [SerializeField] GameObject Shop;

    void Start()
    {
        shopItem = new int[3, pris.Length + 1];
        PourcentageTxt.text = "Vous avec fait " + pourcentage.ToString() + "% du boss";


        for (int i = 1; i < shopItem.GetLength(1); i++)
        {
            //mettre les id
            shopItem[1, i] = i;

            //mettre les prix
            shopItem[2, i] = pris[i - 1];
        }

    }

    // Update is called once per frame
    public void Acheter()
    {
        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;

        if (pourcentage >= shopItem[2, ButtonRef.GetComponent<ItemInfo>().ItemID])
        {
            ButtonRef.GetComponent<ItemInfo>().Acheter();
        }
    }

    public void AfficherShop()
    {
        Shop.SetActive(true);
    }
}
