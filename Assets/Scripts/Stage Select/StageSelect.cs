using UnityEngine;
using System.Collections;

public class StageSelect : MonoBehaviour
{

    void Awake()
    {
        SceneEffector.instance.CheckInstance();
        StartCoroutine(SceneEffector.instance.FadeIn(2f));
    }
}
