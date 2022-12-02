using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character.Player {
	public class Statue : Interactable {
		public GameObject blackGem, blueGem;

		public string AddGem(string gem) {
			if (gem == "Black Gem")
				blackGem.SetActive(true);
			if (gem == "Blue Gem")
				blueGem.SetActive(true);

			if (blackGem.activeSelf && blueGem.activeSelf)
				return "Ending 2";
			if (blackGem.activeSelf || blueGem.activeSelf)
				return "Ending 1";

			return "";
		}
	}
}
