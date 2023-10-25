<?php

include("funcs.php"); 
$db = new SQLite3('Mini.db');

$name = $_POST['name'];
if (isValidName($name)) {
    $stmt = $db->prepare("SELECT * FROM Players WHERE name = :name");
    $stmt->bindValue(':name', $name, SQLITE3_TEXT);
    $result = $stmt->execute();
    $row = $result->fetchArray(SQLITE3_ASSOC);
    echo !$row ? 0 : 1;
}

?>