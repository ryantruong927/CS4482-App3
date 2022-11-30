using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Character.Player {
    public class PlayerHUD : MonoBehaviour {
        public Image healthBar, chargeBar;

        public void UpdateHealth(int health) {
			healthBar.fillAmount = health / 8f;
		}

		public void UpdateCharge(float charge) {
			chargeBar.fillAmount = Mathf.Clamp01(charge); ;
		}
	}
}
