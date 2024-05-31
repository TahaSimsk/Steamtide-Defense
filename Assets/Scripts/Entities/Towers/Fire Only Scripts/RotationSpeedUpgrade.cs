
public class RotationSpeedUpgrade : UpgradeBaseClass
{
    FireData fireData;
    protected override void OnEnable()
    {
        base.OnEnable();
        fireData = towerData as FireData;
        maxUpgradeCount = fireData.RotateSpeedUpgradeValues.Count;
        if (counter >= maxUpgradeCount) return;
        upgradeMoneyCost = fireData.RotateSpeedUpgradeCosts[counter];
    }

    protected override void DoUpgrade()
    {
        float defRotationSpeed = towerInfo.DefTowerData.TowerRotationSpeed;
        fireData.TowerRotationSpeed = defRotationSpeed + (defRotationSpeed * fireData.RotateSpeedUpgradeValues[counter] * 0.01f);

        if (counter + 1 < maxUpgradeCount)
        {
            upgradeMoneyCost = fireData.RotateSpeedUpgradeCosts[counter + 1];
        }
        else
        {
            upgradeMoneyCost = fireData.RotateSpeedUpgradeCosts[counter];
        }
    }
}
