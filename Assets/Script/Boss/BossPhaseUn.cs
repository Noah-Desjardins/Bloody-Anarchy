using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class BossPhaseUn : MonoBehaviour
{
    [SerializeField] int vie = 1000;
    [SerializeField] int attack = 5;
    [SerializeField] float vitesse = 4;
    [SerializeField] float distanceEsquive = 10;
    [SerializeField] float vitesseAttack = 2;
   
    int vieRestant;
    bool bossPret = false;
    bool peutBouger = true;
    float vitesseAttackRestant;

    CameraController camController;
    BossGeneral bossGeneral;
    Player joueur;
    SpriteRenderer spriteRenderer;
    Animator animator;
    GameObject target;
    UIController uicontroller;
    Transform parentTransform;
    [SerializeField]  GameObject nextPhase;

    [SerializeField] TextMeshProUGUI titreBoss;
    [SerializeField] GameObject empecherMoveJoueur;
    [SerializeField] GameObject projectile;
    [SerializeField] GameObject explosionSign;
    [SerializeField] GameObject damageZone;
    [SerializeField] Slider healthBar;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("player");
        uicontroller = GameObject.FindGameObjectWithTag("UIController").GetComponent<UIController>();
            
        camController = FindAnyObjectByType<CameraController>();
        bossGeneral = GetComponentInParent<BossGeneral>();
        joueur = target.GetComponent<Player>();
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        parentTransform = GetComponentInParent<Transform>();

        vieRestant = vie;
        vitesseAttackRestant = vitesseAttack;
        healthBar.maxValue = vie;
        healthBar.value = vieRestant;

        StartCoroutine(Esquive());
        StartCoroutine(Attack());
        StartCoroutine(ApparaitreBoss());
    }

    // Update is called once per frame
    void Update()
    {
        if (bossPret && target != null)
        {
            Vector3 VectorPos = target.transform.position - transform.position;
            animator.SetBool("walk",  peutBouger && VectorPos.magnitude > 2.5);
            if (VectorPos.magnitude > 2.5)
            {
                
                if (peutBouger)
                {
                    transform.Translate(VectorPos.normalized * vitesse * Time.deltaTime); //marcher direction le joueur
                    if (VectorPos.normalized.x < 0)
                    {
                        spriteRenderer.flipX = true;
                    }
                    else
                    {
                        spriteRenderer.flipX = false;
                    }
                }
            }
            else
            {

                if(vitesseAttackRestant > vitesseAttack)
                {
                    //attack de corp a corp (de proche)
                    Instantiate(damageZone, transform.position + (target.transform.position - transform.position).normalized, Quaternion.LookRotation(Vector3.forward, VectorPos), transform) ;
                    vitesseAttackRestant = 0;
                }
            }
            vitesseAttackRestant += Time.deltaTime;
            if (vieRestant <= 0)
                NextPhase();
        }
    }
    void NextPhase()
    {
        GameObject temp = Instantiate(nextPhase, transform.position, Quaternion.identity, transform.parent);
        if (temp != null)
            Destroy(gameObject);
    }
    IEnumerator ApparaitreBoss()
    {
        camController.afficherJoueur = false; //mettre camera sur boss
        empecherMoveJoueur.SetActive(true);

        yield return new WaitForSeconds(2);

        StartCoroutine(FadeText()); // afficher le titre du boss

        yield return new WaitForSeconds(4);

        StartCoroutine(FadeText(false)); // effacer le titre du boss
        camController.afficherJoueur = true; //mettre camera sur joueur
        empecherMoveJoueur.SetActive(false);
        bossPret = true;
    }
    public IEnumerator FadeText(bool fade = true, float fadeSpeed = 1f)
    {
        Color color = titreBoss.color;
        float fadeState;
        if (fade)
        {
            color = new Color(color.r, color.g, color.b, 0);
            while (titreBoss.color.a < 1)
            {
                fadeState = color.a + (fadeSpeed * Time.deltaTime);

                color = new Color(color.r, color.g, color.b, fadeState);
                titreBoss.color = color;
                yield return null;
            }
        }
        else
        {
            color = new Color(color.r, color.g, color.b, 1);
            while (titreBoss.color.a > 0)
            {
                fadeState = color.a - (fadeSpeed * Time.deltaTime);

                color = new Color(color.r, color.g, color.b, fadeState);
                titreBoss.color = color;
                yield return null;
            }
        }
    }
    IEnumerator Attack()
    {
        if (bossPret && target != null)
        {
            yield return new WaitForSeconds(5);
            int attack = Random.Range(0, 2);
            switch (attack)
            {
                case 0:
                    StartCoroutine(Attack1());
                    yield return new WaitForSeconds(10f);
                    break;
                case 1:
                    StartCoroutine(Attack2());
                    yield return new WaitForSeconds(10f);
                    break;
                case 2:

                    break;
            }
        }
        yield return null;
        StartCoroutine(Attack());
    }
    IEnumerator Attack1()
    {
        peutBouger = false;
        float nombreDeBalle = 15;
        for (int i = 0; i < 2; i++)
        {
            while (nombreDeBalle > 0)
            {
                GameObject projectileTemp = ObjectPool.instance.GetPoolObject(projectile);
                if (projectileTemp != null && target != null)
                {
                    projectileTemp.transform.position = transform.position;
                    projectileTemp.transform.rotation = transform.rotation;
                    projectileTemp.SetActive(true);
                }
                nombreDeBalle--;
                yield return new WaitForSeconds(0.2f);
            }
            nombreDeBalle = 15;
            if (i == 0)
                yield return new WaitForSeconds(2f);
        }
        peutBouger = true;
    }
    IEnumerator Attack2()
    {
        peutBouger = false;
        float nombreExplosion = 5;

        while (nombreExplosion > 0)
        {
            GameObject explosionSignTemp = ObjectPool.instance.GetPoolObject(explosionSign);
            if (explosionSignTemp != null && target != null)
            {
                explosionSignTemp.transform.position = target.transform.position;
                explosionSignTemp.transform.rotation = transform.rotation;
                explosionSignTemp.SetActive(true);
            }
            nombreExplosion--;
            yield return new WaitForSeconds(1f);
        }
        peutBouger = true;
    }
    IEnumerator Esquive()
    {
        yield return new WaitForSeconds(10);
        int attack = Random.Range(0, 2);
        switch (attack)
        {
            case 0:
                StartCoroutine(EssquiveDroite());
                break;
            case 1:
                StartCoroutine(EssquiveGauche());
                break;
        }
        StartCoroutine(Esquive());
    }
    IEnumerator EssquiveDroite()
    {
        int hautaur = Random.Range(0, 2);
        switch (hautaur)
        {
            case 0:
                transform.Translate(Vector2.right * distanceEsquive + Vector2.up * distanceEsquive / 2);
                break;
            case 1:
                transform.Translate(Vector2.right * distanceEsquive + Vector2.down * distanceEsquive / 2);
                break;
        }
        yield return null;
    }
    IEnumerator EssquiveGauche()
    {
        int hautaur = Random.Range(0, 2);
        switch (hautaur)
        {
            case 0:
                transform.Translate(Vector2.left * distanceEsquive + Vector2.up * distanceEsquive / 2);
                break;
            case 1:
                transform.Translate(Vector2.left * distanceEsquive + Vector2.down * distanceEsquive / 2);
                break;
        }
        yield return null;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print(collision.tag);
        if (collision.tag == "damageZone")
        {
            bossGeneral.vieRestant -= joueur.damage;
            vieRestant -= joueur.damage;
            healthBar.value = vieRestant;
        }
        else if (collision.tag == "bullet")
        {
            Bullet bullet = collision.GetComponent<Bullet>();
            int damage = bullet.howManyDamage();
            bossGeneral.vieRestant -= damage;
            vieRestant -= damage;
            healthBar.value = vieRestant;
        }
    }
}
