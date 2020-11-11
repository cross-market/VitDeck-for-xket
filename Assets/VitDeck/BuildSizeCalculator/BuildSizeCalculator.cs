using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using SpottedZebra.UnitySizeExplorer.WPF.ViewModels.Pages;
using VitDeck.Utilities;

namespace VitDeck.BuildSizeCalculator
{
    /// <summary>
    /// ビルドサイズを計算します。
    /// </summary>
    public class BuildSizeCalculator : ScriptableWizard
    {
        /// <summary>
        /// メニューの表示項目の接頭辞。
        /// </summary>
        private const string Prefix = "VitDeck/";

        /// <summary>
        /// <see cref="Application.consoleLogPath"/>を後ろから何バイトずつ読み込むか。
        /// </summary>
        private static readonly int BufferSize = 1024;

        /// <summary>
        /// <see cref="Application.consoleLogPath"/>を後ろから読み込み、ヒットしたら読み込む終了とする文字。
        /// </summary>
        private static readonly string BuildSizeSectionStartPattern = "Bundle Name: customscene.vrcw";

        [SerializeField]
        private DefaultAsset baseFolder;

#if VRC_SDK_VRCSDK2 && !VITDECK_HIDE_MENUITEM
        [MenuItem(BuildSizeCalculator.Prefix + "Calculate Build Size", priority = 120)]
#endif
        public static void Open()
        {
            ScriptableWizard.DisplayWizard<BuildSizeCalculator>("VitDeck", "Build and Calculate").LoadSettings();
        }

        protected override bool DrawWizardGUI()
        {
            base.DrawWizardGUI();
            this.isValid = baseFolder;
            return true;
        }

        /// <summary>
        /// VitDeckのユーザー設定を読み込みます。
        /// </summary>
        private void LoadSettings()
        {
            var userSettings = UserSettingUtility.GetUserSettings();
            this.baseFolder = AssetDatabase.LoadAssetAtPath<DefaultAsset>(userSettings.validatorFolderPath);
        }

        /// <summary>
        /// VitDeckのユーザー設定を保存します。
        /// </summary>
        private void SaveSettings()
        {
            var userSettings = UserSettingUtility.GetUserSettings();
            userSettings.validatorFolderPath = AssetDatabase.GetAssetPath(this.baseFolder);
            UserSettingUtility.SaveUserSettings(userSettings);
        }

        private async Task OnWizardCreate()
        {
            this.SaveSettings();

            if (!this.OpenScene())
            {
                EditorUtility.DisplayDialog("VitDeck", $"{this.GetScenePath()} が存在しません。", "OK");
                return;
            }

            this.Build();

            EditorUtility.DisplayDialog("VitDeck", $"{AssetDatabase.GetAssetPath(this.baseFolder)} のビルドサイズは {await this.Calculate()} MB です。", "OK");
        }

        /// <summary>
        /// <see cref="baseFolder"/>を基に、シーンのパスを返します。
        /// </summary>
        /// <returns></returns>
        private string GetScenePath()
        {
            var baseFolderPath = AssetDatabase.GetAssetPath(this.baseFolder);
            var id = Path.GetFileName(baseFolderPath);
            return $"Assets/{id}/{id}.unity";
        }

        /// <summary>
        /// 入稿用シーンを開きます。
        /// </summary>
        /// <returns>シーンが存在しなければ <c>false</c> を返します。</returns>
        private bool OpenScene()
        {
            var scenePath = this.GetScenePath();
            var scene = EditorSceneManager.GetSceneByPath(scenePath);
            if (!scene.IsValid())
            {
                return false;
            }

            if (!scene.isLoaded)
            {
                EditorSceneManager.OpenScene(scenePath);
            }

            return true;
        }

        /// <summary>
        /// VRChat SDKを利用してワールドをビルドし、<see cref="Application.consoleLogPath"/>へビルドサイズを出力させます。
        /// </summary>
        private void Build()
        {
            // 再ビルドに必要
            File.Delete(Path.Combine(Application.temporaryCachePath, "customscene.vrcw.manifest"));

#if VRC_SDK_VRCSDK2
            VRC_SdkBuilder.ExportSceneResource();
#endif
        }

        /// <summary>
        /// <see cref="Application.consoleLogPath"/>を解析し、ベースフォルダのビルド容量を計算します。
        /// </summary>
        /// <returns>MB単位の容量を返します。</returns>
        private async Task<float> Calculate()
        {
            var content = "";
            using (var fileStream
                = new FileStream(Application.consoleLogPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                var bytes = new List<byte>();
                fileStream.Seek(-BuildSizeCalculator.BufferSize, SeekOrigin.End);
                while (!content.Contains(BuildSizeCalculator.BuildSizeSectionStartPattern))
                {
                    var buffer = new byte[BuildSizeCalculator.BufferSize];
                    await fileStream.ReadAsync(buffer, 0, BuildSizeCalculator.BufferSize);
                    bytes.InsertRange(0, buffer);
                    content = Encoding.UTF8.GetString(bytes.ToArray());
                    fileStream.Seek(-2 * BuildSizeCalculator.BufferSize, SeekOrigin.Current);
                }
            }

            var baseFolderPath = AssetDatabase.GetAssetPath(this.baseFolder);
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(content)))
            {
                return (await FileBuilder.FromStream(stream))
                    .GroupBy(pathSizePair => pathSizePair.Item1, pathSizePair => pathSizePair.Item2)
                    .ToDictionary(pathSizesPair => pathSizesPair.Key, pathSizesPair => pathSizesPair.First())
                    .Where(pathSizePair => pathSizePair.Key.StartsWith(baseFolderPath))
                    .Sum(pathSizePair => pathSizePair.Value);
            }
        }
    }
}
