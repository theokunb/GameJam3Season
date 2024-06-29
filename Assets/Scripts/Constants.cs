using System.IO;
using UnityEngine;

public static class Constants
{
    public static class SceneIndex
    {
        public static int Menu = 0;
        public static int Game = 1;
    }

    public static class Prefabs
    {
        public static string Arrow = Path.Combine("Prefabs", "Arrow");
    }

    public static class AnimationParams
    {
        public static string Open = nameof(Open);
        public static string Speed = nameof(Speed);
        public static string Talk = nameof(Talk);
        public static string Wrong = nameof(Wrong);
    }

    public static class Materials
    {
        public static string DoorStatusGreen = Path.Combine("Materials", "DoorStatusGreen");
        public static string DoorStatusYellow = Path.Combine("Materials", "DoorStatusYellow");
        public static string DoorStatusRed = Path.Combine("Materials", "DoorStatusRed");
    }

    public static class TerminalButton
    {
        public static Color DefaultColor = new Color(0.3470986f, 0.4985095f, 0.9811321f, 1.0f);
        public static Color AcceptedColor = new Color(0.427451f, 0.9529412f, 0.6470588f, 1.0f);
    }

    public static class Interactions
    {
        public static string DoorLevel1 = Path.Combine("Prefabs", "Interactions", "Door", "DoorLevel1");
        public static string DoorLevel2 = Path.Combine("Prefabs", "Interactions", "Door", "DoorLevel2");
        public static string DoorLevel3 = Path.Combine("Prefabs", "Interactions", "Door", "DoorLevel3");
        public static string DoorLevel4 = Path.Combine("Prefabs", "Interactions", "Door", "DoorLevel4");

        public static string Tube = Path.Combine("Prefabs", "Interactions", "Tube", "Tube");

        public static string Drill = Path.Combine("Prefabs", "Interactions", "Drill", "Drill");

        public static string Asteroid = Path.Combine("Prefabs", "Interactions", "Asteroids", "Asteroid");
        public static string Asteroids = Path.Combine("Prefabs", "Interactions", "Asteroids", "Asteroids");
        public static string AsteroidsView = Path.Combine("Prefabs", "Interactions", "Asteroids", "AsteroidsView");

        public static string Terminal = Path.Combine("Prefabs", "Interactions", "Terminal", "Terminal");
        public static string TerminalView = Path.Combine("Prefabs", "Interactions", "Terminal", "TerminalView");

        public static string Trash = Path.Combine("Prefabs", "Interactions", "Trash", "Trash");
    }

    public static class Sounds
    {
        public static string Asteroids = Path.Combine("Sounds", "Annonces", "Asteroids");
        public static string Drill = Path.Combine("Sounds", "Annonces", "Drill");
        public static string Door = Path.Combine("Sounds", "Annonces", "Door");
        public static string Terminal = Path.Combine("Sounds", "Annonces", "Terminal");
        public static string Trash = Path.Combine("Sounds", "Annonces", "Trash");
        public static string Tube = Path.Combine("Sounds", "Annonces", "Tube");
        public static string Complete = Path.Combine("Sounds", "Annonces", "Complete");

        public static string MiamiNight = Path.Combine("Sounds", "GameSounds", "miamiNight");
        public static string DoopOpen = Path.Combine("Sounds", "GameSounds", "doorOpen");
        public static string ButtonClick = Path.Combine("Sounds", "GameSounds", "ButtonClick");
        public static string WrongClick = Path.Combine("Sounds", "GameSounds", "wrong");
        public static string Success = Path.Combine("Sounds", "GameSounds", "success");
        public static string Sucked = Path.Combine("Sounds", "GameSounds", "trash");
        public static string AsteroidsDie = Path.Combine("Sounds", "GameSounds", "AsteroidDie");

        public static string EndPhrase1 = Path.Combine("Sounds", "Dialog", "endPhrase1");
        public static string EndPhrase2 = Path.Combine("Sounds", "Dialog", "endPhrase2");
        public static string EndPhrase3 = Path.Combine("Sounds", "Dialog", "endPhrase3");

    }

    public static class Translations
    {
        public static string Pause = nameof(Pause);
        public static string TimeUp = nameof(TimeUp);
    }
}
