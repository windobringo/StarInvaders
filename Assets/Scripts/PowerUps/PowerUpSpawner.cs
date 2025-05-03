using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    public PowerUp_Shield powerUpShield;
    

    public float timeMin = 0.1f, timeMax = 0.3f;
    public int maxPowerUp = 2;

    public BoxCollider2D boxCollider;

    public float startDelay = 1;
    public int waveCount = 5;
    public int currentWave = 0;

    List<GameObject> currentPowerUps = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnWaveWithDelay(startDelay));
    }

    private IEnumerator SpawnWaveWithDelay(float startDelay)
    {
        currentWave++;
        yield return new WaitForSeconds(startDelay);
        float minX = boxCollider.bounds.min.x;
        float maxX = boxCollider.bounds.max.x;

        for (int i = 0; i < maxPowerUp; i++)
        {
            Vector3 spawnPoint = new Vector3(UnityEngine.Random.Range(minX, maxX), transform.position.y, 0);
            GameObject newPowerUp = Instantiate(powerUpShield.gameObject, spawnPoint, Quaternion.Euler(0, 0, 0));
            currentPowerUps.Add(newPowerUp);
            yield return new WaitForSeconds(12f);
        }

    }

    //public void EnemyKilled(Enemy enemy, bool playerKill)
    //{
    //    if (currentPowerUps.Remove(enemy.gameObject))
    //    {
    //        if (playerKill)
    //        {
    //            score += 100;
    //            scoreText.text = score + "";
    //        }

    //        if (currentPowerUps.Count == 0)
    //        {
    //            if (currentWave == waveCount)
    //            {
    //                Debug.Log("You win");
    //                scoreSystem.SetScore(score);
    //                winScreen.Toggle();
    //                menuButton.interactable = false;
    //                return;
    //            }
    //            StartCoroutine(SpawnWaveWithDelay(0.5f));
    //        }
    //    }

    //}
}
