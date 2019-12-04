/*
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Net;
using System.IO;
public class webcam : MonoBehaviour
{
    [HideInInspector]
    public Byte[] JpegData;
    [HideInInspector]
    public string resolution = "480x360";
    private Texture2D texture;
    private Stream stream;
    private WebResponse resp;
    public MeshRenderer frame;
    public void Start()
    {
        GetVideo();
    }
    public void StopStream()
    {
        stream.Close();
        resp.Close();
    }
    public void GetVideo()
    {
        texture = new Texture2D(2, 2);
        //Working
        string url = "http://10.0.0.150:8081/video7.mjpg";
        //string url = "http://10.0.0.150:8081/mjpg/video.mjpg";
        //string url = "http://24.172.4.142/mjpg/video.mjpg?COUNTER";
        //Working
        //string url = "http://200.36.58.250/mjpg/video.mjpg?resolution=640x480";

        HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
        //For testing
        // req.ProtocolVersion = HttpVersion.Version10;
        // get response
        resp = req.GetResponse();
        // get response stream
        stream = resp.GetResponseStream();
        frame.material.color = Color.white;
        StartCoroutine(GetFrame());
    }
    public IEnumerator GetFrame()
    {
        // Byte [] JpegData = new Byte[105536];
        //   Byte [] JpegData = new Byte[205536];
        Byte[] JpegData = new Byte[505536];
        while (true)
        {
            int bytesToRead = FindLength(stream);
            //Debug.Log("bytes to read: " + bytesToRead);

            if (bytesToRead == -1)
            {
                yield break;
            }
            int leftToRead = bytesToRead;
            while (leftToRead > 0)
            {
                leftToRead -= stream.Read(JpegData, bytesToRead - leftToRead, leftToRead);
                yield return null;
            }
            MemoryStream ms = new MemoryStream(JpegData, 0, bytesToRead, false, true);
            texture.LoadImage(ms.GetBuffer());
            frame.material.mainTexture = texture;
            frame.material.color = Color.white;
            stream.ReadByte(); // CR after bytes
            stream.ReadByte(); // LF after bytes
        }
    }
    int FindLength(Stream stream)
    {
        int b;
        string line = "";
        int result = -1;
        bool atEOL = false;
        while ((b = stream.ReadByte()) != -1)
        {
            if (b == 10) continue; // ignore LF char
            if (b == 13)
            { // CR
                if (atEOL)
                {  // two blank lines means end of header
                    stream.ReadByte(); // eat last LF
                    return result;
                }
                if (line.StartsWith("Content-Length:"))
                {
                    result = Convert.ToInt32(line.Substring("Content-Length:".Length).Trim());
                }
                else
                {
                    line = "";
                }
                atEOL = true;
            }
            else
            {
                atEOL = false;
                line += (char)b;
            }
        }
        return -1;
    }
}
*/
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

