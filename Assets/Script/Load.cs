using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Load : MonoBehaviour
{
    // Start is called before the first frame update
    string levelToLoad = "Lobby";
    void Start()
    {
        //permet d'afficehr le jeu 
        print("loading");
        if (PlayerPrefs.HasKey("LevelToLoad"))
            levelToLoad = PlayerPrefs.GetString("LevelToLoad");
        SceneManager.LoadScene(levelToLoad);
    }
}
