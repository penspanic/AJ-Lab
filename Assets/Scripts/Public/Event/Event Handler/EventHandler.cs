using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EventHandler : MonoBehaviour
{
    public bool messageEvent;
    public bool dialogEvent;

    List<EventData> eventList;

    void Awake()
    {
        StartCoroutine(EventProcess());
    }

    IEnumerator EventProcess()
    {
        EventData currEvent;
        while (true)
        {
            currEvent = EventManager.GetEventByOrder();
            if (currEvent != null)
            {
                switch (currEvent.type)
                {
                    case EventType.Dialog:
                        yield return StartCoroutine(
                            DialogViewer.instance.ShowDialogue(currEvent.data));
                        break;
                    case EventType.Popup:
                        yield return StartCoroutine(
                            PopupViewer.instance.ShowPopup(currEvent.data));
                        break;
                }
                yield return new WaitForSeconds(1f);
            }
            yield return null;
        }
    }


}
