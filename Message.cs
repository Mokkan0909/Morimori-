using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace niconico
{
    //コメント情報の保管・スライドを行うクラス
    class Message
    {
        //フィールドの決定
        private String message, color;
        private Dictionary<string, Brush> fontColorMap = new Dictionary<string, Brush>();
        private Dictionary<string, double> fontSizeMap = new Dictionary<string, double>();
        private Dictionary<string, Image> ImageMap = new Dictionary<string, Image>();
        private Canvas canvas;
        private Double Height, Width;
        BitmapImage bitmap;
        private int verticalPosition = 0;
        public delegate void MoveMessageDelegate();
        
        //コメント内容,文字色,表示位置などを決定
        public Message(String text, Canvas canvas, Double Height, Double Width, String Color)
        {
            this.message = text;
            this.canvas = canvas;
            this.Height = Height;
            this.Width = Width;
            this.color = Color;
            fontColorMap.Add("blue", Brushes.Aqua);
            fontColorMap.Add("red", Brushes.Red);
            fontColorMap.Add("green", Brushes.PaleGreen);
            fontColorMap.Add("yallow", Brushes.LemonChiffon);

            fontSizeMap.Add("big", 200.0);
            fontSizeMap.Add("small", 50.0);

           // bitmap = new BitmapImage(new Uri("Z:/401_便利なもの/201_LINEスタンプ_ハトヤマ先生の日常/10.png"));


        }

        public void Move()
        {
            //マルチスレッド処理を行いスレッドごとにメソッドを実行
            Dispatcher dispatcher = Application.Current.Dispatcher;
            if (!dispatcher.CheckAccess())
            {
                //非同期処理を実行
                dispatcher.BeginInvoke(DispatcherPriority.Normal, new MoveMessageDelegate(this.MoveMessage));
            }
            else
            {
                MoveMessage();
            }
            
        }
        
        public void MoveMessage()
        {
                
                // フォントサイズの決定
                double fontsize = 100;
                foreach (string sizeName in fontSizeMap.Keys)
                {
                    if (message.Contains(sizeName))
                    {
                        message = message.Remove(message.IndexOf(sizeName), sizeName.Length);
                        fontsize = fontSizeMap[sizeName];
                    }
                }
                // フォントカラーの決定
                Brush fontcolor = Brushes.White;
                //Console.WriteLine(color);

                foreach (string colorname in fontColorMap.Keys)
                {
                    if (colorname == color)
                    {
                        fontcolor = fontColorMap[colorname];
                    }
                }
                // 新しいテキストを生成
                TextBlock textBlock = new TextBlock();
                textBlock.FontSize = fontsize;
                textBlock.Text = message.Trim();
                textBlock.Foreground = fontcolor;
                canvas.Children.Add(textBlock);
                // テキストに影をつける
                DropShadowBitmapEffect effect = new DropShadowBitmapEffect();
                effect.ShadowDepth = 4;
                effect.Direction = 330;
                effect.Color = (Color)ColorConverter.ConvertFromString("black");
                textBlock.BitmapEffect = effect;
                // 文字サイズに合わせてテキストの位置を指定
                verticalPosition += (int)fontsize;
                if (verticalPosition >= this.Height) verticalPosition = 0;
                TranslateTransform transform = new TranslateTransform(this.Width, this.Height);
                // テキストのアニメーション
                textBlock.RenderTransform = transform;
                Duration duration;
                //文字数によって流す速さを変更
                if (message.Length < 10)
                {
                    duration = new Duration(TimeSpan.FromMilliseconds(5000));
                }
                else if(message.Length > 30)
                {
                    duration = new Duration(TimeSpan.FromMilliseconds(20000));
                }
                else
                {
                    duration = new Duration(TimeSpan.FromMilliseconds(message.Length * 500));
                }

                //アニメーションを使用して右から左に流す(横方向のためX座標)
                DoubleAnimation animationX = new DoubleAnimation(-1 * message.Length * fontsize, duration);
                transform.BeginAnimation(TranslateTransform.XProperty, animationX);
        }

        //スタンプ表示の機構（未完成）
        public void movestamps()
        {
            // 新しい画像を生成
            Image image1 = new Image();
            image1.Source = bitmap;
            canvas.Children.Add(image1);
            // 画像の位置を指定
            TranslateTransform transform = new TranslateTransform(this.Width - 300, this.Height);



            // テキストのアニメーション
            image1.RenderTransform = transform;

            var fadeInAnimation = new DoubleAnimation();
            fadeInAnimation.Duration = new Duration(new TimeSpan(0, 0, 0, 0, 250));
            fadeInAnimation.From = 0.0;
            fadeInAnimation.To = 1.0;
            fadeInAnimation.DecelerationRatio = 0.8;
            Storyboard.SetTargetProperty(fadeInAnimation, new PropertyPath("Opacity"));

            var fadeOutAnimation = new DoubleAnimation();
            fadeOutAnimation.Duration = new Duration(new TimeSpan(0, 0, 0, 0, 500));
            fadeOutAnimation.From = 1.0;
            fadeOutAnimation.To = 0.0;
            fadeInAnimation.DecelerationRatio = 0.8;
            Storyboard.SetTargetProperty(fadeOutAnimation, new PropertyPath("Opacity"));


            Duration duration = new Duration(TimeSpan.FromMilliseconds(1000));
            DoubleAnimation animationY = new DoubleAnimation(this.Height - 80, duration);
            DoubleAnimation animationX = new DoubleAnimation(this.Height - 80, duration);
            AnimationClock myClock = animationY.CreateClock();
            animationY.AutoReverse = true;
            animationX.AutoReverse = true;
            animationY.BeginTime = (TimeSpan.FromMilliseconds(2000));
            transform.BeginAnimation(TranslateTransform.YProperty, animationY);
            transform.BeginAnimation(TranslateTransform.XProperty, animationX);
            
            

        }
        


    }
}
