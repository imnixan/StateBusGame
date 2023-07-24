using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class Enemy : ExplodableObject
{
    protected float xBorder;
    protected Rigidbody2D rb;

    public virtual void Init()
    {
        base.Init();
        Vector2 scale = transform.localScale;
        scale.x = GetOrientation();
        transform.localScale = scale;
        xBorder = transform.position.x * -1;
        rb = gameObject.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        StartFlying();
    }

    private void Update()
    {
        if (Mathf.Abs(transform.position.x) > Mathf.Abs(xBorder))
        {
            Destroy(gameObject);
        }
    }

    protected abstract void StartFlying();

    private float GetOrientation()
    {
        return transform.position.x / Mathf.Abs(transform.position.x);
    }

    protected override void OnExplode()
    {
        StateBus.Explosion += transform.position;
        StateBus.EnemyKilled += true;
        Destroy(gameObject);
    }
}
