<?php

include("funcs.php"); 
$db = new SQLite3('../Mini.db');

// スコアの追加関数
function addScore($name, $score) {
    global $db;

    $stmt = $db->prepare("INSERT INTO Results (name, score, date) VALUES (:name, :score, :date)");
    $stmt->bindValue(':name', $name, SQLITE3_TEXT);
    $stmt->bindValue(':score', $score, SQLITE3_INTEGER);
    $stmt->bindValue(':date', time(), SQLITE3_INTEGER);
    $result = $stmt->execute();
    if(!$result) {
        die("Execution failed: " . $db->lastErrorMsg());
    }


    // スコアの上書きを試みる前に、名前が存在するか確認
    $stmt = $db->prepare("SELECT * FROM Players WHERE name = :name");
    $stmt->bindValue(':name', $name, SQLITE3_TEXT);
    $result = $stmt->execute();
    $row = $result->fetchArray(SQLITE3_ASSOC);

    if (!$row) {
        // 名前が存在しない場合、新しいプレーヤーを登録
        $stmt = $db->prepare("INSERT INTO Players (name, score) VALUES (:name, :score)");
        $stmt->bindValue(':name', $name, SQLITE3_TEXT);
        $stmt->bindValue(':score', $score, SQLITE3_INTEGER);
        $result = $stmt->execute();
        if(!$result) return "insert";
    } else if($score > $row['score']){
        // 名前が既に存在しスコアが上回る場合、スコアを上書き
        $stmt = $db->prepare("UPDATE Players SET score = :score WHERE name = :name");
        $stmt->bindValue(':name', $name, SQLITE3_TEXT);
        $stmt->bindValue(':score', $score, SQLITE3_INTEGER);
        $result = $stmt->execute();
        if(!$result) return "update";
    }

    return "ok";
}

if (isset($_POST['key']) && $_POST['key'] === 'sq9YZY0ZfQA7vI9zK3QIsHawIb') {
    
    $name = $_POST['name'];
    $score = intval($_POST['score']);

    if (isValidName($name) && isValidScore($score)) {
        $result = addScore($name, $score);
        if ($result == "ok") {
            echo 1;
        }else echo $result;
    }else echo "value";
} else echo "key";

?>