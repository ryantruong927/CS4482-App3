using Character.PowerUp;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Character.Player {
	public class PlayerHUD : MonoBehaviour {
		public Image healthBar, chargeBar;
		public GameObject dash, dj, special, blackGem, blueGem;

		public void UpdateHealth(int health) {
			healthBar.fillAmount = health / 8f;
		}

		public void UpdateCharge(float charge) {
			chargeBar.fillAmount = Mathf.Clamp01(charge); ;
		}

		public void ShowItem(string item) {
			switch (item) {
				case "Black Gem":
					blackGem.SetActive(true);

					break;
				case "Blue Gem":
					blueGem.SetActive(true);

					break;
				case "Special":
					special.SetActive(true);

					break;
				case "Dash":
					dash.SetActive(true);

					break;
				case "Double Jump":
					dj.SetActive(true);

					break;
				default:
					break;
			}
		}
	}
}
