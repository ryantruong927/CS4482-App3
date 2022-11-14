using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager {
	[RequireComponent(typeof(MenuManager))]
	[RequireComponent(typeof(SceneManager))]
	[RequireComponent(typeof(SaveManager))]
	[RequireComponent(typeof(InventoryManager))]
	public class GameManager : MonoBehaviour {
		private static MenuManager menuManager;
		private static SceneManager sceneManager;
		private static SaveManager saveManager;
		private static InventoryManager inventoryManager;

		public static float gravity = -5f;

		private void Start() {
			menuManager = GetComponent<MenuManager>();
			sceneManager = GetComponent<SceneManager>();
			saveManager = GetComponent<SaveManager>();
			inventoryManager = GetComponent<InventoryManager>();
		}

		public static void StartGame() {
			saveManager.Load(0);
		}

		public static void ExitGame() {
			saveManager.Save();
			sceneManager.ChangeScene(0);
		}

		public static void QuitGame() {
			Application.Quit();
			Debug.Log("Exiting game...");
		}
	}
}
