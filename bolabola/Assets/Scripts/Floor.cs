using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour {

    private Scorer scorer;

	// Use this for initialization
	void Start () {
        scorer = Camera.main.GetComponent<Scorer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            scorer.updateDamage(collision.gameObject.transform.localScale.x);

            // Play animation for asteroid
            collision.gameObject.GetComponent<Ball>().playExplosion();

            //Destroy(collision.gameObject);
        }
    }
}
