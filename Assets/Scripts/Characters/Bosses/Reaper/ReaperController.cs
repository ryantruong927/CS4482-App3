using Character.Enemy.Phase;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character.Enemy.Boss {
	public class ReaperController : BossController {
		public override void StartCombat() {
			base.StartCombat();
		}

		public void Materialize() {
			gameObject.AddComponent<ReaperPhase1>();
			currentPhase = GetComponent<ReaperPhase1>();
			currentPhase.Init(this, player);
		}

		public void NextPhase(Type nextPhase) {
			gameObject.AddComponent(nextPhase);
			currentPhase = (AbstractPhase)GetComponent(nextPhase);
			currentPhase.Init(this, player);
		}
	}
}
