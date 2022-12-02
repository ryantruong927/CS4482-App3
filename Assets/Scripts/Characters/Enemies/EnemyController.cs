using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character.Enemy {
	public class EnemyController : CharacterController {
		public bool isPatrolling;
		protected bool inCombat = false;
		public float retreatMultiplier = 2f;
		public float retreatDistance = 4f;

		protected Vector2 lastSeenPosition;
		protected bool hasSeenPlayer;
		protected bool isChasing;
		public float chaseSpeed = 2.5f;
		public float chaseDistance = 10f;
		public float chaseTime = 2;
		protected float chaseTimer;

		public float changeTime = 2;
		protected float changeTimer;

		protected override void Start() {
			base.Start();

			lookDirection = sr.flipX ? -1f : 1f;
			changeTimer = changeTime;
		}

		protected override void Update() {
			base.Update();

			if (isHit) {
				hitTimer -= Time.deltaTime;

				if (hitTimer < 0) {
					sr.material = material;
					isHit = false;
				}
			}
			
			if (isAttacking)
				rb.velocity = new Vector2(0, rb.velocity.y);
			else {
				if (isChasing) {
					float distance = Vector2.Distance(rb.position, lastSeenPosition);
					if (distance <= hitboxCollider.size.x + 0.5f) {
						if (hasSeenPlayer) {
							lookDirection = lastSeenPosition.x <= rb.position.x ? -1 : 1;
							isAttacking = true;
							anim.SetBool("IsAttacking", true);
							hitboxTransform.localPosition = new Vector2(hitboxCollider.size.x * lookDirection, 0);
						}
						else {
							isChasing = false;
							changeTimer = changeTime;
						}
					}
					else if (distance < chaseDistance) {
						lookDirection = lastSeenPosition.x <= rb.position.x ? -1 : 1;

						rb.velocity = new Vector2(chaseSpeed * lookDirection, rb.velocity.y);
					}
					else {
						isChasing = false;
						changeTimer = changeTime;
					}
				}
				else if (isPatrolling) {
					Patrol();
				}
			}

			sr.flipX = lookDirection == -1f;
			anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
		}

		public virtual void StartCombat() {
			inCombat = true;
			anim.SetBool("InCombat", true);
		}

		protected void Patrol() {
			changeTimer -= Time.deltaTime;

			if (changeTimer < 0) {
				changeTimer = changeTime;
				lookDirection *= -1;
			}

			rb.velocity = new Vector2(speed * lookDirection, rb.velocity.y);
		}

		public void Chase(Vector2 playerPos) {
			Vector2 pos = rb.position;
			lastSeenPosition = playerPos;

			if (!isChasing) {
				if ((lookDirection == 1f && lastSeenPosition.x >= rb.position.x) || (lookDirection == -1f && lastSeenPosition.x <= rb.position.x)) {
					isChasing = true;
				}
				else {
					isChasing = false;
				}
			}
		}

		public void HasSeenPlayer(bool hasSeenPlayer) {
			this.hasSeenPlayer = hasSeenPlayer;
		}
	}
}
