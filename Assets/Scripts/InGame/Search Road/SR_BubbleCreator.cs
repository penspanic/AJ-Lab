using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class SR_BubbleCreator : MonoBehaviour
{
    public GameObject bubblePrefab;
    public float createInterval;

    public Vector2 leftTop;
    public Vector2 rightBottom;

    List<GameObject> bubbleList = new List<GameObject>();

    void Awake()
    {
        StartCoroutine(CreateProcess());
    }

    IEnumerator CreateProcess()
    {
        GameObject newBubble = null;
        while(true)
        {
            newBubble = Instantiate<GameObject>(bubblePrefab);
            newBubble.transform.position = new Vector3(
                Random.Range(leftTop.x, rightBottom.x),
                Random.Range(rightBottom.y, leftTop.y),
                1);
            bubbleList.Add(newBubble);
            yield return new WaitForSeconds(createInterval);
        }
    }
}
