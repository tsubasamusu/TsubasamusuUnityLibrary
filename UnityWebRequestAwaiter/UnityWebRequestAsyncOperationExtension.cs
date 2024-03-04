using System;
using UnityEngine.Networking;

namespace TSUBASAMUSU.UnityWebRequestAwaiter
{
    public static class UnityWebRequestAsyncOperationExtension
    {
        public static UnityWebRequestAsyncOperationAwaiter GetAwaiter(this UnityWebRequestAsyncOperation unityWebRequestAsyncOperation) => new UnityWebRequestAsyncOperationAwaiter(unityWebRequestAsyncOperation);

        public static UnityWebRequestAsyncOperationAwaiter ConfigureAwait(this UnityWebRequestAsyncOperation unityWebRequestAsyncOperation, IProgress<float> progress)
        {
            WebRequestProgressNotifier webRequestProgressNotifier = new WebRequestProgressNotifier(unityWebRequestAsyncOperation, progress);

            ProgressUpdater.Instance.AddItem(webRequestProgressNotifier);

            return new UnityWebRequestAsyncOperationAwaiter(unityWebRequestAsyncOperation);
        }
    }
}