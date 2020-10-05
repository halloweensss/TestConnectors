using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    [SerializeField] private SphereController sphereController;
    [SerializeField] private PlatformMovement platformMovement;
    [SerializeField] private LinkingObjects linkingObjects;
    
    [SerializeField] private Color activeColor;
    [SerializeField] private Color possibleColor;
    
    private Color _baseColor;

    public Action<ObjectController> OnClick;
    public Action<ObjectController> OnEnter;
    public Action<ObjectController> OnExit;
    public Action<ObjectController> OnMouseUp;
    public Action OnMove;

    private void OnEnable()
    {
        platformMovement.OnMove += OnMovePlatform;
        sphereController.OnMouseClick += OnClickToSphere;
        sphereController.OnMouseEnterSphere += OnEnterSphere;
        sphereController.OnMouseExitSphere += OnExitSphere;
        sphereController.OnMouseUpSphere += OnMouseUpToSphere;
    }
    
    private void OnDisable()
    {
        platformMovement.OnMove -= OnMovePlatform;
        sphereController.OnMouseClick -= OnClickToSphere;
        sphereController.OnMouseEnterSphere -= OnEnterSphere;
        sphereController.OnMouseExitSphere -= OnExitSphere;
        sphereController.OnMouseUpSphere -= OnMouseUpToSphere;
    }

    private void OnClickToSphere()
    {
        OnClick?.Invoke(this);
    }
    
    private void OnMouseUpToSphere()
    {
        OnMouseUp?.Invoke(this);
    }
    
    private void OnEnterSphere()
    {
        OnEnter?.Invoke(this);
    }
    
    private void OnExitSphere()
    {
        OnExit?.Invoke(this);
    }

    private void OnMovePlatform()
    {
        OnMove?.Invoke();
    }

    private void Start()
    {
        _baseColor = sphereController.GetColor();
    }

    public void SetBaseColor()
    {
        sphereController.SetColor(_baseColor);
    }
    
    public void SetActiveColor()
    {
        sphereController.SetColor(activeColor);
    }

    public void SetPossibleColor()
    {
        sphereController.SetColor(possibleColor);
    }

    public Vector3 GetPosition()
    {
        return sphereController.GetPosition();
    }

    public LinkingObjects GetLinkingObjects()
    {
        return linkingObjects;
    }
}
