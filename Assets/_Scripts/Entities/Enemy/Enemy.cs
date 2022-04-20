using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public string Id = "e_01";
    public string EntityName = "La Entidad";
    public string Description = "La entidad es una criatura sin forma que te acecha. ¿Por qué? Espero no saberlo nunca.";

    [SerializeField] private GameObject PlayerObject;
    // public EnemyMovement Movement;
    public NavMeshAgent Agent;

    public int Health = 100;

    // Configuraciones de NavMesh
    public float AIUpdateInterval = 0.1f;
    public float Acceleration = 8;
    public float AngularSpeed = 120;
    public int AreaMask = -1; // -1 significa que abarca todo.
    public int AvoidancePriority = 50;
    public float BaseOffset = 0;
    public float Height = 2f;
    public ObstacleAvoidanceType ObstacleAvoidanceType = ObstacleAvoidanceType.LowQualityObstacleAvoidance;
    public float Radius = 0.5f;
    public float Speed = 3f;
    public float StoppingDistance = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(new Vector3 (PlayerObject.transform.position.x, transform.position.y, PlayerObject.transform.position.z));
    }

    void OnEnable()
    {
        SetupAgentFromConfiguration();
    }

    void OnDisable()
    {
        Agent.enabled = false;
    }

    public void SetupAgentFromConfiguration() {
        Agent.acceleration = this.Acceleration;
        Agent.angularSpeed = this.AngularSpeed;
        Agent.areaMask = this.AreaMask;
        Agent.avoidancePriority = this.AvoidancePriority;
        Agent.baseOffset = this.BaseOffset;
        Agent.height = this.Height;
        Agent.obstacleAvoidanceType = this.ObstacleAvoidanceType;
        Agent.radius = this.Radius;
        Agent.speed = this.Speed;
        Agent.stoppingDistance = this.StoppingDistance;
    }


    
}
