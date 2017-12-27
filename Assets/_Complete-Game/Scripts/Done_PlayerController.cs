using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

[System.Serializable]
public class Done_Boundary 
{
	public float xMin, xMax, zMin, zMax;
}





public class Done_PlayerController : NetworkBehaviour
{
    public float speed;
	public float tilt;
	public Done_Boundary boundary;

	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
    public float bulletspeed;

    //private float nextFire;

    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

        transform.Rotate(0, x , 0);
        transform.Translate(0, 0, z);

        if (Input.GetKeyDown(KeyCode.Space)) // && Time.time > nextFire
        {
           // nextFire = Time.time + fireRate;
            CmdFire();
        }

    }

    // This [Command] code is called on the Client …
    // … but it is run on the Server!
    [Command]
    void CmdFire()
    {
        var bullet = Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        bullet.GetComponent<Rigidbody>().velocity = transform.forward * bulletspeed;
        GetComponent<AudioSource>().Play();
    }

    void FixedUpdate ()
	{

        if (!isLocalPlayer)
        {
            return;
        }

        float moveHorizontal = Input.GetAxis ("Horizontal") * Time.deltaTime * speed;


        Vector3 movement = new Vector3 (moveHorizontal, 0.0f, 0.0f);
        GetComponent<Rigidbody>().velocity = movement * speed;

		
		GetComponent<Rigidbody>().position = new Vector3
		(
			Mathf.Clamp (GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax), 
			0.0f, 
			Mathf.Clamp (GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
		);
		
		GetComponent<Rigidbody>().rotation = Quaternion.Euler (0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt);
	}

}
