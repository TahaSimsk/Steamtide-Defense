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
        EventManager.onButtonPressed += CreateTower;
        EventManager.onESCPressed += DestroyTowers;
    }
    private void OnDisable()
    {
        EventManager.onButtonPressed -= CreateTower;
        EventManager.onESCPressed -= DestroyTowers;
    }

    private void Start()
    {

        moneySystem = FindObjectOfType<MoneySystem>();
        flagManager = FindObjectOfType<FlagManager>();
    }

    private void Update()
    {

        PositionHoverTower();
    }


    void CreateTower(Button button)
    {
        try
        {
            DataTower dataTower = (DataTower)button.GetComponent<ButtonColorChanger>().data;
            InitTower(dataTower.towerHoverPrefab, dataTower.towerNPHoverPrefab, dataTower.objectPrefab, dataTower.objectCost_MoneyDrop);
            currentTowerData = dataTower;
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
            EventManager.OnTowerPlaced(instantiatedTower);
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
