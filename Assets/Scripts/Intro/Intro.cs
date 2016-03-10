using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Intro : MonoBehaviour
{
    public Sprite[] introSprite;
    public Image cutSceneImage;

    const float introCutSceneInterval = 2f;

    void Awake()
    {
        Button skipButton = GameObject.Find("Skip Button").GetComponent<Button>();
        skipButton.onClick.AddListener(OnSkipButtonDown);

        SceneEffector.instance.CheckInstance();
        StartCoroutine(SceneEffector.instance.FadeIn(3f));

        StartCoroutine(IntroProcess());
    }

    IEnumerator IntroProcess()
    {
        yield return new WaitForSeconds(3f);
        cutSceneImage.gameObject.SetActive(true);

        for(int i = 0;i<introSprite.Length;i++)
        {
            StartCoroutine(SceneEffector.instance.FadeIn(1f));
            cutSceneImage.sprite = introSprite[i];

            yield return new WaitForSeconds(2f);
            
            StartCoroutine(SceneEffector.instance.FadeOut(1f));

            yield return new WaitForSeconds(1f);
        }

    }

    void OnSkipButtonDown()
    {
        StopCoroutine("IntroProcess");
        StartCoroutine(SceneEffector.instance.FadeOut(3f, "Main"));
    }
}
