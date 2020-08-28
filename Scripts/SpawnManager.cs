using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject _enemyPrefab = null;

    [SerializeField]
    private GameObject _enemyContainer = null;

    [SerializeField]
    private GameObject[] PowerupsPrefab = null;

    



    private bool _stopSpawning = false;

    void Start()
    {
       
        
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator  SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(2.5f);

        while (_stopSpawning == false)
        {
            
            Vector3 spawnPos = new Vector3(Random.Range(-9.55f, 9.55f), 7.5f, 0);
           
            GameObject newEnemy = Instantiate(_enemyPrefab, spawnPos, Quaternion.identity );
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5f);
        }

    }

    IEnumerator SpawnPowerupRoutine()
    {
        yield return new WaitForSeconds(2.5f);

        while (_stopSpawning == false)
        {
            Vector3 spawnPosPowerup = new Vector3(Random.Range(-9.55f, 9.55f), 7.5f, 0);
            /*
             if (Random.Range(0f, 1f) <= 0.355f)
            {
                Instantiate(_tripleShotPowerup, spawnPosPowerup, Quaternion.identity);
            }
            */

            //Instantiate(_PowerupsPrefab, spawnPosPowerup, Quaternion.identity);
            int randomPowerUp = Random.Range(0, 3);
            
                Instantiate(PowerupsPrefab[randomPowerUp], spawnPosPowerup, Quaternion.identity);

            yield return new WaitForSeconds(Random.Range(3, 8));


        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }


}
