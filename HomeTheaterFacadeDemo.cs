using System;

namespace HomeTheaterFacadeDemo
{
    // Подсистема: ТВ
    public class TV
    {
        public void On() => Console.WriteLine("TV: Включен.");
        public void Off() => Console.WriteLine("TV: Выключен.");
        public void SetChannel(string channel) => Console.WriteLine($"TV: Установлен канал: {channel}");
    }

    // Подсистема: Аудиосистема
    public class AudioSystem
    {
        private int _volume = 10;
        public void On() => Console.WriteLine("AudioSystem: Включена.");
        public void Off() => Console.WriteLine("AudioSystem: Выключена.");
        public void SetVolume(int vol)
        {
            _volume = Math.Max(0, Math.Min(100, vol));
            Console.WriteLine($"AudioSystem: Установлена громкость: {_volume}");
        }
        public void Mute() => Console.WriteLine("AudioSystem: Отключено звуковое сопровождение (Mute).");
    }

    // Подсистема: DVD-проигрыватель
    public class DVDPlayer
    {
        public void On() => Console.WriteLine("DVDPlayer: Включен.");
        public void Off() => Console.WriteLine("DVDPlayer: Выключен.");
        public void Play(string movie) => Console.WriteLine($"DVDPlayer: Воспроизведение \"{movie}\".");
        public void Pause() => Console.WriteLine("DVDPlayer: Пауза.");
        public void Stop() => Console.WriteLine("DVDPlayer: Остановлен.");
    }

    // Подсистема: Игровая консоль
    public class GameConsole
    {
        public void On() => Console.WriteLine("GameConsole: Включена.");
        public void Off() => Console.WriteLine("GameConsole: Выключена.");
        public void StartGame(string game) => Console.WriteLine($"GameConsole: Запуск игры \"{game}\".");
    }

    // Фасад: HomeTheaterFacade
    public class HomeTheaterFacade
    {
        private readonly TV _tv;
        private readonly AudioSystem _audio;
        private readonly DVDPlayer _dvd;
        private readonly GameConsole _game;

        public HomeTheaterFacade(TV tv, AudioSystem audio, DVDPlayer dvd, GameConsole game)
        {
            _tv = tv;
            _audio = audio;
            _dvd = dvd;
            _game = game;
        }

        // Сценарий: начать просмотр фильма
        public void WatchMovie(string movie)
        {
            Console.WriteLine("\n[Фасад] Подготовка к просмотру фильма...");
            _tv.On();
            _tv.SetChannel("HD Cinema");
            _audio.On();
            _audio.SetVolume(30);
            _dvd.On();
            _dvd.Play(movie);
            Console.WriteLine("[Фасад] Система готова к просмотру фильма.\n");
        }

        // Сценарий: выключить систему
        public void EndMovie()
        {
            Console.WriteLine("\n[Фасад] Выключение кино-системы...");
            _dvd.Stop();
            _dvd.Off();
            _audio.Off();
            _tv.Off();
            Console.WriteLine("[Фасад] Система выключена.\n");
        }

        // Сценарий: запустить игру
        public void PlayGame(string game)
        {
            Console.WriteLine("\n[Фасад] Подготовка к игре...");
            _tv.On();
            _audio.On();
            _audio.SetVolume(40);
            _game.On();
            _game.StartGame(game);
            Console.WriteLine("[Фасад] Система готова для игры.\n");
        }

        // Сценарий: слушать музыку
        public void ListenToMusic()
        {
            Console.WriteLine("\n[Фасад] Подготовка к прослушиванию музыки...");
            _tv.On();
            _audio.On();
            _audio.SetVolume(25);
            Console.WriteLine("[Фасад] Вы можете воспроизвести музыку на TV/Audiosystem.\n");
        }

        // Общая возможность регулировки громкости через фасад
        public void SetVolume(int vol)
        {
            _audio.SetVolume(vol);
        }

        // Полное выключение всего
        public void ShutdownAll()
        {
            Console.WriteLine("\n[Фасад] Полное выключение всех подсистем...");
            _dvd.Off();
            _game.Off();
            _audio.Off();
            _tv.Off();
            Console.WriteLine("[Фасад] Все подсистемы выключены.\n");
        }
    }

    // Клиент
    class Program
    {
        static void Main()
        {
            var tv = new TV();
            var audio = new AudioSystem();
            var dvd = new DVDPlayer();
            var game = new GameConsole();

            var homeTheater = new HomeTheaterFacade(tv, audio, dvd, game);

            Console.WriteLine("=== Демонстрация фасада HomeTheater ===");

            homeTheater.WatchMovie("Inception");
            homeTheater.SetVolume(35);
            homeTheater.EndMovie();

            homeTheater.PlayGame("SuperMario");
            homeTheater.ShutdownAll();

            homeTheater.ListenToMusic();
        }
    }
}
