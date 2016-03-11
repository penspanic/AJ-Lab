using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DialogueViewer : MonoBehaviour
{
    static DialogueViewer _instance;
    public static DialogueViewer instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = Instantiate(Resources.Load<GameObject>("Prefab/UI/Dialogue Window")).AddComponent<DialogueViewer>();
            }
            return _instance;
        }
    }

    public bool isShowing
    {
        get;
        private set;
    }

    Image portraitImage;
    Text dialogueText;
    
    public void CheckInstance()
    {

    }

    void Awake()
    {
        portraitImage = transform.FindChild("Portrait").GetComponent<Image>();
        dialogueText = transform.FindChild("Dialogue").GetComponent<Text>();
    }

    public void ShowDialogue(string dialogueName)
    {
        if (isShowing)
            throw new UnityException("Already showing dialogue!");

    }
}
