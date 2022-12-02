using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Tooltip : MonoBehaviour
{
    private SpriteRenderer sr;

    private void Start() {
        sr = GetComponent<SpriteRenderer>();
        sr.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        StartCoroutine(Show());
	}

    private IEnumerator Show() {
		sr.enabled = true;

        yield return new WaitForSecondsRealtime(7f);

        Destroy(gameObject);
	}
}
