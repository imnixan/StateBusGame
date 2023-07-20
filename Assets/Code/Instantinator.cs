using System.Collections;
using UnityEngine;
using UnityEngine.UI;


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

}
