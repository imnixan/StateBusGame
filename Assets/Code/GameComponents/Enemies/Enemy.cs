using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class Enemy : MonoBehaviour
{
    protected float xBorder;
    protected Rigidbody2D rb;
    BoxCollider2D collider;

    public virtual void Init()
    {
        Vector2 scale = transform.localScale;
        scale.x = GetOrientation();
        transform.localScale = scale;
        xBorder = transform.position.x * -1;
        rb = gameObject.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        collider = gameObject.AddComponent<BoxCollider2D>();
        collider.size = GetComponent<RectTransform>().sizeDelta;
        collider.isTrigger = true;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Rocket"))
        {
            StateBus.Explosion += transform.position;
            StateBus.EnemyKilled += true;
            Destroy(gameObject);
        }
    }
}
