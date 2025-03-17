using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextScaler : MonoBehaviour
{
    [SerializeField] private float scaleFactor;
    private Text buttonText;
    private Vector3 originalScale;

    // Start is called before the first frame update
    void Start()
    {
        buttonText = GetComponentInChildren<Text>();
        originalScale = buttonText.rectTransform.localScale;
    }

    public void ScaleUp()
    {
        buttonText.rectTransform.localScale = originalScale * scaleFactor;
    }

    public void ScaleDown()
    {
        buttonText.rectTransform.localScale = originalScale;
    }
}
