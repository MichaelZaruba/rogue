using UnityEngine;

public class Enemy : MonoBehaviour
{
   [SerializeField] private Rigidbody2D _rigidbody;

   [SerializeField] private float _speed;

   [SerializeField] private int _maxHealth = 100;

   [SerializeField] private int _currentHealth = 100;


   Vector3 _positionCorrect;

   private void Start()
   {
      _currentHealth = _maxHealth;
   }

   public void takeDamage(int damage)
   {
      _currentHealth -= damage;

      //there should be damage-taking animation but we dont have one

      if (_currentHealth <= 0)
      {
         Die();
      }
   }

   void Die()
    {
      Debug.Log("Enemy Died!");
      GetComponent<Collider2D>().enabled = false;
      this.enabled = false;
   }

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