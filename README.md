# Проекты: Facade (HomeTheater) и Composite (FileSystem)

## Курс
Шаблоны проектирования приложений — Модуль 09: Структурные паттерны (Фасад, Компоновщик)

---

## 1. HomeTheaterFacade (Фасад)

### Описание
Реализована система управления мультимедиа-центром, включающая подсистемы:
- TV (включение/выбор канала),
- AudioSystem (включение/громкость),
- DVDPlayer (play/pause/stop),
- GameConsole (включение/запуск игры).

Класс `HomeTheaterFacade` предоставляет простые сценарии:
- `WatchMovie(movie)`,
- `EndMovie()`,
- `PlayGame(game)`,
- `ListenToMusic()`,
- `SetVolume(vol)`,
- `ShutdownAll()`.


## 2. FileSystemComposite (Компоновщик)

### Описание
Реализована иерархическая файловая система:
- Абстрактный `FileSystemComponent` (Display, GetSize).
- `FileComponent` — конкретный файл (имя, размер).
- `DirectoryComponent` — папка, содержащая дочерние компоненты; поддерживает Add и Remove с проверкой.

### Как запустить
1. Создать новый консольный проект на C#.
2. Вставить код `FileSystemCompositeDemo.cs`.
3. Запустить проект.

