using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuiScale : MonoBehaviour
{
    InvSlot slot;

    float parentRect = 0.0f;
    public float headerSize = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        slot.sizeHeader = headerSize;
    }

    public void SetSize()
    {
        RectTransform thisRect = this.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
            Debug.Log(transform.parent.GetComponent<RectTransform>().rect.width);
            parentRect = transform.parent.GetComponent<RectTransform>().rect.width;
            //headerSize = this.transform.GetComponent<RectTransform>().rect.height;
            transform.GetComponent<RectTransform>().sizeDelta = new Vector2(parentRect, headerSize);       
    }
}
