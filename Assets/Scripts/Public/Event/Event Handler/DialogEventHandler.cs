using UnityEngine;
using System.Collections;

public class DialogEventHandler : EventHandler
{

    protected override IEnumerator EventProcess()
    {
        eventList = EventManager.GetEvents(EventType.Dialog);

        foreach(EventData eachEvent in eventList)
        {
            yield return StartCoroutine(
                DialogViewer.instance.ShowDialogue(eachEvent.data));
        }
    }
}