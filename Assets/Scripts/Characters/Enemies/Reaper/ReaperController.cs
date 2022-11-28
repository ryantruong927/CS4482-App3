using Character.Enemy.Phase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character.Enemy.Boss {
	public class ReaperController : BossController {
		public override void StartCombat() {
			base.StartCombat();

			gameObject.AddComponent<ReaperPhase1>();
			currentPhase = GetComponent<ReaperPhase1>();
			currentPhase.Init(this, player);
		}
	}
}
