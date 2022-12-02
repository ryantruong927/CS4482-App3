using Character.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character.PowerUp {
	[RequireComponent(typeof(CircleCollider2D))]
	public class PowerUp : MonoBehaviour {
		public string powerUp;

		protected virtual void OnTriggerStay2D(Collider2D collision) {
			if (collision.CompareTag("Player") && collision.GetComponent<PlayerController>().PowerUp(powerUp))
				Destroy(gameObject);
		}
	}
}
