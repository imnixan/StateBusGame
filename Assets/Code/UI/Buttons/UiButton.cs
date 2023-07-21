using UnityEngine;
using System.Collections;

public abstract class UIButton : MonoBehaviour
{
    private const float MaxLoops = 100;

    [SerializeField]
    protected StateMachine.ButtonType buttonType;

    [SerializeField]
    protected Vector2 ShowPos,
        HidePos;
    protected WaitForFixedUpdate waitForFixedUpdate;

    protected Vector2 OldPosition;
    protected RectTransform rt;
    BoxCollider2D collider;

    protected virtual void Start()
    {
        Init();
        gameObject.tag = "Button";
    }

    public void ShowButton()
    {
        collider.enabled = true;
        StartCoroutine(MovePosition(ShowPos));
    }

    public void HideButton()
    {
        collider.enabled = false;
        StartCoroutine(MovePosition(HidePos));
    }

    protected IEnumerator MovePosition(Vector2 destination)
    {
        for (int i = 0; i < MaxLoops && rt.anchoredPosition != destination; i++)
        {
            float speed = Mathf.Pow(Vector2.Distance(OldPosition, destination) / (MaxLoops - i), 2);
            rt.anchoredPosition = Vector2.MoveTowards(rt.anchoredPosition, destination, speed);
            yield return waitForFixedUpdate;
        }
        OldPosition = rt.anchoredPosition;
    }

    private void Init()
    {
        rt = GetComponent<RectTransform>();
        collider = gameObject.AddComponent<BoxCollider2D>();
        collider.size = GetComponent<RectTransform>().sizeDelta;

        waitForFixedUpdate = new WaitForFixedUpdate();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Rocket"))
        {
            DoButtonStaff();
        }
    }

    protected abstract void DoButtonStaff();
}
