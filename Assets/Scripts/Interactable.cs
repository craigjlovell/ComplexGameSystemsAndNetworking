using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Mirror;

public class Interactable : NetworkBehaviour
{
    public UnityEvent onUsed;
    
    public void Use()
    {
        onUsed.Invoke();
    }
}
