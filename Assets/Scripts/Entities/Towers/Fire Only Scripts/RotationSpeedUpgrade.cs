
public class RotationSpeedUpgrade : UpgradeBaseClass
{
    FireData fireData;
    protected override void OnEnable()
    {
        base.OnEnable();
        fireData = towerData as FireData;
        maxUpgradeCount = fireData.RotateSpeedUpgradeValues.Count;
        if (counter >= maxUpgradeCount) return;
        upgradeCost = fireData.RotateSpeedUpgradeCosts[counter];
    }

    protected override void DoUpgrade()
    {
        float defRotationSpeed = towerInfo.DefTowerData.TowerRotationSpeed;
        fireData.TowerRotationSpeed = defRotationSpeed + (defRotationSpeed * fireData.RotateSpeedUpgradeValues[counter] * 0.01f);

        if (counter + 1 < maxUpgradeCount)
        {
            upgradeCost = fireData.RotateSpeedUpgradeCosts[counter + 1];
        }
        else
        {
            upgradeCost = fireData.RotateSpeedUpgradeCosts[counter];
        }
    }
}
