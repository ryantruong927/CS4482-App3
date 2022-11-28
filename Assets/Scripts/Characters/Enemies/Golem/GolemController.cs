using Character.Enemy.Phase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character.Enemy.Boss {
	public class GolemController : BossController {
		public override void StartCombat() {
			base.StartCombat();

			gameObject.AddComponent<GolemPhase1>();
			currentPhase = GetComponent<GolemPhase1>();
			currentPhase.Init(this, player);
		}
	}
}
