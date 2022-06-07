//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.EventSystems;
//using System;

//public class DragCommand : Draggable
//{
//    public override void Awake()
//    {
//        base.Awake();
//        OnEndDragHandler += DragStopped;
//    }

//    private void DragStopped(PointerEventData eventData, bool dropSuccess)
//    {
//        if (StartParent.GetComponent<InventorySlot>() != null && !dropSuccess)
//        {
//            DestroyImmediate(gameObject);
//        }

//    }
//}
