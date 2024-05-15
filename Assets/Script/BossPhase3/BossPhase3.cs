using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BossPhase3 : MonoBehaviour
{
    [SerializeField] Player player;
    float followSharpness = 0.005f;
    Vector3 _followOffset;
    // Start is called before the first frame update
    void Start()
    {
        _followOffset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = player.transform.position + _followOffset;
        transform.position += (targetPosition - transform.position) * followSharpness;
    }
}
