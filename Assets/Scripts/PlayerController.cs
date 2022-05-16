using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerController : NetworkBehaviour
{
    Animator animator;
    CharacterController cc;

    public InvObject inventory;

    public float MoveSpeed = 10.0f;
    public float RotateSpeed = 180.0f;
    public int index = 1;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
            return;

        float fwd = Input.GetAxis("Vertical");
        animator.SetFloat("Forward", Mathf.Abs(fwd));
        animator.SetFloat("Sense", Mathf.Sign(fwd));

        animator.SetFloat("Turn", Input.GetAxis("Horizontal"));
    }

    public void OnControllerColliderHit(ControllerColliderHit hit)
    {
         var item = hit.collider.GetComponent<Item>();
        if (item != null)
        {
            inventory.AddItem(item.item, 1);
            Destroy(hit.gameObject);
        }

    }
    private void OnApplicationQuit()
    {
        inventory.container.Clear();
    }
}
