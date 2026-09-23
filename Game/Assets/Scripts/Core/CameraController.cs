using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;
    
    [SerializeField]
    private Vector3 _offset = new Vector3(0, 0, -10);
    
    [SerializeField]
    private float _smoothTime = 0.2f;

    [SerializeField] private HumanController _controller;

    private PartyManager _partyManager;

    private Vector3 _velocity;

    private void Awake()
    {
        _partyManager = GetComponent<PartyManager>();

        if (_controller == null)
            _controller = GetComponent<HumanController>();
    }

    private void LateUpdate()
    {
        if (_partyManager == null || _controller == null)
            return;

        if (!_partyManager.TryGetControlledCharacter(_controller, out BaseCharacter character))
            return;

        Vector3 targetPosition = character.transform.position + _offset;

        _camera.transform.position = Vector3.SmoothDamp(
            _camera.transform.position,
            targetPosition,
            ref _velocity,
            _smoothTime);
    }
}