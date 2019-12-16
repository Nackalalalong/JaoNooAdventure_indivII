using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformationEffectSpawner: MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject starBurst;
    public void SpawnTransformationEffect(){
        GameObject gameObject = Instantiate(starBurst, spawnPoint.position, Quaternion.identity);
        gameObject.transform.SetParent(spawnPoint.parent);
        gameObject.SetActive(true);
        Destroy(gameObject, 0.5f);
    }
}
