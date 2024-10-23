using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject StickyNote001;
    public GameObject StickyNote002;
    public GameObject Text001;
    public GameObject Text002;

    private bool textOrNote;
    void Start()
    {
        textOrNote = false;
        StickyNote001.SetActive(false);
        StickyNote001.SetActive(false);
        Text001.SetActive(false);
        Text002.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(2))
        {
            textOrNote = !textOrNote;
        }

        if (textOrNote)
        {
            StickyNote001.SetActive(true);
            StickyNote002.SetActive(true);
            Text001.SetActive(false);
            Text002.SetActive(false);
        }
        else
        {
            StickyNote001.SetActive(false);
            StickyNote002.SetActive(false);
            Text001.SetActive(true);
            Text002.SetActive(true);
        }
    }
}
