using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongYa : BaseSkill {
	public const float JudgePower = 1.4f;
	protected override int MaxCoolDown {
		get {
			return 180;
		}
	}
	protected override bool SkillActivation {
		get {
			return Input.GetKeyDown (KeyCode.R);
		}
	}
	protected override string Name {
		get {
			return "LongYa";
		}
	}

	protected override void OnSkillBegin () {
		EnterCD ();
	}

	protected override void DuringSkill (int frame) {
		if (frame < 5)
			LocalCommandBuffers.AddMessage (RightHandTranslation.MOVE, new object[]{ Vector3.forward / 2 });
		else
			LocalCommandBuffers.AddMessage (RightHandTranslation.MOVE, new object[]{ -Vector3.forward / 6 });
		if (frame == 20)
			EndSkill ();
	}

	protected override void OnSkillEnd () {
		LocalCommandBuffers.AddMessage (RightHandTranslation.GOBACK, null);
	}
}
