using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Phonograph : Item
{

    public static bool bgmOn
    {
        get;
        private set;
    }

    SpriteRenderer sprRenderer;

    protected override void Awake()
    {
        ItemManager.instance.CheckInstance();

        sprRenderer = GetComponent<SpriteRenderer>();
        isHave = ItemManager.instance.HasItem(itemName);
        sprRenderer.enabled = isHave;
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (!isHave)
        {
            ItemManager.instance.GetItem(itemName);
            sprRenderer.enabled = true;
        }
        else
        {
            // 음악 재생 애니메이션 끄기 , 켜기

        }
    }
}