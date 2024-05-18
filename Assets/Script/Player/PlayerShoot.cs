using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class PlayerShoot : MonoBehaviour
{
    Camera cam;

    SpriteRenderer sr;
    Animator animator;
    [SerializeField] GameObject bulletPrefab; // Ici ça va être bullet
    [SerializeField] GameObject shop;

    PlayerAbility playerAbility;
    GameObject abilityContainer;
    Cooldown shootingCooldown; // Ici mettre le cooldown de shoot

    Vector3 mousePosition = Vector3.zero;
    Quaternion bulletRotation = Quaternion.identity;

    [SerializeField] Texture2D shootingCursor;
    PlayerCursor playerCursor;

    bool isShooting = false;
    // Start is called before the first frame update
    void Start()
    {
        abilityContainer = GameObject.FindGameObjectWithTag("abilityContaineur");
        
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        playerAbility = GetComponent<PlayerAbility>();
        playerCursor = GetComponent<PlayerCursor>();
        if (playerAbility.GetAbility("canShoot"))
        {
            playerCursor.setCursor(shootingCursor);

        }
    }
    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (shootingCooldown == null && playerAbility.GetAbility("canShoot"))
            setShootingCooldown();
        if (isShooting)
            Shoot();
    }
    public void setShootingCooldown()
    {
        foreach (Cooldown cooldown in abilityContainer.GetComponentsInChildren<Cooldown>())
        {
            if (cooldown.tag == "canShoot")
                shootingCooldown = cooldown;
            print(shootingCooldown);
        }
    }
    public void Shoot()
    {
        if (!shop.activeSelf)
        {
            if (playerAbility.GetAbility("canShoot") && !shootingCooldown.isCoolingDown)
            {
                mousePosition = Input.mousePosition;
                mousePosition = cam.ScreenToWorldPoint(mousePosition);

                GameObject bulletTemp = ObjectPool.instance.GetPoolObject(bulletPrefab);
                shootingCooldown.StartCoolDown();
                if (sr.flipX)
                {
                    bulletTemp.transform.position = new Vector3(transform.position.x - sr.bounds.size.x / 2f, transform.position.y, transform.position.z);

                }
                else
                {
                    bulletTemp.transform.position = new Vector3(transform.position.x + sr.bounds.size.x / 2f, transform.position.y, transform.position.z);
                }
                bulletRotation = Quaternion.LookRotation(Vector3.forward, mousePosition - bulletTemp.transform.position);
                bulletTemp.transform.rotation = bulletRotation;
                bulletTemp.SetActive(true);
                //animator.SetTrigger("sideAttack");
            }
        }
    }
    public void Attack(InputAction.CallbackContext context)
    {

        isShooting = context.ReadValue<float>() > 0;
    }
}
