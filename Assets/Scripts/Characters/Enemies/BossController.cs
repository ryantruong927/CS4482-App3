using Character.Enemy.Phase;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character.Enemy.Boss {
	public class BossController : EnemyController {
		protected Transform player;
		protected AbstractPhase currentPhase;
		protected Type nextPhase;
		public GameObject gemPrefab;

		protected bool isInvincible = true;

		protected override void Start() {
			base.Start();

			player = GameObject.FindGameObjectWithTag("Player").transform;
		}

		protected override void Update() {
			if (isHit) {
				hitTimer -= Time.deltaTime;

				if (hitTimer < 0) {
					sr.material = material;
					isHit = false;
				}
			}

			if (inCombat && !anim.GetBool("IsAttacking")) {
				sr.flipX = rb.position.x > player.position.x;
				lookDirection = sr.flipX ? -1f : 1f;
				hitboxTransform.localPosition = new Vector2(hitboxCollider.offset.x + hitboxCollider.size.x * lookDirection, 0);
			}
		}

		public override void Hit(int amount) {
			if (!isInvincible) {
				if (CurrentHealth - amount <= 0) {
					Destroy(currentPhase);
					DropGem();
				}
				base.Hit(amount);
			}
		}

		private void DropGem() {
			GameObject gem = Instantiate(gemPrefab, rb.position, Quaternion.identity);
		}

		public void NextPhase(Type nextPhase) {
			isInvincible = true;
			Destroy(currentPhase);
			this.nextPhase = nextPhase;
			anim.SetBool("IsChangingPhase", true);
		}

		public void SwitchPhase() {
			gameObject.AddComponent(nextPhase);
			currentPhase = (AbstractPhase)GetComponent(nextPhase);
			currentPhase.Init(this, player);
			anim.SetBool("IsChangingPhase", false);
			isInvincible = false;
		}
	}
}

