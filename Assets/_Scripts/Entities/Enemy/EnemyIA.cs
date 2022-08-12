using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyIA : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent; //Reference of the enemy
    [SerializeField] private bool _isChasing; //If enemy is chasing player
    [SerializeField] private float _radius; //Radius of FOV
    [Range(0,360)]
    [SerializeField] private float _angle; //Angle of FOV
    private GameObject _playerRef; //Reference to the player
    [SerializeField] private LayerMask _targetMask; //Layer of the player
    [SerializeField] private LayerMask _obstructionMask; //Layer of environment
    [SerializeField] private Transform[] _waypoints; //Point of patrol
    private int _waypointIndex; //In which point is or is going the enemy
    private Vector3 targetPosition; // Position the enemy suppose to go
    private bool _canSeePlayer; //If Enemy can see player
    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _waypointIndex = 0;
        UpdateDestination();
    }
    private void Start()
    {
        _playerRef = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVRoutine());
    }
    private void Update()
    {
        //Is chasing the player or not
        if (_isChasing)
        {
            _navMeshAgent.destination = _playerRef.transform.position;
        }
        else
        {
            Patrolling();
        }
        
    }

    private void Patrolling()
    {
        if (Vector3.Distance(transform.position, targetPosition)<1)
        {
            IterateWayPointIndex();
            UpdateDestination();
        }
    }

    private void IterateWayPointIndex()
    {
        //If the enemy went to all points reset else just go next
        _waypointIndex++;
        if(_waypointIndex == _waypoints.Length)
        {
            _waypointIndex = 0;
            //Shuffle the vector
            for (int i = 0; i < _waypoints.Length; i++)
            {
                Transform temp = _waypoints[i];
                int rnd = UnityEngine.Random.Range(0, i);
                _waypoints[i] = _waypoints[rnd];
                _waypoints[rnd] = temp;
            }
        }
    }

    private void UpdateDestination()
    {
        //When we need a new destination go to next one
        targetPosition = _waypoints[_waypointIndex].position;
        _navMeshAgent.destination = targetPosition;
    }

    private IEnumerator FOVRoutine()
    {
        //Every 0.2 seconds, the enemy check his FOV
        float delay = 0.2f;
        WaitForSeconds wait = new WaitForSeconds(delay);
        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, _radius, _targetMask);//Checks if the player is in the enemy radius
        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform; //The player is in the enemy radius
            Vector3 directionToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, directionToTarget) < _angle / 2)//Checks if the player is in the enemy angle
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                //RaycastHit hit;
                //!Physics.Raycast(transform.position, directionToTarget, out hit, distanceToTarget, _obstructionMask)
                RaycastHit[] hits = Physics.RaycastAll(transform.position, directionToTarget, distanceToTarget, _obstructionMask);//Checks if there is something that block the view of the enemy
                if (hits.Length == 0) 
                {
                    _canSeePlayer = true; //All true
                }
                else
                {
                    _canSeePlayer = false; //There is something blocking the view of the enemy
                }
            }else
            {
                _canSeePlayer = false; //The player is not in the angle
            }
        }else if (_canSeePlayer)
        {
            _canSeePlayer = false; //The player is not in the radius or was but not anymore
        }
    }


    //Gets for references in other scripts
    public float getRadius()
    {
        return _radius;
    }
    public float getAngle()
    {
        return _angle;
    }
    public bool getCanSeePlayer()
    {
        return _canSeePlayer;
    }

    public GameObject getplayerRef()
    {
        return _playerRef;
    }
}
