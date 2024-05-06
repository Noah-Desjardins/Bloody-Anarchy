using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    Camera cam;

    SpriteRenderer sr;
    Animator animator;
    [SerializeField] Bullet bulletPrefab; // Ici ça va être bullet
    [SerializeField] GameObject? shop;

    PlayerAbility playerAbility;
    [SerializeField] GameObject abilityContainer;
    Cooldown shootingCooldown; // Ici mettre le cooldown de shoot

    Vector3 mousePosition = Vector3.zero;
    Quaternion bulletRotation = Quaternion.identity;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        playerAbility = GetComponent<PlayerAbility>();
    }

    // Update is called once per frame
    void Update()
    {
        if (shootingCooldown == null && playerAbility.GetAbility("canShoot"))
            setShootingCooldown();
    }
    public void setShootingCooldown()
    {
        foreach (Cooldown cooldown in abilityContainer.GetComponentsInChildren<Cooldown>())
        {
            if (cooldown.tag == "canShoot")
                shootingCooldown = cooldown;
        }
    }
    public void Attack(InputAction.CallbackContext context)
    {
        
        if (!shop.activeSelf)
        {
            if (context.started && playerAbility.GetAbility("canShoot") /*&& !shootingCooldown.isCoolingDown*/)
            {
                mousePosition = Input.mousePosition;
                mousePosition = cam.ScreenToWorldPoint(mousePosition);
                print(mousePosition);
                Vector3 v = mousePosition - transform.position;
                bulletRotation = Quaternion.LookRotation(Vector3.forward, v);
                //shootingCooldown.StartCoolDown();

                //METTRE DANS UN OBJECT POOL OUBLIE PAS DU CON!
                    if (sr.flipX)
                        Instantiate(bulletPrefab, new Vector3(transform.position.x - sr.bounds.size.x / 2f, transform.position.y, transform.position.z), bulletRotation);
                    else
                       Instantiate(bulletPrefab, new Vector3(transform.position.x + sr.bounds.size.x / 2f, transform.position.y, transform.position.z), bulletRotation);

                //animator.SetTrigger("sideAttack");
            }
        }
    }
}
