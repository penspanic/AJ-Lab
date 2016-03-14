using UnityEngine;
using System.Collections;

public class StageSelect : MonoBehaviour
{

    bool isChanging = false;
    void Awake()
    {
        SceneEffector.instance.CheckInstance();
        StartCoroutine(SceneEffector.instance.FadeIn(2f));
    }

    public void GameStart(int stage)
    {
        if (isChanging)
            return;
        isChanging = true;
        StartCoroutine(SceneEffector.instance.FadeOut(2f, StageManager.instance.GetSceneName(stage)));
    }
}
