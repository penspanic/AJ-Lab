using UnityEngine;
using System.Collections.Generic;


public class ItemManager : MonoBehaviour
{
    static ItemManager _instance;
    public static ItemManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject("Item Manager").AddComponent<ItemManager>();
            }
            return _instance;
        }
    }

    List<string> ownItemList = new List<string>();
    Dictionary<string, bool> ownItemDictionary = new Dictionary<string, bool>();
    public void CheckInstance()
    {
        
    }

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        //GetAllItem();
    }

    void GetAllItem()
    {
        GetItem("Excalibur");
        GetItem("Snowball");
        GetItem("Sunglasses");
        GetItem("Mini Ballon");
        GetItem("Hourglass");
        GetItem("Fly Swatter");
        
        // System items
        GetItem("Books");
        GetItem("Award");
        GetItem("Phonograph");
    }

    public bool HasItem(string itemName)
    {
        bool isHave = PlayerPrefs.HasKey(itemName) || ownItemDictionary.ContainsKey(itemName);
        if (isHave)
        {
            bool activated = PlayerPrefs.GetInt(itemName) == 1 ? true : false;
            if (!ownItemDictionary.ContainsKey(itemName))
                ownItemDictionary.Add(itemName, activated);
        }
        return isHave;
    }

    public bool IsItemActivated(string itemName)
    {
        if(HasItem(itemName))
        {
            return ownItemDictionary[itemName];
        }
        return false;
    }

    public void ItemActivate(string itemName, bool value)
    {
        if (!ownItemDictionary.ContainsKey(itemName))
            throw new UnityException("Don't have " + itemName + "!");
        ownItemDictionary[itemName] = value;
    }

    public void GetItem(string itemName) //  HasItem 메서드로 보유중인지 확인해야 함, 없을 때만 호출
    {
        if (!ownItemDictionary.ContainsKey(itemName))
            ownItemDictionary.Add(itemName, false);
    }

    void OnApplicationQuit()
    {
        foreach(string eachItem in ownItemDictionary.Keys)
        {
            PlayerPrefs.SetInt(eachItem, ownItemDictionary[eachItem] == true ? 1 : 0);
        }
    }
}
