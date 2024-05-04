using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zoneBorder : MonoBehaviour
{
    Camera cam;
    GameObject joueur;
    float vitesse = 0;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        joueur = GameObject.FindGameObjectWithTag("player");
    }

    // Update is called once per frame
    void Update()
    {
        float hauteur = 2f * cam.orthographicSize;
        float width = hauteur * cam.aspect;

        transform.localScale = new Vector3(width, transform.localScale.y, 1);
        transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y + hauteur / 2,0);
        cam.orthographicSize += vitesse;
        if ((transform.position.y - joueur.transform.position.y) > 2)
            vitesse = -0.05f;
        else
            vitesse = 0;
        if (cam.orthographicSize < 5)
        {
            vitesse = 0;
            cam.orthographicSize = 5f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        vitesse = 0.1f;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //vitesse = -0.1f;
        
    }

}
