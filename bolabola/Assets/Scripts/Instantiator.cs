using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiator : MonoBehaviour {

    public GameObject ASTEROID_PREFAB;

	public float TOP_SCREEN;
	public float RIGHT_SCREEN;

	private float SPAWN_HEIGHT;
	private float MIN_X;
	private float MAX_X;

    public float CHILD_X;
    public float CHILD_Y;

	private float LIM_MIN_X;
	private float LIM_MAX_X;
	private float LIM_MAX_Y;

    // Use this for initialization
    void Start () {
		TOP_SCREEN = Camera.main.orthographicSize;
		RIGHT_SCREEN = ((TOP_SCREEN * 2.0f * Screen.width) / Screen.height) / 2;

		SPAWN_HEIGHT = TOP_SCREEN + 1f;
		MIN_X = -1 * RIGHT_SCREEN + 0.5f;
		MAX_X = RIGHT_SCREEN - 0.5f;

		LIM_MIN_X = -1 * RIGHT_SCREEN;
		LIM_MAX_X = RIGHT_SCREEN;
		LIM_MAX_Y = TOP_SCREEN - 1f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void generateAsteroid()
    {
		generateAsteroid (new Vector2 (Random.Range (MIN_X, MAX_X), SPAWN_HEIGHT));
	}

    public void generateAsteroid(Vector2 position)
    {
        GameObject newAst = Instantiate(ASTEROID_PREFAB, position, Quaternion.identity);
		newAst.tag = "NewAsteroid";
		SpriteRenderer renderer = newAst.GetComponent<SpriteRenderer> ();
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
