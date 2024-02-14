using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



[CreateAssetMenu(menuName = "Events/2Param")]
public class GameEvent2ParamSO : ScriptableObject
{
    public UnityAction<object, object> onEventRaised;

    public void RaiseEvent(object data1, object data2)
    {
        onEventRaised?.Invoke(data1, data2);
    }
}
