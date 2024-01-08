<?php

include("funcs.php"); 
$db = new SQLite3('../Mini.db');

function getMyScores($name,$limit) {
    global $db;

    $stmt = $db->prepare("SELECT * FROM Results WHERE name = :name ORDER BY score DESC LIMIT :limit");
    $stmt->bindValue(':name', $name, SQLITE3_TEXT);
    $stmt->bindValue(':limit', $limit, SQLITE3_INTEGER);
    $result = $stmt->execute();

    $scores = [];
    while ($row = $result->fetchArray(SQLITE3_ASSOC)) {
        $scores[] = $row;
    }

    return $scores;
}

header('Content-Type: application/json');
header("Access-Control-Allow-Origin: *");
header("Access-Control-Allow-Methods: GET, POST, OPTIONS");
header("Access-Control-Allow-Headers: Accept, X-Access-Token, X-Application-Name, X-Request-Sent-Time");

$name = $_POST['name'];
if (isValidName($name)) {
    $scores = getMyScores($name,100);
    echo json_encode($scores);
}

?>