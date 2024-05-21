using System.Collections;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    [SerializeField] float sweepDuration = 2f;
    [SerializeField] float damage = 50f;
    [SerializeField] AudioClip sweepSound;
    [SerializeField] AudioSource audioSource;
    LineRenderer lineRenderer;
    bool isSweeping = false;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
        }

        lineRenderer.startWidth = 0.2f;
        lineRenderer.endWidth = 0.2f;
        lineRenderer.positionCount = 2;

        // Initialize audio source
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.loop = true; // Loop the sound while sweeping
    }

    public IEnumerator SweepAcrossScreen()
    {
        isSweeping = true;
        float elapsedTime = 0f;
        Vector3 startPosition = new Vector3(-10, transform.position.y, 0);
        Vector3 endPosition = new Vector3(10, transform.position.y, 0);

        while (elapsedTime < sweepDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / sweepDuration;
            Vector3 currentPosition = Vector3.Lerp(startPosition, endPosition, t);
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, currentPosition);

            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, currentPosition - transform.position);
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider != null && hit.collider.CompareTag("Player"))
                {
                    Player player = hit.collider.GetComponent<Player>();
                    if (player != null)
                    {
                        int damageToApply = Mathf.RoundToInt(damage * Time.deltaTime);
                        player.health -= damageToApply;
                    }
                }
            }

            if (!audioSource.isPlaying && sweepSound != null)
            {
                audioSource.clip = sweepSound;
                audioSource.Play();
            }

            yield return null;
        }

        isSweeping = false;
        lineRenderer.enabled = false;
        audioSource.Stop();
    }
}