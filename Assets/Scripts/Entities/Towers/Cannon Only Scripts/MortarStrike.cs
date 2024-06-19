using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MortarStrike : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] ObjectInfo towerInfo;
    [SerializeField] GameEvent0ParamSO onEscPressed;
    [SerializeField] XPManager xpManager;
    CannonData cannonData;
    GameObject targetIndicator;

    bool escPressed;
    bool strikeStarted;
    void Start()
    {
        cannonData = (CannonData)towerInfo.InstTowerData;
        button.onClick.AddListener(BeginStrikeSequence);
    }

    private void OnEnable()
    {
        onEscPressed.onEventRaised += HandleEscPressed;
    }
    private void OnDisable()
    {
        onEscPressed.onEventRaised -= HandleEscPressed;
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

                if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
                {
                    StartCoroutine(StartTimer());
                    StartCoroutine(LaunchMissiles(hit.point));
                    strikeStarted = false;
                    break;
                }
            }
            yield return null;
        }
    }

    void HandleEscPressed()
    {
        if (strikeStarted)
            escPressed = true;
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
            missile.xpManager = xpManager;
            yield return new WaitForSeconds(cannonData.timeBetweenMissiles);
        }
    }

}
