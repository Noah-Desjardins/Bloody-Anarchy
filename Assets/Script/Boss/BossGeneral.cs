using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class BossGeneral : MonoBehaviour
{
    [SerializeField] float vieTotal = 10000;
    public float vieRestant;
    float healthtemp;
    public float pourcentageFait = 0;
    GameManager gameManager;
    void Start()
    {
        vieRestant = vieTotal;
        gameManager = FindAnyObjectByType<GameManager>();
        healthtemp = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (vieRestant != healthtemp)
        {
            pourcentageFait = Mathf.Floor(100 - (vieRestant * 100 / vieTotal));
            if (pourcentageFait > 100)
                pourcentageFait = 100;
            healthtemp = vieRestant;
        }

    }
    public void bossDead()
    {
        PlayerPrefs.SetInt("score", (int)pourcentageFait);
        gameManager.ChangeRoom("lobby");
    }
}
