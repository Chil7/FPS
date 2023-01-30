using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody rb;

    [Range(1, 20)]
    [SerializeField] private float speed = 5;
    [Range(1, 100)]
    [SerializeField] private float degreesPerSecond = 100;

    private bool isJump;
    [Range(1,100)]
    [SerializeField] private float jumpForce = 10f;
    private int jumpCount;

    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform firePoint;

    private float fireRate = 0.5f;
    private float lastShot = 0;

    [SerializeField] private Transform[] spreadPoints;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        isJump = true;
        jumpCount = 2;
    }

    // Update is called once per frame
    void Update()
    {
        //Check if player moves forward and backward
        if(Input.GetKey("w"))
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }
        else if(Input.GetKey("s"))
        {
            transform.position += -transform.forward * speed * Time.deltaTime;
        }
        //Checks if player moces left and right
        if(Input.GetKey("a"))
        {
            transform.position += -transform.right * speed * Time.deltaTime;
        }
        else if(Input.GetKey("d"))
        {
            transform.position += transform.right * speed * Time.deltaTime;
        }

        //Checks if player press Jump or shift
        if(Input.GetKeyDown("space"))
        {
            if (jumpCount > 0)
            {
                if(isJump == true)
                {
                    jumpCount--;
                    rb.AddForce(Vector3.up * jumpForce,ForceMode.Impulse);
 
                }
            }
            else
            {
                isJump = false;
            }
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            transform.position += -transform.up * speed * Time.deltaTime;
        }

        //check ig the player is pressing q or e
        if (Input.GetKey("q"))
        {
            transform.Rotate(new Vector3(0, -degreesPerSecond, 0) * Time.deltaTime);
        }
        else if (Input.GetKey("e"))
        {
            transform.Rotate(new Vector3(0, degreesPerSecond, 0) * Time.deltaTime);
        }

        //Check if player press elft click
        if (Input.GetMouseButton(0))
        {
            Fire();
        }

        else if (Input.GetMouseButtonDown(1))
        {
            SpreadFire();
        }
    }

    private void Fire()
    {
        if (Time.time > fireRate + lastShot)
        {
            Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation);
            lastShot = Time.time;
        }
    }
    private void SpreadFire()
    {
        foreach (Transform spreadpoint in spreadPoints)
        {
            Instantiate(bullet, spreadpoint.transform.position, spreadpoint.transform.rotation);
            lastShot = Time.time;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "ground")
        {
            jumpCount = 2;
            isJump = true;
        }
    }
}
