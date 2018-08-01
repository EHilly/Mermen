using UnityEngine;
using System.Collections;

public class WallBehavior : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D coll) {

		GameObject tPart = coll.gameObject;

		if(tPart.name.Equals("tEmbedder")) {
			tPart.GetComponentInParent<Rigidbody2D> ().velocity = Vector2.zero;
			tPart.GetComponentInParent<TridentBehavior> ().flying = false;
			tPart.GetComponentInParent<TridentBehavior> ().recalling = false;
		}

	}

}