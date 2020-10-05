using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    [SerializeField] private Transform transform;
    
    private float _zCoord;
    private Vector3 _offset;

    public Action OnMove;

    private void OnMouseDown()
    {
        _zCoord = Camera.main.WorldToScreenPoint(transform.position).z;
        _offset = transform.position - GetMousePositionToWorldPoint(_zCoord);
    }

    private void OnMouseDrag()
    {
        transform.position = GetMousePositionToWorldPoint(_zCoord) + _offset;
        OnMove?.Invoke();
    }

    private Vector3 GetMousePositionToWorldPoint(float distance)
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = distance;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}
