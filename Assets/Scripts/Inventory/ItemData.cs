using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : MonoBehaviour
{
    public static ItemData instance { get; private set; }

    private void Awake()
    {
        instance = this;
    }

    public Transform pfItemWorld;

    public Sprite swordSprite;
    public Sprite gunSprite;
    public Sprite iceResourceSprite;
}
