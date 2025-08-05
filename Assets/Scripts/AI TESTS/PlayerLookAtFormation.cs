using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLookAtFormation : MonoBehaviour
{
    [Header("Formation Position & Rotation Objects")]
    [SerializeField] private Transform unitRotationTarget;
    [SerializeField] private Transform formUpTarget;

    [Header("Formation Variables")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private int unitsPerRow = 6;
    [SerializeField] private float spacing = 1.5f;
    [SerializeField] private bool defensiveStance;

    [Header("Units in formations")]
    [HideInInspector] public List<AIMeleeUnit> units = new List<AIMeleeUnit>();

    private void Start()
    {
        Debug.Log("Number of men in unit: " + units.Count);
    }

    private void Update()
    {
        if(defensiveStance)
            RotateFormationToTarget();
        
        BeginUnitFormation();
    }

    private void BeginUnitFormation()
    {
        if (formUpTarget == null)
        {
            Debug.LogWarning("Unit Has Nowhere To Form Up");
            return;
        }

        int rowCount = Mathf.CeilToInt((float)units.Count / unitsPerRow);

        float totalWidth = (unitsPerRow - 1) * spacing;
        float totalDepth = (rowCount - 1) * spacing;

        for (int i = 0; i < units.Count; i++)
        {
            int row = i / unitsPerRow;
            int col = i % unitsPerRow;

            float offsetX = (col * spacing) - (totalWidth / 2f);
            float offsetZ = -(row * spacing) + (totalDepth / 2f);

            Vector3 localOffset = new Vector3(offsetX, 0, offsetZ);
            Vector3 worldSlot = formUpTarget.TransformPoint(localOffset);

            units[i].SetMeleeFormationSlot(worldSlot);
        }
    }

    private void RotateFormationToTarget()
    {
        if (unitRotationTarget == null || formUpTarget == null) return;

        Vector3 directionToTarget = unitRotationTarget.position - formUpTarget.position;
        directionToTarget.y = 0f; // Prevent rotation on the Y axis (no tilting)

        if (directionToTarget != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
            formUpTarget.rotation = Quaternion.Slerp(formUpTarget.rotation, targetRotation, Time.deltaTime * 5f);
        }
    }


    public void RegisterUnit(AIMeleeUnit unit)
    {
        units.Add(unit);
    }

    public void UnregisterUnit(AIMeleeUnit unit)
    {
        units.Remove(unit);
    }
}
