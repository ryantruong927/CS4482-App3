using Character.Enemy.Boss;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character.Enemy.Phase {
	public class ReaperPhase1 : AbstractPhase {
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

			if (enemy.CurrentHealth < enemy.maxHealth / 2)
				((ReaperController)enemy).NextPhase(typeof(ReaperPhase2));

			switch (state) {
				case State.CHASING:
					Chase();
					CheckDistance();

					break;
				case State.ATTACKING:
					if (!anim.GetBool("IsAttacking")) {
						if (true) {
							willRegenerate = true;
							state = State.RETREATING;
							Retreat();
						}
						else
							Retreat();
					}

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
			if (Vector2.Distance(enemyPos, playerPos) >= 2f)
				state = State.CHASING;
			else {
				state = State.ATTACKING;
				anim.SetBool("IsAttacking", true);
			}
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

		private void Regenerate() {
			state = State.REGENERATING;
			regenerateTimer = regenerateTime;
			enemy.Heal(1);
		}
	}
}
