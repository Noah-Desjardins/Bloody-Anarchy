using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float vitesseDeplacement = 2.0f;
    [SerializeField] float rollSpeed = 6.0f;
    // Start is called before the first frame update
    public float speed;
    public Vector2 direction { get; private set; } = Vector2.zero;
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator animator;
    [SerializeField] GameObject ShopUI;

    PlayerAbility playerAbility;
    [SerializeField] GameObject abilityContainer;
    Cooldown rollingCooldown;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        speed = vitesseDeplacement;
        playerAbility = GetComponent<PlayerAbility>();
    }
    public void setRollingCooldown()
    {
        foreach (Cooldown cooldown in abilityContainer.GetComponentsInChildren<Cooldown>())
        {
            if (cooldown.tag == "canRoll")
                rollingCooldown = cooldown;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (rollingCooldown == null && playerAbility.GetAbility("canRoll"))
            setRollingCooldown();

        if (direction.x > 0)
            sr.flipX = false;
            
        if (direction.x < 0)
            sr.flipX = true;
        if (direction.magnitude > 0)
        {
            Deplacement();
            rollBoost();
        }
        else
            animator.SetBool("walking", false);

    }
    void Deplacement()
    {
        animator.SetBool("walking", true);
        if (ShopUI.activeSelf)
            direction = new Vector2(0, 0);
        rb.MovePosition((rb.position + direction.normalized * speed * Time.fixedDeltaTime));
    }
    public bool flipped(bool notWhenYAxes = false)
    {
        if (!notWhenYAxes)
            return sr.flipX;
        if (direction.y == 1 || direction.y == -1)
            return false;
        return sr.flipX;
            
    }
    public void Move(InputAction.CallbackContext context)
    {
        if(!ShopUI.activeSelf)
            direction = context.ReadValue<Vector2>();
    }
    void rollBoost()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("roll"))
        {
            speed = rollSpeed;

        }
        else
        {
            speed = vitesseDeplacement;
        }
    }
    public void Roll(InputAction.CallbackContext context)
    {
        if (playerAbility.GetAbility("canRoll") && context.started && !rollingCooldown.isCoolingDown)
        {
            if (direction.magnitude != 0 && !animator.GetCurrentAnimatorStateInfo(0).IsName("roll"))
            {
                rollingCooldown.StartCoolDown();
                animator.SetTrigger("roll");
            }
        }

                
    }
}
