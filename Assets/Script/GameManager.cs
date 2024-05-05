using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] UIController uicontroller;
    [SerializeField] bool fadeText = false;
    // Start is called before the first frame update
    void Start()
    {
        if(fadeText)
            StartCoroutine(uicontroller.FadeText(false));
        else
            StartCoroutine(uicontroller.Fade(false));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeRoom(string sceneName)
    {
        PlayerPrefs.SetString("LevelToLoad", sceneName);
        PlayerPrefs.Save();
        StartCoroutine(waitForFade());
    }
    IEnumerator waitForFade()
    {
        //FADE ICI
        StartCoroutine(uicontroller.Fade());
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("Load");
    }
}
