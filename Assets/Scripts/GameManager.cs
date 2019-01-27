using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Rendering;
using TMPro;

public class GameManager : MonoBehaviour, IService
{
    public CameraVFX cameraVFX;
    public CameraController cameraController;
    public Image healthImageFill;
    public Image chargeUpImageFill;
    public AudioSource thumpAudio;
    public GameObject player;

    [Header("Prefabs")]
    public GameObject[] enemyPrefabs;
    public GameObject[] housePrefabs;

    [Header("Waves")]
    public int wave = 1;
    public int score;
    public TextMeshProUGUI scoreText;
    public List<EnemyController> enemies;
    public Transform[] spawnLocations;


    private void Awake()
    {
        ServiceLocator.Instance.AddService(this);
    }

    public void Start()
    {
        StartCoroutine(StartWave());
    }

    public IEnumerator StartWave()
    {
        wave++;
        int randAmount = Random.Range(wave / 2, wave + 1);
        if (wave == 1)
            randAmount = 1;
        int randLocation = Random.Range(0, spawnLocations.Length);
        for (int j = 0; j < enemyPrefabs.Length; j++)
            for (int i = 0; i < randAmount; i++)
            {
                enemies.Add(Instantiate(enemyPrefabs[j], spawnLocations[randLocation].position, Quaternion.identity).GetComponent<EnemyController>());
                yield return new WaitForSeconds(0.5f);
            }
    }

    public void EnemyDeath(EnemyController enemy)
    {
        enemies.Remove(enemy);
        if (enemy is ShotgunEnemy)
            score += 10;
        else if (enemy is CircleEnemy)
            score += 15;
        else
            score += 5;
        scoreText.text = score.ToString();
        if (enemies.Count == 0)
            StartCoroutine(StartWave());
    }

    private void OnDestroy()
    {
        ServiceLocator.Instance.RemoveService<GameManager>();
    }
}
