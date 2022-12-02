using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager {
    public class MenuManager : MonoBehaviour {
        private GameManager gameManager;

		private void Start() {

		}

        public void SetGameManager(GameManager gameManager) {
            this.gameManager = gameManager;
        }

		public void StartButton() {
			gameManager.StartGame();
        }

        public void SaveButton() {

        }

        public void ExitButton() {
			gameManager.ExitGame();
        }

        public void QuitButton() {
			gameManager.QuitGame();
        }
    }
}
