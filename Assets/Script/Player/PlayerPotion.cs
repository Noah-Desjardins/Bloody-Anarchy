using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;

public class PlayerPotion : MonoBehaviour
{
    int potionHealthGain = 50;
    Player player;
    [SerializeField] potionContainer potionContainer;

    PlayerAbility playerAbility;

    void Start()
    {
        player = GetComponent<Player>();
        playerAbility = GetComponent<PlayerAbility>();
        
    }
    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

    }
    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        potionContainer.nbPotions = potionContainer.nbPotionsStart;

    }

    public void ConsumePotion(InputAction.CallbackContext context)
    {
        if (playerAbility.GetAbility("canPotion") && context.started && potionContainer.nbPotions > 0 && player.health < player.StartingHealth)
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
