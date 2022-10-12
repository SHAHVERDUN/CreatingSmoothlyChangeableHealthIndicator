using UnityEngine;

public class Shell : MonoBehaviour
{
    [SerializeField]
    private float _timeToDestroy;

    [SerializeField]
    private float _damageForce;

    public float DamageForce => _damageForce;

    private void Start()
    {
        DestroyObjectDelayed();
    }

    private void DestroyObjectDelayed()
    {
        Destroy(gameObject, _timeToDestroy);
    }
}