using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character.Player {
    [RequireComponent(typeof(CircleCollider2D))]
    public class Interactable : MonoBehaviour {
        public string msg;

        public virtual string Interact() {
            return msg;
        }
    }
}
