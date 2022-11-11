using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SceneManagement = UnityEngine.SceneManagement;

namespace Manager {
	public class SceneManager : MonoBehaviour {
		public void ChangeScene(int buildIndex) {
			SceneManagement.SceneManager.LoadScene(buildIndex);
		}
	}
}
