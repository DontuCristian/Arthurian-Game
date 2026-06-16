using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;
    
    [SerializeField]
    private Vector3 _offset = new Vector3(0, 0, -10);
    
    [SerializeField]
    private float _smoothTime = 0.2f;
    
    private PartyManager _partyManager;

    private Vector3 _velocity;

    private void Awake()
    {
        _partyManager = GetComponent<PartyManager>();
    }
    
    private void LateUpdate()
    {
        if (_partyManager.ActiveCharacter == null)
            return;

        Vector3 targetPosition =  _partyManager.ActiveCharacter.transform.position;

        targetPosition += _offset;

        _camera.transform.position = Vector3.SmoothDamp( _camera.transform.position, targetPosition, ref _velocity, _smoothTime);
        
    }
    
}
