using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scorer : MonoBehaviour {

    public Text scoreText;
    public Text damageText;
    public Gameplay gameplay;

    private int score;
    private float damage;

	// Use this for initialization
	void Start () {
        score = 0;
        scoreText.text = "0";

        damage = 0;
        damageText.text = "0%";
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void updateScore(float scale)
    {
        print(scale);
        if (scale == 1f)
        {
            score += 1;
        }
        else if (scale == 0.8f)
        {
            score += 2;
        }
        else
        {
            score += 4;
        }
        scoreText.text = "" + score;
    }

    public void updateDamage(float scale)
    {
        if (scale == 1f)
        {
            damage += 5;
        }
        else if (scale == 0.8f)
        {
            damage += 2.5f;
        }
        else
        {
            damage += 1;
        }
        damage = Mathf.Min(100, damage);
        damageText.text = damage + "%";

        // Check for damage
        if (damage >= 100)
        {
            gameplay.setIsOver();
        }
    }
}
