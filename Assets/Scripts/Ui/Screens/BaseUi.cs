using System;
using Enums;
using UnityEngine;
using Enum = Enums.Enum;

namespace Ui.Screens
{
    
    public abstract class BaseUi : MonoBehaviour
    {
        [SerializeField] private Enum.GameScreen screen;

        protected virtual void OnDisable()
        {
            Reset();
        }

        public Enum.GameScreen Screen
        {
            get => screen;
            set => screen = value;
        }

        public virtual void Reset()
        {
            
        }
    }
}