using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DemolishInfo : MonoBehaviour
{
    public GameObject NormalTile;
    public GatherState CurrentGatherState;
    public DataDemolish dataDemolish;
    public Slider slider;
    public Resource[] Resources;
    float timer;
    private void Awake()
    {
        Resources = GetComponents<Resource>();

    }

    public IEnumerator StartDemolishSequence()
    {
        //change state from gatherable to gathering
        CurrentGatherState = GatherState.Gathering;
        //start countdown
        while (timer < dataDemolish.GatherDuration)
        {
            timer += Time.deltaTime;
            //start cutting or mining animation
            yield return null;
        }
        //wait for demolish time
        //add resources to the bank

        foreach (var item in Resources)
        {
            item.Drop();
        }

        Instantiate(NormalTile, transform.position, Quaternion.identity);
        Destroy(gameObject);
        //spawn normal tile
        //destroy this tile
    }

}
