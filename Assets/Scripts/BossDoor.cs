using Character.Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoor : MonoBehaviour {
    public EnemyController boss;
    private void OnTriggerEnter2D(Collider2D collision) {
        boss.StartCombat();
        Destroy(gameObject);
    }
}
