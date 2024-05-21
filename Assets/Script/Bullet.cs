using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed;
    [SerializeField] AudioClip hitSound;
    [SerializeField] AudioSource audioSource;
    Animator animator;
    Collider2D bulletCollider;
    public int mindamage = 2;
    public int maxdamage = 5;
    bool hasHit = false;
    float explostionDuration = 0;

    // Start is called before the first frame update
    void Start()
    {
        hasHit = false;
        animator = GetComponentInChildren<Animator>();
        bulletCollider = GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }
    private void OnEnable()
    {
        if (bulletCollider != null)
            bulletCollider.enabled = true;
        hasHit = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (!hasHit)
            transform.Translate(Vector2.up * bulletSpeed * Time.deltaTime, Space.Self);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(waitForAnim());

        if (hitSound != null)
        {
            audioSource.clip = hitSound;
            audioSource.Play();
        }
    }
    public IEnumerator waitForAnim()
    {
        animator.SetTrigger("hit");
        bulletCollider.enabled = false;
        hasHit = true;
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
    public int howManyDamage() => UnityEngine.Random.Range(mindamage, maxdamage + 1);
}