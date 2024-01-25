using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demolish : MonoBehaviour
{
    [SerializeField] LayerMask demolishLayer;
    [SerializeField] GameObject tile;

    FlagManager flagManager;
    // Start is called before the first frame update
    void Start()
    {
        flagManager = FindObjectOfType<FlagManager>();
    }

    // Update is called once per frame
    void Update()
    {
        DemolishObjects();
    }

    void DemolishObjects()
    {
        if (flagManager.demolishMode)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, demolishLayer) && Input.GetMouseButtonDown(0))
            {
                Instantiate(tile, hit.transform.position, Quaternion.identity);
                Destroy(hit.transform.gameObject);

            }
        }
    }
}
