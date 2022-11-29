using Character.Enemy.Phase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Character.Enemy.Boss {
	public class GolemController : BossController {
		public GameObject projectilePrefab;

		public float projectileForce = 400f;
		public float fireCooldown = 2f;

		protected override void Start() {
			base.Start();

			anim.enabled = false;
		}

		public override void StartCombat() {
			base.StartCombat();

			gameObject.AddComponent<GolemPhase1>();
			currentPhase = GetComponent<GolemPhase1>();
			currentPhase.Init(this, player);

			anim.enabled = true;
		}

		public void Fire() {
			Vector3 pos = transform.position;
			pos.x += lookDirection;
			Vector3 direction = (player.position - pos).normalized;
			float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
			Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

			GameObject projectile = Instantiate(projectilePrefab, pos, rotation);
			projectile.GetComponent<Projectile>().owner = tag;
			projectile.GetComponent<Rigidbody2D>().AddForce(direction * projectileForce);
		}
	}
}
