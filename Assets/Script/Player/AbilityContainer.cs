using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class AbilityContainer : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> allAbilities;
    GameObject temp;
    int abilitiesCountTemp = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (allAbilities.Count != abilitiesCountTemp)
        {
            foreach (Transform ability in gameObject.transform)
            {
                Destroy(ability.gameObject);
            }
            for (int i = 0; i < allAbilities.Count; i++)
            {
                temp = Instantiate(allAbilities[i], new Vector3(gameObject.transform.position.x + (i * -2.2f), gameObject.transform.position.y, 0), Quaternion.identity, gameObject.transform);

            }
            abilitiesCountTemp = allAbilities.Count;
        }
    }
}
