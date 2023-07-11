using UnityEngine;

[CreateAssetMenu(fileName = "EndpointsData", menuName = "ScriptableObjects/ServerScriptableObject", order = 1)]
public class Endpoints : ScriptableObject
{
    public string DevelopEndpoint = "localhost:3000";
    public string StateEndpoint = "/state";
}
