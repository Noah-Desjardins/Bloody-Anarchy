using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TTT : MonoBehaviour
{
    [SerializeField] float tempsDeVie = 3.0f;
    float tempsInit;
    void Start()
    {
        tempsInit = Time.time;
    }

    void Update()
    {
        // Temps avant de desactiver
        if (Time.time > tempsInit + tempsDeVie)
        {
            Destroy(gameObject);
        }
    }



}
