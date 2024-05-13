using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPhase3Hand : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] float speed = 5f;

    Rigidbody2D rb;

    //Attaque rush du joueur
    bool isRushing = false;
    //modifiée quand rushPlayer() est appelée
    Vector3 playerRushedPosition = Vector2.zero;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(testAttack());
    }

    // Update is called once per frame
    void Update()
    {
        if (!isRushing)
            transform.rotation = Quaternion.LookRotation(Vector3.forward,player.transform.position - transform.position);

        if (isRushing)
        {
            print(playerRushedPosition);
            transform.position += transform.up * Time.deltaTime * speed;
        }
    }
    public IEnumerator rushPlayer()
    {
        isRushing = true;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, player.transform.position - transform.position);
        yield return new WaitForSeconds(2.5f);
        isRushing = false;
    }
    public IEnumerator testAttack()
    {
        yield return new WaitForSeconds(3f);
        StartCoroutine(rushPlayer());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
