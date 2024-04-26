using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = new Vector3 (player.transform.position.x, player.transform.position.y, transform.position.z);
        transform.position = position;
    }
}
