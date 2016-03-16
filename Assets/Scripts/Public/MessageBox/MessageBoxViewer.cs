using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MessageBoxViewer : MonoBehaviour
{

    static MessageBoxViewer _instance;
    public static MessageBoxViewer instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new GameObject("Message Box Viewer").AddComponent<MessageBoxViewer>();
            }
            return _instance;
        }
    } 

    public bool isShowing
    {
        get;
        private set;
    }

    GameObject messageBox;
    Text messageText;

    void Awake()
    {
        isShowing = false;


        DontDestroyOnLoad(this.gameObject);

        messageBox = Instantiate<GameObject>(Resources.Load<GameObject>("Prefab/UI/Message Box"));
        messageBox.transform.SetParent(this.transform);
        messageText = messageBox.transform.FindChild("Message Text").GetComponent<Text>();

        messageBox.GetComponent<Button>().onClick.AddListener(CloseMessageBox);

    }

    void ResetParent()
    {
        transform.SetParent(GameObject.FindObjectOfType<Canvas>().transform, false);
    }
    
    void CloseMessageBox()
    {
        isShowing = false;
        messageBox.SetActive(false);
    }

    public IEnumerator ShowMessage(string message)
    {
        if (isShowing)
            throw new UnityException("Already showing message!");
        isShowing = true;

        messageBox.SetActive(true);
        ResetParent();
        messageText.text = message;

        while(true)
        {
            if (!isShowing)
            {
                break;
            }
            yield return null;
        }
    }
}