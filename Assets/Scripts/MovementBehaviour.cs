using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[NetworkSettings(channel = 1, sendInterval = 0.05f)]
public class MovementBehaviour : NetworkBehaviour {

    Rigidbody self;
    public float movementForce;
    public float turnForce;
    public int updatesPerSecond;

    List<WheelCollider> wheels;

	// Use this for initialization
	void Start ()
    {
        self = GetComponent<Rigidbody>();
        if (isLocalPlayer)
            StartCoroutine(NetMove());

        wheels = new List<WheelCollider>();

        foreach (WheelCollider i in GetComponentsInChildren<WheelCollider>())
        {
            wheels.Add(i);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!isLocalPlayer)
            return;

        Vector2 inputVec = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Move(inputVec);
	}

    public void Move(Vector2 input)
    {
        Vector3 turnforce = new Vector3(0, input.x * turnForce, 0) * self.mass;
        self.AddRelativeTorque(turnforce);

        foreach (WheelCollider i in wheels)
        {
            i.motorTorque = movementForce * input.y;
        }
    }

    [Command]
    void CmdMove(Vector3 vel, Vector3 pos, Vector3 rot, Vector2 input)
    {
        RpcMove(vel, pos, rot, input);
    }

    [ClientRpc]
    void RpcMove(Vector3 vel, Vector3 pos, Vector3 rot, Vector2 input)
    {
        if (!isLocalPlayer)
        {
            self.velocity = vel;
            self.position = pos;
            self.rotation = Quaternion.Euler(rot);
            Move(input);
        }
    }

    IEnumerator NetMove()
    {
        while(true)
        {
            yield return new WaitForSeconds(Mathf.Min(0.05f, 1/updatesPerSecond));
            CmdMove(self.velocity, self.position, self.rotation.eulerAngles, new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
        }
    }
}
