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

        // プレイヤーへの参照
        protected Player player;

        // コンストラクタ
        public Block(MainNode mainNode, Vector2F position, int hp) : base(mainNode, position)
        {
            // 衝突判定を行うように設定
            doSurvey = true;

            // 座標を設定
            // Position = position;

            // ブロックのHPの設定
            this.hp = hp;

            // テクスチャの設定
            switch (hp)
            {
                case 1:
                    Texture = Texture2D.LoadStrict("Resources/block1.png");
                    break;

                case 2:
                    Texture = Texture2D.LoadStrict("Resources/block2.png");
                    break;

                case 3:
                    Texture = Texture2D.LoadStrict("Resources/block3.png");
                    break;

                case 4:
                    Texture = Texture2D.LoadStrict("Resources/block4.png");
                    break;

                default:
                    Texture = Texture2D.LoadStrict("Resources/block5.png");
                    break;
            }

            // コライダの半径を設定
            collider.Radius = Texture.Size.Y / 2;

            // 中心座標を設定
            CenterPosition = ContentSize / 2;
        }

        // カウンタ
        private int counter = 0;

        protected override void OnUpdate()
        {
            counter++;
            if (counter % 100 == 0)
            {
                //  3. ブロックを移動させる
                Move();

            }
            // CollidableOnjectのOnupdate呼び出し
            base.OnUpdate();
        }

        // プレイヤーの移動を行う
        void Move()
        {
            if (Position.Y < 750)
            {
                Position += new Vector2F(0.0f, 100.0f);
            }
        }

        // 衝突時に実行
        protected override void OnCollision(CollidableObject obj)
        {
            // 衝突対象が自機弾だったら
            if (obj is Bullet)
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
                    switch(hp)
                    {
                        case 1:
                            Texture = Texture2D.LoadStrict("Resources/block1.png");
                            break;

                        case 2:
                            Texture = Texture2D.LoadStrict("Resources/block2.png");
                            break;

                        case 3:
                            Texture = Texture2D.LoadStrict("Resources/block3.png");
                            break;

                        case 4:
                            Texture = Texture2D.LoadStrict("Resources/block4.png");
                            break;

                        default:
                            Texture = Texture2D.LoadStrict("Resources/block5.png");
                            break;
                    }
                }
            }
        }
    }
}