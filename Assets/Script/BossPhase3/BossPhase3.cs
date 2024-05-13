using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BossPhase3 : MonoBehaviour
{
    List<BossPhase3Hand> hands = new ();
    // Start is called before the first frame update
    void Start()
    {
        hands = GetComponentsInChildren<BossPhase3Hand>().ToList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
