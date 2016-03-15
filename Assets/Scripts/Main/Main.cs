using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Main : MonoBehaviour
{
    public GameObject a;

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

        EventManager.PushEvent(new EventData(EventType.Message, "First Message"));
        EventManager.PushEvent(new EventData(EventType.Message, "Second Message"));
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
