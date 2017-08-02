using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    public float SCALE_LIMIT;
    public GameObject explosion;

    private float resistance;

    private Instantiator instantiator;
    private Scorer scorer;
    private Rigidbody2D rigidBody;
    private Animator animator;

	// Use this for initialization
	void Start () {
        rigidBody = this.GetComponent<Rigidbody2D>();
        animator = explosion.GetComponent<Animator>();

        instantiator = Camera.main.GetComponent<Instantiator>();
        scorer = Camera.main.GetComponent<Scorer>();

        resistance = this.transform.localScale.x;
    }
	
	// Update is called once per frame
	void Update () {

	}

    void LateUpdate()
    {
        if (animator.gameObject.activeSelf) {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Finished")) {
                Destroy(this.gameObject);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
		if (coll.gameObject.tag == "Asteroid" && this.gameObject.tag != "NewAsteroid") 
        {
            // Decide whether to split or just boucne based on resistance
            float collResistance = coll.gameObject.GetComponent<Ball>().getResistance();

            resistance -= Random.Range(collResistance / 8, collResistance / 2);

            if (resistance < 0)
            {
                scorer.updateScore(this.transform.localScale.x);
                // Split
                HandleContact();
            } else
            {
                // Bounce back by adding force
                rigidBody.AddForce((this.transform.position - coll.transform.position).normalized * 5);
            }
        } else if (coll.gameObject.tag == "Wall")
        {
            float xForce = ((new Vector2(this.transform.position.x, this.transform.position.y) - coll.contacts[0].point).normalized * 5).x;
            rigidBody.AddForce(new Vector2(xForce, 0));
        }
    }

    void OnMouseDown()
    {
		if (this.gameObject.tag != "NewAsteroid") {
			// Update score
			scorer.updateScore(this.transform.localScale.x);

			HandleContact();
		}
    }

    void HandleContact()
    {
        // Check the scale
        if (this.transform.localScale.x > SCALE_LIMIT)
        {
            // Get radius
            float radius = this.gameObject.GetComponent<CircleCollider2D>().radius;
            instantiator.splitAsteroid(this.transform.position, this.transform.localScale, radius);
        }

        Destroy(this.gameObject);
    }

    float getResistance()
    {
        return resistance;
    }

    void setResistance(float newRes)
    {
        resistance = newRes;
    }

    public void playExplosion()
    {
        // Set active
        explosion.SetActive(true);

        // Set the bool
        animator.SetBool("isPassing", true);
    }
}
