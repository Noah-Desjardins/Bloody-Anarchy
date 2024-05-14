using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossPhase3Hand : MonoBehaviour
{
    public bool isAttacking = false;
    bool isGoingBack = false;
    bool isGoingTopRight = false;

    [SerializeField] Player player;
    [SerializeField] float speed = 5f;
    float speedTemp;
    //Attaque rush du joueur
    bool isRushing = false;
    //modifiée quand rushPlayer() est appelée
    Vector3 playerRushedPosition = Vector2.zero;

    //Attaque tirer en ligne droite sur le joueur
    [SerializeField] GameObject bulletPrefab;


    Vector3 localPos = Vector3.zero;
    Vector3 topRight = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        localPos = transform.localPosition;
        StartCoroutine(testAttack());
    }

    // Update is called once per frame
    void Update()
    {
        if (!isRushing && !isGoingBack && !isGoingTopRight)
            transform.rotation = Quaternion.LookRotation(Vector3.forward,player.transform.position - transform.position);

        if (isRushing)
        {
            transform.position += transform.up * Time.deltaTime * speedTemp;
        }
        if (isGoingBack)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.forward, localPos - transform.localPosition);
            transform.position += transform.up * Time.deltaTime * (speedTemp * 1.5f);
        }
        if (isGoingTopRight)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.forward, topRight - transform.position);
            transform.position += transform.up * Time.deltaTime * (speedTemp * 1.5f); // ICI PROBLEMO ÇA VEUT PAS BOUGER ET JSP PK!!!
        }
    }
    public IEnumerator backToPosition()
    {
        isAttacking = false;
        isGoingBack = true;
        while (transform.localPosition != localPos && !isAttacking)
        {
            yield return null;
        }
        isGoingBack = false;
    }
    public IEnumerator rushPlayer(float timeToRush, float speedrush)
    {
        isAttacking = true;
        isRushing = true;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, player.transform.position - transform.position);
        speedTemp = speedrush;
        yield return new WaitForSeconds(timeToRush);
        speedTemp = speed;
        isRushing = false;
        isAttacking = false;
        StartCoroutine(backToPosition());
    }
    public IEnumerator rushPlayerXtrem(float timeToRush, float speedrush)
    {
        isAttacking = true;
        int nbRush = 3;
        while (nbRush > 0)
        {
            yield return new WaitForSeconds(timeToRush);
            StartCoroutine(rushPlayer(timeToRush, speedrush));
            nbRush--;
        }
        isAttacking = false;
    }
    public IEnumerator goTopRight()
    {
        isGoingTopRight = true;
        isAttacking = true;
        topRight = new Vector3(player.transform.position.x + 8, player.transform.position.y + 8, player.transform.position.z);
        while (transform.position != topRight)
        {
            yield return null;
        }
        isGoingTopRight = false;
    }
    public IEnumerator shootLine()
    {
        isAttacking = true;
        int nbBullet = 21;
        while (nbBullet > 0)
        {
            yield return new WaitForSeconds(0.2f);
            GameObject bulletTemp = ObjectPool.instance.GetPoolObject(bulletPrefab);
            bulletTemp.transform.position = transform.position;
            bulletTemp.transform.rotation = transform.rotation;
            bulletTemp.SetActive(true);
            nbBullet--;
        }
        isAttacking = false;
    }
    public IEnumerator testAttack()
    {
        yield return new WaitForSeconds(3f);
        StartCoroutine(rushPlayer(2,2));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
