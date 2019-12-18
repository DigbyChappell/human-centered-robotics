using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using UnityEngine;
using Tobii.XR;

public class showData : MonoBehaviour {

    private TcpListener tcpListener;
    private Thread tcpListenerThread;
    private TcpClient connectedTcpClient;
    public GameObject mainCam;
    public GameObject Canvas;
    // Use this for initialization
    void Start () {
        tcpListenerThread = new Thread(new ThreadStart(ListenForIncommingRequests));
        tcpListenerThread.IsBackground = true;
        tcpListenerThread.Start();
    }

    private void Update()
    {
        var gazeRay = TobiiXR.EyeTrackingData.GazeRay;

        // Check if gaze ray is valid
        if (TobiiXR.EyeTrackingData.GazeRay.IsValid)
        {
            // Eye Direction is a normalized direction vector in World Space
            var rayDirection = TobiiXR.EyeTrackingData.GazeRay.Direction;

            var rayDirectionLoc = transform.InverseTransformDirection(rayDirection);
            var gazeLine = Canvas.transform.localPosition.z / rayDirectionLoc.z;
            var xScreen = gazeLine * rayDirectionLoc.x;
            var yScreen = gazeLine * rayDirectionLoc.y;
            Debug.Log("X,Y coordinate on screen: x = " + xScreen + "y = " + yScreen);

            SendMessage("[" + "X" + xScreen + "Y" + yScreen + "]");

            // Blinking check
            var isLeftEyeBlinking = TobiiXR.EyeTrackingData.IsLeftEyeBlinking;
            var isRightEyeBlinking = TobiiXR.EyeTrackingData.IsRightEyeBlinking;
            if (isLeftEyeBlinking && isRightEyeBlinking)
            {
                Debug.Log("Eyes are closed control signal");
            }

        }
    }

    private void ListenForIncommingRequests()
    {
        try
        {
            // Create listener on localhost port 8052. 			
            tcpListener = new TcpListener(IPAddress.Parse("10.0.0.70"), 8052);
            tcpListener.Start();
            Debug.Log("Server is listening");
            Byte[] bytes = new Byte[64];
            while (true)
            {
                using (connectedTcpClient = tcpListener.AcceptTcpClient())
                {
                    // Get a stream object for reading 					
                    using (NetworkStream stream = connectedTcpClient.GetStream())
                    {
                        int length;
                        // Read incomming stream into byte arrary. 						
                        while ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
                        {
                            var incommingData = new byte[length];
                            Array.Copy(bytes, 0, incommingData, 0, length);
                            // Convert byte array to string message. 							
                            string clientMessage = Encoding.ASCII.GetString(incommingData);
                            Debug.Log("client message received as: " + clientMessage);
                        }
                    }
                }
            }
        }
        catch (SocketException socketException)
        {
            Debug.Log("SocketException " + socketException.ToString());
        }
    }

    private void SendMessage(string message)
    {
        if (connectedTcpClient == null)
        {
            return;
        }

        try
        {
            // Get a stream object for writing. 			
            NetworkStream stream = connectedTcpClient.GetStream();
            if (stream.CanWrite)
            {
                string serverMessage = message;
                // Convert string message to byte array.                 
                byte[] serverMessageAsByteArray = Encoding.ASCII.GetBytes(serverMessage);
                // Write byte array to socketConnection stream.               
                stream.Write(serverMessageAsByteArray, 0, serverMessageAsByteArray.Length);
            }
        }
        catch (SocketException socketException)
        {
            Debug.Log("Socket exception: " + socketException);
        }
    }
}
