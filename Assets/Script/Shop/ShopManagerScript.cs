using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Windows;


public class ShopManagerScript : MonoBehaviour
{
    [SerializeField] int[] pris;
    public int[,] shopItem ;
    int pourcentage;
    [SerializeField] TextMeshProUGUI PourcentageTxt;
    [SerializeField] GameObject Shop;

    void Start()
    {
        shopItem = new int[3, pris.Length + 1];


        for (int i = 1; i < shopItem.GetLength(1); i++)
        {
            //mettre les id
            shopItem[1, i] = i;

            //mettre les prix
            shopItem[2, i] = pris[i - 1];
        }

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
        if (PlayerPrefs.HasKey("score"))
        pourcentage = PlayerPrefs.GetInt("score");
        print(pourcentage);
        PourcentageTxt.text = "Vous avez fait " + pourcentage + "% du boss";
    }
    public void AfficherShop()
    {
        Shop.SetActive(true);
    }

    // Update is called once per frame
    public void Acheter()
    {
        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;

        if (pourcentage >= shopItem[2, ButtonRef.GetComponent<ItemInfo>().ItemID])
        {
            ButtonRef.GetComponent<ItemInfo>().Acheter();
        }
    }

   
}
