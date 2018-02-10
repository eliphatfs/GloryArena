using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		PhotonNetwork.offlineMode = true;
		PhotonNetwork.autoJoinLobby = true;
		PhotonNetwork.automaticallySyncScene = true;
		PhotonNetwork.CreateRoom ("__local__");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
