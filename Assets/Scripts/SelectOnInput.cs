using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectOnInput : MonoBehaviour {

	public EventSystem eventSystem;
	public GameObject selectedObj;

	private bool buttonSelected;
	
	// Update is called once per frame
	void Update () {

		if (Input.GetAxisRaw ("Vertical1") != 0 && !buttonSelected) {
			eventSystem.SetSelectedGameObject (selectedObj);
			buttonSelected = true;
		}
	}

	void OnDisable() {
		buttonSelected = false;
	}
}
