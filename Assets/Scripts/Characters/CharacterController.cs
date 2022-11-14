using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character {
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent (typeof(BoxCollider2D))]
    public class CharacterController : MonoBehaviour {
        protected Rigidbody2D rb;
        protected BoxCollider2D boxCollider2D;

        protected Vector2 pos, velocity;
        protected bool isGrounded = true;
        public float speed = 6f;
        protected float gravity;

        protected virtual void Start() {
            rb = GetComponent<Rigidbody2D>();
            boxCollider2D = GetComponent<BoxCollider2D>();

            velocity = Vector2.zero;
            gravity = -9.81f;
			//gravity = Manager.GameManager.gravity;
		}

        protected virtual void Update() {

		}

		protected virtual void FixedUpdate() {
            pos = rb.position;
            velocity.y += gravity * Time.deltaTime;
		}
	}
}
