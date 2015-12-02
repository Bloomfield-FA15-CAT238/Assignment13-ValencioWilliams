using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Bullet : NetworkBehaviour {
	[SyncVar]
	public Color color;

	[SyncVar]
	public NetworkInstanceId parentNetId;
	
	public override void OnStartClient() {
		gameObject.GetComponent<Renderer> ().material.color = color;
	}

	void OnTriggerEnter(Collider other) {
		if(isServer) {
			Player player = ClientScene.FindLocalObject(parentNetId).GetComponent<Player>();
			player.score += 100;
			player.health -= 10;
			 if (player.health <= 0)
				Destroy(player.gameObject);
		}
	}


}