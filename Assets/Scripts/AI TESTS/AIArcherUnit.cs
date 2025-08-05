using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AIArcherUnit : MonoBehaviour
{
    private NavMeshAgent agent;
    private Vector3 targetSlot;
    [SerializeField] private Transform playerTarget;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        ArcherFormation formation = GetComponentInParent<ArcherFormation>();
        if (formation != null)
        {
            formation.RegisterUnit(this);
        }
    }

    private void Update()
    {
        LookAtPlayer();
        Debug.Log(agent.velocity);
    }

    public void SetArcherFormationSlot(Vector3 position)
    {
        targetSlot = position;
        agent.SetDestination(targetSlot);
    }

    private void LookAtPlayer()
    {
        this.transform.LookAt(playerTarget);
    }
}
