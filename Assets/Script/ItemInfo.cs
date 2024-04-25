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
    Player Player;

    // Update is called once per frame

    private void Start()
    {
        Player = FindAnyObjectByType<Player>();
    }
    void Update()
    {
        PriceTxt.text = ShopManager.GetComponent<ShopManagerScript>().shopItem[2,ItemID].ToString() + '%';
    }

    public void Acheter()
    {
        switch (ItemID)
        {
            case 1:
                Player.vie += 150;
                break;
            case 2:
                Player.degat += 4;
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            default:
                print("erreur");
                break;
        }
        Destroy(gameObject);
    }
}
