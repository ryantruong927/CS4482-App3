using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character.Player {
    public class Interact : MonoBehaviour {
        private PlayerController player;
        private bool isInteracting;

        private void Start() {
            player = GetComponentInParent<PlayerController>();
        }

        private void Update() {
            isInteracting = Input.GetButtonDown("Interact");
        }

        private void OnTriggerStay2D(Collider2D collision) {
            if (isInteracting && collision.CompareTag("Interactable"))
                player.Interact(collision);
        }
    }
}
