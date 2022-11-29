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
		public float shieldDistance = 1.5f;

		private void Start() {
			state = State.FIRING;
		}

		private void FixedUpdate() {
			enemyPos = rb.position;
			playerPos = playerTransform.position;

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
				case State.SHIELDING:

					break;
				case State.RETREATING:
					Retreat();

					break;
			}
		}

		private bool CheckDistance() {
			return Vector2.Distance(enemyPos, playerPos) >= shieldDistance;
		}

		private void Retreat() {
			float retreatDirection = enemyPos.x > playerPos.x ? 1f : -1f;

			if (Vector2.Distance(enemyPos, playerPos) >= enemy.retreatDistance) {
				state = State.FIRING;
			}
			else {
				Vector2 velocity = rb.velocity;
				velocity.x = speed * enemy.retreatMultiplier * retreatDirection;
				rb.velocity = velocity;
			}
		}


		public override void NextPhase() {
		}

		public override void PrevPhase() {
		}
	}
}
