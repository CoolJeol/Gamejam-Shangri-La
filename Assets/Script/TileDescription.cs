using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TileDescription : MonoBehaviour
{
    public static TileDescription Instance;

    public GameObject descriptionObject;
    public TextMeshProUGUI objectNameText;
    public TextMeshProUGUI descriptionText;
    public Image Image; 

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetDescription(string description, string objectName, Sprite objectSprite)
    {
        descriptionObject.SetActive(true);
        descriptionText.text = description;
        objectNameText.text = objectName;
        Image.sprite = objectSprite;
    }
    
    public void HideDescription()
    {
        descriptionObject.SetActive(false);
    }
}