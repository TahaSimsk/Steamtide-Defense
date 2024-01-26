using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacementManager : MonoBehaviour
{
    [SerializeField] GameObject ballistaPrefab;
    [SerializeField] GameObject ballistaHoverPrefab;
    [SerializeField] GameObject ballistaNPHoverPrefab;

    [SerializeField] GameObject blasterPrefab;
    [SerializeField] GameObject blasterHoverPrefab;
    [SerializeField] GameObject blasterNPHoverPrefab;

    [SerializeField] GameObject cannonPrefab;
    [SerializeField] GameObject cannonHoverPrefab;
    [SerializeField] GameObject cannonNPHoverPrefab;

    [SerializeField] LayerMask placeableLayer;

    [SerializeField] Vector3 offset;


    GameObject instantiatedHoverTower;
    GameObject instantiatedHoverNPTower;
    GameObject instantiatedTower;

    bool init;
    float currentTowerMoneyCost;


    ObjectPool objectPool;
    MoneySystem moneySystem;
    FlagManager flagManager;

    private void Start()
    {
        objectPool = FindObjectOfType<ObjectPool>();
        moneySystem = FindObjectOfType<MoneySystem>();
        flagManager = FindObjectOfType<FlagManager>();
    }

    private void Update()
    {
        InitializeTowers();

        PositionHoverTower();
    }

    void InitializeTowers()
    {
        InitTower(flagManager.ballistaMode, ballistaHoverPrefab, ballistaNPHoverPrefab, ballistaPrefab, moneySystem.ballistaCost);
        InitTower(flagManager.blasterMode, blasterHoverPrefab, blasterNPHoverPrefab, blasterPrefab, moneySystem.blasterCost);
        InitTower(flagManager.cannonMode, cannonHoverPrefab, cannonNPHoverPrefab, cannonPrefab, moneySystem.cannonCost);
    }


    void InitTower(bool mode, GameObject hoverTower, GameObject hoverNPTower, GameObject tower, float towerCost)
    {
        if (mode && !init)
        {
            instantiatedHoverTower = Instantiate(hoverTower, Input.mousePosition, Quaternion.identity);

            instantiatedHoverNPTower = Instantiate(hoverNPTower, Input.mousePosition, Quaternion.identity);
            instantiatedHoverNPTower.SetActive(false);

            instantiatedTower = Instantiate(tower, Input.mousePosition, Quaternion.identity);
            instantiatedTower.SetActive(false);

            currentTowerMoneyCost = towerCost;
            init = true;
        }
    }

    void PositionHoverTower()
    {
        if (instantiatedHoverTower == null || flagManager.hoverMode) return;




        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, placeableLayer))
        {
            TileInfo tileInfo = hit.transform.GetComponent<TileInfo>();
            if (tileInfo == null) return;

            if (moneySystem.IsPlaceable(currentTowerMoneyCost) && tileInfo.placeable)
            {
                instantiatedHoverNPTower.SetActive(false);
                instantiatedHoverTower.SetActive(true);

                instantiatedHoverTower.transform.position = hit.transform.position + offset;

                PlaceTower(hit.transform.position, ref tileInfo.placeable);
            }
            else
            {
                instantiatedHoverNPTower.SetActive(true);
                instantiatedHoverTower.SetActive(false);

                instantiatedHoverNPTower.transform.position = hit.transform.position + offset;
            }
        }
    }

    void PlaceTower(Vector3 pos, ref bool info)
    {
        if (instantiatedTower != null && Input.GetMouseButtonDown(0))
        {
            instantiatedTower.transform.position = pos + offset;
            instantiatedTower.SetActive(true);
            info = false;

            instantiatedTower = Instantiate(instantiatedTower, Input.mousePosition, Quaternion.identity);
            instantiatedTower.SetActive(false);
            Debug.Log("created");

        }
    }





}
