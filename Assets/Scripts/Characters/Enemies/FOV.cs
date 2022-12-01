using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character.Enemy {
    public class FOV : MonoBehaviour {
        private EnemyController enemy;

        private void Start() {
            enemy = GetComponentInParent<EnemyController>();
        }

        private void OnTriggerStay2D(Collider2D collision) {
            enemy.Chase(collision.transform.position);
            enemy.HasSeenPlayer(true);
		}

		private void OnTriggerExit2D(Collider2D collision) {
			enemy.Chase(collision.transform.position);
			enemy.HasSeenPlayer(false);
		}
	}
}
