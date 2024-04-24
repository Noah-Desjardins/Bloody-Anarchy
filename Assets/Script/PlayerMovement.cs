using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float vitesseDeplacement = 2.0f;
    // Start is called before the first frame update
    Vector2 direction = Vector2.zero;
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator animator;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (direction.x > direction.y)
            sr.flipX = false;
            
        if (direction.y > direction.x)
            sr.flipX = true;
        if (direction.magnitude > 0)
            Deplacement();
        else
            animator.SetBool("walking", false);

    }
    void Deplacement()
    {
        print("ddd");
        animator.SetBool("walking", true);
        rb.MovePosition((rb.position + direction.normalized * vitesseDeplacement * Time.deltaTime));
    }
    public void Move(InputAction.CallbackContext context)
    {
        print("ddd");
        direction = context.ReadValue<Vector2>();
    }
}
