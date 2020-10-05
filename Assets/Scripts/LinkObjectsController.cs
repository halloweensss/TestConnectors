using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkObjectsController : MonoBehaviour
{
   [SerializeField] private LineRenderer lineRenderer;
   [SerializeField] private ObjectsManager objectsManager;
   [SerializeField] private ObjectController _firstObject;
   [SerializeField] private ObjectController _secondObject;

   private bool _mouseDown;
   private void OnEnable()
   {
      objectsManager.OnClick += Link;
      objectsManager.OnEnter += PossibleLink;
      objectsManager.OnExit += ClearPossibleLink;
   }
   
   private void OnDisable()
   {
      objectsManager.OnClick -= Link;
      objectsManager.OnEnter -= PossibleLink;
      objectsManager.OnExit -= ClearPossibleLink;
   }

   private void ConnectPossibleLink(ObjectController objectController)
   {
      if (_firstObject != null && _firstObject != objectController && objectController != null)
      {
         _firstObject.GetLinkingObjects().Add(_secondObject);
      }
      Clear();
   }

   private void ClearPossibleLink(ObjectController objectController)
   {
      if (_firstObject != null && _mouseDown && _firstObject != objectController)
      {
         _secondObject.SetPossibleColor();
         _secondObject = null;
      }
   }
   private void PossibleLink(ObjectController objectController)
   {
      if (_firstObject != null && _mouseDown && _firstObject != objectController)
      {
         _secondObject = objectController;
         _secondObject.SetActiveColor();
      }
   }
   
   private void Link(ObjectController objectController)
   {
      if (_firstObject == null)
      {
         _firstObject = objectController;
         _firstObject.OnMove += RecalculatePosition;
         
         objectsManager.SetPossibleObjects();
         _firstObject.SetActiveColor();
         SetFirstPosition(_firstObject.GetPosition());
         return;
      }

      if (_firstObject != objectController)
      {
         _secondObject = objectController;
         _firstObject.GetLinkingObjects().Add(_secondObject);
         Clear();
      }
      else
      {
         Clear();
      }
   }

   private void Clear()
   {
      if (_firstObject != null)
      {
         _firstObject.OnMove -= RecalculatePosition;
      }

      _firstObject = null;
      _secondObject = null;
      lineRenderer.positionCount = 0;
      objectsManager.SetBaseColorObjects();
   }

   private void RecalculatePosition()
   {
      SetFirstPosition(_firstObject.GetPosition());
   }

   private void SetFirstPosition(Vector3 position)
   {
      lineRenderer.positionCount = 1;
      lineRenderer.SetPosition(0, position);
   }

   private void SetSecondPosition(Vector3 position)
   {
      lineRenderer.positionCount = 2;
      lineRenderer.SetPosition(1, position);
   }
   
   private void Update()
   {
      if (_firstObject != null)
      {
         SetSecondPosition(CalculatePosition());
      }
      if(Input.GetMouseButtonDown(0))
      {
         _mouseDown = true;
         if (!CheckObject())
         {
            Clear();
         }
      }

      if (Input.GetMouseButtonUp(0))
      {
         _mouseDown = false;
         if (!CheckObject())
         {
            Clear();
         }
         else
         {
            ConnectPossibleLink(_secondObject);
         }
         
      }
   }

   private Vector3 CalculatePosition()
   {
      float distance = Camera.main.WorldToScreenPoint(_firstObject.GetPosition()).z;
      Vector3 mousePos = Input.mousePosition;
      mousePos.z = distance;
      return Camera.main.ScreenToWorldPoint(mousePos);
   }

   private bool CheckObject()
   {
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      RaycastHit hit;

      Debug.DrawRay(ray.origin, ray.direction * 1000f, Color.red);
      if (Physics.Raycast(ray, out hit, Mathf.Infinity))
      {
         if (hit.transform.GetComponent<SphereController>() != null)
         {
            return true;
         }
      }

      return false;
   }
}
