using UnityEngine;
using System;
using System.Collections;
using System.IO;
using VitDeck.Utilities;
using UnityEngine.Networking;

namespace VitDeck.Main
{
    /// <summary>
    /// 指定したURLからJSON形式のリリース情報を取得するクラス。
    /// <summary>
    public static class JsonReleaseInfo
    {
        internal static readonly string VitDeckRootPath = "Assets/VitDeck";
        internal static readonly string JsonReleaseInfoPath = "ReleaseInfo.json";
        private static readonly string ReleaseBranchName = "develop";
        private static string version = null;
        private static string packageName = null;
        private static string downloadUrl = null;

        public static string GetReleaseUrl()
        {
            return Path.Combine(
                ProductInfoUtility.GetDeveloperLinkURL()
                    .Replace("https://github.com/", "https://raw.githubusercontent.com/"),
                JsonReleaseInfo.ReleaseBranchName,
                JsonReleaseInfo.VitDeckRootPath,
                JsonReleaseInfo.JsonReleaseInfoPath
            );
        }

        public static string GetVersion()
        {
            return version;
        }

        public static string GetPackageName()
        {
            return packageName;
        }

        public static string GetDownloadURL()
        {
            return downloadUrl;
        }

        public static void FetchInfo(string releaseUrl)
        {
            if (string.IsNullOrEmpty(releaseUrl))
                return;
            var release = ReleaseEnumerator(releaseUrl);
            while (release.MoveNext()) { }
            if (release != null && release.Current != null)
            {
                var info = JsonUtility.FromJson<ReleaseInfo>(release.Current.ToString());
                version = info.version;
                packageName = info.package_name;
                downloadUrl = info.download_url;
            }
        }

        static IEnumerator ReleaseEnumerator(string releaseUrl)
        {
            using (var request = UnityWebRequest.Get(releaseUrl))
            {
                request.downloadHandler = new DownloadHandlerBuffer();
                yield return request.SendWebRequest();

                while (!request.isDone)
                {
                    yield return null;
                }

                if (request.isHttpError || request.isNetworkError)
                {
                    Debug.LogWarning(request.error);
                    Debug.LogWarning("URL (that can not be got version info): " + releaseUrl);
                }
                else
                {
                    var text = request.downloadHandler.text;
                    yield return text;
                }
            }
        }

        [Serializable]
        public class ReleaseInfo
        {
            public string version;
            public string package_name;
            public string download_url;
        }
    }
}