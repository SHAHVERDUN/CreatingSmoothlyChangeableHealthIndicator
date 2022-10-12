using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Slider _slider;

    [SerializeField]
    private float _smoothingChangingValue;

    [SerializeField]
    private Gradient _gradient;

    private float _targetValue;

    private void Start()
    {
        _slider.value = _slider.maxValue;
    }

    public void SetNewState(float normalizedCountOfHealth)
    {
        SetNewValue(normalizedCountOfHealth);
        SetNewColor(normalizedCountOfHealth);
    }

    private void SetNewColor(float normalizedValue)
    {
        _slider.fillRect.GetComponent<Image>().color = _gradient.Evaluate(normalizedValue);
    }

    private void SetNewValue(float normalizedValue)
    {
        _targetValue = normalizedValue * _slider.maxValue;

        StartCoroutine(ChangeSmoothlyValue());
    }

    private IEnumerator ChangeSmoothlyValue()
    {
        while (_slider.value != _targetValue)
        {
            _slider.value = Mathf.MoveTowards(_slider.value, _targetValue, _smoothingChangingValue);

            yield return null;
        }
    }
}