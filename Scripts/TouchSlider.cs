using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TouchSlider : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    // Slider �n de�i�me durumlar�n� ba�ka scriptlere inform etmek ad�na de�i�ken olu�turduk.
    public UnityAction OnPointerDownEvent;
    public UnityAction<float> OnPointerDragEvent;
    public UnityAction OnPointerUpEvent;

    private Slider uiSlider;

    private void Awake()
    {
        uiSlider = GetComponent<Slider>();
        uiSlider.onValueChanged.AddListener(OnSliderValueChanged);
    }



    public void OnPointerDown(PointerEventData eventData)
    {
        if (OnPointerDownEvent != null)
        {
            OnPointerDownEvent.Invoke();
        }
        if (OnPointerDragEvent != null)
        {
            OnPointerDragEvent.Invoke(uiSlider.value);
        }
        
    }

    private void OnSliderValueChanged(float value)
    {
        if (OnPointerDragEvent != null)
        {
            OnPointerDragEvent.Invoke(value);
        }
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        if (OnPointerUpEvent != null)
        {
            OnPointerUpEvent.Invoke();
        }

        // Slider de�erini s�f�rla
        uiSlider.value = 0f;

    }

    private void OnDestroy()
    {
        // Listeners kald�r. (Haf�za i�in)
        uiSlider.onValueChanged.RemoveListener(OnSliderValueChanged);
    }

}
