<?php
    include("partials/header.view.php");
?>

<div class="main-container">
            <div class="row">
                <div>
                    <h1><?php echo $_SESSION["CURRENT_FIRST_NAME"] . " " . $_SESSION["CURRENT_LAST_NAME"] ?></h1>
                </div>
                <div class="flex-end">
                    <img src="<?=ROOT?>/assets/images/search-2-line.png" id="search-btn"/>
                </div>
            </div>
            <div class="row-3">
                <div class="col">
                    <div class="date-time">
                        <p id="current-date"></p>
                        <p id="current-time"></p>
                    </div>
                    <div class="weather-summary">
                        <div class="weather-summary-details">
                            <div>
                                <h3 id="current-city"></h3>
                                <h3 id="current-temperature" class="temperature-text">24°C</h3>
                            </div>
                            <div class="weather-daily-info">
                                <div class="weather-bubble">
                                    <h3>Wind</h3>
                                    <p id="current-wind"></p>
                                </div>
                                <div class="weather-bubble">
                                    <h3>Humidity</h3>
                                    <p id="current-humidity"></p>
                                </div> 
                            </div>
                        </div>
                        <div>
                            <img class="weather-summary-icon" id="weather-condition-img" src="<?=ROOT?>/assets/images/weather-conditions/weather_clouds_sun_sunny_icon.png" />
                        </div>
                    </div>
                </div>
                <div class="col">
                    <div class="weather-data">
                        <ul>
                            <li class="flex-between"><h3>Feels Like</h3><h4 id="feel-temperature">Temp</h4></li>
                            <li class="flex-between"><h3>Low</h3><h4 id="low-temperature">Temp</h4></li>
                            <li class="flex-between"><h3>High</h3><h4 id="high-temperature">Temp</h4></li>
                        </ul>
                    </div>
                </div>
                <div class="col">
                    <div class="alerts">
                        <h3>Alerts & Notifications</h3>
                        <p>No Current Alerts</p>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="forecast-container">
                    <div class="forecast-title">
                        <h3>5 Day Forecast</h3>
                    </div>
                    <div class="forecast">
                        <div class="forecast-card">
                            <h4 id="forecast-day-1">Saturday</h4>
                            <img id="forecast-img-1" class="forecast-icon" src="<?=ROOT?>/assets/images/weather-conditions/weather_cloud_clouds_cloudy_icon.png"/>
                            <h5 id="forecast-condition-1">Cloudy</h5>
                            <h4 id="forecast-temperature-1">20°C</h4>
                        </div>
                        <div class="forecast-card">
                            <h4 id="forecast-day-2">Sunday</h4>
                            <img id="forecast-img-2" class="forecast-icon" src="<?=ROOT?>/assets/images/weather-conditions/weather_sun_sunny_temperature_icon.png" />
                            <h5 id="forecast-condition-2">Sunny</h5>
                            <h4 id="forecast-temperature-2">19°C</h4>
                        </div>
                        <div class="forecast-card">
                            <h4 id="forecast-day-3">Monday</h4>
                            <img id="forecast-img-3" class="forecast-icon" src="<?=ROOT?>/assets/images/weather-conditions/weather_hurricane_storm_tornado_icon.png"/>
                            <h5 id="forecast-condition-3">Windy</h5>
                            <h4 id="forecast-temperature-3">17°C</h4>
                        </div>
                        <div class="forecast-card">
                            <h4 id="forecast-day-4">Tuesday</h4>
                            <img id="forecast-img-4" class="forecast-icon" src="<?=ROOT?>/assets/images/weather-conditions/weather_clouds_sun_sunny_icon.png" />
                            <h5 id="forecast-condition-4">Partially Cloudy</h5>
                            <h4 id="forecast-temperature-4">23°C</h4>
                        </div>
                        <div class="forecast-card">
                            <h4 id="forecast-day-5">Wednesday</h4>
                            <img id="forecast-img-5" class="forecast-icon" src="<?=ROOT?>/assets/images/weather-conditions/weather_clouds_night_storm_icon.png" />
                            <h5 id="forecast-condition-5">Thunder</h5>
                            <h4 id="forecast-temperature-5">21°C</h4>
                        </div>
                    </div>
                </div>
                <div class="additional-weather-info">
                    <div class="sun-info">
                        <h3>Sunrise & Sunset</h3>
                        <div class="sun-info-desc">
                            <div>
                                <img class="sun-info-icon" src="<?=ROOT?>/assets/images/weather-conditions/weather_sunrise_morning_icon.png" />
                                <p id="current-sunrise">06:32AM</p>
                            </div>
                            <div>
                                <img class="sun-info-icon" src="<?=ROOT?>/assets/images/weather-conditions/weather_sunset_evening_icon.png" />
                                <p id="current-sunset">08:13PM</p>
                            </div>
                        </div>
                    </div>
                    <div class="atmos-info">
                        <div>
                            <h3>Air Quality</h3>
                            <div>
                                <h4 id="current-air-quality"></h4>
                                <h4> - </h4>
                                <h5 id="current-air-standard"></h5>
                            </div>
                        </div>
                        <div>
                            <h3>Pressure</h3>
                            <h4 id="current-pressure"></h4>
                        </div>
                        <div>
                            <h3>Visibility</h3>
                            <h4 id="current-visibility"></h4>
                        </div>
                        <div>
                            <h3>Clouds</h3>
                            <h4 id="current-clouds"></h4>
                        </div>
                        <div>
                            <h3>Rain</h3>
                            <h4 id="current-rain"></h4>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <dialog id="search-modal">
        <div class="flex-column">
            <form class="searchbar" id="search-form">
                <img src="<?=ROOT?>/assets/images/search-line.png"/>
                <input type="text" name="search" id="search-input"/>
            </form>
            <ul id="search-list"></ul>
            <button id="close-btn" class="btn primary-btn">Return to dashboard</button>
        </div>
    </dialog>

    <script type="text/javascript" src="<?=ROOT?>/assets/js/script.js"></script>
</body>
</html>