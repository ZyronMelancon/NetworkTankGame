using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HealthBehaviour : NetworkBehaviour {

    public HealthScriptable startHealth;
    [SyncVar]
    public int m_Health;

    private void Start()
    {
        //Copies starting health
        m_Health = startHealth.m_Health;
    }

    public void TakeDamage(int amount)
    {
        //Makes sure the server is handling this, to prevent players from taking control
        if (!isServer)
            return;
        //Applies damage
        m_Health -= amount;

        if (m_Health <= 0)
            Destroy(gameObject); //If health hit zero or under, destroy the player
    }
}

