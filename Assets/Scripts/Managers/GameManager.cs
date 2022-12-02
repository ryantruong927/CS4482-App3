using Character.Player;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Manager {
	[RequireComponent(typeof(SaveManager))]
	public class GameManager : MonoBehaviour {
		private SaveManager saveManager;

		public PlayerController player;
		public GameObject dash, dj, special;
		public GameObject reaper, golem;

		public GameObject pauseMenu, endingScreen;

		private int campfireNum;
		private int buildIndex;

		private void Awake() {
			saveManager = GetComponent<SaveManager>();

			saveManager.SetGameManager(this);

			buildIndex = SceneManager.GetActiveScene().buildIndex;

			if (buildIndex == 1) {
				PowerUpInfo powerUpInfo = saveManager.Load(PlayerPrefs.GetInt("saveNum"));

				if (powerUpInfo.hasDash) {
					player.PowerUp("Dash");
					Destroy(dash);
				}
				if (powerUpInfo.hasDJ) {
					player.PowerUp("Double Jump");
					Destroy(dj);
				}
				if (powerUpInfo.hasSpecial) {
					player.PowerUp("Special");
					Destroy(special);
				}
				if (powerUpInfo.hasBlackGem) {
					player.PowerUp("Black Gem");
					Destroy(reaper);
				}
				if (powerUpInfo.hasBlueGem) {
					player.PowerUp("Blue Gem");
					Destroy(golem);
				}

				campfireNum = PlayerPrefs.GetInt("CampfireNum");
				Vector2 campfirePos = GameObject.Find("Campfire " + campfireNum).transform.position;
				player.transform.position = new Vector2(campfirePos.x - 2f, campfirePos.y + 1.15f);
			}
		}

		public void ChangeScene(int buildIndex) {
			SceneManager.LoadScene(buildIndex);
		}

		public void Save(int campfireNum, PowerUpInfo powerUpInfo) {
			this.campfireNum = campfireNum;
			saveManager.Save(PlayerPrefs.GetInt("saveNum"), campfireNum, powerUpInfo);
		}

		public void StartGame(int saveNum) {
			PlayerPrefs.SetInt("saveNum", saveNum);
			PlayerPrefs.Save();

			ChangeScene(1);
		}

		public void PauseGame(bool isPaused) {
			pauseMenu.SetActive(isPaused);
			Time.timeScale = isPaused ? 0 : 1;
		}

		public void EndGame(string ending) {
			TextMeshProUGUI text;
			switch (ending) {
				case "Ending 1":
					endingScreen.SetActive(true);
					text = endingScreen.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
					text.text = "The knight rests as they have completed half of their quest.\n" +
						"Even though the powerful monster was defeated,\n" +
						"the world is still in unrest...";
					break;
				case "Ending 2":
					endingScreen.SetActive(true);
					text = endingScreen.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
					text.text = "The knight stands alone, gracing their master's memorial with\n" +
						"the trophies of those that ruined the world.\n" +
						"Though not much can be done to revive the fallen world,\n" +
						"the knight has finally found peace.";
					break;
			}
		}

		public void ExitGame() {
			saveManager.Save(PlayerPrefs.GetInt("saveNum"), campfireNum, player.powerUpInfo);
			PlayerPrefs.Save();
			Time.timeScale = 1;
			ChangeScene(0);
		}

		public void QuitGame() {
			Application.Quit();
			Debug.Log("Exiting game...");
		}
	}
}
