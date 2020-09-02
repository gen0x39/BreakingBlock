using Altseed2;
using System;
using System.Drawing;
using System.Collections.Generic;

namespace BlockShoot
{
    // メインステージのクラス
    public class MainNode : Node
    {
        // BGMのID
        private int? bgmID = null;

        // キャラクターを表示するノード
        private Node characterNode = new Node();

        // プレイヤーの参照
        private Player player;

        // スコアを表示するノード
        private TextNode scoreNode;


        // 他画面へ遷移しているかどうか
        private bool fading = false;

        // スコア
        public int score;

        // エンジンに追加された時に実行
        protected override void OnAdded()
        {
            // キャラクターノードを追加
            AddChildNode(characterNode);

            // UIを表示するノード
            var uiNode = new Node();

            // UIノードを追加
            AddChildNode(uiNode);

            // 背景に使用するテクスチャ
            var backTexture = new SpriteNode();
            // 背景のテクスチャを読み込む
            backTexture.Texture = Texture2D.LoadStrict("Resources/Background.png");
            // 表示位置を奥に設定
            backTexture.ZOrder = -100;

            // 背景テクスチャを追加
            AddChildNode(backTexture);

            // プレイヤーを設定
            var player = new Player(this, new Vector2F(350, 900));

            // キャラクターノードにプレイヤーを追加
            characterNode.AddChildNode(player);

            // スコアを表示するノードを設定
            scoreNode = new TextNode();
            // スコア表示に使うフォントを読み込む
            scoreNode.Font = Font.LoadDynamicFontStrict("Resources/GenYoMinJP-Bold.ttf", 30);
            // スコア表示の位置を設定
            scoreNode.Position = new Vector2F();

            // UIノードにスコア表示ノードを追加
            uiNode.AddChildNode(scoreNode);

            // BGMを初期化する
            InitBGM();
        }

        // BGMを初期化
        private void InitBGM()
        {
            // BGMを読み込む
            var bgm = Sound.LoadStrict("Resources/BGM.wav", false);

            // BGMをループするように設定
            bgm.IsLoopingMode = true;

            // ループ開始位置を設定
            bgm.LoopStartingPoint = 11.33f;

            // ループ終了位置を設定
            bgm.LoopEndPoint = 33.93f;

            // BGMのプレイ開始
            bgmID = Engine.Sound.Play(bgm);
        }

        // カウンタ
        private int counter = 0;

        // フレーム毎に実行
        private PointF[] positions =
        {
            new PointF(150, 150),
            new PointF(250, 150),
            new PointF(350, 150),
            new PointF(450, 150),
            new PointF(550, 150)
        };

        // フレーム毎に実行
        /*
         *   1. カウンタを作る
         *   2. 一定時間たったらブロックを生成
         *   3. 生成したブロックと今あるブロックを移動させる
         */
        protected override void OnUpdate()
        {
            // スコア表示の更新
            scoreNode.Text = "Score : " + score;

            if (score >= 100)
            {
                if (!fading)
                {
                    if (bgmID.HasValue)
                    {
                        Engine.Sound.FadeOut(bgmID.Value, 1.0f);

                        bgmID = null;
                    }

                    Engine.RemoveNode(this);

                    Engine.AddNode(new LevelCompletedNode());

                    fading = true;
                }

            }

            //  1.  カウンタ
            counter++;
            if (counter % 100 == 0)
            {
                //  2. ブロックを2つ生成(違う場所)
                List<int> indexs = new List<int>();

                // 重複しない2つの乱数を取り出したい
                for (int i = 0; i < 5; i++)
                {
                    indexs.Add(i);
                }
                Random r = new System.Random();
                int index1 = r.Next(0, 5);
                PointF p1 = positions[indexs[index1]];
                indexs.RemoveAt(index1);
                int index2 = r.Next(0, 4);
                PointF p2 = positions[indexs[index2]];
                var block1 = new Block(this, new Vector2F(p1.X, p1.Y), r.Next(1, 6));
                characterNode.AddChildNode(block1);
                var block2 = new Block(this, new Vector2F(p2.X, p2.Y), r.Next(1, 6));
                characterNode.AddChildNode(block2);

                //  3. ブロックを移動させる
                Console.WriteLine(150);
                //Console.WriteLine(counter);
            }

        }
    }
    
}

/*
 *   


            // 自機
            var line = new LineNode();
            line.Point1 = new Vector2F(10.0f, 10.0f);
            line.Point1 = new Vector2F(100.0f, 10.0f);
            line.Thickness = 2.0f;


            // 自機をエンジンに追加
            Engine.AddNode(block);
            Engine.AddNode(line);
 */