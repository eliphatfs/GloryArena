using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSkill : MonoBehaviour {
	protected abstract string Name {
		get;
	}
	protected abstract int MaxCoolDown {
		get;
	}
	protected int CoolDown;

	internal StateMachine fsm;
	private int mSkillTime;
	// Use this for initialization
	private void Start () {
		fsm = GetComponent<StateMachine> ();
		fsm.RegisterState (Name, _enter);
		fsm.RegisterTransition ("Idle", () => {
			if (CoolDown <= 0 && SkillActivation)
				return Name;
			return null;
		});
		fsm.RegisterTransition (Name, () => {
			DuringSkill (mSkillTime++);
			return null;
		});
	}
	
	// Update is called once per frame
	private void Update () {
		CoolDown--;
	}

	private void _enter () {
		mSkillTime = 0;
		OnSkillBegin ();
	}

	public void ExitCD () {
		CoolDown = 1;
	}

	public void EnterCD () {
		CoolDown = MaxCoolDown;
	}

	protected virtual void OnSkillBegin () {
	}

	protected virtual void DuringSkill (int frame) {
	}

	protected virtual void OnSkillEnd () {
	}

	protected void EndSkill () {
		fsm.ForceSetState ("Idle");
		OnSkillEnd ();
	}

	protected abstract bool SkillActivation {
		get;
	}
}
