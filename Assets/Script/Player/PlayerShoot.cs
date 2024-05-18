using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    Camera cam;
    SpriteRenderer sr;
    Animator animator;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject? shop;

    PlayerAbility playerAbility;
    [SerializeField] GameObject abilityContainer;
    Cooldown shootingCooldown;
    Vector3 mousePosition = Vector3.zero;
    Quaternion bulletRotation = Quaternion.identity;

    [SerializeField] Texture2D shootingCursor;
    PlayerCursor playerCursor;

    bool isShooting = false;

    // Sound effect variables
    [SerializeField] AudioClip shootSound;
    [SerializeField] AudioSource audioSource;

    void Start()
    {
        cam = Camera.main;
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        playerAbility = GetComponent<PlayerAbility>();
        playerCursor = GetComponent<PlayerCursor>();
        if (playerAbility.GetAbility("canShoot"))
        {
            playerCursor.setCursor(shootingCursor);
        }

        // Initialize audio source
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        if (shootingCooldown == null && playerAbility.GetAbility("canShoot"))
            setShootingCooldown();
        if (isShooting)
            Shoot();
    }

    public void setShootingCooldown()
    {
        foreach (Cooldown cooldown in abilityContainer.GetComponentsInChildren<Cooldown>())
        {
            if (cooldown.tag == "canShoot")
                shootingCooldown = cooldown;
            print(shootingCooldown);
        }
    }

    public void Shoot()
    {
        if (!shop.activeSelf)
        {
            if (playerAbility.GetAbility("canShoot") && !shootingCooldown.isCoolingDown)
            {
                mousePosition = Input.mousePosition;
                mousePosition = cam.ScreenToWorldPoint(mousePosition);

                GameObject bulletTemp = ObjectPool.instance.GetPoolObject(bulletPrefab);
                shootingCooldown.StartCoolDown();
                if (sr.flipX)
                {
                    bulletTemp.transform.position = new Vector3(transform.position.x - sr.bounds.size.x / 2f, transform.position.y, transform.position.z);
                }
                else
                {
                    bulletTemp.transform.position = new Vector3(transform.position.x + sr.bounds.size.x / 2f, transform.position.y, transform.position.z);
                }
                bulletRotation = Quaternion.LookRotation(Vector3.forward, mousePosition - bulletTemp.transform.position);
                bulletTemp.transform.rotation = bulletRotation;
                bulletTemp.SetActive(true);

                // Play shoot sound
                if (shootSound != null)
                {
                    audioSource.clip = shootSound;
                    audioSource.Play();
                }
            }
        }
    }

    public void Attack(InputAction.CallbackContext context)
    {
        isShooting = context.ReadValue<float>() > 0;
    }
}
