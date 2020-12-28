using System;
using System.Linq;
using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.Build.Reporting;
using VitDeck.Utilities;

namespace VitDeck.BuildSizeCalculator
{
    /// <summary>
    /// ビルドサイズを計算します。
    /// </summary>
    public class BuildSizeCalculator : ScriptableWizard
    {
        private static readonly string LastBuildReportPath = "Library/LastBuild.buildreport";

        /// <summary>
        /// メニューの表示項目の接頭辞。
        /// </summary>
        private const string Prefix = "VitDeck/";

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

        private void OnWizardCreate()
        {
            this.SaveSettings();

            if (!this.OpenScene())
            {
                EditorUtility.DisplayDialog("VitDeck", $"{this.GetScenePath()} が存在しません。", "OK");
                return;
            }


            this.Build();

            EditorUtility.DisplayDialog(
                "VitDeck",
                $"{this.GetScenePath()} のビルドサイズは {this.Calculate() / Math.Pow(2, 20):0.00} MiB です。",
                "OK"
            );
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
        /// VRChat SDKを利用してワールドをビルドし、「Library/LastBuild.buildreport」へビルドレポートを出力させます。
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
        /// 「Library/LastBuild.buildreport」から、合計容量を取得して返します。
        /// </summary>
        /// <returns>合計バイト数を返します。</returns>
        private float Calculate()
        {
            if (!AssetDatabase.GetSubFolders("Assets/VitDeck").Contains("Temporary"))
            {
                AssetDatabase.CreateFolder("Assets/VitDeck", "Temporary");
            }
            var buildResultPath = "Assets/VitDeck/Temporary/" + Path.GetFileName(BuildSizeCalculator.LastBuildReportPath);

            File.Copy(BuildSizeCalculator.LastBuildReportPath, buildResultPath, overwrite: true);
            AssetDatabase.ImportAsset(buildResultPath);

            return AssetDatabase.LoadAssetAtPath<BuildReport>(buildResultPath).summary.totalSize;
        }
    }
}
