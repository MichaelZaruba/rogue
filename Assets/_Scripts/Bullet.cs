using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _damage;

    public Vector3 direction;

    public float Damage { get => _damage; set => _damage = value; }

    void Update()
    {
        transform.Translate(_speed * direction * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            other.gameObject.GetComponent<Player>().GetDamage(_damage);
            Destroy(this.gameObject);
        }
        if (other.gameObject.GetComponent<Ground>())
        {
            Destroy(this.gameObject);
        }
    }
}
