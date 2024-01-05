using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionAnimation : MonoBehaviour
{
 
[SerializeField] GameObject explosionPrefab;
[SerializeField] Transform explosionLocation;

void Start()
{
    var newExplosion = Instantiate(explosionPrefab, explosionLocation);
    newExplosion.transform.parent = null;
    newExplosion.transform.Rotate(0, 0, Random.Range(-30, 30));
}

}
