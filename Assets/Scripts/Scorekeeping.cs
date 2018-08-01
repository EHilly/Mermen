using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scorekeeping : MonoBehaviour {

	public Text[] displayedScores;

	public GameObject middleWall; //temporary solutiuon
	public GameObject pf_wall;

	void Start() {

		for (int i = 0; i < displayedScores.Length; i++) {
			displayedScores [i] = transform.GetChild (i).GetComponent<Text> (); 
		}

		middleWall = Instantiate (pf_wall, new Vector2 (0, -3f), Quaternion.identity);
		middleWall.transform.localScale = new Vector2 (4f, 4f);
		middleWall.SetActive(false);

	}

	void FixedUpdate() {
		if (Input.GetKeyDown (KeyCode.B))
			middleWall.SetActive (!middleWall.activeInHierarchy);

	}

	public void addScorePoint (int n) {

		int newScore = int.Parse (displayedScores [n].text) + 1;
		displayedScores [n].text = newScore.ToString ();

	}
}