using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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


    float currentTowerMoneyCost;


    MoneySystem moneySystem;
    FlagManager flagManager;

    Data currentTowerData;

    private void OnEnable()
    {
        FlagManager.onStateChanged += InitializeTowers;
        EventManager.onButtonPressed += Anan;
        EventManager.onESCPressed += DestroyTowers;
    }
    private void OnDisable()
    {
        FlagManager.onStateChanged -= InitializeTowers;
        EventManager.onButtonPressed -= Anan;
        EventManager.onESCPressed -= DestroyTowers;
    }

    private void Start()
    {

        moneySystem = FindObjectOfType<MoneySystem>();
        flagManager = FindObjectOfType<FlagManager>();
    }

    private void Update()
    {
        //InitializeTowers();

        PositionHoverTower();
    }

    void InitializeTowers()
    {
        //switch (flagManager.currentMode)
        //{
        //    case FlagManager.CurrentMode.ballista:
        //        InitTower(ballistaHoverPrefab, ballistaNPHoverPrefab, ballistaPrefab, moneySystem.ballistaCost);

        //        break;
        //    case FlagManager.CurrentMode.blaster:
        //        InitTower(blasterHoverPrefab, blasterNPHoverPrefab, blasterPrefab, moneySystem.blasterCost);
        //        break;
        //    case FlagManager.CurrentMode.cannon:
        //        InitTower(cannonHoverPrefab, cannonNPHoverPrefab, cannonPrefab, moneySystem.cannonCost);
        //        break;
        //    default:
        //        DestroyTowers();
        //        break;
        //}
    }

    void Anan(Data data, Button button)
    {
        try
        {
            DataTower dataTower = (DataTower)data;
            InitTower(dataTower.towerHoverPrefab, dataTower.towerNPHoverPrefab, data.objectPrefab, data.cost);
            currentTowerData = data;
        }
        catch (System.Exception)
        {
            DestroyTowers();
            return;
        }

    }


    void InitTower(GameObject hoverTower, GameObject hoverNPTower, GameObject tower, float towerCost)
    {

        DestroyTowers();
        instantiatedHoverTower = Instantiate(hoverTower, Input.mousePosition, Quaternion.identity);

        instantiatedHoverNPTower = Instantiate(hoverNPTower, Input.mousePosition, Quaternion.identity);
        instantiatedHoverNPTower.SetActive(false);

        instantiatedTower = Instantiate(tower, Input.mousePosition, Quaternion.identity);
        instantiatedTower.SetActive(false);

        currentTowerMoneyCost = towerCost;


    }

    void PositionHoverTower()
    {
        if (instantiatedHoverTower == null) return;

        if (flagManager.hoverMode)
        {
            instantiatedHoverTower.SetActive(false);
            instantiatedHoverNPTower.SetActive(false);
            return;
        }



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
        else
        {
            instantiatedHoverTower.SetActive(false);
            instantiatedHoverNPTower.SetActive(false);
        }
    }

    void PlaceTower(Vector3 pos, ref bool info)
    {
        if (instantiatedTower != null && Input.GetMouseButtonDown(0))
        {
            instantiatedTower.transform.position = pos + offset;
            instantiatedTower.SetActive(true);
            info = false;
            //moneySystem.DecreaseMoney(currentTowerMoneyCost);
            EventManager.OnTowerPlaced(currentTowerData);
            moneySystem.UpdateMoneyDisplay();

            instantiatedTower = Instantiate(instantiatedTower, Input.mousePosition, Quaternion.identity);

            instantiatedTower.SetActive(false);

        }
    }

    void DestroyTowers()
    {
        Destroy(instantiatedTower);
        instantiatedTower = null;
        Destroy(instantiatedHoverTower);
        instantiatedHoverTower = null;
        Destroy(instantiatedHoverNPTower);
        instantiatedHoverNPTower = null;

    }



}
