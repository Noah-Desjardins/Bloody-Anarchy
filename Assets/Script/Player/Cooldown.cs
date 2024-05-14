using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cooldown : MonoBehaviour
{
    [SerializeField] float cooldownTime;
    float cooldownTimeTemp;
    public bool isCoolingDown;
    Image abilityImage;
    // Start is called before the first frame update
    void Start()
    {
        isCoolingDown = false;
        cooldownTimeTemp = cooldownTime;
        abilityImage = GetComponent<Image>();
        abilityImage.fillAmount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (isCoolingDown)
        {
            if (cooldownTimeTemp <= 0)
            {
                isCoolingDown = false;
                abilityImage.fillAmount = 1;
                cooldownTimeTemp = cooldownTime;
            }
            else
            {
                abilityImage.fillAmount += 1 / cooldownTime * Time.deltaTime;
                cooldownTimeTemp -= Time.deltaTime;
            }
        }
        else
        {
            cooldownTimeTemp = cooldownTime;
        }
    }
    public void StartCoolDown()
    {
        if (!isCoolingDown)
        {
            abilityImage.fillAmount = 0;
            isCoolingDown = true;
        }
            
    }
    public float TimeRemaining() => cooldownTimeTemp;
}
