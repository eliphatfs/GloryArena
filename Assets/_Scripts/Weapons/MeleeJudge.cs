using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MeleeJudge : MonoBehaviour {
	public class JudgeInfo {
		public float Power, DamageMulti;
		public Action<Vector3> OnJudge;
	}

	Dictionary<int, JudgeInfo> _uidJudgeDict = new Dictionary<int, JudgeInfo> ();
	int _globalUIDCounter = 0;

	public int RegisterJudge (float power, Action<Vector3> whenJudge, float? damagemultiplier = null) {
		int uid = _globalUIDCounter++;
		_uidJudgeDict.Add (uid, new JudgeInfo () {
			Power = power,
			DamageMulti = damagemultiplier == null ? 1f : damagemultiplier.Value,
			OnJudge = whenJudge
		});
		return uid;
	}

	public void RemoveJudge (int uid) {
		_uidJudgeDict.Remove (uid);
	}

	void OnCollisionEnter(Collision collision) {
		OnCollisionStay (collision);
	}

	void OnCollisionStay(Collision collision) {
		if (_uidJudgeDict.Count == 0 || collision.gameObject.layer <= 12)
			return;
		MeleeJudge othjudge = collision.gameObject.GetComponent<MeleeJudge> ();
		if (othjudge != null) {
			Queue<int> thsk = new Queue<int> (), othk = new Queue<int> ();
			Queue<JudgeInfo> thsv = new Queue<JudgeInfo> (), othv = new Queue<JudgeInfo> ();
			foreach (var entry in _uidJudgeDict) {
				thsk.Enqueue (entry.Key);
				thsv.Enqueue (entry.Value);
			}
			foreach (var entry in othjudge._uidJudgeDict) {
				othk.Enqueue (entry.Key);
				othv.Enqueue (entry.Value);
			}
			while (thsk.Count > 0 && othk.Count > 0) {
				if (thsv.Peek ().Power > othv.Peek ().Power) {
					thsv.Peek ().Power -= othv.Peek ().Power;
					othjudge.RemoveJudge (othk.Dequeue ());
					othv.Dequeue ();
				} else if (thsv.Peek ().Power < othv.Peek ().Power) {
					othv.Peek ().Power -= thsv.Peek ().Power;
					this.RemoveJudge (thsk.Dequeue ());
					thsv.Dequeue ();
				} else {
					this.RemoveJudge (thsk.Dequeue ());
					thsv.Dequeue ();
					othjudge.RemoveJudge (othk.Dequeue ());
					othv.Dequeue ();
				}
			}
		}
		if (_uidJudgeDict.Count == 0)
			return;
		float dmg = 0;
		WeaponData wd = GetComponent<WeaponData> ();
		foreach (var entry in _uidJudgeDict) {
			dmg += entry.Value.DamageMulti
			* entry.Value.Power
			* wd.Damage
			* Mathf.Lerp (1f - wd.DamageFloating, 1f + wd.DamageFloating, Mathf.Clamp01 (Mathf.Log (collision.relativeVelocity.sqrMagnitude) / 9f));
			entry.Value.OnJudge.Invoke (collision.contacts [0].point);
		}
		HP hp = collision.collider.GetComponent<HP> ();
		if (hp != null)
			hp.DealDamage (dmg);
		_uidJudgeDict.Clear ();
	}
}
