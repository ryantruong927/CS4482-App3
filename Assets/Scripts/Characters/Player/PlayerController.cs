using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character.Player {
	public class PlayerController : CharacterController {
		private float input;

		protected override void Start() {
			base.Start();
		}

		protected override void Update() {
			base.Update();
			Vector2 velocity = rb.velocity;

			input = Input.GetAxisRaw("Horizontal");

			if (input != 0)
				sr.flipX = input == -1;

			anim.SetBool("IsGrounded", isGrounded);
			if (isGrounded) {
				velocity.x = speed * input;

				if (Input.GetButton("Sprint")) {
					velocity.x *= sprintMultiplier;
					anim.SetBool("IsSprinting", true);
				}
				else
					anim.SetBool("IsSprinting", false);

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

			anim.SetFloat("Speed X", Mathf.Abs(velocity.x));
			anim.SetFloat("Speed Y", velocity.y);
			rb.velocity = velocity;
		}
	}
}
