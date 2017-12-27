using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine;

public class destroy_boundary : NetworkBehaviour {

	void OnTriggerExit(Collider other)
    {
        Destroy(other.gameObject);
    }
	
}
