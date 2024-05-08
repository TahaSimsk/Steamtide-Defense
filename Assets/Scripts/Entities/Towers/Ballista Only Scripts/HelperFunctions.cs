using System;
using System.Collections;
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


    public static void LookAtTarget(Vector3 _targetPos, Transform _partToRotate, float _rotationSpeed)
    {

        Vector3 dir = _targetPos - _partToRotate.position;

        dir = new Vector3(dir.x, 0, dir.z);

        Quaternion lookRotation = Quaternion.LookRotation(dir);

        Vector3 rotation = Quaternion.Lerp(_partToRotate.rotation, lookRotation, Time.deltaTime * _rotationSpeed).eulerAngles;

        _partToRotate.rotation = Quaternion.Euler(rotation);

    }

    public static float CalculatePercentage(float _number, float _percentage, bool _positive)
    {
        if (_positive)
        {
            _number += _number * _percentage * 0.01f;
            return _number;
        }
        else
        {
            _number -= _number * _percentage * 0.01f;
            return _number;
        }
    }
  
}