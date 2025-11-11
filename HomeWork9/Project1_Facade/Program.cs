﻿var homeTheaterFacade = 
    new HomeTheaterFacade(new AudioSystem(), new Tv(), new DvdPlayer(), new GameConsole());
homeTheaterFacade.SartDvdPlayer();
homeTheaterFacade.StartTv();
homeTheaterFacade.EndDvd();
homeTheaterFacade.EndTv();
homeTheaterFacade.PlayGameStart();
homeTheaterFacade.PlayGameStop();


#region Классы под системы

public class AudioSystem
{
    public void TurnOn() =>
        Console.WriteLine("Включение Аудио системы");
    
    public void SetVolume(int level) =>
        Console.WriteLine($"Установка громкости звука: {level}");
    
    public void TurnOff() =>
        Console.WriteLine("Выключение аудио системы");
}

public class Tv
{
    public void TurnOn() =>
        Console.WriteLine("Включение Tv");

    public void SetChannel(int channel) =>
        Console.WriteLine($"Переключение канала: {channel}");
    
    public void TurnOff() =>
        Console.WriteLine("Выключение Tv");
}

public class DvdPlayer
{
    public void PlaybackDvd() =>
        Console.WriteLine("Воспроизведение DVD-диска");

    public void PauseDvd() =>
        Console.WriteLine("Пауза видео плеера");
    
    public void StopDvd() =>
        Console.WriteLine("Остановка DVD-диска");
}

public class GameConsole
{
    public void TurnOnGame() =>
        Console.WriteLine("Включение консоли, запуск игры");

    public void StartGame() =>
        Console.WriteLine("Запуск игры");
}

#endregion

#region Фасад

public class HomeTheaterFacade(AudioSystem audio, Tv tv, DvdPlayer dvdPlayer, GameConsole gameConsole)
{
    public void StartTv()
    {
        Console.WriteLine("Подготовка к просмотру Tv: ");
        tv.TurnOn();
        tv.SetChannel(5);
        audio.TurnOn();
        audio.SetVolume(8);
        Console.WriteLine("Tv готов к просмотру");
    }

    public void SartDvdPlayer()
    {
        Console.WriteLine("Подготовка к просмотру DvdPlayer:");
        dvdPlayer.PlaybackDvd();
        audio.TurnOn();
        audio.SetVolume(7);
        Console.WriteLine("Dvd готов к просмотру");
    }

    public void EndTv()
    {
        Console.WriteLine("Фильм закончен");
        tv.TurnOff();
        audio.TurnOff();
        Console.WriteLine("Выключение Tv");
    }

    public void EndDvd()
    {
        Console.WriteLine("Фильм закончен");
        dvdPlayer.StopDvd();
        audio.TurnOff();
        Console.WriteLine("Выключение Dvd");
    }

    public void PlayGameStart() =>
        gameConsole.StartGame();

    public void PlayGameStop() =>
        gameConsole.TurnOnGame();
}

#endregion