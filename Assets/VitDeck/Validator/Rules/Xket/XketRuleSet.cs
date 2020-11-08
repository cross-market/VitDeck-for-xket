// 当ファイルは Assets/VitDeck/Validator/Rules/Vket5/Vket5RuleSetBase.cs をコピーして改変したもの

using System;
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
                    ignorePrefabGUIDs: Vket5OfficialAssetData.GUIDs,
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
