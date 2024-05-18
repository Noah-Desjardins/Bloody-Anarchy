using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossPhase3Hand : MonoBehaviour
{
    public bool isAttacking = false;
    bool isGoingBack = false;
    bool isGoingTopRight = false;
    bool isGoingLeft = false;
    bool isParrying = false;
    bool isGoingNearPlayer = false;
    bool isTurningAroundPlayer = false;

    [SerializeField] Player player;
    [SerializeField] float speed = 5f;
    float speedTemp;
    //Attaque rush du joueur
    bool isRushing = false;
    //modifiée quand rushPlayer() est appelée
    Vector3 playerRushedPosition = Vector2.zero;

    [SerializeField] float turningRadius = 5.0f;
    float StartAngle = 0;
    float angle = -1;

    //Attaque tirer en ligne droite sur le joueur
    [SerializeField] GameObject bulletPrefab;

    SpriteRenderer sr;
    Vector3 localPos = Vector3.zero;
    Vector3 topRight = Vector3.zero;

    void Start()
    {
        speedTemp = speed;
        localPos = transform.localPosition;
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!isRushing && !isGoingBack && !isGoingTopRight && !isGoingLeft && !isGoingNearPlayer)
            transform.rotation = Quaternion.LookRotation(Vector3.forward, player.transform.position - transform.position);

        if (isRushing)
        {
            transform.position += transform.up * Time.deltaTime * speedTemp;
        }
        if (isGoingBack)
        {
            transform.position += transform.up * Time.deltaTime * (speedTemp * 1.5f);
        }
        if (isGoingTopRight)
        {
            transform.position += transform.up * Time.deltaTime * (speedTemp * 1.5f);
        }
        if (isGoingLeft)
        {
            transform.position += transform.right * Time.deltaTime * (speedTemp * 0.5f);
        }
        if (isGoingNearPlayer)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.forward, player.transform.position - transform.position);
            transform.position += transform.up * Time.deltaTime * (speedTemp * 1.5f);
        }
        if (isTurningAroundPlayer)
        {
            if (angle == -1)
                angle = StartAngle;
            angle += Time.deltaTime * (speed * 0.3f);
            transform.position = player.transform.position + new Vector3(
                Mathf.Cos(angle) * turningRadius,
                Mathf.Sin(angle) * turningRadius,
                0
            );
        }
    }

    public IEnumerator backToPosition()
    {
        isAttacking = false;
        isGoingBack = true;
        while (Mathf.Abs((transform.localPosition - localPos).magnitude) >= 1f && !isAttacking)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.forward, localPos - transform.localPosition);
            yield return null;
        }
        isGoingBack = false;
    }

    public IEnumerator rushPlayer(float timeToRush, float speedrush, bool isXtrem = false)
    {
        isAttacking = true;
        isRushing = true;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, player.transform.position - transform.position);
        speedTemp = speedrush;
        yield return new WaitForSeconds(timeToRush);
        speedTemp = speed;
        isRushing = false;
        if (!isXtrem)
            isAttacking = false;
    }

    public IEnumerator rushPlayerXtrem(float timeToRush, float speedrush)
    {
        isAttacking = true;
        int nbRush = 3;
        while (nbRush > 0)
        {
            StartCoroutine(rushPlayer(timeToRush, speedrush, true));
            nbRush--;
            yield return new WaitForSeconds(timeToRush);
        }
        isAttacking = false;
    }

    public IEnumerator shootFromTop()
    {
        StartCoroutine(goTopRight());
        while (isAttacking)
        {
            if (!isGoingTopRight && !isGoingLeft)
            {
                isGoingLeft = true;
                StartCoroutine(shootLine(16));
                transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.down);
            }
            yield return null;
        }
        isGoingLeft = false;
        isAttacking = false;
    }

    public IEnumerator shootLine(int nbBullet)
    {
        isAttacking = true;
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

    public IEnumerator bulletParry()
    {
        isParrying = true;
        sr.color = Color.blue;
        yield return new WaitForSeconds(5f);
        sr.color = Color.white;
        isParrying = false;
    }

    public IEnumerator shootArroundPlayer()
    {
        StartCoroutine(goNearPlayer());
        while (isAttacking)
        {
            if (!isGoingNearPlayer && !isTurningAroundPlayer)
            {
                isTurningAroundPlayer = true;
                StartCoroutine(shootLine(15));
            }
            yield return null;
        }
        isTurningAroundPlayer = false;
    }

    public IEnumerator goTopRight()
    {
        isAttacking = true;
        isGoingTopRight = true;
        topRight = new Vector3(player.transform.position.x + 6, player.transform.position.y + 7, player.transform.position.z);
        while (Mathf.Abs((transform.position - topRight).magnitude) >= 1f)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.forward, topRight - transform.position);
            yield return null;
        }
        transform.position = topRight;
        isGoingTopRight = false;
    }

    IEnumerator goNearPlayer()
    {
        isGoingNearPlayer = true;
        isAttacking = true;
        while (Mathf.Abs((transform.position - player.transform.position).magnitude) >= 4f)
        {
            yield return null;
        }
        float y = transform.position.y - player.transform.position.y;
        StartAngle = Mathf.Asin(y / turningRadius);
        isGoingNearPlayer = false;
    }

    public IEnumerator testAttack()
    {
        yield return new WaitForSeconds(3f);
        StartCoroutine(rushPlayerXtrem(0.5f, 10));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("bullet") && isParrying)
        {
            collision.gameObject.SetActive(false);
            GameObject bulletTemp = ObjectPool.instance.GetPoolObject(bulletPrefab);
            bulletTemp.transform.position = collision.transform.position;
            bulletTemp.transform.rotation = Quaternion.LookRotation(Vector3.forward, player.transform.position - collision.transform.position);
            bulletTemp.SetActive(true);
        }
    }
}
