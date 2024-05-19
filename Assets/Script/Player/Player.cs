using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int StartingHealth = 100;
    public int nbPotions = 0;
    public int health = 100;
    public int damage = 1;
    bool invincible = false;

    SpriteRenderer sr;
    Color spriteColor;

    Image healthBar;

    [SerializeField] bool GodMod = false;

    void Start()
    {
        healthBar = GameObject.FindGameObjectWithTag("healtBar").GetComponent<Image>();
        sr = GetComponent<SpriteRenderer>();
        spriteColor = sr.color;

        health = StartingHealth;
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
        this.gameObject.transform.position = new Vector3(0, 0, 0);
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
            healthBar.fillAmount = (float)health / StartingHealth;

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
            else if (collision.tag == "Explosion")
            {
                ExplosionDamage explsosion = collision.GetComponent<ExplosionDamage>();
                health -= explsosion.damage;
            }
            else if (collision.tag == "AttackBoss1")
            {
                closeDamge close = collision.GetComponent<closeDamge>();
                health -= close.damage;
            }
            else if (collision.tag == "bossbullet")
            {
                Bullet bullet = collision.GetComponent<Bullet>();
                health -= bullet.howManyDamage();
                StartCoroutine(InvincibilityFrames(Color.red));
            }
            else if (collision.tag == "hand")
            {
                BossPhase3Hand hand = collision.GetComponent<BossPhase3Hand>();
                health -= hand.howManyDamage();
                StartCoroutine(InvincibilityFrames(Color.red));
            }
        }

    }

}
