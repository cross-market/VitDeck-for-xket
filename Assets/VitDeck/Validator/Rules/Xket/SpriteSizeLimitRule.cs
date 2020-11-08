using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

namespace VitDeck.Validator
{
    /// <summary>
    /// スプライト (Sprite) 画像の解像度を制限します。
    /// </summary>
    public class SpriteSizeLimitRule : BaseRule
    {
        private readonly int resolutionLimit;

        /// <param name="name">ルール名。</param>
        /// <param name="resolutionLimit">1辺のピクセル数の上限値 (width = height)。</param>
        public SpriteSizeLimitRule(string name, int resolutionLimit) : base(name)
        {
            this.resolutionLimit = resolutionLimit;
        }

        protected override void Logic(ValidationTarget target)
        {
            var alreadyCheckedSpriteAssetPaths = new List<string>();

            var baseFolderPath = target.GetBaseFolderPath();
            foreach (var gameObject in target.GetAllObjects())
            {
                foreach (var sprite in this.GetSprites(gameObject))
                {
                    var path = AssetDatabase.GetAssetPath(sprite);
                    if (alreadyCheckedSpriteAssetPaths.Contains(path))
                    {
                        continue;
                    }

                    alreadyCheckedSpriteAssetPaths.Add(path);

                    if (!path.StartsWith(baseFolderPath)
                        || sprite.rect.width <= resolutionLimit && sprite.rect.height <= resolutionLimit)
                    {
                        continue;
                    }

                    this.AddIssue(new Issue(gameObject, IssueLevel.Error, string.Format(
                        "スプライト画像の解像度が上限({0}×{0}px)を超えています。({1}×{2}px)",
                        this.resolutionLimit,
                        sprite.rect.width,
                        sprite.rect.height
                    )));
                    break;
                }
            }
        }

        /// <summary>
        /// 指定されたオブジェクトが参照するスプライト画像を取得します。
        /// </summary>
        /// <param name="gameObject"></param>
        /// <returns></returns>
        private IEnumerable<Sprite> GetSprites(GameObject gameObject)
        {
            var image = gameObject.GetComponent<Image>();
            var spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            var spriteMask = gameObject.GetComponent<SpriteMask>();
            return new[] {
                // ?. 演算子はUnityのオブジェクトに対しては使えない
                image ? image.sprite : null,
                spriteRenderer ? spriteRenderer.sprite : null,
                spriteMask ? spriteRenderer.sprite : null,
            }.Where(sprite => sprite);
        }
    }
}
