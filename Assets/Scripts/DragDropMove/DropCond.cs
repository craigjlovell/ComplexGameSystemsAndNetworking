using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public abstract class DropCond
{
    public abstract bool Check(Draggable draggable);
}
