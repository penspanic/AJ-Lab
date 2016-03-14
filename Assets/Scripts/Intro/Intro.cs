using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Intro : MonoBehaviour
{
    public Sprite[] introSprite;
    public Image cutSceneImage;

    const float introCutSceneInterval = 2f;

    Coroutine introRoutine;

    void Awake()
    {

        SceneEffector.instance.CheckInstance();
        StartCoroutine(SceneEffector.instance.FadeIn(2f));

        introRoutine = StartCoroutine(IntroProcess());

        EventManager.PushEvent(new EventData(EventType.Dialog, "First"));
    }

    IEnumerator IntroProcess()
    {
        yield return new WaitForSeconds(2f);
        cutSceneImage.gameObject.SetActive(true);

        yield return StartCoroutine(SceneEffector.instance.FadeOut(1f));

        for(int i = 0;i<introSprite.Length;i++)
        {
            StartCoroutine(SceneEffector.instance.FadeIn(1f));
            cutSceneImage.sprite = introSprite[i];

            yield return new WaitForSeconds(2f);
            
            yield return StartCoroutine(SceneEffector.instance.FadeOut(1f));
        }
        Application.LoadLevel("Main");
    }
}
