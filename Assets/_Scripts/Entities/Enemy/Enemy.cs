/* SCRIPT PARA CONTROLAR EL COMPORTAMIENTO DEL ENEMIGO */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    /* VARIABLES */

    // IDENTIFICADORES DEL ENEMIGO
    public string Id = "e_01";
    public string EntityName = "La Entidad";
    public string Description = "La entidad es una criatura sin forma que te acecha. ¿Por qué? Espero no saberlo nunca.";

    // REFERENCIAS
    [SerializeField] private GameObject _playerObject;
    // public EnemyMovement Movement;
    public NavMeshAgent Agent;

    // PARAMETROS DEL JUGADOR
    public int Health = 100;

    // CONFIGURACIONES DE NAVMESH
    public float AIUpdateInterval = 0.1f;
    public float Acceleration = 8;
    public float AngularSpeed = 120;
    public int AreaMask = -1;  // -1 significa que abarca todo.
    public int AvoidancePriority = 50;
    public float BaseOffset = 0;
    public float Height = 2f;
    public ObstacleAvoidanceType ObstacleAvoidanceType = ObstacleAvoidanceType.LowQualityObstacleAvoidance;
    public float Radius = 0.5f;
    public float Speed = 3f;
    public float StoppingDistance = 0.5f;

    /* METODOS BASICOS */

    // METODO UPDATE
    // Update es llamado una vez por frame
    private void Update()
    {

        // CONTROLAR ROTACION DEL ENEMIGO
        transform.LookAt(new Vector3 (_playerObject.transform.position.x, transform.position.y, _playerObject.transform.position.z));

    }

    // METODO QUE SE ACTIVA CON EL SCRIPT
    private void OnEnable()
    {
        SetupAgentFromConfiguration();
    }

    // METODO QUE SE ACTIVA AL DESACTIVAR EL SCRIPT
    private void OnDisable()
    {
        Agent.enabled = false;
    }

    /* METODOS DEL ENEMIGO */

    // METODO PARA FIJAR LOS PARAMETROS DEL ENEMIGO
    public void SetupAgentFromConfiguration() 
    {
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
