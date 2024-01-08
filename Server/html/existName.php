<?php

include("funcs.php"); 
$db = new SQLite3('../Mini.db');

header('Content-Type: application/json');
header("Access-Control-Allow-Origin: *");
header("Access-Control-Allow-Methods: GET, POST, OPTIONS");
header("Access-Control-Allow-Headers: Accept, X-Access-Token, X-Application-Name, X-Request-Sent-Time");

$name = $_POST['name'];
if (isValidName($name)) {
    $stmt = $db->prepare("SELECT * FROM Players WHERE name = :name");
    $stmt->bindValue(':name', $name, SQLITE3_TEXT);
    $result = $stmt->execute();
    $row = $result->fetchArray(SQLITE3_ASSOC);
    echo !$row ? 0 : 1;
}

?>