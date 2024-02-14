using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/1Param")]
public class GameEvent1ParamSO : ScriptableObject
{
    public UnityAction<object> onEventRaised;

    public void RaiseEvent(object data)
    {
        onEventRaised?.Invoke(data);
    }
}
