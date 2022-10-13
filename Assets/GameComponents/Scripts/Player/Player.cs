using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]

public class Player : MonoBehaviour
{
    [SerializeField]
    public event UnityAction<float> HealthChanged;

    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    [SerializeField]
    private float _maxHealth;

    [SerializeField]
    private float _healForce;

    [SerializeField]
    private Color _colorDeath;

    private Color _initialColor;

    private float _currentHealth;

    private float _normalizedCountOfHealth;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _initialColor = _spriteRenderer.color;

        _currentHealth = _maxHealth;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Shell shell))
        {
            TakeDamage(shell.DamageForce);
        }
    }

    private void ChangeHealthStatus()
    {
        if (_currentHealth < 0)
        {
            _currentHealth = 0;
        }
        else if (_currentHealth > _maxHealth)
        {
            _currentHealth = _maxHealth;
        }

        DetermineNormalizedCountOfHealth();
        SetNewColor();

        HealthChanged?.Invoke(_normalizedCountOfHealth);
    }

    private void DetermineNormalizedCountOfHealth()
    {
        _normalizedCountOfHealth = _currentHealth / _maxHealth;
    }

    private void SetNewColor()
    {
        _spriteRenderer.color = Color.Lerp(_colorDeath, _initialColor, _normalizedCountOfHealth);
    }

    private void TakeDamage(float damageForce)
    {
        _currentHealth -= damageForce;

        ChangeHealthStatus();
    }

    public void Heal()
    {
        _currentHealth += _healForce;

        ChangeHealthStatus();
    }
}