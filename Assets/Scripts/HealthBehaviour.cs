using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HealthBehaviour : NetworkBehaviour {

    public HealthScriptable startHealth;
    public HealthScriptable runTimeHealth;

    private void Start()
    {
        //Creates a runtime copy of startHealth
        runTimeHealth = Instantiate(startHealth);
    }

    public void TakeDamage(int amount)
    {
        //Makes sure the server is handling this, to prevent players from taking control
        if (!isServer)
            return;
        //Calls TakeDamage on the runtime scriptable
        runTimeHealth.TakeDamage(amount);

        if (runTimeHealth.m_Health <= 0)
            Destroy(gameObject); //If health hit zero or under, destroy the player
        else
            RpcUpdateHealth(runTimeHealth.m_Health); //Sends an update command to other clients
    }

    [ClientRpc]
    void RpcUpdateHealth(int newHealth)
    {
        if (!isLocalPlayer)
            //Updates all other clients with player's current health
            runTimeHealth.m_Health = newHealth;
    }
}

