using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemHolder : MonoBehaviour
{
    [SerializeField] Image image;

    public void SetSprite(Sprite sprite) {
        image.sprite = sprite;
    }
}
