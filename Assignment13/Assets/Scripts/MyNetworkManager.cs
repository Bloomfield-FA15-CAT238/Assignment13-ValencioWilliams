using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class MyNetworkManager : NetworkManager {
	public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId) {
		GameObject playerToSpawn = (GameObject)Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
		playerToSpawn.GetComponent<Player>().color = new Color(Random.Range(0.0f, 1.0f),Random.Range(0.0f, 1.0f),Random.Range(0.0f, 1.0f));
		NetworkServer.AddPlayerForConnection(conn, playerToSpawn, playerControllerId);
	}
}



