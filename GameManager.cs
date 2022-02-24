using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public List<GameObject> allUnits;
    public static GameManager gameManager = null;
    [HideInInspector]
    public GameObject[] allies;
    [HideInInspector]
    public GameObject[] enemies;
    [HideInInspector]
    public List<GameObject> alliesList = null;
    [HideInInspector]
    public List<GameObject> enemiesList = null;

    public Enums.GameStates gameState;
    private int round;
    private int totalRounds;
    private int level = 1;            // 8a to pairnei apo allou
    public UnityEvent updateEnemies;
    public UnityEvent updateAllies;

    public TMP_Text timerText;
    public TMP_Text roundText;
    public TMP_Text stageText;
    float timer = 10f;

    bool inPreparation = false;
    bool fightEnded = false;
    private void Awake()
    {
        if(gameManager == null)
        {
            gameManager = this;
        }
        else
        {
            Destroy(this);
        }

        timerText = GameObject.Find("Timer").GetComponent<TMP_Text>();
        roundText = GameObject.Find("Round").GetComponent<TMP_Text>();
        stageText = GameObject.Find("Stage").GetComponent<TMP_Text>();
    }
    // Start is called before the first frame update
    void Start()
    {
        if (updateEnemies == null)
            updateEnemies = new UnityEvent();
        if (updateAllies == null)
            updateAllies = new UnityEvent();

        updateEnemies.AddListener(UpdateEnemies);
        updateAllies.AddListener(UpdateAllies);
        gameState = Enums.GameStates.Preparation; // 8a alla3ei
        updateAllies.Invoke();
        updateEnemies.Invoke();
        round = 1;
        totalRounds = Enums.roundsPerLevel[level, 0];
    }

    
    public void UpdateEnemies()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enemy in enemies)
        {
            if (enemy.GetComponent<UnitAttributes>().currentHealth > 0)
            {
                enemiesList.Add(enemy);
            }
        }
        enemies = null;
    }
    public void UpdateAllies()
    {
        allies = GameObject.FindGameObjectsWithTag("Unit");
        foreach (GameObject ally in allies)
        {
            if (ally.GetComponent<UnitAttributes>().currentHealth > 0)
            {
                alliesList.Add(ally);
            }
        }
        allies = null;
    }
    // Update is called once per frame
    void Update()
    {
        TextUpdates();
        RoundManager();
        if (inPreparation)
        {
            timer -= Time.deltaTime;
        }
    }

    private void TextUpdates()
    {
        timerText.text = "Time left: " + (int)(timer);
        roundText.text = "Round " + round;
        stageText.text = "Stage :" + gameState;
    }

    void RoundManager()
    {
        switch (gameState)
        {
            case Enums.GameStates.Preparation:
                if (!inPreparation)
                {
                    StartCoroutine(PreparationRoutine());
                }
                break;
            case Enums.GameStates.Fight:
                if(!fightEnded)
                endOfFightCheck();
                break;
            case Enums.GameStates.EndOfRound:
                break;
            case Enums.GameStates.EndOfLevel:
                break;
        }
        
        
    }

    IEnumerator PreparationRoutine()
    {
        timer = 10f;
        inPreparation = true;
        timerText.enabled = true;
        yield return new WaitUntil(() => timer <= 0f);
        inPreparation = false;
        timerText.enabled = false;
        gameState = Enums.GameStates.Fight;

    }

    void endOfFightCheck()
    {
        if (enemiesList.Count == 0 || alliesList.Count == 0)
        {
            fightEnded = true;
            StartCoroutine(ThreeSecDelay());
        }
    }

    IEnumerator ThreeSecDelay()
    {
        yield return new WaitForSeconds(3f);
        if (enemiesList.Count == 0)
        {
            round++;
            if (round != totalRounds)
            {
                gameState = Enums.GameStates.Preparation;
            }
            else
            {
                gameState = Enums.GameStates.EndOfLevel;
            }
            
        }
        else
        {
            gameState = Enums.GameStates.GameOver;
        }
    }

}
