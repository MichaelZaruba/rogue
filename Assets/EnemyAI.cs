using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
[RequireComponent(typeof(Seeker))]
public class EnemyAI : MonoBehaviour
{
   private Transform _target;
   public float Movementspeed = 100f;
   public float NextWaypointDistance = 3f;

   Path path;
   private int _currentWaypoint = 0;
   private bool _reachedEndOfPath = false;
   public bool IsTargetActive = false;

    private Enemy _enemy;
   Seeker seeker;
   Rigidbody2D _rigidbody;

   public void Initialize(Player player)
   {
      _target = player.transform;
       seeker = GetComponent<Seeker>();
        _enemy = GetComponent<Enemy>();
       _rigidbody = GetComponent<Rigidbody2D>();
        InvokeRepeating("UpdatePath", 0f, 1f);
    }



   public void UpdatePath()
   {
      if (seeker.IsDone() && _enemy.IsFindPlayer)
      { 
         seeker.StartPath(_rigidbody.position, _target.position, OnPathComplete);

      }
   } 
   public void OnPathComplete(Path p)
   {
      if (!p.error)
      {
         path = p;
         _currentWaypoint = 0;
      }
   }

    public void LateUpdate()
    {
        if (path == null)
            return;

        if (_currentWaypoint >= path.vectorPath.Count)
        {
            _reachedEndOfPath = true;
            return;
        }
        else
        {
            _reachedEndOfPath = false;
        }
        if (_enemy.IsFindPlayer)
            {
            Vector2 direction = ((Vector2)path.vectorPath[_currentWaypoint] - _rigidbody.position);
            direction.Normalize();
            Vector2 force = direction * Movementspeed * Time.deltaTime;
            float distance = Vector2.Distance(_rigidbody.position, path.vectorPath[_currentWaypoint]);
            _rigidbody.velocity = new Vector2(force.x, _rigidbody.velocity.y); ;

            if (distance < NextWaypointDistance)
            {
                _currentWaypoint++;
            }
        }
    }
    
}
