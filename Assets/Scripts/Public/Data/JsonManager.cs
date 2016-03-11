using UnityEngine;
using System.Collections;
using LitJson;

class DialogueData
{
    string[] dialogue;
}

public class JsonManager : MonoBehaviour
{
    static JsonManager _instance;
    public static JsonManager instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new GameObject("Json Manager").AddComponent<JsonManager>();
            }
            return _instance;
        }
    }

    TextAsset dialogueJson;

    public void CheckInstance()
    {

    }

    void Awake()
    {
        dialogueJson = Resources.Load<TextAsset>("Text File/Dialogue");
    }

    public DialogueData GetDialogueData(string dialogueName)
    {
        JsonReader r = new JsonReader(dialogueJson.text);
        r.
        JsonData dialogueJsonData;
        DialogueData returnData = new DialogueData();

        returnData
    }
}
