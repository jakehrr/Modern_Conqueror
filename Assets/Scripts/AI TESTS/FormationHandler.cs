using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationHandler : MonoBehaviour
{
    [SerializeField] private Transform unitMoveToTarget;
    [SerializeField] private Transform formUpTarget;

    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private int unitsPerRow = 6;
    [SerializeField] private float spacing = 1.5f;

    [HideInInspector] public List<AIUnit> units = new List<AIUnit>();

    private void Update()
    {
        BeginUnitFormation();
    }

    private void BeginUnitFormation()
    {
        if(formUpTarget == null)
        {
            Debug.LogWarning("Unit Has Nowhere To Form Up");
            return;
        }

        for (int i = 0; i < units.Count; i++)
        {
            int row = i / unitsPerRow;
            int col = i % unitsPerRow;

            Vector3 localOffset = new Vector3(col * spacing, 0, -row * spacing);
            Vector3 worldSlot = formUpTarget.TransformPoint(localOffset);

            units[i].SetFormationSlot(worldSlot);
        }
    }

    public void RegisterUnit(AIUnit unit)
    {
        units.Add(unit);
    }

    public void UnregisterUnit(AIUnit unit)
    {
        units.Remove(unit);
    }
}
