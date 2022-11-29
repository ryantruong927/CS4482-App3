using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character.Player {
	public class PlayerController : CharacterController {
		private struct PowerUpInfo {
			public bool hasDash;
		}
		private PowerUpInfo powerUpInfo;

		private float input;

		private bool isDashing = false;
		private bool hasDashed = false;
		public float dashTime = 0.25f;
		private float dashTimer;
		public float dashMultiplier = 1.5f;

		protected override void Start() {
			base.Start();
		}

		protected override void Update() {
			base.Update();

			Vector2 velocity = Vector2.zero;

			if (isAttacking) {
			}
			else {
				if (isDashing) {
					dashTimer -= Time.deltaTime;

					if (dashTimer <= 0) {
						rb.gravityScale = 1;
						isDashing = false;
						velocity = Vector2.zero;
					}
					else {
						velocity.x = speed * dashMultiplier * lookDirection;
						velocity.y = 0;
					}

					rb.velocity = velocity;
				}
				else if (Input.GetButtonDown("Attack")) {
					isAttacking = true;
					anim.SetBool("IsAttacking", true);
				}
				else {
					input = Input.GetAxisRaw("Horizontal");

					if (input != 0) {
						lookDirection = input;
						sr.flipX = lookDirection == -1;
					}

					velocity = rb.velocity;

					anim.SetBool("IsGrounded", isGrounded);
					if (isGrounded) {
						velocity.x = speed * input;

						if (Input.GetButton("Crouch")) {
							velocity.x *= crouchMultiplier;
							isCrouching = true;
						}
						else {
							isCrouching = false;

							if (Input.GetButton("Sprint")) {
								velocity.x *= sprintMultiplier;
								isSprinting = true;
							}
							else
								isSprinting = false;
						}

						isJumping = false;
						hasCancelledJump = false;

						if (Input.GetButtonDown("Jump")) {
							velocity.y = maxJumpForce;
							isJumping = true;
						}

						hasDashed = false;
					}
					else {
						if (velocity.x == 0)
							velocity.x = speed * jumpSpeedMultiplier * input;
						else if (velocity.x > 0 && input < 0)
							velocity.x = -speed * jumpSpeedMultiplier;
						else if (velocity.x < 0 && input > 0)
							velocity.x = speed * jumpSpeedMultiplier;
					}


					if (Input.GetButtonDown("Dash") && powerUpInfo.hasDash && !hasDashed) {
						rb.gravityScale = 0;
						isDashing = true;
						hasDashed = true;
						dashTimer = dashTime;
						velocity.x += speed * dashMultiplier * lookDirection;
						velocity.y = 0;
					}
					else if (Input.GetButtonUp("Jump") && !hasCancelledJump && velocity.y > 0) {
						isJumping = false;
						hasCancelledJump = true;

						velocity.y = velocity.y >= maxJumpForce ? minJumpForce : 0;
					}

					rb.velocity = velocity;

					anim.SetFloat("Speed X", Mathf.Abs(velocity.x));
					anim.SetFloat("Speed Y", velocity.y);
					anim.SetBool("IsSprinting", isSprinting);
					anim.SetBool("IsCrouching", isCrouching);
					anim.SetBool("IsDashing", isDashing);
				}
			}
		}

		public override void Hit(int amount) {
			if (!isDashing)
				base.Hit(amount);
		}

		public bool PowerUp(string powerUp) {
			switch (powerUp) {
				case "Dash":
					powerUpInfo.hasDash = true;
					return true;
				default:
					return false;
			}
		}
	}
}
