using System.Collections;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    private const float Speed = 2f;
    private const float AdditionValue = 1.5f;
    private Rigidbody2D rb;
    private Transform parent,
        grandParent;
    private RectTransform rocketTransform;

    private ParticleSystem ps;

    private Vector2 cellPosition;
    private bool checkCell;
    private WaitForSecondsRealtime waitForSeconds;

    private void Start()
    {
        Camera camera = Camera.main;
        rocketTransform = GetComponent<RectTransform>();
        parent = rocketTransform.parent;
        grandParent = parent.parent;
        rb = gameObject.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        ps = GetComponentInChildren<ParticleSystem>();
        waitForSeconds = new WaitForSecondsRealtime(0.5f);
    }

    private void Update()
    {
        if (checkCell)
        {
            if (Vector2.Distance(rocketTransform.position, cellPosition) < 0.1f)
            {
                StateBus.Explosion += rocketTransform.position;
                ReturnOnStart();
            }
        }
    }

    public void ReturnOnStart()
    {
        rocketTransform.SetParent(parent);
        rocketTransform.SetSiblingIndex(0);
        rocketTransform.localEulerAngles = Vector2.zero;
        rocketTransform.anchoredPosition = Vector2.zero;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
        ps.Stop();
        checkCell = false;
    }

    public void Shot()
    {
        cellPosition = StateBus.CrosshairPosition;
        rocketTransform.SetParent(grandParent);
        rocketTransform.SetSiblingIndex(0);
        rb.AddForce(
            (StateBus.CrosshairPosition - (Vector2)rocketTransform.position) * Speed,
            ForceMode2D.Impulse
        );
        ps.Play();
        checkCell = true;
    }
}
