using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiator : MonoBehaviour {

    public GameObject ASTEROID_PREFAB;
    public float SPAWN_HEIGHT;
    public float MIN_X;
    public float MAX_X;
    public float CHILD_X;
    public float CHILD_Y;
    public float LIM_MIN_X;
    public float LIM_MAX_X;
    public float LIM_MAX_Y;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void generateAsteroid()
    {
        Instantiate(ASTEROID_PREFAB, new Vector2(Random.Range(MIN_X, MAX_X), SPAWN_HEIGHT), Quaternion.identity);
    }

    public void generateAsteroid(Vector2 position)
    {
        Instantiate(ASTEROID_PREFAB, position, Quaternion.identity);
    }

    public void splitAsteroid(Vector3 parentPosition, Vector3 parentScale, float radius)
    {
        // Generate two child
        Vector2 child1Pos = new Vector2(Mathf.Min(parentPosition.x + radius, LIM_MAX_X - radius), Mathf.Min(parentPosition.y + radius, LIM_MAX_Y - radius));
        Vector2 child2Pos = new Vector2(Mathf.Max(parentPosition.x - radius, LIM_MIN_X + radius), Mathf.Min(parentPosition.y + radius, LIM_MAX_Y - radius));

        GameObject child1 = Instantiate(ASTEROID_PREFAB, child1Pos, Quaternion.identity);
        GameObject child2 = Instantiate(ASTEROID_PREFAB, child2Pos, Quaternion.identity);

        // Scale child
        child1.transform.localScale = parentScale - new Vector3(0.2f, 0.2f, 0.2f);
        child2.transform.localScale = parentScale - new Vector3(0.2f, 0.2f, 0.2f);

        // Add Force
        child1.GetComponent<Rigidbody2D>().AddForce(new Vector2(CHILD_X, CHILD_Y));
        child2.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1 * CHILD_X, CHILD_Y));
    }
}
