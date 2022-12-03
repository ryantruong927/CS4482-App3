using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character.Player {
    public class Campfire : Interactable {
		private GameObject saveIcon;

		private void Start() {
			saveIcon = transform.GetChild(3).gameObject;
			saveIcon.SetActive(false);
		}

		public override string Interact() {
			StartCoroutine(ShowIcon());
			return base.Interact();
		}

		private IEnumerator ShowIcon() {
			saveIcon.SetActive(true);
			yield return new WaitForSecondsRealtime(3f);
			saveIcon.SetActive(false);
		}
	}
}
