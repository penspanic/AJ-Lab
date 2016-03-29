using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TutorialViewer : MonoBehaviour
{

    static TutorialViewer _instance;
    public static TutorialViewer instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject("Tutorial Viewer").AddComponent<TutorialViewer>();
            }
            return _instance;
        }
    }

    GameObject tutorialWindow;
    Image tutorialImage;
    Animator windowAnimator;
    bool isChanging = false;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        tutorialWindow = Instantiate(Resources.Load<GameObject>("Prefab/UI/Tutorial Window"));
        tutorialWindow.transform.SetParent(transform);

        tutorialImage = tutorialWindow.transform.FindChild("Tutorial Image").GetComponent<Image>();
        windowAnimator = tutorialWindow.GetComponent<Animator>();

        tutorialWindow.GetComponent<Button>().onClick.AddListener(CloseTutorial);
    }

    void ResetParent()
    {
        transform.SetParent(GameObject.FindObjectOfType<Canvas>().transform, false);
    }

    void CloseTutorial()
    {
        if (isChanging)
            return;
        isChanging = true;

        // 애니메이터 처리
        windowAnimator.Play("Disappear");
    }

    public IEnumerator ShowTutorial(string gameName)
    {
        isChanging = true;

        Sprite tutorialSprite = Resources.Load<Sprite>("Sprite/InGame/UI/Tutorial/" + gameName);
        tutorialImage.sprite = tutorialSprite;

        ResetParent();
        windowAnimator.Play("Show");

        yield return new WaitForSeconds(1f);
        isChanging = false;
    }
}
