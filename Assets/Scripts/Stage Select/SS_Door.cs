using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class SS_Door : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool cleared
    {
        get;
        private set;
    }

    public int stage;
    public bool showTutorial;

    StageSelect stageSelect;
    SpriteRenderer markRenderer;

    bool scaleChanging = false;

    void Awake()
    {
        stageSelect = GameObject.FindObjectOfType<StageSelect>();
        markRenderer = transform.FindChild("Mark").GetComponent<SpriteRenderer>();

        StageManager.instance.CheckInstance();

        if(StageManager.instance.StageCleared(stage))
        {
            cleared = true;
            markRenderer.enabled = false;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!scaleChanging)
            StartCoroutine(BigAndSmall(0.5f));
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        stageSelect.GameStart(stage, showTutorial);
    }

    IEnumerator BigAndSmall(float time)
    {
        float elapsedTime = 0f;

        Vector3 originalSize = transform.localScale;
        scaleChanging = true;
        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;

            transform.localScale = originalSize + 
                (originalSize * Mathf.Sin(elapsedTime * 20)) / (elapsedTime * 5 + 5);

            yield return null;
        }
        transform.localScale = originalSize;
        scaleChanging = false;
    }
}
