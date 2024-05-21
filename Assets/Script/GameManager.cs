using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] UIController uicontroller;
    [SerializeField] bool fadeText = false;
    // Start is called before the first frame update
    void Start()
    {
        
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
            StartCoroutine(uicontroller.Fade(false));
    }
    void Update()
    {
        
    }
    public void ChangeRoom(string sceneName, bool attende = true)
    {
        PlayerPrefs.SetString("LevelToLoad", sceneName);
        PlayerPrefs.Save();
        StartCoroutine(waitForFade(attende));
    }
    IEnumerator waitForFade(bool attende)
    {
        //FADE ICI
        if (attende)
        {
            StartCoroutine(uicontroller.Fade());
            yield return new WaitForSeconds(1.5f);
        }
        SceneManager.LoadScene("Load");
    }
}
