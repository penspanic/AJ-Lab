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
        StartCoroutine(SceneEffector.instance.FadeIn(2f));

        DialogueViewer.instance.CheckInstance();
        DialogueViewer.instance.ShowDialogue("First");

        stageButton = GameObject.Find("Stage Button").GetComponent<Button>();
        stageButton.onClick.AddListener(OnStageButtonDown);

    }

    void OnStageButtonDown()
    {
        if (isChanging)
            return;
        isChanging = true;
        
        SceneEffector.instance.CheckInstance();
        StartCoroutine(SceneEffector.instance.FadeOut(2f, "Stage Select"));
    }
}
