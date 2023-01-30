using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody rb;
    private float bulletSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        bulletSpeed = 20;
        
        rb.velocity = transform.forward * bulletSpeed;

        Invoke("Despawn", 5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            
        }else
        {
            Destroy(gameObject);

        }

        if (other.tag == "Wall")
        {
            Destroy(other.gameObject);
        }
    }

    private void Despawn()
    {
        Destroy(gameObject);
    }

}
