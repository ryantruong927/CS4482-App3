using Character.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character.Enemy {
	public class SummonController : EnemyController {
		private Transform player;
		private bool isFollowing = false;

		private void FixedUpdate() {
			if (isFollowing) {
				Vector2 pos = rb.position;
				Vector2 playerPos = player.position;
				Vector2 movePos = Vector2.MoveTowards(pos, playerPos, speed * Time.deltaTime);
				rb.MovePosition(movePos);
			}
		}

		public void Follow(Transform player) {
			this.player = player;
			isFollowing = true;
		}

		public override void Hit(int amount) {
			Destroy(gameObject);
		}

		protected void OnTriggerEnter2D(Collider2D collision) {
			if (collision.CompareTag("Player")) {
				PlayerController player = collision.GetComponent<PlayerController>();

				if (player != null) {
					player.Hit(1);
					Destroy(gameObject);
				}
			}
		}
	}
}
