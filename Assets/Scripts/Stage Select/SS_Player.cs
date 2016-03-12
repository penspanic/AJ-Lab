using UnityEngine;
using System.Collections;

public class SS_Player : MonoBehaviour
{
    SS_CameraMove cameraMove;

    float moveSpeed = 2f;
    public bool floorChanging
    {
        get;
        private set;
    }

    void Awake()
    {
        cameraMove = GameObject.FindObjectOfType<SS_CameraMove>();
    }

    void Update()
    {
        if (Mathf.Abs(cameraMove.transform.position.x - transform.position.x) < 0.3f)
            return;
        if (floorChanging)
            return;

        bool moveLeft = cameraMove.transform.position.x - transform.position.x < 0;

        transform.Translate(Vector2.left * (moveLeft ? 1 : -1) * Time.deltaTime * moveSpeed);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other);
        if(other.CompareTag("MainCamera"))
        {
            moveSpeed = 2f;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log(other);
        if(other.CompareTag("MainCamera"))
        {
            moveSpeed = 8f;
        }
    }
    
    public IEnumerator FloorChange(float floorY)
    {
        floorChanging = true;

        float elapsedTime = 0f;
        float moveTime = 1f;

        Vector3 startPos = transform.position;
        Vector3 endPos = startPos;
        endPos.x = -5f;

        while(elapsedTime < moveTime)
        {
            elapsedTime += Time.deltaTime;

            transform.position = EasingUtil.EaseVector3(EasingUtil.linear, startPos, endPos, elapsedTime / moveTime);
            yield return null;
        }

        startPos = transform.position;
        endPos.y = floorY;
        elapsedTime = 0f;
        // Move Character Y to next floor.
        while (elapsedTime < moveTime) 
        {
            elapsedTime += Time.deltaTime;

            transform.position = EasingUtil.EaseVector3(EasingUtil.linear, startPos, endPos, elapsedTime / moveTime);
            yield return null;
        }

        floorChanging = false;
    }
}
