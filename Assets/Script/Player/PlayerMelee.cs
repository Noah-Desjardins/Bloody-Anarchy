using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMelee : MonoBehaviour
{
    SpriteRenderer sr;
    SpriteRenderer attackZone;
    Animator animator;
    PlayerMovement playerMovement;
    GameObject temp;
    [SerializeField] GameObject attackPrefab;
    [SerializeField] damageZone damageZone;
    [SerializeField] GameObject shop;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Attack(InputAction.CallbackContext context)
    {
        if(!shop.activeSelf)
        {
            if (context.started)
            {
                if (playerMovement.direction.y == 1)
                {
                    Instantiate(attackPrefab, new Vector3(transform.position.x - 0.2f, transform.position.y + sr.bounds.size.y / 2f, transform.position.z), Quaternion.Euler(0, 0, 90), transform);
                }
                else if (playerMovement.direction.y == -1)
                {
                    Instantiate(attackPrefab, new Vector3(transform.position.x + 0.2f, transform.position.y - sr.bounds.size.y / 2f, transform.position.z), Quaternion.Euler(0, 0, -90), transform);
                }
                else
                {
                    if (sr.flipX)
                        Instantiate(attackPrefab, new Vector3(transform.position.x - sr.bounds.size.x / 2f, transform.position.y, transform.position.z), quaternion.identity, transform);
                    else
                        Instantiate(attackPrefab, new Vector3(transform.position.x + sr.bounds.size.x / 2f, transform.position.y, transform.position.z), quaternion.identity, transform);
                }


                animator.SetTrigger("sideAttack");
            }
        }    
    }
}
