using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Menu : MonoBehaviour
{
    CanvasGroup canvas;
    GameManager gameManager;
    void Start()
    {
        canvas = GetComponent<CanvasGroup>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator Desactivate()
    {
        yield return new WaitForSeconds(0);
        this.gameObject.SetActive(false);
    }
    public void backToLobby()
    {
        gameManager.ChangeRoom("lobby", false);
        StartCoroutine(Desactivate());
       
    }
}
