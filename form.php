<!DOCTYPE HTML>
<html lang="ja">
<head>
<meta charset="utf-8">
<title>MoriMori動画</title>
</head>
<body>
  <h1>コメントの入力</h1>
<form action="" method = "get">
<input type = "text" name ="message" tabindex="0" autofocus>
<input type = "submit" value ="送信" /></br/>

色を選択
<input type="radio" name="color" value="red">赤
<input type="radio" name="color" value="blue">青
<input type="radio" name="color" value="yallow">黄色

</form>

<?php
if(isset($_GET['message'])){
$message = $_GET['message'];
if(isset($_GET['color'])){
  $color = $_GET['color'];
}
else{
  $color = "white";
}


switch ($color) {
	case "red":
		$color="red";
		break;
	case "blue":
		$date="blue";
		break;
	case "yellow":
		$date="yellow";
		break;
}

date_default_timezone_set('Asia/Tokyo');
$now = new DateTime();
$str1 = $now->format('Y-m-d H:i:s');
$str = getUnixTimeMillSecond();

$IP = ip2long($_SERVER['REMOTE_ADDR']);

//$db = new mysqli('127.0.0.1', 'root', 'niconico', 'example');
//$db = new mysqli('127.0.0.1', 'root', 'niconico', 'message');
$db = new mysqli("host_name", "username", "password", "connect_to_DB");
if (!$db) {
    print("DB接続失敗<br>");
}

//print("DB接続成功<br>");



// データ取得
$cmd = 'SELECT Content FROM Comment';
$result2 = mysqli_query($db, $cmd);
if (!$result2) {
    print("データ取得に失敗<br>");
}

/*
// 取得した値を表示
while ($row = mysqli_fetch_assoc($result2)) {
    print($row['Content'].'<br>');
}
*/
// データ追加
$row_cnt = mysqli_num_rows($result2);
//$cmd = "INSERT INTO comment(Content, Size, PositionY, Color) VALUES('$message', 20, 20, 'White')";
//$cmd = "INSERT INTO comment(ID, Content) VALUES('$row_cnt', '$message')";
$cmd = "INSERT INTO Comment(ID, Content, Color, Time, IP) VALUES('$row_cnt', '$message', '$color', '$str', '$IP')";
$result1 = mysqli_query($db, $cmd);
if (!$result1) {
    print("データ追加に失敗<br>");
}


// DB切断
$closed_flag = mysqli_close($db);

if (!$closed_flag){
    print("DB切断失敗<br>");
}

//print("DB切断成功<br>");
header('Location: https://hsgw-nas.fuis.u-fukui.ac.jp/php/form.php');
}

function getUnixTimeMillSecond(){
    //microtimeを.で分割
    $arrTime = explode('.',microtime(true));
    //日時＋ミリ秒
    return date('Y-m-d H:i:s', $arrTime[0]) . '.' .$arrTime[1];
}




?>
<p><?php

?></p>
</body>
</html>
