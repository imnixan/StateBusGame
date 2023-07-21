using UnityEngine;
using System.Collections;
using UnityEngine.UI;

using TMPro;

public abstract class UIButton : MonoBehaviour
{
    private const float MaxLoops = 30;

    [SerializeField]
    protected StateMachine.ButtonType buttonType;

    [SerializeField]
    protected Vector2 ShowPos,
        HidePos;
    protected WaitForFixedUpdate waitForFixedUpdate;

    protected Vector2 OldPosition;

    private Rigidbody2D rb;

    private bool active;

    protected virtual void Start()
    {
        Init();
        InitRb();
    }

    public void ShowButton()
    {
        active = true;
        StartCoroutine(MovePosition(ShowPos));
    }

    public void HideButton()
    {
        active = false;
        StartCoroutine(MovePosition(HidePos));
    }

    protected IEnumerator MovePosition(Vector2 destination)
    {
        for (int i = 0; i < MaxLoops && (Vector2)transform.position != destination; i++)
        {
            float speed = Mathf.Pow(Vector2.Distance(OldPosition, destination) / (MaxLoops - i), 2);
            rb.MovePosition(Vector2.MoveTowards(transform.position, destination, speed));
            yield return waitForFixedUpdate;
        }
        OldPosition = transform.position;
    }

    private void InitRb()
    {
        rb = gameObject.AddComponent<Rigidbody2D>();
        rb.isKinematic = true;
        rb.gravityScale = 0;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
    }

    private void Init()
    {
        active = true;
        waitForFixedUpdate = new WaitForFixedUpdate();
        BoxCollider2D collider = gameObject.AddComponent<BoxCollider2D>();
        collider.size = GetComponent<RectTransform>().sizeDelta;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (active && collision.gameObject.CompareTag("Rocket"))
        {
            DoButtonStaff();
        }
    }

    protected abstract void DoButtonStaff();
}
