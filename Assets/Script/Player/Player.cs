using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Windows;

public class Player : MonoBehaviour
{
    public int StartingHealth = 100;
    public int nbPotions = 0;
    public int health = 100;
    public int damage = 1;
    bool isDead = false;
    bool invincible = false;

    BossGeneral boss;
    deathUI deathUI;
    PlayerInput input;

    SpriteRenderer sr;
    Color spriteColor;

    Image healthBar;

    [SerializeField] bool GodMod = false;

    // Sound effect variables
    [SerializeField] AudioClip hitSound;
    [SerializeField] AudioSource audioSource;

    void Start()
    {
        health = StartingHealth;
        healthBar = GameObject.FindGameObjectWithTag("healtBar").GetComponent<Image>();
        sr = GetComponent<SpriteRenderer>();
        spriteColor = sr.color;
        
        
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
        health = StartingHealth;
        isDead = false;
        input = GetComponent<PlayerInput>();
        input.currentActionMap.Enable();
        this.gameObject.transform.position = new Vector3(0, 0, 0);
        if (GameObject.FindGameObjectWithTag("bossGeneral"))
        {
            boss = GameObject.FindGameObjectWithTag("bossGeneral").GetComponent<BossGeneral>();
        }
        deathUI = GameObject.FindGameObjectWithTag("deathui").GetComponent<deathUI>();

        // Initialize audio source
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        HealthBarManager();
        if (health <= 0 && !GodMod && !isDead)
        {
            killPlayer();
        }
    }

    public void HealthBarManager()
    {
        if ((int)(healthBar.fillAmount * StartingHealth) != health)
        {
            healthBar.fillAmount = (float)health / StartingHealth;
        }
            

    }
    void killPlayer()
    {
        isDead = true;
        //ici anim de mort
        if (boss != null && PlayerPrefs.GetInt("score") < (int)boss.pourcentageFait)
            PlayerPrefs.SetInt("score",(int)boss.pourcentageFait);
        PlayerPrefs.Save();
        input.currentActionMap.Disable();
        deathUI.show((int)boss.pourcentageFait);
    }
    public IEnumerator InvincibilityFrames(Color color)
    {
        //Les frame d'invicibilit� avant d'etre vuln�rable
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
                ProjectileGuide projectile = collision.GetComponent<ProjectileGuide>();
                health -= projectile.degat;
                PlayHitSound();
            }
            else if (collision.tag == "Explosion")
            {
                ExplosionDamage explsosion = collision.GetComponent<ExplosionDamage>();
                health -= explsosion.damage;
                PlayHitSound();
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
                PlayHitSound();
            }
            else if (collision.tag == "hand")
            {
                BossPhase3Hand hand = collision.GetComponent<BossPhase3Hand>();
                health -= hand.howManyDamage();
                StartCoroutine(InvincibilityFrames(Color.red));
                PlayHitSound();
            }
        }
    }

    void PlayHitSound()
    {
        if (hitSound != null)
        {
            audioSource.clip = hitSound;
            audioSource.Play();
        }
    }
}
