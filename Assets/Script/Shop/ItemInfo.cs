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
    PlayerAbility PlayerAbility;
    potionContainer potionContainer;
    PlayerPotion playerPotion;

    // Update is called once per frame

    private void Start()
    {
        Player = FindAnyObjectByType<Player>();
        PlayerAbility = FindAnyObjectByType<PlayerAbility>();
        potionContainer = FindAnyObjectByType<potionContainer>();
        playerPotion = FindAnyObjectByType<PlayerPotion>();
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
                Player.StartingHealth += 50;
                Player.health = Player.StartingHealth;
                break;
            case 2: //Plus de Dégat
                Player.damage += 20;
                break;
            case 3: //REflect balle not implemented
                break;
            case 4: // Esquive
                PlayerAbility.SwitchAbility("canRoll");
                break;
            case 5: // Balle reflect damage not implemented
                break;
            case 6: // augment degat plus.
                Player.damage += 50;
                break;
            case 7: // augmente pdv plus
                Player.health += 100;
                Player.health = Player.StartingHealth;
                break;
            case 8: // une potion 25% de vie
                potionContainer.nbPotions = potionContainer.nbPotions > 1? potionContainer.nbPotions: 1;
                if (!PlayerAbility.GetAbility("canPotion"))
                    PlayerAbility.SwitchAbility("canPotion");
                break;
            case 9: // GUN
                PlayerAbility.SwitchAbility("canSlash");
                PlayerAbility.SwitchAbility("canShoot");
                break;
            case 10: // bouclier potion
                PlayerAbility.SwitchAbility("canPotionShield");
                break;
            case 11: // 2 potions
                potionContainer.nbPotions = potionContainer.nbPotions > 2 ? potionContainer.nbPotions : 2;
                if (!PlayerAbility.GetAbility("canPotion"))
                    PlayerAbility.SwitchAbility("canPotion");
                break;
            case 12: // REVIVE not implemented
                break;
            case 13: // Boss Commence avec 32% de vie en moin not implemented
                break;
            case 14: // 3 potion
                potionContainer.nbPotions = potionContainer.nbPotions > 3 ? potionContainer.nbPotions : 3;
                if (!PlayerAbility.GetAbility("canPotion"))
                    PlayerAbility.SwitchAbility("canPotion");
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
