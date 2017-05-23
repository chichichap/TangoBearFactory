using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameplayController : MonoBehaviour {

    public static GameplayController Singleton;
    public Spawn SpawnController;

    public float roundTime;
    public float roundWaitTime;

    public int Score;
    
    enum GameplayState
    {
        Start,
        RoundStart,
        InRound,
        RoundEnd,
    }
    GameplayState state;
    
    void Awake()
    {
        Singleton = this;
    }

    float roundStartTime;
    float roundEndTime;

    void Update()
    {
        switch(state)
        {
            case GameplayState.Start:
                state = GameplayState.RoundStart;
                break;
            case GameplayState.RoundStart:
                roundStartTime = Time.time;
                SpawnController.IsSpawning = true;
                state = GameplayState.InRound;
                break;
            case GameplayState.InRound:
                var currTime = Time.time - roundStartTime;
                var timeLeft = roundTime - currTime;
                GameObject.FindGameObjectWithTag("MainCanvas").transform.Find("TextTimer").GetComponent<Text>().text = Mathf.Clamp(timeLeft, 0, 999999).ToString();
                if (timeLeft <= 0)
                {
                    roundEndTime = Time.time;
                    state = GameplayState.RoundEnd;
                }
                break;
            case GameplayState.RoundEnd:
                SpawnController.IsSpawning = false;
                if (Time.time - roundEndTime > roundWaitTime)
                {
                    state = GameplayState.RoundStart;
                }
                break;
        }
        
        GameObject.FindGameObjectWithTag("MainCanvas").transform.Find("TextScore").GetComponent<Text>().text = Score.ToString();
    }


}
