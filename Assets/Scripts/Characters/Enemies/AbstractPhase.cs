using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character.Enemy {
    public abstract class AbstractPhase : MonoBehaviour {
        protected Animator anim;
        protected Rigidbody2D rb;
        protected EnemyController enemy;
        protected Transform enemyTransform, playerTransform;

        protected float speed;

        public abstract void PrevPhase();
        public abstract void NextPhase();
		public void Init(EnemyController enemy, Transform playerTransform) {
            this.enemy = enemy;
            this.playerTransform = playerTransform;

            anim = enemy.GetComponent<Animator>();
			rb = enemy.GetComponent<Rigidbody2D>();

			speed = enemy.speed;
		}
	}
}
