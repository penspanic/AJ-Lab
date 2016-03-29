using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum PopupResult
{
    Yes,
    No
}

public class PopupViewer : MonoBehaviour
{

    static PopupViewer _instance;
    public static PopupViewer instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new GameObject("Popup Viewer").AddComponent<PopupViewer>();
            }
            return _instance;
        }
    }

    public bool isShowing
    {
        get;
        private set;
    }

    public PopupResult result
    {
        get;
        private set;
    }

    GameObject popupWindow;
    Text popupText;

    public void CheckInstance()
    {

    }

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        isShowing = false;

        popupWindow = Instantiate<GameObject>(Resources.Load<GameObject>("Prefab/UI/Popup Window"));
        popupWindow.transform.SetParent(this.transform);
        popupText = popupWindow.transform.FindChild("Popup Text").GetComponent<Text>();

        popupWindow.transform.FindChild("Yes Button").GetComponent<Button>().onClick.AddListener(OnYesButtonDown);
        popupWindow.transform.FindChild("No Button").GetComponent<Button>().onClick.AddListener(OnNoButtonDown);
    }

    void OnYesButtonDown()
    {
        result = PopupResult.Yes;
        isShowing = false;
        popupWindow.SetActive(false);
    }

    void OnNoButtonDown()
    {
        result = PopupResult.No;
        isShowing = false;
        popupWindow.SetActive(false);
    }

    void ResetParent()
    {
        transform.SetParent(GameObject.FindObjectOfType<Canvas>().transform, false);
    }

    public IEnumerator ShowPopup(string message)
    {
        if (isShowing)
            throw new UnityException("Already popup window enabled!");
        isShowing = true;

        ResetParent();
        popupWindow.SetActive(true);
        popupText.text = message;

        while(true)
        {
            if (!isShowing)
                break;
            yield return null;
        }
    }
}
