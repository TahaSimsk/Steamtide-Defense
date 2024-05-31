using UnityEngine;

public class Wood : Resource
{
    public override void Drop()
    {
        base.Drop();
        MoneyManager.AddWood(DropAmount);
    }
}
