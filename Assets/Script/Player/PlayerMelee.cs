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
    GameObject temp;
    [SerializeField] GameObject attackPrefab;
    [SerializeField] damageZone damageZone;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Attack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (sr.flipX)
                Instantiate(attackPrefab,new Vector3(transform.position.x - sr.bounds.size.x / 1.7f, transform.position.y, transform.position.z),quaternion.identity,transform);
            else
                Instantiate(attackPrefab, new Vector3(transform.position.x + sr.bounds.size.x/1.7f, transform.position.y, transform.position.z), quaternion.identity, transform);

            animator.SetTrigger("sideAttack");
        }
            
    }
}
