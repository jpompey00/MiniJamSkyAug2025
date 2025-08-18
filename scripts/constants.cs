using System;
using Godot;

// enum Controls
// {
//     Left,
//     Right,
//     Throw1,
//     Throw2
// }

// enum CustomDataNames
// {
//     tilePosition
// }

// enum PositionOnTile
// {
//     ON_LEFT_EDGE = -1,
//     ON_RIGHT_EDGE = 1
// }


public static class Constants
{
    public const float SPEED = 750f;
    public const String CONTROLS_LEFT = "Left";
    public const String CONTROLS_RIGHT = "Right";
    public const String CONTROLS_THROW1 = "Throw1";
    public const String CONTROLS_THROW2 = "Throw2";
    public const String CUSTOM_DATA_TILE_POSITION = "tilePosition";
    public const int POSITION_ON_TILE_ON_LEFT_EDGE = -1;
    public const int POSITION_ON_TILE_ON_RIGHT_EDGE = 1;
    public const float COOLDOWN_TIME_SHOT = 0.75f;

    // public const float GRAVITY = 980f;
    public const float GRAVITY = 2000f;


    public const string LEVEL_1_PATH = "res://Scenes/Stages/Stage1.tscn";
    public const string LEVEL_2_PATH = "res://Scenes/Stages/Stage2.tscn";
    public const string LEVEL_3_PATH = "res://Scenes/Stages/Stage3.tscn";

    public const String FALL_ANIMATION_PATH = "res://Scenes/FallAnimation.tscn";

    public const String VICTORY_SCREEN = "res://Scenes/VictoryScreen.tscn";

    public const String TITLE_SCREEN = "res://Scenes/TitleScreen.tscn";

    public const String GAMEPLAY_MUSIC_CHILLED = "res://assets/Music/gameplay_chilled.mp3";

    public const String TITLE_SCREEN_MUSIC = "res://assets/Music/Sky_Tower_Title_Screen.mp3";
    public const String VICTORY = "res://assets/Music/Victory.mp3";
}

