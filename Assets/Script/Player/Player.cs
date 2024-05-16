using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int StartingHealth = 100;
    public int nbPotions = 0;
    public int health = 100;
    public int damage = 1;
    [SerializeField] int explosionDegat = 25;
    bool invincible = false;

    SpriteRenderer sr;
    Color spriteColor;

    [SerializeField] Image healthBar;

    [SerializeField] bool GodMod = false;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        spriteColor = sr.color;

        health = StartingHealth;
    }

    // Update is called once per frame
    void Update()
    {
        HealthBarManager();
        if (health <= 0 && !GodMod)
        {
            Destroy(gameObject);
        }
    }
    public void HealthBarManager()
    {
        if (healthBar.fillAmount * StartingHealth != health)
            healthBar.fillAmount = (float) health / StartingHealth;

    }

    public IEnumerator InvincibilityFrames(Color color)
    {
        //Les frame d'invicibilité avant d'etre vulnérable
        invincible = true;
        int nbClignotement = 21;
        while (nbClignotement > 0)
        {
            yield return new WaitForSeconds(0.05f);
            sr.color = nbClignotement % 2 == 0 ? spriteColor : color;
            invincible = true;
            nbClignotement--;
        }
        invincible = false;
        sr.color = spriteColor;
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!invincible)
        {
            if (collision.tag == "sword")
            {
                projectileGuide projectile = collision.GetComponent<projectileGuide>();
                health -= projectile.degat;
            }
            if (collision.tag == "Explosion" || collision.tag == "AttackBoss1")
            {
                health -= explosionDegat;
            }
            if (collision.tag == "bossbullet")
            {
                Bullet bullet = collision.GetComponent<Bullet>();
                health -= bullet.howManyDamage();
                StartCoroutine(InvincibilityFrames(Color.red));
            }
            if (collision.tag == "hand")
            {
                BossPhase3Hand hand = collision.GetComponent<BossPhase3Hand>();
                health -= hand.howManyDamage();
                StartCoroutine(InvincibilityFrames(Color.red));
            }
        }

    }

}
