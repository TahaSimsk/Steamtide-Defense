using UnityEngine;

public class Resource : MonoBehaviour
{
    public string NameOfResource;
    public int DropAmount { get; set; }
    public Vector2Int MinMaxDropAmount;
    public MoneyManager MoneyManager;

    protected virtual void Awake()
    {
        DropAmount = Random.Range(MinMaxDropAmount.x, MinMaxDropAmount.y);
    }



    public virtual void Drop()
    {

    }
}