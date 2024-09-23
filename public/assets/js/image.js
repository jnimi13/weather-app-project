const ROOT = "http://localhost/WebDev/WeatherAppPhp/public/assets/"
const homeBackground = ["background-1.jpg", "background-2.jpg", "background-3.jpg", "background-4.jpg", "background-5.jpg"];

function homePageBackground() {
    document.querySelector("#home-page").style.backgroundImage = `url(${ROOT}images/home/${homeBackground[Math.floor(Math.random() * homeBackground.length)]})`;
    console.log(`url(${ROOT}images/home/${homeBackground[Math.floor(Math.random() * homeBackground.length)]})`);
}

homePageBackground();
setInterval(homePageBackground, 5000);