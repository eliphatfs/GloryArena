using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HP : MonoBehaviour {

	public float MaxLife;
	[SerializeField]
	private float _life;
	public UnityEvent OnDying;

	void Start () {
		_life = MaxLife;
	}

	void Update () {
		if (_life > MaxLife)
			_life = MaxLife;
		if (_life <= 0f)
			OnDying.Invoke ();
	}

	public void DealDamage (float dmg) {
		_life -= dmg;
	}
}
