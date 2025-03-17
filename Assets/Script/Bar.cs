using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    [SerializeField] private Image bar;

    public void SetAmount(float amount)
    {
        bar.fillAmount = amount;
    }
}
