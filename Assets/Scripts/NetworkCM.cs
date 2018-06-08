using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkCM : NetworkBehaviour {

    public GameObject CMRig;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!ClientScene.ready)
            return;
        else if (isLocalPlayer && !CMRig.activeSelf)
            CMRig.SetActive(true);
	}
}
