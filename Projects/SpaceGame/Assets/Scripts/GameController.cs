using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    [SerializeField] private Transform asteroidSpawner;
    [SerializeField] private GameObject[] listAsteroids;
    [SerializeField] private Transform background;
    [SerializeField] private TextMeshProUGUI textScore;
    [SerializeField] private Score score;

    private Vector3 screenScale;
    private Vector3 boundaryScreen;
    private float asteroidSpawnerBoundary;
    private void Awake()
    {
        ScreenBoundary();
        AsteroidSpawner();
        BackgroundScale();
    }
    

    private void UpdateScore()
    {
        if (score.oldScore != score.newScore)
        {
            score.oldScore = score.newScore;
            ScoreView();
        }
    }
    private void ScoreView()
    {
        textScore.text = $"Score: {score.newScore}";
    }

    private void ScreenBoundary()
    {
        screenScale = new Vector3(Screen.width, 0, Screen.height);
        boundaryScreen = new Vector3(Camera.main.ScreenToWorldPoint(screenScale).x, .1f, Camera.main.ScreenToWorldPoint(screenScale).z);
    }
    private void AsteroidSpawner()
    {
        asteroidSpawner.localScale = new Vector3(boundaryScreen.x, .1f, 1);
        asteroidSpawnerBoundary = asteroidSpawner.localScale.x;
    }

    private void BackgroundScale()
    {
        background.localScale = new Vector3(boundaryScreen.x * 2, 15, 1);
    }

    private void Start()
    {
        ScoreView();
        StartCoroutine(spawnTimer(true, 1));
    }

    private void Update()
    {
        UpdateScore();
    }

    private IEnumerator spawnTimer(bool isGame, float time)
    {
        while (isGame)
        {
            yield return new WaitForSeconds(time);
            Vector3 pos = new Vector3(Random.Range(-asteroidSpawnerBoundary, asteroidSpawnerBoundary), asteroidSpawner.position.y, asteroidSpawner.position.z);
            Instantiate(listAsteroids[Random.Range(0, listAsteroids.Length)], pos, Quaternion.identity);
        }
    }
}
