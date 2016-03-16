using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public enum RoadType
{
    Straight,   // |
    Curved,     // ㄱ
    Crossed     // +
}

public enum Road
[ExecuteInEditMode]
public class Road : MonoBehaviour, IPointerClickHandler
{
    public RoadType type;

    static Sprite straightPipeSprite;
    static Sprite curvedPipeSprite;
    static Sprite crossedPipeSprite;

    RoadManager roadMgr;
    SpriteRenderer sprRenderer;

    void Awake()
    {

        if(straightPipeSprite == null)
        {
            straightPipeSprite = Resources.Load<Sprite>("Sprite/InGame/Plumber/Pipe_Straight");
            curvedPipeSprite = Resources.Load<Sprite>("Sprite/InGame/Plumber/Pipe_Curved");
            crossedPipeSprite = Resources.Load<Sprite>("Sprite/InGame/Plumber/Pipe_Crossed");
        }
        sprRenderer = GetComponent<SpriteRenderer>();

        SpriteSet();
    }

    void Update()
    {

    }

    public void FieldChanged()
    {
        SpriteSet();
    }

    public bool CheckOpened(

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

    void Rotate()
    {
        transform.Rotate(new Vector3(0, 0, -90));
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Rotate();
    }
}