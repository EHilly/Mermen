using UnityEngine;
using System.Collections;

public class TridentManager : MonoBehaviour {

	public int currentTridents;

	public GameObject pf_trident;
	public GameObject[] activeTridents;
	public GameObject scorekeeper;

	public bool invulnerable;

	void Start() {
		scorekeeper = GameObject.FindWithTag ("GUI");
		setupInvulnerability ();
	}


	public void throwTrident() {

		if (currentTridents < activeTridents.Length) {
			Vector2 pos = transform.position;
			GameObject tri = Instantiate (pf_trident, pos, Quaternion.Euler (new Vector3 (0, 0, transform.eulerAngles.z - 90)));
			tri.GetComponent<TridentBehavior> ().owner = gameObject;

			for (int i = 0; i < activeTridents.Length; i++) {
				if (activeTridents [i] == null) {
					activeTridents [i] = tri;
					break;
				}
			}

			currentTridents++;
		}
	}

	public void recallTridents() {

		for(int i = 0; i < activeTridents.Length; i++) {
			if(activeTridents[i] != null) {
				activeTridents [i].GetComponent<TridentBehavior> ().flying = false;
				activeTridents [i].GetComponent<TridentBehavior> ().recalling = true;
			}
		}

	}

	void OnTriggerEnter2D(Collider2D coll) {

		if (coll.gameObject.tag.Equals("Sharp")) {

			GameObject thrower = coll.gameObject.GetComponentInParent<TridentBehavior> ().owner;

			if (thrower == gameObject) {
				currentTridents--;
				Destroy (coll.gameObject.transform.parent.gameObject);
			} else if(!invulnerable) {
				scorekeeper.GetComponent<Scorekeeping> ().addScorePoint (thrower.GetComponent<PlayerMovement>().pNum);

				die ();
			}
		}
	}

	void die() {

		for (int i = 0; i < activeTridents.Length; i++) {
			if(activeTridents[i] != null)
				activeTridents [i].GetComponent<TridentBehavior> ().owner = null;
		}

		currentTridents = 0;

		setupInvulnerability ();
		transform.position = new Vector2 (0, -9.7f);

	}

	void setupInvulnerability() {

		invulnerable = true;
		Invoke ("removeInvulnerability", 2f);

		Color temp = GetComponent<SpriteRenderer> ().color;
		GetComponent<SpriteRenderer> ().color = new Color(temp.r, temp.g, temp.b, 0.5f);
	
	}

	void removeInvulnerability () {
		invulnerable = false;
		Color temp = GetComponent<SpriteRenderer> ().color;
		GetComponent<SpriteRenderer> ().color = new Color(temp.r, temp.g, temp.b, 1f);
	}
}