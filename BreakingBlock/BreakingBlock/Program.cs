using Altseed2;
using System;

namespace BreakingBlock
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            // エンジンを初期化
            Engine.Initialize("Tutorial", 1280, 960);

            // バー
            var bar = new RectangleNode();
            bar.RectangleSize = new Vector2F(70, 10);
            bar.Position = new Vector2F(640, 880);
            bar.CenterPosition = bar.ContentSize / 2;
            Engine.AddNode(bar);

            // ボール
            var ball = new CircleNode();
            ball.VertNum = 100;
            ball.Radius = 10.0f;
            ball.Color = new Color(255, 255, 0);
            ball.Position = new Vector2F(640, 880);
            ball.CenterPosition = ball.ContentSize / 2;
            Engine.AddNode(ball);


            


            // メインループ
            while (Engine.DoEvents())
            {
                // エンジンを更新
                Engine.Update();



                // Escapeキーでゲーム終了
                if (Engine.Keyboard.GetKeyState(Key.Escape) == ButtonState.Push)
                {
                    break;
                }
                // →キーで
                if (Engine.Keyboard.GetKeyState(Key.Right) == ButtonState.Hold)
                {
                    bar.Position += new Vector2F(2.5f, 0.0f);
                }
                // ←キーで
                if (Engine.Keyboard.GetKeyState(Key.Left) == ButtonState.Hold)
                {
                    bar.Position += new Vector2F(-2.5f, 0.0f);
                }
            }

            // エンジンの終了処理を行う
            Engine.Terminate();
        }
    }
}