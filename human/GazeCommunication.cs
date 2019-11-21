using AsyncIO;
using NetMQ;
using NetMQ.Sockets;
using UnityEngine;
using System;
using Tobii.XR;

/// <summary>
///     Example of requester who only sends Hello. Very nice guy.
///     You can copy this class and modify Run() to suits your needs.
///     To use this class, you just instantiate, call Start() when you want to start and Stop() when you want to stop.
/// </summary>
public class GazeCommunication : RunAbleThread
{
    /// <summary>
    ///     Request Hello message to server and receive message back. Do it 10 times.
    ///     Stop requesting when Running=false.
    /// </summary>
    protected override void Run()
    {
        ForceDotNet.Force(); // this line is needed to prevent unity freeze after one use, not sure why yet
        using (RequestSocket client = new RequestSocket())
        {
            client.Connect("tcp://localhost:5555");

            for (int i = 0; i < 10 && Running; i++)
            {

                var gazeRay = TobiiXR.EyeTrackingData.GazeRay;

                // Check if gaze ray is valid
                if (TobiiXR.EyeTrackingData.GazeRay.IsValid)
                {
                    // Eye Origin is in World Space
                    var rayOrigin = TobiiXR.EyeTrackingData.GazeRay.Origin;
                    Debug.Log("User gaze origin: " + rayOrigin);
                    client.SendFrame("O" + rayOrigin);
                    // Eye Direction is a normalized direction vector in World Space
                    var rayDirection = TobiiXR.EyeTrackingData.GazeRay.Direction;
                    Debug.Log("User gaze direction: " + rayDirection);
                    client.SendFrame("D" + rayDirection);
                    string message = null;
                    bool gotMessage = false;
                    while (Running)
                    {
                        gotMessage = client.TryReceiveFrameString(out message); // this returns true if it's successful
                        if (gotMessage) break;
                    }

                    if (gotMessage)
                    {
                        Debug.Log("Received " + message);
                    }
                }                
            }
        }
        NetMQConfig.Cleanup(); // this line is needed to prevent unity freeze after one use, not sure why yet
    }
}