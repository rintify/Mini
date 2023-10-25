<?php

$db = new SQLite3('Mini.db');

function getTopScores($limit) {
    global $db;

    $stmt = $db->prepare("SELECT * FROM Players ORDER BY score DESC LIMIT :limit");
    $stmt->bindValue(':limit', $limit, SQLITE3_INTEGER);
    $result = $stmt->execute();

    $scores = [];
    while ($row = $result->fetchArray(SQLITE3_ASSOC)) {
        $scores[] = $row;
    }

    return $scores;
}

$scores = getTopScores(100);
echo json_encode($scores);

?>