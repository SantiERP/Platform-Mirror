using System.Collections.Generic;

public class EventManager
{
    public enum EventsType
    {
        Event_PlayerDead,
        Event_SubstactLife,
        Event_EndOfLevel,
        Event_Restart
    }

    public delegate void EventReceiver(object[] parameters);

    static Dictionary<EventsType, EventReceiver> _events;

    //Para guardar el ultimo dato enviado
    static Dictionary<EventsType, object[]> _lastEventValue;

    EventReceiver restart;

    public static void SubscribeToEvent(EventsType eventType, EventReceiver listener)
    {
        if (_events == null)
        {
            _events = new Dictionary<EventsType, EventReceiver>();
            _lastEventValue = new Dictionary<EventsType, object[]>();
        }

        if (!_events.ContainsKey(eventType))
        {
            _events.Add(eventType, null);
        }

        _events[eventType] += listener;

        if (_lastEventValue.ContainsKey(eventType))
        {
            listener(_lastEventValue[eventType]);
        }
    }

    public static void UnsubscribeToEvent(EventsType eventType, EventReceiver listener)
    {
        if (_events == null) return;

        if (!_events.ContainsKey(eventType)) return;

        _events[eventType] -= listener;
    }

    public static void TriggerEvent(EventsType eventType, params object[] parameters)
    {
        if (_events == null) return;

        if (!_events.ContainsKey(eventType)) return;

        if (_events[eventType] == null) return;

        _events[eventType](parameters);
    }
}
