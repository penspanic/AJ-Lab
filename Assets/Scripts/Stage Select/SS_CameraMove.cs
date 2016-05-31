using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SS_CameraMove : MonoBehaviour
{
    public int currFloor
    {
        get;
        private set;
    }
 
    bool isMoving = false;
    bool canScroll = true;

    bool prevMouseDown = false;

    bool floor1Stayed = false;
    bool floor7Stayed = false;

    Vector2 prevTouchPos = Vector2.zero;
    Vector2 currTouchPos = Vector2.zero;
    Vector2 deltaPos = Vector2.zero;

    SS_Player player;

    void Awake()
    {
        currFloor = 1;

        player = GameObject.FindObjectOfType<SS_Player>();

        Button upButton = GameObject.Find("Up Button").GetComponent<Button>();
        Button downButton = GameObject.Find("Down Button").GetComponent<Button>();

        upButton.onClick.AddListener(OnUpButtonDown);
        downButton.onClick.AddListener(OnDownButtonDown);
    }
    
    void Update()
    {
        currTouchPos = Input.mousePosition;

        deltaPos = prevTouchPos - currTouchPos;
        prevTouchPos = currTouchPos;

        if(Input.GetMouseButton(0) && !isMoving && IsBackgroundTouched() && prevMouseDown)
        {
            float moveValue = deltaPos.x * Time.deltaTime;

            Vector3 newPos = transform.position;
            newPos.x = Mathf.Clamp(newPos.x + moveValue, 0, GetMaxPosX(currFloor));
            transform.position = newPos;

            prevMouseDown = true;
        }
        if (Input.GetMouseButton(0))
            prevMouseDown = true;
        else
            prevMouseDown = false;
    }

    #region Event
    void OnUpButtonDown()
    {
        if (isMoving || player.floorChanging || currFloor == 5)
            return;
        StartCoroutine(FloorChange(currFloor + 1));
        currFloor++;
    }

    void OnDownButtonDown()
    {
        if (isMoving || player.floorChanging || currFloor == 1)
            return;
        StartCoroutine(FloorChange(currFloor - 1));
        currFloor--;
    }
    #endregion

    IEnumerator FloorChange(int floor) // Completed in 2 seconds.
    {
        isMoving = true;

        StartCoroutine(player.FloorChange(7.2f * (floor - 1)));

        float elapsedTime = 0f;
        float moveTime = 1f;

        Vector3 startPos = transform.position;
        Vector3 endPos = new Vector3(0, transform.position.y, -5);

        bool moveLeft = startPos.x > 0.1f;
        //Debug.Log(startPos.x);
        
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
        endPos = new Vector3(0, 7.2f * (floor - 1), -5);

        // Move Camera to up stair.
        while (elapsedTime < moveTime)
        {
            elapsedTime += Time.deltaTime;

            transform.position = EasingUtil.EaseVector3(EasingUtil.easeInQuad, startPos, endPos, elapsedTime / moveTime);
            yield return null;
        }
        transform.position = endPos;
        isMoving = false;

        ExcaliburGetCheck();
    }
    float GetMaxPosX(int floor)
    {
        return 6.4f * (floor - 1);
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

    void ExcaliburGetCheck()
    {
        if (currFloor == 1)
            floor1Stayed = true;
        else if (currFloor == 5)
            floor7Stayed = true;

        if(floor1Stayed && floor7Stayed)
        {
            ItemManager.instance.CheckInstance();
            ItemManager.instance.GetItem("Excalibur");
        }
    }
}