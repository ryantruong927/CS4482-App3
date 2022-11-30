using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character {
    public class Projectile : MonoBehaviour {
        [HideInInspector]
        public string owner;
        public int damage;

        protected virtual void OnTriggerEnter2D(Collider2D collision) {
            if (!collision.CompareTag(owner)) {
                CharacterController character = collision.GetComponent<CharacterController>();

                if (character != null)
                    character.Hit(damage);
                else
                    Destroy(gameObject);
            }
        }
    }
}
