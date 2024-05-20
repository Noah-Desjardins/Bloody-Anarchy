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
            case 1: //1-UP plus de vie
                Player.StartingHealth += 150;
                break;
            case 2: //Plus de Dégat
                Player.damage += 19;
                break;
            case 3: //REflect balle
                break;
            case 4: // Esquive
                break;
            case 5: // Balle reflect damage
                break;
            case 6: // augment degat plus
                break;
            case 7: // augmente pdv plus
                break;
            case 8: // une potion 25% de vie
                break;
            case 9: // GUN
                break;
            case 10: // bouclier potion
                break;
            case 11: // 2 potions
                break;
            case 12: // REVIVE
                break;
            case 13: // Boss Commence avec 32% de vie en moin
                break;
            case 14: // 3 potion
                break;
            case 15: // win
                break;
            default:
                print("erreur");
                break;
        }
        Destroy(gameObject);
    }
}
