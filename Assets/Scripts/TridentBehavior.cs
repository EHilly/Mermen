using UnityEngine;
using System.Collections;

public class TridentBehavior : MonoBehaviour {

	public bool flying;
	public bool recalling;
	public GameObject owner;

	void Start () {
		GetComponent<SpriteRenderer> ().color = owner.GetComponent<SpriteRenderer> ().color;
	}

	void FixedUpdate () {
		if (owner != null) {
			if (flying)
				transform.Translate (24f * Time.deltaTime, 0f, 0f, Space.Self);
			else if (recalling) {
				transform.right = owner.transform.position - transform.position;
				transform.Translate (15f * Time.deltaTime, 0f, 0f, Space.Self);
			}
		} else {
			Destroy (gameObject);
		}
	}
		
}