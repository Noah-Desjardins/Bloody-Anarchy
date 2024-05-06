using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    List<GameObject> pool = new List<GameObject>();

    [SerializeField] GameObject[] objetsAPool;
    [SerializeField] int[] nombreAPool;

    public static ObjectPool instance;

    private void Awake()
    {
        if(instance == null)
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < Mathf.Min(objetsAPool.Length, nombreAPool.Length); i++)
        {
            for (int j = 0;j < nombreAPool[i]; j++)
            {
                GameObject obj = Instantiate(objetsAPool[i]);
                obj.name = objetsAPool[i].name;
                obj.SetActive(false);
                pool.Add(obj);
            }
        }
    }

    public GameObject GetPoolObject(GameObject typeObjet)
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].activeInHierarchy && typeObjet.name == pool[i].name)
                return pool[i];
        }

        return null;
    }
}
