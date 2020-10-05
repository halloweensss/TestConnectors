using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Vector3 offset;

    [SerializeField] private Main main;

    private Transform _cameraTransform;
    
    void Start()
    {
        _cameraTransform = GetComponent<Transform>();
    }
    
    void Update()
    {
        _cameraTransform.position = main.transform.position + offset;
        _cameraTransform.LookAt(main.transform);
    }
}
