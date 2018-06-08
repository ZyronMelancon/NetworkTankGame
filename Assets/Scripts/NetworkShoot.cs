using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[NetworkSettings(channel = 0, sendInterval = 0.05f)]
public class NetworkShoot : NetworkBehaviour {

    public Transform barrel;
    public GameObject bulletPrefab;
    public float shotForce;
    public float cooldownTime = 1;
    public float bulletLife = 2;

    bool canShoot = true;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!isLocalPlayer)
            return;
        if (Input.GetAxis("Fire1") == 1 && canShoot)
        {
            StartCoroutine(ShotCooldown());
            canShoot = false;
            CmdShoot();
        }
	}

    [Command]
    void CmdShoot()
    {
        //Instantiated on clientside
        var bullet = Instantiate(bulletPrefab, barrel.position, barrel.rotation) as GameObject;
        //Velocity set
        bullet.GetComponent<Rigidbody>().velocity = -barrel.forward * shotForce;
        //Destroy after X seconds
        Destroy(bullet, bulletLife);

        //Spawn on the server for all clients to see
        NetworkServer.Spawn(bullet);
    }

    IEnumerator ShotCooldown()
    {
        yield return new WaitForSeconds(cooldownTime);
        canShoot = true;
    }

}
