using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SR_Player : MonoBehaviour
{
    public bool isMoving
    { get; private set; }

    Animator animator;
    InGameBase ingame;
    void Awake()
    {
        animator = GetComponent<Animator>();
        animator.enabled = false;

        ingame = GameObject.FindObjectOfType<InGameBase>();

    }

    public void Move(List<RoadDirection> answerList)
    {
        animator.enabled = true;
        isMoving = true;
        StartCoroutine(MoveProcess(answerList));
    }

    IEnumerator MoveProcess(List<RoadDirection> answerList)
    {
        float elaspedTime = 0f;
        float moveTime = 0.33f;
        Vector3 startPos;
        Vector3 endPos;

        foreach (RoadDirection currDirection in answerList)
        {
            if(currDirection == RoadDirection.Left || currDirection == RoadDirection.Right)
            {
                transform.rotation = Quaternion.Euler(0, currDirection == RoadDirection.Right ? 0 : 180, 0);
            }

            elaspedTime = 0f;
            startPos = transform.position;
            endPos = startPos + GetDirectionVec(currDirection);
            while(elaspedTime < moveTime)
            {
                elaspedTime += Time.deltaTime;
                transform.position = EasingUtil.EaseVector3(EasingUtil.linear, startPos, endPos, elaspedTime / moveTime);
                yield return null;
            }
            transform.position = endPos;
        }
        animator.enabled = false;
        ingame.GameEnd();
    }

    Vector3 GetDirectionVec(RoadDirection dir)
    {
        switch(dir)
        {
            case RoadDirection.Bottom:
                return Vector3.down;
            case RoadDirection.Left:
                return Vector3.left;
            case RoadDirection.Top:
                return Vector3.up;
            case RoadDirection.Right:
                return Vector3.right;
        }
        throw new System.ArgumentException();
    }
}