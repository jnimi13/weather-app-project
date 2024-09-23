<?php
//This checks if class that is called and does not exist, and it will load it
spl_autoload_register(function($classname) {
        require $filename = "../app/models/" .ucfirst($classname). ".php";
});

require "config.php";
require "functions.php";
require "Database.php";
require "Model.php";
require "Controller.php";
require "App.php";