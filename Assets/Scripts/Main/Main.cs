using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Main : MonoBehaviour
{
   
    Button stageButton;

    bool isChanging = false;

    void Awake()
    {
        SceneEffector.instance.CheckInstance();
        StartCoroutine(SceneEffector.instance.FadeIn(1f));

        DialogViewer.instance.CheckInstance();
        DialogViewer.instance.ShowDialogue("First");

        stageButton = GameObject.Find("Stage Button").GetComponent<Button>();
        stageButton.onClick.AddListener(OnStageButtonDown);

        //EventManager.PushEvent(new EventData(EventType.Dialog, "First"));
        //EventManager.PushEvent(new EventData(EventType.Dialog, "Second"));
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
