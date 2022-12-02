using Character.Player;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;

namespace Manager {
	public class SaveManager : MonoBehaviour {
		private GameManager gameManager;

		int saveNum;
		string dataPath;

		public void SetGameManager(GameManager gameManager) {
			this.gameManager = gameManager;
		}

		public void Load(int saveNum) {
		}

		public void Save(PowerUpInfo powerUpInfo) {

			if (name.Length - 1 == 3) {
				FileStream fs;
				BinaryFormatter bf = new();

				SaveData saveData;

				if (!File.Exists(dataPath)) {
					fs = new(dataPath, FileMode.Create, FileAccess.Write, FileShare.None);
					saveData = new SaveData();
				}
				else {
					fs = new(dataPath, FileMode.Open, FileAccess.Read, FileShare.None);

					try {
						saveData = (SaveData)bf.Deserialize(fs);
					}
					catch (SerializationException) {
						saveData = new SaveData();
					}

					fs.Close();
					fs = new(dataPath, FileMode.Open, FileAccess.Write, FileShare.None);
				}

				bf.Serialize(fs, saveData);
				fs.Close();

				gameManager.ChangeScene(1);
			}

			Time.timeScale = 1;
		}
	}
}
