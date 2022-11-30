using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character.Player {
	public class PlayerController : CharacterController {
		private struct PowerUpInfo {
			public bool hasSpecial;
			public bool hasDash;
			public bool hasDJ;
		}
		private PowerUpInfo powerUpInfo;

		private PlayerHUD playerHUD;

		private float input;

		public SpecialProjectile projectile;
		public Vector2 projectilePos = new Vector2(-0.5f, -0.21875f);
		private bool isUsingSpecial = false;
		private bool isCharging = false;
		public float cooldownTime = 5f;
		private float cooldownTimer;

		private bool isDashing = false;
		private bool usedDash;
		public float dashTime = 0.25f;
		private float dashTimer;
		public float dashMultiplier = 1.5f;

		private bool usedDJ;

		public float pauseTime = 0.3f;
		public bool isPaused = false;

		protected override void Start() {
			base.Start();

			playerHUD = GetComponent<PlayerHUD>();
			projectile = transform.GetChild(1).GetComponent<SpecialProjectile>();
			projectile.owner = tag;
		}

		protected override void Update() {
			if (!isPaused) {
				base.Update();

				Vector2 velocity = Vector2.zero;

				if (isCharging) {
					cooldownTimer += Time.deltaTime;

					playerHUD.UpdateCharge(cooldownTimer / cooldownTime);

					if (cooldownTimer >= cooldownTime) {
						projectile.gameObject.SetActive(false);
						projectile.transform.localPosition = projectilePos;
						isCharging = false;
					}
				}

				if (!isAttacking && !isUsingSpecial) {
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
							else if (Input.GetButtonDown("Special") && powerUpInfo.hasSpecial && !isCharging) {
								isUsingSpecial = true;
								anim.SetBool("IsUsingSpecial", true);
							}

							usedDash = false;
							usedDJ = false;
						}
						else {
							if (velocity.x == 0)
								velocity.x = speed * jumpSpeedMultiplier * input;
							else if (velocity.x > 0 && input < 0)
								velocity.x = -speed * jumpSpeedMultiplier;
							else if (velocity.x < 0 && input > 0)
								velocity.x = speed * jumpSpeedMultiplier;

							if (Input.GetButtonDown("Jump") && powerUpInfo.hasDJ && !usedDJ) {
								velocity.y = maxJumpForce;
								usedDJ = true;
							}
						}


						if (Input.GetButtonDown("Dash") && powerUpInfo.hasDash && !usedDash) {
							rb.gravityScale = 0;
							isDashing = true;
							usedDash = true;
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

				hitboxTransform.localPosition = new Vector2(hitboxCollider.size.x * lookDirection, 0);
			}
		}

		public void Fire() {
			Vector2 pos = new Vector2(projectilePos.x * lookDirection, projectilePos.y);

			projectile.transform.localPosition = pos;
			projectile.LookForEnemies();
		}

		public void EndSpecial() {
			isUsingSpecial = false;
			isCharging = true;
			cooldownTimer = 0;
			anim.SetBool("IsUsingSpecial", false);
		}

		public override void Heal(int amount) {
			base.Heal(amount);

			playerHUD.UpdateHealth(CurrentHealth);
		}

		public override void Hit(int amount) {
			if (!isPaused && !isDashing) {
				base.Hit(amount);

				playerHUD.UpdateHealth(CurrentHealth);

				if (CurrentHealth > 0) {
					Time.timeScale = 0;
					isPaused = true;
					StartCoroutine(Pause());
				}
			}
		}

		private IEnumerator Pause() {
			yield return new WaitForSecondsRealtime(pauseTime);
			Time.timeScale = 1;
			isPaused = false;
		}

		public bool PowerUp(string powerUp) {
			switch (powerUp) {
				case "Special":
					powerUpInfo.hasSpecial = true;
					playerHUD.UpdateCharge(1);
					return true;
				case "Dash":
					powerUpInfo.hasDash = true;
					return true;
				case "Double Jump":
					powerUpInfo.hasDJ = true;
					return true;
				default:
					return false;
			}
		}
	}
}
