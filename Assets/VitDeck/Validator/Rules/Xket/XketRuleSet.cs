// 当ファイルは Assets/VitDeck/Validator/Rules/Vket5/Vket5RuleSetBase.cs をコピーして改変したもの

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using VitDeck.Language;

#if VRC_SDK_VRCSDK2
using VRCSDK2;
#endif

namespace VitDeck.Validator
{

    /// <summary>
    /// クロスマーケットの基本ルールセット。
    /// </summary>
    public class XketRuleSet : IRuleSet
    {
        public string RuleSetName => "クロスマーケット2ルールセット";
        protected readonly long MegaByte = 1048576;

        private readonly Vket5TargetFinder targetFinder = new Vket5TargetFinder();
        public IValidationTargetFinder TargetFinder => targetFinder;

        public IRule[] GetRules()
        {
            // デフォルトで使っていたAttribute式は宣言時にconst以外のメンバーが利用できない。
            // 継承したプロパティを参照して挙動を変えることが出来ない為、直接リストを返す方式に変更した。
            return new IRule[]
            {
#if VRC_SDK_VRCSDK2

                new UnityVersionRule(LocalizedMessage.Get("Vket5RuleSetBase.UnityVersionRule.Title", "2018.4.20f1"), "2018.4.20f1"),

                new BaseFolderPathRule(
                    "ベースフォルダパスルール",
                    new Regex("^Assets/2[01][0-9][0-9]$"),
                    "Base Folderは、入稿ツールによってAssets直下へ作成された、半角数字4桁のサークルIDのフォルダです。なお、サークルIDは、右のHelpボタンから飛べる出展サークル一覧ページで確認できます。",
                    "https://id.pokemori.jp/cross-market2/circles.xhtml"
                ),

                new ExistInSubmitFolderRule(LocalizedMessage.Get("Vket5RuleSetBase.ExistInSubmitFolderRule.Title"), Vket5OfficialAssetData.GUIDs, targetFinder),

                new AssetGuidBlacklistRule(LocalizedMessage.Get("Vket5RuleSetBase.OfficialAssetDontContainRule.Title"), Vket5OfficialAssetData.GUIDs),

                new FolderSizeRule(LocalizedMessage.Get("Vket5RuleSetBase.FolderSizeRule.Title"), FolderSizeLimit),

                new ExhibitStructureRule(LocalizedMessage.Get("Vket5RuleSetBase.ExhibitStructureRule.Title")),

                new StaticFlagRule(LocalizedMessage.Get("Vket5RuleSetBase.StaticFlagsRule.Title")),

                new AssetExtentionBlacklistRule("アセット拡張子ルール", new []{
                    ".shader", ".cginc", ".shadervariants",
                    ".cs", ".asmdef", ".dll", ".com", ".exe", ".bat", ".cmd", ".vbs", ".vbe", ".js", ".jse", ".wsf", ".wsh", ".lnk",
                    ".asf", ".avi", ".dv", ".m4v", ".mov", ".mp4", ".mpg", ".mpeg", ".ogv", ".vp8", ".webm", ".wmv",
                    }),

                new BoothBoundsRule(LocalizedMessage.Get("Vket5RuleSetBase.BoothBoundsRule.Title"),
                    size: BoothSizeLimit,
                    margin: 0.01f),

                new LightmapSizeLimitRule(
                    LocalizedMessage.Get("Vket5RuleSetBase.LightMapsLimitRule.Title", LightmapCountLimit, 256),
                    lightmapCountLimit: LightmapCountLimit,
                    lightmapResolutionLimit: 256),

                new UsableComponentListRule(LocalizedMessage.Get("Vket5RuleSetBase.UsableComponentListRule.Title"),
                    GetComponentReferences(),
                    ignorePrefabGUIDs: Vket5OfficialAssetData.GUIDs
                        .Except(Vket5OfficialAssetData.CanvasPrefabGUIDs).ToArray(),
                    unregisteredComponent: ValidationLevel.DISALLOW),

                new ReflectionProbeRule(LocalizedMessage.Get("Vket5RuleSetBase.ReflectionProbeRule.Title")),

                new VRCTriggerConfigRule(LocalizedMessage.Get("Vket5RuleSetBase.VRCTriggerConfigRule.Title"),
                            VRCTriggerBroadcastTypesWhitelist,
                            (VRC_Trigger.TriggerType[])Enum.GetValues(typeof(VRC_Trigger.TriggerType)),
                            VRCTriggerActionWhitelist,
                            Vket5OfficialAssetData.GUIDs),

                new VRCTriggerCountLimitRule(LocalizedMessage.Get("Vket5RuleSetBase.VRCTriggerCountLimitRule.Title", VRCTriggerCountLimit), VRCTriggerCountLimit),

                new LightCountLimitRule(LocalizedMessage.Get("Vket5RuleSetBase.DirectionalLightLimitRule.Title"), UnityEngine.LightType.Directional, 0),

                new LightConfigRule(LocalizedMessage.Get("Vket5RuleSetBase.PointLightConfigRule.Title"), UnityEngine.LightType.Point, ApprovedPointLightConfig),

                new LightConfigRule(LocalizedMessage.Get("Vket5RuleSetBase.SpotLightConfigRule.Title"), UnityEngine.LightType.Spot, ApprovedSpotLightConfig),

                new LightConfigRule(LocalizedMessage.Get("Vket5RuleSetBase.AreaLightConfigRule.Title"), UnityEngine.LightType.Area, ApprovedAreaLightConfig),

                new CanvasRenderModeRule(LocalizedMessage.Get("Vket5RuleSetBase.CanvasRenderModeRule.Title")),

                new PickupObjectSyncPrefabRule(LocalizedMessage.Get("Vket5RuleSetBase.PickupObjectSyncRule.Title"), Vket5OfficialAssetData.PickupObjectSyncPrefabGUIDs),

                new AvatarPedestalPrefabRule(LocalizedMessage.Get("Vket5RuleSetBase.AvatarPedestalPrefabRule.Title"), Vket5OfficialAssetData.AvatarPedestalPrefabGUIDs),

                new AudioSourcePrefabRule(LocalizedMessage.Get("Vket5RuleSetBase.AudioSourcePrefabRule.Title"),  Vket5OfficialAssetData.AudioSourcePrefabGUIDs),

                new RigidbodyRule(LocalizedMessage.Get("Vket5RuleSetBase.RigidbodyRule.Title")),

                new PrefabLimitRule(
                    LocalizedMessage.Get("Vket5RuleSetBase.PickupObjectSyncPrefabLimitRule.Title", PickupObjectSyncUsesLimit),
                    Vket5OfficialAssetData.PickupObjectSyncPrefabGUIDs,
                    PickupObjectSyncUsesLimit),

                new ErrorShaderRule("エラーシェーダールール"),

                new ShaderWhitelistRule("シェーダーホワイトリストルール", new Dictionary<string, string>()
                {
                    { "TextMeshPro/Mobile/Distance Field", "fe393ace9b354375a9cb14cdbbc28be4" },
                    { "VRChat/Mobile/Bumped Diffuse", "f8c1f8ac363df824899534a0b30eef00" },
                    { "VRChat/Mobile/Bumped Mapped Specular", "584dc70fbb9834e48beb29e3206e3ca0" },
                    { "VRChat/Mobile/Diffuse", "2dcd9e0568e0a6f45b92c60ba2eb16a0" },
                    { "VRChat/Mobile/Lightmapped", "b1f7ecc80417c414b9d62ce541d5bcbf" },
                    { "VRChat/Mobile/MatCap Lit", "3ad043b7f9839cb48a75a9238d433dec" },
                    { "VRChat/Mobile/Particles/Additive", "9200bec112b65ec4fbbbd33fa89c20f4" },
                    { "VRChat/Mobile/Particles/Multiply", "d5b89f0c74ccf5049ba803c14a090378" },
                    { "VRChat/Mobile/Skybox", "c0d3cb006bb294142bef136f492f2568" },
                    { "VRChat/Mobile/Standard Lite", "0b7113dea2069fc4e8943843eff19f70" },
                    { "VRChat/Mobile/Toon Lit", "affc81f3d164d734d8f13053effb1c5c" },
                    { "VRChat/Panosphere", "1278163a2a3ba2b4cad540a862292784" },
                    { "UniGLTF/NormalMapDecoder", "53762a37d0a403e42a4921e3e3b84915" },
                    { "UniGLTF/NormalMapEncoder", "3e39586253f31b34f87fa7e133449b1e" },
                    { "UniGLTF/StandardVColor", "5ef7bdb14a8f23043805e41692d10787" },
                    { "UniGLTF/UniUnlit", "8c17b56f4bf084c47872edcb95237e4a" },
                    { "VRM/MToon", "1a97144e4ad27a04aafd70f7b915cedb" },
                    { "VRM/UnlitCutout", "4c9ce97af40038f45811fc4b0975a483" },
                    { "VRM/UnlitTexture", "1a70c9898704e1a4691843883f5101af" },
                    { "VRM/UnlitTransparent", "df359ad0838642d4fa0339514fcbbb2d" },
                    { "VRM/UnlitTransparentZWrite", "429a3203ab2959741aab76fa2856b450" },
                    { "UnlitWF/UnToon_Mobile/WF_UnToon_Mobile_Texture", "4bd76f6599a5b8e4d88d81300fb74c37" },
                    { "UnlitWF/UnToon_Mobile/WF_UnToon_Mobile_TransCutout", "af3422dc9372a89449a9f44d409d9714" },
                    { "UnlitWF/UnToon_Mobile/WF_UnToon_Mobile_Transparent", "0a7a6cdca16a38548a5d81aca8d4e3ba" },
                    { "UnlitWF/UnToon_Mobile/WF_UnToon_Mobile_TransparentOverlay", "4e4be4aab63a2bd4fbcea2390ae92fdf" },
                    { "MMS3/Mnmrshader3", "8dd7c14dadb834c4e8324f7d08c5674e" },
                    { "MMS3/Mnmrshader3_Cutout", "128f4720891e8914ab7e6673099df0f0" },
                    { "MMS3/Mnmrshader3_Outline", "fbaec084851cef64fbd877b3b15716cb" },
                    { "MMS3/Mnmrshader3_Transparent", "fda424b70f79d4e5488e1cc3ee100a95" },
                    { "MMS3/Stencil/MMS3_Reader ", "f889d00a055a0488e9ecbf22c558ae76" },
                    { "MMS3/Stencil/MMS3_Writer", "f55508f2ed8cc477f9574099971bc4eb" },
                    { "UnityChanToonShader/Mobile/Toon_DoubleShadeWithFeather", "1d10c7840eb6ba74c889a27f14ba6081" },
                    { "UnityChanToonShader/Mobile/Toon_DoubleShadeWithFeather_Clipping", "88791c14394118d42a5e176b433af322" },
                    { "UnityChanToonShader/Mobile/Toon_DoubleShadeWithFeather_Clipping_StencilMask", "41f4ee183cb66ad40bc74a9f8f944974" },
                    { "UnityChanToonShader/Mobile/Toon_DoubleShadeWithFeather_StencilMask", "dec01cbdbc5b8da4ca8671815cda1557" },
                    { "UnityChanToonShader/Mobile/Toon_DoubleShadeWithFeather_StencilOut", "55e8b9eeaaff205469365133fe7bc744" },
                    { "UnityChanToonShader/Mobile/Toon_DoubleShadeWithFeather_TransClipping", "d4c592285a93c3844aafdaafffc07ec7" },
                    { "UnityChanToonShader/Mobile/Toon_DoubleShadeWithFeather_TransClipping_StencilMask", "100d373b596f44d49ac9bb944d671d32" },
                    { "UnityChanToonShader/Mobile/AngelRing/Toon_ShadingGradeMap", "23e399973d807464fb195291a44a614c" },
                    { "UnityChanToonShader/Mobile/AngelRing/Toon_ShadingGradeMap_StencilOut", "8d33e4e4084e5af449f3e762fecce3c9" },
                    { "UnityChanToonShader/Mobile/Toon_ShadingGradeMap", "f90e11a40dcf4f745ae6b21b857943fa" },
                    { "UnityChanToonShader/Mobile/Toon_ShadingGradeMap_StencilMask", "206c554c8b0c60041a9d242385f543d3" },
                    { "UnityChanToonShader/Mobile/Toon_ShadingGradeMap_StencilOut", "cfc201757f2519c4bb6ef9265a046582" },
                    { "UnityChanToonShader/Mobile/Toon_ShadingGradeMap_TransClipping", "cce1da34c52aff745adf0222f56a356c" },
                    { "UnityChanToonShader/Mobile/Toon_ShadingGradeMap_TransClipping_StencilMask", "e88039bab21b7894e918126e8fce5d1b" },
                    // ゲーミング波紋シェーダー
                    // <https://noriben.booth.pm/items/1205481>
                    { "Noriben/CircleLED", "5f94a00059794d8469e1ec8b94e48a09" },
                    { "Noriben/FlowLED", "bc6048cc78f66194a8e162f2d79ba974" },
                }, "ブース入稿規定の使用可能シェーダーをご参照ください。", "https://id.pokemori.jp/cross-market2/#shaders"),
#endif
            };
        }

        protected long FolderSizeLimit => 50 * this.MegaByte;

        protected Vector3 BoothSizeLimit => new Vector3(10, 10, 10);

#if VRC_SDK_VRCSDK2
        protected virtual VRC_EventHandler.VrcBroadcastType[] VRCTriggerBroadcastTypesWhitelist
        {
            get
            {
                return new VRC_EventHandler.VrcBroadcastType[]{
                    VRC_EventHandler.VrcBroadcastType.Local,
                    VRC_EventHandler.VrcBroadcastType.AlwaysUnbuffered,
                    VRC_EventHandler.VrcBroadcastType.MasterUnbuffered,
                    VRC_EventHandler.VrcBroadcastType.OwnerUnbuffered };
            }
        }

        protected virtual VRC_EventHandler.VrcEventType[] VRCTriggerActionWhitelist
            => (VRC_EventHandler.VrcEventType[])Enum.GetValues(typeof(VRC_EventHandler.VrcEventType));
#endif

        protected int VRCTriggerCountLimit => 10;

        protected int LightmapCountLimit => 1;

        private ComponentReference[] GetComponentReferences()
        {
            return new ComponentReference[] {
                new ComponentReference(null, new[]
                {
                    "VRCSDK2.VRCTriggerRelay",
                    "VRCSDK2.VRC_AudioBank",
                    "VRCSDK2.VRC_DataStorage",
                    "VRCSDK2.VRC_EventHandler",
                    "VRCSDK2.VRC_IKFollower",
                    "VRCSDK2.VRC_Label",
                    "VRCSDK2.VRC_ObjectSpawn",
                    "VRCSDK2.VRC_ObjectSync",
                    "VRCSDK2.VRC_SpatialAudioSource",
                    "VRCSDK2.VRC_SpecialLayer",
                    "VRCSDK2.VRC_Trigger",
                    "VRCSDK2.VRC_UiShape",
                    "TMPro.TMP_Dropdown",
                    "TMPro.TMP_InputField",
                    "TMPro.TMP_ScrollbarEventHandler",
                    "TMPro.TMP_SelectionCaret",
                    "TMPro.TMP_SpriteAnimator",
                    "TMPro.TMP_SubMesh",
                    "TMPro.TMP_SubMeshUI",
                    "TMPro.TMP_Text",
                    "TMPro.TextMeshPro",
                    "TMPro.TextMeshProUGUI",
                    "TMPro.TextContainer",
                    "TMPro.TMP_Dropdown+DropdownItem",
                    "UnityEngine.UI.Button",
                    "UnityEngine.UI.Dropdown",
                    "UnityEngine.UI.Dropdown+DropdownItem",
                    "UnityEngine.UI.GraphicRaycaster",
                    "UnityEngine.UI.InputField",
                    "UnityEngine.UI.Mask",
                    "UnityEngine.UI.RawImage",
                    "UnityEngine.UI.RectMask2D",
                    "UnityEngine.UI.Scrollbar",
                    "UnityEngine.UI.ScrollRect",
                    "UnityEngine.UI.Selectable",
                    "UnityEngine.UI.Slider",
                    "UnityEngine.UI.Text",
                    "UnityEngine.UI.Toggle",
                    "UnityEngine.UI.ToggleGroup",
                    "UnityEngine.UI.AspectRatioFitter",
                    "UnityEngine.UI.CanvasScaler",
                    "UnityEngine.UI.ContentSizeFitter",
                    "UnityEngine.UI.GridLayoutGroup",
                    "UnityEngine.UI.HorizontalLayoutGroup",
                    "UnityEngine.UI.HorizontalOrVerticalLayoutGroup",
                    "UnityEngine.UI.LayoutElement",
                    "UnityEngine.UI.LayoutGroup",
                    "UnityEngine.UI.VerticalLayoutGroup",
                    "UnityEngine.UI.BaseMeshEffect",
                    "UnityEngine.UI.Outline",
                    "UnityEngine.UI.PositionAsUV1",
                    "UnityEngine.UI.Shadow",
                    "ONSPAudioSource",
                    "UnityEngine.WindZone",
                    "UnityEngine.Tilemaps.Tilemap",
                    "UnityEngine.Tilemaps.TilemapRenderer",
                    "UnityEngine.Terrain",
                    "UnityEngine.Tree",
                    "UnityEngine.ParticleEmitter",
                    "UnityEngine.EllipsoidParticleEmitter",
                    "UnityEngine.MeshParticleEmitter",
                    "UnityEngine.ParticleAnimator",
                    "UnityEngine.ParticleRenderer",
                    "UnityEngine.WorldParticleCollider",
                    "UnityEngine.Grid",
                    "UnityEngine.GridLayout",
                    "UnityEngine.AudioSource",
                    "UnityEngine.AudioReverbZone",
                    "UnityEngine.AudioLowPassFilter",
                    "UnityEngine.AudioHighPassFilter",
                    "UnityEngine.AudioDistortionFilter",
                    "UnityEngine.AudioEchoFilter",
                    "UnityEngine.AudioChorusFilter",
                    "UnityEngine.AudioReverbFilter",
                    "UnityEngine.Playables.PlayableDirector",
                    "UnityEngine.TerrainCollider",
                    "UnityEngine.Canvas",
                    "UnityEngine.CanvasGroup",
                    "UnityEngine.CanvasRenderer",
                    "UnityEngine.GUIText",
                    "UnityEngine.TextMesh",
                    "UnityEngine.Animation",
                    "UnityEngine.Animator",
                    "UnityEngine.AI.NavMeshAgent",
                    "UnityEngine.AI.NavMeshObstacle",
                    "UnityEngine.AI.OffMeshLink",
                    "UnityEngine.Cloth",
                    "UnityEngine.WheelCollider",
                    "UnityEngine.Rigidbody",
                    "UnityEngine.Joint",
                    "UnityEngine.HingeJoint",
                    "UnityEngine.SpringJoint",
                    "UnityEngine.FixedJoint",
                    "UnityEngine.CharacterJoint",
                    "UnityEngine.ConfigurableJoint",
                    "UnityEngine.ConstantForce",
                    "UnityEngine.Collider",
                    "UnityEngine.BoxCollider",
                    "UnityEngine.SphereCollider",
                    "UnityEngine.CapsuleCollider",
                    "UnityEngine.CharacterController",
                    "UnityEngine.ParticleSystem",
                    "UnityEngine.ParticleSystemRenderer",
                    "UnityEngine.BillboardRenderer",
                    "UnityEngine.SkinnedMeshRenderer",
                    "UnityEngine.Renderer",
                    "UnityEngine.TrailRenderer",
                    "UnityEngine.LineRenderer",
                    "UnityEngine.GUIElement",
                    "UnityEngine.GUITexture",
                    "UnityEngine.GUILayer",
                    "UnityEngine.Light",
                    "UnityEngine.LightProbeGroup",
                    "UnityEngine.LightProbeProxyVolume",
                    "UnityEngine.LODGroup",
                    "UnityEngine.ReflectionProbe",
                    "UnityEngine.Transform",
                    "UnityEngine.RectTransform",
                    "UnityEngine.Rendering.SortingGroup",
                    "UnityEngine.Projector",
                    "UnityEngine.OcclusionPortal",
                    "UnityEngine.OcclusionArea",
                    "UnityEngine.LensFlare",
                    "UnityEngine.MeshFilter",
                    "UnityEngine.Halo",
                    "UnityEngine.MeshRenderer",

                    "OVRLipSync",
                    "OVRLipSyncContext",
                    "OVRLipSyncContextBase",
                    "OVRLipSyncContextCanned",
                    "OVRLipSyncContextMorphTarget",
                    "OVRLipSyncContextTextureFlip",

                    "PhysSound.PhysSoundBase",
                    "PhysSound.PhysSoundObject",
                    "PhysSound.PhysSoundTempAudio",
                    "PhysSound.PhysSoundTempAudioPool",
                    "PhysSound.PhysSoundTerrain",
                }, ValidationLevel.ALLOW),
            };
        }

        protected LightConfigRule.LightConfig ApprovedPointLightConfig
            => new LightConfigRule.LightConfig(new[] { LightmapBakeType.Baked });

        protected LightConfigRule.LightConfig ApprovedSpotLightConfig
            => new LightConfigRule.LightConfig(new[] { LightmapBakeType.Baked });

        protected LightConfigRule.LightConfig ApprovedAreaLightConfig
            => new LightConfigRule.LightConfig(new[] { LightmapBakeType.Baked });

        protected int PickupObjectSyncUsesLimit => 5;
    }
}
