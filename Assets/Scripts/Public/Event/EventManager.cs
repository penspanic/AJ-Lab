using System.Collections.Generic;

public enum EventType
{
    Dialog,
    ItemGet,
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
    
    public static EventData[] GetEvent(EventType type)
    {
        EventData[] dataArray = eventList.FindAll((data) =>
        {
            return data.type == type;
        }).ToArray();

        for (int i = 0; i < dataArray.Length; i++)
            eventList.Remove(dataArray[i]);

        return dataArray;
    }
}