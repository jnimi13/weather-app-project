//HTML ELEMENTS
const searchBtn = document.querySelector("#search-btn");
const currentDate = document.querySelector("#current-date");
const currentTime = document.querySelector("#current-time");
const wind = document.querySelector("#current-wind");
const humidity = document.querySelector("#current-humidity");
const uvIndex = document.querySelector("#current-uv");
const sunrise = document.querySelector("#current-sunrise");
const sunset = document.querySelector("#current-sunset");
const currentAirQuality = document.querySelector("#current-air-quality");
const city = document.querySelector("#current-city");
const temperature = document.querySelector("#current-temperature");
const currentWeatherCondition = document.querySelector("#weather-condition-img");

const forecastOne = document.querySelector("#forecast-day-1");
const forecastTwo = document.querySelector("#forecast-day-2");
const forecastThree = document.querySelector("#forecast-day-3");
const forecastFour = document.querySelector("#forecast-day-4");
const forecastFive = document.querySelector("#forecast-day-5");

const forecastConditionOne = document.querySelector("#forecast-condition-1");
const forecastConditionTwo = document.querySelector("#forecast-condition-2");
const forecastConditionThree = document.querySelector("#forecast-condition-3");
const forecastConditionFour = document.querySelector("#forecast-condition-4");
const forecastConditionFive = document.querySelector("#forecast-condition-5");

const forecastImageOne = document.querySelector("#forecast-img-1");
const forecastImageTwo = document.querySelector("#forecast-img-2");
const forecastImageThree = document.querySelector("#forecast-img-3");
const forecastImageFour = document.querySelector("#forecast-img-4");
const forecastImageFive = document.querySelector("#forecast-img-5");

const forecastTemperatureOne = document.querySelector("#forecast-temperature-1");
const forecastTemperatureTwo = document.querySelector("#forecast-temperature-2");
const forecastTemperatureThree = document.querySelector("#forecast-temperature-3");
const forecastTemperatureFour = document.querySelector("#forecast-temperature-4");
const forecastTemperatureFive = document.querySelector("#forecast-temperature-5");

//DISPLAY TODAY'S DATE AND THE CURRENT TIME
const days = ["Sunday", "Monday","Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"];
const months = ["January","February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
const date = new Date();
let dayOfTheWeek = date.getDay();
let day = date.getDate();
let month = date.getMonth();
let year = date.getFullYear();

let hours = date.getHours();
let minutes = date.getMinutes();

setInterval(() => {
    currentDate.innerHTML = `${days[dayOfTheWeek]} ${day}, ${months[month]} ${year}`;
    currentTime.innerHTML = `${hours}:${minutes}`;
}, 1000);


//DISPLAY THE CORRECT IMAGE FOR THE WEATHER CONDITION 
const imageSource = ["weather_cloud_clouds_cloudy_icon.png", "weather_clouds_cloudy_forecast_rain_icon.png", "weather_clouds_cloudy_rain_sunny_icon.png",
    "weather_clouds_night_storm_icon.png", "weather_clouds_snow_winter_icon.png", "weather_clouds_sun_sunny_icon.png", "weather_hurricane_storm_tornado_icon.png",
    "weather_sun_sunny_temperature_icon.png", "weather_wind_windy_icon.png"];


function setWeatherConditionImage(condition) {
    let img = "images/weather-conditions/";
    if (condition >= 200 && condition <= 232) {
        img += `${imageSource[3]}`;
    } else if (condition >=300 && condition <= 321) {
        img += `${imageSource[1]}`;
    } else if (condition >= 500 && condition <=531) {
        img += `${imageSource[1]}`;
    } else if (condition >= 600 && condition <= 622) {
        img += `${imageSource[4]}`;
    } else if (condition >= 802 && condition <= 804) {
        img += `${imageSource[0]}`;
    } else if (condition == 801) {
        img += `${imageSource[5]}`;
    } else {
        img += `${imageSource[7]}`;
    }

    return img;
}

//API DATA
const API_KEY = "f9673a962feafddab377fa8d4a1db2df";
let input_city = "New York";
let input_country = "US";
let lattitude;
let longitude;

//CONVERT THE TIME TO A PROPER READABLE FORMAT
function msToTime(duration) {
    var milliseconds = parseInt((duration%1000)/100)
        , seconds = parseInt((duration/1000)%60)
        , minutes = parseInt((duration/(1000*60))%60)
        , hours = parseInt((duration/(1000*60*60))%24);

    hours = (hours < 10) ? "0" + hours : hours;
    minutes = (minutes < 10) ? "0" + minutes : minutes;
    seconds = (seconds < 10) ? "0" + seconds : seconds;

    return hours + ":" + minutes + ":" + seconds + "." + milliseconds;
}

//FIND THE COORDINATES OF A SPECIFIC CITY
async function findCoordinates() {
    const response = await fetch(`http://api.openweathermap.org/geo/1.0/direct?q=${input_city},${input_country}&limit=1&appid=${API_KEY}`);
    const data = await response.json();
    lattitude = data[0].lat;
    longitude = data[0].lon;
}

//FIND THE AIR QUALITY
async function getAirQuality() {
    const response = await fetch(`http://api.openweathermap.org/data/2.5/air_pollution?lat=${lattitude}&lon=${longitude}&appid=${API_KEY}`);
    const data = await response.json();
    return data;
}

//GET THE WEATHER DATA
async function getWeather() {
    await findCoordinates();
    const airQuality = await getAirQuality();
    const response = await fetch(`https://api.openweathermap.org/data/2.5/weather?lat=${lattitude}&lon=${longitude}&units=metric&appid=${API_KEY}`);
    const data = await response.json();
    
    city.innerHTML = `${data.name}, ${data.sys.country}`;
    temperature.innerHTML = `${Math.round(data.main.temp)}°C`;
    wind.innerHTML = `${(data.wind.speed * (18/5)).toFixed(2)}km/h`;
    humidity.innerHTML = `${data.main.humidity}`;
    sunrise.innerHTML = `${msToTime(data.sys.sunrise)}`;
    sunset.innerHTML = `${msToTime(data.sys.sunset)}`;
    currentWeatherCondition.src = setWeatherConditionImage(data.weather[0].id);
}

//GET THE FORECAST DATA
async function getFiveDayForecast() {
    await findCoordinates();
    const response = await fetch(`https://api.openweathermap.org/data/2.5/forecast?lat=${lattitude}&lon=${longitude}&units=metric&appid=${API_KEY}`);
    const data = await response.json();
    console.log(data);
    console.log(data.list[0].weather[0].main);
    
    /*forecastOne.innerHTML = 
    forecastTwo.innerHTML = 
    forecastThree.innerHTML = 
    forecastFour.innerHTML = 
    forecastFive.innerHTML = */

    forecastImageOne.src = setWeatherConditionImage(data.list[0].weather[0].id);
    forecastImageTwo.src = setWeatherConditionImage(data.list[8].weather[0].id);
    forecastImageThree.src = setWeatherConditionImage(data.list[16].weather[0].id);
    forecastImageFour.src = setWeatherConditionImage(data.list[24].weather[0].id);
    forecastImageFive.src = setWeatherConditionImage(data.list[32].weather[0].id);

    forecastConditionOne.innerHTML = data.list[0].weather[0].main;
    forecastConditionTwo.innerHTML = data.list[8].weather[0].main;
    forecastConditionThree.innerHTML = data.list[16].weather[0].main;
    forecastConditionFour.innerHTML = data.list[24].weather[0].main;
    forecastConditionFive.innerHTML = data.list[32].weather[0].main;

    forecastTemperatureOne.innerHTML = `${Math.round(data.list[0].main.temp)}°C`;
    forecastTemperatureTwo.innerHTML = `${Math.round(data.list[8].main.temp)}°C`;
    forecastTemperatureThree.innerHTML = `${Math.round(data.list[16].main.temp)}°C`;
    forecastTemperatureFour.innerHTML = `${Math.round(data.list[24].main.temp)}°C`;
    forecastTemperatureFive.innerHTML = `${Math.round(data.list[32].main.temp)}°C`;
}

getWeather();
getFiveDayForecast();

//FUNCTION TO HANDLE DATA FROM THE SEARCHBAR
async function sendData() {
    const form = document.querySelector("#search-form");
    const formData = new FormData(form);

    const response = await fetch("index.html/post", {
            method: "POST",
            body: formData
        });

    const data = await response.json();
    console.log(data);
}

searchBtn.addEventListener("click", sendData);