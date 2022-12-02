using Character.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager {
	[RequireComponent(typeof(MenuManager))]
	[RequireComponent(typeof(SceneManager))]
	[RequireComponent(typeof(SaveManager))]
	public class GameManager : MonoBehaviour {
		private MenuManager menuManager;
		private SceneManager sceneManager;
		private SaveManager saveManager;

		public PlayerController player;
		public GameObject reaper, golem;

		private void Awake() {
			menuManager = GetComponent<MenuManager>();
			sceneManager = GetComponent<SceneManager>();
			saveManager = GetComponent<SaveManager>();

			menuManager.SetGameManager(this);
			sceneManager.SetGameManager(this);
			saveManager.SetGameManager(this);
		}

		public void ChangeScene(int buildIndex) {
			sceneManager.ChangeScene(buildIndex);
		}

		public void Save(PowerUpInfo powerUpInfo) {
			saveManager.Save(powerUpInfo);
		}

		public void StartGame() {
			saveManager.Load(0);
		}

		public void ExitGame() {
			saveManager.Save(player.powerUpInfo);
			sceneManager.ChangeScene(0);
		}

		public void QuitGame() {
			Application.Quit();
			Debug.Log("Exiting game...");
		}
	}
}
