using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Load : MonoBehaviour
{
    string levelToLoad = "SampleScene";
    void Start()
    {
        //permet d'afficehr le jeu 
        print("loading");
        if (PlayerPrefs.HasKey("LevelToLoad"))
            levelToLoad = PlayerPrefs.GetString("LevelToLoad");
        SceneManager.LoadScene(levelToLoad);
    }
}
