using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpear : StateMachine {
	protected override void InitializeStates () {
		RegisterState ("Loaded");
		RegisterState ("Idle");
	}

	protected override void InitializeTransitions () {
		RegisterTransition ("Loaded", () => {
			return "Idle";
		});
	}

	protected override string InitialState {
		get {
			return "Loaded";
		}
	}
}
