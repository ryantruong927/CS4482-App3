using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character {
	public class PlayerController : CharacterController {
		private float input;

		protected override void Start() {
			base.Start();
		}

		protected override void Update() {
			base.Update();
			Vector2 velocity = rb.velocity;

			input = Input.GetAxisRaw("Horizontal");

			if (isGrounded) {
				velocity.x = speed * input;

				if (Input.GetButton("Sprint"))
					velocity.x *= sprintMultiplier;

				isJumping = false;
				hasCancelledJump = false;

				if (Input.GetButtonDown("Jump")) {
					velocity.y = maxJumpForce;
					isJumping = true;
				}
			}
			else {
				if (velocity.x == 0)
					velocity.x = speed * jumpSpeedMultiplier * input;
				else if (velocity.x > 0 && input < 0)
					velocity.x = -speed * jumpSpeedMultiplier;
				else if (velocity.x < 0 && input > 0)
					velocity.x = speed * jumpSpeedMultiplier;
			}

			if (Input.GetButtonUp("Jump") && !hasCancelledJump && velocity.y > 0) {
				isJumping = false;
				hasCancelledJump = true;

				velocity.y = velocity.y >= maxJumpForce ? minJumpForce : 0;
			}

			rb.velocity = velocity;
		}
	}
}
