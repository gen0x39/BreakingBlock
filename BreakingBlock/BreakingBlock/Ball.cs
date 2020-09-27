using System;
using Altseed2;

namespace BlockShoot
{
    // 弾のクラス
    public class Ball : CollidableObject
    {
        // フレーム毎に進む距離
        private float velocity;

        // mainNodeへの参照
        protected MainNode mainNode;

        // コンストラクタ
        public Ball(MainNode mainNode, Vector2F position, float velocity) : base(mainNode, 100)
        {
            // mainNodeへの参照
            this.mainNode = mainNode;

            // 衝突判定を行わないように設定
            doSurvey = false;

            // テクスチャを読み込む
            Texture = Texture2D.LoadStrict("Resources/Ball.png");

            // 中心座標を設定
            CenterPosition = ContentSize / 2;

            // 弾速を設定
            this.velocity = velocity;

            // 弾の角度は発射された時の角度
            Angle = 0.0f;

            collider.Position = new Vector2F(position.X - Texture.Size.X / 2, position.Y - Texture.Size.Y / 2);

            this.Position = position;

            // 中心座標を設定
            CenterPosition = ContentSize / 2;

            // 短径コライダの幅・高さを設定
            collider.Size = new Vector2F(Texture.Size.X, Texture.Size.Y);

            // 自機弾の表示位置を自機より奥に設定
            ZOrder--;
        }
        // フレーム毎に実行
        protected override void OnUpdate()
        {
            // Shooterの角度に応じて弾を発射する
            var x = -1 * velocity * Math.Sin(Angle * (Math.PI / 180));
            var y = velocity * Math.Cos(Angle * (Math.PI / 180));
            Position -= new Vector2F((float)x, (float)y);

            // CollidableObjectのOnUpdateを呼び出す
            base.OnUpdate();

            //壁で反射
            ReflectionOnWall();


            // 画面外に出たら自身を削除
            RemoveMyselfIfOutOfWindow();
        }

        private void ReflectionOnWall()
        {
            var halfSize = Texture.Size / 2;
            // 左右で反射
            if (Position.X < -halfSize.X
                || Position.X > Engine.WindowSize.X + halfSize.X)
            {
                Console.WriteLine("Out Of Window! Angle {0}", Angle);
                Angle = -1.0f * Angle;
            }
            // 上で反射
            if (Position.Y < -halfSize.Y)
            {
                Console.WriteLine("Out Of Window! Angle {0}", Angle);
                //float angle = Angle - 180.0f;
                Angle = 180.0f - Angle;
            }
        }

        // 衝突時に実行
        protected override void OnCollision(CollidableObject obj)
        {
            // 衝突対象がBlockだったら
            if (obj is Block)
            {
                // 反射
                Angle = 180.0f - Angle;
                Console.WriteLine("Angle is {0}", Angle);
                //Parent?.RemoveChildNode(this);
            }
            else if(obj is Bar)
            {
                float angle = 45.0f;
                float ballCenter = Position.X + ContentSize.X / 2;
                float barCenter = obj.Position.X + obj.ContentSize.X / 2;
                float toAngle = ballCenter - barCenter;
                toAngle = toAngle * angle / obj.ContentSize.X / 2;
                Console.WriteLine("Ball {0}, Bar {1}, toAngle {2}", ballCenter, barCenter, toAngle);
                Angle = (float)toAngle;
                //Angle = 0.0f;
            }
            else if(obj is SpecialBlock)
            {
                
                switch (obj.kind)
                {
                    // Ball
                    case 0:
                        mainNode.AddBall();
                        break;
                    // Speed
                    case 1:
                        velocity *= 2;
                        break;
                    // HP
                    default:
                        // 読み取りをする
                        var x = mainNode.characterNode.Children;

                        // Copy

                        // もとのBlockを削除

                        // 
                        break;
                }
                
            }
        }
    }
}
