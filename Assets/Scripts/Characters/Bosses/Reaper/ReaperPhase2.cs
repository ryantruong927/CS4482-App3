using Character.Enemy.Boss;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character.Enemy.Phase {
	public class ReaperPhase2 : AbstractPhase {
		private enum State {
			CHASING,
			ATTACKING,
			RETREATING,
			SUMMONING
		}
		private State state;

		float retreatDirection;
		private bool willSummon = false;
		private int summonNum;
		private const float summonTime = 1f;
		private float summonTimer;
		private Vector2 enemyPos, playerPos;

		private void Start() {
			state = State.SUMMONING;
			summonNum = 5;
			anim.SetBool("IsSummoning", true);
		}

		private void FixedUpdate() {
			enemyPos = rb.position;
			playerPos = playerTransform.position;

			switch (state) {
				case State.CHASING:
					Chase();

					if (IsPlayerWithinDistance(2f)) {
						state = State.ATTACKING;
						anim.SetBool("IsAttacking", true);
					}

					break;
				case State.ATTACKING:
					if (!anim.GetBool("IsAttacking")) {
						if (IsPlayerWithinDistance(enemy.retreatDistance)) {
							willSummon = true;
							retreatDirection = enemyPos.x > playerPos.x ? 1f : -1f;
							state = State.RETREATING;
						}
						else
							state = State.CHASING;
					}

					break;
				case State.RETREATING:
					anim.SetBool("IsRetreating", true);
					Retreat();

					break;
				case State.SUMMONING:
					summonTimer -= Time.deltaTime;

					if (summonTimer < 0) {
						if (summonNum > 0) {
							((ReaperController)enemy).Summon();
							summonTimer = summonTime;
							summonNum--;
						}
						else {
							state = State.CHASING;
							anim.SetBool("IsSummoning", false);
						}
					}

					break;
			}
		}

		private bool IsPlayerWithinDistance(float distance) {
			return Vector2.Distance(enemyPos, playerPos) <= distance;
		}

		private void Chase() {
			Vector2 pos = Vector2.MoveTowards(enemyPos, playerPos, speed * Time.deltaTime);
			rb.MovePosition(pos);
		}

		private void Retreat() {
			if (!IsPlayerWithinDistance(enemy.retreatDistance)) {
				if (willSummon) {
					anim.SetBool("IsRetreating", false);
					rb.velocity = Vector2.zero;

					summonTimer = summonTime;
					state = State.SUMMONING;
					summonNum = UnityEngine.Random.Range(2, 7);
					anim.SetBool("IsSummoning", true);
				}
				else {
					anim.SetBool("IsRetreating", false);
					state = State.CHASING;
				}
			}
			else {
				Vector2 velocity = rb.velocity;
				velocity.x = speed * enemy.retreatMultiplier * retreatDirection;
				rb.velocity = velocity;
			}
		}
	}
}
