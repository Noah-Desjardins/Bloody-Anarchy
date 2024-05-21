using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPotion : MonoBehaviour
{
    int potionHealthGain;
    public float gainPercent = 0.25f;
    Player player;
    [SerializeField] potionContainer potionContainer;

    PlayerAbility playerAbility;

    // Sound effect variables
    [SerializeField] AudioClip potionSound;
    [SerializeField] AudioSource audioSource;

    void Start()
    {
        player = GetComponent<Player>();
        playerAbility = GetComponent<PlayerAbility>();

        // Initialize audio source
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void ConsumePotion(InputAction.CallbackContext context)
    {
        potionHealthGain = (int)(player.StartingHealth * gainPercent);
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

            // Play potion sound
            if (potionSound != null)
            {
                audioSource.clip = potionSound;
                audioSource.Play();
            }
        }
    }
}
