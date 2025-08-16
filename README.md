# 🎧 Music — небольшой сайт для прослушивания музыки
(Не закончено.Для примера понимания архитерктуры и общих принципов)
> Небольшое веб-приложение для прослушивания треков. Проект построен по принципам **Clean Architecture**, написан на **ASP.NET MVC**, а для хранения медиа использует внешнее облачное хранилище **Cloudinary**.

---

## ✨ Возможности
- Загрузка и хранение аудиотреков во внешнем облаке (**Cloudinary**)
- Прослушивание треков прямо на сайте (HTML5 audio)
- Каталогизация: исполнители, альбомы, теги
- Поиск и фильтрация
- Минималистичный, адаптивный UI (Bootstrap)

---

## 🧱 Технологии и стек

<p>
  <img src="https://raw.githubusercontent.com/github/explore/80688e429a7d4ef2fca1e82350fe8e3517d3494d/topics/dotnet/dotnet.png" alt=".NET" width="40" height="40" />
  <img src="https://raw.githubusercontent.com/github/explore/80688e429a7d4ef2fca1e82350fe8e3517d3494d/topics/csharp/csharp.png" alt="C#" width="40" height="40" />
  <img src="https://raw.githubusercontent.com/github/explore/80688e429a7d4ef2fca1e82350fe8e3517d3494d/topics/html/html.png" alt="HTML5" width="40" height="40" />
  <img src="https://raw.githubusercontent.com/github/explore/80688e429a7d4ef2fca1e82350fe8e3517d3494d/topics/css/css.png" alt="CSS3" width="40" height="40" />
  <img src="https://raw.githubusercontent.com/github/explore/80688e429a7d4ef2fca1e82350fe8e3517d3494d/topics/bootstrap/bootstrap.png" alt="Bootstrap" width="40" height="40" />
</p>

- **Backend:** ASP.NET MVC (.NET), C#
- **Архитектура:** Clean Architecture (Domain / Application / Infrastructure / Web)
- **Хранилище медиа:** Cloudinary (Audio/Images)
- **База данных:** любая SQL (например, PostgreSQL/SQL Server) через EF Core
- **UI:** Bootstrap 5, частичные представления, TagHelpers
- **Логирование:** Microsoft.Extensions.Logging

---

## 🧭 Архитектурный срез (Clean Architecture)
