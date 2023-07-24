using System.Collections;
using UnityEngine;

public class Jet : Enemy
{
    protected override void StartFlying()
    {
        rb.velocity = new Vector2(xBorder, 0) * 1.2f;
    }
}
