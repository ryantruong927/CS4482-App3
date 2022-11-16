using Character.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PowerUp {
	[RequireComponent(typeof(CircleCollider2D))]
	public class PowerUp : MonoBehaviour {
		public string powerUp;

		protected void OnTriggerEnter2D(Collider2D collision) {
			if (collision.GetComponent<PlayerController>().PowerUp(powerUp))
				Destroy(gameObject);
		}

	}
}
