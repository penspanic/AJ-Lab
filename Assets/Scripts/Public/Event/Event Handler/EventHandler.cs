using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class EventHandler : MonoBehaviour
{
    protected List<EventData> eventList;

    void Awake()
    {
        StartCoroutine(EventProcess());
    }

    protected abstract IEnumerator EventProcess();


}
