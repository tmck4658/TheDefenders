using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public int waveLength = 10;              // Reference to the player's heatlh.
    public GameObject[] theWave;             // The list of enemy prefab to be spawned.
    public float spawnTime = 3f;             // How long between each spawn.
    public GameObject[] spawnPoints;          // An array of the spawn points this enemy can spawn from.

    private int enemySpawnId;

    void Start ()
    {
        // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
        Manager.level = 1;
        InvokeRepeating ("Spawn", spawnTime, spawnTime);
    }

    void Update (){
        if(waveLength <= 0){
            waveLength += 10;
            Manager.level += 1;
        }
    }

    void Spawn ()
    {
        // Find a random index between zero and one less than the number of spawn points.
        int spawnPointIndex = Random.Range (0, spawnPoints.Length);
        if(Manager.level <= theWave.Length){
            enemySpawnId = Random.Range(0, Manager.level);
        }else{
            enemySpawnId = Random.Range(0, theWave.Length);
        }

        float randomX = Random.Range(-spawnPoints[spawnPointIndex].GetComponent<BoxCollider2D>().size.x, spawnPoints[spawnPointIndex].GetComponent<BoxCollider2D>().size.x) * 0.5f;
        float randomY = Random.Range(-spawnPoints[spawnPointIndex].GetComponent<BoxCollider2D>().size.y, spawnPoints[spawnPointIndex].GetComponent<BoxCollider2D>().size.y) * 0.5f;

        // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
        //Instantiate (Zombie, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        GameObject spawnObject = Instantiate<GameObject>(theWave[enemySpawnId]);
        spawnObject.transform.position = new Vector2(randomX + spawnPoints[spawnPointIndex].transform.position.x, randomY + spawnPoints[spawnPointIndex].transform.position.y);
        waveLength -= 1;
    }
}
