using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character.Enemy.Boss {
    public class Gem : PowerUp.PowerUp {
        private bool canBePickedUp = false;

        private void Start() {
            StartCoroutine(Wait());
        }

        private IEnumerator Wait() {
            yield return new WaitForSecondsRealtime(3f);

            canBePickedUp = true;
        }

        protected override void OnTriggerStay2D(Collider2D collision) {
            if (canBePickedUp)
                base.OnTriggerStay2D(collision);
        }
    }
}
