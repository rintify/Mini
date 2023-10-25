<?php

$db = new SQLite3('your_database_name.db');

// スコアの追加関数
function addScore($player_name, $score) {
    global $db;

    $stmt = $db->prepare("INSERT INTO rankings (player_name, score) VALUES (:player_name, :score)");
    $stmt->bindValue(':player_name', $player_name, SQLITE3_TEXT);
    $stmt->bindValue(':score', $score, SQLITE3_INTEGER);

    return $stmt->execute();
}

// POSTからデータを取得
if (isset($_POST['player_name']) && isset($_POST['score'])) {
    $player_name = $_POST['player_name'];
    $score = intval($_POST['score']); // 数値に変換

    // データの追加
    if (addScore($player_name, $score)) {
        echo "Score submitted successfully!";
    } else {
        echo "Failed to submit score.";
    }
} else {
    echo "Invalid data.";
}

?>