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
        public Node characterNode = new Node();

        // スコアを表示するノード
        private TextNode scoreNode;

        // Blockへの参照, Nodeだと子要素が読み取り専用なので
        // HPを2倍にするときに使う
        public List<Block> blockList = new List<Block>();

        // 他画面へ遷移しているかどうか
        private bool fading = false;

        // スコア
        public int score;

        // 特殊なブロック(Speed, HP, Ball)への参照

        public void AddBall()
        {
            var ball = new Ball(this, new Vector2F(1100, 915), 10.0f);
            characterNode.AddChildNode(ball);
        }

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
            //var backTexture = new SpriteNode();
            // 背景のテクスチャを読み込む
            //backTexture.Texture = Texture2D.LoadStrict("Resources/Background.png");
            // 表示位置を奥に設定
            //backTexture.ZOrder = -100;

            // 背景テクスチャを追加
            //AddChildNode(backTexture);

            // バーを設定
            var bar = new Bar(this, new Vector2F(640, 900));
            characterNode.AddChildNode(bar);

            // ボールを設定
            //var ball = new Ball(this, new Vector2F(1100, 915), 10.0f);
            var ball = new Ball(this, new Vector2F(640, 850), 10.0f);
            characterNode.AddChildNode(ball);

            // ブロック崩しの高さと横幅
            int height = 1;
            int width = 10;
            
            // 特殊ブロックの配置を確認するための2次元配列
            // この書き方だと任意の要素がFalseで初期化されていた
            bool[,] isBlock = new bool[height, width];

            // 重複しない2つの乱数を取り出したい
            Random rand = new System.Random();

            int len = height * width;
            int[] numbers = new int[len];

            // 0 ~ height * widthまでの並んだデータを作成
            for (int i = 0; i < len; i++)
            {
                numbers[i] = i;
            }

            // シャッフル
            for (int i = numbers.Length - 1; i > 0; i--)
            {
                int j = rand.Next(i + 1);
                int tmp = numbers[i];
                numbers[i] = numbers[j];
                numbers[j] = tmp;
            }

            int blockKind = 3;  // ブロックの種類
            int blockNum = 3;   // 各ブロックの個数
            for (int i = 0; i < blockKind * blockNum; i++)
            {
                int index = numbers[i];
                int h = index % height;
                int w = index / width;
                Console.WriteLine(string.Format("i : {0}, j : {1} index : {2}", h, w, index));
                isBlock[h, w] = true;
            }
            var r = new Random();

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (!isBlock[i, j])
                    {
                        // HPを1～3までの範囲で設定
                        int hp = r.Next(1, 4);
                        //Console.WriteLine(hp);

                        // ブロックの場所を設定
                        float x = (j * 128 + 64);
                        float y = (i * 30 + 15) + 10;

                        var block = new Block(this, new Vector2F(x, y), hp);
                        //blockList.Add(block);
                        characterNode.AddChildNode(block);
                        //Console.WriteLine(isBlock[i, j]);
                    }
                    else
                    {
                        Console.WriteLine("hoge");
                    }
                }
            }

            for (int i = 0; i < blockKind * blockNum; i++)
            {
                int index = numbers[i];
                int h = index % height;
                int w = index / width;

                float x = w * 128 + 64;
                float y = h * 30 + 15 + 10;

                string path;
                if (i % blockKind == 0)
                {
                    path = "Resources/BallBlock.png";
                }
                else if (i % blockKind == 1)
                {
                    path = "Resources/SpeedBlock.png";
                }
                else
                {
                    path = "Resources/HpBlock.png";
                }
                var block = new SpecialBlock(this, new Vector2F(x, y), path, (i % blockKind));
                characterNode.AddChildNode(block);
            }

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
        }
    }
}