using System.Collections;
using UnityEngine;

public class Helicopter : Enemy
{
    private Vector3 rotorRotation = new Vector3(0, 0, 50);
    private WaitForSeconds waitForSeconds;
    private Transform heliTransform;

    public override void Init()
    {
        base.Init();
        heliTransform = transform;
        waitForSeconds = new WaitForSeconds(0.5f);
    }

    protected override void StartFlying()
    {
        rb.velocity = new Vector2(xBorder, 0) / 3;
        StartCoroutine(ChangeHeight());
    }

    IEnumerator ChangeHeight()
    {
        while (true)
        {
            rb.velocity = new Vector2(xBorder, Random.Range(-5, 5)) / 3;
            yield return waitForSeconds;
        }
    }

    private void FixedUpdate()
    {
        foreach (Transform rotor in heliTransform)
        {
            rotor.Rotate(rotorRotation);
        }
    }
}
