using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character {
	public class PlayerController : CharacterController {
		private bool isJumping;
		private float input;
		public float maxJumpHeight = 3f;
		public float minJumpHeight = 0.5f;
		private float maxJumpSpeed;

		protected override void Start() {
			base.Start();

			maxJumpSpeed = Mathf.Sqrt(2 * -gravity * maxJumpHeight);
		}

		protected override void Update() {
			base.Update();

			input = Input.GetAxisRaw("Horizontal");

			if (Input.GetButtonDown("Jump"))
				isJumping = true;
			else if (Input.GetButtonUp("Jump"))
				isJumping = false;
		}

		protected override void FixedUpdate() {
			base.FixedUpdate();

			velocity.x += speed * input * Time.deltaTime;

			if (isJumping)
				velocity.y += maxJumpSpeed * Time.deltaTime;
			//else
			//	velocity.y += gravity * Time.deltaTime;

			rb.MovePosition(pos + velocity);
		}
	}
}
