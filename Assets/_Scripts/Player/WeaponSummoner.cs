using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSummoner : MonoBehaviour {

	// Use this for initialization
	void Start () {
		PhotonNetwork.Instantiate ("_Glory/Models/Weapon_Spear", Vector3.zero, Quaternion.identity, 0).transform.parent = transform;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
