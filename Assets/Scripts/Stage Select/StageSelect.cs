using UnityEngine;
using System.Collections;

public class StageSelect : MonoBehaviour
{

    bool isChanging = false;
    void Awake()
    {
        SceneEffector.instance.CheckInstance();
        StartCoroutine(SceneEffector.instance.FadeIn(1f));
    }

    public void GameStart(int stage, bool showTutorial)
    {
        if (isChanging)
            return;
        isChanging = true;
        StageManager.instance.showTutorial = showTutorial;
        StageManager.instance.currStage = stage;
        StartCoroutine(SceneEffector.instance.FadeOut(1f, StageManager.instance.GetSceneName(stage)));
    }
}