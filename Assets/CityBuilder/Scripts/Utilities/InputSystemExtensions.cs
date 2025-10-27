using System;
using UnityEngine.InputSystem;
using UniRx;

namespace CityBuilder.Utilities
{
    public static class InputSystemExtensions
    {
        public static IObservable<Unit> PerformedAsObservable(this InputAction inputAction)
        {
            return Observable.FromEvent<InputAction.CallbackContext>(
                h => inputAction.performed += h,
                h => inputAction.performed -= h
            ).AsUnitObservable();
        }

        public static IObservable<Unit> StartedAsObservable(this InputAction inputAction)
        {
            return Observable.FromEvent<InputAction.CallbackContext>(
                h => inputAction.started += h,
                h => inputAction.started -= h
            ).AsUnitObservable();
        }

        public static IObservable<Unit> CanceledAsObservable(this InputAction inputAction)
        {
            return Observable.FromEvent<InputAction.CallbackContext>(
                h => inputAction.canceled += h,
                h => inputAction.canceled -= h
            ).AsUnitObservable();
        }
    }
}
