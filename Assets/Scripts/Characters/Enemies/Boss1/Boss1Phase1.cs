using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character.Enemy.Phase {
	public class Boss1Phase1 : AbstractPhase {
		private enum State {
			CHASING,
			ATTACKING,
			RETREATING,
			REGENERATING
		}
		private State state;

		private bool willRegenerate = false;
		private const float regenerateTime = 1f;
		private float regenerateTimer;
		private Vector2 enemyPos, playerPos;

		private void Start() {
			state = State.CHASING;
		}

		private void FixedUpdate() {
			enemyPos = rb.position;
			playerPos = playerTransform.position;

			switch (state) {
				case State.CHASING:
					Chase();
					CheckDistance();

					break;
				case State.ATTACKING:
					if (Attack()) {
						willRegenerate = true;
						state = State.RETREATING;
						Retreat();
					}
					else
						Retreat();

					break;
				case State.RETREATING:
					Retreat();

					break;
				case State.REGENERATING:
					regenerateTimer -= Time.deltaTime;

					if (regenerateTimer <= 0)
						state = State.CHASING;

					break;
			}
		}

		private void CheckDistance() {
			if (Vector2.Distance(enemyPos, playerPos) >= 1f)
				state = State.CHASING;
			else
				state = State.ATTACKING;
		}

		private void Chase() {
			Vector2 pos = Vector2.MoveTowards(enemyPos, playerPos, speed * Time.deltaTime);
			rb.MovePosition(pos);
		}

		private void Retreat() {
			float retreatDirection = enemyPos.x > playerPos.x ? 1f : -1f;

			if (Vector2.Distance(enemyPos, playerPos) >= enemy.retreatDistance) {
				if (willRegenerate) {
					rb.velocity = Vector2.zero;
					willRegenerate = false;
					Regenerate();
				}
				else
					state = State.CHASING;
			}
			else {
				Vector2 velocity = rb.velocity;
				velocity.x = speed * enemy.retreatMultiplier * retreatDirection;
				rb.velocity = velocity;
			}
		}

		private bool Attack() {
			return enemy.Attack();
		}

		private void Regenerate() {
			state = State.REGENERATING;
			regenerateTimer = regenerateTime;
			enemy.Heal(1);
		}

		public override void NextPhase() {
		}

		public override void PrevPhase() {
		}
	}
}
