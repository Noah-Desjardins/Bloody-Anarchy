using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TTD : MonoBehaviour
{
    [SerializeField] float tempsDeVie = 3.0f;
    float tempsInit;
    void OnEnable()
    {
        tempsInit = Time.time;
    }
   
    void Update()
    {
        // Temps avant de desactiver
        if (Time.time > tempsInit + tempsDeVie)
        {
            gameObject.SetActive(false);
        }
    }
}
