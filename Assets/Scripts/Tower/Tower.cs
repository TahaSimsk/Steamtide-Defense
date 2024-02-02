using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] SphereCollider targetScanner;

    //[Header("Weapon Type")]
    //[SerializeField] bool ballista;
    //[SerializeField] bool blaster;
    //[SerializeField] bool cannon;

    enum WeaponType { ballista, blaster, cannon }
    [SerializeField] WeaponType weaponType;

    [Header("Weapon Attributes")]
    [SerializeField] float projectileSpeed;
    [SerializeField] float projectileDmg;
    [SerializeField] float projectileLife;
    [SerializeField] float shootingDelay;
    [SerializeField] float weaponRotationSpeed;
    [SerializeField] float weaponRange;
    [SerializeField] float moneyCost;

    [Header("Projectile Positions")]
    [SerializeField] GameObject ballistaProjectilePos;
    [SerializeField] GameObject blasterProjectilePos;
    [SerializeField] GameObject cannonProjectilePos;



    ObjectPool objectPool;


    GameObject pooledProjectile;
    public List<GameObject> enemies = new List<GameObject>();



    bool hasProjectile, isProjectileFired;

    float timerForShootingDelay, timerForProjectileLife;



    private void Start()
    {
        objectPool = FindObjectOfType<ObjectPool>();
        targetScanner.radius = weaponRange;
    }

    void Update()
    {
        //TO DO: Sürekli hangi türde olduðunu check etmesine gerek yok startta bir kere yap currentSelected variable'ý ile ona ata ve update'de sadece onu al. Veya inheritance kullanarak base method oluþtur ve her yeni type için script oluþturup o methodu override et.
        switch (weaponType)
        {
            case WeaponType.ballista:
                GetProjectile(objectPool.GetObjectFromPool(objectPool.ballistaProjectileName, true), ballistaProjectilePos);
                break;
            case WeaponType.blaster:
                GetProjectile(objectPool.GetObjectFromPool(objectPool.blasterProjectileName, true), blasterProjectilePos);
                break;
            case WeaponType.cannon:
                GetProjectile(objectPool.GetObjectFromPool(objectPool.cannonProjectileName, true), cannonProjectilePos);
                break;
        }

        //if (ballista)
        //    GetProjectile(objectPool.GetObjectFromPool(objectPool.ballistaProjectileName, true), ballistaProjectilePos);
        //if (blaster)
        //    GetProjectile(objectPool.GetObjectFromPool(objectPool.blasterProjectileName, true), blasterProjectilePos);
        //if (cannon)
        //    GetProjectile(objectPool.GetObjectFromPool(objectPool.cannonProjectileName, true), cannonProjectilePos);


        LookAtEnemy();
        Shoot();
        DeactivateProjectile();
    }




    void GetProjectile(GameObject projectileFromPool, GameObject gameObjectToSetProjectilePosition)
    {
        timerForShootingDelay -= Time.deltaTime;

        if (!hasProjectile && timerForShootingDelay <= 0)
        {
            if (weaponType == WeaponType.ballista)
            {
                ballistaProjectilePos.SetActive(true);
            }
            pooledProjectile = projectileFromPool;

            pooledProjectile.GetComponent<Projectile>().currentTower = this.transform;

            pooledProjectile.transform.parent = gameObjectToSetProjectilePosition.transform.parent;
            pooledProjectile.transform.SetPositionAndRotation(gameObjectToSetProjectilePosition.transform.position, gameObjectToSetProjectilePosition.transform.rotation);

            hasProjectile = true;
        }
    }


    void LookAtEnemy()
    {
        if (enemies.Count == 0)
        {
            return;
        }
        Vector3 dir = enemies[0].transform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * weaponRotationSpeed).eulerAngles;
        transform.rotation = Quaternion.Euler(rotation);
    }


    void Shoot()
    {

        if (enemies.Count > 0 && pooledProjectile != null)
        {

            if (!isProjectileFired)
            {
                timerForShootingDelay = shootingDelay;
                isProjectileFired = true;
                if (weaponType == WeaponType.ballista)
                {
                    ballistaProjectilePos.SetActive(false);
                }
                pooledProjectile.SetActive(true);
                pooledProjectile.GetComponent<Projectile>().GetInfo(enemies[0].transform, projectileSpeed, projectileDmg, true);


                //deðiþtirildi
                //enemies[0].GetComponent<EnemyHealth>().GetTower(this);

                pooledProjectile.transform.parent = objectPool.gameObject.transform;
            }
        }
    }


    void DeactivateProjectile()
    {
        if (isProjectileFired)
        {


            timerForProjectileLife += Time.deltaTime;
            if (timerForProjectileLife >= projectileLife)
            {
                HandleProjectileHit();
                return;
            }
            else if (pooledProjectile.GetComponent<Projectile>().hit)
            {
                HandleProjectileHit();
                return;
            }
        }
    }




    void HandleProjectileHit()
    {
        hasProjectile = false;
        //timerForShootingDelay = shootingDelay;
        timerForProjectileLife = 0;

        pooledProjectile.SetActive(false);
        pooledProjectile = null;

        isProjectileFired = false;
    }

    public void AddEnemy(GameObject enemy)
    {
        enemies.Add(enemy);
    }

    public void RemoveEnemy(GameObject enemy)
    {
        enemies.Remove(enemy);
    }

    public void SetWeaponUpgradeAttributes(float projectileSpeed, float projectileDmg, float projectileLife, float shootingDelay, float weaponRange)
    {
        this.projectileSpeed = projectileSpeed;
        this.projectileDmg = projectileDmg;
        this.projectileLife = projectileLife;
        this.shootingDelay = shootingDelay;
        this.weaponRange = weaponRange;
    }


}



