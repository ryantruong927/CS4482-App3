using Character.Enemy.Phase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character.Enemy {
	public class Boss : EnemyController {
		private Transform player;
		public AbstractPhase currentPhase;

		protected override void Start() {
			base.Start();

			player = GameObject.FindGameObjectWithTag("Player").transform;
		}

		protected override void Update() {
			sr.flipX = rb.position.x > player.position.x;
		}

		public override void StartCombat() {
			base.StartCombat();

			gameObject.AddComponent<Boss1Phase1>();
			currentPhase = GetComponent<Boss1Phase1>();
			currentPhase.Init(this, player);
		}
	}
}
