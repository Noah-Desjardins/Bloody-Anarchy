using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPotion : MonoBehaviour
{
    public int potionHealthGain = 10;
    Player player;
    [SerializeField] potionContainer potionContainer;
    PlayerAbility playerAbility;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        playerAbility = GetComponent<PlayerAbility>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ConsumePotion(InputAction.CallbackContext context)
    {
        if (playerAbility.GetAbility("canPotion") && context.started && potionContainer.nbPotions > 0 && player.health < 100)
        {
            if ((player.health + potionHealthGain) > player.StartingHealth)
                player.health = player.StartingHealth;
            else
                player.health += potionHealthGain;
            if (playerAbility.GetAbility("canPotionShield"))
            {
                StartCoroutine(player.InvincibilityFrames(Color.blue));
            }

            potionContainer.nbPotions--;
            
        }
    }
}
