using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BulletBehaviour : NetworkBehaviour {

    public int m_damage;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<HealthBehaviour>())
        {
            other.gameObject.GetComponent<HealthBehaviour>().TakeDamage(m_damage);
            Destroy(gameObject);
        }
    }
}
