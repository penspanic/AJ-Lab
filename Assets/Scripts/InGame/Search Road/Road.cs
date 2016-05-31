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

    static Sprite straightRoadSprite;
    static Sprite curvedRoadSprite;


    RoadManager roadMgr;
    SpriteRenderer sprRenderer;
    SR_Player player;

    public bool isRotating
    {
        get;
        private set;
    }

    void Awake()
    {

        if(straightRoadSprite == null)
        {
            straightRoadSprite = Resources.Load<Sprite>("Sprite/InGame/Search Road/Road_Straight");
            curvedRoadSprite = Resources.Load<Sprite>("Sprite/InGame/Search Road/Road_Curved");
        }
        sprRenderer = GetComponent<SpriteRenderer>();
        roadMgr = GameObject.FindObjectOfType<RoadManager>();
        player = GameObject.FindObjectOfType<SR_Player>();

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
                targetSprite = straightRoadSprite;
                break;
            case RoadType.Curved:
                targetSprite = curvedRoadSprite;
                break;
        }
        sprRenderer.sprite = targetSprite;
    }
    
    public void Rotate()
    {
        if (isRotating || player.isMoving)
            return;
        StartCoroutine(RotateProcess());
    }

    IEnumerator RotateProcess()
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
        Rotate();   
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