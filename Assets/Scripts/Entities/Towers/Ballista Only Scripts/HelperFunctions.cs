using System;
using UnityEngine;

public static class HelperFunctions
{
    public static bool CheckImmunity(GameObject go, Enum en)
    {
        ObjectInfo ob = go.GetComponent<ObjectInfo>();

        if (ob == null) return false;

        EnemyData enemyData = ob.DefObjectGameData as EnemyData;
        if (enemyData.Immunity.HasFlag(en))
        {
            return true;
        }
        else
        {
            return false;
        }

    }
}