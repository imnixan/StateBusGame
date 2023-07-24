using System.Collections;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private float birthTime;

    private void Awake()
    {
        birthTime = Time.time;
    }

    public void DisposeExplosion()
    {
        Destroy(gameObject);
    }

    public float GetBirthTIme()
    {
        return birthTime;
    }
}
