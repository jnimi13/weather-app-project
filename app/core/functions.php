<?php

function show($e) {
    echo "<pre>";
    print_r($e);
    echo "<br/>";
    echo "</pre>";
}

function esc($str) {
    return htmlspecialchars($str);
}

function redirect($path) {
    header("Location: " . ROOT . "/" . $path);
    die;
}

function obj_to_array($obj) {
    $new_array = json_decode(json_encode($obj), true);
    return $new_array;
}

function array_to_html($arr) {
    $stmt = '<p class="text-center error">';

    foreach($arr as $key => $value) {
        $stmt .= $value . " | ";
    }

    $stmt = substr($stmt, 0 , -3);
    $stmt .= "</p>";
    return $stmt;

}