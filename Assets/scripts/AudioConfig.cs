using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioConfig : MonoBehaviour
{
    [SerializeField] string label;
    [SerializeField] Slider slider;

    private void Awake()
    {
        slider.value = 1;
    }
    public void SetVolume(float value)
    {
        AudioManager.instance.SetVolume(label, value);
    }
}
