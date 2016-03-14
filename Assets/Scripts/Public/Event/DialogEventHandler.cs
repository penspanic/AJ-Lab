using UnityEngine;
using System.Collections;

public class DialogEventHandler : EventHandler
{

    void Awake()
    {
        StartCoroutine(EventProcess());
    }
    

    IEnumerator EventProcess()
    {
        EventData[] dataArray = EventManager.GetEvent(EventType.Dialog);

        if (dataArray != null)
        {
            DialogViewer.instance.CheckInstance();
            if (dataArray[0] != null)
            {
                yield return StartCoroutine(
                    DialogViewer.instance.ShowDialogue(dataArray[0].data));
            }
        }
    }
}