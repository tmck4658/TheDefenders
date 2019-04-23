using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public int waveLength = 10;              // Reference to the player's heatlh.
    public GameObject Zombie;                // The enemy prefab to be spawned.
    public float spawnTime = 3f;             // How long between each spawn.
    public GameObject[] spawnPoints;          // An array of the spawn points this enemy can spawn from.


    void Start ()
    {
        // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
        InvokeRepeating ("Spawn", spawnTime, spawnTime);
    }


    void Spawn ()
    {
        // If the player has no health left...
        if(waveLength <= 0)
        {
            // ... exit the function.
            return;
        }

        // Find a random index between zero and one less than the number of spawn points.
        int spawnPointIndex = Random.Range (0, spawnPoints.Length);

        float randomX = Random.Range(-spawnPoints[spawnPointIndex].GetComponent<BoxCollider2D>().size.x, spawnPoints[spawnPointIndex].GetComponent<BoxCollider2D>().size.x) * 0.5f;
        float randomY = Random.Range(-spawnPoints[spawnPointIndex].GetComponent<BoxCollider2D>().size.y, spawnPoints[spawnPointIndex].GetComponent<BoxCollider2D>().size.y) * 0.5f;

        // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
        //Instantiate (Zombie, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        GameObject spawnObject = Instantiate<GameObject>(Zombie);
        spawnObject.transform.position = new Vector2(randomX + spawnPoints[spawnPointIndex].transform.position.x, randomY + spawnPoints[spawnPointIndex].transform.position.y);
        waveLength -= 1;
        Debug.Log(waveLength);
    }
}
