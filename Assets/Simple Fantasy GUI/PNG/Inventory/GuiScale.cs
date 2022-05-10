using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GuiScale : MonoBehaviour
{
    float parentRect = 0.0f;
    public float headerSize = 25.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetSize()
    {
        RectTransform thisRect = this.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (parentRect == 0.0f)
        {
            Debug.Log(transform.parent.GetComponent<RectTransform>().rect.width);
            parentRect = transform.parent.GetComponent<RectTransform>().rect.width;
            transform.GetComponent<RectTransform>().sizeDelta = new Vector2(parentRect, headerSize);
        }
    }
}
