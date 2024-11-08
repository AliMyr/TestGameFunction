using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [SerializeField] private CharacterFactory characterFactory;

    private ScoreSystem scoreSystem;
    private float gameSessionTime;
    private float timeBetweenEnemySpawn;
    private bool isGameActive;

    public static GameManager Instance { get; private set; }
    public CharacterFactory CharacterFactory => characterFactory;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Initialize();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Initialize()
    {
        scoreSystem = new ScoreSystem();
        isGameActive = false;
    }

    public void StartGame()
    {
        if (isGameActive) return;

        Character player = characterFactory.GetCharacter(CharacterType.Player);
        player.transform.position = Vector3.zero;
        player.gameObject.SetActive(true);
        player.Initialize();

        // �������� � �������� �� �������
        if (player.LiveComponent != null)
        {
            player.LiveComponent.OnCharacterDeath += CharacterDeathHandler;
        }

        gameSessionTime = 0;
        timeBetweenEnemySpawn = gameData.TimeBetweenEnemySpawn;
        scoreSystem.startGame();
        isGameActive = true;
    }

    private void Update()
    {
        if (!isGameActive) return;

        gameSessionTime += Time.deltaTime;
        timeBetweenEnemySpawn -= Time.deltaTime;

        if (timeBetweenEnemySpawn <= 0)
        {
            SpawnEnemy();
            timeBetweenEnemySpawn = gameData.TimeBetweenEnemySpawn;
        }

        if (gameSessionTime >= gameData.SessionTimeSecond)
        {
            GameVictory();
        }
    }

    private void CharacterDeathHandler(Character deathCharacter)
    {
        if (deathCharacter == null) return;

        switch (deathCharacter.CharacterType)
        {
            case CharacterType.Player:
                GameOver();
                break;
            case CharacterType.DefaultEnemy:
                scoreSystem.AddScore(deathCharacter.CharacterData.ScoreCost);
                break;
        }

        deathCharacter.gameObject.SetActive(false);
        characterFactory.ReturnCharacter(deathCharacter);

        // ������� �� �������
        if (deathCharacter.LiveComponent != null)
        {
            deathCharacter.LiveComponent.OnCharacterDeath -= CharacterDeathHandler;
        }
    }

    private void SpawnEnemy()
    {
        Character enemy = characterFactory.GetCharacter(CharacterType.DefaultEnemy);
        Vector3 playerPosition = characterFactory.Player.transform.position;
        enemy.transform.position = new Vector3(playerPosition.x + GetOffset(), 0, playerPosition.z + GetOffset());
        enemy.gameObject.SetActive(true);
        enemy.Initialize();

        // �������� � �������� �� �������
        if (enemy.LiveComponent != null)
        {
            enemy.LiveComponent.OnCharacterDeath += CharacterDeathHandler;
        }

        float GetOffset()
        {
            bool isPlus = Random.Range(0, 100) % 2 == 0;
            float offset = Random.Range(gameData.MinSpawnOffset, gameData.MaxSpawnOffset);
            return isPlus ? offset : -offset;
        }
    }

    private void GameVictory()
    {
        scoreSystem.EndGame();
        Debug.Log("Victory");
        isGameActive = false;
    }

    private void GameOver()
    {
        scoreSystem.EndGame();
        Debug.Log("Defeat");
        isGameActive = false;
    }
}
