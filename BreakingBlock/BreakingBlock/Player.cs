using System;
using Altseed2;

namespace BlockShoot
{
    // プレイヤーのクラス
    public class Player : CollidableObject
    {
        // ショット時の効果音
        private Sound shotSound;

        // コンストラクタ
        public Player(MainNode mainNode, Vector2F position) : base(mainNode, position)
        {
            // 座標を設定
            //Position = position;

            // 衝突判定を行うように設定
            doSurvey = true;

            // テクスチャを読み込む
            Texture = Texture2D.LoadStrict("Resources/Shooter.png");

            // 中心座標を設定
            CenterPosition = ContentSize / 2;

            // 自機の角度(度数法を設定)
            Angle = (float)0.0;

            // コライダの半径を設定
            collider.Radius = Texture.Size.Y / 2;

            // ショット音を読み込む
            shotSound = Sound.LoadStrict("Resources/shot1.wav", true);
        }

        // フレーム毎に実行
        protected override void OnUpdate()
        {
            // 移動を実行
            Move();

            // ショットを実行
            Shot();

            // CollidableObjectのOnUpdate呼び出し
            base.OnUpdate();
        }

        // プレイヤーの移動を行う
        void Move()
        {
            // 回転させる角度
            float rotationAngle = 1.0f;

            // ←キーで左に回転
            if (Engine.Keyboard.GetKeyState(Key.Left) == ButtonState.Hold)
            {
                Angle -= rotationAngle;
                Console.WriteLine(Angle);
            }

            // →キーで右に回転
            if (Engine.Keyboard.GetKeyState(Key.Right) == ButtonState.Hold)
            {
                Angle += rotationAngle;
                Console.WriteLine(Angle);
            }
        }

        // ショット
        private void Shot()
        {
            // Spaceキーが押された時に実行
            if (Engine.Keyboard.GetKeyState(Key.Space) == ButtonState.Push)
            {
                // Spaceキーでショットを放つ
                Parent.AddChildNode(new Bullet(mainNode, Position, 30.0f, Angle));

                // ショット音を鳴らす
                Engine.Sound.Play(shotSound);
            }
        }
    }
}
