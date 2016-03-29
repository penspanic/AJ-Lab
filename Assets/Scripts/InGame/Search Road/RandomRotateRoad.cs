using UnityEngine;
using System.Collections;

public class RandomRotateRoad : MonoBehaviour
{

    public bool rotateRoad;
    public float rotateInterval;

    RoadMap map;

    float elapsedTime = 0f;
    void Awake()
    {
        map = GameObject.FindObjectOfType<RoadMap>();
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        if(elapsedTime > rotateInterval)
        {
            elapsedTime = 0f;
            RotateRoad();
        }
    }

    void RotateRoad()
    {
        map.GetStoppedRoad().Rotate();
    }
}
