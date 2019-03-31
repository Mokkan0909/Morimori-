Morimori動画の概要

形式：Windowsアプリケーション
使用言語：C#
OS:Windows10 pro
使用したエディタ：Microsoft Visual Studio 2017
上記エディタで編集し実行
作成環境
プロセッサ：intel(R) core(TM) i7-7500U CPU @ 2.70GHz 2.90GHz
実装メモリ：16.0GB
システム：64ビットオペレーションシステム x64 ベースプロセッサ
サーバー：福井大学の学内サーバーを用いた

アプリの仕様：
1.Webページのフォーム上から入力されたコメントをDBに格納する
2.DBに追加されたコメントを取得し画面上にスライドして流す

作成したソースコード
MainWindow.xaml.cs:画面の設定やシステムの実行を行う
Message.cs:入力されたコメントを運用・管理するクラス
LogForm.cs：コメントのログを表示するフォームを扱うクラス
taskicon.cs:タスクトレイに表示するタスクアイコンを扱うクラス
Form.php:コメントを入力するためのWebページを表示する

用いたデータベース
種類：Mysql
カラム
ID：今までに送信されたコメントの通し番号(Int)
content:送信されたコメント内容(char)
Color:スライド表示させる際の文字色(char)
Time:送信された時の日時と時間(datetime)
IP:送信元のPCのIPアドレス(char)

インポートしたライブラリ
Microdoft.CSharp
MySql.Data
PresentationCore
PresentationFramework
System
System.Data
System.Core
System.Data.DataSetExtentions
System.Drawing
System.Net.Http
System.Windows.Forms
System.Xaml
System.Xml
System.Xml.Linq
WindowsBase

XAMPP上で動かすために準備すること
0.zipファイルを解凍する
1.phpMyadminを用いて上記のデータベースを作成する
2.Form.phpのDB接続欄を書き換える(hostname,password,usernameなど)
3.MainWindow.xaml.csのDB接続欄を書き換える(hostname,password,usernameなど)
4.Form.phpを所定のフォルダに置く
5.プログラムを実行する