using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character {
    public class Hitbox : MonoBehaviour {
        private string owner;

        private void Start() {
            owner = transform.parent.tag;
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            Debug.Log(collision);
			if (!collision.CompareTag(owner))
                collision.GetComponent<CharacterController>().Hit(1);
        }
    }
}
