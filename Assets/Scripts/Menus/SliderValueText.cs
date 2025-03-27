using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SliderValueText : MonoBehaviour
{
    [SerializeField] private Slider m_slider;
    [SerializeField] private Text m_text;

    public void Start()
    {
        UpdateSlider(m_slider.value);
    }

    private void UpdateSlider(float value)
    {
        m_text.text = value.ToString("F2");
    }
}
