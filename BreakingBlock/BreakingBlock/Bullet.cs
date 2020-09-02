using System;
using Altseed2;

namespace BlockShoot
{
    // 弾のクラス
    public class Bullet : CollidableObject
    {
        // フレーム毎に進む距離
        private float velocity;

        // コンストラクタ
        public Bullet(MainNode mainNode, Vector2F position, float velocity, float angle) : base(mainNode, position)
        {
            // 衝突判定を行わないように設定
            doSurvey = false;

            // 自機弾の座標を設定
            //Position = position;

            // テクスチャを読み込む
            Texture = Texture2D.LoadStrict("Resources/Bullet_Blue.png");

            // 中心座標を設定
            CenterPosition = ContentSize / 2;

            // 弾速を設定
            this.velocity = velocity;

            // 弾の角度は発射された時の角度
            Angle = angle;

            // 半径を設定
            collider.Radius = Texture.Size.X / 2;

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

            // 画面外に出たら自身を削除
            RemoveMyselfIfOutOfWindow();
        }

        // 衝突時に実行
        protected override void OnCollision(CollidableObject obj)
        {
            // 衝突対象がBlockだったら
            if (obj is Block)
            {
                Parent?.RemoveChildNode(this);
            }
        }
    }
}
