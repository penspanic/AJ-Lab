using UnityEngine;
using System.Collections;

public class DD_Game : InGameBase
{
    public bool gameOver
    {
        get;
        private set;
    }

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Update()
    {
        base.Update();

    }
    
    protected override void OnGameStart()
    {
        base.OnGameStart();

    }

    protected override void OnGameEnd()
    {

        base.OnGameEnd();
    }
}
