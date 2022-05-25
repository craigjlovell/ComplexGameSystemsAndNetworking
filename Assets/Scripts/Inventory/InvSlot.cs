using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using System;

public class InvSlot : MonoBehaviour
{
    public GameObject prefab;

    public Vector2 invSize;

    [Range(0, 100)]
    public int numOfSlots = 0;
    public Vector2 slotSize;
    public float editHeader;

    public RectOffset invPadding;
    public Vector2 invSpacing;

    GridLayoutGroup grid;
    GuiScale obj;    

    int numOfSlotsLastFrame = 0;
    Vector2 invSizeLastFrame = new Vector2(0, 0);

    public List<GameObject> invSlots = new List<GameObject>();    

    bool vecSlot = false; // if changing 
    bool intSlot = false;

    bool inventorySizeChangable = false;    

    void Awake()
    {
        invSizeLastFrame = invSize;
        numOfSlots = (int)invSize.x * (int)invSize.y;

        obj = gameObject.transform.GetComponentInChildren<GuiScale>();

        grid = GetComponentInParent<GridLayoutGroup>();
        grid.constraintCount = (int)invSize.y;

        InitialiseSlotsOnStart();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    
    void InitialiseSlotsOnStart()
    {
        for (int x = 1; x <= invSize.x; x++)
        {
            for (int y = 1; y <= invSize.y; y++)
            {
                GameObject slot = Instantiate(prefab) as GameObject;
                slot.transform.SetParent(transform);
                slot.name = "Slot " + "Row " + x + " Col " + y;
                invSlots.Add(slot);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        obj.headerSize = editHeader;

        grid.cellSize = slotSize;

        RectOffset temp = new RectOffset();

        temp.left = invPadding.left;
        temp.right = invPadding.right;
        temp.top = invPadding.top;
        temp.bottom = invPadding.bottom;

        grid.padding = temp;

        grid.spacing = invSpacing;

        if (inventorySizeChangable)
        {
            if (invSize != invSizeLastFrame)
                vecSlot = true;
            else
                vecSlot = false;

            if (numOfSlots != numOfSlotsLastFrame)
                intSlot = true;
            else
                intSlot = false;
        }

        if (vecSlot == true)
        {
            if (invSize.x > invSizeLastFrame.x || invSize.y > invSizeLastFrame.y )
            {
                AddInventorySlotsVector();
            }

            if (invSize.x < invSizeLastFrame.x || invSize.y < invSizeLastFrame.y )
            {
                RemoveInventorySlotsVector();
            }
        }

        if (intSlot == true)
        {

            if (numOfSlotsLastFrame < numOfSlots  )
            {
                AddInventorySlotsInt();
            }

            if (numOfSlotsLastFrame > numOfSlots )
            {
                RemoveInventorySlotsInt();
            }
        }

        numOfSlotsLastFrame = numOfSlots;
        invSizeLastFrame = invSize;

        if (!inventorySizeChangable)
            inventorySizeChangable = !inventorySizeChangable;
    }

    void AddInventorySlotsVector()
    {
        int tempNum = numOfSlots;
        numOfSlots = (int)invSize.x * (int)invSize.y;
        tempNum = numOfSlots - tempNum;

        for (int x = 1; x <= tempNum; x++)
        {
            GameObject slot = Instantiate(prefab) as GameObject;
            slot.transform.parent = this.transform;
            invSlots.Add(slot);
        }
        numOfSlots = invSlots.Count;
        grid.constraintCount = (int)invSize.y;
    }

    void AddInventorySlotsInt()
    {
        int tempNum = numOfSlots - numOfSlotsLastFrame;

        for (int x = 1; x <= tempNum; x++)
        {
            GameObject slot = Instantiate(prefab) as GameObject;
            slot.transform.parent = this.transform;
            invSlots.Add(slot);
        }
        numOfSlots = invSlots.Count;
        grid.constraintCount = (int)invSize.y;
    }

    void RemoveInventorySlotsVector()
    {
        int tempNum = numOfSlots;
        numOfSlots = (int)invSize.x * (int)invSize.y;
        tempNum = tempNum - numOfSlots;

        for (int x = 1; x <= tempNum; x++)
        {
            GameObject toRemove = invSlots[invSlots.Count - 1];

            invSlots.Remove(toRemove);
            Destroy(toRemove);
        }
        numOfSlots = invSlots.Count;
        grid.constraintCount = (int)invSize.y;
    }

    void RemoveInventorySlotsInt()
    {
        int tempNum = numOfSlotsLastFrame - numOfSlots;

        for (int x = 1; x <= tempNum; x++)
        {
            GameObject toRemove = invSlots[invSlots.Count - 1];

            invSlots.Remove(toRemove);
            Destroy(toRemove);
        }
        numOfSlots = invSlots.Count;
        grid.constraintCount = (int)invSize.y;
    }
}


