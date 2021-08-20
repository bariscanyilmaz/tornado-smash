using UnityEngine;

public abstract class BaseTransform:MonoBehaviour
{
   public Vector3 Rotate(float height, float angle, float radius) =>

    new Vector3(
                Mathf.Cos((angle * Mathf.Deg2Rad)) * radius,
                height,
                Mathf.Sin((angle * Mathf.Deg2Rad)) * radius
                );

    public Vector3 Scale(float localScale) => new Vector3(localScale, localScale, localScale);
}
