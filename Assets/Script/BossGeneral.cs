using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class BossGeneral : MonoBehaviour
{
    [SerializeField] int vieTotal = 10000;
    public int vieRestant;
    public int pourcentageFait = 0;
    void Start()
    {
        vieRestant = vieTotal;
    }

    // Update is called once per frame
    void Update()
    {
        pourcentageFait = 100 - (vieRestant * 100 / vieTotal);
        if (pourcentageFait > 100)
            pourcentageFait = 100;
    }
}
