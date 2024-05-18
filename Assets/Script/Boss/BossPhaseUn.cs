using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField] GameObject projectile;
    [SerializeField] GameObject explosionSign;
    [SerializeField] GameObject target;
    [SerializeField] GameObject damageZone;
    [SerializeField] UIController uicontroller;
    [SerializeField] GameObject empecherMoveJoueur;
    [SerializeField] Slider healthBar;
    [SerializeField] AudioClip projectileSound;
    [SerializeField] AudioClip explosionSound;
    [SerializeField] AudioClip bossRoarSound;

    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        camController = FindAnyObjectByType<CameraController>();
        bossGeneral = GetComponent<BossGeneral>();
        joueur = target.GetComponent<Player>();

        vieRestant = vie;
        vitesseAttackRestant = vitesseAttack;
        healthBar.maxValue = vie;
        healthBar.value = vieRestant;

        StartCoroutine(Esquive());
        StartCoroutine(Attack());
        StartCoroutine(ApparaitreBoss());
    }

    void Update()
    {
        if (bossPret && target != null)
        {
            Vector3 VectorPos = target.transform.position - transform.position;
            if (VectorPos.magnitude > 2.5)
            {
                if (peutBouger)
                    transform.Translate(VectorPos.normalized * vitesse * Time.deltaTime); //marcher direction le joueur
            }
            else
            {
                if (vitesseAttackRestant > vitesseAttack)
                {
                    //attack de corp a corp (de proche)
                    Instantiate(damageZone, transform.position + (target.transform.position - transform.position).normalized, Quaternion.LookRotation(Vector3.forward, VectorPos), transform);
                    vitesseAttackRestant = 0;
                }
            }
            vitesseAttackRestant += Time.deltaTime;
            if (vieRestant <= 0)
                Destroy(gameObject);
        }
    }
    IEnumerator ApparaitreBoss()
    {
        camController.afficherJoueur = false; //mettre camera sur boss
        empecherMoveJoueur.SetActive(true);
        audioSource.PlayOneShot(bossRoarSound);

        yield return new WaitForSeconds(2);

        StartCoroutine(uicontroller.FadeText()); // afficher le titre du boss

        yield return new WaitForSeconds(4);

        StartCoroutine(uicontroller.FadeText(false)); // effacer le titre du boss
        camController.afficherJoueur = true; //mettre camera sur joueur
        empecherMoveJoueur.SetActive(false);
        bossPret = true;
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
                    audioSource.PlayOneShot(projectileSound);
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
                audioSource.PlayOneShot(explosionSound);
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
        if (collision.tag == "damageZone" || collision.tag == "Bullet")
        {
            bossGeneral.vieRestant -= joueur.damage;
            vieRestant -= joueur.damage;
            healthBar.value = vieRestant;
        }
    }
}
