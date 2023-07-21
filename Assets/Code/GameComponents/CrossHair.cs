using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CrossHair : MonoBehaviour
{
    [SerializeField]
    private Color red,
        gray;

    private const float MinYPos = -3;

    private Transform crosshairTransform;
    private Camera camera;
    private bool _fingerHold;
    private Color _currentColor;
    private Image image;
    private Vector2 fingerPosition;

    private bool FingerHold
    {
        get
        {
            if (Input.GetMouseButtonDown(0) && !_fingerHold)
            {
                _fingerHold = true;
            }
            if (Input.GetMouseButtonUp(0) && _fingerHold)
            {
                _fingerHold = false;
            }

            return _fingerHold;
        }
    }
    private Color CurrentColor
    {
        get { return _currentColor; }
        set
        {
            if (_currentColor != value)
            {
                _currentColor = value;
                image.color = _currentColor;
            }
        }
    }

    void Start()
    {
        image = GetComponent<Image>();
        crosshairTransform = transform;
        camera = Camera.main;
        CurrentColor = red;
    }

    private void Update()
    {
        if (FingerHold)
        {
            fingerPosition = (Vector2)camera.ScreenToWorldPoint(Input.mousePosition);
            if (fingerPosition.y > MinYPos)
            {
                crosshairTransform.position = fingerPosition;
                StateBus.CrosshairPosition = crosshairTransform.position;
            }
        }
        UpdateCrosshairColor();
    }

    private void UpdateCrosshairColor()
    {
        if (StateBus.PlayerShot)
        {
            CurrentColor = gray;
        }
        else if (CurrentColor == gray && StateBus.CanShoot)
        {
            CurrentColor = red;
        }
    }
}
