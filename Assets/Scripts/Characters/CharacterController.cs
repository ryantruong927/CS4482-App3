using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character {
	[RequireComponent(typeof(Rigidbody2D))]
	[RequireComponent(typeof(BoxCollider2D))]
	public class CharacterController : MonoBehaviour {
		protected Rigidbody2D rb;
		protected BoxCollider2D boxCollider2D;

		public float speed = 6f;
		protected bool isSprinting = false;
		public float sprintMultiplier = 1.2f;

		protected bool isGrounded = true;
		protected float gravity;
		protected bool isJumping, hasCancelledJump;
		public float jumpSpeedMultiplier = 0.9f;
		public float maxJumpHeight = 3f;
		public float minJumpHeight = 0.5f;
		protected float maxJumpForce, minJumpForce; // vf^2 = sqrt(2 * vi^2 * a * d)

		protected virtual void Start() {
			rb = GetComponent<Rigidbody2D>();
			boxCollider2D = GetComponent<BoxCollider2D>();

			gravity = Physics2D.gravity.y;
			//gravity = Manager.GameManager.gravity;
		}

		protected virtual void Update() {
			isGrounded = CheckIfGrounded();
			maxJumpForce = Mathf.Sqrt(2 * -gravity * maxJumpHeight);
			minJumpForce = Mathf.Sqrt(2 * -gravity * minJumpHeight);
		}

		private bool CheckIfGrounded() {
			return Physics2D.OverlapBox(new Vector2(rb.position.x, rb.position.y + boxCollider2D.offset.y - boxCollider2D.size.y * 0.5f), new Vector2(boxCollider2D.size.x, 0.125f), default, 1 << LayerMask.NameToLayer("Ground"));
		}

		private void OnDrawGizmos() {
			if (rb == null)
				rb = GetComponent<Rigidbody2D>();
			if (boxCollider2D == null)
				boxCollider2D = GetComponent<BoxCollider2D>();

			Gizmos.DrawWireCube(new Vector2(rb.position.x, rb.position.y + boxCollider2D.offset.y - boxCollider2D.size.y * 0.5f), new Vector2(boxCollider2D.size.x, 0.125f));
		}
	}
}
