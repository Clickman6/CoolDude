using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventsArray : MonoBehaviour {
    [SerializeField] private UnityEvent[] _events;

    public void StartEvent(int index) {
        _events[index].Invoke();
    }
}
