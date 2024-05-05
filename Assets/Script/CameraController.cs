using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject boss;
    Camera camera;
    public bool afficherJoueur = true;
    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 positionBoss = new Vector3(boss.transform.position.x, boss.transform.position.y, transform.position.z);
        Vector3 positionJoueur = new Vector3 (player.transform.position.x, player.transform.position.y, transform.position.z);
        float disanceX = positionBoss.x - positionJoueur.x;
        float distanceY = positionBoss.y - positionJoueur.y;
        if(afficherJoueur)
            transform.position = new Vector3(positionJoueur.x,positionJoueur.y,-10);
        else
            transform.position = new Vector3(positionBoss.x, positionBoss.y, -10);
        //transform.position = new Vector3((positionBoss.x + positionJoueur.x)/2, (positionBoss.y + positionJoueur.y)/ 2, transform.position.z);
        //camera.orthographicSize = disanceX / 3.2f + distanceY / 1.6f;
        //if (camera.orthographicSize < 5)
        //{
        //    camera.orthographicSize = 5f;
        //}
    }
}
