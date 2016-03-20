using UnityEngine;
using System.Collections.Generic;

public enum EventType
{
    Dialog,
    Popup,
}

public class EventData
{
    public EventData(EventType type, string data)
    {
        this.type = type;
        this.data = data;       
    }
    public EventType type;
    public string data;

}

public static class EventManager
{
    static List<EventData> eventList = new List<EventData>();

    public static void PushEvent(EventData data)
    {
        if (eventList.Contains(data))
            return;
        eventList.Add(data);
    }
    
    public static List<EventData> GetEvents(EventType type)
    {
        List<EventData> returnList = eventList.FindAll((data) =>
        {
            return data.type == type;
        });

        foreach(EventData eachData in returnList)
        {
            eventList.Remove(eachData);
        }
        return returnList;
    }

    public static EventData GetEvent(EventType type)
    {
        EventData returnData = eventList.Find((data) =>
        {
            return data.type == type;
        });

        eventList.Remove(returnData);

        return returnData;
    }

    public static EventData GetEventByOrder()
    {
        EventData returnData = eventList.Find((data) =>
        {
            return data.type == EventType.Dialog;
        });

        if(returnData == null)
        {
            returnData = eventList.Find((data) =>
            {
                return data.type == EventType.Popup;
            });
        }

        eventList.Remove(returnData);

        return returnData;
    }
}