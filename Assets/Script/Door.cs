using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;

public class Door : MonoBehaviour
{
    // Start is called before the first frame update.
    [SerializeField] string whereToGo = "Lobby";
    GameManager gameManager;
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("player"))
        {
            gameManager.ChangeRoom(whereToGo);
        }
    }
}
