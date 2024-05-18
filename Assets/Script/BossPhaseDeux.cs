using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossPhaseDeux : MonoBehaviour
{
    // Serialized fields
    [SerializeField] int vie = 3000;
    [SerializeField] int attack = 63;
    [SerializeField] GameObject projectile;
    [SerializeField] GameObject target;
    [SerializeField] UIController uicontroller;
    [SerializeField] float vitesse = 4;
    [SerializeField] GameObject empecherMoveJoueur;
    [SerializeField] float distanceEsquive = 10;
    [SerializeField] Slider healthBar;
    [SerializeField] GameObject laserPrefab; // Prefab for the laser attack
    [SerializeField] AudioClip hitSound; // Sound when the boss is hit
    [SerializeField] AudioSource audioSource; // AudioSource for playing sounds

    // Private variables
    int vieRestant;
    Rigidbody2D rb;
    bool bossPret = false;
    bool peutBouger = true;
    CameraController camController;
    BossGeneral bossGeneral;
    Player joueur;

    void Start()
    {
        bossGeneral = GetComponent<BossGeneral>();
        joueur = FindObjectOfType<Player>();
        camController = FindObjectOfType<CameraController>();
        vieRestant = vie;
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

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
                    StartCoroutine(LaserSweepAttack());
                    yield return new WaitForSeconds(2);
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
            rb.AddForce(direction.normalized * vitesse * 10, ForceMode2D.Impulse);
            yield return new WaitForSeconds(1);
            if (target != null)
            {
                Player player = target.GetComponent<Player>();
                if (player != null)
                {
                    player.health -= attack * 2;
                }
            }

            // Play hit sound
            if (hitSound != null)
            {
                audioSource.clip = hitSound;
                audioSource.Play();
            }
        }
        yield return new WaitForSeconds(5);
    }

    IEnumerator LaserSweepAttack()
    {
        peutBouger = false;

        GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
        LaserController laserController = laser.GetComponent<LaserController>();
        if (laserController != null)
        {
            yield return laserController.SweepAcrossScreen();
        }

        Destroy(laser);
        yield return new WaitForSeconds(5);
        peutBouger = true;
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
        int cote = Random.Range(0, 2);
        switch (cote)
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
        int cote = Random.Range(0, 2);
        switch (cote)
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            bossGeneral.vieRestant -= joueur.damage;
            vieRestant -= joueur.damage;
            healthBar.value = vieRestant;

            // Play hit sound
            if (hitSound != null)
            {
                audioSource.clip = hitSound;
                audioSource.Play();
            }
        }
    }
}
