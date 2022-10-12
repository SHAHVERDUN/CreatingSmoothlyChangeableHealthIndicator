using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    private GameObject _shell;

    [SerializeField]
    private float _forceOfShot;

    public void Shoot()
    {
        GameObject shell = Instantiate(_shell, transform.position, Quaternion.identity);

        shell.GetComponent<Rigidbody2D>().AddForce(transform.right.normalized * _forceOfShot, ForceMode2D.Impulse);
    }
}