using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfo : MonoBehaviour
{
    public int ItemID;
    [SerializeField] TextMeshProUGUI PriceTxt;
    [SerializeField] GameObject ShopManager;

    // Update is called once per frame
    void Update()
    {
        PriceTxt.text = ShopManager.GetComponent<ShopManagerScript>().shopItem[2,ItemID].ToString() + '%';
    }

    public void Desactiver()
    {
        Destroy(gameObject);
    }
}
