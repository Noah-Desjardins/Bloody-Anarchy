using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class deathUI : MonoBehaviour
{
    // Start is called before the first frame update
    CanvasGroup canvas;
    TextMeshProUGUI score;
    GameManager gameManager;
    void Start()
    {
        canvas = GetComponent<CanvasGroup>();
        score = GameObject.FindGameObjectWithTag("score").GetComponent<TextMeshProUGUI>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void show(int scorepercent = 0, bool show = true)
    {
        canvas.alpha = show?1.0f:0f;
        canvas.interactable = show;

        score.text =PlayerPrefs.HasKey("score")? scorepercent + "%":"There aren't any score";
    }
    public void backToLobby()
    {
        show(0,false);
        gameManager.ChangeRoom("lobby");
    }
}
