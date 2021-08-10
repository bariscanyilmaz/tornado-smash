using UnityEngine;

[CreateAssetMenu(fileName = "RotatePropertyData", menuName = "ScriptableObjects/RotateProperyData", order = 2)]
public class RotatePropertySO : ScriptableObject
{
    public float RotateSpeed=360f;
    public float RiseSpeed=5f;
    public float ScaleSpeed=0.5f;

    public float RadiusReduceSpeed=30f;
}
