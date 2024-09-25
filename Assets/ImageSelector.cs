using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageSelector : MonoBehaviour
{
    //private ImageArray ImageArrayScript;
    //private GameObject ScriptHolder;

    public RawImage[] rawImages;



    // Start is called before the first frame update
    void Start()
    {
        //ScriptHolder = GameObject.Find("ScriptHolder");
        //ImageArrayScript = ScriptHolder.GetComponent<ImageArray>();

        /*
         for (int i = 0; i<rawImages.Length;i++)
        {

        }
         */

        int RandomNum = Random.Range(0,rawImages.Length);

        rawImages[RandomNum].gameObject.SetActive(true);


        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
