using UnityEngine;

public  class Enemy : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;

    [SerializeField] private float _speed;

    Vector3 _positionCorrect;

    private void Initialize()
    {
       _positionCorrect = transform.position;
    }

    private void Update()
    {
        Move();
    }


    private void Move()
    {      
        if (_positionCorrect.x -transform.position.x  < 5f)
            _rigidbody.velocity = new Vector2(-_speed, _rigidbody.velocity.y);
    }
}