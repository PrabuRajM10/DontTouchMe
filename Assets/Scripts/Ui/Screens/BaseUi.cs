using System;
using Enums;
using UnityEngine;

namespace Ui.Screens
{
    
    public abstract class BaseUi : MonoBehaviour
    {
        [SerializeField] private DTMEnum.GameScreen screen;

        protected virtual void OnDisable()
        {
            Reset();
        }

        public DTMEnum.GameScreen Screen
        {
            get => screen;
            set => screen = value;
        }

        public virtual void Reset()
        {
            
        }
    }
}