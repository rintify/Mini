<?php

function isValidName($name) {
    return !empty($name) && strlen($name) >= 1 && strlen($name) <= 1000;
}

function isValidScore($score) {
    return is_numeric($score) && $score >= 0 && $score <= 10000;
}

?>