using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitterShopBtn : MonoBehaviour
{
    [SerializeField] GameObject Shop;
    private void Start()
    {
    }
    public void QuitterShop()
    {
        Shop.SetActive(false);
    }
}
