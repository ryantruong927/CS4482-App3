using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character.Enemy {
    public class EnemyController : CharacterController {
        public bool isPatrolling;
        protected bool inCombat = false;
        public float retreatMultiplier = 2f;
        public float retreatDistance = 4f;

        protected override void Update() {
            base.Update();

            if (isPatrolling) {
                Patrol();
            }
        }

        public virtual void StartCombat() {
            inCombat = true;
            anim.SetBool("InCombat", true);
		}

        protected void Patrol() {

        }
	}
}
