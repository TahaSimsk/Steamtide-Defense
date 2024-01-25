using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    [SerializeField] GameObject bombHoverPrefab;
    [SerializeField] GameObject bombPrefab;

    [SerializeField] GameObject slowHoverPrefab;
    [SerializeField] GameObject slowPrefab;

    [SerializeField] float bombDamage;
    [SerializeField] float slowPercent;
    [SerializeField] float slowDuration;
    [SerializeField] float slowDropRadius;

    [SerializeField] LayerMask ignoreEnemyLayer;

    GameObject instantiatedBombHover;
    GameObject instantiatedSlowHover;

    FlagManager flagManager;

    MoneySystem moneySystem;

    private void Awake()
    {
        flagManager = FindObjectOfType<FlagManager>();
        bombPrefab.GetComponent<Skills>().bombDamage = bombDamage;
        slowPrefab.GetComponent<SkillSlow>().slowPercent = slowPercent;
        moneySystem = FindObjectOfType<MoneySystem>();
    }


    private void Update()
    {
        CreateBombHover(flagManager.bombMode, ref instantiatedBombHover, bombHoverPrefab);
        CreateBombHover(flagManager.slowMode, ref instantiatedSlowHover, slowHoverPrefab);

        SetBombHoverPos(instantiatedBombHover);
        SetBombHoverPos(instantiatedSlowHover);

        DropBomb(instantiatedBombHover, moneySystem.bombCost, bombPrefab);
        DropSlow();
    }

    void CreateBombHover(bool mode, ref GameObject instantiatedSkillHover, GameObject skillHoverPrefab)
    {

        if (mode && instantiatedSkillHover == null)
        {
            instantiatedSkillHover = Instantiate(skillHoverPrefab, Input.mousePosition, Quaternion.identity);

        }
        else if (!mode && instantiatedSkillHover != null)
        {
            DestroyBombHover(instantiatedSkillHover);
        }
    }

    void SetBombHoverPos(GameObject instantiatedSkillHover)
    {
        if (instantiatedSkillHover == null) return;

        if (flagManager.hoverMode)
        {
            instantiatedSkillHover.SetActive(false);
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~ignoreEnemyLayer))
        {
            instantiatedSkillHover.SetActive(true);
            instantiatedSkillHover.transform.position = hit.transform.position + Vector3.up * 2;

        }
        else
        {
            instantiatedSkillHover.SetActive(false);
        }
    }


    void DropBomb(GameObject instantiatedSkillHover, float skillCost, GameObject skillPrefab)
    {
        if (instantiatedSkillHover == null || flagManager.hoverMode || moneySystem.IsPlaceable(skillCost) == false) { return; }

        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(skillPrefab, instantiatedSkillHover.transform.position, Quaternion.identity);
            moneySystem.DecreaseMoney(skillCost);
            moneySystem.UpdateMoneyDisplay();
        }
    }

    void DropSlow()
    {
        if (instantiatedSlowHover == null || flagManager.hoverMode || moneySystem.IsPlaceable(moneySystem.slowCost) == false) { return; }

        if (Input.GetMouseButtonDown(0))
        {


            Collider[] hitColliders = Physics.OverlapSphere(instantiatedSlowHover.transform.position, slowDropRadius);
            foreach (var collider in hitColliders)
            {
                if (collider.transform.CompareTag("Path2"))
                {
                    GameObject instSlowPrefab = Instantiate(slowPrefab, collider.transform.position, Quaternion.identity);

                    instSlowPrefab.transform.localScale = collider.transform.localScale;
                    SkillSlow skillSlow = instSlowPrefab.GetComponent<SkillSlow>();
                    skillSlow.slowPercent = slowPercent;
                    skillSlow.slowDuration = slowDuration;


                }

            }

            moneySystem.DecreaseMoney(moneySystem.slowCost);
            moneySystem.UpdateMoneyDisplay();
        }
    }

    public void DestroyBombHover(GameObject instantiatedSkillHover)
    {
        if (instantiatedSkillHover == null) { return; }
        Destroy(instantiatedSkillHover);
    }

    public void ActivateBombHover(bool value)
    {
        if (instantiatedBombHover == null) { return; }
        instantiatedBombHover.SetActive(value);
    }
}
