/*
* Сессия теперь создается в общей области видимости.
* Будет "захватываться" тремя функциями
* 
* */
let session = new Map();

/*
* Функция для фильтрации контента
* Будет вызываться благодаря атрибуту oninput на index.html
*
* */
function filterContent() {

    // Находим контейнеры с видео, которые необходимо фильтровать
    let elements = document.getElementsByClassName('video-container');

    // Пробегаемся по контейнерам
    for (let i = 0; i <= elements.length; i++) {
        // Вытаскиваем текст описания видео, которое необходимо отфильтровать
        let videoText = elements[i].querySelector(".video-title").innerText;
        // Выполняем фильтрацию, сравнивая значения в нижнем регистре
        if (!videoText.toLowerCase().includes(inputParseFunc().toLowerCase())) {
            // Скрываем неподходящие
            elements[i].style.display = 'none';
        } else {
            // Показываем подходящие
            elements[i].style.display = 'inline-block';
        }
    }
}

/*
* Функция для проверки и сохранения  данных пользователя
* Также блокирует доступ к сайту лицам, не подтвердившим свой возраст
*
* */

/*
* Вывод данных сессии в консоль
* 
* */
let sessionLog = function logSession() {
    // Вывод в консоль
    for (let result of session) {
        console.log(result)
    }
}

/*
* Сохранение данных сессии сразу при заходе пользователя на страницу
* 
* */
function handleSession() {

    // Сохраним время начала сессии
    session.set("startDate", new Date().toLocaleString())
    // Сохраним UserAgent
    session.set("userAgent", window.navigator.userAgent)
}

/*
* Проверка возраста пользователя
* 
* */
function checkAge() {
    session.set("age", prompt("Пожалуйста, введите ваш возраст?"))

    if (session.get("age") >= 18) {
        alert("Приветствуем на LifeSpot! " + '\n' + "Текущее время: " + new Date().toLocaleString());
    }
    else {
        alert("Наши трансляции не предназначены для лиц моложе 18 лет. Вы будете перенаправлены");
        window.location.href = "http://www.google.com"
    }
}