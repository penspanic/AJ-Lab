using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

/// <summary>
/// This class provides object's outling when object is touched.
/// </summary>
public class FocusOutline : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    static OutlineEffect outlineEffect;

    public int colorID;
    new Renderer renderer;

    void Awake()
    {
        if(outlineEffect == null)
        {
            outlineEffect = GameObject.FindObjectOfType<OutlineEffect>();
        }
        renderer = GetComponent<Renderer>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        outlineEffect.SetOutlineObject(renderer, colorID);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        outlineEffect.RemoveOutlineObject(renderer);
    }
}