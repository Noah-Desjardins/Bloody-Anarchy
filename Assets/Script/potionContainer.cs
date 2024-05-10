using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class potionContainer : MonoBehaviour
{
    public int nbPotions = 0;
    int nbPotionsTemp = 0;
    int nbPotionsStart = 0;
    [SerializeField] GameObject potionPrefab;
    [SerializeField] PlayerAbility playerAbility;
    GameObject potionTemp;
    Image imagePotionTemp;
    // Start is called before the first frame update
    void Start()
    {
        nbPotionsStart = nbPotions;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerAbility.GetAbility("canPotion") && nbPotions != nbPotionsTemp)
        {
            nbPotionsStart = Mathf.Max(nbPotionsStart, nbPotions);
            foreach (Transform potion in gameObject.transform)
            {
                Destroy(potion.gameObject);
            }
            for (int i = 0; i < nbPotionsStart; i++)
            {
                potionTemp = Instantiate(potionPrefab, new Vector3(gameObject.transform.position.x + (i * 0.5f), gameObject.transform.position.y, 0), Quaternion.identity, gameObject.transform);               
                if (i >= nbPotions)
                {
                    imagePotionTemp = potionTemp.GetComponentInChildren<Image>();
                    var tempColor = imagePotionTemp.color;
                    tempColor.a = 0.5f;
                    imagePotionTemp.color = tempColor;
                }
            }
            nbPotionsTemp = nbPotions;
        }
    }
}
