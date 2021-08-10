using UnityEngine;

[CreateAssetMenu(fileName = "TornadoPropertyData", menuName = "ScriptableObjects/TornadoProperyData", order = 1)]
public class TornadoPropertySO : ScriptableObject
{
    public float MaxDistance=7f;
    public float MinDistance=3f;
    public float Damper=0f;
    public float SpringForce=50f;
    public float Force=50f;
    public float MaxRadius = 35f; 
    public float RadiusReduceSpeed=30f;  
}
