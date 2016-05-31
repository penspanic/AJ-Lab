using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System;

public class Main_Door : MonoBehaviour, IPointerClickHandler
{
    bool isChanging;

    public void OnPointerClick(PointerEventData eventData)
    {
        OnStageButtonDown();
    }

    void OnStageButtonDown()
    {
        if (isChanging)
            return;
        isChanging = true;

        SceneEffector.instance.CheckInstance();
        StartCoroutine(SceneEffector.instance.FadeOut(1f, "Stage Select"));
    }
}
