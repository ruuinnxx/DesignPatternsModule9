﻿var audio = new AudioSystem();
var video = new VideoProjector();
var light = new LightingSystem();

var homeTheaterFacade = new HomeTheaterFacade(audio,video, light);
homeTheaterFacade.StartMovie();
Console.WriteLine("\nПросмотр...\n");
await Task.Delay(3000);
homeTheaterFacade.EndMovie();

#region Классы под системы

public class AudioSystem
{
    public void TurnOn()
    {
        Console.WriteLine("Включение Аудио системы");
    }

    public void SetVolume(int level)
    {
        Console.WriteLine($"Установка громкости звука: {level}");
    }
    public void TurnOff()
    {
        Console.WriteLine("Выключение аудио системы");
    }
}

public class VideoProjector
{
    public void TurnOn()
    {
        Console.WriteLine("Включение видео проекта");
    }

    public void SetResolution(string resolution)
    {
        Console.WriteLine($"Установлено: {resolution} разрешение");
    }
    
    public void TurnOff()
    {
        Console.WriteLine("Выключение видео проекта");
        
    }
}

public class LightingSystem
{
    public void TurnOn()
    {
        Console.WriteLine("Включение света");
    }

    public void SetBrightness(int level)
    {
        Console.WriteLine($"Установка уровня света: {level}");
    }
    
    public void TurnOff()
    {
        Console.WriteLine("Выключение света");
        
    }
}

#endregion

#region Фасад 

public class HomeTheaterFacade(AudioSystem audio, VideoProjector video, LightingSystem light)
{
    private AudioSystem audio = audio;
    private VideoProjector video = video;
    private LightingSystem light = light;

    public void StartMovie()
    {
        Console.WriteLine("Подготовка к просмотру фильма: ");
        light.TurnOn();
        light.SetBrightness(5);
        audio.TurnOn();
        audio.SetVolume(8);
        video.TurnOn();
        video.SetResolution("HD");
        Console.WriteLine("Фильм готов к просмотру");
    }
    
    public void EndMovie()
    {
        Console.WriteLine("Фильм закончен");
        video.TurnOff();
        audio.TurnOff();
        light.TurnOff();
        Console.WriteLine("Выключение фильма");
    }

}

#endregion