using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants {
    //****************TAGS******************
    public const string PLAYERS_TAG = "Player";
    public const string PLAYER_PARENT_GO = "Players";
    public const string GROUND_TAG = "Ground";
    public const string PLATEFORM_TAG = "Plateform";
    public const string MOVING_PLATEFORM_TAG = "MovingPlateform";
    public const string SPAWNPOINT_TAG = "Spawn";
    public const string WEAPON_TAG = "Weapon";
    //****************INPUTS*****************
    public const string PLAYERS_HORIZONTAL_INPUT = "HorizontalP";
    public const string PLAYER_JUMP_INPUT = "JumpP";
    public const string PLAYER_FALLOFF_INPUT = "FallOffP";
    public const string PLAYER_ATTACK_INPUT = "AttackP";
    public const string PLAYER_SPELL_INPUT = "SpellP";
    //****************GUI Names**************
    public const string COUNT_TEXT_NAME = "Count";
    public const string END_TEXT_NAME = "End";
    public const string HEALTH_BAR_GRID_PANEL_NAME = "HealthBarsPanel";
    public const string START_POINT_MOVING_PLATEFORM = "StartPoint";
    public const string END_POINT_MOVING_PLATEFORM = "EndPoint";
    public const string LABEL_PANEL = "Player ";
    public const string TITLE_PANEL_BAR = "PlayerPanel";
    public const string TITLE_HEALTH_BAR = "PlayerHealth";
    public const string TITLE_FURY_BAR = "PlayerFury";
    //*****************GAMEPLAY***************
    public const int FURY_GAIN_SIMPLE_ATTACK = 3;
    //*****************OTHERS*****************
    public static Vector3 HEALTH_BAR_DEFAULT_POS = new Vector3(70, 350, 0);

}
