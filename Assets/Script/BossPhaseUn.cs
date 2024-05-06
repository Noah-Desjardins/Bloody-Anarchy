using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPhaseUn : MonoBehaviour
{
    [SerializeField] int vie = 1000;
    int vieRestant;
    Rigidbody2D rb;
    [SerializeField] int attack = 5;
    [SerializeField] GameObject projectile;
    [SerializeField] GameObject target;
    [SerializeField] UIController uicontroller;
    [SerializeField] float vitesse = 4;
    bool bossPret = false;
    bool peutBouger = true;
    CameraController camController;
    [SerializeField] GameObject empecherMoveJoueur;
    [SerializeField] float distanceEsquive = 10;
    void Start()
    {
        camController = FindAnyObjectByType<CameraController>();
        vieRestant = vie;
        rb = GetComponent<Rigidbody2D>();
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
            if (VectorPos.magnitude > 2)
            {
                if (peutBouger)
                    transform.Translate(VectorPos.normalized * vitesse * Time.deltaTime);
            }
        }
    }
    IEnumerator ApparaitreBoss()
    {
        camController.afficherJoueur = false;
        empecherMoveJoueur.SetActive(true);
        yield return new WaitForSeconds(2);
        StartCoroutine(uicontroller.FadeText());
        yield return new WaitForSeconds(4);
        StartCoroutine(uicontroller.FadeText(false));
        camController.afficherJoueur = true;
        empecherMoveJoueur.SetActive(false);
        bossPret = true;
    }
    IEnumerator Attack()
    {
        if (bossPret && target != null)
        {
            yield return new WaitForSeconds(7);
            int attack = Random.Range(0, 1);
            switch (attack)
            {
                case 0:
                    StartCoroutine(Attack1());
                    yield return new WaitForSeconds(10f);
                    break;
                case 1:

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
                yield return new WaitForSeconds(0.2f);
                GameObject projectileTemp = ObjectPool.instance.GetPoolObject(projectile);
                if (projectileTemp != null)
                {
                    projectileTemp.transform.position = transform.position;
                    projectileTemp.transform.rotation = transform.rotation;
                    projectileTemp.SetActive(true);
                }
                nombreDeBalle--;
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
        float nombreDeBalle = 5;

        while (nombreDeBalle > 0)
        {
            yield return new WaitForSeconds(0.2f);
            GameObject projectileTemp = ObjectPool.instance.GetPoolObject(projectile);
            if (projectileTemp != null)
            {
                projectileTemp.transform.position = transform.position;
                projectileTemp.transform.rotation = transform.rotation;
                projectileTemp.SetActive(true);
            }
            nombreDeBalle--;
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
}
