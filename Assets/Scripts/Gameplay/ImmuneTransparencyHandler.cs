using System;
using UnityEngine;

namespace Gameplay
{
    public class ImmuneTransparencyHandler : MonoBehaviour
    {
        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private Color defaultColor;
        [SerializeField] private Color immuneColor;

        private Material _material;

        private void OnValidate()
        {
            if (meshRenderer != null) return;
            meshRenderer = GetComponent<MeshRenderer>();
        }

        private void Awake()
        {
            if(meshRenderer != null) _material = meshRenderer.material;
            else
            {
                Debug.LogError("MeshRenderer is null");
            }
        }

        public void SetImmune(bool isImmune)
        {
            _material.color = isImmune ? immuneColor : defaultColor;
        }
    }
}