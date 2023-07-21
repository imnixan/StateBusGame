using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class Enemy : MonoBehaviour
{
    protected float xBorder;

    public void Init()
    {
        Vector2 scale = transform.localScale;
        scale.x = GetOrientation();
        transform.localScale = scale;
        xBorder = Mathf.Abs(transform.position.x);
    }

    private void Update()
    {
        if (Mathf.Abs(transform.position.x) > xBorder)
        {
            Destroy(gameObject);
        }
    }

    protected abstract void StartFlying();

    private float GetOrientation()
    {
        return transform.position.x / Mathf.Abs(transform.position.x);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Rocket"))
        {
            StateBus.Explosion += transform.position;
            StateBus.EnemyKilled += true;
        }
    }
}
