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
        StartCoroutine(SceneEffector.instance.FadeIn(2f));

        DialogueViewer.instance.CheckInstance();
        DialogueViewer.instance.ShowDialogue("First");

        stageButton = GameObject.Find("Stage Button").GetComponent<Button>();
        stageButton.onClick.AddListener(OnStageButtonDown);

        OutlineEffect outlineEffect = GameObject.FindObjectOfType<OutlineEffect>();
        outlineEffect.SetOutlineObject(a.GetComponent<Renderer>(), 2);
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
