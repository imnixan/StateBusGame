using System.Collections;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    private Transform launcherTransform;
    private Rocket rocket;

    private void Start()
    {
        launcherTransform = transform;
        rocket = GetComponentInChildren<Rocket>();
    }

    private void Update()
    {
        RotateLauncher();
    }

    private void RotateLauncher()
    {
        launcherTransform.right = StateBus.CrosshairPosition - (Vector2)launcherTransform.position;
    }

    public void Shoot()
    {
        rocket.Shot();
    }
}
