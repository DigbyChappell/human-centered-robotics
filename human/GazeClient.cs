using UnityEngine;

public class GazeClient : MonoBehaviour
{
    private GazeClient _gazeClient;

    private void Start()
    {
        _gazeCommunication = new GazeCommunication();
        _gazeCommunication.Start();
    }

    private void OnDestroy()
    {
        _gazeCommunication.Stop();
    }
}