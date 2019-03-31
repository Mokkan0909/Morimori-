using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Timers;
using System.Windows.Forms;

namespace niconico
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        //フィールドの決定
        private Canvas canvas;
        private ArrayList list = new ArrayList();
        private int  LastID = 0;
        private static DispatcherTimer timer2 = new DispatcherTimer();
        private LogForm1 logform;
        private Screen[] allsc = Screen.AllScreens;
        // MySQLへの接続情報

        public MainWindow()
        {
            InitializeComponent();
            logform = new LogForm1();
            logform.Show();

            // 透明で常に前面に設定
            this.AllowsTransparency = true;
            //Windowサイズの決定（使用モニターの幅に固定）
            this.Height = System.Windows.SystemParameters.VirtualScreenHeight;
            this.Width = System.Windows.SystemParameters.VirtualScreenWidth;
            //フォームの左上をモニターの左上と一致させる
            this.Top = 0;
            this.Left = 0;
            //フォームの全画面表示
            this.WindowState = WindowState.Maximized;
            //画面を完全に透明化して背景と合わせる
            this.Background =  Brushes.Transparent;
            //フォームの枠線のスタイルを決定
            this.WindowStyle = WindowStyle.None;
            //常にWindowの最前面に表示する
            this.Topmost = true;
            //基準となるパネルを設定
            this.canvas = new Canvas();
            this.Content = this.canvas;
            //下にあるメソッドの実行（DBの最終IDをチェック）
            this.LastIDUpdate();

            //一定間隔で処理を実行するための機構
            timer2.Interval = TimeSpan.FromMilliseconds(2500);
            timer2.Tick += new EventHandler(Timer_Elapsed);

            // タイマーを開始する
            timer2.Start();
        }

        private void Timer_Elapsed(object sender, EventArgs e)
        {
            //以下にあるメソッドの実行（DBをチェックして追加されたコメントの読み込み）
            this.DBCheck();
            //コメントを表示する
            foreach (Message ar in list)
            {
                ar.Move();
                //ar.movestamps();
            }
            list.Clear();
        }

        //タイマーを一時停止するメソッド
        public static void Stop_timer()
        {
            timer2.Stop();
        }
        //タイマーを再開するメソッド
        public static void Resume_timer()
        {
            timer2.Start();
        }

        //DateBase(MySQL)に接続しListに追加
        public void DBCheck()
        {


            //DBに接続する際の各引数を設定
            string server = "127.0.0.1";        // MySQLサーバホスト名
            string user = "root";           // MySQLユーザ名
            string pass = "niconico";           // MySQLパスワード
            string database = "message";      // 接続するデータベース名
            string connectionString = string.Format("Server={0};Database={1};Uid={2};Pwd={3};SSL Mode = none", server, database, user, pass);
            int j = 0;


            // MySQLへの接続
            try
            {
                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();
                //Console.WriteLine("MySQLに接続しました！");

                int temp = LastID + 10;

                //SQL文を決定し10件分の情報テーブル(コメント内容，文字色)を読み込み
                string com = "SELECT content, color FROM Comment WHERE ID > " + LastID + "&& ID < " + temp;
                MySqlCommand myCommand = new MySqlCommand(com);
                myCommand.Connection = connection;
                MySqlDataReader reader = myCommand.ExecuteReader();

                //サンプル数だけループ
                while (reader.Read())
                {
                    //読み込んだ情報をmessageクラスの引数としlistに格納
                    string[] row = new string[reader.FieldCount];
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        row[i] = reader.GetString(i);
                    }
                    list.Add(new Message(row[0], this.canvas, j * 100, this.Width, row[1]));
                    //logフォームにコメントを入力
                    logform.messagelog(row[0]);
                    j++;
                }
                //LastIDを更新
                LastID = LastID + j;
                
                // 接続の解除
                connection.Close();

            }
            catch (MySqlException me)
            {
                Console.WriteLine("ERROR: " + me.Message);
            }
        }

        //実行時のDBの総数を取得しLastIDを更新する
        public void LastIDUpdate()
        {


            //DBに接続する際の各引数を設定
            string server = "127.0.0.1";        // MySQLサーバホスト名
            string user = "root";           // MySQLユーザ名
            string pass = "niconico";           // MySQLパスワード
            string database = "message";      // 接続するデータベース名
            string connectionString = string.Format("Server={0};Database={1};Uid={2};Pwd={3};SSL Mode = none", server, database, user, pass);
            int j = 0;

            // MySQLへの接続
            try
            {
                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();
                Console.WriteLine("MySQLに接続しました！");
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM Comment", connection);
                var count = cmd.ExecuteScalar();
                LastID = Convert.ToInt32(count) - 1;

                // 接続の解除
                connection.Close();

            }
            catch (MySqlException me)
            {
                Console.WriteLine("ERROR: " + me.Message);
            }
        }

    }

}
