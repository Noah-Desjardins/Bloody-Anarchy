using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damageZone : MonoBehaviour
{
    // Start is called before the first frame update
    PlayerMovement player;
    SpriteRenderer sr;
    float SlashTime = 0.517f;
    float tempSlashTime;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();    
        player = GetComponentInParent<PlayerMovement>();
        tempSlashTime = SlashTime;
        sr.flipX = player.flipped(true);
    }

    // Update is called once per frame
    void Update()
    {
        tempSlashTime -= Time.deltaTime;
        if (tempSlashTime <= 0)
            Destroy(gameObject);
    }
}
