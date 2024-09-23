//HTML elements where all the weather live data is required
const currentDate = document.querySelector("#current-date");
const currentTime = document.querySelector("#current-time");
const wind = document.querySelector("#current-wind");
const humidity = document.querySelector("#current-humidity");
const uvIndex = document.querySelector("#current-uv");
const sunrise = document.querySelector("#current-sunrise");
const sunset = document.querySelector("#current-sunset");
const currentAirQuality = document.querySelector("#current-air-quality");
const currentAirStandard = document.querySelector("#current-air-standard");
const city = document.querySelector("#current-city");
const temperature = document.querySelector("#current-temperature");
const lowTemperature = document.querySelector("#low-temperature");
const highTemperature = document.querySelector("#high-temperature");
const feelTemperature = document.querySelector("#feel-temperature");
const currentWeatherCondition = document.querySelector("#weather-condition-img");
const currentPressure = document.querySelector("#current-pressure");
const currentVisibility = document.querySelector("#current-visibility");
const currentRain = document.querySelector("#current-rain");
const currentClouds = document.querySelector("#current-clouds");

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
//Main app container where the background will change depending on the weather
const container = document.querySelector("#background");

//This is all the components required to run our search section
const form = document.querySelector("#search-form");
const searchBtn = document.querySelector("#search-btn");
const searchInput = document.querySelector("#search-input");
const closeBtn = document.querySelector("#close-btn");

const searchModal = document.querySelector("#search-modal");
const searchList = document.querySelector("#search-list");
const locationResults = document.querySelectorAll(".location");
let searchResults = [];

const ROOT = "http://localhost/WebDev/WeatherAppPhp/public/assets/"


//Store the days and month

function getCurrentDate() {
    const days = ["Sunday", "Monday","Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"];
    const months = ["January","February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
    const date = new Date();
    const dayOfTheWeek = date.getDay();
    const day = date.getDate();
    const month = date.getMonth();
    const year = date.getFullYear();

    return `${days[dayOfTheWeek]} ${day}, ${months[month]} ${year}`;
}

function getCurrentDateNumberFormat() {
    const date = new Date();
    const day = date.getDate();
    const month = date.getMonth();
    const year = date.getFullYear();

    return `${year}-${(month + 1 < 10) ? "0" + (month + 1) : (month + 1)}-${(day < 10) ? "0" + day : day}`;
}

function getCurrentTime() {
    const date = new Date();
    let hours = date.getHours();
    let minutes = date.getMinutes();
    let seconds = date.getSeconds();
    return `${(hours < 10) ? "0" + hours : hours}:${(minutes < 10) ? "0" + minutes : minutes}:${seconds = (seconds < 10) ? "0" + seconds : seconds}`;
}

//Runs the clock
setInterval(() => {
    currentDate.innerHTML = getCurrentDate();
    currentTime.innerHTML = getCurrentTime();
}, 1000);


//Stores all the image reference links for our weather conditions
const imageSource = ["weather_cloud_clouds_cloudy_icon.png", "weather_clouds_cloudy_forecast_rain_icon.png", "weather_clouds_cloudy_rain_sunny_icon.png",
    "weather_clouds_night_storm_icon.png", "weather_clouds_snow_winter_icon.png", "weather_clouds_sun_sunny_icon.png", "weather_hurricane_storm_tornado_icon.png",
    "weather_sun_sunny_temperature_icon.png", "weather_wind_windy_icon.png"];

//Sets the right image for the weather condition
function setWeatherConditionImage(condition) {
    let img = `${ROOT}images/weather-conditions/`;
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

//Sets the background image depending on the current weather
const background = ["cloudy_day.jpg", "sunny_day_desert.jpg", "sunny_day.jpg", "part_cloudy_day.jpg", "light_snow_day.jpg", "rainy_day.jpg",
    "heavy_rain.jpg", "thunderstorm.jpg"];

function setBackgroundImage(condition) {
    let img = `${ROOT}images/background/`;
    if (condition >= 200 && condition <= 232) {
        img += `${background[7]}`;
    } else if (condition >=300 && condition <= 321) {
        img += `${background[6]}`;
    } else if (condition >= 500 && condition <=531) {
        img += `${background[5]}`;
    } else if (condition >= 600 && condition <= 622) {
        img += `${background[4]}`;
    } else if (condition >= 802 && condition <= 804) {
        img += `${background[0]}`;
    } else if (condition == 801) {
        img += `${background[3]}`;
    } else {
        img += `${background[1]}`;
    }

    return img;
}

//API DATA
const API_KEY = "f9673a962feafddab377fa8d4a1db2df";
let input_city = "Toronto";
let input_state = "Ontario";
let input_country = "CA";
let lattitude;
let longitude;

//Convert the time from milliseconds to a readable format
function msToTime(duration) {
    var milliseconds = parseInt((duration%1000)/100)
        , seconds = parseInt((duration/1000)%60)
        , minutes = parseInt((duration/(1000*60))%60)
        , hours = parseInt(((duration - 18000000)/(1000*60*60))%24);

    hours = (hours < 10) ? "0" + hours : hours;
    minutes = (minutes < 10) ? "0" + minutes : minutes;
    seconds = (seconds < 10) ? "0" + seconds : seconds;

    return hours + ":" + minutes;
}

//Find the coordinates of a specific city
async function findCoordinates() {
    const url = `http://api.openweathermap.org/geo/1.0/direct?q=${input_city},${input_state},${input_country}&limit=1&appid=${API_KEY}`;

    const response = await fetch(url);
    const data = await response.json();
    //console.log(data);
    lattitude = data[0].lat;
    longitude = data[0].lon;
}

//Create a new location - This is used in conjunction with findLocation() to store our search results
function Location(city, state, country) {
    this.city = city;
    this.state = state;
    this.country = country;
}

//Find matching locations based on a search
async function findLocation(input) {
    const url = `http://api.openweathermap.org/geo/1.0/direct?q=${input}&limit=10&appid=${API_KEY}`;
    const response = await fetch(url);
    const data = await response.json();
    let i = 0;
    data.forEach((elem) => {
        if (elem.state !== undefined) {
            console.log(elem.name);
            text = `${elem.name}, ${elem.state}, ${elem.country}`;
            addListElem(text, i);
            i++;
            searchResults.push(new Location(elem.name, elem.state, elem.country));
        }
    });
}

//Find the air quality
const air = ["Good", "Fair", "Moderate", "Poor", "Very Poor"];

async function getAirQuality() {
    const url = `http://api.openweathermap.org/data/2.5/air_pollution?lat=${lattitude}&lon=${longitude}&appid=${API_KEY}`
    const response = await fetch(url);
    const data = await response.json();
    return data;
}

//Get the weather data for our dashboard
async function getWeather() {
    await findCoordinates();
    const airQuality = await getAirQuality();

    const url = `https://api.openweathermap.org/data/2.5/weather?lat=${lattitude}&lon=${longitude}&units=metric&appid=${API_KEY}`;
    const response = await fetch(url);
    const data = await response.json();
    console.log(data);
    
    city.innerHTML = `${input_city}, ${input_state}, ${input_country}`;
    temperature.innerHTML = `${Math.round(data.main.temp)}°C`;
    lowTemperature.innerHTML = `${Math.round(data.main.temp_min)}°C`;
    highTemperature.innerHTML = `${Math.round(data.main.temp_max)}°C`;
    feelTemperature.innerHTML = `${Math.round(data.main.feels_like)}°C`;
    wind.innerHTML = `${(data.wind.speed * (18/5)).toFixed(2)}km/h`;
    humidity.innerHTML = `${data.main.humidity}`;
    sunrise.innerHTML = `${msToTime((data.sys.sunrise) - 43200000)}`;
    sunset.innerHTML = `${msToTime((data.sys.sunset))}`;
    currentAirQuality.innerHTML = `${airQuality.list[0].main.aqi}`;
    currentAirStandard.innerHTML = `${air[(airQuality.list[0].main.aqi) - 1]}`;
    currentPressure.innerHTML = `${data.main.pressure} hPa`;
    currentVisibility.innerHTML = `${data.visibility} m`;
    currentRain.innerHTML = `${(data.rain !== undefined) ? data.rain["1h"] : "0"} mm/h`;
    currentClouds.innerHTML = `${data.clouds.all}`;
    currentWeatherCondition.src = setWeatherConditionImage(data.weather[0].id);
    
    container.style.backgroundImage = `url(${setBackgroundImage(data.weather[0].id)})`;

    getFiveDayForecast();
}

//Get the forecast data for the next 5 days
async function getFiveDayForecast() {
    await findCoordinates();

    const url = `https://api.openweathermap.org/data/2.5/forecast?lat=${lattitude}&lon=${longitude}&units=metric&appid=${API_KEY}`;
    const response = await fetch(url);
    const data = await response.json();
    let forecast = [];

    for (i = 0; i < data.list.length; i++) {
        if (data.list[i].dt_txt.substr(-8) === "00:00:00" && data.list[i].dt_txt.substr(0,10) != getCurrentDateNumberFormat()) { 
            //Add forecast for the next 5 days at 12PM to the array
            forecast.push(data.list[i]);
        }
    }
    
    forecastOne.innerHTML = (forecast[0].dt_txt).substr(0, 10);
    forecastTwo.innerHTML = (forecast[1].dt_txt).substr(0, 10);
    forecastThree.innerHTML = (forecast[2].dt_txt).substr(0, 10);
    forecastFour.innerHTML = (forecast[3].dt_txt).substr(0, 10);
    forecastFive.innerHTML = (forecast[4].dt_txt).substr(0, 10);

    forecastImageOne.src = setWeatherConditionImage(forecast[0].weather[0].id);
    forecastImageTwo.src = setWeatherConditionImage(forecast[1].weather[0].id);
    forecastImageThree.src = setWeatherConditionImage(forecast[2].weather[0].id);
    forecastImageFour.src = setWeatherConditionImage(forecast[3].weather[0].id);
    forecastImageFive.src = setWeatherConditionImage(forecast[4].weather[0].id);

    forecastConditionOne.innerHTML = forecast[0].weather[0].main;
    forecastConditionTwo.innerHTML = forecast[1].weather[0].main;
    forecastConditionThree.innerHTML = forecast[2].weather[0].main;
    forecastConditionFour.innerHTML = forecast[3].weather[0].main;
    forecastConditionFive.innerHTML = forecast[4].weather[0].main;

    forecastTemperatureOne.innerHTML = `${Math.round(forecast[0].main.temp)}°C`;
    forecastTemperatureTwo.innerHTML = `${Math.round(forecast[1].main.temp)}°C`;
    forecastTemperatureThree.innerHTML = `${Math.round(forecast[2].main.temp)}°C`;
    forecastTemperatureFour.innerHTML = `${Math.round(forecast[3].main.temp)}°C`;
    forecastTemperatureFive.innerHTML = `${Math.round(forecast[4].main.temp)}°C`;
}

//Add new items to our search list on the webpage
function addListElem(data, id) {
    const newLi = document.createElement("li");
    newLi.setAttribute("id", id);
    newLi.setAttribute("class", "location");
    newLi.innerHTML = data;
    searchList.appendChild(newLi);
}

//Launch our app
getWeather();

//Prevents us form submitting the form by pressing enter
form.addEventListener("submit", (e) => {
    e.preventDefault();
});

//Toggles our search menu
searchBtn.addEventListener("click", () => {
    searchModal.showModal();
});

//Closes the search menu
closeBtn.addEventListener("click", () => {
    searchModal.close();
});

//Searches for matches when typing
searchInput.addEventListener("keyup", ()=> {
    input_city = searchInput.value;
    findLocation(input_city);
});

//Deletes past matches as we narrow our search
searchInput.addEventListener("keydown", ()=> {
    searchResults = [];
    searchList.innerHTML = "";
});

//Selects the location we want to find the weather data for
searchList.addEventListener("click", (e) => {
    //console.log(e.target.getAttribute("id"));
    let index = e.target.getAttribute("id");
    //console.log(searchResults[index].city);
    input_city = searchResults[index].city;
    input_state = searchResults[index].state;
    input_country = searchResults[index].country;

    getWeather();
    searchModal.close();
});