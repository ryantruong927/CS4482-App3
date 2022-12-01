using Character.Enemy.Phase;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character.Enemy.Boss {
	public class ReaperController : BossController {
		public GameObject summonPrefab;

		public override void StartCombat() {
			base.StartCombat();
		}

		public void Materialize() {
			gameObject.AddComponent<ReaperPhase1>();
			currentPhase = GetComponent<ReaperPhase1>();
			currentPhase.Init(this, player);
			isInvincible = false;
		}

		public void Summon() {
			Vector3 pos = transform.position;
			pos.x += lookDirection;
			pos.y += 0.5f;

			GameObject summon = Instantiate(summonPrefab, pos, Quaternion.identity);
			summon.GetComponent<SummonController>().Follow(player);
		}
	}
}
