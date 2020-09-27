using System;
using System.Net.NetworkInformation;
using Altseed2;

namespace BlockShoot
{
    // プレイヤーのクラス
    public class Bar : CollidableObject
    {
        // ショット時の効果音
        private Sound shotSound;

        // コンストラクタ
        public Bar(MainNode mainNode, Vector2F position) : base(mainNode, 100)
        {
            // 衝突判定を行うように設定
            doSurvey = true;

            // テクスチャを読み込む
            Texture = Texture2D.LoadStrict("Resources/Bar.png");

            // 中心座標を設定
            CenterPosition = ContentSize / 2;

            // 自機の角度(度数法を設定)
            Angle = (float)0.0;

            // コライダの半径を設定
            //collider.Radius = Texture.Size.Y / 2;

            // コライダの半径を設定
            //collider.Radius = Texture.Size.Y / 2;

            // 中心座標を設定
            CenterPosition = ContentSize / 2;

            // 短径コライダの幅・高さを設定
            collider.Size = new Vector2F(Texture.Size.X, Texture.Size.Y);

            collider.Position = new Vector2F(position.X - Texture.Size.X / 2, position.Y - Texture.Size.Y / 2);

            this.Position = position;

            // ショット音を読み込む
            shotSound = Sound.LoadStrict("Resources/shot1.wav", true);
        }

        // フレーム毎に実行
        protected override void OnUpdate()
        {
            // 移動を実行
            Move();

            // CollidableObjectのOnUpdate呼び出し
            base.OnUpdate();
        }

        // プレイヤーの移動を行う
        void Move()
        {
            // 画面外にはいかないように設定する
            var halfSize = Texture.Size / 2;
            if (true)
            //if (Position.X > -halfSize.X
                //&& Position.X < Engine.WindowSize.X + halfSize.X)
            {
                float speed = 1.0f;
                if (Engine.Keyboard.GetKeyState(Key.Space) == ButtonState.Hold)
                {
                    speed = 2.0f;
                }
                // ←キーで左に回転
                if (Engine.Keyboard.GetKeyState(Key.Left) == ButtonState.Hold)
                {
                    Position += new Vector2F(-10.0f, 0.0f) * speed;
                    Console.WriteLine(Angle);
                }

                // →キーで右に回転
                if (Engine.Keyboard.GetKeyState(Key.Right) == ButtonState.Hold)
                {
                    Position += new Vector2F(10.0f, 0.0f) * speed;
                    Console.WriteLine(Angle);
                }
            }
        }
    }
}
