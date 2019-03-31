using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace niconico
{
    using System;
    using System.ComponentModel;
    using System.Windows;
 
    /// <summary>
    /// タスクトレイ通知アイコン
    /// </summary>
    public partial class taskicon : Component
    {
        public taskicon()
        {
            InitializeComponent();
            
            // コンテキストメニューのイベントを設定
            this.toolStripMenu_Open.Click += this.toolStripMenuItem_Open_Click;
            this.toolStripMenu_Exit.Click += this.toolStripMenuItem_Exit_Click;
            this.toolStripMenu_Stop.Click += this.toolStripMenuItem_Stop_Click;
            this.toolStripMenu_Resume.Click += this.toolStripMenuItem_Resume_Click;
        }

        public taskicon(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        /// <summary>
        /// コンテキストメニュー "表示" を選択したとき呼ばれます。
        /// </summary>
        /// <param name="sender">呼び出し元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void toolStripMenuItem_Open_Click(object sender, EventArgs e)
        {
            // MainWindow を生成、表示
            //var wnd = new MainWindow();
            //wnd.Show();
        }

        /// <summary>
        /// コンテキストメニュー "終了" を選択したとき呼ばれます。
        /// </summary>
        /// <param name="sender">呼び出し元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void toolStripMenuItem_Exit_Click(object sender, EventArgs e)
        {
            // 現在のアプリケーションを終了
            Application.Current.Shutdown();
        }

        //コメントのスライドを再開
        private void toolStripMenuItem_Resume_Click(object sender, EventArgs e)
        {
            MainWindow.Resume_timer();
        }
        //コメントのスライドを一時停止
        private void toolStripMenuItem_Stop_Click(object sender, EventArgs e)
        {
            MainWindow.Stop_timer();
        }
    }
}
