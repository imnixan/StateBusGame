using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Truck : MonoBehaviour
{
    private const float ReloadTime = 0.5f;
    private const float MaxIterations = 100;
    private float reloadSpeed;

    private Launcher launcher;
    private Button fireButton;
    private Image reloadStatus;
    private WaitForSecondsRealtime waitForSecondsRealtime;

    private void Start()
    {
        launcher = GetComponentInChildren<Launcher>();
        StateBus.CanShoot = true;
        fireButton = GetComponentInChildren<Button>();
        fireButton.onClick.AddListener(Fire);
        waitForSecondsRealtime = new WaitForSecondsRealtime(ReloadTime / MaxIterations);
        reloadStatus = fireButton.transform.parent.GetComponent<Image>();
    }

    private void Fire()
    {
        if (StateBus.CanShoot)
        {
            StateBus.PlayerShot += true;
            StateBus.CanShoot = false;
            launcher.Shoot();
            reloadStatus.fillAmount = 0;
            StartCoroutine(ReloadGun());
        }
    }

    private void Update()
    {
        fireButton.interactable = StateBus.CanShoot;
    }

    private IEnumerator ReloadGun()
    {
        reloadSpeed = 1 / MaxIterations;
        for (int i = 0; i < MaxIterations; i++)
        {
            reloadStatus.fillAmount += reloadSpeed;
            yield return waitForSecondsRealtime;
        }
        StateBus.CanShoot = true;
    }
}
