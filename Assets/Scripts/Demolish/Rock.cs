using UnityEngine;

public class Rock :Resource
{
    
    public override void Drop()
    {
        base.Drop();
        MoneyManager.AddRock(DropAmount);
    }
}
