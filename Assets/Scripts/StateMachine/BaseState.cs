using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState
{
  
    public abstract void EnterState(GameStateManager gameStateManager);

    public abstract void ExitState();

    public abstract void UpdateState(GameStateManager gameStateManager);

    


}
