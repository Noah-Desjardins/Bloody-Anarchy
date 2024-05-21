using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMelee : MonoBehaviour
{
    SpriteRenderer sr;
    Animator animator;
    PlayerMovement playerMovement;
    [SerializeField] GameObject attackPrefab;
    [SerializeField] damageZone damageZone;
    [SerializeField] GameObject shop;

    PlayerAbility playerAbility;
    GameObject abilityContainer;
    Cooldown slashingCooldown;

    // Sound effect variables
    [SerializeField] AudioClip slashSound;
    [SerializeField] AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        abilityContainer = GameObject.FindGameObjectWithTag("abilityContaineur");
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        playerAbility = GetComponent<PlayerAbility>();

        // Initialize audio source
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void setSlashingCooldown()
    {
        foreach (Cooldown cooldown in abilityContainer.GetComponentsInChildren<Cooldown>())
        {
            if (cooldown.tag == "canSlash")
                slashingCooldown = cooldown;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (slashingCooldown == null && playerAbility.GetAbility("canSlash"))
            setSlashingCooldown();
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (!shop.activeSelf)
        {
            if (context.started && playerAbility.GetAbility("canSlash") && !slashingCooldown.isCoolingDown)
            {
                slashingCooldown.StartCoolDown();
                PlaySlashSound();
                if (playerMovement.direction.y == 1)
                {
                    Instantiate(attackPrefab, new Vector3(transform.position.x - 0.2f, transform.position.y + sr.bounds.size.y / 2f, transform.position.z), Quaternion.Euler(0, 0, 90), transform);
                }
                else if (playerMovement.direction.y == -1)
                {
                    Instantiate(attackPrefab, new Vector3(transform.position.x + 0.2f, transform.position.y - sr.bounds.size.y / 2f, transform.position.z), Quaternion.Euler(0, 0, -90), transform);
                }
                else
                {
                    if (sr.flipX)
                        Instantiate(attackPrefab, new Vector3(transform.position.x - sr.bounds.size.x / 2f, transform.position.y, transform.position.z), quaternion.identity, transform);
                    else
                        Instantiate(attackPrefab, new Vector3(transform.position.x + sr.bounds.size.x / 2f, transform.position.y, transform.position.z), quaternion.identity, transform);
                }

                animator.SetTrigger("sideAttack");
            }
        }
    }

    void PlaySlashSound()
    {
        if (slashSound != null)
        {
            audioSource.clip = slashSound;
            audioSource.Play();
        }
    }
}
