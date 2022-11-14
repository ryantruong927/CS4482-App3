using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData : ScriptableObject {
	public Character.PlayerData playerData;
	public int roomNum;
	public bool isRoomCleared;
	public bool isBoss1Defeated, isBoss2Defeated;
}
