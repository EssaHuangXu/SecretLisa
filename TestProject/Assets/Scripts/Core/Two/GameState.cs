using System;
using System.Collections.Generic;
using UnityEngine;

namespace SecretLisa
{
    public interface IGameState
    {
        public IGameState HandleEvent(IGameStateEvent gameState);

        public void OnEnter();

        public void OnExit();
    }

    public class GameState : IGameState
    {
        private readonly Dictionary<Type, Type> transitions = new Dictionary<Type, Type>();

        public IGameState HandleEvent(IGameStateEvent gameState)
        {
            if (transitions.TryGetValue(gameState.GetType(), out var transition))
            {
                //TODO Use factory to create GameState
                return (IGameState)Activator.CreateInstance(transition);
            }

            return null;
        }

        public virtual void OnEnter()
        {
        }

        public virtual void OnExit()
        {
        }

        protected void AddTransition(Type eventType, Type toStateType)
        {
            if (eventType == null || typeof(IGameStateEvent).IsAssignableFrom(eventType) == false)
            {
                Debug.LogError("Game State Event Type not assignable to IGameStateEvent");
                return;
            }

            if (toStateType == null || typeof(IGameState).IsAssignableFrom(toStateType) == false)
            {
                Debug.LogError("Game State Type not assignable to IGameState");
                return;
            }

            transitions.TryAdd(eventType, toStateType);
        }
    }
}