using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvSlot : MonoBehaviour
{
    public GameObject prefab;
    public Vector2 invSize = new Vector2 (0,0);
    public float slotSize;
    public Vector2 windowSize;
    // Start is called before the first frame update
    void Start()
    {
        for(int x = 1; x <= invSize.x; x++)
        {
            for (int y = 1; y <= invSize.y; y++)
            {
                GameObject slot = Instantiate(prefab) as GameObject;
                slot.transform.parent = this.transform;
                slot.name = "slot" + x + "_" + y;
                slot.GetComponent<RectTransform>().anchoredPosition = new Vector3(windowSize.x/ (invSize.x+1)*x, windowSize.y / (invSize.y+1)* -y, 0);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
