using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class AbilityContainer : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> allAbilities;
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
                Instantiate(allAbilities[i], new Vector3(gameObject.transform.position.x + (i * -120f), gameObject.transform.position.y, 100), Quaternion.identity, gameObject.transform);

            }
            abilitiesCountTemp = allAbilities.Count;
        }
    }
}
