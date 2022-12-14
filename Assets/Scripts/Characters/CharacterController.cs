using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character {
	[RequireComponent(typeof(SpriteRenderer))]
	[RequireComponent(typeof(Animator))]
	[RequireComponent(typeof(Rigidbody2D))]
	[RequireComponent(typeof(BoxCollider2D))]
	public class CharacterController : MonoBehaviour {
		protected SpriteRenderer sr;
		protected Animator anim;
		protected Rigidbody2D rb;
		protected BoxCollider2D boxCollider2D, hitboxCollider;
		protected Transform hitboxTransform;
		protected Material material;
		public Material HitMaterial;

		public int maxHealth;
		public int CurrentHealth { get; protected set; }

		protected float lookDirection = 1f;
		public float speed = 3f;
		protected bool isSprinting = false;
		public float sprintMultiplier = 1.5f;

		protected bool isCrouching = false;
		public float crouchMultiplier = 0.5f;

		protected bool isGrounded = true;
		protected float gravity;
		protected bool isJumping, hasCancelledJump;
		public float jumpSpeedMultiplier = 0.9f;
		public float maxJumpHeight = 2f;
		public float minJumpHeight = 0.5f;
		protected float maxJumpForce, minJumpForce; // vf^2 = sqrt(2 * vi^2 * a * d)

		protected bool isAttacking = false;
		public float attackTime = 0.2f;
		protected float attackTimer;

		protected float hitTime = 0.2f;
		protected float hitTimer;
		protected bool isHit = false;

		protected virtual void Start() {
			sr = GetComponent<SpriteRenderer>();
			anim = GetComponent<Animator>();
			rb = GetComponent<Rigidbody2D>();
			boxCollider2D = GetComponent<BoxCollider2D>();
			if (transform.childCount > 0) {
				hitboxTransform = transform.GetChild(0).transform;
				hitboxCollider = hitboxTransform.GetComponent<BoxCollider2D>();
			}
			material = sr.material;

			CurrentHealth = maxHealth;
			gravity = Physics2D.gravity.y;
			maxJumpForce = Mathf.Sqrt(2 * -gravity * maxJumpHeight);
			minJumpForce = Mathf.Sqrt(2 * -gravity * minJumpHeight);
		}

		protected virtual void Update() {
			isGrounded = IsGrounded();
		}

		protected bool IsGrounded() {
			return Physics2D.OverlapBox(new Vector2(rb.position.x + boxCollider2D.offset.x, rb.position.y + boxCollider2D.offset.y - boxCollider2D.size.y * 0.5f), new Vector2(boxCollider2D.size.x * 0.98f, 0.125f), default, 1 << LayerMask.NameToLayer("Ground"));
		}

		public virtual void EndAttack() {
			isAttacking = false;
			anim.SetBool("IsAttacking", false);
		}

		public virtual void Hit(int amount) {
			CurrentHealth -= amount;

			if (CurrentHealth <= 0) {
				rb.velocity = new Vector2(0, rb.velocity.y);
				sr.material = material;
				anim.SetBool("IsDead", true);
				Destroy(hitboxTransform.gameObject);
				Destroy(this);
			}
			else {
				sr.material = HitMaterial;
				hitTimer = hitTime;
				isHit = true;
				anim.SetTrigger("Hit");
			}
		}

		public virtual void Heal(int amount) {
			CurrentHealth = Mathf.Clamp(CurrentHealth + amount, 0, maxHealth);
		}

		private void OnDrawGizmos() {
			if (rb == null)
				rb = GetComponent<Rigidbody2D>();
			if (boxCollider2D == null)
				boxCollider2D = GetComponent<BoxCollider2D>();

			Gizmos.DrawWireCube(new Vector2(rb.position.x + boxCollider2D.offset.x, rb.position.y + boxCollider2D.offset.y - boxCollider2D.size.y * 0.5f), new Vector2(boxCollider2D.size.x * 0.99f, 0.125f));
		}
	}
}
