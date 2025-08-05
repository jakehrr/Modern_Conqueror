using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AIUnit : MonoBehaviour
{
    private NavMeshAgent agent;
    private Vector3 targetSlot;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Register with the formation manager
        FormationHandler formation = GetComponentInParent<FormationHandler>();
        if (formation != null)
        {
            formation.RegisterUnit(this);
        }
    }

    private void Update()
    {
        Debug.Log(agent.speed);
    }

    public void SetFormationSlot(Vector3 position)
    {
        targetSlot = position;
        agent.SetDestination(targetSlot);
    }
}
