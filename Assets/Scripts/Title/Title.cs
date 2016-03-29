using UnityEngine;
using System.Collections;

public class Title : MonoBehaviour
{
    
    void Awake()
    {
        SceneEffector.instance.CheckInstance();
        ItemManager.instance.CheckInstance();

        PlayerPrefs.SetInt("GameRunCount", PlayerPrefs.GetInt("GameRunCount") + 1);

        if (PlayerPrefs.GetInt("GameRunCount") == 3)
            ItemManager.instance.GetItem("Credit");

        Debug.Log(PlayerPrefs.GetInt("GameRunCount"));
        StartCoroutine(SceneChangeProcess());
    }

    IEnumerator SceneChangeProcess()
    {
        yield return StartCoroutine(SceneEffector.instance.FadeIn(1f));

        yield return new WaitForSeconds(2f);

        StartCoroutine(SceneEffector.instance.FadeOut(1f, "Main"));
    }
}
