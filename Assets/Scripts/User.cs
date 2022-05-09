using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class User : NetworkBehaviour
{
    // Update is called once per frame
    void Start()
    {

    }


    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit))
            {
                if(Vector3.Distance(hit.point, transform.position) < 2)
                {
                    Interactable target = hit.transform.GetComponent<Interactable>();
                    if (target)
                        CmdUse(target);
                }
            }
        }
    }

    [Command]
    public void CmdUse(Interactable target)
    {
        RpcUse(target);
    }

    [ClientRpc]
    public void RpcUse(Interactable target)
    {
        target.Use();
    }
}
