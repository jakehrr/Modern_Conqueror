using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AIMeleeUnit : MonoBehaviour
{
    private NavMeshAgent agent;
    private Vector3 targetSlot;
    [SerializeField] private Transform playerTarget;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        MeleeFormation formation = GetComponentInParent<MeleeFormation>();
        if (formation != null)
        {
            formation.RegisterUnit(this);
        }
    }

    private void Update()
    {
        LookAtPlayer();
    }

    public void SetMeleeFormationSlot(Vector3 position)
    {
        targetSlot = position;
        agent.SetDestination(targetSlot);
    }

    private void LookAtPlayer()
    {
        this.transform.LookAt(playerTarget);
    }
}
