using UnityEngine;
using System.Collections;

public abstract class InGameBase : MonoBehaviour
{
    // If there's no next cut scene, variable will be null.
    public string nextCutScene;
    // If there's no next dialogue,           " "         .
    public string nextDialogue;


    protected virtual void Awake()
    {

    }

    protected virtual void Update()
    {

    }

    protected virtual void OnGameStart()
    {

    }

    protected virtual void OnGameEnd()
    {

    }
}
