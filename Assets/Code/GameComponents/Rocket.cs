using System.Collections;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    private const float Speed = 1f;
    private const float AdditionValue = 1.5f;
    private Rigidbody2D rb;
    private BoxCollider2D collider;
    private Transform parent,
        grandParent;
    private RectTransform rocketTransform;

    private Vector2 minBorders,
        maxBorders;
    private Vector2 StartPosition;

    private void Start()
    {
        Camera camera = Camera.main;
        rocketTransform = GetComponent<RectTransform>();
        parent = rocketTransform.parent;
        grandParent = parent.parent;
        minBorders = camera.ViewportToWorldPoint(Vector2.zero);
        maxBorders = camera.ViewportToWorldPoint(new Vector2(1, 1));
        collider = gameObject.AddComponent<BoxCollider2D>();
        collider.size = GetComponent<RectTransform>().sizeDelta;
        rb = gameObject.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }

    private void Update()
    {
        Vector2 position = rocketTransform.position;
        if (
            position.x > maxBorders.x + AdditionValue
            || position.x < minBorders.x - AdditionValue
            || position.y > maxBorders.y + AdditionValue
            || position.y < minBorders.y - AdditionValue
        )
        {
            ReturnOnStart();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Button"))
        {
            StateBus.Explosion += rocketTransform.position;
            ReturnOnStart();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            ReturnOnStart();
        }
    }

    private void ReturnOnStart()
    {
        StateBus.CanShoot = true;
        rocketTransform.SetParent(parent);
        rocketTransform.SetSiblingIndex(0);
        rocketTransform.localEulerAngles = Vector2.zero;
        rocketTransform.anchoredPosition = Vector2.zero;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
    }

    public void Shot()
    {
        rocketTransform.SetParent(grandParent);
        rocketTransform.SetSiblingIndex(0);
        rb.AddForce(
            (StateBus.CrosshairPosition - (Vector2)rocketTransform.position) * Speed,
            ForceMode2D.Impulse
        );
    }
}
