using System;
using UnityEngine.Networking;

namespace TSUBASAMUSU.UnityWebRequestAwaiter
{
    public class WebRequestProgressNotifier
    {
        private UnityWebRequestAsyncOperation unityWebRequestAsyncOperation;

        private IProgress<float> progress;

        public WebRequestProgressNotifier(UnityWebRequestAsyncOperation unityWebRequestAsyncOperation, IProgress<float> progress)
        {
            this.unityWebRequestAsyncOperation = unityWebRequestAsyncOperation;

            this.progress = progress;
        }

        public bool NotifyProgress()
        {
            progress.Report(unityWebRequestAsyncOperation.progress);

            return unityWebRequestAsyncOperation.isDone;
        }
    }
}