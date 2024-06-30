using System;
using DG.Tweening;
using Problem1;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class Drone : MonoBehaviour
{
    private Camera _camera;
    private Vector3 _lookTarget;
    [SerializeField] private float lookAtYOffset;
    [SerializeField] private LocalPlayer player;
    [SerializeField , Range(0,10)] private float minDistanceFromPlayer;
    [SerializeField] private float minFollowDuration;
    [SerializeField] private float maxFollowDuration;
    [SerializeField] private float yOffsetFromPlayer;

    [SerializeField] private bool isMoving;
    [SerializeField] private Ease followType;

    private void OnValidate()
    {
        if(_camera == null)_camera = Camera.main;
        if(yOffsetFromPlayer <= 0)yOffsetFromPlayer = transform.position.y;
    }

    private void Update()
    {
        SetTargetToLookAt(InputManager.Instance.MousePosition);
    }

    private void LateUpdate()
    {
        transform.LookAt(_lookTarget);
        
        var distance = (transform.position - player.transform.position).magnitude;
        if (distance >= minDistanceFromPlayer)
        {
            if(isMoving) return;
            
            isMoving = true;
            
            var position = player.transform.position;
            
            var targetPosition =
                new Vector3(position.x, yOffsetFromPlayer, position.z);
            
            transform.DOMove(targetPosition, Utils.GetValueByPercentage(minFollowDuration , maxFollowDuration ,
                    Vector3.Distance(transform.position , player.transform.position) / distance) ).SetEase(followType).onComplete =
                () =>
                {
                    isMoving = false;
                };
        }
    }

    void SetTargetToLookAt(Vector3 lookAtPos)
    {

        var ray = _camera.ScreenPointToRay(lookAtPos);
        if (Physics.Raycast(ray, out RaycastHit hit, 100))
        {
            _lookTarget = hit.point;
            _lookTarget.y = lookAtYOffset;
        }
    }
}