// 当ファイルは Assets/VitDeck/Validator/Rules/Vket5/Vket5RuleSetBase.cs をコピーして改変したもの

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

                new BoothBoundsRule(LocalizedMessage.Get("Vket5RuleSetBase.BoothBoundsRule.Title"),
                    size: BoothSizeLimit,
                    margin: 0.01f),

                new LightmapSizeLimitRule(
                    LocalizedMessage.Get("Vket5RuleSetBase.LightMapsLimitRule.Title", LightmapCountLimit, 512),
                    lightmapCountLimit: LightmapCountLimit,
                    lightmapResolutionLimit: 512),

                new UsableComponentListRule(LocalizedMessage.Get("Vket5RuleSetBase.UsableComponentListRule.Title"),
                    GetComponentReferences(),
                    ignorePrefabGUIDs: Vket5OfficialAssetData.GUIDs),

                new ReflectionProbeRule(LocalizedMessage.Get("Vket5RuleSetBase.ReflectionProbeRule.Title")),

                new VRCTriggerConfigRule(LocalizedMessage.Get("Vket5RuleSetBase.VRCTriggerConfigRule.Title"),
                            VRCTriggerBroadcastTypesWhitelist,
                            new VRC_Trigger.TriggerType[] {
                                VRC_Trigger.TriggerType.Custom,
                                VRC_Trigger.TriggerType.OnInteract,
                                VRC_Trigger.TriggerType.OnEnterTrigger,
                                VRC_Trigger.TriggerType.OnExitTrigger,
                                VRC_Trigger.TriggerType.OnPickup,
                                VRC_Trigger.TriggerType.OnDrop,
                                VRC_Trigger.TriggerType.OnPickupUseDown,
                                VRC_Trigger.TriggerType.OnPickupUseUp   },
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
                    VRC_EventHandler.VrcBroadcastType.Local };
            }
        }

        protected virtual VRC_EventHandler.VrcEventType[] VRCTriggerActionWhitelist
        {
            get
            {
                return new VRC_EventHandler.VrcEventType[] {
                    VRC_EventHandler.VrcEventType.ActivateCustomTrigger,
                    VRC_EventHandler.VrcEventType.AudioTrigger,
                    VRC_EventHandler.VrcEventType.PlayAnimation,
                    VRC_EventHandler.VrcEventType.SetParticlePlaying,
                    VRC_EventHandler.VrcEventType.SetComponentActive,
                    VRC_EventHandler.VrcEventType.SetGameObjectActive,
                    VRC_EventHandler.VrcEventType.AnimationBool,
                    VRC_EventHandler.VrcEventType.AnimationFloat,
                    VRC_EventHandler.VrcEventType.AnimationInt,
                    VRC_EventHandler.VrcEventType.AnimationIntAdd,
                    VRC_EventHandler.VrcEventType.AnimationIntDivide,
                    VRC_EventHandler.VrcEventType.AnimationIntMultiply,
                    VRC_EventHandler.VrcEventType.AnimationIntSubtract,
                    VRC_EventHandler.VrcEventType.AnimationTrigger};
            }
        }
#endif

        protected int VRCTriggerCountLimit => 10;

        protected int LightmapCountLimit => 1;

        private ComponentReference[] GetComponentReferences()
        {
            return new ComponentReference[] {
                new ComponentReference("VRC_Trigger", new string[]{"VRCSDK2.VRC_Trigger", "VRCSDK2.VRC_EventHandler"}, AdvancedObjectValidationLevel),
                new ComponentReference("VRC_Object Sync", new string[]{"VRCSDK2.VRC_ObjectSync"}, ValidationLevel.DISALLOW),
                new ComponentReference("VRC_Pickup", new string[]{"VRCSDK2.VRC_Pickup"}, ValidationLevel.DISALLOW),
                new ComponentReference("VRC_Audio Bank", new string[]{"VRCSDK2.VRC_AudioBank"}, ValidationLevel.DISALLOW),
                new ComponentReference("VRC_Avatar Pedestal", new string[]{"VRCSDK2.VRC_AvatarPedestal"}, ValidationLevel.DISALLOW),
                new ComponentReference("VRC_Ui Shape", new string[]{"VRCSDK2.VRC_UiShape"}, AdvancedObjectValidationLevel),
                new ComponentReference("Rigidbody", new string[]{"UnityEngine.Rigidbody"}, ValidationLevel.ALLOW),
                new ComponentReference("Cloth", new string[]{"UnityEngine.Cloth"}, ValidationLevel.DISALLOW),
                new ComponentReference("Collider", new string[]{"UnityEngine.SphereCollider", "UnityEngine.BoxCollider", "UnityEngine.SphereCollider", "UnityEngine.CapsuleCollider", "UnityEngine.MeshCollider", "UnityEngine.WheelCollider"}, ValidationLevel.ALLOW),
                new ComponentReference("Dynamic Bone", new string[]{"DynamicBone"}, ValidationLevel.ALLOW),
                new ComponentReference("Dynamic Bone Collider", new string[]{"DynamicBoneCollider"}, ValidationLevel.ALLOW),
                new ComponentReference("Skinned Mesh Renderer", new string[]{"UnityEngine.SkinnedMeshRenderer"}, ValidationLevel.ALLOW),
                new ComponentReference("Mesh Renderer ", new string[]{"UnityEngine.MeshRenderer"}, ValidationLevel.ALLOW),
                new ComponentReference("Mesh Filter", new string[]{"UnityEngine.MeshFilter"}, ValidationLevel.ALLOW),
                new ComponentReference("Particle System", new string[]{"UnityEngine.ParticleSystem", "UnityEngine.ParticleSystemRenderer"}, ValidationLevel.ALLOW),
                new ComponentReference("Trail Renderer", new string[]{"UnityEngine.TrailRenderer"}, ValidationLevel.ALLOW),
                new ComponentReference("Line Renderer", new string[]{"UnityEngine.LineRenderer"}, ValidationLevel.ALLOW),
                new ComponentReference("Light", new string[]{"UnityEngine.Light"}, AdvancedObjectValidationLevel),
                new ComponentReference("LightProbeGroup", new string[]{"UnityEngine.LightProbeGroup"}, AdvancedObjectValidationLevel),
                new ComponentReference("ReflectionProbe", new string[]{"UnityEngine.ReflectionProbe"}, AdvancedObjectValidationLevel),
                new ComponentReference("Camera", new string[]{"UnityEngine.Camera"}, MoreAdvancedObjectValidationLevel),
                new ComponentReference("Projector", new string[]{"UnityEngine.Projector"}, MoreAdvancedObjectValidationLevel),
                new ComponentReference("LookatTarget", new string[]{"UnityStandardAssets.Cameras.LookatTarget" }, MoreAdvancedObjectValidationLevel),
                new ComponentReference("FollowTarget", new string[]{"UnityStandardAssets.Utility.FollowTarget" }, MoreAdvancedObjectValidationLevel),
                new ComponentReference("Suspension", new string[]{"UnityStandardAssets.Vehicles.Car.Suspension" }, MoreAdvancedObjectValidationLevel),
                new ComponentReference("Animator", new string[]{"UnityEngine.Animator"}, ValidationLevel.ALLOW),
                new ComponentReference("Animation", new string[]{"UnityEngine.Animation"}, ValidationLevel.ALLOW),
                new ComponentReference("Audio Source", new string[]{"UnityEngine.AudioSource", "ONSPAudioSource", "VRCSDK2.VRC_SpatialAudioSource"}, ValidationLevel.DISALLOW),
                new ComponentReference("Canvas", new string[]{"UnityEngine.Canvas", "UnityEngine.CanvasGroup", "UnityEngine.RectTransform", "UnityEngine.UI.CanvasScaler", "UnityEngine.UI.GraphicRaycaster", "UnityEngine.UI.AspectRatioFitter", "UnityEngine.UI.LayoutElement", "UnityEngine.UI.ContentSizeFitter", "UnityEngine.UI.HorizontalLayoutGroup", "UnityEngine.UI.VerticalLayoutGroup", "UnityEngine.UI.GridLayoutGroup", "UnityEngine.UI.Text", "UnityEngine.UI.Image", "UnityEngine.UI.RawImage", "UnityEngine.UI.Mask", "UnityEngine.UI.RectMask2D", "UnityEngine.UI.Button", "UnityEngine.UI.InputField", "UnityEngine.UI.Toggle", "UnityEngine.UI.ToggleGroup", "UnityEngine.UI.Slider", "UnityEngine.UI.Scrollbar", "UnityEngine.UI.Dropdown", "UnityEngine.UI.ScrollRect", "UnityEngine.UI.Selectable", "UnityEngine.UI.Shadow", "UnityEngine.UI.Outline", "UnityEngine.UI.PositionAsUV1", "UnityEngine.RectTransform", "UnityEngine.CanvasRenderer"}, ValidationLevel.ALLOW),
                new ComponentReference("VideoPlayer", new string[]{"UnityEngine.Video.VideoPlayer" }, MoreAdvancedObjectValidationLevel),
                new ComponentReference("VRC_Station", new string[]{"VRCSDK2.VRC_Station"}, ValidationLevel.DISALLOW),
                new ComponentReference("VRC_Mirror", new string[]{ "VRCSDK2.VRC_MirrorCamera", "VRCSDK2.VRC_MirrorReflection" }, ValidationLevel.DISALLOW),
                new ComponentReference("VRC_PlayerAudioOverride", new string[]{"VRCSDK2.VRC_PlayerAudioOverride"}, ValidationLevel.DISALLOW),
                new ComponentReference("EventSystem", new string[]{"UnityEngine.EventSystems.EventSystem", "UnityEngine.EventSystems.StandaloneInputModule"}, ValidationLevel.DISALLOW),
                new ComponentReference("StandaloneInputModule", new string[]{"UnityEngine.EventSystems.StandaloneInputModule"}, ValidationLevel.DISALLOW),
                new ComponentReference("VRC_SceneResetPosition", new string[]{"VRCSDK2.VRC_SceneResetPosition"}, ValidationLevel.DISALLOW),
                new ComponentReference("PlayableDirector", new string[]{"UnityEngine.Playables.PlayableDirector" }, ValidationLevel.DISALLOW),
                new ComponentReference("VRC_Panorama", new string[]{"VRCSDK2.scripts.Scenes.VRC_Panorama" }, ValidationLevel.DISALLOW),
                new ComponentReference("VRC_SyncVideoPlayer", new string[]{"VRCSDK2.VRC_SyncVideoPlayer", "VRCSDK2.VRC_SyncVideoStream" }, ValidationLevel.DISALLOW),
            };
        }

        protected virtual ValidationLevel AdvancedObjectValidationLevel
        {
            get
            {
                return ValidationLevel.ALLOW;
            }
        }

        protected virtual ValidationLevel MoreAdvancedObjectValidationLevel
        {
            get
            {
                return ValidationLevel.DISALLOW;
            }
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
