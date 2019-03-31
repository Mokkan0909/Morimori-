using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace niconico
{
    public partial class LogForm1 : Form
    {
        private ArrayList list = new ArrayList();
        private string all = "";
        public LogForm1()
        {
            InitializeComponent();
            //フォームの表示位置を取得
            int h = System.Windows.Forms.Screen.GetWorkingArea(this).Height - this.Height;
            int w = System.Windows.Forms.Screen.GetWorkingArea(this).Width - this.Width;

            this.SetDesktopLocation(w, h);

            this.messagebox.Font = new Font("Arial", 12);

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        //ログに新規コメントを追加していくメソッド
        public void messagelog(string message)
        {            
            foreach(String temp in StringExtensions.SubstringAtCount(message, 15))
            {
                all = all + temp + "\r\n";
            }

            messagebox.Text = all;
            //キャロットの位置をテキストの最終位置まで移動しスクロール
            messagebox.SelectionStart = messagebox.Text.Length;
            messagebox.ScrollToCaret();
        }


        //メニューを介してスライド機能を再開するメソッド
        private void 表示ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainWindow.Resume_timer();
        }
        //メニューを介してスライド機能を一時停止するメソッド
        private void 一時停止ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainWindow.Stop_timer();
        }
    }

    
    public static class StringExtensions
    {
        //文字列を一定文字数ごとに区切るメソッド
        public static string[] SubstringAtCount(this string self, int count)
        {
            var result = new List<string>();
            var length = (int)Math.Ceiling((double)self.Length / count);

            for (int i = 0; i < length; i++)
            {
                int start = count * i;
                if (self.Length <= start)
                {
                    break;
                }
                if (self.Length < start + count)
                {
                    result.Add(self.Substring(start));
                }
                else
                {
                    result.Add(self.Substring(start, count));
                }
            }

            return result.ToArray();
        }
    }
}
