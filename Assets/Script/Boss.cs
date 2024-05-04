using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] int currentPhase = 1;
    [SerializeField] int maxHealth = 1000;
    [SerializeField] int currentHealth;
    [SerializeField] int attackDamage = 25;
    [SerializeField] GameObject swordProjectilePrefab; // Prefab projectile
    [SerializeField] float projectileSpeed = 5f; // vitesse projectile
    [SerializeField] Transform player; // transform joueur
    [SerializeField] float moveSpeed = 2f; // Boss speed
    [SerializeField] Vector2 minBounds; // Minimum background
    [SerializeField] Vector2 maxBounds; // Maximum background

    private void Start()
    {
        currentHealth = maxHealth;
        SetupPhase1();
    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            currentPhase++;
            SetupNextPhase();
        }

        switch (currentPhase)
        {
            case 1:
                LaunchSwordProjectile();
                MoveRandomly();
                // actions à ajouter selon ce qu'on décide de faire de plus
                break;
            case 2:
                // actions à ajouter selon ce qu'on décide de faire
                break;
            case 3:
                // actions à ajouter selon ce qu'on décide de faire
                break;
            default:
                // normalement rien
                break;
        }
    }

    private void SetupNextPhase()
    {
        switch (currentPhase)
        {
            case 2:
                SetupPhase2();
                break;
            case 3:
                SetupPhase3();
                break;
            default:
                //rien
                break;
        }
    }

    private void SetupPhase1()
    {
        maxHealth = 1000;
        currentHealth = maxHealth;
        attackDamage = 25;
    }

    private void SetupPhase2()
    {
        maxHealth = 3000;
        currentHealth = maxHealth;
        attackDamage = 63;
    }

    private void SetupPhase3()
    {
        maxHealth = 6000;
        currentHealth = maxHealth;
        attackDamage = 125;
    }

    private void LaunchSwordProjectile()
    {
        // Instantier projectile and lancer vers joueur
        GameObject projectile = Instantiate(swordProjectilePrefab, transform.position, Quaternion.identity);
        Vector3 direction = (player.position - transform.position).normalized;
        projectile.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
    }

    private void MoveRandomly()
    {
        // Reste les animations à faire

        // Generer une random position en fonction des limites du background et s'y rendre
        Vector2 randomPosition = new Vector2(Random.Range(minBounds.x, maxBounds.x), Random.Range(minBounds.y, maxBounds.y));
        transform.position = Vector2.MoveTowards(transform.position, randomPosition, moveSpeed * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        //il reste à ajouter les animations
    }
}
