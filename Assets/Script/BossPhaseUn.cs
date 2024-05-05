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
        if (bossPret)
        {
            Vector3 VectorPos = target.transform.position - transform.position;
            if (VectorPos.magnitude > 2)
                transform.Translate(VectorPos.normalized * vitesse * Time.deltaTime);
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
        yield return new WaitForSeconds(2);
        int attack = Random.Range(0, 3);
        switch (attack)
        {
            case 0:

                break;
            case 1:

                break;
            case 2:

                break;
        }
        StartCoroutine(Attack());
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
            case 2:

                break;
        }
        StartCoroutine(Esquive());
    }
    IEnumerator EssquiveDroite()
    {
        transform.Translate(Vector2.right * distanceEsquive);
        yield return null;
    }
    IEnumerator EssquiveGauche()
    {
        transform.Translate(Vector2.left * 10);
        yield return null;
    }
}
