using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Player : NetworkBehaviour {

	public GameObject SpawnPoint;
	public GameObject bulletPrefab;

	[SyncVar]
	public int health;

	[SyncVar]
	public Color color;

	[SyncVar]
	public int score;
	
	float moveSpeed = 1.875f;

	//NetworkServer.AddPlayerForConnection(conn, playerToSpawn, playerControllerId);
	//playerToSpawn.GetComponent<Renderer>().material.color = color;

	private Text scoreText;
	private Text healthText;

	public GameObject player;

	public override void OnStartClient() {
		health = 100;
		gameObject.GetComponent<Renderer>().material.color = color;
		scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
		healthText = GameObject.Find("HealthText").GetComponent<Text>();

	}
	
	void Update() {
		if(isLocalPlayer) {
			GetInput();
			scoreText.text = "Score: " + score;
			healthText.text = "Health: " + health;

		}
	}
	
	void GetInput() {
		float x = Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime;
		float y = Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime;
		
		if(isServer) {
			RpcMoveIt(x,y);
		} else {
			CmdMoveIt(x,y);
		}

		if(Input.GetButtonUp("Fire1")) {
			CmdDoFire();
		}
	}
	
	[ClientRpc]
	void RpcMoveIt(float x, float y) {
		transform.Translate(x * 5, y * 5,0);
	}
	
	[Command]
	public void CmdMoveIt(float x, float y) {
		RpcMoveIt(x,y);
	}

	[Command]
	public void CmdDoFire() { 
		GameObject bullet = (GameObject)Instantiate(bulletPrefab, this.transform.position + this.transform.right + this.transform.forward, Quaternion.identity);
		bullet.GetComponent<Rigidbody>().velocity = Vector3.forward * 17.5f;
		bullet.GetComponent<Bullet>().color = color;
		bullet.GetComponent<Bullet>().parentNetId = this.netId;
		Destroy(bullet,1.5f);
		NetworkServer.Spawn(bullet);
	}
	
}

