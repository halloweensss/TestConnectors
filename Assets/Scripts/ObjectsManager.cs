using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectsManager : MonoBehaviour
{
   [SerializeField] private GameObject objectPrefab;
   [SerializeField] private Main main;
   [SerializeField] private int countObjects;

   private List<ObjectController> _objects = new List<ObjectController>();

   public Action<ObjectController> OnClick;
   public Action<ObjectController> OnEnter;
   public Action<ObjectController> OnExit;
   public Action<ObjectController> OnMouseUp;
   
   private void Start()
   {
      CreateObjects(countObjects);
   }

   private void CreateObjects(int count)
   {
      for (int i = 0; i < count; i++)
      {
         float angle = Random.Range(0f, 360f);
         float radius = Random.Range(0, main.Radius);
         
         Vector3 position = new Vector3(radius * Mathf.Sin(angle), 
            main.transform.position.y, radius * Mathf.Cos(angle));
         
         CreateObject(position);
      }
   }

   private void CreateObject(Vector3 position)
   {
      ObjectController objectController = Instantiate(objectPrefab,position,Quaternion.identity)
         .GetComponent<ObjectController>();

      objectController.OnClick += OnClickToObject;
      objectController.OnEnter += OnEnterToObject;
      objectController.OnExit += OnExitToObject;
      objectController.OnMouseUp += OnMouseUp;
      
      _objects.Add(objectController);
   }

   public void OnClickToObject(ObjectController objectController)
   {
      OnClick?.Invoke(objectController);
   }
   
   public void OnMouseUpToObject(ObjectController objectController)
   {
      OnMouseUp?.Invoke(objectController);
   }
   
   public void OnEnterToObject(ObjectController objectController)
   {
      OnEnter?.Invoke(objectController);
   }
   
   public void OnExitToObject(ObjectController objectController)
   {
      OnExit?.Invoke(objectController);
   }
   
   private void DestroyAllObjects()
   {
      for (int i = 0; i < _objects.Count; i++)
      {
         _objects[i].OnClick -= OnClickToObject;
         _objects[i].OnEnter -= OnEnterToObject;
         _objects[i].OnExit -= OnExitToObject;
         _objects[i].OnMouseUp -= OnMouseUp;
         
         Destroy(_objects[i].gameObject);
      }
      
      _objects = new List<ObjectController>();
   }

   public void SetPossibleObjects()
   {
      foreach (var objectController in _objects)
      {
         objectController.SetPossibleColor();
      }
   }

   public void SetBaseColorObjects()
   {
      foreach (var objectController in _objects)
      {
         objectController.SetBaseColor();
      }
   }
}
