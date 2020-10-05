using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereController : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Transform transform;
    private Material _material;
    
    public Action OnMouseClick;
    public Action OnMouseEnterSphere;
    public Action OnMouseExitSphere;
    public Action OnMouseUpSphere;
    
    private void Start()
    {
        _material = meshRenderer.materials[0];
    }

    public void OnMouseDown()
    {
        OnMouseClick?.Invoke();
    }

    public void OnMouseUp()
    {
        OnMouseUpSphere?.Invoke();
    }

    public void OnMouseEnter()
    {
        OnMouseEnterSphere?.Invoke();
    }
    
    public void OnMouseExit()
    {
        OnMouseExitSphere?.Invoke();
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void SetColor(Color color)
    {
        _material.SetColor("_Color", color);
    }

    public Color GetColor()
    {
        return meshRenderer.material.GetColor("_Color");
    }
}
