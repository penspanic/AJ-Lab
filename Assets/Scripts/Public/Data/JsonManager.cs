using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;

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
        JsonData dialogueJsonData = JsonMapper.ToObject(dialogueJson.text)[dialogueName];

        List<string> dialogueList = new List<string>();
        List<string> portraitList = new List<string>();

        for(int i = 0;i<dialogueJsonData["Dialogue"].Count;i++)
        {
            dialogueList.Add(dialogueJsonData["Dialogue"][i].ToString());
        }

        for(int i = 0;i<dialogueJsonData["Portrait"].Count;i++)
        {
            portraitList.Add(dialogueJsonData["Portrait"][i].ToString());
        }

        DialogueData returnData = new DialogueData();
        returnData.dialogue = dialogueList.ToArray();
        returnData.portrait = portraitList.ToArray();

        return returnData;
    }
}
