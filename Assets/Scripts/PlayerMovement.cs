using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;


public class PlayerMovement : NetworkBehaviour
{

    private Transform myTransform;
    public float speed = 15f;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;

    void Start()
    {
        myTransform = GetComponent<Transform>();
        this.transform.position = new Vector2(Random.Range(-7, 7), 0);
    }

    void Update()
    {

        if (!isLocalPlayer)
        {
            //Smooth Camera für 3D Spiele
            //Camera.main.transform.position = this.transform.position - this.transform.forward * 10 + this.transform.up * 3;
            //Camera.main.transform.LookAt(this.transform.transform.position);
            //Camera.main.transform.parent = this.transform;

            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }

        var x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        //var z = Input.GetAxis("Vertical") * Time.deltaTime * speed;

        myTransform.Translate(new Vector2(x, 0) * Time.deltaTime * speed);

    }

    void Fire()
    {
        // Create the Bullet from the Bullet Prefab
        var bullet = (GameObject)Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);

        // Add velocity to the bullet
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.up * 10;

        // Destroy the bullet after 2 seconds
        Destroy(bullet, 2.0f);
    }



}