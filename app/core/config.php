<?php

$domain_address = "";

if ($_SERVER["SERVER_NAME"] == "localhost") {

    define("ROOT", "http://localhost/WebDev/WeatherAppPhp/public");
} else {
    define("ROOT", $domain_address);
}

define("APPNAME", "My Website");
define("APP_DESC", "Website Description");

//When set to TRUE, errors will show on the webpage
define("DEBUG", true);