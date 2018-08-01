using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public int pNum;

	//movement 'feel' stuff
	public float swimmingStep;
	public float swimmingMax;
	public float maxAscension;
	public float uDrag;
	public float terminalV;

	//surfacing/submerging
	public bool underwater;
	public float surfaceLevel;

	//control
	public bool firePressed;
	public string xAxis;
	public string yAxis;

	//caching
	private Rigidbody2D myRigidbody;
	private SpriteRenderer lineR;

	void Awake() {
		myRigidbody = GetComponent<Rigidbody2D> ();
		lineR = transform.GetChild(0).GetComponent<SpriteRenderer> ();
	}

	void FixedUpdate () {

		float hor = Input.GetAxis(xAxis);
		float ver = Input.GetAxis(yAxis);
		Vector2 velo = myRigidbody.velocity;

		bool oldUnderwater = underwater;
		underwater = transform.position.y < surfaceLevel;

		if (underwater) {

			velo.x = horizontalSwimming (hor, velo);
			velo.y = verticalSwimming (ver, velo);

		} else {

			if(ver > 0 && !firePressed)
				GetComponent<TridentManager> ().throwTrident ();

			velo.y = Mathf.Max (velo.y - 16f*Time.deltaTime, terminalV); //apply gravity
			aerialControl(hor, ver);
		}

		if (oldUnderwater && !underwater) { //if the player was recently underwater, but now is not (i.e. just surfaced)
			velo.x *= 0.52f;			  //dampen the lateral movement - it feels strange to go flying out of the water at a 45 degree angle
			lineR.enabled = true;
		} else if (!oldUnderwater && underwater) {
			transform.rotation = Quaternion.identity;
			lineR.enabled = false;
		}

		firePressed = ver > 0;

		if (ver < 0)
			GetComponent<TridentManager> ().recallTridents ();

		myRigidbody.velocity = velo;
	}
	
	float horizontalSwimming(float hor, Vector2 velo) {

		float newX = velo.x;

		if (hor == 0f)
			newX = Mathf.Abs (velo.x) < uDrag ? 0 : velo.x - (uDrag * Mathf.Sign(velo.x)); 
		else if(velo.x * hor < swimmingMax) //if moving above the swimming speed max (e.g. propelled by explosion), you may only slow down apply force in opposite direction
			newX = velo.x+(hor*swimmingStep*Time.deltaTime);

		return newX;
	}

	float verticalSwimming(float ver, Vector2 velo) {

		float newY = velo.y;

		if (ver > 0) {
			
			if (velo.y <= 2.3f)
				newY += 22f * Time.deltaTime;
			else
				newY = Mathf.Min (maxAscension, Mathf.Pow (velo.y, 1.1f));
			
		} else if (velo.y > 0) {
			newY = Mathf.Max (velo.y - 30f * Time.deltaTime, 0f);
		} else if (velo.y > -4.8) {
			newY += (-5 - velo.y) * 0.008f;
		} else {
			newY = Mathf.Min (velo.y + 8f * Time.deltaTime, -4.8f);
		}

		return newY;
	}
	
	void aerialControl(float hor, float ver) {
		transform.Rotate (Vector3.forward * -hor, 220f * Time.deltaTime);
	}
}