using System.Collections;
using UnityEngine;

public class ExplosionManager : MonoBehaviour
{
    [SerializeField]
    GameObject explosionPrefab;

    void Update()
    {
        if (StateBus.Explosion)
        {
            Instantiate(explosionPrefab, StateBus.Explosion, new Quaternion(), transform);
        }
    }
}
