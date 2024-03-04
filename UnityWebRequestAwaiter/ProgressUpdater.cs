#pragma warning disable CS8618
#pragma warning disable CS8625
using System.Collections.Generic;
using UnityEngine;

namespace TSUBASAMUSU.UnityWebRequestAwaiter
{
    public class ProgressUpdater : MonoBehaviour
    {
        private static ProgressUpdater instance;

        private List<WebRequestProgressNotifier> webRequestProgressNotifiers = new List<WebRequestProgressNotifier>();

        public static ProgressUpdater Instance
        {
            get
            {
                if (instance == null) instance = new GameObject("ProgressUpdater").AddComponent<ProgressUpdater>();

                return instance;
            }
        }

        public void AddItem(WebRequestProgressNotifier webRequestProgressNotifier)
        {
            if (!webRequestProgressNotifier.NotifyProgress()) webRequestProgressNotifiers.Add(webRequestProgressNotifier);
        }

#pragma warning disable IDE0051
        private void Update()
#pragma warning restore IDE0051
        {
            for (int i = 0; i < webRequestProgressNotifiers.Count; i++)
            {
                WebRequestProgressNotifier webRequestProgressNotifier = webRequestProgressNotifiers[i];

                if (webRequestProgressNotifier.NotifyProgress()) webRequestProgressNotifiers[i] = null;
            }

            webRequestProgressNotifiers.RemoveAll(webRequestProgressNotifier => webRequestProgressNotifier == null);
        }
    }
}