using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
   private Transform _target;
   public float Movementspeed = 100f;
   public float NextWaypointDistance = 3f;

   Path path;
   private int _currentWaypoint = 0;
   private bool _reachedEndOfPath = false;
   public bool IsTargetActive = false;

   Seeker seeker;
   Rigidbody2D rb2d;

   public void Initialize(Player player)
   {
      _target = player.transform;

   }

   public void Start()
   {
      seeker = GetComponent<Seeker>();
      rb2d = GetComponent<Rigidbody2D>();

      seeker.StartPath(rb2d.position, _target.position, OnPathComplete);
      InvokeRepeating("UpdatePath", 0f, 2f);

   }

   public void UpdatePath()
   {
      if (seeker.IsDone() && IsTargetActive)
      {
         seeker.StartPath(rb2d.position, _target.position, OnPathComplete);
         IsTargetActive = false;
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

    public void Update()
    {
      if (path == null)
         return;

      if (_currentWaypoint >= path.vectorPath.Count)
      {
         _reachedEndOfPath = true;
         return;
      } else
      {
         _reachedEndOfPath = false;
      }

      Vector2 direction = ((Vector2)path.vectorPath[_currentWaypoint] - rb2d.position).normalized;
      Vector2 force = direction * Movementspeed * Time.deltaTime;
      float distance = Vector2.Distance(rb2d.position, path.vectorPath[_currentWaypoint]);
      Debug.Log(force);
      if(IsTargetActive)
      rb2d.velocity = force;

      if (distance < NextWaypointDistance)
      {
         _currentWaypoint++;
      }
    }
}
