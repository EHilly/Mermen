using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManagement : MonoBehaviour {

	public GameObject p_player;

	public GameObject p3; //crappy bandaid code till I get menus working
	public GameObject p4;

	void Start () {
		spawnPlayer (0, 3f, -10f, "Horizontal1", "Vertical1", Color.yellow);
		spawnPlayer (1, -11f, -10f, "Horizontal2", "Vertical2", Color.green);
		spawnPlayer (2, -3f, -10f, "Horizontal3", "Vertical3", Color.magenta);
		spawnPlayer (3, 11f, -10f, "Horizontal4", "Vertical4", Color.gray);
	}

	void spawnPlayer(int n, float xSpawn, float ySpawn, string xName, string yName, Color c) {

		GameObject newPlayer = (GameObject)Instantiate(p_player.gameObject, new Vector2( xSpawn, ySpawn), Quaternion.identity);

		newPlayer.GetComponent<SpriteRenderer>().color = c;
		newPlayer.GetComponent<PlayerMovement> ().xAxis = xName;
		newPlayer.GetComponent<PlayerMovement> ().yAxis = yName;
		newPlayer.GetComponent<PlayerMovement> ().pNum = n;

		//temp
		if (n == 2)
			p3 = newPlayer;

		if (n == 3)
			p4 = newPlayer;
	}

	//temp
	void FixedUpdate() {
		if (Input.GetKey (KeyCode.N))
			Destroy (p4);

		if (Input.GetKey (KeyCode.M))
			Destroy (p3);
	}


}