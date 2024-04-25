using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OpenShop : MonoBehaviour
{
    [SerializeField] bool isInRange;
    [SerializeField] KeyCode interactKey;
    [SerializeField] UnityEvent interactAction;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isInRange)
        {
            if(Input.GetKeyDown(interactKey))
            {
                interactAction.Invoke();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            isInRange = true;
            print("player est dans la range du shop");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            isInRange = false;
            print("player n'est plus dans la range du shop");
        }
    }
}
