using System.Collections;
using UnityEngine;

public abstract class ExplodableObject : Hideable
{
    protected BoxCollider2D collider;

    public virtual void Init()
    {
        collider = gameObject.AddComponent<BoxCollider2D>();
        collider.size = GetComponent<RectTransform>().sizeDelta;
        collider.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Explosion"))
        {
            Explosion explosion = collision.GetComponentInParent<Explosion>();
            if (birthTime < explosion.GetBirthTIme())
            {
                OnExplode();
            }
        }
    }

    protected abstract void OnExplode();
}
