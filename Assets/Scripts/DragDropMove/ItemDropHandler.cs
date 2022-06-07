using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropHandler : MonoBehaviour
{
	public List<DropCond> DropConditions = new List<DropCond>();
	public event Action<Draggable> OnDropHandler;

	public bool Accepts(Draggable draggable)
	{
		return DropConditions.TrueForAll(cond => cond.Check(draggable));
	}

	public void Drop(Draggable draggable)
	{
		OnDropHandler?.Invoke(draggable);
	}
}
