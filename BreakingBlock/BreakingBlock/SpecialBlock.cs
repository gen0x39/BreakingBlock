using System;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using Altseed2;
using Microsoft.CSharp.RuntimeBinder;

namespace BlockShoot
{
    // 敵の基礎となるクラス
    public class SpecialBlock : CollidableObject
    {
        protected MainNode mainNode;

        public int kind = 1;

        // コンストラクタ
        public SpecialBlock(MainNode mainNode, Vector2F position, String path, int kind) : base(mainNode, kind)
        {
            this.mainNode = mainNode;
            // 衝突判定を行うように設定
            doSurvey = true;

            this.kind = kind;

            // テクスチャの設定
            Texture = Texture2D.LoadStrict(path);

            // 中心座標を設定
            CenterPosition = ContentSize / 2;

            // 短径コライダの幅・高さを設定
            collider.Size = new Vector2F(Texture.Size.X, Texture.Size.Y);

            collider.Position = new Vector2F(position.X - Texture.Size.X / 2, position.Y - Texture.Size.Y / 2);

            this.Position = position;

            // 中心座標を設定
            CenterPosition = ContentSize / 2;
        }

        protected override void OnUpdate()
        {
            // CollidableOnjectのOnupdate呼び出し
            base.OnUpdate();
        }
        public int GetKind()
        {
            return kind;
        }

        // 衝突時に実行
        protected override void OnCollision(CollidableObject obj)
        {
            // 衝突対象が自機弾だったら
            if (obj is Ball)
            {
                var x = mainNode.characterNode.Children;
                // スコアを加算
                mainNode.score += 1;

                // 死亡時エフェクトを再生
                //Parent.AddChildNode(new DeathEffect(Position));

                // 自身を削除
                Parent.RemoveChildNode(this);

                // 死亡音を読み込む
                //var deathSound = Sound.LoadStrict("Resources/Explosion.wav", true);

                // 死亡音を再生
                //Engine.Sound.Play(deathSound);
            }
        }
    }
}