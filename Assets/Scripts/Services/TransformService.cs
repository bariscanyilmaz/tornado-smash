using UnityEngine;
public class TransformService
{
    public Vector3 Scale(float localScale) => new Vector3(localScale, localScale, localScale);
    public Vector3 Rotate(float angle, float height, float radius) =>
     new Vector3(
                Mathf.Cos((angle * Mathf.Deg2Rad)) * radius,
                height,
                Mathf.Sin((angle * Mathf.Deg2Rad)) * radius
                );

}