using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character {
    public class Hitbox : MonoBehaviour {
        private string owner;
        public int damage = 1;

        private void Start() {
            owner = transform.parent.tag;
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            CharacterController character = collision.GetComponent<CharacterController>();
			if (character != null && !collision.CompareTag(owner))
                character.Hit(damage);
        }
    }
}
