using UnityEngine;
using System.Collections.Generic;


public class ItemManager : MonoBehaviour
{
    static ItemManager _instance;
    public static ItemManager instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new GameObject("Item Manager").AddComponent<ItemManager>();
            }
            return _instance;
        }
    }

    void Awake()
    {

    }

    public bool HasItem(string itemName)
    {
        throw new System.NotImplementedException();
    }

    public void GetItem(string itemName)
    {

    }
}
