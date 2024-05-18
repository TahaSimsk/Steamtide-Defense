using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class DemolishInfo : MonoBehaviour
{
    DemolishState1 CurrentDemolishState;
    public DataDemolish dataDemolish;
        


    
    ,,0public IEnumerator StartDemolishSequence()
    {
        //change state from demolishable to demolishing
        //start countdown
        //start cutting or mining animation
        //wait for demolish time
        //add resources to the bank
        //spawn normal tile
        //destroy this tile
        yield return new WaitForEndOfFrame();
    }

}
