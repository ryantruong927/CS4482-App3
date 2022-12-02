using Character.Enemy.Phase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Character.Enemy.Boss {
	public class GolemController : BossController {
		public GameObject projectilePrefab;

		public float xLeft = -7.625f;
		public float xMiddle = 0.375f;
		public float xRight = 8.375f;
		private int xCurr = 2;

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
			isInvincible = false;

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

		public void Teleport() {
			int xNum = Random.Range(1, 4);

			while (xNum == xCurr)
				xNum = Random.Range(1, 4);

			xCurr = xNum;

			switch (xNum) {
				case 1:
					transform.localPosition = new Vector2(xLeft, transform.localPosition.y);
					break;
				case 2:
					transform.localPosition = new Vector2(xMiddle, transform.localPosition.y);
					break;
				case 3:
					transform.localPosition = new Vector2(xRight, transform.localPosition.y);
					break;
			}

			anim.SetBool("IsTeleporting", false);
		}
	}
}
