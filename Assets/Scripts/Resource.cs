using UnityEngine;

public abstract class Resource : MonoBehaviour
{
    public int Amount;
    public MoneyManager MoneyManager;
    public abstract void Drop();
}