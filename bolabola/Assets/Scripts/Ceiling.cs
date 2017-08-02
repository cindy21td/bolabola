using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ceiling : MonoBehaviour {

	private Collider2D collider;

	// Use this for initialization
	void Start () {
		collider = this.GetComponent<Collider2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.tag == "NewAsteroid") {
			Physics2D.IgnoreCollision(collision.collider, collider);
		}
	}

	void OnCollisionStay2D(Collision2D collision) {
		if (collision.gameObject.tag == "NewAsteroid") {
			Physics2D.IgnoreCollision(collision.collider, collider);
		}
	}

	void OnCollisionExit2D(Collision2D collision) {
		if (collision.gameObject.tag == "NewAsteroid") {
			collision.gameObject.tag = "Asteroid";
			SpriteRenderer renderer = collision.gameObject.GetComponent<SpriteRenderer> ();
		}
	}
}
