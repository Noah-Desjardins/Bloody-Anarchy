using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BossPhase3 : MonoBehaviour
{
    Player player;
    [SerializeField] int startHealth = 6000;
    int health;
    [SerializeField] int attack = 5;
    [SerializeField] Slider healthBar;
    float followSharpness = 0.005f;
    Vector3 _followOffset;
    changeMusicPhase audioSource;
    [SerializeField] AudioClip music;
    BossGeneral bossGeneral;
    void Start()
    {
        audioSource = GameObject.FindGameObjectWithTag("music").GetComponent<changeMusicPhase>();
        bossGeneral = GetComponentInParent<BossGeneral>();
        player = GameObject.FindGameObjectWithTag("player").GetComponent<Player>();
        _followOffset = new Vector3(player.transform.position.x, player.transform.position.y + 5) - player.transform.position;
        health = startHealth;
        healthBar.value = 1;
        audioSource.changeMusic(music);
    }

    void Update()
    {
        Vector3 targetPosition = player.transform.position + _followOffset;
        transform.position += (targetPosition - transform.position) * followSharpness;
    }

    public void takeDamage(int damage)
    {
        bossGeneral.vieRestant -= damage;
        health -= damage;
        healthBar.value = (float)health / startHealth;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "bullet")
        {
            Bullet bullet = collision.GetComponent<Bullet>();
            int damage = bullet.howManyDamage() / 5;
            takeDamage(damage);
        }
        if (collision.tag == "damageZone")
        {
            takeDamage(player.damage);
        }
    }
}
