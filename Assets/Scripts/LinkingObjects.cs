using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkingObjects : MonoBehaviour
{
    [SerializeField] private ObjectController thisObject;
    [SerializeField] private LineRenderer lineRenderer;
    
    private List<ObjectController> _linkObjects = new List<ObjectController>();

    private void Start()
    {
        thisObject.OnMove += RecalculatePosition;
        RecalculatePosition();
    }

    private bool CheckDuplicate(ObjectController objectController)
    {
        foreach (var linkObject in _linkObjects)
        {
            if (objectController == linkObject)
            {
                return true;
            }
        }

        return false;
    }
    
    public void Add(ObjectController objectController)
    {
        if (CheckDuplicate(objectController))
        {
            return;
        }
        
        objectController.OnMove += RecalculatePosition;
        
        _linkObjects.Add(objectController);
        AddPositionLine(objectController.GetPosition());
    }

    private void AddPositionLine(Vector3 position)
    {
        lineRenderer.positionCount += 1;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, position);
        lineRenderer.positionCount += 1;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, thisObject.GetPosition()); 
    }

    public void RecalculatePosition()
    {
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, thisObject.GetPosition());
        foreach (var linkObject in _linkObjects)
        {
            AddPositionLine(linkObject.GetPosition());
        }
    }
}
