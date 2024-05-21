using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PlayerAbility : MonoBehaviour
{
    [SerializeField] List<GameObject> AbilitiesPrefabs;
    AbilityContainer abilityContainer;
    bool canRoll = false;
    bool canShoot = false;
    bool canSlash = true;
    bool canPotion = false;
    bool canPotionShield = false;
    // Start is called before the first frame update
    void Start()
    {
        abilityContainer = GameObject.FindGameObjectWithTag("abilityContaineur").GetComponent<AbilityContainer>();
        PlayerPrefs.SetInt("canRoll", BoolToInt(canRoll));
        PlayerPrefs.SetInt("canShoot", BoolToInt(canShoot));
        PlayerPrefs.SetInt("canSlash", BoolToInt(canSlash));
        PlayerPrefs.SetInt("canPotion", BoolToInt(canPotion));
        PlayerPrefs.SetInt("canPotionShield", BoolToInt(canPotionShield));
        PlayerPrefs.Save();

        showAbility();
    }
    public void showAbility()
    {
        eraseAbility();
        foreach (var ability in AbilitiesPrefabs)
        {
            // Un peu mal fait mais il faut que le tag du prefab de l'Abileté soit la clé du playerPref :)
            if (GetAbility(ability.tag))
            {
                print(ability.tag);
                abilityContainer.allAbilities.Add(ability);
            }
            /*else
            {
                if (PlayerPrefs.HasKey(ability.tag))
                {
                    abilityContainer.allAbilities.Remove(ability);
                }
            }*/
        }
        abilityContainer.ShowAbility();
    }
    void eraseAbility()
    {
        foreach (var ability in AbilitiesPrefabs)
        {
            abilityContainer.allAbilities.Remove(ability);
            print("remove");
        }
    }

    public void SwitchAbility(string abilityName)
    {
        if (PlayerPrefs.HasKey(abilityName))
        {
            PlayerPrefs.SetInt(abilityName, PlayerPrefs.GetInt(abilityName) == 0 ? 1:0);
            PlayerPrefs.Save();
            showAbility();
        }
        
    }
    public bool GetAbility(string abilityName)
    {
        if (PlayerPrefs.HasKey(abilityName))
        {
            return PlayerPrefs.GetInt(abilityName) == 1;
        }
        return false;
    }
    public int BoolToInt(bool value) => value ? 1 : 0;
    public bool IntToBool(int value) => value == 1;

}
