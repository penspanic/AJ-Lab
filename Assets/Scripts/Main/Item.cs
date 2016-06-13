using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System;

public class Item : MonoBehaviour, IPointerClickHandler
{
    public string itemName;
    public bool isHave;

    protected virtual void Awake()
    {
        ItemManager.instance.CheckInstance();
        PopupViewer.instance.CheckInstance();
        JsonManager.instance.CheckInstance();

        isHave = ItemManager.instance.HasItem(itemName);

        gameObject.SetActive(isHave);
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        if (!PopupViewer.instance.isShowing)
        {
            StartCoroutine(ClickProcess());
        }
    }

    protected virtual IEnumerator ClickProcess()
    {
        yield return StartCoroutine(PopupViewer.instance.ShowPopup(
            JsonManager.instance.GetObjectDescription(itemName)));

        bool activate = PopupViewer.instance.result == PopupResult.Yes ? true : false;

        ItemManager.instance.ItemActivate(itemName, activate);
    }
}