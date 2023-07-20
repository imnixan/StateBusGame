using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


    public class Instantinator : MonoBehaviour
    {

    public static   RectTransform CreateRectTransform(string name, Vector2 position, Transform parent)
    {
        return Instantiate(new GameObject(name), position, new Quaternion(), parent).AddComponent<RectTransform>();
    }
        
    public static Image CreateImage(string name, Vector2 position, Transform parent, Sprite sprite)
    {
        Image newImage = CreateRectTransform(name, position, parent).gameObject.AddComponent<Image>();
        newImage.sprite = sprite;
        return newImage;
    }


    public static RectTransform CreateCanvas(string name, Transform parent)
    {
        Canvas canvas = Instantiate(new GameObject(name), parent).AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = Camera.main;
        canvas.gameObject.AddComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvas.gameObject.AddComponent<GraphicRaycaster>();
        if(GameObject.FindGameObjectsWithTag("EventSystem").Length == 0)
        {
            GameObject eventSystem = Instantiate(new GameObject("EventSystem"), parent);
            eventSystem.tag = "EventSystem";
            eventSystem.AddComponent<EventSystem>();
            eventSystem.AddComponent<StandaloneInputModule>();
        }

        return canvas.GetComponent<RectTransform>();
    }
}
