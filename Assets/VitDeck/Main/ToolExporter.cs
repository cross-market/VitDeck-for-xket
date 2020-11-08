using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.IO;
using UnityEngine;
using UnityEditor;
using VitDeck.Utilities;

namespace VitDeck.Main
{
    /// <summary>
    /// VitDeck自体のunitypackageを作成し、デスクトップへ出力します。
    /// </summary>
    internal class ToolExporter
    {
        private static readonly string DestinationFolderPath
            = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

        private static readonly Regex IgnorePattern = new Regex(@"^Assets/VitDeck/(
            Main/ToolExporter\.cs
            |Temporary
            |.+/Tests/.+|
            |TestUtilities(/.+)?
            |Templates/Sample_template(/.+)?
            |Validator/Rules/(Sample(/.+)?|Vket5/.+RuleSet\.cs)
            |Config/(UserSettings\.asset|(DefaultExportSetting|Vket).*\.asset)
        )$", RegexOptions.ExplicitCapture | RegexOptions.IgnorePatternWhitespace);

        private static string GetPackageName()
        {
            return $"{ProductInfoUtility.GetDeveloperLinkTitle().Replace(" ", "-")}-{ProductInfoUtility.GetVersion()}.unitypackage";
        }

        [MenuItem("VitDeck/Export VitDeck", false, 201)]
        private static void Export()
        {
            ToolExporter.SaveReleaseInfo();
            AssetDatabase.ExportPackage(
                AssetDatabase.GetAllAssetPaths().Where(path => path == JsonReleaseInfo.VitDeckRootPath
                    || path.StartsWith(JsonReleaseInfo.VitDeckRootPath + "/")
                        && !ToolExporter.IgnorePattern.IsMatch(path)).ToArray(),
                Path.Combine(ToolExporter.DestinationFolderPath, ToolExporter.GetPackageName())
            );
        }

        private static void SaveReleaseInfo()
        {
            File.WriteAllText(Path.Combine(
                Path.GetDirectoryName(Application.dataPath),
                JsonReleaseInfo.VitDeckRootPath,
                JsonReleaseInfo.JsonReleaseInfoPath
            ), JsonUtility.ToJson(new JsonReleaseInfo.ReleaseInfo()
            {
                version = ProductInfoUtility.GetVersion(),
                package_name = ProductInfoUtility.GetDeveloperLinkTitle(),
                download_url = $"{ProductInfoUtility.GetDeveloperLinkURL()}/releases/download/v{ProductInfoUtility.GetVersion()}/{ToolExporter.GetPackageName()}",
            }, true));
            AssetDatabase.Refresh();
        }
    }
}
