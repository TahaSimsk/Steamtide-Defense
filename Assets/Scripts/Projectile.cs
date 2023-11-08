using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public bool hit;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    private void OnEnable()
    {
        hit = false;

    }

    //public void Shoot()
    //{
    //    rb.velocity = Vector3.zero;
    //    rb.AddRelativeForce(Vector3.forward * 100, ForceMode.Impulse);
    //}



    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            hit = true;

            

        }
    }
}
