//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Projectile : MonoBehaviour
//{

//    [HideInInspector]
//    public bool hit;
//    [HideInInspector]
//    public float projetileDmg;

//    public Transform currentTower;

//    Transform target;
//    Vector3 targetPos;
//    float projectileSpeed;
//    bool canShoot;

//    private void OnEnable()
//    {
//        hit = false;

//    }

//    private void OnDisable()
//    {
//        target = null;

//        canShoot = false;
//        currentTower = null;

//    }

//    private void Update()
//    {
//        if (target != null)
//        {
//            targetPos = target.position;
//        }

//        if (canShoot)
//        {

//            transform.position = Vector3.MoveTowards(transform.position, targetPos, projectileSpeed * Time.deltaTime);
//            transform.LookAt(targetPos);

//        }
//    }

//    public void GetInfo(Transform target, float projectileSpeed, float projectileDmg, bool canShoot)
//    {
//        this.target = target;
//        target.GetComponent<EnemyHealth>().GetProjectile(this);
//        this.projectileSpeed = projectileSpeed;
//        this.projetileDmg = projectileDmg;
//        this.canShoot = canShoot;
//    }

//    public void ResetTarget()
//    {
//        target = null;
        
//    }



//    void OnTriggerEnter(Collider other)
//    {
//        if (other.CompareTag("Enemy"))
//        {
//            other.GetComponent<EnemyHealth>().ReduceHealth(projetileDmg);
//            hit = true;
//            if (target != null)
//                target.GetComponent<EnemyHealth>().RemoveProjectile(this);

//        }
//    }
//}
