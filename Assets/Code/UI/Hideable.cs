using System.Collections;
using UnityEngine;

public abstract class Hideable : MonoBehaviour
{
    [SerializeField]
    protected Vector2 ShowPos,
        HidePos;

    protected const float MaxLoops = 100;
    protected Vector2 OldPosition;
    protected RectTransform rt;
    protected float birthTime;

    protected virtual void Start()
    {
        birthTime = Time.time;
    }

    public virtual void ShowButton()
    {
        birthTime = Time.time;
        StartCoroutine(MovePosition(ShowPos));
    }

    public virtual void HideButton()
    {
        StartCoroutine(MovePosition(HidePos));
    }

    private void CheckRt()
    {
        if (!rt)
        {
            rt = GetComponent<RectTransform>();
        }
    }

    protected IEnumerator MovePosition(Vector2 destination)
    {
        CheckRt();
        WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();
        OldPosition = rt.anchoredPosition;
        for (int i = 0; i < MaxLoops && rt.anchoredPosition != destination; i++)
        {
            float speed = Mathf.Pow(Vector2.Distance(OldPosition, destination) / (MaxLoops - i), 2);
            rt.anchoredPosition = Vector2.MoveTowards(rt.anchoredPosition, destination, speed);
            yield return waitForFixedUpdate;
        }
    }
}
