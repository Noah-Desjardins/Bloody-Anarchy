using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;

public class Door : MonoBehaviour
{
    // Start is called before the first frame update.
    [SerializeField] string whereToGo = "Lobby";
    [SerializeField] GameManager gameManager;
    void Start()
    {
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("player"))
        {
            gameManager.ChangeRoom(whereToGo);
        }
    }
}
