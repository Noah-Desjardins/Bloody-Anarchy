using System.Collections;
using UnityEngine;

public class BossPhaseDeux : MonoBehaviour
{
    [SerializeField] int vie = 3000;
    int vieRestant;
    Rigidbody2D rb;
    [SerializeField] int attack = 63;
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
        camController = FindObjectOfType<CameraController>();
        vieRestant = vie;
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(Esquive());
        StartCoroutine(Attack());
        StartCoroutine(ApparaitreBoss());
    }

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
        while (bossPret && target != null)
        {
            yield return new WaitForSeconds(7);
            int attackType = Random.Range(0, 2);
            switch (attackType)
            {
                case 0:
                    StartCoroutine(ChargeAttack());
                    yield return new WaitForSeconds(2);
                    break;
                case 1:
                    // Ajouter une attaque
                    break;
                case 2:
                    // Ajouter une autre attaque
                    break;
            }
        }
    }

    IEnumerator ChargeAttack()
    {
        yield return new WaitForSeconds(1);

        if (target != null)
        {
            Vector3 direction = target.transform.position - transform.position;
            //https://docs.unity3d.com/ScriptReference/ForceMode2D.Impulse.html
            //applique une force sur l'objet qui recevra la collision
            rb.AddForce(direction.normalized * vitesse * 10, ForceMode2D.Impulse);
            yield return new WaitForSeconds(1);
            // Appliquer les dégâts au joueur
            if (target != null)
            {
                Player player = target.GetComponent<Player>();
                if (player != null)
                {
                    player.health -= attack * 2;
                }
            }
        }
        yield return new WaitForSeconds(5);
    }

    IEnumerator Esquive()
    {
        while (true)
        {
            yield return new WaitForSeconds(10);
            int esquiveType = Random.Range(0, 2);
            switch (esquiveType)
            {
                case 0:
                    StartCoroutine(EsquiveHaut());
                    break;
                case 1:
                    StartCoroutine(EsquiveBas());
                    break;
            }
        }
    }

    IEnumerator EsquiveHaut()
    {
        int hautaur = Random.Range(0, 2);
        switch (hautaur)
        {
            case 0:
                transform.Translate(Vector2.up * distanceEsquive + Vector2.right * distanceEsquive / 2);
                break;
            case 1:
                transform.Translate(Vector2.up * distanceEsquive + Vector2.left * distanceEsquive / 2);
                break;
        }
        yield return null;
    }

    IEnumerator EsquiveBas()
    {
        int hautaur = Random.Range(0, 2);
        switch (hautaur)
        {
            case 0:
                transform.Translate(Vector2.down * distanceEsquive + Vector2.right * distanceEsquive / 2);
                break;
            case 1:
                transform.Translate(Vector2.down * distanceEsquive + Vector2.left * distanceEsquive / 2);
                break;
        }
        yield return null;
    }
}
