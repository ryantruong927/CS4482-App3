using Character.PowerUp;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Character.Player {
	public class PlayerHUD : MonoBehaviour {
		public Image healthBar, chargeBar;
		public GameObject dash, dj, wj, special, blackGem, blueGem;
		public GameObject powerUpText;

		public void UpdateHealth(int health) {
			healthBar.fillAmount = health / 8f;
		}

		public void UpdateCharge(float charge) {
			chargeBar.fillAmount = Mathf.Clamp01(charge); ;
		}

		public void ShowItem(bool isPlayerCalling, string item) {
			switch (item) {
				case "Wall Jump":
					wj.SetActive(true);
					if (isPlayerCalling)
					StartCoroutine(ShowPowerUpText(item));

					break;
				case "Dash":
					dash.SetActive(true);
					if (isPlayerCalling)
						StartCoroutine(ShowPowerUpText(item));

					break;
				case "Double Jump":
					dj.SetActive(true);
					if (isPlayerCalling)
						StartCoroutine(ShowPowerUpText(item));

					break;
				case "Special":
					special.SetActive(true);
					if (isPlayerCalling)
						StartCoroutine(ShowPowerUpText(item));

					break;
				case "Black Gem":
					blackGem.SetActive(true);
					if (isPlayerCalling)
						StartCoroutine(ShowPowerUpText(item));

					break;
				case "Blue Gem":
					blueGem.SetActive(true);
					if (isPlayerCalling)
						StartCoroutine(ShowPowerUpText(item));

					break;
				default:
					break;
			}
		}

		private IEnumerator ShowPowerUpText(string item) {
			powerUpText.GetComponent<TextMeshProUGUI>().text = item + " unlocked!";
			powerUpText.SetActive(true);
			yield return new WaitForSecondsRealtime(5f);
			powerUpText.SetActive(false);
		}
	}
}
