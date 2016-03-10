using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SS_CameraMove : MonoBehaviour
{
    int currFloor = 1;
    bool isMoving = false;
    bool canScroll = true;

    Vector2 prevTouchPos = Vector2.zero;
    Vector2 currTouchPos = Vector2.zero;
    Vector2 deltaPos = Vector2.zero;

    void Awake()
    {
        Button upButton = GameObject.Find("Up Button").GetComponent<Button>();
        Button downButton = GameObject.Find("Down Button").GetComponent<Button>();

        upButton.onClick.AddListener(OnUpButtonDown);
        downButton.onClick.AddListener(OnDownButtonDown);
    }
    
    void LateUpdate()
    {
        currTouchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        deltaPos = prevTouchPos - currTouchPos;
        prevTouchPos = currTouchPos;

        if(Input.GetMouseButton(0) && !isMoving && IsBackgroundTouched())
        {
            float moveValue = deltaPos.x;

            Vector3 newPos = transform.position;
            newPos.x = Mathf.Clamp(newPos.x + moveValue, 0, GetMaxPosX(currFloor));
            transform.position = newPos;
        }
    }

    #region Event
    void OnUpButtonDown()
    {
        if (isMoving || currFloor == 5)
            return;
        StartCoroutine(FloorChange(currFloor + 1));
        currFloor++;
    }

    void OnDownButtonDown()
    {
        if (isMoving || currFloor == 1)
            return;
        StartCoroutine(FloorChange(currFloor - 1));
        currFloor--;
    }
    #endregion

    IEnumerator FloorChange(int floor)
    {
        isMoving = true;

        float elapsedTime = 0f;
        float moveTime = 1f;

        Vector3 startPos = transform.position;
        Vector3 endPos = new Vector3(0, transform.position.y, -10);

        bool moveLeft = startPos.x > 0.1f;
        Debug.Log(startPos.x);

        if (moveLeft)
        {
            while (elapsedTime < moveTime)
            {
                elapsedTime += Time.deltaTime;

                transform.position = EasingUtil.EaseVector3(EasingUtil.easeInOutBack, startPos, endPos, elapsedTime / moveTime);
                yield return null;
            }
        }

        elapsedTime = 0f;
        moveTime = 1f;

        startPos = transform.position;
        endPos = new Vector3(0, 7.2f * (floor - 1),-10);

        // Move Camera to up stair.
        while(elapsedTime < moveTime)
        {
            elapsedTime += Time.deltaTime;

            transform.position = EasingUtil.EaseVector3(EasingUtil.easeInOutBack, startPos, endPos, elapsedTime / moveTime);
            yield return null;
        }
        isMoving = false;
    }

    int GetMaxPosX(int floor)
    {
        return 10 * (floor - 1);
    }

    bool IsBackgroundTouched()
    {
        RaycastHit2D[] hitInfo = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        for (int i = 0; i < hitInfo.Length;i++)
        {
            if (hitInfo[i].collider != null)
            {
                if (hitInfo[i].collider.CompareTag("Floor"))
                {
                    return true;
                }
            }
        }
        return false;
    }
}