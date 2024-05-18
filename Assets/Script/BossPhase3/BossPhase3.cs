using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BossPhase3 : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] int startHealth = 6000;
    int health;
    [SerializeField] int attack = 5;
    [SerializeField] Slider healthBar;
    float followSharpness = 0.005f;
    Vector3 _followOffset;

    void Start()
    {
        _followOffset = transform.position - player.transform.position;
        health = startHealth;
        healthBar.value = 1;
    }

    void Update()
    {
        Vector3 targetPosition = player.transform.position + _followOffset;
        transform.position += (targetPosition - transform.position) * followSharpness;
    }

    public void takeDamage(int damage)
    {
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
            health -= damage;
        }
    }
}
