using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageSelector : MonoBehaviour
{
    //private ImageArray ImageArrayScript;
    //private GameObject ScriptHolder;

    public static RawImage[] rawImages;

    public RawImage blankImage;



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




        if(rawImages.Length > 0)
        {
            int RandomNum = Random.Range(0, rawImages.Length);

            rawImages[RandomNum].gameObject.SetActive(true);
            RemoveAt(ref rawImages, RandomNum);
        }
        else
        {
            blankImage.gameObject.SetActive(true);
        }
        




    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void RemoveAt<T>(ref T[] arr, int index)
    {
        for (int a = index; a < arr.Length - 1; a++)
        {
            // moving elements downwards, to fill the gap at [index]
            arr[a] = arr[a + 1];
        }
        // finally, let's decrement Array's size by one
        System.Array.Resize(ref arr, arr.Length - 1);
    }
}
