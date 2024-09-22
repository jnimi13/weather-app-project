const apiKey = '657b507e09f23c54c538ae1dda981260';
const form = document.getElementById('searchForm');

// Adding event listener to the form submission
form.addEventListener('submit', async (e) => {
    e.preventDefault();
    const city = form.search.value.trim();
    if (city) {
        await fetchWeatherData(city);
    } else {
        alert('Please enter a city name.');
    }
});

// Fetching weather data from the API
async function fetchWeatherData(city) {
    try {
        const response = await fetch(`https://api.openweathermap.org/data/2.5/weather?q=${city}&appid=${apiKey}&units=metric`);
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }
        const data = await response.json();
        updateWeatherDisplay(data);
    } catch (error) {
        console.error('Error fetching weather data:', error);
        alert('Error fetching weather data. Please try again.');
    }
}

// Updating the display with the fetched weather data
function updateWeatherDisplay(data) {
    const date = new Date(data.dt * 1000);
    document.getElementById('date-time').textContent = `${date.toLocaleDateString()} ${date.toLocaleTimeString()}`;
    document.getElementById('location').textContent = `${data.name}, ${data.sys.country}`;
    document.getElementById('temperature').textContent = `${Math.round(data.main.temp)}°C`;
    document.getElementById('wind').textContent = `${data.wind.speed} km/h`;
    document.getElementById('humidity').textContent = `${data.main.humidity}%`;
    document.getElementById('uvIndex').textContent = 'N/A'; // UV Index is not provided in the current weather API

    const weatherIcon = `http://openweathermap.org/img/wn/${data.weather[0].icon}.png`;
    document.getElementById('weatherIcon').src = weatherIcon;

    // Optional: You can implement a 5-day forecast here if desired
}
