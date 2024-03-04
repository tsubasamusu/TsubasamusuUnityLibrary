using System;
using System.Runtime.CompilerServices;
using UnityEngine.Networking;

namespace TSUBASAMUSU.UnityWebRequestAwaiter
{
    public class UnityWebRequestAsyncOperationAwaiter : INotifyCompletion
    {
        private UnityWebRequestAsyncOperation unityWebRequestAsyncOperation;

        public bool IsCompleted
        {
            get => unityWebRequestAsyncOperation.isDone;
        }

        public UnityWebRequestAsyncOperationAwaiter(UnityWebRequestAsyncOperation unityWebRequestAsyncOperation)
        {
            this.unityWebRequestAsyncOperation = unityWebRequestAsyncOperation;
        }

        public void GetResult()
        {

        }

        public void OnCompleted(Action action) => unityWebRequestAsyncOperation.completed += _ => { action(); };

        public UnityWebRequestAsyncOperationAwaiter GetAwaiter() => this;
    }
}