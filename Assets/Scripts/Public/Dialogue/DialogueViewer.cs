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
                _instance = new GameObject("Dialogue Viewer").AddComponent<DialogueViewer>();
            }
            return _instance;
        }
    }

    public bool isShowing
    {
        get;
        private set;
    }

    GameObject dialogueWindow; 

    Image portraitImage;
    Text dialogueText;
    Sprite currPortrait;

    DialogueData currDialogueData;
    int currIndex = 0;
    bool textEffecting = false;

    public void CheckInstance()
    {

    }

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        dialogueWindow = Instantiate(Resources.Load<GameObject>("Prefab/UI/Dialogue Window"));
        dialogueWindow.transform.SetParent(transform);

        portraitImage = dialogueWindow.transform.FindChild("Portrait").GetComponent<Image>();
        dialogueText = dialogueWindow.transform.FindChild("Dialogue").GetComponent<Text>();

        dialogueWindow.transform.GetComponent<Button>().onClick.AddListener(NextDialogue);

        JsonManager.instance.CheckInstance();
    }

    void ResetParent()
    {
        transform.SetParent(GameObject.FindObjectOfType<Canvas>().transform, false);
    }
    public void ShowDialogue(string dialogueName)
    {
        if (isShowing)
        {
            throw new UnityException("Already showing dialogue!");
        }
        currDialogueData = JsonManager.instance.GetDialogueData(dialogueName);
        currIndex = 0;

        ResetParent();
        dialogueWindow.SetActive(true);

        SetDialogue(currDialogueData.dialogue[0], currDialogueData.portrait[0]);
    }

    void SetDialogue(string dialogue, string portrait)
    {
        if(!(currPortrait != null && currPortrait.name == portrait))
            currPortrait = Resources.Load<Sprite>("Sprite/Portrait/" + portrait);

        portraitImage.sprite = currPortrait;
        dialogueText.text = dialogue;
        StartCoroutine(TextEffect());
    }

    IEnumerator TextEffect()
    {
        textEffecting = true;

        float elapsedTime = 0f;
        float effectTime = 0.6f;

        Vector2 startPos = new Vector2(1.14f, -3.44f);
        Vector2 endPos = new Vector2(1.14f, -2.44f);

        while(elapsedTime < effectTime)
        {
            elapsedTime += Time.deltaTime;
            dialogueText.color = new Color(0, 0, 0, EasingUtil.easeOutSine(0, 1, elapsedTime / effectTime));
            dialogueText.transform.position = EasingUtil.EaseVector2(EasingUtil.easeOutSine, startPos, endPos, elapsedTime / effectTime);
            yield return null;
        }
        dialogueText.color = Color.black;
        dialogueText.transform.position = endPos;

        textEffecting = false;
    }

    void NextDialogue()
    {
        if (textEffecting)
            return;

        if (currDialogueData.dialogue.Length - 1 == currIndex)
        {
            CloseDialogueWindow();
            return;
        }
        currIndex++;
        SetDialogue(currDialogueData.dialogue[currIndex], GetPortrait(currIndex));
    }

    string GetPortrait(int index)
    {
        if (currDialogueData.portrait.Length == 1)
            return currDialogueData.portrait[0];
        else
            return currDialogueData.portrait[index];
    }

    void CloseDialogueWindow()
    {
        dialogueWindow.SetActive(false);
    }
}