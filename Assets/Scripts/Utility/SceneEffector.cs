using UnityEngine;
using System.Collections;

/// <summary>
/// This class handles many effects in game scene.
/// </summary>
public class SceneEffector : MonoBehaviour
{
    static SceneEffector _instance;
    public static SceneEffector instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new GameObject("SceneEffector").AddComponent<SceneEffector>();
                _instance.fadeObject = Instantiate(Resources.Load<GameObject>("Prefab/UI/Fade"));
                _instance.fadeObject.transform.SetParent(_instance.transform, false);

                _instance.sprRenderer = _instance.fadeObject.GetComponent<SpriteRenderer>();
                _instance.collider = _instance.fadeObject.GetComponent<BoxCollider2D>();
            }
            return _instance;
        }
    }
    GameObject fadeObject;
    SpriteRenderer sprRenderer;
    new BoxCollider2D collider;
    Sprite black;
    Sprite white;

    public void CheckInstance()
    {
        
    }

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        black = Resources.Load<Sprite>("Sprite/UI/Black");
        white = Resources.Load<Sprite>("Sprite/UI/White");
    }

    public IEnumerator FadeOut(float duration, string nextScene = null)
    {
        float fadeAlpha = 0;

        sprRenderer.enabled = true;
        sprRenderer.sprite = black;
        collider.enabled = true;

        float elapsedTIme = 0f;
        
        while(elapsedTIme < duration)
        {
            elapsedTIme += Time.unscaledDeltaTime;
            fadeAlpha = elapsedTIme / duration;
            sprRenderer.color = new Color(0, 0, 0, fadeAlpha);

            yield return null;
        }

        sprRenderer.color = new Color(0, 0, 0, 1);
        //sprRenderer.enabled = false;

        if (nextScene != null)
            Application.LoadLevel(nextScene);
    }


    public IEnumerator FadeIn(float duration, string nextScene = null)
    {
        float fadeAlpha = 1;

        sprRenderer.enabled = true;
        sprRenderer.sprite = black;
        collider.enabled = true;

        float elapsedTime = 0f;

        while(elapsedTime < duration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            fadeAlpha = 1 - (elapsedTime / duration);
            sprRenderer.color = new Color(0, 0, 0, fadeAlpha);

            yield return null;
        }

        sprRenderer.enabled = false;
        collider.enabled = false;
        if (nextScene != null)
            Application.LoadLevel(nextScene);
    }

    public IEnumerator CameraShake(float duration, float period, float value)
    {
        float elapsedTime = 0f;

        Vector3 originalPos = Camera.main.transform.position;
        Vector3 additionalPos;

        while (elapsedTime < duration)
        {
            elapsedTime += period;

            additionalPos = Random.insideUnitCircle * value * (1 - (elapsedTime / duration));
            Camera.main.transform.position = originalPos + additionalPos;

            yield return new WaitForSeconds(period);
        }

        Camera.main.transform.position = originalPos;
    }

    public void StopFading()
    {
        StopCoroutine(FadeIn(1f));
        StopCoroutine(FadeOut(1f));
    }
}
