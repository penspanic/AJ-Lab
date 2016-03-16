using UnityEngine;
using System.Collections;

public class TestGame : InGameBase
{

    Vector2 startPos;
    Vector2 endPos;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Update()
    {
        base.Update();
        if(Input.GetMouseButtonDown(0))
        {
            startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        else if(Input.GetMouseButtonUp(0))
        {
            endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    protected override void OnGameStart()
    {
        base.OnGameStart();
        //
    }

    protected override void OnGameEnd()
    {
        //
        base.OnGameEnd();
    }
}