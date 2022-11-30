using Character.Enemy.Boss;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour {
    public BossController boss;

    private void OnTriggerEnter2D(Collider2D collision) {
		boss.StartCombat();
        Destroy(gameObject);
    }
}
