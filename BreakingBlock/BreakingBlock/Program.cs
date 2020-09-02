using Altseed2;
using System;
using System.Collections.Generic;

namespace BlockShoot
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            // エンジンを初期化
            Engine.Initialize("Tutorial", 1280, 960);

            // タイトル画面をエンジンに追加
            Engine.AddNode(new TitleNode());

            // メイン画面をエンジンに追加
            //Engine.AddNode(new MainNode());

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
            }

            // エンジンの終了処理を行う
            Engine.Terminate();
        }
    }
}