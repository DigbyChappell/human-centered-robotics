using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class webcam : MonoBehaviour
{
    public string url = "http://10.0.0.150:8081/image7.jpg";
    public RawImage rawImage;

    // automatically called when game started
    void Start()
    {
        StartCoroutine(LoadFromLikeCoroutine()); // execute the section independently

        // the following will be called even before the load finished
        // rawImage.color = Color.red;
    }

    void Update()
    {
        StartCoroutine(LoadFromLikeCoroutine()); // execute the section independently

        // the following will be called even before the load finished
        //rawImage.color = Color.red;
    }

    // this section will be run independently
    private IEnumerator LoadFromLikeCoroutine()
    {
        Debug.Log("Loading ....");
        WWW wwwLoader = new WWW(url);   // create WWW object pointing to the url
        yield return wwwLoader;         // start loading whatever in that url ( delay happens here )

        Debug.Log("Loaded");
        rawImage.color = Color.white;              // set white
        rawImage.texture = wwwLoader.texture;  // set loaded image
    }
}

