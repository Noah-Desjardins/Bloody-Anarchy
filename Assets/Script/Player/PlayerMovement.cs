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
    [SerializeField] GameObject ShopUI;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (direction.x > 0)
            sr.flipX = false;
            
        if (direction.x < 0)
            sr.flipX = true;
        if (direction.magnitude > 0)
            Deplacement();
        else
            animator.SetBool("walking", false);

    }
    void Deplacement()
    {
        animator.SetBool("walking", true);
        if (ShopUI.activeSelf)
            direction = new Vector2(0, 0);
        rb.MovePosition((rb.position + direction.normalized * vitesseDeplacement * Time.fixedDeltaTime));
    }
    public bool flipped()
    {
        return sr.flipX;
    }
    public void Move(InputAction.CallbackContext context)
    {
        if(!ShopUI.activeSelf)
            direction = context.ReadValue<Vector2>();
    }
}
