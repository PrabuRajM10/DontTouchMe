using System;
using System.Collections.Generic;
using DG.Tweening;
using Helpers;
using Managers;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gameplay
{
    public class Drone : MonoBehaviour
    {
        [SerializeField] private Player player;
        [SerializeField] private Gun gun1;
        [SerializeField] private BulletProperties defaultBulletPropertiesSo;
        
        [SerializeField , Range(0,10)] private float minDistanceFromPlayer;
        [SerializeField] private float lookAtYOffset;
        [SerializeField] private float minFollowDuration;
        [SerializeField] private float maxFollowDuration;
        [SerializeField] private float yOffsetFromPlayer;

        [SerializeField] private bool isMoving;
        [SerializeField] private Ease followType;

        [SerializeField] private Transform defaultGunPos;
        [SerializeField] private Transform dualGunPos1;
        [SerializeField] private Transform dualGunPos2;
        [SerializeField]private Camera mainCamera;

        [SerializeField] private LayerMask lookTargetLayerMask;

        private Vector3 _lookTarget;
        private Gun _gun2;
        private readonly List<Gun> _currentActiveGuns = new List<Gun>();

        private void OnValidate()
        {
            if(mainCamera == null)mainCamera = Camera.main;
            if(yOffsetFromPlayer <= 0)yOffsetFromPlayer = transform.position.y;
        }

        private void Awake()
        {
            _currentActiveGuns.Add(gun1);
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
            var ray = mainCamera.ScreenPointToRay(lookAtPos);
            if (Physics.Raycast(ray, out RaycastHit hit, 100 , lookTargetLayerMask))
            {
                _lookTarget = hit.point;
                _lookTarget.y = lookAtYOffset;
            }
        }

        public void DualGuns(bool dualGuns)
        {
            if (dualGuns)
            {
                gun1.transform.LeanMoveLocal(dualGunPos1.localPosition, 0.2f);
                if (_gun2 == null)
                {
                    _gun2 = Instantiate(gun1, dualGunPos2);
                    _gun2.transform.localPosition = Vector3.zero;
                }
                _currentActiveGuns.Add(_gun2);
                if (!_gun2.gameObject.activeSelf) _gun2.gameObject.SetActive(true);
            }
            else
            {
                _gun2.gameObject.SetActive(false);
                _currentActiveGuns.Remove(_gun2);
                gun1.transform.LeanMoveLocal(defaultGunPos.localPosition, 0.2f);
            }
        }

        public void Shoot()
        {
            foreach (var gun in _currentActiveGuns)
            {
                gun.Shoot();
            }
        }

        public void SetBulletDamage(int damageValue)
        {
            defaultBulletPropertiesSo.DamageAmount = damageValue;
        }

        public void ResetBulletDamage()
        {
            defaultBulletPropertiesSo.ResetDamage();
        }
    }
}