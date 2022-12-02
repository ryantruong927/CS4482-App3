using Character.Enemy.Boss;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character.Enemy.Phase {
	public class GolemPhase1 : AbstractPhase {
		private enum State {
			FIRING,
			FIRED,
			SHIELDING,
			RETREATING,
		}
		private State state;

		private Vector2 enemyPos, playerPos;
		private float cooldownTimer;
		public float fireDistance = 100f;

		private void Start() {
			state = State.FIRING;
		}

		private void FixedUpdate() {
			enemyPos = rb.position;
			playerPos = playerTransform.position;

			if (enemy.CurrentHealth <= enemy.maxHealth / 2) {
				((GolemController)enemy).NextPhase(typeof(GolemPhase2));
			}

			switch (state) {
				case State.FIRING:
					if (CheckDistance()) {
						anim.SetTrigger("IsFiring");
						cooldownTimer = ((GolemController)enemy).fireCooldown;
						state = State.FIRED;
					}

					break;
				case State.FIRED:
					cooldownTimer -= Time.deltaTime;

					if (cooldownTimer < 0)
						state = State.FIRING;
					break;
			}
		}

		private bool CheckDistance() {
			return Vector2.Distance(enemyPos, playerPos) <= fireDistance;
		}
	}
}
