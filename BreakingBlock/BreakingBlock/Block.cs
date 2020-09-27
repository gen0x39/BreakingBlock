using System;
using System.Drawing;
using Altseed2;

namespace BlockShoot
{
    // 敵の基礎となるクラス
    public class Block : CollidableObject
    {
        // ブロックのHP
        protected int hp;
        // コンストラクタ
        public Block(MainNode mainNode, Vector2F position, int hp) : base(mainNode, 100)
        {
            // 衝突判定を行うように設定
            doSurvey = true;

            // ブロックのHPの設定
            this.hp = hp;

            // テクスチャの設定
            String path = "Resources/Block" + hp.ToString() + ".png";
            Texture = Texture2D.LoadStrict(path);

            this.Position = position;

            // 中心座標を設定
            CenterPosition = ContentSize / 2;

            // 短径コライダの幅・高さを設定
            collider.Size = new Vector2F(Texture.Size.X, Texture.Size.Y);
        }

        public void HpDouble()
        {
            hp *= Math.Max(hp, 8);
            String path = "Resources/Block" + hp.ToString() + ".png";
            Texture = Texture2D.LoadStrict(path);
        }

        protected override void OnUpdate()
        {
            // CollidableOnjectのOnupdate呼び出し
            base.OnUpdate();
        }

        // 衝突時に実行
        protected override void OnCollision(CollidableObject obj)
        {
            // 衝突対象が自機弾だったら
            if (obj is Ball)
            {
                // スコアを加算
                mainNode.score += 1;

                // HPを1減らす
                hp--;
                // HPが0になったら自身を削除
                if (hp == 0)
                {
                    // 死亡時エフェクトを再生
                    Parent.AddChildNode(new DeathEffect(Position));

                    // 自身を削除
                    Parent.RemoveChildNode(this);

                    // 死亡音を読み込む
                    var deathSound = Sound.LoadStrict("Resources/Explosion.wav", true);

                    // 死亡音を再生
                    Engine.Sound.Play(deathSound);
                }
                else
                {
                    // HPが減ったのでテクスチャを変更
                    String path = "Resources/Block" + hp.ToString() + ".png";
                    Texture = Texture2D.LoadStrict(path);
                }
            }
        }
    }
}