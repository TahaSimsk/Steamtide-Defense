using UnityEngine;

public class Rock :Resource
{
    
    public override void Drop()
    {
        MoneyManager.AddRock(Amount);
    }
}
