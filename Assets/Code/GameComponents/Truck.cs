using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Truck : MonoBehaviour
{
    private Launcher launcher;

    private void Start()
    {
        launcher = GetComponentInChildren<Launcher>();
        StateBus.CanShoot = true;
        GetComponentInChildren<Button>().onClick.AddListener(Shoot);
    }

    private void Shoot()
    {
        if (StateBus.CanShoot)
        {
            StateBus.PlayerShot += true;
            StateBus.CanShoot = false;
            launcher.Shoot();
        }
    }
}
