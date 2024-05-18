using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject player;
    [SerializeField] GameObject boss;
    public bool afficherJoueur = true;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null) {
            Vector3 positionBoss = new Vector3(boss.transform.position.x, boss.transform.position.y, transform.position.z);
            Vector3 positionJoueur = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
            float disanceX = positionBoss.x - positionJoueur.x;
            float distanceY = positionBoss.y - positionJoueur.y;
            if (afficherJoueur)
                transform.position = new Vector3(positionJoueur.x, positionJoueur.y, -10);
            else
                transform.position = new Vector3(positionBoss.x, positionBoss.y, -10);
        }

    }
}
