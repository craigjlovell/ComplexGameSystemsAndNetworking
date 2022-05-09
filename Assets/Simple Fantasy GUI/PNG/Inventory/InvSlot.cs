using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvSlot : MonoBehaviour
{
    public GameObject prefab;

    public Vector2 invSize;
    public Vector2 slotSize;

    public RectOffset invPadding;
    public Vector2 invSpacing;

    GridLayoutGroup grid;

    // Start is called before the first frame update
    void Start()
    {
        grid = this.GetComponentInParent<GridLayoutGroup>();
        grid.constraintCount = (int)invSize.y;
        grid.cellSize = slotSize;
        grid.padding = invPadding;

        grid.spacing = invSpacing;

        for(int x = 1; x <= invSize.x; x++)
        {
            for (int y = 1; y <= invSize.y; y++)
            {
                GameObject slot = Instantiate(prefab) as GameObject;
                slot.transform.parent = this.transform;
                slot.name = "slot" + x + "_" + y;
                
            }
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
