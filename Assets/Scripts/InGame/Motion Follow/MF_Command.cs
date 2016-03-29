using UnityEngine;
using UnityEngine.EventSystems;

public enum CommandType : int
{
    Left = 0,
    Right = 1,
    Up = 2,
    Down = 3,
    Max = 4
}

public class MF_Command : MonoBehaviour, IPointerClickHandler
{

    void Awake()
    {

    }

    public void OnPointerClick(PointerEventData eventData)
    {

    }
}