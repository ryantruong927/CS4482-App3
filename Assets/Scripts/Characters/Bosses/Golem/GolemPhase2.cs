using Character.Enemy.Boss;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character.Enemy.Phase {
	public class GolemPhase2 : AbstractPhase {
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
		private int fireNum = 3;
		public float fireDistance = 100f;

		private void Start() {
			state = State.FIRING;
		}

		private void FixedUpdate() {
			enemyPos = rb.position;
			playerPos = playerTransform.position;
			Debug.Log(fireNum);

			switch (state) {
				case State.FIRING:
					if (fireNum < 0) {
						fireNum = Random.Range(2, 6);

						anim.SetBool("IsTeleporting", true);
					}
					else if (CheckDistance()) {
						anim.SetTrigger("IsFiring");
						cooldownTimer = ((GolemController)enemy).fireCooldown;
						state = State.FIRED;
					}

					break;
				case State.FIRED:
					cooldownTimer -= Time.deltaTime;

					if (cooldownTimer < 0) {
						fireNum--;
						state = State.FIRING;
					}
					break;
			}
		}

		private bool CheckDistance() {
			return Vector2.Distance(enemyPos, playerPos) <= fireDistance;
		}
	}
}
