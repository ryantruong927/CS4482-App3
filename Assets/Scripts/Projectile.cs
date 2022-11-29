using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    [HideInInspector]
    public string owner;
    public int damage;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (!collision.CompareTag(owner)) {
            Character.CharacterController character = collision.GetComponent<Character.CharacterController>();

            if (character != null)
                character.Hit(damage);

            Destroy(gameObject);
        }
    }
}
