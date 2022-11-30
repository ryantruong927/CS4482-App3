using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character.Player {
	public class SpecialProjectile : Projectile {
		private Rigidbody2D rb;
		private CircleCollider2D circleCollider2D;
		private Transform enemy;

		public float projectileSpeed = 10f;
		private const float radius = 0.5625f;
		private bool isLooking = false;
		public float lookRadius = 15f;
		public float lookTime = 1f;
		private float lookTimer;

		private void Start() {
			rb = GetComponent<Rigidbody2D>();
			circleCollider2D = GetComponent<CircleCollider2D>();
		}

		private void FixedUpdate() {
			if (lookTimer < lookTime)
				lookTimer += Time.deltaTime;
			else if (!isLooking) {
				Vector2 pos = Vector2.MoveTowards(rb.position, enemy.position, projectileSpeed * Time.deltaTime);
				rb.MovePosition(pos);
			}
		}

		public void LookForEnemies() {
			lookTimer = 0;
			isLooking = true;
			gameObject.SetActive(true);

			if (circleCollider2D == null)
				circleCollider2D = GetComponent<CircleCollider2D>();

			circleCollider2D.radius = lookRadius;
		}

		protected override void OnTriggerEnter2D(Collider2D collision) {
			if (collision.CompareTag("Enemy")) {
				enemy = collision.GetComponent<Enemy.EnemyController>().transform;

				if (enemy != null) {
					if (isLooking) {
						circleCollider2D.radius = radius;
						isLooking = false;
					}
					else if (lookTimer > lookTime) {
						Debug.Log("Hit!");
						enemy.GetComponent<Enemy.EnemyController>().Hit(damage);
						gameObject.SetActive(false);
					}
				}
			}
		}
	}
}
