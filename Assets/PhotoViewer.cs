using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PhotoViewer : MonoBehaviour
{

	GameObject[] gameObj;
	Texture2D[] textList;

	string[] files;
	string pathPreFix;

	// Use this for initialization
	void Start()
	{
		/*
		//Change this to change pictures folder
		string path = @"C:\Users\jeanf\Downloads\Images\";

		pathPreFix = @"file://";

		files = System.IO.Directory.GetFiles(path, "*.png");

		gameObj = GameObject.FindGameObjectsWithTag("Image");

		StartCoroutine(LoadImages());
		*/
	}


	void Update()
	{

	}

	public void LoadImagesAfterStart()
    {
		//Change this to change pictures folder
		string path = @"C:\Users\jeanf\Downloads\Images\";

		pathPreFix = @"file://";

		files = System.IO.Directory.GetFiles(path, "*.png");

		gameObj = GameObject.FindGameObjectsWithTag("Image");

		StartCoroutine(LoadImages());
	}

	private IEnumerator LoadImages()
	{
		//load all images in default folder as textures and apply dynamically to plane game objects.
		//6 pictures per page
		textList = new Texture2D[files.Length];

		int dummy = 0;
		foreach (string tstring in files)
		{

			string pathTemp = pathPreFix + tstring;
			WWW www = new WWW(pathTemp);
			yield return www;
			Texture2D texTmp = new Texture2D(1024, 1024, TextureFormat.DXT1, false);
			www.LoadImageIntoTexture(texTmp);

			textList[dummy] = texTmp;
			

			gameObj[dummy].GetComponent<Renderer>().material.SetTexture("_MainTex", texTmp);
			dummy++;
		}

	}
}
