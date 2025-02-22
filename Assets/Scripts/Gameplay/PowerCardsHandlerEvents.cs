using System;
using Enums;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay
{
    [Serializable]
    public class PowerCardsEventsData
    {
        public Enums.DTMEnum.PowerCardsId EventName;
        public UnityEvent OnExecute;
        public UnityEvent OnExit;

    }
    
    public class PowerCardsHandlerEvents : MonoBehaviour
    {
        public PowerCardsEventsData[] powerCardsEvents;

        public void OnReceivedEvents(DTMEnum.PowerCardsId eventName)
        {
            foreach (var powerCardsEvent in powerCardsEvents)
            {
                if (eventName.Equals(powerCardsEvent.EventName))
                {
                    powerCardsEvent.OnExecute?.Invoke();    
                }
            }
        }
        
        public void OnReceivedExitEvents(DTMEnum.PowerCardsId eventName)
        {
            foreach (var powerCardsEvent in powerCardsEvents)
            {
                if (eventName.Equals(powerCardsEvent.EventName))
                {
                    powerCardsEvent.OnExit?.Invoke();    
                }
            }
        }
        
    }
}