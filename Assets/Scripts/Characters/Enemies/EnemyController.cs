using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character.Enemy {
    public class EnemyController : CharacterController {
        public bool isFacingLeft;
        protected bool inCombat = false;
        public float retreatMultiplier = 2f;
        public float retreatDistance = 4f;

		public virtual void StartCombat() {
            inCombat = true;
            anim.SetBool("InCombat", true);
		}
	}
}
