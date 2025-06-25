using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    [SerializeField] private Transform _headBone;
    [SerializeField] private Vector3 _positonOffset;
    [SerializeField] private Vector3 _rotationOffset;

    [SerializeField] private VolumetricAdditionalLight volumetric;
    void Start()
    {
        
    }

    void Update()
    {
        transform.position = _headBone.position + _headBone.rotation * _positonOffset;
        transform.rotation = _headBone.rotation * Quaternion.Euler(_rotationOffset);


        float yAngle = transform.eulerAngles.y;

        if (yAngle > 330 || yAngle < 30)
        {
            volumetric.Anisotropy = 0.66f;
        }
        else if (yAngle > 85 && yAngle < 275)
        {
            volumetric.Anisotropy = -0.66f;
        }
        
        else
        {
            volumetric.Anisotropy = 0.4f;
        }


    }
}
