using Manager;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character.Player {
	public struct PowerUpInfo {
		public bool hasSpecial;
		public bool hasDash;
		public bool hasDJ;
		public bool hasBlackGem, hasBlueGem;
	}

	public class PlayerController : CharacterController {
		public PowerUpInfo powerUpInfo;

		public GameManager gameManager;
		private PlayerHUD playerHUD;

		private float input;
		private bool isStanding = true;
		private bool isResting = false;

		public float coyoteTime = 0.1f;
		private float coyoteTimer;
		private bool hasCoyoteTime = false;
		private bool canJump = true;

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

				if (isUsingSpecial) {
					velocity.x = 0;
					velocity.y = rb.velocity.y;
					rb.velocity = velocity;
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

						if (isGrounded || hasCoyoteTime) {
							canJump = true;

							if (hasCoyoteTime) {
								coyoteTimer -= Time.deltaTime;

								if (coyoteTimer < 0) {
									hasCoyoteTime = false;
									canJump = false;
								}
							}

							if (isStanding)
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
								hasCoyoteTime = false;
								canJump = false;
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
							if (!hasCoyoteTime && canJump) {
								coyoteTimer = coyoteTime;
								hasCoyoteTime = true;
							}

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

						if (isResting)
							anim.SetFloat("Speed X", input);
						else
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

		//private void FixedUpdate() {
		//	if (isGrounded) {
		//		RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.5f, 1 << LayerMask.NameToLayer("Ground"));

		//		if (hit.collider != null && Mathf.Abs(hit.normal.x) > 0.1f) {
		//			rb.velocity = new Vector2(rb.velocity.x - hit.normal.x, rb.velocity.y);
		//			Vector3 pos = rb.position;
		//			pos.y += -hit.normal.x * Mathf.Abs(rb.velocity.x) * Time.deltaTime * (rb.velocity.x - hit.normal.x > 0 ? 1 : -1);
		//			transform.position = pos;
		//		}
		//	}
		//}

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
				isAttacking = false;
				isUsingSpecial = false;
				anim.SetBool("IsAttacking", false);
				anim.SetBool("IsUsingSpecial", false);

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

		public void Interact(Collider2D collision) {
			Interactable interactable = collision.GetComponent<Interactable>();

			string msg = interactable.Interact();
			Debug.Log(msg);

			switch (msg) {
				case "Statue":
					string ending = "";
					if (powerUpInfo.hasBlackGem)
						ending = ((Statue)interactable).AddGem("Black Gem");
					if (powerUpInfo.hasBlueGem)
						ending = ((Statue)interactable).AddGem("Blue Gem");
					Debug.Log(ending);
					break;
				case "Save":
					if (isGrounded) {
						anim.SetTrigger("Rest");
						lookDirection = rb.position.x < collision.transform.position.x ? 1f : -1f;
						sr.flipX = lookDirection == -1f;
						Rest();
					}

					break;
			}
		}

		public void Rest() {
			rb.velocity = new Vector2(0, rb.velocity.y);

			isStanding = false;
			isResting = true;

			Heal(maxHealth);
			gameManager.Save(powerUpInfo);
		}

		public void Stand() {
			isStanding = true;
			isResting = false;
		}

		public bool PowerUp(string powerUp) {
			playerHUD.ShowItem(powerUp);

			switch (powerUp) {
				case "Black Gem":
					powerUpInfo.hasBlackGem = true;
					return true;
				case "Blue Gem":
					powerUpInfo.hasBlueGem = true;
					return true;
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
