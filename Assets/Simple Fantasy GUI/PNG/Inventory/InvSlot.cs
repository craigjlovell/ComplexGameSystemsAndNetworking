using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvSlot : MonoBehaviour
{
    public GameObject prefab;

    public Vector2 invSize;
    public int numOfSlots = 0;
    public Vector2 slotSize;
    public float sizeHeader;

    public RectOffset invPadding;
    public Vector2 invSpacing;

    GridLayoutGroup grid;

    GuiScale obj;
    bool hasUpdated = false;

    Vector2 invSizeLastFrame = new Vector2(0, 0);

    public List<GameObject> invSlots = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        invSizeLastFrame = invSize;
        numOfSlots = (int)invSize.x * (int)invSize.y;

        grid = this.GetComponentInParent<GridLayoutGroup>();

        grid.constraintCount = (int)invSize.y;
        grid.cellSize = slotSize;
        grid.padding = invPadding;
        //sizeHeader = obj.headerSize;

        grid.spacing = invSpacing;

        for(int x = 1; x <= invSize.x; x++)
        {
            for (int y = 1; y <= invSize.y; y++)
            {
                GameObject slot = Instantiate(prefab) as GameObject;
                slot.transform.parent = this.transform;
                slot.name = "Slot " + "Row " + x + " Col " + y;
                invSlots.Add(slot);
            }
        }
    }

    private void Update()
    {
        if (invSize.x > invSizeLastFrame.x || invSize.y > invSizeLastFrame.y)
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
            grid.constraintCount = (int)invSize.y;
        }

        if (invSize.x < invSizeLastFrame.x || invSize.y < invSizeLastFrame.y)
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
            grid.constraintCount = (int)invSize.y;
        }

        invSizeLastFrame = invSize;
    }

    public void FirstInvCreation()
    {

    }
}
