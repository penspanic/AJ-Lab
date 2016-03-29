using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StageManager : MonoBehaviour
{
    static StageManager _instance;
    public static StageManager instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new GameObject("Stage Manager").AddComponent<StageManager>();
            }
            return _instance;
        }
    }

    public bool showTutorial;

    bool[] stageCleared = new bool[77];

    public void CheckInstance()
    {

    }

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        stageCleared[1] = true;
    }

    public bool StageCleared(int stage)
    {
        return stageCleared[stage - 1];
    }

    public string GetSceneName(int stage)
    {
        return "Search Road";
    }
}

/*

    11111111111111111
    111111111111111
    1111111111111
    11111111111
    111111111
    1111111
    11111

*/