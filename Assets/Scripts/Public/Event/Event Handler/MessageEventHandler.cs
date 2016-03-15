using UnityEngine;
using System.Collections;
using System;

public class MessageEventHandler : EventHandler
{

    protected override IEnumerator EventProcess()
    {
        EventData newEvent;
        while(true)
        {
            newEvent = EventManager.GetEvent(EventType.Message);
            if (newEvent != null)
            {
                yield return StartCoroutine(
                    MessageBoxViewer.instance.ShowMessage(newEvent.data));
                yield return new WaitForSeconds(1f);
            }
            yield return null;
        }
    }
}