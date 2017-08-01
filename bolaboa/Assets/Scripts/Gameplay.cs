using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gameplay : MonoBehaviour {

    public float BASE_SPAWN_TIME;
    public int BASE_CYCLE;
    public float FIRST_AST_X;
    public float FIRST_AST_Y;
    public Instantiator instantiator;
    public GameObject gameOverPanel;
    public GameObject instructionPanel;
    public GameObject readyPanel;
    public Text readyText;

    private enum GameState
    {
        Start,
        Ready,
        Go,
        Over
    }

    private float SPAWN_TIME_LIMIT = 3f;
    private float READY_TIMER = 1f;

    private float curentSpawnTime;
    private int currentCycle;
    private float currentReadyTimer;
    private int currentReadyValue;
    private GameState currentGameState;

	// Use this for initialization
	void Start () {
        curentSpawnTime = BASE_SPAWN_TIME;
        currentCycle = BASE_CYCLE;
        currentReadyTimer = READY_TIMER;
        currentReadyValue = 3;

        currentGameState = GameState.Start;
	}
	
	// Update is called once per frame
	void Update () {
        switch (currentGameState)
        {
            case GameState.Start:
                break;
            case GameState.Ready:
                // Play countdown
                if (currentReadyTimer <= 0)
                {
                    currentReadyValue -= 1;

                    if (currentReadyValue < 0)
                    {
                        setIsGo();
                    }

                    readyText.text = "" + (currentReadyValue);
                    currentReadyTimer = READY_TIMER;
                }

                currentReadyTimer -= Time.deltaTime;
                break;
            case GameState.Go:
                if (curentSpawnTime < 0)
                {
                    instantiator.generateAsteroid();

                    currentCycle -= 1;
                    curentSpawnTime = BASE_SPAWN_TIME;
                }

                if (currentCycle <= 0)
                {
                    currentCycle = BASE_CYCLE;
                    BASE_SPAWN_TIME -= 0.25f;
                    BASE_SPAWN_TIME = Mathf.Max(BASE_SPAWN_TIME, SPAWN_TIME_LIMIT);
                }

                curentSpawnTime -= Time.deltaTime;
                break;
            case GameState.Over:
                break;
        }
	}

    public void setIsOver()
    {
        // Enable game over panel;
        gameOverPanel.SetActive(true);
        currentGameState = GameState.Over;
    }

    public void setIsReady()
    {
        // Disable instruction
        instructionPanel.SetActive(false);
        readyPanel.SetActive(true);
        currentGameState = GameState.Ready;
    }

    public void setIsGo()
    {
        // Disable ready panel
        readyPanel.SetActive(false);
        // Drop first asteroid
        instantiator.generateAsteroid(new Vector2(FIRST_AST_X, FIRST_AST_Y));
        currentGameState = GameState.Go;
    }
}
