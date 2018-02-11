using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearPullback : BaseSkill {
	protected override int MaxCoolDown {
		get {
			return 16;
		}
	}

	protected override string Name {
		get {
			return "Pullback";
		}
	}

	protected override void OnSkillBegin () {
		EnterCD ();
		LocalCommandBuffers.AddMessage (RightHandTranslation.ROTBACK, new object[]{ 16 });
	}

	protected override bool SkillActivation {
		get {
			return Input.GetKeyDown (KeyCode.E);
		}
	}

	protected override void DuringSkill (int frame) {
		if (frame == 16)
			EndSkill ();
	}
}
