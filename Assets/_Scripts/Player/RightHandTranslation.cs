using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHandTranslation : MonoBehaviour {
	public const int MOVE = 9713, ROTATE = 7352, GOBACK = 5168, ROTBACK = 3387;
	void Update () {
		while (LocalCommandBuffers.HasMoreMessages (MOVE))
			transform.localPosition += (Vector3)LocalCommandBuffers.PollMessageOfType (MOVE) [0];
		while (LocalCommandBuffers.HasMoreMessages (ROTATE))
			transform.Rotate ((Vector3)LocalCommandBuffers.PollMessageOfType (ROTATE) [0]);
		while (LocalCommandBuffers.HasMoreMessages (GOBACK)) {
			transform.localPosition = new Vector3 (3, 1.5f, 1.5f);
			LocalCommandBuffers.PollMessageOfType (GOBACK);
		}
		if (LocalCommandBuffers.HasMoreMessages (ROTBACK)) {
			StartCoroutine (rotateback ((int)LocalCommandBuffers.PollMessageOfType (ROTBACK) [0]));
			while (LocalCommandBuffers.HasMoreMessages (ROTBACK))
				LocalCommandBuffers.PollMessageOfType (ROTBACK);
		}
	}

	IEnumerator rotateback(int frames) {
		float prog = 0;
		float timeseconds = frames / 60f;
		Quaternion begin = transform.localRotation;
		Quaternion end = Quaternion.Euler (90, 0, 0);
		for (int i = 0; i < timeseconds / 0.03f; i++) {
			transform.localRotation = Quaternion.Slerp (begin, end, prog);
			prog += 0.03f / timeseconds;
			yield return new WaitForSeconds (0.03f);
		}
		transform.localRotation = end;
	}
}
