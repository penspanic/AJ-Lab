using UnityEngine;
using System.Collections;

/*
같은 분기점에 속해 있는 스테이지는 똑같음.
분기점을 지나면 미니게임이 변하게 되서 총 게임 씬은 21개(미니게임 7개 * 3)
*/

public abstract class InGameBase : MonoBehaviour
{
    // 미니게임의 이름
    public string gameName;
    // 게임 클리어 후 보여줄 컷씬
    public string nextCutScene;
    // 게임 클리어 후 출력할 대화
    public string nextDialogue;

    protected bool isChanging = false;

    protected virtual void Awake()
    {
        StartCoroutine(StartProcess());
    }

    IEnumerator StartProcess()
    {
        isChanging = true;
        yield return StartCoroutine(SceneEffector.instance.FadeIn(1f));
        if(StageManager.instance.showTutorial)
            yield return StartCoroutine(TutorialViewer.instance.ShowTutorial(gameName));
        isChanging = false;
        OnGameStart();
    }

    protected virtual void Update()
    {

    }

    protected virtual void OnGameStart()
    {

    }

    protected virtual void OnGameEnd() // 게임의 모든 처리가 끝난 후 다음 씬으로 넘어가야 할 때 호출
    {
        isChanging = true;
        StartCoroutine(SceneEffector.instance.FadeOut(1f));
    }
}