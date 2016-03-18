using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public enum RoadType
{
    Straight,   // |
    Curved,     // ㄱ
    Crossed     // +
}

public enum RoadDirection
{
    Left,
    Right,
    Top,
    Bottom
}

[ExecuteInEditMode]
public class Road : MonoBehaviour, IPointerClickHandler
{
    public RoadType type;

    static Sprite straightPipeSprite;
    static Sprite curvedPipeSprite;
    static Sprite crossedPipeSprite;


    RoadManager roadMgr;
    SpriteRenderer sprRenderer;

    bool isRotating = false;

    void Awake()
    {

        if(straightPipeSprite == null)
        {
            straightPipeSprite = Resources.Load<Sprite>("Sprite/InGame/Plumber/Pipe_Straight");
            curvedPipeSprite = Resources.Load<Sprite>("Sprite/InGame/Plumber/Pipe_Curved");
            crossedPipeSprite = Resources.Load<Sprite>("Sprite/InGame/Plumber/Pipe_Crossed");
        }
        sprRenderer = GetComponent<SpriteRenderer>();
        roadMgr = GameObject.FindObjectOfType<RoadManager>();

        SpriteSet();
    }

    void Update()
    {

    }

    void SpriteSet()
    {
        Sprite targetSprite = null;
        
        switch(type)
        {
            case RoadType.Straight:
                targetSprite = straightPipeSprite;
                break;
            case RoadType.Curved:
                targetSprite = curvedPipeSprite;
                break;
            case RoadType.Crossed:
                targetSprite = crossedPipeSprite;
                break;
        }
        sprRenderer.sprite = targetSprite;
    }

    IEnumerator Rotate()
    {
        isRotating = true;

        float elapsedTime = 0f;
        float rotateTime = 0.3f;

        Vector3 startRotation = transform.rotation.eulerAngles;
        Vector3 endRotation = startRotation;
        endRotation.z -= 90;

        while(elapsedTime< rotateTime)
        {
            elapsedTime += Time.deltaTime;

            transform.rotation = Quaternion.Euler(EasingUtil.EaseVector3(EasingUtil.easeOutQuad,
                startRotation, endRotation, elapsedTime / rotateTime));
            yield return null;
        }
        transform.rotation = Quaternion.Euler(endRotation);
        isRotating = false;
        RoadDirection[] roadDirection = GetDirection();
        string s = "";
        foreach(RoadDirection eachDir in roadDirection)
        {
            s += eachDir.ToString() + " ";
        }
        roadMgr.CheckClear();

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isRotating)
            return;
        StartCoroutine(Rotate());
        
    }

    static RoadDirection[] GetDirection(RoadType type, Vector3 rotation)
    {
        int zRotation = Mathf.RoundToInt(rotation.z);
        switch(type)
        {
            case RoadType.Straight:
                if(zRotation == 90 || zRotation == 270)
                    return new RoadDirection[] { RoadDirection.Left, RoadDirection.Right };
                else
                    return new RoadDirection[] { RoadDirection.Top, RoadDirection.Bottom };
            case RoadType.Curved:
                if (zRotation == 0 || zRotation == 360)
                    return new RoadDirection[] { RoadDirection.Top, RoadDirection.Right };
                else if(zRotation == 90)
                    return new RoadDirection[] { RoadDirection.Top, RoadDirection.Left };
                else if(zRotation == 180)
                    return new RoadDirection[] { RoadDirection.Bottom, RoadDirection.Left };
                else // 270
                    return new RoadDirection[] { RoadDirection.Bottom, RoadDirection.Right };
            case RoadType.Crossed:
                return new RoadDirection[] {RoadDirection.Top,RoadDirection.Bottom,
                    RoadDirection.Left,RoadDirection.Right };
        }
        return null;
    }

    //// TODO : type 에 따라 다르게 리턴(십자 모양때문에)
    //public RoadDirection GetOppositeDirection(RoadDirection dir)
    //{
    //    switch(dir)
    //    {

    //    }
    //}

    public RoadDirection[] GetDirection()
    {
        return GetDirection(type, transform.rotation.eulerAngles);
    }

    public bool HasDirection(RoadDirection dir)
    {
        RoadDirection[] dirs = GetDirection();

        for(int i = 0;i<dirs.Length;i++)
        {
            if (dirs[i] == dir)
                return true;
        }
        return false;
    }
}