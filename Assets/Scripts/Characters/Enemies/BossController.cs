using Character.Enemy.Phase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character.Enemy.Boss {
	public class BossController : EnemyController {
		protected Transform player;
		protected AbstractPhase currentPhase;

		protected override void Start() {
			base.Start();

			player = GameObject.FindGameObjectWithTag("Player").transform;
		}

		protected override void Update() {
			if (!isAttacking)
				sr.flipX = rb.position.x > player.position.x;
		}
	}
}
