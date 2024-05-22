using UnityEngine;

public class Wood : Resource
{
    public override void Drop()
    {
        MoneyManager.AddWood(DropAmount);
    }
}
