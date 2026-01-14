using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TileDescription : MonoBehaviour
{
    public static TileDescription Instance;

    public GameObject descriptionObject;
    public TextMeshProUGUI objectNameText;
    public TextMeshProUGUI descriptionText;

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

    public void SetDescription(string description, string objectName)
    {
        descriptionObject.SetActive(true);
        descriptionText.text = description;
        objectNameText.text = objectName;
    }
    
    public void HideDescription()
    {
        descriptionObject.SetActive(false);
    }
}