using Character.Player;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using UnityEngine;

namespace Manager {
	public class SaveManager : MonoBehaviour {
		private GameManager gameManager;

		public void SetGameManager(GameManager gameManager) {
			this.gameManager = gameManager;
		}

		public PowerUpInfo Load(int saveNum) {
			string dataPath = Application.persistentDataPath + "/save" + saveNum + ".txt";
			PowerUpInfo powerUpInfo = new PowerUpInfo();

			FileStream fs;
			BinaryFormatter bf = new();

			try {
				fs = new(dataPath, FileMode.Open, FileAccess.Read, FileShare.None);

				SaveData saveData = (SaveData)bf.Deserialize(fs);

				fs.Close();

				PlayerPrefs.SetInt("CampfireNum", saveData.campfireNum);
				PlayerPrefs.Save();
				powerUpInfo.hasWJ = saveData.hasWJ;
				powerUpInfo.hasDash = saveData.hasDash;
				powerUpInfo.hasDJ = saveData.hasDJ;
				powerUpInfo.hasSpecial = saveData.hasSpecial;
				powerUpInfo.hasBlackGem = saveData.hasBlackGem;
				powerUpInfo.hasBlueGem = saveData.hasBlueGem;
			}
			catch {
				Save(saveNum, 0, new PowerUpInfo());
			}

			return powerUpInfo;
		}

		public void Save(int saveNum, int campfireNum, PowerUpInfo powerUpInfo) {
			string dataPath = Application.persistentDataPath + "/save" + saveNum + ".txt";

			FileStream fs;
			BinaryFormatter bf = new();

			SaveData saveData = new SaveData();
			saveData.campfireNum = campfireNum;
			saveData.hasWJ = powerUpInfo.hasWJ;
			saveData.hasDash = powerUpInfo.hasDash;
			saveData.hasDJ = powerUpInfo.hasDJ;
			saveData.hasSpecial = powerUpInfo.hasSpecial;
			saveData.hasBlackGem = powerUpInfo.hasBlackGem;
			saveData.hasBlueGem = powerUpInfo.hasBlueGem;

			if (!File.Exists(dataPath)) {
				fs = new(dataPath, FileMode.Create, FileAccess.Write, FileShare.None);
			}
			else {
				fs = new(dataPath, FileMode.Open, FileAccess.Write, FileShare.None);
			}

			bf.Serialize(fs, saveData);
			fs.Close();
		}
	}
}
