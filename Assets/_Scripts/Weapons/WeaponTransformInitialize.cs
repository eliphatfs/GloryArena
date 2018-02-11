using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTransformInitialize : MonoBehaviour {

	// Use this for initialization
	void Start () {
		WeaponData wd = GetComponent<WeaponData> ();
		transform.localPosition = wd.InitPosition;
		transform.localScale = wd.InitScale * 2;
		transform.localScale = wd.InitScale;
		transform.localEulerAngles = wd.InitRotation;
	}
	
	// Update is called once per frame
	void Update () {
		Start ();
		enabled = false;
	}
}
