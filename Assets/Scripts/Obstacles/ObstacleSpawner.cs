using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public float timeBetweenSpawns = 3f;
    public float startDelay = 0f;
    public GameObject prefab;

    void Start()
    {
        StartCoroutine(Spawn());        
    }

    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(startDelay);

        while (GameManager.Instance.gameActive)
        {
            Instantiate(prefab, transform.position, Quaternion.identity);

            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }
}
