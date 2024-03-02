using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MortarStrike : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] TowerInfo towerInfo;
    [SerializeField] GameEvent0ParamSO onEscPressed;
    [SerializeField] GameEvent1ParamSO onUIHovered;
    CannonData cannonData;
    GameObject targetIndicator;

    bool escPressed;
    bool strikeStarted;
    bool uiHovered;
    void Start()
    {
        cannonData = (CannonData)towerInfo.InstTowerData;
        button.onClick.AddListener(BeginStrikeSequence);
    }

    private void OnEnable()
    {
        onEscPressed.onEventRaised += HandleEscPressed;
        onUIHovered.onEventRaised += HandleUIHovered;
    }
    private void OnDisable()
    {
        onEscPressed.onEventRaised -= HandleEscPressed;
        onUIHovered.onEventRaised -= HandleUIHovered;
    }

    private void OnDestroy()
    {
        button.onClick.RemoveListener(BeginStrikeSequence);
    }




    void BeginStrikeSequence()
    {
        StartCoroutine(BeginStrike());
    }

    IEnumerator BeginStrike()
    {
        while (true)
        {
            strikeStarted = true;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (escPressed)
                {
                    Destroy(targetIndicator);
                    strikeStarted = false;
                    escPressed = false;
                    break;
                }

                if (targetIndicator == null)
                    targetIndicator = Instantiate(cannonData.MortarTargetIndicator, hit.point, Quaternion.identity);

                targetIndicator.transform.position = hit.point + Vector3.up * 2f;

                if (Input.GetMouseButtonDown(0) && !uiHovered)
                {
                    StartCoroutine(StartTimer());
                    StartCoroutine(LaunchMissiles(hit.point));
                    strikeStarted = false;
                    break;
                }
                else
                {
                    Debug.Log("mevenin anasý");
                }
            }
            yield return null;
        }
    }

    void HandleEscPressed()
    {
        if (strikeStarted)
            escPressed = true;
        uiHovered = false;
    }

    void HandleUIHovered(object uiHovered)
    {
        if (uiHovered is bool b)
            this.uiHovered = b;
    }

    IEnumerator StartTimer()
    {

        button.interactable = false;
        yield return new WaitForSeconds(cannonData.MortarCooldown);
        button.interactable = true;
    }

    IEnumerator LaunchMissiles(Vector3 hitPoint)
    {
        for (int i = 0; i < cannonData.numOfMissilesToLaunch; i++)
        {
            GameObject tempMissile = Instantiate(cannonData.missilePrefab, towerInfo.transform.position + Vector3.up * 4f, Quaternion.identity);

            Missile missile = tempMissile.GetComponent<Missile>();

            if (i == cannonData.numOfMissilesToLaunch - 1)
            {
                missile.targetIndicator = targetIndicator;
            }
            missile.cannonData = cannonData;
            missile.targetPos = hitPoint;
            yield return new WaitForSeconds(cannonData.timeBetweenMissiles);
        }
    }

}
