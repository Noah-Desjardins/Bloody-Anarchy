using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float vitesseDeplacement = 2.0f;
    [SerializeField] float rollSpeed = 6.0f;
    public float speed;
    public bool canMove = true;
    public Vector2 direction { get; private set; } = Vector2.zero;
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator animator;
    [SerializeField] GameObject ShopUI;

    PlayerAbility playerAbility;
    GameObject abilityContainer;
    Cooldown rollingCooldown;

    void Start()
    {
        abilityContainer = GameObject.FindGameObjectWithTag("abilityContaineur");
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        speed = vitesseDeplacement;
        playerAbility = GetComponent<PlayerAbility>();

        // Initialize rolling cooldown
        if (rollingCooldown == null && playerAbility.GetAbility("canRoll"))
            setRollingCooldown();
    }

    public void setRollingCooldown()
    {
        foreach (Cooldown cooldown in abilityContainer.GetComponentsInChildren<Cooldown>())
        {
            if (cooldown.tag == "canRoll")
                rollingCooldown = cooldown;
        }
    }

    void Update()
    {
        if (direction.x > 0)
            sr.flipX = false;

        if (direction.x < 0)
            sr.flipX = true;

        if (rollingCooldown == null && playerAbility.GetAbility("canRoll"))
            setRollingCooldown();

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
        if (ShopUI.activeSelf || !canMove)
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
        if (!ShopUI.activeSelf)
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
