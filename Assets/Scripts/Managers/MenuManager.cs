using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager {
    public class MenuManager : MonoBehaviour {

		private void Start() {

		}

		public void StartButton() {
            GameManager.StartGame();
        }

        public void SaveButton() {

        }

        public void ExitButton() {
            GameManager.ExitGame();
        }

        public void QuitButton() {
            GameManager.QuitGame();
        }
    }
}
