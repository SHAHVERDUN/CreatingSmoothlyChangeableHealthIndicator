using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Player _player;

    [SerializeField]
    private Slider _slider;

    [SerializeField]
    private Image _sliderFillRectangle;

    [SerializeField]
    private float _smoothingChangingValue;

    [SerializeField]
    private Gradient _gradient;

    private Coroutine _coroutineChangeSmoothlySliderValue;

    private void OnEnable()
    {
        _player.HealthChanged += SetNewStateHealthBar;
    }

    private void OnDisable()
    {
        _player.HealthChanged -= SetNewStateHealthBar;
    }

    private void Start()
    {
        _slider.value = _slider.maxValue;

        _sliderFillRectangle = _slider.fillRect.GetComponent<Image>();
    }

    private void SetNewStateHealthBar(float normalizedCountOfHealth)
    {
        SetNewSliderValue(normalizedCountOfHealth);
        SetNewFillColorForSliderRectangle(normalizedCountOfHealth);
    }

    private void SetNewFillColorForSliderRectangle(float normalizedValue)
    {
        _sliderFillRectangle.color = _gradient.Evaluate(normalizedValue);
    }

    private void SetNewSliderValue(float normalizedValue)
    {
        float targetSliderValue = normalizedValue * _slider.maxValue;

        RestartCoroutineChangeSmoothlyValue(targetSliderValue);
    }

    private void RestartCoroutineChangeSmoothlyValue(float targetValue)
    {
        if (_coroutineChangeSmoothlySliderValue != null)
        {
            StopCoroutine(_coroutineChangeSmoothlySliderValue);
        }

        _coroutineChangeSmoothlySliderValue = StartCoroutine(ChangeSmoothlySliderValue(targetValue));
    }

    private IEnumerator ChangeSmoothlySliderValue(float targetValue)
    {
        while (_slider.value != targetValue)
        {
            _slider.value = Mathf.MoveTowards(_slider.value, targetValue, _smoothingChangingValue);

            yield return null;
        }
    }
}