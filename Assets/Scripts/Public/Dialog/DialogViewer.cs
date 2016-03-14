using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DialogViewer : MonoBehaviour
{
    static DialogViewer _instance;
    public static DialogViewer instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new GameObject("Dialog Viewer").AddComponent<DialogViewer>();
            }
            return _instance;
        }
    }

    public bool isShowing
    {
        get;
        private set;
    }

    GameObject dialogWindow; 

    Image portraitImage;
    Text dialogText;
    Sprite currPortrait;

    DialogData currDialogueData;
    int currIndex = 0;
    bool textEffecting = false;

    public void CheckInstance()
    {

    }

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        dialogWindow = Instantiate(Resources.Load<GameObject>("Prefab/UI/Dialog Window"));
        dialogWindow.transform.SetParent(transform);

        portraitImage = dialogWindow.transform.FindChild("Portrait").GetComponent<Image>();
        dialogText = dialogWindow.transform.FindChild("Dialog").GetComponent<Text>();

        dialogWindow.transform.GetComponent<Button>().onClick.AddListener(NextDialogue);

        JsonManager.instance.CheckInstance();
    }

    void ResetParent()
    {
        transform.SetParent(GameObject.FindObjectOfType<Canvas>().transform, false);
    }
    public IEnumerator ShowDialogue(string dialogueName)
    {
        if (isShowing)
        {
            throw new UnityException("Already showing dialog!");
        }
        isShowing = true;

        currDialogueData = JsonManager.instance.GetDialogueData(dialogueName);
        currIndex = 0;

        ResetParent();
        dialogWindow.SetActive(true);

        SetDialogue(currDialogueData.dialog[0], currDialogueData.portrait[0]);

        while (isShowing)
            yield return null;
    }

    void SetDialogue(string dialogue, string portrait)
    {
        if(!(currPortrait != null && currPortrait.name == portrait))
            currPortrait = Resources.Load<Sprite>("Sprite/Portrait/" + portrait);

        portraitImage.sprite = currPortrait;
        dialogText.text = dialogue;
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
            dialogText.color = new Color(0, 0, 0, EasingUtil.easeOutSine(0, 1, elapsedTime / effectTime));
            dialogText.transform.position = EasingUtil.EaseVector2(EasingUtil.easeOutSine, startPos, endPos, elapsedTime / effectTime);
            yield return null;
        }
        dialogText.color = Color.black;
        dialogText.transform.position = endPos;

        textEffecting = false;
    }

    void NextDialogue()
    {
        if (textEffecting)
            return;

        if (currDialogueData.dialog.Length - 1 == currIndex)
        {
            isShowing = false;
            CloseDialogueWindow();
            return;
        }
        currIndex++;
        SetDialogue(currDialogueData.dialog[currIndex], GetPortrait(currIndex));
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
        dialogWindow.SetActive(false);
    }
}