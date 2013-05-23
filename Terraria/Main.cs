using Microsoft.Win32;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
namespace Terraria
{
	public class Main : Game
	{
		private const int MF_BYPOSITION = 1024;
		public const int sectionWidth = 200;
		public const int sectionHeight = 150;
		public const int maxTileSets = 150;
		public const int maxWallTypes = 32;
		public const int maxBackgrounds = 32;
		public const int maxDust = 2000;
		public const int maxCombatText = 100;
		public const int maxItemText = 20;
		public const int maxPlayers = 255;
		public const int maxChests = 1000;
		public const int maxItemTypes = 604;
		public const int maxItems = 200;
		public const int maxBuffs = 41;
		public const int maxProjectileTypes = 112;
		public const int maxProjectiles = 1000;
		public const int maxNPCTypes = 147;
		public const int maxNPCs = 200;
		public const int maxGoreTypes = 160;
		public const int maxGore = 200;
		public const int maxInventory = 48;
		public const int maxItemSounds = 37;
		public const int maxNPCHitSounds = 11;
		public const int maxNPCKilledSounds = 15;
		public const int maxLiquidTypes = 2;
		public const int maxMusic = 14;
		public const int numArmorHead = 45;
		public const int numArmorBody = 26;
		public const int numArmorLegs = 25;
		public const double dayLength = 54000.0;
		public const double nightLength = 32400.0;
		public const int maxStars = 130;
		public const int maxStarTypes = 5;
		public const int maxClouds = 100;
		public const int maxCloudTypes = 4;
		public const int maxHair = 36;
		public static int curRelease = 39;
		public static string versionNumber = "v1.1.2";
		public static string versionNumber2 = "v1.1.2";
		public static bool skipMenu = false;
		public static bool verboseNetplay = false;
		public static bool stopTimeOuts = false;
		public static bool showSpam = false;
		public static bool showItemOwner = false;
		public static int oldTempLightCount = 0;
		public static int musicBox = -1;
		public static int musicBox2 = -1;
		public static bool cEd = false;
		public static float upTimer;
		public static float upTimerMax;
		public static float upTimerMaxDelay;
		public static float[] drawTimer = new float[10];
		public static float[] drawTimerMax = new float[10];
		public static float[] drawTimerMaxDelay = new float[10];
		public static float[] renderTimer = new float[10];
		public static float[] lightTimer = new float[10];
		public static bool drawDiag = false;
		public static bool drawRelease = false;
		public static bool renderNow = false;
		public static bool drawToScreen = false;
		public static bool targetSet = false;
		public static int mouseX;
		public static int mouseY;
		public static bool mouseLeft;
		public static bool mouseRight;
		public static float essScale = 1f;
		public static int essDir = -1;
		public static string debugWords = "";
		public static bool gamePad = false;
		public static bool xMas = false;
		public static int snowDust = 0;
		public static bool chTitle = false;
		public static int keyCount = 0;
		public static string[] keyString = new string[10];
		public static int[] keyInt = new int[10];
		public static bool netDiag = false;
		public static int txData = 0;
		public static int rxData = 0;
		public static int txMsg = 0;
		public static int rxMsg = 0;
		public static int maxMsg = 62;
		public static int[] rxMsgType = new int[Main.maxMsg];
		public static int[] rxDataType = new int[Main.maxMsg];
		public static int[] txMsgType = new int[Main.maxMsg];
		public static int[] txDataType = new int[Main.maxMsg];
		public static float uCarry = 0f;
		public static bool drawSkip = false;
		public static int fpsCount = 0;
		public static Stopwatch fpsTimer = new Stopwatch();
		public static Stopwatch updateTimer = new Stopwatch();
		public bool gammaTest;
		public static bool showSplash = true;
		public static bool ignoreErrors = true;
		public static string defaultIP = "";
		public static int dayRate = 1;
		public static int maxScreenW = 1920;
		public static int minScreenW = 800;
		public static int maxScreenH = 1200;
		public static int minScreenH = 600;
		public static float iS = 1f;
		public static bool render = false;
		public static int qaStyle = 0;
		public static int zoneX = 99;
		public static int zoneY = 87;
		public static float harpNote = 0f;
		public static bool[] projHostile = new bool[112];
		public static bool[] pvpBuff = new bool[41];
		public static bool[] debuff = new bool[41];
		public static string[] buffName = new string[41];
		public static string[] buffTip = new string[41];
		public static int maxMP = 10;
		public static string[] recentWorld = new string[Main.maxMP];
		public static string[] recentIP = new string[Main.maxMP];
		public static int[] recentPort = new int[Main.maxMP];
		public static bool shortRender = true;
		public static bool owBack = true;
		public static int quickBG = 2;
		public static int bgDelay = 0;
		public static int bgStyle = 0;
		public static float[] bgAlpha = new float[10];
		public static float[] bgAlpha2 = new float[10];
		public bool showNPCs;
		public int mouseNPC = -1;
		public static int wof = -1;
		public static int wofT;
		public static int wofB;
		public static int wofF = 0;
		private static int offScreenRange = 200;
		private RenderTarget2D backWaterTarget;
		private RenderTarget2D waterTarget;
		private RenderTarget2D tileTarget;
		private RenderTarget2D blackTarget;
		private RenderTarget2D tile2Target;
		private RenderTarget2D wallTarget;
		private RenderTarget2D backgroundTarget;
		private int firstTileX;
		private int lastTileX;
		private int firstTileY;
		private int lastTileY;
		private double bgParrallax;
		private int bgStart;
		private int bgLoops;
		private int bgStartY;
		private int bgLoopsY;
		private int bgTop;
		public static int renderCount = 99;
		private GraphicsDeviceManager graphics;
		private SpriteBatch spriteBatch;
		private Process tServer = new Process();
		private static Stopwatch saveTime = new Stopwatch();
		public static MouseState mouseState = Mouse.GetState();
		public static MouseState oldMouseState = Mouse.GetState();
		public static KeyboardState keyState = Keyboard.GetState();
		public static Color mcColor = new Color(125, 125, 255);
		public static Color hcColor = new Color(200, 125, 255);
		public static Color bgColor;
		public static bool mouseHC = false;
		public static string chestText = "Chest";
		public static bool craftingHide = false;
		public static bool armorHide = false;
		public static float craftingAlpha = 1f;
		public static float armorAlpha = 1f;
		public static float[] buffAlpha = new float[41];
		public static Item trashItem = new Item();
		public static bool hardMode = false;
		public float chestLootScale = 1f;
		public bool chestLootHover;
		public float chestStackScale = 1f;
		public bool chestStackHover;
		public float chestDepositScale = 1f;
		public bool chestDepositHover;
		public static bool drawScene = false;
		public static Vector2 sceneWaterPos = default(Vector2);
		public static Vector2 sceneTilePos = default(Vector2);
		public static Vector2 sceneTile2Pos = default(Vector2);
		public static Vector2 sceneWallPos = default(Vector2);
		public static Vector2 sceneBackgroundPos = default(Vector2);
		public static bool maxQ = true;
		public static float gfxQuality = 1f;
		public static float gfxRate = 0.01f;
		public int DiscoStyle;
		public static int DiscoR = 255;
		public static int DiscoB = 0;
		public static int DiscoG = 0;
		public static int teamCooldown = 0;
		public static int teamCooldownLen = 300;
		public static bool gamePaused = false;
		public static int updateTime = 0;
		public static int drawTime = 0;
		public static int uCount = 0;
		public static int updateRate = 0;
		public static int frameRate = 0;
		public static bool RGBRelease = false;
		public static bool qRelease = false;
		public static bool netRelease = false;
		public static bool frameRelease = false;
		public static bool showFrameRate = false;
		public static int magmaBGFrame = 0;
		public static int magmaBGFrameCounter = 0;
		public static int saveTimer = 0;
		public static bool autoJoin = false;
		public static bool serverStarting = false;
		public static float leftWorld = 0f;
		public static float rightWorld = 134400f;
		public static float topWorld = 0f;
		public static float bottomWorld = 38400f;
		public static int maxTilesX = (int)Main.rightWorld / 16 + 1;
		public static int maxTilesY = (int)Main.bottomWorld / 16 + 1;
		public static int maxSectionsX = Main.maxTilesX / 200;
		public static int maxSectionsY = Main.maxTilesY / 150;
		public static int numDust = 2000;
		public static int maxNetPlayers = 255;
		public static string[] chrName = new string[147];
		public static int worldRate = 1;
		public static float caveParrallax = 1f;
		public static string[] tileName = new string[150];
		public static int dungeonX;
		public static int dungeonY;
		public static Liquid[] liquid = new Liquid[Liquid.resLiquid];
		public static LiquidBuffer[] liquidBuffer = new LiquidBuffer[10000];
		public static bool dedServ = false;
		public static int spamCount = 0;
		public static int curMusic = 0;
		public int newMusic;
		public static bool showItemText = true;
		public static bool autoSave = true;
		public static string buffString = "";
		public static string libPath = "";
		public static int lo = 0;
		public static int LogoA = 255;
		public static int LogoB = 0;
		public static bool LogoT = false;
		public static string statusText = "";
		public static string worldName = "";
		public static int worldID;
		public static int background = 0;
		public static Color tileColor;
		public static double worldSurface;
		public static double rockLayer;
		public static Color[] teamColor = new Color[5];
		public static bool dayTime = true;
		public static double time = 13500.0;
		public static int moonPhase = 0;
		public static short sunModY = 0;
		public static short moonModY = 0;
		public static bool grabSky = false;
		public static bool bloodMoon = false;
		public static int checkForSpawns = 0;
		public static int helpText = 0;
		public static bool autoGen = false;
		public static bool autoPause = false;
		public static int[] projFrames = new int[112];
		public static float demonTorch = 1f;
		public static int demonTorchDir = 1;
		public static int numStars;
		public static int cloudLimit = 100;
		public static int numClouds = Main.cloudLimit;
		public static float windSpeed = 0f;
		public static float windSpeedSpeed = 0f;
		public static Cloud[] cloud = new Cloud[100];
		public static bool resetClouds = true;
		public static int sandTiles;
		public static int evilTiles;
		public static int snowTiles;
		public static int holyTiles;
		public static int meteorTiles;
		public static int jungleTiles;
		public static int dungeonTiles;
		public static int fadeCounter = 0;
		public static float invAlpha = 1f;
		public static float invDir = 1f;
		[ThreadStatic]
		public static Random rand;
		public static Texture2D[] bannerTexture = new Texture2D[3];
		public static Texture2D[] npcHeadTexture = new Texture2D[12];
		public static Texture2D[] destTexture = new Texture2D[3];
		public static Texture2D[] wingsTexture = new Texture2D[3];
		public static Texture2D[] armorHeadTexture = new Texture2D[45];
		public static Texture2D[] armorBodyTexture = new Texture2D[26];
		public static Texture2D[] femaleBodyTexture = new Texture2D[26];
		public static Texture2D[] armorArmTexture = new Texture2D[26];
		public static Texture2D[] armorLegTexture = new Texture2D[25];
		public static Texture2D timerTexture;
		public static Texture2D reforgeTexture;
		public static Texture2D wallOutlineTexture;
		public static Texture2D wireTexture;
		public static Texture2D gridTexture;
		public static Texture2D lightDiscTexture;
		public static Texture2D MusicBoxTexture;
		public static Texture2D EyeLaserTexture;
		public static Texture2D BoneEyesTexture;
		public static Texture2D BoneLaserTexture;
		public static Texture2D trashTexture;
		public static Texture2D chainTexture;
		public static Texture2D probeTexture;
		public static Texture2D confuseTexture;
		public static Texture2D chain2Texture;
		public static Texture2D chain3Texture;
		public static Texture2D chain4Texture;
		public static Texture2D chain5Texture;
		public static Texture2D chain6Texture;
		public static Texture2D chain7Texture;
		public static Texture2D chain8Texture;
		public static Texture2D chain9Texture;
		public static Texture2D chain10Texture;
		public static Texture2D chain11Texture;
		public static Texture2D chain12Texture;
		public static Texture2D chaosTexture;
		public static Texture2D cdTexture;
		public static Texture2D wofTexture;
		public static Texture2D boneArmTexture;
		public static Texture2D boneArm2Texture;
		public static Texture2D[] npcToggleTexture = new Texture2D[2];
		public static Texture2D[] HBLockTexture = new Texture2D[2];
		public static Texture2D[] buffTexture = new Texture2D[41];
		public static Texture2D[] itemTexture = new Texture2D[604];
		public static Texture2D[] npcTexture = new Texture2D[147];
		public static Texture2D[] projectileTexture = new Texture2D[112];
		public static Texture2D[] goreTexture = new Texture2D[160];
		public static Texture2D cursorTexture;
		public static Texture2D dustTexture;
		public static Texture2D sunTexture;
		public static Texture2D sun2Texture;
		public static Texture2D moonTexture;
		public static Texture2D[] tileTexture = new Texture2D[150];
		public static Texture2D blackTileTexture;
		public static Texture2D[] wallTexture = new Texture2D[32];
		public static Texture2D[] backgroundTexture = new Texture2D[32];
		public static Texture2D[] cloudTexture = new Texture2D[4];
		public static Texture2D[] starTexture = new Texture2D[5];
		public static Texture2D[] liquidTexture = new Texture2D[2];
		public static Texture2D heartTexture;
		public static Texture2D manaTexture;
		public static Texture2D bubbleTexture;
		public static Texture2D[] treeTopTexture = new Texture2D[5];
		public static Texture2D shroomCapTexture;
		public static Texture2D[] treeBranchTexture = new Texture2D[5];
		public static Texture2D inventoryBackTexture;
		public static Texture2D inventoryBack2Texture;
		public static Texture2D inventoryBack3Texture;
		public static Texture2D inventoryBack4Texture;
		public static Texture2D inventoryBack5Texture;
		public static Texture2D inventoryBack6Texture;
		public static Texture2D inventoryBack7Texture;
		public static Texture2D inventoryBack8Texture;
		public static Texture2D inventoryBack9Texture;
		public static Texture2D inventoryBack10Texture;
		public static Texture2D inventoryBack11Texture;
		public static Texture2D loTexture;
		public static Texture2D logoTexture;
		public static Texture2D logo2Texture;
		public static Texture2D logo3Texture;
		public static Texture2D textBackTexture;
		public static Texture2D chatTexture;
		public static Texture2D chat2Texture;
		public static Texture2D chatBackTexture;
		public static Texture2D teamTexture;
		public static Texture2D reTexture;
		public static Texture2D raTexture;
		public static Texture2D splashTexture;
		public static Texture2D fadeTexture;
		public static Texture2D ninjaTexture;
		public static Texture2D antLionTexture;
		public static Texture2D spikeBaseTexture;
		public static Texture2D ghostTexture;
		public static Texture2D evilCactusTexture;
		public static Texture2D goodCactusTexture;
		public static Texture2D wraithEyeTexture;
		public static Texture2D skinBodyTexture;
		public static Texture2D skinLegsTexture;
		public static Texture2D playerEyeWhitesTexture;
		public static Texture2D playerEyesTexture;
		public static Texture2D playerHandsTexture;
		public static Texture2D playerHands2Texture;
		public static Texture2D playerHeadTexture;
		public static Texture2D playerPantsTexture;
		public static Texture2D playerShirtTexture;
		public static Texture2D playerShoesTexture;
		public static Texture2D playerUnderShirtTexture;
		public static Texture2D playerUnderShirt2Texture;
		public static Texture2D femaleShirt2Texture;
		public static Texture2D femalePantsTexture;
		public static Texture2D femaleShirtTexture;
		public static Texture2D femaleShoesTexture;
		public static Texture2D femaleUnderShirtTexture;
		public static Texture2D femaleUnderShirt2Texture;
		public static Texture2D[] playerHairTexture = new Texture2D[36];
		public static Texture2D[] playerHairAltTexture = new Texture2D[36];
		public static SoundEffect[] soundMech = new SoundEffect[1];
		public static SoundEffectInstance[] soundInstanceMech = new SoundEffectInstance[1];
		public static SoundEffect[] soundDig = new SoundEffect[3];
		public static SoundEffectInstance[] soundInstanceDig = new SoundEffectInstance[3];
		public static SoundEffect[] soundTink = new SoundEffect[3];
		public static SoundEffectInstance[] soundInstanceTink = new SoundEffectInstance[3];
		public static SoundEffect[] soundPlayerHit = new SoundEffect[3];
		public static SoundEffectInstance[] soundInstancePlayerHit = new SoundEffectInstance[3];
		public static SoundEffect[] soundFemaleHit = new SoundEffect[3];
		public static SoundEffectInstance[] soundInstanceFemaleHit = new SoundEffectInstance[3];
		public static SoundEffect soundPlayerKilled;
		public static SoundEffectInstance soundInstancePlayerKilled;
		public static SoundEffect soundGrass;
		public static SoundEffectInstance soundInstanceGrass;
		public static SoundEffect soundGrab;
		public static SoundEffectInstance soundInstanceGrab;
		public static SoundEffect soundPixie;
		public static SoundEffectInstance soundInstancePixie;
		public static SoundEffect[] soundItem = new SoundEffect[38];
		public static SoundEffectInstance[] soundInstanceItem = new SoundEffectInstance[38];
		public static SoundEffect[] soundNPCHit = new SoundEffect[12];
		public static SoundEffectInstance[] soundInstanceNPCHit = new SoundEffectInstance[12];
		public static SoundEffect[] soundNPCKilled = new SoundEffect[16];
		public static SoundEffectInstance[] soundInstanceNPCKilled = new SoundEffectInstance[16];
		public static SoundEffect soundDoorOpen;
		public static SoundEffectInstance soundInstanceDoorOpen;
		public static SoundEffect soundDoorClosed;
		public static SoundEffectInstance soundInstanceDoorClosed;
		public static SoundEffect soundMenuOpen;
		public static SoundEffectInstance soundInstanceMenuOpen;
		public static SoundEffect soundMenuClose;
		public static SoundEffectInstance soundInstanceMenuClose;
		public static SoundEffect soundMenuTick;
		public static SoundEffectInstance soundInstanceMenuTick;
		public static SoundEffect soundShatter;
		public static SoundEffectInstance soundInstanceShatter;
		public static SoundEffect[] soundZombie = new SoundEffect[5];
		public static SoundEffectInstance[] soundInstanceZombie = new SoundEffectInstance[5];
		public static SoundEffect[] soundRoar = new SoundEffect[2];
		public static SoundEffectInstance[] soundInstanceRoar = new SoundEffectInstance[2];
		public static SoundEffect[] soundSplash = new SoundEffect[2];
		public static SoundEffectInstance[] soundInstanceSplash = new SoundEffectInstance[2];
		public static SoundEffect soundDoubleJump;
		public static SoundEffectInstance soundInstanceDoubleJump;
		public static SoundEffect soundRun;
		public static SoundEffectInstance soundInstanceRun;
		public static SoundEffect soundCoins;
		public static SoundEffectInstance soundInstanceCoins;
		public static SoundEffect soundUnlock;
		public static SoundEffectInstance soundInstanceUnlock;
		public static SoundEffect soundChat;
		public static SoundEffectInstance soundInstanceChat;
		public static SoundEffect soundMaxMana;
		public static SoundEffectInstance soundInstanceMaxMana;
		public static SoundEffect soundDrown;
		public static SoundEffectInstance soundInstanceDrown;
		public static AudioEngine engine;
		public static SoundBank soundBank;
		public static WaveBank waveBank;
		public static Cue[] music = new Cue[14];
		public static float[] musicFade = new float[14];
		public static float musicVolume = 0.75f;
		public static float soundVolume = 1f;
		public static SpriteFont fontItemStack;
		public static SpriteFont fontMouseText;
		public static SpriteFont fontDeathText;
		public static SpriteFont[] fontCombatText = new SpriteFont[2];
		public static bool[] tileLighted = new bool[150];
		public static bool[] tileMergeDirt = new bool[150];
		public static bool[] tileCut = new bool[150];
		public static bool[] tileAlch = new bool[150];
		public static int[] tileShine = new int[150];
		public static bool[] tileShine2 = new bool[150];
		public static bool[] wallHouse = new bool[32];
		public static int[] wallBlend = new int[32];
		public static bool[] tileStone = new bool[150];
		public static bool[] tilePick = new bool[150];
		public static bool[] tileAxe = new bool[150];
		public static bool[] tileHammer = new bool[150];
		public static bool[] tileWaterDeath = new bool[150];
		public static bool[] tileLavaDeath = new bool[150];
		public static bool[] tileTable = new bool[150];
		public static bool[] tileBlockLight = new bool[150];
		public static bool[] tileNoSunLight = new bool[150];
		public static bool[] tileDungeon = new bool[150];
		public static bool[] tileSolidTop = new bool[150];
		public static bool[] tileSolid = new bool[150];
		public static bool[] tileNoAttach = new bool[150];
		public static bool[] tileNoFail = new bool[150];
		public static bool[] tileFrameImportant = new bool[150];
		public static int[] backgroundWidth = new int[32];
		public static int[] backgroundHeight = new int[32];
		public static bool tilesLoaded = false;
		public static Tile[,] tile = new Tile[Main.maxTilesX, Main.maxTilesY];
		public static Dust[] dust = new Dust[2001];
		public static Star[] star = new Star[130];
		public static Item[] item = new Item[201];
		public static NPC[] npc = new NPC[201];
		public static Gore[] gore = new Gore[201];
		public static Projectile[] projectile = new Projectile[1001];
		public static CombatText[] combatText = new CombatText[100];
		public static ItemText[] itemText = new ItemText[20];
		public static Chest[] chest = new Chest[1000];
		public static Sign[] sign = new Sign[1000];
		public static Vector2 screenPosition;
		public static Vector2 screenLastPosition;
		public static int screenWidth = 800;
		public static int screenHeight = 600;
		public static int chatLength = 600;
		public static bool chatMode = false;
		public static bool chatRelease = false;
		public static int numChatLines = 7;
		public static string chatText = "";
		public static ChatLine[] chatLine = new ChatLine[Main.numChatLines];
		public static bool inputTextEnter = false;
		public static float[] hotbarScale = new float[]
		{
			1f,
			0.75f,
			0.75f,
			0.75f,
			0.75f,
			0.75f,
			0.75f,
			0.75f,
			0.75f,
			0.75f
		};
		public static byte mouseTextColor = 0;
		public static int mouseTextColorChange = 1;
		public static bool mouseLeftRelease = false;
		public static bool mouseRightRelease = false;
		public static bool playerInventory = false;
		public static int stackSplit;
		public static int stackCounter = 0;
		public static int stackDelay = 7;
		public static Item mouseItem = new Item();
		public static Item guideItem = new Item();
		public static Item reforgeItem = new Item();
		private static float inventoryScale = 0.75f;
		public static bool hasFocus = true;
		public static Recipe[] recipe = new Recipe[Recipe.maxRecipes];
		public static int[] availableRecipe = new int[Recipe.maxRecipes];
		public static float[] availableRecipeY = new float[Recipe.maxRecipes];
		public static int numAvailableRecipes;
		public static int focusRecipe;
		public static int myPlayer = 0;
		public static Player[] player = new Player[256];
		public static int spawnTileX;
		public static int spawnTileY;
		public static bool npcChatRelease = false;
		public static bool editSign = false;
		public static string signText = "";
		public static string npcChatText = "";
		public static bool npcChatFocus1 = false;
		public static bool npcChatFocus2 = false;
		public static bool npcChatFocus3 = false;
		public static int npcShop = 0;
		public Chest[] shop = new Chest[10];
		public static bool craftGuide = false;
		public static bool reforge = false;
		private static Item toolTip = new Item();
		private static int backSpaceCount = 0;
		public static string motd = "";
		public bool toggleFullscreen;
		private int numDisplayModes;
		private int[] displayWidth = new int[99];
		private int[] displayHeight = new int[99];
		public static bool gameMenu = true;
		public static Player[] loadPlayer = new Player[5];
		public static string[] loadPlayerPath = new string[5];
		private static int numLoadPlayers = 0;
		public static string playerPathName;
		public static string[] loadWorld = new string[999];
		public static string[] loadWorldPath = new string[999];
		private static int numLoadWorlds = 0;
		public static string worldPathName;
		public static string SavePath = string.Concat(new object[]
		{
			Environment.GetFolderPath(Environment.SpecialFolder.Personal),
			Path.DirectorySeparatorChar,
			"My Games",
			Path.DirectorySeparatorChar,
			"Terraria"
		});
		public static string WorldPath = Main.SavePath + Path.DirectorySeparatorChar + "Worlds";
		public static string PlayerPath = Main.SavePath + Path.DirectorySeparatorChar + "Players";
		public static string[] itemName = new string[604];
		public static string[] npcName = new string[147];
		private static KeyboardState inputText;
		private static KeyboardState oldInputText;
		public static int invasionType = 0;
		public static double invasionX = 0.0;
		public static int invasionSize = 0;
		public static int invasionDelay = 0;
		public static int invasionWarn = 0;
		public static int[] npcFrameCount = new int[]
		{
			1,
			2,
			2,
			3,
			6,
			2,
			2,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			2,
			16,
			14,
			16,
			14,
			15,
			16,
			2,
			10,
			1,
			16,
			16,
			16,
			3,
			1,
			15,
			3,
			1,
			3,
			1,
			1,
			16,
			16,
			1,
			1,
			1,
			3,
			3,
			15,
			3,
			7,
			7,
			4,
			5,
			5,
			5,
			3,
			3,
			16,
			6,
			3,
			6,
			6,
			2,
			5,
			3,
			2,
			7,
			7,
			4,
			2,
			8,
			1,
			5,
			1,
			2,
			4,
			16,
			5,
			4,
			4,
			15,
			15,
			15,
			15,
			2,
			4,
			6,
			6,
			18,
			16,
			1,
			1,
			1,
			1,
			1,
			1,
			4,
			3,
			1,
			1,
			1,
			1,
			1,
			1,
			5,
			6,
			7,
			16,
			1,
			1,
			16,
			16,
			12,
			20,
			21,
			1,
			2,
			2,
			3,
			6,
			1,
			1,
			1,
			15,
			4,
			11,
			1,
			14,
			6,
			6,
			3,
			1,
			2,
			2,
			1,
			3,
			4,
			1,
			2,
			1,
			4,
			2,
			1,
			15,
			3,
			16,
			4,
			5,
			7,
			3
		};
		private static bool mouseExit = false;
		private static float exitScale = 0.8f;
		private static bool mouseReforge = false;
		private static float reforgeScale = 0.8f;
		public static Player clientPlayer = new Player();
		public static string getIP = Main.defaultIP;
		public static string getPort = Convert.ToString(Netplay.serverPort);
		public static bool menuMultiplayer = false;
		public static bool menuServer = false;
		public static int netMode = 0;
		public static int timeOut = 120;
		public static int netPlayCounter;
		public static int lastNPCUpdate;
		public static int lastItemUpdate;
		public static int maxNPCUpdates = 5;
		public static int maxItemUpdates = 5;
		public static string cUp = "W";
		public static string cLeft = "A";
		public static string cDown = "S";
		public static string cRight = "D";
		public static string cJump = "Space";
		public static string cThrowItem = "T";
		public static string cInv = "Escape";
		public static string cHeal = "H";
		public static string cMana = "M";
		public static string cBuff = "B";
		public static string cHook = "E";
		public static string cTorch = "LeftShift";
		public static Color mouseColor = new Color(255, 50, 95);
		public static Color cursorColor = Color.White;
		public static int cursorColorDirection = 1;
		public static float cursorAlpha = 0f;
		public static float cursorScale = 0f;
		public static bool signBubble = false;
		public static int signX = 0;
		public static int signY = 0;
		public static bool hideUI = false;
		public static bool releaseUI = false;
		public static bool fixedTiming = false;
		private int splashCounter;
		public static string oldStatusText = "";
		public static bool autoShutdown = false;
		private float logoRotation;
		private float logoRotationDirection = 1f;
		private float logoRotationSpeed = 1f;
		private float logoScale = 1f;
		private float logoScaleDirection = 1f;
		private float logoScaleSpeed = 1f;
		private static int maxMenuItems = 14;
		private float[] menuItemScale = new float[Main.maxMenuItems];
		private int focusMenu = -1;
		private int selectedMenu = -1;
		private int selectedMenu2 = -1;
		private int selectedPlayer;
		private int selectedWorld;
		public static int menuMode = 0;
		private static Item cpItem = new Item();
		private int textBlinkerCount;
		private int textBlinkerState;
		public static string newWorldName = "";
		private static int accSlotCount = 0;
		private Color selColor = Color.White;
		private int focusColor;
		private int colorDelay;
		private int setKey = -1;
		private int bgScroll;
		public static bool autoPass = false;
		public static int menuFocus = 0;
		[DllImport("User32")]
		private static extern int RemoveMenu(IntPtr hMenu, int nPosition, int wFlags);
		[DllImport("User32")]
		private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
		[DllImport("User32")]
		private static extern int GetMenuItemCount(IntPtr hWnd);
		[DllImport("kernel32.dll")]
		public static extern IntPtr LoadLibrary(string dllToLoad);
		public static void LoadWorlds()
		{
			Directory.CreateDirectory(Main.WorldPath);
			string[] files = Directory.GetFiles(Main.WorldPath, "*.wld");
			int num = files.Length;
			if (!Main.dedServ && num > 5)
			{
				num = 5;
			}
			for (int i = 0; i < num; i++)
			{
				Main.loadWorldPath[i] = files[i];
				try
				{
					using (FileStream fileStream = new FileStream(Main.loadWorldPath[i], FileMode.Open))
					{
						using (BinaryReader binaryReader = new BinaryReader(fileStream))
						{
							binaryReader.ReadInt32();
							Main.loadWorld[i] = binaryReader.ReadString();
							binaryReader.Close();
						}
					}
				}
				catch
				{
					Main.loadWorld[i] = Main.loadWorldPath[i];
				}
			}
			Main.numLoadWorlds = num;
		}
		private static void LoadPlayers()
		{
			Directory.CreateDirectory(Main.PlayerPath);
			string[] files = Directory.GetFiles(Main.PlayerPath, "*.plr");
			int num = files.Length;
			if (num > 5)
			{
				num = 5;
			}
			for (int i = 0; i < 5; i++)
			{
				Main.loadPlayer[i] = new Player();
				if (i < num)
				{
					Main.loadPlayerPath[i] = files[i];
					Main.loadPlayer[i] = Player.LoadPlayer(Main.loadPlayerPath[i]);
				}
			}
			Main.numLoadPlayers = num;
		}
		protected void OpenRecent()
		{
			try
			{
				if (File.Exists(Main.SavePath + Path.DirectorySeparatorChar + "servers.dat"))
				{
					using (FileStream fileStream = new FileStream(Main.SavePath + Path.DirectorySeparatorChar + "servers.dat", FileMode.Open))
					{
						using (BinaryReader binaryReader = new BinaryReader(fileStream))
						{
							binaryReader.ReadInt32();
							for (int i = 0; i < 10; i++)
							{
								Main.recentWorld[i] = binaryReader.ReadString();
								Main.recentIP[i] = binaryReader.ReadString();
								Main.recentPort[i] = binaryReader.ReadInt32();
							}
						}
					}
				}
			}
			catch
			{
			}
		}
		public static void SaveRecent()
		{
			Directory.CreateDirectory(Main.SavePath);
			try
			{
				File.SetAttributes(Main.SavePath + Path.DirectorySeparatorChar + "servers.dat", FileAttributes.Normal);
			}
			catch
			{
			}
			try
			{
				using (FileStream fileStream = new FileStream(Main.SavePath + Path.DirectorySeparatorChar + "servers.dat", FileMode.Create))
				{
					using (BinaryWriter binaryWriter = new BinaryWriter(fileStream))
					{
						binaryWriter.Write(Main.curRelease);
						for (int i = 0; i < 10; i++)
						{
							binaryWriter.Write(Main.recentWorld[i]);
							binaryWriter.Write(Main.recentIP[i]);
							binaryWriter.Write(Main.recentPort[i]);
						}
					}
				}
			}
			catch
			{
			}
		}
		protected void SaveSettings()
		{
			Directory.CreateDirectory(Main.SavePath);
			try
			{
				File.SetAttributes(Main.SavePath + Path.DirectorySeparatorChar + "config.dat", FileAttributes.Normal);
			}
			catch
			{
			}
			try
			{
				using (FileStream fileStream = new FileStream(Main.SavePath + Path.DirectorySeparatorChar + "config.dat", FileMode.Create))
				{
					using (BinaryWriter binaryWriter = new BinaryWriter(fileStream))
					{
						binaryWriter.Write(Main.curRelease);
						binaryWriter.Write(this.graphics.IsFullScreen);
						binaryWriter.Write(Main.mouseColor.R);
						binaryWriter.Write(Main.mouseColor.G);
						binaryWriter.Write(Main.mouseColor.B);
						binaryWriter.Write(Main.soundVolume);
						binaryWriter.Write(Main.musicVolume);
						binaryWriter.Write(Main.cUp);
						binaryWriter.Write(Main.cDown);
						binaryWriter.Write(Main.cLeft);
						binaryWriter.Write(Main.cRight);
						binaryWriter.Write(Main.cJump);
						binaryWriter.Write(Main.cThrowItem);
						binaryWriter.Write(Main.cInv);
						binaryWriter.Write(Main.cHeal);
						binaryWriter.Write(Main.cMana);
						binaryWriter.Write(Main.cBuff);
						binaryWriter.Write(Main.cHook);
						binaryWriter.Write(Main.caveParrallax);
						binaryWriter.Write(Main.fixedTiming);
						binaryWriter.Write(this.graphics.PreferredBackBufferWidth);
						binaryWriter.Write(this.graphics.PreferredBackBufferHeight);
						binaryWriter.Write(Main.autoSave);
						binaryWriter.Write(Main.autoPause);
						binaryWriter.Write(Main.showItemText);
						binaryWriter.Write(Main.cTorch);
						binaryWriter.Write((byte)Lighting.lightMode);
						binaryWriter.Write((byte)Main.qaStyle);
						binaryWriter.Write(Main.owBack);
						binaryWriter.Write((byte)Lang.lang);
						binaryWriter.Close();
					}
				}
			}
			catch
			{
			}
		}
		protected void CheckBunny()
		{
			try
			{
				RegistryKey registryKey = Registry.CurrentUser;
				registryKey = registryKey.CreateSubKey("Software\\Terraria");
				if (registryKey != null && registryKey.GetValue("Bunny") != null && registryKey.GetValue("Bunny").ToString() == "1")
				{
					Main.cEd = true;
				}
			}
			catch
			{
				Main.cEd = false;
			}
		}
		protected void OpenSettings()
		{
			try
			{
				if (File.Exists(Main.SavePath + Path.DirectorySeparatorChar + "config.dat"))
				{
					using (FileStream fileStream = new FileStream(Main.SavePath + Path.DirectorySeparatorChar + "config.dat", FileMode.Open))
					{
						using (BinaryReader binaryReader = new BinaryReader(fileStream))
						{
							int num = binaryReader.ReadInt32();
							bool flag = binaryReader.ReadBoolean();
							Main.mouseColor.R = binaryReader.ReadByte();
							Main.mouseColor.G = binaryReader.ReadByte();
							Main.mouseColor.B = binaryReader.ReadByte();
							Main.soundVolume = binaryReader.ReadSingle();
							Main.musicVolume = binaryReader.ReadSingle();
							Main.cUp = binaryReader.ReadString();
							Main.cDown = binaryReader.ReadString();
							Main.cLeft = binaryReader.ReadString();
							Main.cRight = binaryReader.ReadString();
							Main.cJump = binaryReader.ReadString();
							Main.cThrowItem = binaryReader.ReadString();
							if (num >= 1)
							{
								Main.cInv = binaryReader.ReadString();
							}
							if (num >= 12)
							{
								Main.cHeal = binaryReader.ReadString();
								Main.cMana = binaryReader.ReadString();
								Main.cBuff = binaryReader.ReadString();
							}
							if (num >= 13)
							{
								Main.cHook = binaryReader.ReadString();
							}
							Main.caveParrallax = binaryReader.ReadSingle();
							if (num >= 2)
							{
								Main.fixedTiming = binaryReader.ReadBoolean();
							}
							if (num >= 4)
							{
								this.graphics.PreferredBackBufferWidth = binaryReader.ReadInt32();
								this.graphics.PreferredBackBufferHeight = binaryReader.ReadInt32();
							}
							if (num >= 8)
							{
								Main.autoSave = binaryReader.ReadBoolean();
							}
							if (num >= 9)
							{
								Main.autoPause = binaryReader.ReadBoolean();
							}
							if (num >= 19)
							{
								Main.showItemText = binaryReader.ReadBoolean();
							}
							if (num >= 30)
							{
								Main.cTorch = binaryReader.ReadString();
								Lighting.lightMode = (int)binaryReader.ReadByte();
								Main.qaStyle = (int)binaryReader.ReadByte();
							}
							if (num >= 37)
							{
								Main.owBack = binaryReader.ReadBoolean();
							}
							if (num >= 39)
							{
								Lang.lang = (int)binaryReader.ReadByte();
							}
							binaryReader.Close();
							if (flag && !this.graphics.IsFullScreen)
							{
								this.graphics.ToggleFullScreen();
							}
						}
					}
				}
			}
			catch
			{
			}
		}
		private static void ErasePlayer(int i)
		{
			try
			{
				File.Delete(Main.loadPlayerPath[i]);
				File.Delete(Main.loadPlayerPath[i] + ".bak");
				Main.LoadPlayers();
			}
			catch
			{
			}
		}
		private static void EraseWorld(int i)
		{
			try
			{
				File.Delete(Main.loadWorldPath[i]);
				File.Delete(Main.loadWorldPath[i] + ".bak");
				Main.LoadWorlds();
			}
			catch
			{
			}
		}
		private static string nextLoadPlayer()
		{
			int num = 1;
			while (File.Exists(string.Concat(new object[]
			{
				Main.PlayerPath,
				Path.DirectorySeparatorChar,
				"player",
				num,
				".plr"
			})))
			{
				num++;
			}
			return string.Concat(new object[]
			{
				Main.PlayerPath,
				Path.DirectorySeparatorChar,
				"player",
				num,
				".plr"
			});
		}
		private static string nextLoadWorld()
		{
			int num = 1;
			while (File.Exists(string.Concat(new object[]
			{
				Main.WorldPath,
				Path.DirectorySeparatorChar,
				"world",
				num,
				".wld"
			})))
			{
				num++;
			}
			return string.Concat(new object[]
			{
				Main.WorldPath,
				Path.DirectorySeparatorChar,
				"world",
				num,
				".wld"
			});
		}
		public void autoCreate(string newOpt)
		{
			if (newOpt == "0")
			{
				Main.autoGen = false;
				return;
			}
			if (newOpt == "1")
			{
				Main.maxTilesX = 4200;
				Main.maxTilesY = 1200;
				Main.autoGen = true;
				return;
			}
			if (newOpt == "2")
			{
				Main.maxTilesX = 6300;
				Main.maxTilesY = 1800;
				Main.autoGen = true;
				return;
			}
			if (newOpt == "3")
			{
				Main.maxTilesX = 8400;
				Main.maxTilesY = 2400;
				Main.autoGen = true;
			}
		}
		public void NewMOTD(string newMOTD)
		{
			Main.motd = newMOTD;
		}
		public void LoadDedConfig(string configPath)
		{
			if (File.Exists(configPath))
			{
				using (StreamReader streamReader = new StreamReader(configPath))
				{
					string text;
					while ((text = streamReader.ReadLine()) != null)
					{
						try
						{
							if (text.Length > 6 && text.Substring(0, 6).ToLower() == "world=")
							{
								string text2 = text.Substring(6);
								Main.worldPathName = text2;
							}
							if (text.Length > 5 && text.Substring(0, 5).ToLower() == "port=")
							{
								string value = text.Substring(5);
								try
								{
									int serverPort = Convert.ToInt32(value);
									Netplay.serverPort = serverPort;
								}
								catch
								{
								}
							}
							if (text.Length > 11 && text.Substring(0, 11).ToLower() == "maxplayers=")
							{
								string value2 = text.Substring(11);
								try
								{
									int num = Convert.ToInt32(value2);
									Main.maxNetPlayers = num;
								}
								catch
								{
								}
							}
							if (text.Length > 11 && text.Substring(0, 9).ToLower() == "priority=")
							{
								string value3 = text.Substring(9);
								try
								{
									int num2 = Convert.ToInt32(value3);
									if (num2 >= 0 && num2 <= 5)
									{
										Process currentProcess = Process.GetCurrentProcess();
										if (num2 == 0)
										{
											currentProcess.PriorityClass = ProcessPriorityClass.RealTime;
										}
										else
										{
											if (num2 == 1)
											{
												currentProcess.PriorityClass = ProcessPriorityClass.High;
											}
											else
											{
												if (num2 == 2)
												{
													currentProcess.PriorityClass = ProcessPriorityClass.AboveNormal;
												}
												else
												{
													if (num2 == 3)
													{
														currentProcess.PriorityClass = ProcessPriorityClass.Normal;
													}
													else
													{
														if (num2 == 4)
														{
															currentProcess.PriorityClass = ProcessPriorityClass.BelowNormal;
														}
														else
														{
															if (num2 == 5)
															{
																currentProcess.PriorityClass = ProcessPriorityClass.Idle;
															}
														}
													}
												}
											}
										}
									}
								}
								catch
								{
								}
							}
							if (text.Length > 9 && text.Substring(0, 9).ToLower() == "password=")
							{
								string password = text.Substring(9);
								Netplay.password = password;
							}
							if (text.Length > 5 && text.Substring(0, 5).ToLower() == "motd=")
							{
								string text3 = text.Substring(5);
								Main.motd = text3;
							}
							if (text.Length > 5 && text.Substring(0, 5).ToLower() == "lang=")
							{
								string value4 = text.Substring(5);
								Lang.lang = Convert.ToInt32(value4);
							}
							if (text.Length >= 10 && text.Substring(0, 10).ToLower() == "worldpath=")
							{
								string worldPath = text.Substring(10);
								Main.WorldPath = worldPath;
							}
							if (text.Length >= 10 && text.Substring(0, 10).ToLower() == "worldname=")
							{
								string text4 = text.Substring(10);
								Main.worldName = text4;
							}
							if (text.Length > 8 && text.Substring(0, 8).ToLower() == "banlist=")
							{
								string banFile = text.Substring(8);
								Netplay.banFile = banFile;
							}
							if (text.Length > 11 && text.Substring(0, 11).ToLower() == "autocreate=")
							{
								string a = text.Substring(11);
								if (a == "0")
								{
									Main.autoGen = false;
								}
								else
								{
									if (a == "1")
									{
										Main.maxTilesX = 4200;
										Main.maxTilesY = 1200;
										Main.autoGen = true;
									}
									else
									{
										if (a == "2")
										{
											Main.maxTilesX = 6300;
											Main.maxTilesY = 1800;
											Main.autoGen = true;
										}
										else
										{
											if (a == "3")
											{
												Main.maxTilesX = 8400;
												Main.maxTilesY = 2400;
												Main.autoGen = true;
											}
										}
									}
								}
							}
							if (text.Length > 7 && text.Substring(0, 7).ToLower() == "secure=")
							{
								string a2 = text.Substring(7);
								if (a2 == "1")
								{
									Netplay.spamCheck = true;
								}
							}
						}
						catch
						{
						}
					}
				}
			}
		}
		public void SetNetPlayers(int mPlayers)
		{
			Main.maxNetPlayers = mPlayers;
		}
		public void SetWorld(string wrold)
		{
			Main.worldPathName = wrold;
		}
		public void SetWorldName(string wrold)
		{
			Main.worldName = wrold;
		}
		public void autoShut()
		{
			Main.autoShutdown = true;
		}
		[DllImport("user32.dll")]
		public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
		[DllImport("user32.dll")]
		private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
		public void AutoPass()
		{
			Main.autoPass = true;
		}
		public void AutoJoin(string IP)
		{
			Main.defaultIP = IP;
			Main.getIP = IP;
			Netplay.SetIP(Main.defaultIP);
			Main.autoJoin = true;
		}
		public void AutoHost()
		{
			Main.menuMultiplayer = true;
			Main.menuServer = true;
			Main.menuMode = 1;
		}
		public void loadLib(string path)
		{
			Main.libPath = path;
			Main.LoadLibrary(Main.libPath);
		}
		public void DedServ()
		{
			Main.rand = new Random();
			if (Main.autoShutdown)
			{
				string text = "terraria" + Main.rand.Next(2147483647);
				Console.Title = text;
				IntPtr intPtr = Main.FindWindow(null, text);
				if (intPtr != IntPtr.Zero)
				{
					Main.ShowWindow(intPtr, 0);
				}
			}
			else
			{
				Console.Title = "Terraria Server " + Main.versionNumber2;
			}
			Main.dedServ = true;
			Main.showSplash = false;
			this.Initialize();
			Lang.setLang();
			for (int i = 0; i < 147; i++)
			{
				NPC nPC = new NPC();
				nPC.SetDefaults(i, -1f);
				Main.npcName[i] = nPC.name;
			}
			while (Main.worldPathName == null || Main.worldPathName == "")
			{
				Main.LoadWorlds();
				bool flag = true;
				while (flag)
				{
					Console.WriteLine("Terraria Server " + Main.versionNumber2);
					Console.WriteLine("");
					for (int j = 0; j < Main.numLoadWorlds; j++)
					{
						Console.WriteLine(string.Concat(new object[]
						{
							j + 1,
							'\t',
							'\t',
							Main.loadWorld[j]
						}));
					}
					Console.WriteLine(string.Concat(new object[]
					{
						"n",
						'\t',
						'\t',
						"New World"
					}));
					Console.WriteLine("d <number>" + '\t' + "Delete World");
					Console.WriteLine("");
					Console.Write("Choose World: ");
					string text2 = Console.ReadLine();
					try
					{
						Console.Clear();
					}
					catch
					{
					}
					if (text2.Length >= 2 && text2.Substring(0, 2).ToLower() == "d ")
					{
						try
						{
							int num = Convert.ToInt32(text2.Substring(2)) - 1;
							if (num < Main.numLoadWorlds)
							{
								Console.WriteLine("Terraria Server " + Main.versionNumber2);
								Console.WriteLine("");
								Console.WriteLine("Really delete " + Main.loadWorld[num] + "?");
								Console.Write("(y/n): ");
								string text3 = Console.ReadLine();
								if (text3.ToLower() == "y")
								{
									Main.EraseWorld(num);
								}
							}
						}
						catch
						{
						}
						try
						{
							Console.Clear();
							continue;
						}
						catch
						{
							continue;
						}
					}
					if (text2 == "n" || text2 == "N")
					{
						bool flag2 = true;
						while (flag2)
						{
							Console.WriteLine("Terraria Server " + Main.versionNumber2);
							Console.WriteLine("");
							Console.WriteLine("1" + '\t' + "Small");
							Console.WriteLine("2" + '\t' + "Medium");
							Console.WriteLine("3" + '\t' + "Large");
							Console.WriteLine("");
							Console.Write("Choose size: ");
							string value = Console.ReadLine();
							try
							{
								int num2 = Convert.ToInt32(value);
								if (num2 == 1)
								{
									Main.maxTilesX = 4200;
									Main.maxTilesY = 1200;
									flag2 = false;
								}
								else
								{
									if (num2 == 2)
									{
										Main.maxTilesX = 6300;
										Main.maxTilesY = 1800;
										flag2 = false;
									}
									else
									{
										if (num2 == 3)
										{
											Main.maxTilesX = 8400;
											Main.maxTilesY = 2400;
											flag2 = false;
										}
									}
								}
							}
							catch
							{
							}
							try
							{
								Console.Clear();
							}
							catch
							{
							}
						}
						flag2 = true;
						while (flag2)
						{
							Console.WriteLine("Terraria Server " + Main.versionNumber2);
							Console.WriteLine("");
							Console.Write("Enter world name: ");
							Main.newWorldName = Console.ReadLine();
							if (Main.newWorldName != "" && Main.newWorldName != " " && Main.newWorldName != null)
							{
								flag2 = false;
							}
							try
							{
								Console.Clear();
							}
							catch
							{
							}
						}
						Main.worldName = Main.newWorldName;
						Main.worldPathName = Main.nextLoadWorld();
						Main.menuMode = 10;
						WorldGen.CreateNewWorld();
						flag2 = false;
						while (Main.menuMode == 10)
						{
							if (Main.oldStatusText != Main.statusText)
							{
								Main.oldStatusText = Main.statusText;
								Console.WriteLine(Main.statusText);
							}
						}
						try
						{
							Console.Clear();
							continue;
						}
						catch
						{
							continue;
						}
					}
					try
					{
						int num3 = Convert.ToInt32(text2);
						num3--;
						if (num3 >= 0 && num3 < Main.numLoadWorlds)
						{
							bool flag3 = true;
							while (flag3)
							{
								Console.WriteLine("Terraria Server " + Main.versionNumber2);
								Console.WriteLine("");
								Console.Write("Max players (press enter for 8): ");
								string text4 = Console.ReadLine();
								try
								{
									if (text4 == "")
									{
										text4 = "8";
									}
									int num4 = Convert.ToInt32(text4);
									if (num4 <= 255 && num4 >= 1)
									{
										Main.maxNetPlayers = num4;
										flag3 = false;
									}
									flag3 = false;
								}
								catch
								{
								}
								try
								{
									Console.Clear();
								}
								catch
								{
								}
							}
							flag3 = true;
							while (flag3)
							{
								Console.WriteLine("Terraria Server " + Main.versionNumber2);
								Console.WriteLine("");
								Console.Write("Server port (press enter for 7777): ");
								string text5 = Console.ReadLine();
								try
								{
									if (text5 == "")
									{
										text5 = "7777";
									}
									int num5 = Convert.ToInt32(text5);
									if (num5 <= 65535)
									{
										Netplay.serverPort = num5;
										flag3 = false;
									}
								}
								catch
								{
								}
								try
								{
									Console.Clear();
								}
								catch
								{
								}
							}
							Console.WriteLine("Terraria Server " + Main.versionNumber2);
							Console.WriteLine("");
							Console.Write("Server password (press enter for none): ");
							Netplay.password = Console.ReadLine();
							Main.worldPathName = Main.loadWorldPath[num3];
							flag = false;
							try
							{
								Console.Clear();
							}
							catch
							{
							}
						}
					}
					catch
					{
					}
				}
			}
			try
			{
				Console.Clear();
			}
			catch
			{
			}
			WorldGen.serverLoadWorld();
			Console.WriteLine("Terraria Server " + Main.versionNumber);
			Console.WriteLine("");
			while (!Netplay.ServerUp)
			{
				if (Main.oldStatusText != Main.statusText)
				{
					Main.oldStatusText = Main.statusText;
					Console.WriteLine(Main.statusText);
				}
			}
			try
			{
				Console.Clear();
			}
			catch
			{
			}
			Console.WriteLine("Terraria Server " + Main.versionNumber);
			Console.WriteLine("");
			Console.WriteLine("Listening on port " + Netplay.serverPort);
			Console.WriteLine("Type 'help' for a list of commands.");
			Console.WriteLine("");
			Console.Title = "Terraria Server: " + Main.worldName;
			Stopwatch stopwatch = new Stopwatch();
			if (!Main.autoShutdown)
			{
				Main.startDedInput();
			}
			stopwatch.Start();
			double num6 = 16.666666666666668;
			double num7 = 0.0;
			int num8 = 0;
			Stopwatch stopwatch2 = new Stopwatch();
			stopwatch2.Start();
			while (!Netplay.disconnect)
			{
				double num9 = (double)stopwatch.ElapsedMilliseconds;
				if (num9 + num7 >= num6)
				{
					num8++;
					num7 += num9 - num6;
					stopwatch.Reset();
					stopwatch.Start();
					if (Main.oldStatusText != Main.statusText)
					{
						Main.oldStatusText = Main.statusText;
						Console.WriteLine(Main.statusText);
					}
					if (num7 > 1000.0)
					{
						num7 = 1000.0;
					}
					if (Netplay.anyClients)
					{
						this.Update(new GameTime());
					}
					double num10 = (double)stopwatch.ElapsedMilliseconds + num7;
					if (num10 < num6)
					{
						int num11 = (int)(num6 - num10) - 1;
						if (num11 > 1)
						{
							Thread.Sleep(num11);
							if (!Netplay.anyClients)
							{
								num7 = 0.0;
								Thread.Sleep(10);
							}
						}
					}
				}
				Thread.Sleep(0);
			}
		}
		public static void startDedInput()
		{
			ThreadPool.QueueUserWorkItem(new WaitCallback(Main.startDedInputCallBack), 1);
		}
		public static void startDedInputCallBack(object threadContext)
		{
			while (!Netplay.disconnect)
			{
				Console.Write(": ");
				string text = Console.ReadLine();
				string text2 = text;
				text = text.ToLower();
				try
				{
					if (text == "help")
					{
						Console.WriteLine("Available commands:");
						Console.WriteLine("");
						Console.WriteLine(string.Concat(new object[]
						{
							"help ",
							'\t',
							'\t',
							" Displays a list of commands."
						}));
						Console.WriteLine("playing " + '\t' + " Shows the list of players");
						Console.WriteLine(string.Concat(new object[]
						{
							"clear ",
							'\t',
							'\t',
							" Clear the console window."
						}));
						Console.WriteLine(string.Concat(new object[]
						{
							"exit ",
							'\t',
							'\t',
							" Shutdown the server and save."
						}));
						Console.WriteLine("exit-nosave " + '\t' + " Shutdown the server without saving.");
						Console.WriteLine(string.Concat(new object[]
						{
							"save ",
							'\t',
							'\t',
							" Save the game world."
						}));
						Console.WriteLine("kick <player> " + '\t' + " Kicks a player from the server.");
						Console.WriteLine("ban <player> " + '\t' + " Bans a player from the server.");
						Console.WriteLine("password" + '\t' + " Show password.");
						Console.WriteLine("password <pass>" + '\t' + " Change password.");
						Console.WriteLine(string.Concat(new object[]
						{
							"version",
							'\t',
							'\t',
							" Print version number."
						}));
						Console.WriteLine(string.Concat(new object[]
						{
							"time",
							'\t',
							'\t',
							" Display game time."
						}));
						Console.WriteLine(string.Concat(new object[]
						{
							"port",
							'\t',
							'\t',
							" Print the listening port."
						}));
						Console.WriteLine("maxplayers" + '\t' + " Print the max number of players.");
						Console.WriteLine("say <words>" + '\t' + " Send a message.");
						Console.WriteLine(string.Concat(new object[]
						{
							"motd",
							'\t',
							'\t',
							" Print MOTD."
						}));
						Console.WriteLine("motd <words>" + '\t' + " Change MOTD.");
						Console.WriteLine(string.Concat(new object[]
						{
							"dawn",
							'\t',
							'\t',
							" Change time to dawn."
						}));
						Console.WriteLine(string.Concat(new object[]
						{
							"noon",
							'\t',
							'\t',
							" Change time to noon."
						}));
						Console.WriteLine(string.Concat(new object[]
						{
							"dusk",
							'\t',
							'\t',
							" Change time to dusk."
						}));
						Console.WriteLine("midnight" + '\t' + " Change time to midnight.");
						Console.WriteLine(string.Concat(new object[]
						{
							"settle",
							'\t',
							'\t',
							" Settle all water."
						}));
					}
					else
					{
						if (text == "settle")
						{
							if (!Liquid.panicMode)
							{
								Liquid.StartPanic();
							}
							else
							{
								Console.WriteLine("Water is already settling");
							}
						}
						else
						{
							if (text == "dawn")
							{
								Main.dayTime = true;
								Main.time = 0.0;
								NetMessage.SendData(7, -1, -1, "", 0, 0f, 0f, 0f, 0);
							}
							else
							{
								if (text == "dusk")
								{
									Main.dayTime = false;
									Main.time = 0.0;
									NetMessage.SendData(7, -1, -1, "", 0, 0f, 0f, 0f, 0);
								}
								else
								{
									if (text == "noon")
									{
										Main.dayTime = true;
										Main.time = 27000.0;
										NetMessage.SendData(7, -1, -1, "", 0, 0f, 0f, 0f, 0);
									}
									else
									{
										if (text == "midnight")
										{
											Main.dayTime = false;
											Main.time = 16200.0;
											NetMessage.SendData(7, -1, -1, "", 0, 0f, 0f, 0f, 0);
										}
										else
										{
											if (text == "exit-nosave")
											{
												Netplay.disconnect = true;
											}
											else
											{
												if (text == "exit")
												{
													WorldGen.saveWorld(false);
													Netplay.disconnect = true;
												}
												else
												{
													if (text == "save")
													{
														WorldGen.saveWorld(false);
													}
													else
													{
														if (text == "time")
														{
															string text3 = "AM";
															double num = Main.time;
															if (!Main.dayTime)
															{
																num += 54000.0;
															}
															num = num / 86400.0 * 24.0;
															double num2 = 7.5;
															num = num - num2 - 12.0;
															if (num < 0.0)
															{
																num += 24.0;
															}
															if (num >= 12.0)
															{
																text3 = "PM";
															}
															int num3 = (int)num;
															double num4 = num - (double)num3;
															num4 = (double)((int)(num4 * 60.0));
															string text4 = string.Concat(num4);
															if (num4 < 10.0)
															{
																text4 = "0" + text4;
															}
															if (num3 > 12)
															{
																num3 -= 12;
															}
															if (num3 == 0)
															{
																num3 = 12;
															}
															Console.WriteLine(string.Concat(new object[]
															{
																"Time: ",
																num3,
																":",
																text4,
																" ",
																text3
															}));
														}
														else
														{
															if (text == "maxplayers")
															{
																Console.WriteLine("Player limit: " + Main.maxNetPlayers);
															}
															else
															{
																if (text == "port")
																{
																	Console.WriteLine("Port: " + Netplay.serverPort);
																}
																else
																{
																	if (text == "version")
																	{
																		Console.WriteLine("Terraria Server " + Main.versionNumber);
																	}
																	else
																	{
																		if (text == "clear")
																		{
																			try
																			{
																				Console.Clear();
																				continue;
																			}
																			catch
																			{
																				continue;
																			}
																		}
																		if (text == "playing")
																		{
																			int num5 = 0;
																			for (int i = 0; i < 255; i++)
																			{
																				if (Main.player[i].active)
																				{
																					num5++;
																					Console.WriteLine(string.Concat(new object[]
																					{
																						Main.player[i].name,
																						" (",
																						Netplay.serverSock[i].tcpClient.Client.RemoteEndPoint,
																						")"
																					}));
																				}
																			}
																			if (num5 == 0)
																			{
																				Console.WriteLine("No players connected.");
																			}
																			else
																			{
																				if (num5 == 1)
																				{
																					Console.WriteLine("1 player connected.");
																				}
																				else
																				{
																					Console.WriteLine(num5 + " players connected.");
																				}
																			}
																		}
																		else
																		{
																			if (!(text == ""))
																			{
																				if (text == "motd")
																				{
																					if (Main.motd == "")
																					{
																						Console.WriteLine("Welcome to " + Main.worldName + "!");
																					}
																					else
																					{
																						Console.WriteLine("MOTD: " + Main.motd);
																					}
																				}
																				else
																				{
																					if (text.Length >= 5 && text.Substring(0, 5) == "motd ")
																					{
																						string text5 = text2.Substring(5);
																						Main.motd = text5;
																					}
																					else
																					{
																						if (text.Length == 8 && text.Substring(0, 8) == "password")
																						{
																							if (Netplay.password == "")
																							{
																								Console.WriteLine("No password set.");
																							}
																							else
																							{
																								Console.WriteLine("Password: " + Netplay.password);
																							}
																						}
																						else
																						{
																							if (text.Length >= 9 && text.Substring(0, 9) == "password ")
																							{
																								string text6 = text2.Substring(9);
																								if (text6 == "")
																								{
																									Netplay.password = "";
																									Console.WriteLine("Password disabled.");
																								}
																								else
																								{
																									Netplay.password = text6;
																									Console.WriteLine("Password: " + Netplay.password);
																								}
																							}
																							else
																							{
																								if (text == "say")
																								{
																									Console.WriteLine("Usage: say <words>");
																								}
																								else
																								{
																									if (text.Length >= 4 && text.Substring(0, 4) == "say ")
																									{
																										string text7 = text2.Substring(4);
																										if (text7 == "")
																										{
																											Console.WriteLine("Usage: say <words>");
																										}
																										else
																										{
																											Console.WriteLine("<Server> " + text7);
																											NetMessage.SendData(25, -1, -1, "<Server> " + text7, 255, 255f, 240f, 20f, 0);
																										}
																									}
																									else
																									{
																										if (text.Length == 4 && text.Substring(0, 4) == "kick")
																										{
																											Console.WriteLine("Usage: kick <player>");
																										}
																										else
																										{
																											if (text.Length >= 5 && text.Substring(0, 5) == "kick ")
																											{
																												string text8 = text.Substring(5);
																												text8 = text8.ToLower();
																												if (text8 == "")
																												{
																													Console.WriteLine("Usage: kick <player>");
																												}
																												else
																												{
																													for (int j = 0; j < 255; j++)
																													{
																														if (Main.player[j].active && Main.player[j].name.ToLower() == text8)
																														{
																															NetMessage.SendData(2, j, -1, "Kicked from server.", 0, 0f, 0f, 0f, 0);
																														}
																													}
																												}
																											}
																											else
																											{
																												if (text.Length == 3 && text.Substring(0, 3) == "ban")
																												{
																													Console.WriteLine("Usage: ban <player>");
																												}
																												else
																												{
																													if (text.Length >= 4 && text.Substring(0, 4) == "ban ")
																													{
																														string text9 = text.Substring(4);
																														text9 = text9.ToLower();
																														if (text9 == "")
																														{
																															Console.WriteLine("Usage: ban <player>");
																														}
																														else
																														{
																															for (int k = 0; k < 255; k++)
																															{
																																if (Main.player[k].active && Main.player[k].name.ToLower() == text9)
																																{
																																	Netplay.AddBan(k);
																																	NetMessage.SendData(2, k, -1, "Banned from server.", 0, 0f, 0f, 0f, 0);
																																}
																															}
																														}
																													}
																													else
																													{
																														Console.WriteLine("Invalid command.");
																													}
																												}
																											}
																										}
																									}
																								}
																							}
																						}
																					}
																				}
																			}
																		}
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
				catch
				{
					Console.WriteLine("Invalid command.");
				}
			}
		}
		public Main()
		{
			this.graphics = new GraphicsDeviceManager(this);
			base.Content.RootDirectory = "Content";
		}
		protected void SetTitle()
		{
			base.Window.Title = Lang.title();
		}
		protected override void Initialize()
		{
			NPC.clrNames();
			NPC.setNames();
			Main.bgAlpha[0] = 1f;
			Main.bgAlpha2[0] = 1f;
			for (int i = 0; i < 112; i++)
			{
				Main.projFrames[i] = 1;
			}
			Main.projFrames[72] = 4;
			Main.projFrames[86] = 4;
			Main.projFrames[87] = 4;
			Main.projFrames[102] = 2;
			Main.projFrames[111] = 8;
			Main.pvpBuff[20] = true;
			Main.pvpBuff[24] = true;
			Main.pvpBuff[31] = true;
			Main.pvpBuff[39] = true;
			Main.debuff[20] = true;
			Main.debuff[21] = true;
			Main.debuff[22] = true;
			Main.debuff[23] = true;
			Main.debuff[24] = true;
			Main.debuff[25] = true;
			Main.debuff[28] = true;
			Main.debuff[30] = true;
			Main.debuff[31] = true;
			Main.debuff[32] = true;
			Main.debuff[33] = true;
			Main.debuff[34] = true;
			Main.debuff[35] = true;
			Main.debuff[36] = true;
			Main.debuff[37] = true;
			Main.debuff[38] = true;
			Main.debuff[39] = true;
			for (int j = 0; j < 10; j++)
			{
				Main.recentWorld[j] = "";
				Main.recentIP[j] = "";
				Main.recentPort[j] = 0;
			}
			if (Main.rand == null)
			{
				Main.rand = new Random((int)DateTime.Now.Ticks);
			}
			if (WorldGen.genRand == null)
			{
				WorldGen.genRand = new Random((int)DateTime.Now.Ticks);
			}
			this.SetTitle();
			Main.lo = Main.rand.Next(6);
			Main.tileShine2[6] = true;
			Main.tileShine2[7] = true;
			Main.tileShine2[8] = true;
			Main.tileShine2[9] = true;
			Main.tileShine2[12] = true;
			Main.tileShine2[21] = true;
			Main.tileShine2[22] = true;
			Main.tileShine2[25] = true;
			Main.tileShine2[45] = true;
			Main.tileShine2[46] = true;
			Main.tileShine2[47] = true;
			Main.tileShine2[63] = true;
			Main.tileShine2[64] = true;
			Main.tileShine2[65] = true;
			Main.tileShine2[66] = true;
			Main.tileShine2[67] = true;
			Main.tileShine2[68] = true;
			Main.tileShine2[107] = true;
			Main.tileShine2[108] = true;
			Main.tileShine2[111] = true;
			Main.tileShine2[121] = true;
			Main.tileShine2[122] = true;
			Main.tileShine2[117] = true;
			Main.tileShine[129] = 300;
			Main.tileHammer[141] = true;
			Main.tileHammer[4] = true;
			Main.tileHammer[10] = true;
			Main.tileHammer[11] = true;
			Main.tileHammer[12] = true;
			Main.tileHammer[13] = true;
			Main.tileHammer[14] = true;
			Main.tileHammer[15] = true;
			Main.tileHammer[16] = true;
			Main.tileHammer[17] = true;
			Main.tileHammer[18] = true;
			Main.tileHammer[19] = true;
			Main.tileHammer[21] = true;
			Main.tileHammer[26] = true;
			Main.tileHammer[28] = true;
			Main.tileHammer[29] = true;
			Main.tileHammer[31] = true;
			Main.tileHammer[33] = true;
			Main.tileHammer[34] = true;
			Main.tileHammer[35] = true;
			Main.tileHammer[36] = true;
			Main.tileHammer[42] = true;
			Main.tileHammer[48] = true;
			Main.tileHammer[49] = true;
			Main.tileHammer[50] = true;
			Main.tileHammer[54] = true;
			Main.tileHammer[55] = true;
			Main.tileHammer[77] = true;
			Main.tileHammer[78] = true;
			Main.tileHammer[79] = true;
			Main.tileHammer[81] = true;
			Main.tileHammer[85] = true;
			Main.tileHammer[86] = true;
			Main.tileHammer[87] = true;
			Main.tileHammer[88] = true;
			Main.tileHammer[89] = true;
			Main.tileHammer[90] = true;
			Main.tileHammer[91] = true;
			Main.tileHammer[92] = true;
			Main.tileHammer[93] = true;
			Main.tileHammer[94] = true;
			Main.tileHammer[95] = true;
			Main.tileHammer[96] = true;
			Main.tileHammer[97] = true;
			Main.tileHammer[98] = true;
			Main.tileHammer[99] = true;
			Main.tileHammer[100] = true;
			Main.tileHammer[101] = true;
			Main.tileHammer[102] = true;
			Main.tileHammer[103] = true;
			Main.tileHammer[104] = true;
			Main.tileHammer[105] = true;
			Main.tileHammer[106] = true;
			Main.tileHammer[114] = true;
			Main.tileHammer[125] = true;
			Main.tileHammer[126] = true;
			Main.tileHammer[128] = true;
			Main.tileHammer[129] = true;
			Main.tileHammer[132] = true;
			Main.tileHammer[133] = true;
			Main.tileHammer[134] = true;
			Main.tileHammer[135] = true;
			Main.tileHammer[136] = true;
			Main.tileFrameImportant[139] = true;
			Main.tileHammer[139] = true;
			Main.tileLighted[149] = true;
			Main.tileFrameImportant[149] = true;
			Main.tileHammer[149] = true;
			Main.tileFrameImportant[142] = true;
			Main.tileHammer[142] = true;
			Main.tileFrameImportant[143] = true;
			Main.tileHammer[143] = true;
			Main.tileFrameImportant[144] = true;
			Main.tileHammer[144] = true;
			Main.tileStone[131] = true;
			Main.tileFrameImportant[136] = true;
			Main.tileFrameImportant[137] = true;
			Main.tileFrameImportant[138] = true;
			Main.tileBlockLight[137] = true;
			Main.tileSolid[137] = true;
			Main.tileBlockLight[145] = true;
			Main.tileSolid[145] = true;
			Main.tileMergeDirt[145] = true;
			Main.tileBlockLight[146] = true;
			Main.tileSolid[146] = true;
			Main.tileMergeDirt[146] = true;
			Main.tileBlockLight[147] = true;
			Main.tileSolid[147] = true;
			Main.tileMergeDirt[147] = true;
			Main.tileBlockLight[148] = true;
			Main.tileSolid[148] = true;
			Main.tileMergeDirt[148] = true;
			Main.tileBlockLight[138] = true;
			Main.tileSolid[138] = true;
			Main.tileBlockLight[140] = true;
			Main.tileSolid[140] = true;
			Main.tileAxe[5] = true;
			Main.tileAxe[30] = true;
			Main.tileAxe[72] = true;
			Main.tileAxe[80] = true;
			Main.tileAxe[124] = true;
			Main.tileShine[22] = 1150;
			Main.tileShine[6] = 1150;
			Main.tileShine[7] = 1100;
			Main.tileShine[8] = 1000;
			Main.tileShine[9] = 1050;
			Main.tileShine[12] = 1000;
			Main.tileShine[21] = 1200;
			Main.tileShine[63] = 900;
			Main.tileShine[64] = 900;
			Main.tileShine[65] = 900;
			Main.tileShine[66] = 900;
			Main.tileShine[67] = 900;
			Main.tileShine[68] = 900;
			Main.tileShine[45] = 1900;
			Main.tileShine[46] = 2000;
			Main.tileShine[47] = 2100;
			Main.tileShine[122] = 1800;
			Main.tileShine[121] = 1850;
			Main.tileShine[125] = 600;
			Main.tileShine[109] = 9000;
			Main.tileShine[110] = 9000;
			Main.tileShine[116] = 9000;
			Main.tileShine[117] = 9000;
			Main.tileShine[118] = 8000;
			Main.tileShine[107] = 950;
			Main.tileShine[108] = 900;
			Main.tileShine[111] = 850;
			Main.tileLighted[4] = true;
			Main.tileLighted[17] = true;
			Main.tileLighted[133] = true;
			Main.tileLighted[31] = true;
			Main.tileLighted[33] = true;
			Main.tileLighted[34] = true;
			Main.tileLighted[35] = true;
			Main.tileLighted[36] = true;
			Main.tileLighted[37] = true;
			Main.tileLighted[42] = true;
			Main.tileLighted[49] = true;
			Main.tileLighted[58] = true;
			Main.tileLighted[61] = true;
			Main.tileLighted[70] = true;
			Main.tileLighted[71] = true;
			Main.tileLighted[72] = true;
			Main.tileLighted[76] = true;
			Main.tileLighted[77] = true;
			Main.tileLighted[19] = true;
			Main.tileLighted[22] = true;
			Main.tileLighted[26] = true;
			Main.tileLighted[83] = true;
			Main.tileLighted[84] = true;
			Main.tileLighted[92] = true;
			Main.tileLighted[93] = true;
			Main.tileLighted[95] = true;
			Main.tileLighted[98] = true;
			Main.tileLighted[100] = true;
			Main.tileLighted[109] = true;
			Main.tileLighted[125] = true;
			Main.tileLighted[126] = true;
			Main.tileLighted[129] = true;
			Main.tileLighted[140] = true;
			Main.tileMergeDirt[1] = true;
			Main.tileMergeDirt[6] = true;
			Main.tileMergeDirt[7] = true;
			Main.tileMergeDirt[8] = true;
			Main.tileMergeDirt[9] = true;
			Main.tileMergeDirt[22] = true;
			Main.tileMergeDirt[25] = true;
			Main.tileMergeDirt[30] = true;
			Main.tileMergeDirt[37] = true;
			Main.tileMergeDirt[38] = true;
			Main.tileMergeDirt[40] = true;
			Main.tileMergeDirt[53] = true;
			Main.tileMergeDirt[56] = true;
			Main.tileMergeDirt[107] = true;
			Main.tileMergeDirt[108] = true;
			Main.tileMergeDirt[111] = true;
			Main.tileMergeDirt[112] = true;
			Main.tileMergeDirt[116] = true;
			Main.tileMergeDirt[117] = true;
			Main.tileMergeDirt[123] = true;
			Main.tileMergeDirt[140] = true;
			Main.tileMergeDirt[39] = true;
			Main.tileMergeDirt[122] = true;
			Main.tileMergeDirt[121] = true;
			Main.tileMergeDirt[120] = true;
			Main.tileMergeDirt[119] = true;
			Main.tileMergeDirt[118] = true;
			Main.tileMergeDirt[47] = true;
			Main.tileMergeDirt[46] = true;
			Main.tileMergeDirt[45] = true;
			Main.tileMergeDirt[44] = true;
			Main.tileMergeDirt[43] = true;
			Main.tileMergeDirt[41] = true;
			Main.tileFrameImportant[3] = true;
			Main.tileFrameImportant[4] = true;
			Main.tileFrameImportant[5] = true;
			Main.tileFrameImportant[10] = true;
			Main.tileFrameImportant[11] = true;
			Main.tileFrameImportant[12] = true;
			Main.tileFrameImportant[13] = true;
			Main.tileFrameImportant[14] = true;
			Main.tileFrameImportant[15] = true;
			Main.tileFrameImportant[16] = true;
			Main.tileFrameImportant[17] = true;
			Main.tileFrameImportant[18] = true;
			Main.tileFrameImportant[20] = true;
			Main.tileFrameImportant[21] = true;
			Main.tileFrameImportant[24] = true;
			Main.tileFrameImportant[26] = true;
			Main.tileFrameImportant[27] = true;
			Main.tileFrameImportant[28] = true;
			Main.tileFrameImportant[29] = true;
			Main.tileFrameImportant[31] = true;
			Main.tileFrameImportant[33] = true;
			Main.tileFrameImportant[34] = true;
			Main.tileFrameImportant[35] = true;
			Main.tileFrameImportant[36] = true;
			Main.tileFrameImportant[42] = true;
			Main.tileFrameImportant[50] = true;
			Main.tileFrameImportant[55] = true;
			Main.tileFrameImportant[61] = true;
			Main.tileFrameImportant[71] = true;
			Main.tileFrameImportant[72] = true;
			Main.tileFrameImportant[73] = true;
			Main.tileFrameImportant[74] = true;
			Main.tileFrameImportant[77] = true;
			Main.tileFrameImportant[78] = true;
			Main.tileFrameImportant[79] = true;
			Main.tileFrameImportant[81] = true;
			Main.tileFrameImportant[82] = true;
			Main.tileFrameImportant[83] = true;
			Main.tileFrameImportant[84] = true;
			Main.tileFrameImportant[85] = true;
			Main.tileFrameImportant[86] = true;
			Main.tileFrameImportant[87] = true;
			Main.tileFrameImportant[88] = true;
			Main.tileFrameImportant[89] = true;
			Main.tileFrameImportant[90] = true;
			Main.tileFrameImportant[91] = true;
			Main.tileFrameImportant[92] = true;
			Main.tileFrameImportant[93] = true;
			Main.tileFrameImportant[94] = true;
			Main.tileFrameImportant[95] = true;
			Main.tileFrameImportant[96] = true;
			Main.tileFrameImportant[97] = true;
			Main.tileFrameImportant[98] = true;
			Main.tileFrameImportant[99] = true;
			Main.tileFrameImportant[101] = true;
			Main.tileFrameImportant[102] = true;
			Main.tileFrameImportant[103] = true;
			Main.tileFrameImportant[104] = true;
			Main.tileFrameImportant[105] = true;
			Main.tileFrameImportant[100] = true;
			Main.tileFrameImportant[106] = true;
			Main.tileFrameImportant[110] = true;
			Main.tileFrameImportant[113] = true;
			Main.tileFrameImportant[114] = true;
			Main.tileFrameImportant[125] = true;
			Main.tileFrameImportant[126] = true;
			Main.tileFrameImportant[128] = true;
			Main.tileFrameImportant[129] = true;
			Main.tileFrameImportant[132] = true;
			Main.tileFrameImportant[133] = true;
			Main.tileFrameImportant[134] = true;
			Main.tileFrameImportant[135] = true;
			Main.tileFrameImportant[141] = true;
			Main.tileCut[3] = true;
			Main.tileCut[24] = true;
			Main.tileCut[28] = true;
			Main.tileCut[32] = true;
			Main.tileCut[51] = true;
			Main.tileCut[52] = true;
			Main.tileCut[61] = true;
			Main.tileCut[62] = true;
			Main.tileCut[69] = true;
			Main.tileCut[71] = true;
			Main.tileCut[73] = true;
			Main.tileCut[74] = true;
			Main.tileCut[82] = true;
			Main.tileCut[83] = true;
			Main.tileCut[84] = true;
			Main.tileCut[110] = true;
			Main.tileCut[113] = true;
			Main.tileCut[115] = true;
			Main.tileAlch[82] = true;
			Main.tileAlch[83] = true;
			Main.tileAlch[84] = true;
			Main.tileLavaDeath[104] = true;
			Main.tileLavaDeath[110] = true;
			Main.tileLavaDeath[113] = true;
			Main.tileLavaDeath[115] = true;
			Main.tileSolid[127] = true;
			Main.tileSolid[130] = true;
			Main.tileBlockLight[130] = true;
			Main.tileBlockLight[131] = true;
			Main.tileSolid[107] = true;
			Main.tileBlockLight[107] = true;
			Main.tileSolid[108] = true;
			Main.tileBlockLight[108] = true;
			Main.tileSolid[111] = true;
			Main.tileBlockLight[111] = true;
			Main.tileSolid[109] = true;
			Main.tileBlockLight[109] = true;
			Main.tileSolid[110] = false;
			Main.tileNoAttach[110] = true;
			Main.tileNoFail[110] = true;
			Main.tileSolid[112] = true;
			Main.tileBlockLight[112] = true;
			Main.tileSolid[116] = true;
			Main.tileBlockLight[116] = true;
			Main.tileSolid[117] = true;
			Main.tileBlockLight[117] = true;
			Main.tileSolid[123] = true;
			Main.tileBlockLight[123] = true;
			Main.tileSolid[118] = true;
			Main.tileBlockLight[118] = true;
			Main.tileSolid[119] = true;
			Main.tileBlockLight[119] = true;
			Main.tileSolid[120] = true;
			Main.tileBlockLight[120] = true;
			Main.tileSolid[121] = true;
			Main.tileBlockLight[121] = true;
			Main.tileSolid[122] = true;
			Main.tileBlockLight[122] = true;
			Main.tileBlockLight[115] = true;
			Main.tileSolid[0] = true;
			Main.tileBlockLight[0] = true;
			Main.tileSolid[1] = true;
			Main.tileBlockLight[1] = true;
			Main.tileSolid[2] = true;
			Main.tileBlockLight[2] = true;
			Main.tileSolid[3] = false;
			Main.tileNoAttach[3] = true;
			Main.tileNoFail[3] = true;
			Main.tileSolid[4] = false;
			Main.tileNoAttach[4] = true;
			Main.tileNoFail[4] = true;
			Main.tileNoFail[24] = true;
			Main.tileSolid[5] = false;
			Main.tileSolid[6] = true;
			Main.tileBlockLight[6] = true;
			Main.tileSolid[7] = true;
			Main.tileBlockLight[7] = true;
			Main.tileSolid[8] = true;
			Main.tileBlockLight[8] = true;
			Main.tileSolid[9] = true;
			Main.tileBlockLight[9] = true;
			Main.tileBlockLight[10] = true;
			Main.tileSolid[10] = true;
			Main.tileNoAttach[10] = true;
			Main.tileBlockLight[10] = true;
			Main.tileSolid[11] = false;
			Main.tileSolidTop[19] = true;
			Main.tileSolid[19] = true;
			Main.tileSolid[22] = true;
			Main.tileSolid[23] = true;
			Main.tileSolid[25] = true;
			Main.tileSolid[30] = true;
			Main.tileNoFail[32] = true;
			Main.tileBlockLight[32] = true;
			Main.tileSolid[37] = true;
			Main.tileBlockLight[37] = true;
			Main.tileSolid[38] = true;
			Main.tileBlockLight[38] = true;
			Main.tileSolid[39] = true;
			Main.tileBlockLight[39] = true;
			Main.tileSolid[40] = true;
			Main.tileBlockLight[40] = true;
			Main.tileSolid[41] = true;
			Main.tileBlockLight[41] = true;
			Main.tileSolid[43] = true;
			Main.tileBlockLight[43] = true;
			Main.tileSolid[44] = true;
			Main.tileBlockLight[44] = true;
			Main.tileSolid[45] = true;
			Main.tileBlockLight[45] = true;
			Main.tileSolid[46] = true;
			Main.tileBlockLight[46] = true;
			Main.tileSolid[47] = true;
			Main.tileBlockLight[47] = true;
			Main.tileSolid[48] = true;
			Main.tileBlockLight[48] = true;
			Main.tileSolid[53] = true;
			Main.tileBlockLight[53] = true;
			Main.tileSolid[54] = true;
			Main.tileBlockLight[52] = true;
			Main.tileSolid[56] = true;
			Main.tileBlockLight[56] = true;
			Main.tileSolid[57] = true;
			Main.tileBlockLight[57] = true;
			Main.tileSolid[58] = true;
			Main.tileBlockLight[58] = true;
			Main.tileSolid[59] = true;
			Main.tileBlockLight[59] = true;
			Main.tileSolid[60] = true;
			Main.tileBlockLight[60] = true;
			Main.tileSolid[63] = true;
			Main.tileBlockLight[63] = true;
			Main.tileStone[63] = true;
			Main.tileStone[130] = true;
			Main.tileSolid[64] = true;
			Main.tileBlockLight[64] = true;
			Main.tileStone[64] = true;
			Main.tileSolid[65] = true;
			Main.tileBlockLight[65] = true;
			Main.tileStone[65] = true;
			Main.tileSolid[66] = true;
			Main.tileBlockLight[66] = true;
			Main.tileStone[66] = true;
			Main.tileSolid[67] = true;
			Main.tileBlockLight[67] = true;
			Main.tileStone[67] = true;
			Main.tileSolid[68] = true;
			Main.tileBlockLight[68] = true;
			Main.tileStone[68] = true;
			Main.tileSolid[75] = true;
			Main.tileBlockLight[75] = true;
			Main.tileSolid[76] = true;
			Main.tileBlockLight[76] = true;
			Main.tileSolid[70] = true;
			Main.tileBlockLight[70] = true;
			Main.tileNoFail[50] = true;
			Main.tileNoAttach[50] = true;
			Main.tileDungeon[41] = true;
			Main.tileDungeon[43] = true;
			Main.tileDungeon[44] = true;
			Main.tileBlockLight[30] = true;
			Main.tileBlockLight[25] = true;
			Main.tileBlockLight[23] = true;
			Main.tileBlockLight[22] = true;
			Main.tileBlockLight[62] = true;
			Main.tileSolidTop[18] = true;
			Main.tileSolidTop[14] = true;
			Main.tileSolidTop[16] = true;
			Main.tileSolidTop[114] = true;
			Main.tileNoAttach[20] = true;
			Main.tileNoAttach[19] = true;
			Main.tileNoAttach[13] = true;
			Main.tileNoAttach[14] = true;
			Main.tileNoAttach[15] = true;
			Main.tileNoAttach[16] = true;
			Main.tileNoAttach[17] = true;
			Main.tileNoAttach[18] = true;
			Main.tileNoAttach[19] = true;
			Main.tileNoAttach[21] = true;
			Main.tileNoAttach[27] = true;
			Main.tileNoAttach[114] = true;
			Main.tileTable[14] = true;
			Main.tileTable[18] = true;
			Main.tileTable[19] = true;
			Main.tileTable[114] = true;
			Main.tileNoAttach[86] = true;
			Main.tileNoAttach[87] = true;
			Main.tileNoAttach[88] = true;
			Main.tileNoAttach[89] = true;
			Main.tileNoAttach[90] = true;
			Main.tileLavaDeath[86] = true;
			Main.tileLavaDeath[87] = true;
			Main.tileLavaDeath[88] = true;
			Main.tileLavaDeath[89] = true;
			Main.tileLavaDeath[125] = true;
			Main.tileLavaDeath[126] = true;
			Main.tileLavaDeath[101] = true;
			Main.tileTable[101] = true;
			Main.tileNoAttach[101] = true;
			Main.tileLavaDeath[102] = true;
			Main.tileNoAttach[102] = true;
			Main.tileNoAttach[94] = true;
			Main.tileNoAttach[95] = true;
			Main.tileNoAttach[96] = true;
			Main.tileNoAttach[97] = true;
			Main.tileNoAttach[98] = true;
			Main.tileNoAttach[99] = true;
			Main.tileLavaDeath[94] = true;
			Main.tileLavaDeath[95] = true;
			Main.tileLavaDeath[96] = true;
			Main.tileLavaDeath[97] = true;
			Main.tileLavaDeath[98] = true;
			Main.tileLavaDeath[99] = true;
			Main.tileLavaDeath[100] = true;
			Main.tileLavaDeath[103] = true;
			Main.tileTable[87] = true;
			Main.tileTable[88] = true;
			Main.tileSolidTop[87] = true;
			Main.tileSolidTop[88] = true;
			Main.tileSolidTop[101] = true;
			Main.tileNoAttach[91] = true;
			Main.tileLavaDeath[91] = true;
			Main.tileNoAttach[92] = true;
			Main.tileLavaDeath[92] = true;
			Main.tileNoAttach[93] = true;
			Main.tileLavaDeath[93] = true;
			Main.tileWaterDeath[4] = true;
			Main.tileWaterDeath[51] = true;
			Main.tileWaterDeath[93] = true;
			Main.tileWaterDeath[98] = true;
			Main.tileLavaDeath[3] = true;
			Main.tileLavaDeath[5] = true;
			Main.tileLavaDeath[10] = true;
			Main.tileLavaDeath[11] = true;
			Main.tileLavaDeath[12] = true;
			Main.tileLavaDeath[13] = true;
			Main.tileLavaDeath[14] = true;
			Main.tileLavaDeath[15] = true;
			Main.tileLavaDeath[16] = true;
			Main.tileLavaDeath[17] = true;
			Main.tileLavaDeath[18] = true;
			Main.tileLavaDeath[19] = true;
			Main.tileLavaDeath[20] = true;
			Main.tileLavaDeath[27] = true;
			Main.tileLavaDeath[28] = true;
			Main.tileLavaDeath[29] = true;
			Main.tileLavaDeath[32] = true;
			Main.tileLavaDeath[33] = true;
			Main.tileLavaDeath[34] = true;
			Main.tileLavaDeath[35] = true;
			Main.tileLavaDeath[36] = true;
			Main.tileLavaDeath[42] = true;
			Main.tileLavaDeath[49] = true;
			Main.tileLavaDeath[50] = true;
			Main.tileLavaDeath[52] = true;
			Main.tileLavaDeath[55] = true;
			Main.tileLavaDeath[61] = true;
			Main.tileLavaDeath[62] = true;
			Main.tileLavaDeath[69] = true;
			Main.tileLavaDeath[71] = true;
			Main.tileLavaDeath[72] = true;
			Main.tileLavaDeath[73] = true;
			Main.tileLavaDeath[74] = true;
			Main.tileLavaDeath[79] = true;
			Main.tileLavaDeath[80] = true;
			Main.tileLavaDeath[81] = true;
			Main.tileLavaDeath[106] = true;
			Main.wallHouse[1] = true;
			Main.wallHouse[4] = true;
			Main.wallHouse[5] = true;
			Main.wallHouse[6] = true;
			Main.wallHouse[10] = true;
			Main.wallHouse[11] = true;
			Main.wallHouse[12] = true;
			Main.wallHouse[16] = true;
			Main.wallHouse[17] = true;
			Main.wallHouse[18] = true;
			Main.wallHouse[19] = true;
			Main.wallHouse[20] = true;
			Main.wallHouse[21] = true;
			Main.wallHouse[22] = true;
			Main.wallHouse[23] = true;
			Main.wallHouse[24] = true;
			Main.wallHouse[25] = true;
			Main.wallHouse[26] = true;
			Main.wallHouse[27] = true;
			Main.wallHouse[29] = true;
			Main.wallHouse[30] = true;
			Main.wallHouse[31] = true;
			for (int k = 0; k < 32; k++)
			{
				if (k == 20)
				{
					Main.wallBlend[k] = 14;
				}
				else
				{
					if (k == 19)
					{
						Main.wallBlend[k] = 9;
					}
					else
					{
						if (k == 18)
						{
							Main.wallBlend[k] = 8;
						}
						else
						{
							if (k == 17)
							{
								Main.wallBlend[k] = 7;
							}
							else
							{
								if (k == 16)
								{
									Main.wallBlend[k] = 2;
								}
								else
								{
									Main.wallBlend[k] = k;
								}
							}
						}
					}
				}
			}
			Main.tileNoFail[32] = true;
			Main.tileNoFail[61] = true;
			Main.tileNoFail[69] = true;
			Main.tileNoFail[73] = true;
			Main.tileNoFail[74] = true;
			Main.tileNoFail[82] = true;
			Main.tileNoFail[83] = true;
			Main.tileNoFail[84] = true;
			Main.tileNoFail[110] = true;
			Main.tileNoFail[113] = true;
			for (int l = 0; l < 150; l++)
			{
				Main.tileName[l] = "";
				if (Main.tileSolid[l])
				{
					Main.tileNoSunLight[l] = true;
				}
			}
			Main.tileNoSunLight[19] = false;
			Main.tileNoSunLight[11] = true;
			for (int m = 0; m < Main.maxMenuItems; m++)
			{
				this.menuItemScale[m] = 0.8f;
			}
			for (int n = 0; n < 2001; n++)
			{
				Main.dust[n] = new Dust();
			}
			for (int num = 0; num < 201; num++)
			{
				Main.item[num] = new Item();
			}
			for (int num2 = 0; num2 < 201; num2++)
			{
				Main.npc[num2] = new NPC();
				Main.npc[num2].whoAmI = num2;
			}
			for (int num3 = 0; num3 < 256; num3++)
			{
				Main.player[num3] = new Player();
			}
			for (int num4 = 0; num4 < 1001; num4++)
			{
				Main.projectile[num4] = new Projectile();
			}
			for (int num5 = 0; num5 < 201; num5++)
			{
				Main.gore[num5] = new Gore();
			}
			for (int num6 = 0; num6 < 100; num6++)
			{
				Main.cloud[num6] = new Cloud();
			}
			for (int num7 = 0; num7 < 100; num7++)
			{
				Main.combatText[num7] = new CombatText();
			}
			for (int num8 = 0; num8 < 20; num8++)
			{
				Main.itemText[num8] = new ItemText();
			}
			for (int num9 = 0; num9 < 604; num9++)
			{
				Item item = new Item();
				item.SetDefaults(num9, false);
				Main.itemName[num9] = item.name;
				if (item.headSlot > 0)
				{
					Item.headType[item.headSlot] = item.type;
				}
				if (item.bodySlot > 0)
				{
					Item.bodyType[item.bodySlot] = item.type;
				}
				if (item.legSlot > 0)
				{
					Item.legType[item.legSlot] = item.type;
				}
			}
			for (int num10 = 0; num10 < Recipe.maxRecipes; num10++)
			{
				Main.recipe[num10] = new Recipe();
				Main.availableRecipeY[num10] = (float)(65 * num10);
			}
			Recipe.SetupRecipes();
			for (int num11 = 0; num11 < Main.numChatLines; num11++)
			{
				Main.chatLine[num11] = new ChatLine();
			}
			for (int num12 = 0; num12 < Liquid.resLiquid; num12++)
			{
				Main.liquid[num12] = new Liquid();
			}
			for (int num13 = 0; num13 < 10000; num13++)
			{
				Main.liquidBuffer[num13] = new LiquidBuffer();
			}
			this.shop[0] = new Chest();
			this.shop[1] = new Chest();
			this.shop[1].SetupShop(1);
			this.shop[2] = new Chest();
			this.shop[2].SetupShop(2);
			this.shop[3] = new Chest();
			this.shop[3].SetupShop(3);
			this.shop[4] = new Chest();
			this.shop[4].SetupShop(4);
			this.shop[5] = new Chest();
			this.shop[5].SetupShop(5);
			this.shop[6] = new Chest();
			this.shop[6].SetupShop(6);
			this.shop[7] = new Chest();
			this.shop[7].SetupShop(7);
			this.shop[8] = new Chest();
			this.shop[8].SetupShop(8);
			this.shop[9] = new Chest();
			this.shop[9].SetupShop(9);
			Main.teamColor[0] = Color.White;
			Main.teamColor[1] = new Color(230, 40, 20);
			Main.teamColor[2] = new Color(20, 200, 30);
			Main.teamColor[3] = new Color(75, 90, 255);
			Main.teamColor[4] = new Color(200, 180, 0);
			if (Main.menuMode == 1)
			{
				Main.LoadPlayers();
			}
			for (int num14 = 1; num14 < 112; num14++)
			{
				Projectile projectile = new Projectile();
				projectile.SetDefaults(num14);
				if (projectile.hostile)
				{
					Main.projHostile[num14] = true;
				}
			}
			Netplay.Init();
			if (Main.skipMenu)
			{
				WorldGen.clearWorld();
				Main.gameMenu = false;
				Main.LoadPlayers();
				Main.player[Main.myPlayer] = (Player)Main.loadPlayer[0].Clone();
				Main.PlayerPath = Main.loadPlayerPath[0];
				Main.LoadWorlds();
				WorldGen.generateWorld(-1);
				WorldGen.EveryTileFrame();
				Main.player[Main.myPlayer].Spawn();
			}
			else
			{
				IntPtr systemMenu = Main.GetSystemMenu(base.Window.Handle, false);
				int menuItemCount = Main.GetMenuItemCount(systemMenu);
				Main.RemoveMenu(systemMenu, menuItemCount - 1, 1024);
			}
			if (Main.dedServ)
			{
				return;
			}
			keyBoardInput.newKeyEvent += delegate(char keyStroke)
			{
				if (Main.keyCount < 10)
				{
					Main.keyInt[Main.keyCount] = (int)keyStroke;
					Main.keyString[Main.keyCount] = string.Concat(keyStroke);
					Main.keyCount++;
				}
			};
			this.graphics.PreferredBackBufferWidth = Main.screenWidth;
			this.graphics.PreferredBackBufferHeight = Main.screenHeight;
			this.graphics.ApplyChanges();
			base.Initialize();
			base.Window.AllowUserResizing = true;
			this.OpenSettings();
			this.CheckBunny();
			Lang.setLang();
			if (Lang.lang == 0)
			{
				Main.menuMode = 1212;
			}
			this.SetTitle();
			this.OpenRecent();
			Star.SpawnStars();
			foreach (DisplayMode current in GraphicsAdapter.DefaultAdapter.SupportedDisplayModes)
			{
				if (current.Width >= Main.minScreenW && current.Height >= Main.minScreenH && current.Width <= Main.maxScreenW && current.Height <= Main.maxScreenH)
				{
					bool flag = true;
					for (int num15 = 0; num15 < this.numDisplayModes; num15++)
					{
						if (current.Width == this.displayWidth[num15] && current.Height == this.displayHeight[num15])
						{
							flag = false;
							break;
						}
					}
					if (flag)
					{
						this.displayHeight[this.numDisplayModes] = current.Height;
						this.displayWidth[this.numDisplayModes] = current.Width;
						this.numDisplayModes++;
					}
				}
			}
			if (Main.autoJoin)
			{
				Main.LoadPlayers();
				Main.menuMode = 1;
				Main.menuMultiplayer = true;
			}
			Main.fpsTimer.Start();
			Main.updateTimer.Start();
		}
		protected override void LoadContent()
		{
			try
			{
				Main.engine = new AudioEngine("Content" + Path.DirectorySeparatorChar + "TerrariaMusic.xgs");
				Main.soundBank = new SoundBank(Main.engine, "Content" + Path.DirectorySeparatorChar + "Sound Bank.xsb");
				Main.waveBank = new WaveBank(Main.engine, "Content" + Path.DirectorySeparatorChar + "Wave Bank.xwb");
				for (int i = 1; i < 14; i++)
				{
					Main.music[i] = Main.soundBank.GetCue("Music_" + i);
				}
				Main.soundMech[0] = base.Content.Load<SoundEffect>("Sounds" + Path.DirectorySeparatorChar + "Mech_0");
				Main.soundInstanceMech[0] = Main.soundMech[0].CreateInstance();
				Main.soundGrab = base.Content.Load<SoundEffect>("Sounds" + Path.DirectorySeparatorChar + "Grab");
				Main.soundInstanceGrab = Main.soundGrab.CreateInstance();
				Main.soundPixie = base.Content.Load<SoundEffect>("Sounds" + Path.DirectorySeparatorChar + "Pixie");
				Main.soundInstancePixie = Main.soundGrab.CreateInstance();
				Main.soundDig[0] = base.Content.Load<SoundEffect>("Sounds" + Path.DirectorySeparatorChar + "Dig_0");
				Main.soundInstanceDig[0] = Main.soundDig[0].CreateInstance();
				Main.soundDig[1] = base.Content.Load<SoundEffect>("Sounds" + Path.DirectorySeparatorChar + "Dig_1");
				Main.soundInstanceDig[1] = Main.soundDig[1].CreateInstance();
				Main.soundDig[2] = base.Content.Load<SoundEffect>("Sounds" + Path.DirectorySeparatorChar + "Dig_2");
				Main.soundInstanceDig[2] = Main.soundDig[2].CreateInstance();
				Main.soundTink[0] = base.Content.Load<SoundEffect>("Sounds" + Path.DirectorySeparatorChar + "Tink_0");
				Main.soundInstanceTink[0] = Main.soundTink[0].CreateInstance();
				Main.soundTink[1] = base.Content.Load<SoundEffect>("Sounds" + Path.DirectorySeparatorChar + "Tink_1");
				Main.soundInstanceTink[1] = Main.soundTink[1].CreateInstance();
				Main.soundTink[2] = base.Content.Load<SoundEffect>("Sounds" + Path.DirectorySeparatorChar + "Tink_2");
				Main.soundInstanceTink[2] = Main.soundTink[2].CreateInstance();
				Main.soundPlayerHit[0] = base.Content.Load<SoundEffect>("Sounds" + Path.DirectorySeparatorChar + "Player_Hit_0");
				Main.soundInstancePlayerHit[0] = Main.soundPlayerHit[0].CreateInstance();
				Main.soundPlayerHit[1] = base.Content.Load<SoundEffect>("Sounds" + Path.DirectorySeparatorChar + "Player_Hit_1");
				Main.soundInstancePlayerHit[1] = Main.soundPlayerHit[1].CreateInstance();
				Main.soundPlayerHit[2] = base.Content.Load<SoundEffect>("Sounds" + Path.DirectorySeparatorChar + "Player_Hit_2");
				Main.soundInstancePlayerHit[2] = Main.soundPlayerHit[2].CreateInstance();
				Main.soundFemaleHit[0] = base.Content.Load<SoundEffect>("Sounds" + Path.DirectorySeparatorChar + "Female_Hit_0");
				Main.soundInstanceFemaleHit[0] = Main.soundFemaleHit[0].CreateInstance();
				Main.soundFemaleHit[1] = base.Content.Load<SoundEffect>("Sounds" + Path.DirectorySeparatorChar + "Female_Hit_1");
				Main.soundInstanceFemaleHit[1] = Main.soundFemaleHit[1].CreateInstance();
				Main.soundFemaleHit[2] = base.Content.Load<SoundEffect>("Sounds" + Path.DirectorySeparatorChar + "Female_Hit_2");
				Main.soundInstanceFemaleHit[2] = Main.soundFemaleHit[2].CreateInstance();
				Main.soundPlayerKilled = base.Content.Load<SoundEffect>("Sounds" + Path.DirectorySeparatorChar + "Player_Killed");
				Main.soundInstancePlayerKilled = Main.soundPlayerKilled.CreateInstance();
				Main.soundChat = base.Content.Load<SoundEffect>("Sounds" + Path.DirectorySeparatorChar + "Chat");
				Main.soundInstanceChat = Main.soundChat.CreateInstance();
				Main.soundGrass = base.Content.Load<SoundEffect>("Sounds" + Path.DirectorySeparatorChar + "Grass");
				Main.soundInstanceGrass = Main.soundGrass.CreateInstance();
				Main.soundDoorOpen = base.Content.Load<SoundEffect>("Sounds" + Path.DirectorySeparatorChar + "Door_Opened");
				Main.soundInstanceDoorOpen = Main.soundDoorOpen.CreateInstance();
				Main.soundDoorClosed = base.Content.Load<SoundEffect>("Sounds" + Path.DirectorySeparatorChar + "Door_Closed");
				Main.soundInstanceDoorClosed = Main.soundDoorClosed.CreateInstance();
				Main.soundMenuTick = base.Content.Load<SoundEffect>("Sounds" + Path.DirectorySeparatorChar + "Menu_Tick");
				Main.soundInstanceMenuTick = Main.soundMenuTick.CreateInstance();
				Main.soundMenuOpen = base.Content.Load<SoundEffect>("Sounds" + Path.DirectorySeparatorChar + "Menu_Open");
				Main.soundInstanceMenuOpen = Main.soundMenuOpen.CreateInstance();
				Main.soundMenuClose = base.Content.Load<SoundEffect>("Sounds" + Path.DirectorySeparatorChar + "Menu_Close");
				Main.soundInstanceMenuClose = Main.soundMenuClose.CreateInstance();
				Main.soundShatter = base.Content.Load<SoundEffect>("Sounds" + Path.DirectorySeparatorChar + "Shatter");
				Main.soundInstanceShatter = Main.soundShatter.CreateInstance();
				Main.soundZombie[0] = base.Content.Load<SoundEffect>("Sounds" + Path.DirectorySeparatorChar + "Zombie_0");
				Main.soundInstanceZombie[0] = Main.soundZombie[0].CreateInstance();
				Main.soundZombie[1] = base.Content.Load<SoundEffect>("Sounds" + Path.DirectorySeparatorChar + "Zombie_1");
				Main.soundInstanceZombie[1] = Main.soundZombie[1].CreateInstance();
				Main.soundZombie[2] = base.Content.Load<SoundEffect>("Sounds" + Path.DirectorySeparatorChar + "Zombie_2");
				Main.soundInstanceZombie[2] = Main.soundZombie[2].CreateInstance();
				Main.soundZombie[3] = base.Content.Load<SoundEffect>("Sounds" + Path.DirectorySeparatorChar + "Zombie_3");
				Main.soundInstanceZombie[3] = Main.soundZombie[3].CreateInstance();
				Main.soundZombie[4] = base.Content.Load<SoundEffect>("Sounds" + Path.DirectorySeparatorChar + "Zombie_4");
				Main.soundInstanceZombie[4] = Main.soundZombie[4].CreateInstance();
				Main.soundRoar[0] = base.Content.Load<SoundEffect>("Sounds" + Path.DirectorySeparatorChar + "Roar_0");
				Main.soundInstanceRoar[0] = Main.soundRoar[0].CreateInstance();
				Main.soundRoar[1] = base.Content.Load<SoundEffect>("Sounds" + Path.DirectorySeparatorChar + "Roar_1");
				Main.soundInstanceRoar[1] = Main.soundRoar[1].CreateInstance();
				Main.soundSplash[0] = base.Content.Load<SoundEffect>("Sounds" + Path.DirectorySeparatorChar + "Splash_0");
				Main.soundInstanceSplash[0] = Main.soundRoar[0].CreateInstance();
				Main.soundSplash[1] = base.Content.Load<SoundEffect>("Sounds" + Path.DirectorySeparatorChar + "Splash_1");
				Main.soundInstanceSplash[1] = Main.soundSplash[1].CreateInstance();
				Main.soundDoubleJump = base.Content.Load<SoundEffect>("Sounds" + Path.DirectorySeparatorChar + "Double_Jump");
				Main.soundInstanceDoubleJump = Main.soundRoar[0].CreateInstance();
				Main.soundRun = base.Content.Load<SoundEffect>("Sounds" + Path.DirectorySeparatorChar + "Run");
				Main.soundInstanceRun = Main.soundRun.CreateInstance();
				Main.soundCoins = base.Content.Load<SoundEffect>("Sounds" + Path.DirectorySeparatorChar + "Coins");
				Main.soundInstanceCoins = Main.soundCoins.CreateInstance();
				Main.soundUnlock = base.Content.Load<SoundEffect>("Sounds" + Path.DirectorySeparatorChar + "Unlock");
				Main.soundInstanceUnlock = Main.soundUnlock.CreateInstance();
				Main.soundMaxMana = base.Content.Load<SoundEffect>("Sounds" + Path.DirectorySeparatorChar + "MaxMana");
				Main.soundInstanceMaxMana = Main.soundMaxMana.CreateInstance();
				Main.soundDrown = base.Content.Load<SoundEffect>("Sounds" + Path.DirectorySeparatorChar + "Drown");
				Main.soundInstanceDrown = Main.soundDrown.CreateInstance();
				for (int j = 1; j < 38; j++)
				{
					Main.soundItem[j] = base.Content.Load<SoundEffect>(string.Concat(new object[]
					{
						"Sounds",
						Path.DirectorySeparatorChar,
						"Item_",
						j
					}));
					Main.soundInstanceItem[j] = Main.soundItem[j].CreateInstance();
				}
				for (int k = 1; k < 12; k++)
				{
					Main.soundNPCHit[k] = base.Content.Load<SoundEffect>(string.Concat(new object[]
					{
						"Sounds",
						Path.DirectorySeparatorChar,
						"NPC_Hit_",
						k
					}));
					Main.soundInstanceNPCHit[k] = Main.soundNPCHit[k].CreateInstance();
				}
				for (int l = 1; l < 16; l++)
				{
					Main.soundNPCKilled[l] = base.Content.Load<SoundEffect>(string.Concat(new object[]
					{
						"Sounds",
						Path.DirectorySeparatorChar,
						"NPC_Killed_",
						l
					}));
					Main.soundInstanceNPCKilled[l] = Main.soundNPCKilled[l].CreateInstance();
				}
			}
			catch
			{
				Main.musicVolume = 0f;
				Main.soundVolume = 0f;
			}
			Main.reforgeTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Reforge");
			Main.timerTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Timer");
			Main.wofTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "WallOfFlesh");
			Main.wallOutlineTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Wall_Outline");
			Main.raTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "ra-logo");
			Main.reTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "re-logo");
			Main.splashTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "splash");
			Main.fadeTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "fade-out");
			Main.ghostTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Ghost");
			Main.evilCactusTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Evil_Cactus");
			Main.goodCactusTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Good_Cactus");
			Main.wraithEyeTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Wraith_Eyes");
			Main.MusicBoxTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Music_Box");
			Main.wingsTexture[1] = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Wings_1");
			Main.wingsTexture[2] = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Wings_2");
			Main.destTexture[0] = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Dest1");
			Main.destTexture[1] = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Dest2");
			Main.destTexture[2] = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Dest3");
			Main.wireTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Wires");
			Main.loTexture = base.Content.Load<Texture2D>(string.Concat(new object[]
			{
				"Images",
				Path.DirectorySeparatorChar,
				"logo_",
				Main.rand.Next(1, 7)
			}));
			this.spriteBatch = new SpriteBatch(base.GraphicsDevice);
			for (int m = 1; m < 2; m++)
			{
				Main.bannerTexture[m] = base.Content.Load<Texture2D>(string.Concat(new object[]
				{
					"Images",
					Path.DirectorySeparatorChar,
					"House_Banner_",
					m
				}));
			}
			for (int n = 0; n < 12; n++)
			{
				Main.npcHeadTexture[n] = base.Content.Load<Texture2D>(string.Concat(new object[]
				{
					"Images",
					Path.DirectorySeparatorChar,
					"NPC_Head_",
					n
				}));
			}
			for (int num = 0; num < 150; num++)
			{
				Main.tileTexture[num] = base.Content.Load<Texture2D>(string.Concat(new object[]
				{
					"Images",
					Path.DirectorySeparatorChar,
					"Tiles_",
					num
				}));
			}
			for (int num2 = 1; num2 < 32; num2++)
			{
				Main.wallTexture[num2] = base.Content.Load<Texture2D>(string.Concat(new object[]
				{
					"Images",
					Path.DirectorySeparatorChar,
					"Wall_",
					num2
				}));
			}
			for (int num3 = 1; num3 < 41; num3++)
			{
				Main.buffTexture[num3] = base.Content.Load<Texture2D>(string.Concat(new object[]
				{
					"Images",
					Path.DirectorySeparatorChar,
					"Buff_",
					num3
				}));
			}
			for (int num4 = 0; num4 < 32; num4++)
			{
				Main.backgroundTexture[num4] = base.Content.Load<Texture2D>(string.Concat(new object[]
				{
					"Images",
					Path.DirectorySeparatorChar,
					"Background_",
					num4
				}));
				Main.backgroundWidth[num4] = Main.backgroundTexture[num4].Width;
				Main.backgroundHeight[num4] = Main.backgroundTexture[num4].Height;
			}
			for (int num5 = 0; num5 < 604; num5++)
			{
				Main.itemTexture[num5] = base.Content.Load<Texture2D>(string.Concat(new object[]
				{
					"Images",
					Path.DirectorySeparatorChar,
					"Item_",
					num5
				}));
			}
			for (int num6 = 0; num6 < 147; num6++)
			{
				Main.npcTexture[num6] = base.Content.Load<Texture2D>(string.Concat(new object[]
				{
					"Images",
					Path.DirectorySeparatorChar,
					"NPC_",
					num6
				}));
			}
			for (int num7 = 0; num7 < 147; num7++)
			{
				NPC nPC = new NPC();
				nPC.SetDefaults(num7, -1f);
				Main.npcName[num7] = nPC.name;
			}
			for (int num8 = 0; num8 < 112; num8++)
			{
				Main.projectileTexture[num8] = base.Content.Load<Texture2D>(string.Concat(new object[]
				{
					"Images",
					Path.DirectorySeparatorChar,
					"Projectile_",
					num8
				}));
			}
			for (int num9 = 1; num9 < 160; num9++)
			{
				Main.goreTexture[num9] = base.Content.Load<Texture2D>(string.Concat(new object[]
				{
					"Images",
					Path.DirectorySeparatorChar,
					"Gore_",
					num9
				}));
			}
			for (int num10 = 0; num10 < 4; num10++)
			{
				Main.cloudTexture[num10] = base.Content.Load<Texture2D>(string.Concat(new object[]
				{
					"Images",
					Path.DirectorySeparatorChar,
					"Cloud_",
					num10
				}));
			}
			for (int num11 = 0; num11 < 5; num11++)
			{
				Main.starTexture[num11] = base.Content.Load<Texture2D>(string.Concat(new object[]
				{
					"Images",
					Path.DirectorySeparatorChar,
					"Star_",
					num11
				}));
			}
			for (int num12 = 0; num12 < 2; num12++)
			{
				Main.liquidTexture[num12] = base.Content.Load<Texture2D>(string.Concat(new object[]
				{
					"Images",
					Path.DirectorySeparatorChar,
					"Liquid_",
					num12
				}));
			}
			Main.npcToggleTexture[0] = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "House_1");
			Main.npcToggleTexture[1] = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "House_2");
			Main.HBLockTexture[0] = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Lock_0");
			Main.HBLockTexture[1] = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Lock_1");
			Main.gridTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Grid");
			Main.trashTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Trash");
			Main.cdTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "CoolDown");
			Main.logoTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Logo");
			Main.logo2Texture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Logo2");
			Main.logo3Texture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Logo3");
			Main.dustTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Dust");
			Main.sunTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Sun");
			Main.sun2Texture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Sun2");
			Main.moonTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Moon");
			Main.blackTileTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Black_Tile");
			Main.heartTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Heart");
			Main.bubbleTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Bubble");
			Main.manaTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Mana");
			Main.cursorTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Cursor");
			Main.ninjaTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Ninja");
			Main.antLionTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "AntlionBody");
			Main.spikeBaseTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Spike_Base");
			Main.treeTopTexture[0] = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Tree_Tops_0");
			Main.treeBranchTexture[0] = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Tree_Branches_0");
			Main.treeTopTexture[1] = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Tree_Tops_1");
			Main.treeBranchTexture[1] = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Tree_Branches_1");
			Main.treeTopTexture[2] = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Tree_Tops_2");
			Main.treeBranchTexture[2] = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Tree_Branches_2");
			Main.treeTopTexture[3] = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Tree_Tops_3");
			Main.treeBranchTexture[3] = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Tree_Branches_3");
			Main.treeTopTexture[4] = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Tree_Tops_4");
			Main.treeBranchTexture[4] = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Tree_Branches_4");
			Main.shroomCapTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Shroom_Tops");
			Main.inventoryBackTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Inventory_Back");
			Main.inventoryBack2Texture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Inventory_Back2");
			Main.inventoryBack3Texture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Inventory_Back3");
			Main.inventoryBack4Texture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Inventory_Back4");
			Main.inventoryBack5Texture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Inventory_Back5");
			Main.inventoryBack6Texture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Inventory_Back6");
			Main.inventoryBack7Texture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Inventory_Back7");
			Main.inventoryBack8Texture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Inventory_Back8");
			Main.inventoryBack9Texture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Inventory_Back9");
			Main.inventoryBack10Texture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Inventory_Back10");
			Main.inventoryBack11Texture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Inventory_Back11");
			Main.textBackTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Text_Back");
			Main.chatTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Chat");
			Main.chat2Texture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Chat2");
			Main.chatBackTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Chat_Back");
			Main.teamTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Team");
			for (int num13 = 1; num13 < 26; num13++)
			{
				Main.femaleBodyTexture[num13] = base.Content.Load<Texture2D>(string.Concat(new object[]
				{
					"Images",
					Path.DirectorySeparatorChar,
					"Female_Body_",
					num13
				}));
				Main.armorBodyTexture[num13] = base.Content.Load<Texture2D>(string.Concat(new object[]
				{
					"Images",
					Path.DirectorySeparatorChar,
					"Armor_Body_",
					num13
				}));
				Main.armorArmTexture[num13] = base.Content.Load<Texture2D>(string.Concat(new object[]
				{
					"Images",
					Path.DirectorySeparatorChar,
					"Armor_Arm_",
					num13
				}));
			}
			for (int num14 = 1; num14 < 45; num14++)
			{
				Main.armorHeadTexture[num14] = base.Content.Load<Texture2D>(string.Concat(new object[]
				{
					"Images",
					Path.DirectorySeparatorChar,
					"Armor_Head_",
					num14
				}));
			}
			for (int num15 = 1; num15 < 25; num15++)
			{
				Main.armorLegTexture[num15] = base.Content.Load<Texture2D>(string.Concat(new object[]
				{
					"Images",
					Path.DirectorySeparatorChar,
					"Armor_Legs_",
					num15
				}));
			}
			for (int num16 = 0; num16 < 36; num16++)
			{
				Main.playerHairTexture[num16] = base.Content.Load<Texture2D>(string.Concat(new object[]
				{
					"Images",
					Path.DirectorySeparatorChar,
					"Player_Hair_",
					num16 + 1
				}));
				Main.playerHairAltTexture[num16] = base.Content.Load<Texture2D>(string.Concat(new object[]
				{
					"Images",
					Path.DirectorySeparatorChar,
					"Player_HairAlt_",
					num16 + 1
				}));
			}
			Main.skinBodyTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Skin_Body");
			Main.skinLegsTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Skin_Legs");
			Main.playerEyeWhitesTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Player_Eye_Whites");
			Main.playerEyesTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Player_Eyes");
			Main.playerHandsTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Player_Hands");
			Main.playerHands2Texture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Player_Hands2");
			Main.playerHeadTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Player_Head");
			Main.playerPantsTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Player_Pants");
			Main.playerShirtTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Player_Shirt");
			Main.playerShoesTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Player_Shoes");
			Main.playerUnderShirtTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Player_Undershirt");
			Main.playerUnderShirt2Texture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Player_Undershirt2");
			Main.femalePantsTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Female_Pants");
			Main.femaleShirtTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Female_Shirt");
			Main.femaleShoesTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Female_Shoes");
			Main.femaleUnderShirtTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Female_Undershirt");
			Main.femaleUnderShirt2Texture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Female_Undershirt2");
			Main.femaleShirt2Texture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Female_Shirt2");
			Main.chaosTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Chaos");
			Main.EyeLaserTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Eye_Laser");
			Main.BoneEyesTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Bone_eyes");
			Main.BoneLaserTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Bone_Laser");
			Main.lightDiscTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Light_Disc");
			Main.confuseTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Confuse");
			Main.probeTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Probe");
			Main.chainTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Chain");
			Main.chain2Texture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Chain2");
			Main.chain3Texture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Chain3");
			Main.chain4Texture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Chain4");
			Main.chain5Texture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Chain5");
			Main.chain6Texture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Chain6");
			Main.chain7Texture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Chain7");
			Main.chain8Texture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Chain8");
			Main.chain9Texture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Chain9");
			Main.chain10Texture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Chain10");
			Main.chain11Texture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Chain11");
			Main.chain12Texture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Chain12");
			Main.boneArmTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Arm_Bone");
			Main.boneArm2Texture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Arm_Bone_2");
			Main.fontItemStack = base.Content.Load<SpriteFont>("Fonts" + Path.DirectorySeparatorChar + "Item_Stack");
			Main.fontMouseText = base.Content.Load<SpriteFont>("Fonts" + Path.DirectorySeparatorChar + "Mouse_Text");
			Main.fontDeathText = base.Content.Load<SpriteFont>("Fonts" + Path.DirectorySeparatorChar + "Death_Text");
			Main.fontCombatText[0] = base.Content.Load<SpriteFont>("Fonts" + Path.DirectorySeparatorChar + "Combat_Text");
			Main.fontCombatText[1] = base.Content.Load<SpriteFont>("Fonts" + Path.DirectorySeparatorChar + "Combat_Crit");

            //init the mod
            Mod.Init();
		}
		protected override void UnloadContent()
		{
            //deinit the mod
            Mod.DeInit();
		}
		protected void UpdateMusic()
		{
			try
			{
				if (!Main.dedServ)
				{
					if (Main.curMusic > 0)
					{
						if (!base.IsActive)
						{
							if (!Main.music[Main.curMusic].IsPaused && Main.music[Main.curMusic].IsPlaying)
							{
								try
								{
									Main.music[Main.curMusic].Pause();
								}
								catch
								{
								}
							}
							return;
						}
						if (Main.music[Main.curMusic].IsPaused)
						{
							Main.music[Main.curMusic].Resume();
						}
					}
					bool flag = false;
					bool flag2 = false;
					bool flag3 = false;
					Rectangle rectangle = new Rectangle((int)Main.screenPosition.X, (int)Main.screenPosition.Y, Main.screenWidth, Main.screenHeight);
					int num = 5000;
					for (int i = 0; i < 200; i++)
					{
						if (Main.npc[i].active)
						{
							if (Main.npc[i].type == 134 || Main.npc[i].type == 143 || Main.npc[i].type == 144 || Main.npc[i].type == 145)
							{
								Rectangle value = new Rectangle((int)(Main.npc[i].position.X + (float)(Main.npc[i].width / 2)) - num, (int)(Main.npc[i].position.Y + (float)(Main.npc[i].height / 2)) - num, num * 2, num * 2);
								if (rectangle.Intersects(value))
								{
									flag3 = true;
									break;
								}
							}
							else
							{
								if (Main.npc[i].type == 113 || Main.npc[i].type == 114 || Main.npc[i].type == 125 || Main.npc[i].type == 126)
								{
									Rectangle value2 = new Rectangle((int)(Main.npc[i].position.X + (float)(Main.npc[i].width / 2)) - num, (int)(Main.npc[i].position.Y + (float)(Main.npc[i].height / 2)) - num, num * 2, num * 2);
									if (rectangle.Intersects(value2))
									{
										flag2 = true;
										break;
									}
								}
								else
								{
									if (Main.npc[i].boss || Main.npc[i].type == 13 || Main.npc[i].type == 14 || Main.npc[i].type == 15 || Main.npc[i].type == 134 || Main.npc[i].type == 26 || Main.npc[i].type == 27 || Main.npc[i].type == 28 || Main.npc[i].type == 29 || Main.npc[i].type == 111)
									{
										Rectangle value3 = new Rectangle((int)(Main.npc[i].position.X + (float)(Main.npc[i].width / 2)) - num, (int)(Main.npc[i].position.Y + (float)(Main.npc[i].height / 2)) - num, num * 2, num * 2);
										if (rectangle.Intersects(value3))
										{
											flag = true;
											break;
										}
									}
								}
							}
						}
					}
					if (Main.musicVolume == 0f)
					{
						this.newMusic = 0;
					}
					else
					{
						if (Main.gameMenu)
						{
							if (Main.netMode != 2)
							{
								this.newMusic = 6;
							}
							else
							{
								this.newMusic = 0;
							}
						}
						else
						{
							if (flag2)
							{
								this.newMusic = 12;
							}
							else
							{
								if (flag)
								{
									this.newMusic = 5;
								}
								else
								{
									if (flag3)
									{
										this.newMusic = 13;
									}
									else
									{
										if (Main.player[Main.myPlayer].position.Y > (float)((Main.maxTilesY - 200) * 16))
										{
											this.newMusic = 2;
										}
										else
										{
											if (Main.player[Main.myPlayer].zoneEvil)
											{
												if ((double)Main.player[Main.myPlayer].position.Y > Main.worldSurface * 16.0 + (double)Main.screenHeight)
												{
													this.newMusic = 10;
												}
												else
												{
													this.newMusic = 8;
												}
											}
											else
											{
												if (Main.player[Main.myPlayer].zoneMeteor || Main.player[Main.myPlayer].zoneDungeon)
												{
													this.newMusic = 2;
												}
												else
												{
													if (Main.player[Main.myPlayer].zoneJungle)
													{
														this.newMusic = 7;
													}
													else
													{
														if ((double)Main.player[Main.myPlayer].position.Y > Main.worldSurface * 16.0 + (double)Main.screenHeight)
														{
															if (Main.player[Main.myPlayer].zoneHoly)
															{
																this.newMusic = 11;
															}
															else
															{
																this.newMusic = 4;
															}
														}
														else
														{
															if (Main.dayTime)
															{
																if (Main.player[Main.myPlayer].zoneHoly)
																{
																	this.newMusic = 9;
																}
																else
																{
																	this.newMusic = 1;
																}
															}
															else
															{
																if (!Main.dayTime)
																{
																	if (Main.bloodMoon)
																	{
																		this.newMusic = 2;
																	}
																	else
																	{
																		this.newMusic = 3;
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
					if (Main.gameMenu)
					{
						Main.musicBox2 = -1;
						Main.musicBox = -1;
					}
					if (Main.musicBox2 >= 0)
					{
						Main.musicBox = Main.musicBox2;
					}
					if (Main.musicBox >= 0)
					{
						if (Main.musicBox == 0)
						{
							this.newMusic = 1;
						}
						if (Main.musicBox == 1)
						{
							this.newMusic = 2;
						}
						if (Main.musicBox == 2)
						{
							this.newMusic = 3;
						}
						if (Main.musicBox == 4)
						{
							this.newMusic = 4;
						}
						if (Main.musicBox == 5)
						{
							this.newMusic = 5;
						}
						if (Main.musicBox == 3)
						{
							this.newMusic = 6;
						}
						if (Main.musicBox == 6)
						{
							this.newMusic = 7;
						}
						if (Main.musicBox == 7)
						{
							this.newMusic = 8;
						}
						if (Main.musicBox == 9)
						{
							this.newMusic = 9;
						}
						if (Main.musicBox == 8)
						{
							this.newMusic = 10;
						}
						if (Main.musicBox == 11)
						{
							this.newMusic = 11;
						}
						if (Main.musicBox == 10)
						{
							this.newMusic = 12;
						}
						if (Main.musicBox == 12)
						{
							this.newMusic = 13;
						}
					}
					Main.curMusic = this.newMusic;
					for (int j = 1; j < 14; j++)
					{
						if (j == Main.curMusic)
						{
							if (!Main.music[j].IsPlaying)
							{
								Main.music[j] = Main.soundBank.GetCue("Music_" + j);
								Main.music[j].Play();
								Main.music[j].SetVariable("Volume", Main.musicFade[j] * Main.musicVolume);
							}
							else
							{
								Main.musicFade[j] += 0.005f;
								if (Main.musicFade[j] > 1f)
								{
									Main.musicFade[j] = 1f;
								}
								Main.music[j].SetVariable("Volume", Main.musicFade[j] * Main.musicVolume);
							}
						}
						else
						{
							if (Main.music[j].IsPlaying)
							{
								if (Main.musicFade[Main.curMusic] > 0.25f)
								{
									Main.musicFade[j] -= 0.005f;
								}
								else
								{
									if (Main.curMusic == 0)
									{
										Main.musicFade[j] = 0f;
									}
								}
								if (Main.musicFade[j] <= 0f)
								{
									Main.musicFade[j] -= 0f;
									Main.music[j].Stop(AudioStopOptions.Immediate);
								}
								else
								{
									Main.music[j].SetVariable("Volume", Main.musicFade[j] * Main.musicVolume);
								}
							}
							else
							{
								Main.musicFade[j] = 0f;
							}
						}
					}
				}
			}
			catch
			{
				Main.musicVolume = 0f;
			}
		}
		public static void snowing()
		{
			if (Main.gamePaused)
			{
				return;
			}
			if (Main.snowTiles > 0 && (double)Main.player[Main.myPlayer].position.Y < Main.worldSurface * 16.0)
			{
				int maxValue = 800 / Main.snowTiles;
				float num = (float)Main.screenWidth / 1920f;
				int num2 = (int)(500f * num);
				if ((float)Main.snowDust < (float)num2 * (Main.gfxQuality / 2f + 0.5f) + (float)num2 * 0.1f && Main.rand.Next(maxValue) == 0)
				{
					int num3 = Main.rand.Next(Main.screenWidth + 1000) - 500;
					int num4 = (int)Main.screenPosition.Y;
					if (Main.rand.Next(5) == 0)
					{
						num3 = Main.rand.Next(500) - 500;
					}
					else
					{
						if (Main.rand.Next(5) == 0)
						{
							num3 = Main.rand.Next(500) + Main.screenWidth;
						}
					}
					if (num3 < 0 || num3 > Main.screenWidth)
					{
						num4 += Main.rand.Next((int)((double)Main.screenHeight * 0.5)) + (int)((double)Main.screenHeight * 0.1);
					}
					num3 += (int)Main.screenPosition.X;
					int num5 = Dust.NewDust(new Vector2((float)num3, (float)num4), 10, 10, 76, 0f, 0f, 0, default(Color), 1f);
					Main.dust[num5].velocity.Y = 3f + (float)Main.rand.Next(30) * 0.1f;
					Dust expr_1BD_cp_0 = Main.dust[num5];
					expr_1BD_cp_0.velocity.Y = expr_1BD_cp_0.velocity.Y * Main.dust[num5].scale;
					Main.dust[num5].velocity.X = Main.windSpeed + (float)Main.rand.Next(-10, 10) * 0.1f;
				}
			}
		}
		public static void checkXMas()
		{
			DateTime now = DateTime.Now;
			int day = now.Day;
			int month = now.Month;
			if (day >= 15 && month == 12)
			{
				Main.xMas = true;
				return;
			}
			Main.xMas = false;
		}
		protected override void Update(GameTime gameTime)
		{
			if (Main.netMode != 2)
			{
				Main.snowing();
			}
			if (Main.chTitle)
			{
				Main.chTitle = false;
				this.SetTitle();
			}
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			WorldGen.destroyObject = false;
			if (!Main.dedServ)
			{
				if (Main.fixedTiming)
				{
					if (base.IsActive)
					{
						base.IsFixedTimeStep = false;
					}
					else
					{
						base.IsFixedTimeStep = true;
					}
				}
				else
				{
					base.IsFixedTimeStep = true;
				}
				this.graphics.SynchronizeWithVerticalRetrace = true;
				this.UpdateMusic();
				if (Main.showSplash)
				{
					return;
				}
				if (!Main.gameMenu && Main.netMode == 1)
				{
					if (!Main.saveTime.IsRunning)
					{
						Main.saveTime.Start();
					}
					if (Main.saveTime.ElapsedMilliseconds > 300000L)
					{
						Main.saveTime.Reset();
						WorldGen.saveToonWhilePlaying();
					}
				}
				else
				{
					if (!Main.gameMenu && Main.autoSave)
					{
						if (!Main.saveTime.IsRunning)
						{
							Main.saveTime.Start();
						}
						if (Main.saveTime.ElapsedMilliseconds > 600000L)
						{
							Main.saveTime.Reset();
							WorldGen.saveToonWhilePlaying();
							WorldGen.saveAndPlay();
						}
					}
					else
					{
						if (Main.saveTime.IsRunning)
						{
							Main.saveTime.Stop();
						}
					}
				}
				if (Main.teamCooldown > 0)
				{
					Main.teamCooldown--;
				}
				Main.updateTime++;
				if (Main.fpsTimer.ElapsedMilliseconds >= 1000L)
				{
					if ((float)Main.fpsCount >= 30f + 30f * Main.gfxQuality)
					{
						Main.gfxQuality += Main.gfxRate;
						Main.gfxRate += 0.005f;
					}
					else
					{
						if ((float)Main.fpsCount < 29f + 30f * Main.gfxQuality)
						{
							Main.gfxRate = 0.01f;
							Main.gfxQuality -= 0.1f;
						}
					}
					if (Main.gfxQuality < 0f)
					{
						Main.gfxQuality = 0f;
					}
					if (Main.gfxQuality > 1f)
					{
						Main.gfxQuality = 1f;
					}
					if (Main.maxQ && base.IsActive)
					{
						Main.gfxQuality = 1f;
						Main.maxQ = false;
					}
					Main.updateRate = Main.uCount;
					Main.frameRate = Main.fpsCount;
					Main.fpsCount = 0;
					Main.fpsTimer.Restart();
					Main.updateTime = 0;
					Main.drawTime = 0;
					Main.uCount = 0;
					if (Main.netMode == 2)
					{
						Main.cloudLimit = 0;
					}
				}
				if (Main.fixedTiming)
				{
					float num = 16f;
					float num2 = (float)Main.updateTimer.ElapsedMilliseconds;
					if (num2 + Main.uCarry < num)
					{
						Main.drawSkip = true;
						return;
					}
					Main.uCarry += num2 - num;
					if (Main.uCarry > 1000f)
					{
						Main.uCarry = 1000f;
					}
					Main.updateTimer.Restart();
				}
				Main.uCount++;
				Main.drawSkip = false;
				if (Main.qaStyle == 1)
				{
					Main.gfxQuality = 1f;
				}
				else
				{
					if (Main.qaStyle == 2)
					{
						Main.gfxQuality = 0.5f;
					}
					else
					{
						if (Main.qaStyle == 3)
						{
							Main.gfxQuality = 0f;
						}
					}
				}
				Main.numDust = (int)(2000f * (Main.gfxQuality * 0.75f + 0.25f));
				Gore.goreTime = (int)(600f * Main.gfxQuality);
				Main.cloudLimit = (int)(100f * Main.gfxQuality);
				Liquid.maxLiquid = (int)(2500f + 2500f * Main.gfxQuality);
				Liquid.cycles = (int)(17f - 10f * Main.gfxQuality);
				if ((double)Main.gfxQuality < 0.5)
				{
					this.graphics.SynchronizeWithVerticalRetrace = false;
				}
				else
				{
					this.graphics.SynchronizeWithVerticalRetrace = true;
				}
				if ((double)Main.gfxQuality < 0.2)
				{
					Lighting.maxRenderCount = 8;
				}
				else
				{
					if ((double)Main.gfxQuality < 0.4)
					{
						Lighting.maxRenderCount = 7;
					}
					else
					{
						if ((double)Main.gfxQuality < 0.6)
						{
							Lighting.maxRenderCount = 6;
						}
						else
						{
							if ((double)Main.gfxQuality < 0.8)
							{
								Lighting.maxRenderCount = 5;
							}
							else
							{
								Lighting.maxRenderCount = 4;
							}
						}
					}
				}
				if (Liquid.quickSettle)
				{
					Liquid.maxLiquid = Liquid.resLiquid;
					Liquid.cycles = 1;
				}
				if (!base.IsActive)
				{
					Main.hasFocus = false;
				}
				else
				{
					Main.hasFocus = true;
				}
				if (!base.IsActive && Main.netMode == 0)
				{
					base.IsMouseVisible = true;
					if (Main.netMode != 2 && Main.myPlayer >= 0)
					{
						Main.player[Main.myPlayer].delayUseItem = true;
					}
					Main.mouseLeftRelease = false;
					Main.mouseRightRelease = false;
					if (Main.gameMenu)
					{
						Main.UpdateMenu();
					}
					Main.gamePaused = true;
					return;
				}
				base.IsMouseVisible = false;
				Main.demonTorch += (float)Main.demonTorchDir * 0.01f;
				if (Main.demonTorch > 1f)
				{
					Main.demonTorch = 1f;
					Main.demonTorchDir = -1;
				}
				if (Main.demonTorch < 0f)
				{
					Main.demonTorch = 0f;
					Main.demonTorchDir = 1;
				}
				int num3 = 7;
				if (this.DiscoStyle == 0)
				{
					Main.DiscoG += num3;
					if (Main.DiscoG >= 255)
					{
						Main.DiscoG = 255;
						this.DiscoStyle++;
					}
					Main.DiscoR -= num3;
					if (Main.DiscoR <= 0)
					{
						Main.DiscoR = 0;
					}
				}
				else
				{
					if (this.DiscoStyle == 1)
					{
						Main.DiscoB += num3;
						if (Main.DiscoB >= 255)
						{
							Main.DiscoB = 255;
							this.DiscoStyle++;
						}
						Main.DiscoG -= num3;
						if (Main.DiscoG <= 0)
						{
							Main.DiscoG = 0;
						}
					}
					else
					{
						Main.DiscoR += num3;
						if (Main.DiscoR >= 255)
						{
							Main.DiscoR = 255;
							this.DiscoStyle = 0;
						}
						Main.DiscoB -= num3;
						if (Main.DiscoB <= 0)
						{
							Main.DiscoB = 0;
						}
					}
				}
				if (Main.keyState.IsKeyDown(Keys.F10) && !Main.chatMode && !Main.editSign)
				{
					if (Main.frameRelease)
					{
						Main.PlaySound(12, -1, -1, 1);
						if (Main.showFrameRate)
						{
							Main.showFrameRate = false;
						}
						else
						{
							Main.showFrameRate = true;
						}
					}
					Main.frameRelease = false;
				}
				else
				{
					Main.frameRelease = true;
				}
				if (Main.keyState.IsKeyDown(Keys.F9) && !Main.chatMode && !Main.editSign)
				{
					if (Main.RGBRelease)
					{
						Lighting.lightCounter += 100;
						Main.PlaySound(12, -1, -1, 1);
						Lighting.lightMode++;
						if (Lighting.lightMode >= 4)
						{
							Lighting.lightMode = 0;
						}
						if (Lighting.lightMode == 2 || Lighting.lightMode == 0)
						{
							Main.renderCount = 0;
							Main.renderNow = true;
							int num4 = Main.screenWidth / 16 + Lighting.offScreenTiles * 2;
							int num5 = Main.screenHeight / 16 + Lighting.offScreenTiles * 2;
							for (int i = 0; i < num4; i++)
							{
								for (int j = 0; j < num5; j++)
								{
									Lighting.color[i, j] = 0f;
									Lighting.colorG[i, j] = 0f;
									Lighting.colorB[i, j] = 0f;
								}
							}
						}
					}
					Main.RGBRelease = false;
				}
				else
				{
					Main.RGBRelease = true;
				}
				if (Main.keyState.IsKeyDown(Keys.F8) && !Main.chatMode && !Main.editSign)
				{
					if (Main.netRelease)
					{
						Main.PlaySound(12, -1, -1, 1);
						if (Main.netDiag)
						{
							Main.netDiag = false;
						}
						else
						{
							Main.netDiag = true;
						}
					}
					Main.netRelease = false;
				}
				else
				{
					Main.netRelease = true;
				}
				if (Main.keyState.IsKeyDown(Keys.F7) && !Main.chatMode && !Main.editSign)
				{
					if (Main.drawRelease)
					{
						Main.PlaySound(12, -1, -1, 1);
						if (Main.drawDiag)
						{
							Main.drawDiag = false;
						}
						else
						{
							Main.drawDiag = true;
						}
					}
					Main.drawRelease = false;
				}
				else
				{
					Main.drawRelease = true;
				}
				if (Main.keyState.IsKeyDown(Keys.F11))
				{
					if (Main.releaseUI)
					{
						if (Main.hideUI)
						{
							Main.hideUI = false;
						}
						else
						{
							Main.hideUI = true;
						}
					}
					Main.releaseUI = false;
				}
				else
				{
					Main.releaseUI = true;
				}
				if ((Main.keyState.IsKeyDown(Keys.LeftAlt) || Main.keyState.IsKeyDown(Keys.RightAlt)) && Main.keyState.IsKeyDown(Keys.Enter))
				{
					if (this.toggleFullscreen)
					{
						this.graphics.ToggleFullScreen();
						Main.chatRelease = false;
					}
					this.toggleFullscreen = false;
				}
				else
				{
					this.toggleFullscreen = true;
				}
				if (!Main.gamePad || Main.gameMenu)
				{
					Main.oldMouseState = Main.mouseState;
					Main.mouseState = Mouse.GetState();
					Main.mouseX = Main.mouseState.X;
					Main.mouseY = Main.mouseState.Y;
					Main.mouseLeft = false;
					Main.mouseRight = false;
					if (Main.mouseState.LeftButton == ButtonState.Pressed)
					{
						Main.mouseLeft = true;
					}
					if (Main.mouseState.RightButton == ButtonState.Pressed)
					{
						Main.mouseRight = true;
					}
				}
				Main.keyState = Keyboard.GetState();
				if (Main.editSign)
				{
					Main.chatMode = false;
				}
				if (Main.chatMode)
				{
					if (Main.keyState.IsKeyDown(Keys.Escape))
					{
						Main.chatMode = false;
					}
					string a = Main.chatText;
					Main.chatText = Main.GetInputText(Main.chatText);
					while (Main.fontMouseText.MeasureString(Main.chatText).X > 470f)
					{
						Main.chatText = Main.chatText.Substring(0, Main.chatText.Length - 1);
					}
					if (a != Main.chatText)
					{
						Main.PlaySound(12, -1, -1, 1);
					}
					if (Main.inputTextEnter && Main.chatRelease)
					{
                        if (Main.chatText.Length > 0 && Main.chatText[0] == '.')
                        {
                            Mod.OnCommand(Main.chatText);
                        }
						else if (Main.chatText != "")
						{
							NetMessage.SendData(25, -1, -1, Main.chatText, Main.myPlayer, 0f, 0f, 0f, 0);
						}
						Main.chatText = "";
						Main.chatMode = false;
						Main.chatRelease = false;
						Main.player[Main.myPlayer].releaseHook = false;
						Main.player[Main.myPlayer].releaseThrow = false;
						Main.PlaySound(11, -1, -1, 1);
					}
				}
				if (Main.keyState.IsKeyDown(Keys.Enter) && Main.netMode == 1 && !Main.keyState.IsKeyDown(Keys.LeftAlt) && !Main.keyState.IsKeyDown(Keys.RightAlt))
				{
					if (Main.chatRelease && !Main.chatMode && !Main.editSign && !Main.keyState.IsKeyDown(Keys.Escape))
					{
						Main.PlaySound(10, -1, -1, 1);
						Main.chatMode = true;
						Main.clrInput();
						Main.chatText = "";
					}
					Main.chatRelease = false;
				}
				else
				{
					Main.chatRelease = true;
				}
				if (Main.gameMenu)
				{
					Main.UpdateMenu();
					if (Main.netMode != 2)
					{
						return;
					}
					Main.gamePaused = false;
				}
			}
			if (Main.netMode == 1)
			{
				for (int k = 0; k < 49; k++)
				{
					if (Main.player[Main.myPlayer].inventory[k].IsNotTheSameAs(Main.clientPlayer.inventory[k]))
					{
						NetMessage.SendData(5, -1, -1, Main.player[Main.myPlayer].inventory[k].name, Main.myPlayer, (float)k, (float)Main.player[Main.myPlayer].inventory[k].prefix, 0f, 0);
					}
				}
				if (Main.player[Main.myPlayer].armor[0].IsNotTheSameAs(Main.clientPlayer.armor[0]))
				{
					NetMessage.SendData(5, -1, -1, Main.player[Main.myPlayer].armor[0].name, Main.myPlayer, 49f, (float)Main.player[Main.myPlayer].armor[0].prefix, 0f, 0);
				}
				if (Main.player[Main.myPlayer].armor[1].IsNotTheSameAs(Main.clientPlayer.armor[1]))
				{
					NetMessage.SendData(5, -1, -1, Main.player[Main.myPlayer].armor[1].name, Main.myPlayer, 50f, (float)Main.player[Main.myPlayer].armor[1].prefix, 0f, 0);
				}
				if (Main.player[Main.myPlayer].armor[2].IsNotTheSameAs(Main.clientPlayer.armor[2]))
				{
					NetMessage.SendData(5, -1, -1, Main.player[Main.myPlayer].armor[2].name, Main.myPlayer, 51f, (float)Main.player[Main.myPlayer].armor[2].prefix, 0f, 0);
				}
				if (Main.player[Main.myPlayer].armor[3].IsNotTheSameAs(Main.clientPlayer.armor[3]))
				{
					NetMessage.SendData(5, -1, -1, Main.player[Main.myPlayer].armor[3].name, Main.myPlayer, 52f, (float)Main.player[Main.myPlayer].armor[3].prefix, 0f, 0);
				}
				if (Main.player[Main.myPlayer].armor[4].IsNotTheSameAs(Main.clientPlayer.armor[4]))
				{
					NetMessage.SendData(5, -1, -1, Main.player[Main.myPlayer].armor[4].name, Main.myPlayer, 53f, (float)Main.player[Main.myPlayer].armor[4].prefix, 0f, 0);
				}
				if (Main.player[Main.myPlayer].armor[5].IsNotTheSameAs(Main.clientPlayer.armor[5]))
				{
					NetMessage.SendData(5, -1, -1, Main.player[Main.myPlayer].armor[5].name, Main.myPlayer, 54f, (float)Main.player[Main.myPlayer].armor[5].prefix, 0f, 0);
				}
				if (Main.player[Main.myPlayer].armor[6].IsNotTheSameAs(Main.clientPlayer.armor[6]))
				{
					NetMessage.SendData(5, -1, -1, Main.player[Main.myPlayer].armor[6].name, Main.myPlayer, 55f, (float)Main.player[Main.myPlayer].armor[6].prefix, 0f, 0);
				}
				if (Main.player[Main.myPlayer].armor[7].IsNotTheSameAs(Main.clientPlayer.armor[7]))
				{
					NetMessage.SendData(5, -1, -1, Main.player[Main.myPlayer].armor[7].name, Main.myPlayer, 56f, (float)Main.player[Main.myPlayer].armor[7].prefix, 0f, 0);
				}
				if (Main.player[Main.myPlayer].armor[8].IsNotTheSameAs(Main.clientPlayer.armor[8]))
				{
					NetMessage.SendData(5, -1, -1, Main.player[Main.myPlayer].armor[8].name, Main.myPlayer, 57f, (float)Main.player[Main.myPlayer].armor[8].prefix, 0f, 0);
				}
				if (Main.player[Main.myPlayer].armor[9].IsNotTheSameAs(Main.clientPlayer.armor[9]))
				{
					NetMessage.SendData(5, -1, -1, Main.player[Main.myPlayer].armor[9].name, Main.myPlayer, 58f, (float)Main.player[Main.myPlayer].armor[9].prefix, 0f, 0);
				}
				if (Main.player[Main.myPlayer].armor[10].IsNotTheSameAs(Main.clientPlayer.armor[10]))
				{
					NetMessage.SendData(5, -1, -1, Main.player[Main.myPlayer].armor[10].name, Main.myPlayer, 59f, (float)Main.player[Main.myPlayer].armor[10].prefix, 0f, 0);
				}
				if (Main.player[Main.myPlayer].chest != Main.clientPlayer.chest)
				{
					NetMessage.SendData(33, -1, -1, "", Main.player[Main.myPlayer].chest, 0f, 0f, 0f, 0);
				}
				if (Main.player[Main.myPlayer].talkNPC != Main.clientPlayer.talkNPC)
				{
					NetMessage.SendData(40, -1, -1, "", Main.myPlayer, 0f, 0f, 0f, 0);
				}
				if (Main.player[Main.myPlayer].zoneEvil != Main.clientPlayer.zoneEvil)
				{
					NetMessage.SendData(36, -1, -1, "", Main.myPlayer, 0f, 0f, 0f, 0);
				}
				if (Main.player[Main.myPlayer].zoneMeteor != Main.clientPlayer.zoneMeteor)
				{
					NetMessage.SendData(36, -1, -1, "", Main.myPlayer, 0f, 0f, 0f, 0);
				}
				if (Main.player[Main.myPlayer].zoneDungeon != Main.clientPlayer.zoneDungeon)
				{
					NetMessage.SendData(36, -1, -1, "", Main.myPlayer, 0f, 0f, 0f, 0);
				}
				if (Main.player[Main.myPlayer].zoneJungle != Main.clientPlayer.zoneJungle)
				{
					NetMessage.SendData(36, -1, -1, "", Main.myPlayer, 0f, 0f, 0f, 0);
				}
				if (Main.player[Main.myPlayer].zoneHoly != Main.clientPlayer.zoneHoly)
				{
					NetMessage.SendData(36, -1, -1, "", Main.myPlayer, 0f, 0f, 0f, 0);
				}
				bool flag = false;
				for (int l = 0; l < 10; l++)
				{
					if (Main.player[Main.myPlayer].buffType[l] != Main.clientPlayer.buffType[l])
					{
						flag = true;
					}
				}
				if (flag)
				{
					NetMessage.SendData(50, -1, -1, "", Main.myPlayer, 0f, 0f, 0f, 0);
					NetMessage.SendData(13, -1, -1, "", Main.myPlayer, 0f, 0f, 0f, 0);
				}
			}
			if (Main.netMode == 1)
			{
				Main.clientPlayer = (Player)Main.player[Main.myPlayer].clientClone();
			}
			if (Main.netMode == 0 && (Main.playerInventory || Main.npcChatText != "" || Main.player[Main.myPlayer].sign >= 0) && Main.autoPause)
			{
				Keys[] pressedKeys = Main.keyState.GetPressedKeys();
				Main.player[Main.myPlayer].controlInv = false;
				for (int m = 0; m < pressedKeys.Length; m++)
				{
					string a2 = string.Concat(pressedKeys[m]);
					if (a2 == Main.cInv)
					{
						Main.player[Main.myPlayer].controlInv = true;
					}
				}
				if (Main.player[Main.myPlayer].controlInv)
				{
					if (Main.player[Main.myPlayer].releaseInventory)
					{
						Main.player[Main.myPlayer].toggleInv();
					}
					Main.player[Main.myPlayer].releaseInventory = false;
				}
				else
				{
					Main.player[Main.myPlayer].releaseInventory = true;
				}
				if (Main.playerInventory)
				{
					int num6 = (Main.mouseState.ScrollWheelValue - Main.oldMouseState.ScrollWheelValue) / 120;
					Main.focusRecipe += num6;
					if (Main.focusRecipe > Main.numAvailableRecipes - 1)
					{
						Main.focusRecipe = Main.numAvailableRecipes - 1;
					}
					if (Main.focusRecipe < 0)
					{
						Main.focusRecipe = 0;
					}
					Main.player[Main.myPlayer].dropItemCheck();
				}
				Main.player[Main.myPlayer].head = Main.player[Main.myPlayer].armor[0].headSlot;
				Main.player[Main.myPlayer].body = Main.player[Main.myPlayer].armor[1].bodySlot;
				Main.player[Main.myPlayer].legs = Main.player[Main.myPlayer].armor[2].legSlot;
				if (!Main.player[Main.myPlayer].hostile)
				{
					if (Main.player[Main.myPlayer].armor[8].headSlot >= 0)
					{
						Main.player[Main.myPlayer].head = Main.player[Main.myPlayer].armor[8].headSlot;
					}
					if (Main.player[Main.myPlayer].armor[9].bodySlot >= 0)
					{
						Main.player[Main.myPlayer].body = Main.player[Main.myPlayer].armor[9].bodySlot;
					}
					if (Main.player[Main.myPlayer].armor[10].legSlot >= 0)
					{
						Main.player[Main.myPlayer].legs = Main.player[Main.myPlayer].armor[10].legSlot;
					}
				}
				if (Main.editSign)
				{
					if (Main.player[Main.myPlayer].sign == -1)
					{
						Main.editSign = false;
					}
					else
					{
						Main.npcChatText = Main.GetInputText(Main.npcChatText);
						if (Main.inputTextEnter)
						{
							byte[] bytes = new byte[]
							{
								10
							};
							Main.npcChatText += Encoding.ASCII.GetString(bytes);
						}
					}
				}
				Main.gamePaused = true;
				return;
			}
			Main.gamePaused = false;
			if (!Main.dedServ && (double)Main.screenPosition.Y < Main.worldSurface * 16.0 + 16.0 && Main.netMode != 2)
			{
				Star.UpdateStars();
				Cloud.UpdateClouds();
			}
			int n = 0;
			while (n < 255)
			{
				if (Main.ignoreErrors)
				{
					try
					{
						Main.player[n].UpdatePlayer(n);
						goto IL_15BE;
					}
					catch
					{
						goto IL_15BE;
					}
					goto IL_15AF;
				}
				goto IL_15AF;
				IL_15BE:
				n++;
				continue;
				IL_15AF:
				Main.player[n].UpdatePlayer(n);
				goto IL_15BE;
			}
			if (Main.netMode != 1)
			{
				NPC.SpawnNPC();
			}
			for (int num7 = 0; num7 < 255; num7++)
			{
				Main.player[num7].activeNPCs = 0f;
				Main.player[num7].townNPCs = 0f;
			}
			if (Main.wof >= 0 && !Main.npc[Main.wof].active)
			{
				Main.wof = -1;
			}
			int num8 = 0;
			while (num8 < 200)
			{
				if (Main.ignoreErrors)
				{
					try
					{
						Main.npc[num8].UpdateNPC(num8);
						goto IL_166E;
					}
					catch (Exception)
					{
						Main.npc[num8] = new NPC();
						goto IL_166E;
					}
					goto IL_165F;
				}
				goto IL_165F;
				IL_166E:
				num8++;
				continue;
				IL_165F:
				Main.npc[num8].UpdateNPC(num8);
				goto IL_166E;
			}
			int num9 = 0;
			while (num9 < 200)
			{
				if (Main.ignoreErrors)
				{
					try
					{
						Main.gore[num9].Update();
						goto IL_16B5;
					}
					catch
					{
						Main.gore[num9] = new Gore();
						goto IL_16B5;
					}
					goto IL_16A8;
				}
				goto IL_16A8;
				IL_16B5:
				num9++;
				continue;
				IL_16A8:
				Main.gore[num9].Update();
				goto IL_16B5;
			}
			int num10 = 0;
			while (num10 < 1000)
			{
				if (Main.ignoreErrors)
				{
					try
					{
						Main.projectile[num10].Update(num10);
						goto IL_1700;
					}
					catch
					{
						Main.projectile[num10] = new Projectile();
						goto IL_1700;
					}
					goto IL_16F1;
				}
				goto IL_16F1;
				IL_1700:
				num10++;
				continue;
				IL_16F1:
				Main.projectile[num10].Update(num10);
				goto IL_1700;
			}
			int num11 = 0;
			while (num11 < 200)
			{
				if (Main.ignoreErrors)
				{
					try
					{
						Main.item[num11].UpdateItem(num11);
						goto IL_174B;
					}
					catch
					{
						Main.item[num11] = new Item();
						goto IL_174B;
					}
					goto IL_173C;
				}
				goto IL_173C;
				IL_174B:
				num11++;
				continue;
				IL_173C:
				Main.item[num11].UpdateItem(num11);
				goto IL_174B;
			}
			if (Main.ignoreErrors)
			{
				try
				{
					Dust.UpdateDust();
					goto IL_1791;
				}
				catch
				{
					for (int num12 = 0; num12 < 2000; num12++)
					{
						Main.dust[num12] = new Dust();
					}
					goto IL_1791;
				}
			}
			Dust.UpdateDust();
			IL_1791:
			if (Main.netMode != 2)
			{
				CombatText.UpdateCombatText();
				ItemText.UpdateItemText();
			}
			if (Main.ignoreErrors)
			{
				try
				{
					Main.UpdateTime();
					goto IL_17BF;
				}
				catch
				{
					Main.checkForSpawns = 0;
					goto IL_17BF;
				}
			}
			Main.UpdateTime();
			IL_17BF:
			if (Main.netMode != 1)
			{
				if (Main.ignoreErrors)
				{
					try
					{
						WorldGen.UpdateWorld();
						Main.UpdateInvasion();
						goto IL_17E7;
					}
					catch
					{
						goto IL_17E7;
					}
				}
				WorldGen.UpdateWorld();
				Main.UpdateInvasion();
			}
			IL_17E7:
			if (Main.ignoreErrors)
			{
				try
				{
					if (Main.netMode == 2)
					{
						Main.UpdateServer();
					}
					if (Main.netMode == 1)
					{
						Main.UpdateClient();
					}
					goto IL_182F;
				}
				catch
				{
					int arg_1812_0 = Main.netMode;
					goto IL_182F;
				}
			}
			if (Main.netMode == 2)
			{
				Main.UpdateServer();
			}
			if (Main.netMode == 1)
			{
				Main.UpdateClient();
			}
			IL_182F:
			if (Main.ignoreErrors)
			{
				try
				{
					for (int num13 = 0; num13 < Main.numChatLines; num13++)
					{
						if (Main.chatLine[num13].showTime > 0)
						{
							Main.chatLine[num13].showTime--;
						}
					}
					goto IL_18CE;
				}
				catch
				{
					for (int num14 = 0; num14 < Main.numChatLines; num14++)
					{
						Main.chatLine[num14] = new ChatLine();
					}
					goto IL_18CE;
				}
			}
			for (int num15 = 0; num15 < Main.numChatLines; num15++)
			{
				if (Main.chatLine[num15].showTime > 0)
				{
					Main.chatLine[num15].showTime--;
				}
			}
			IL_18CE:
			Main.upTimer = (float)stopwatch.ElapsedMilliseconds;
			if (Main.upTimerMaxDelay > 0f)
			{
				Main.upTimerMaxDelay -= 1f;
			}
			else
			{
				Main.upTimerMax = 0f;
			}
			if (Main.upTimer > Main.upTimerMax)
			{
				Main.upTimerMax = Main.upTimer;
				Main.upTimerMaxDelay = 400f;
			}
			base.Update(gameTime);
		}
		private static void UpdateMenu()
		{
			Main.playerInventory = false;
			Main.exitScale = 0.8f;
			if (Main.netMode == 0)
			{
				if (!Main.grabSky)
				{
					Main.time += 86.4;
					if (!Main.dayTime)
					{
						if (Main.time > 32400.0)
						{
							Main.bloodMoon = false;
							Main.time = 0.0;
							Main.dayTime = true;
							Main.moonPhase++;
							if (Main.moonPhase >= 8)
							{
								Main.moonPhase = 0;
								return;
							}
						}
					}
					else
					{
						if (Main.time > 54000.0)
						{
							Main.time = 0.0;
							Main.dayTime = false;
							return;
						}
					}
				}
			}
			else
			{
				if (Main.netMode == 1)
				{
					Main.UpdateTime();
				}
			}
		}
		public static void clrInput()
		{
			Main.keyCount = 0;
		}
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern short GetKeyState(int keyCode);
		public static string GetInputText(string oldString)
		{
			if (!Main.hasFocus)
			{
				return oldString;
			}
			Main.inputTextEnter = false;
			string text = oldString;
			string text2 = "";
			if (text == null)
			{
				text = "";
			}
			bool flag = false;
			for (int i = 0; i < Main.keyCount; i++)
			{
				int num = Main.keyInt[i];
				string str = Main.keyString[i];
				if (num == 13)
				{
					Main.inputTextEnter = true;
				}
				else
				{
					if (num >= 32 && num != 127)
					{
						text2 += str;
					}
				}
			}
			Main.keyCount = 0;
			text += text2;
			Main.oldInputText = Main.inputText;
			Main.inputText = Keyboard.GetState();
			Keys[] pressedKeys = Main.inputText.GetPressedKeys();
			Keys[] pressedKeys2 = Main.oldInputText.GetPressedKeys();
			if (Main.inputText.IsKeyDown(Keys.Back) && Main.oldInputText.IsKeyDown(Keys.Back))
			{
				if (Main.backSpaceCount == 0)
				{
					Main.backSpaceCount = 7;
					flag = true;
				}
				Main.backSpaceCount--;
			}
			else
			{
				Main.backSpaceCount = 15;
			}
			for (int j = 0; j < pressedKeys.Length; j++)
			{
				bool flag2 = true;
				for (int k = 0; k < pressedKeys2.Length; k++)
				{
					if (pressedKeys[j] == pressedKeys2[k])
					{
						flag2 = false;
					}
				}
				string a = string.Concat(pressedKeys[j]);
				if (a == "Back" && (flag2 || flag) && text.Length > 0)
				{
					text = text.Substring(0, text.Length - 1);
				}
			}
			return text;
		}
		protected void MouseText(string cursorText, int rare = 0, byte diff = 0)
		{
			if (this.mouseNPC > -1)
			{
				return;
			}
			if (cursorText == null)
			{
				return;
			}
			int num = Main.mouseX + 10;
			int num2 = Main.mouseY + 10;
			Color color = new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor);
			float num20;
			if (Main.toolTip.type > 0)
			{
				if (Main.player[Main.myPlayer].kbGlove)
				{
					Main.toolTip.knockBack *= 1.7f;
				}
				rare = Main.toolTip.rare;
				int num3 = 20;
				int num4 = 1;
				string[] array = new string[num3];
				bool[] array2 = new bool[num3];
				bool[] array3 = new bool[num3];
				for (int i = 0; i < num3; i++)
				{
					array2[i] = false;
					array3[i] = false;
				}
				array[0] = Main.toolTip.AffixName();
				if (Main.toolTip.stack > 1)
				{
					string[] array4;
					string[] expr_DB = array4 = array;
					int arg_11F_1 = 0;
					object obj = array4[0];
					expr_DB[arg_11F_1] = string.Concat(new object[]
					{
						obj,
						" (",
						Main.toolTip.stack,
						")"
					});
				}
				if (Main.toolTip.social)
				{
					array[num4] = Lang.tip[0];
					num4++;
					array[num4] = Lang.tip[1];
					num4++;
				}
				else
				{
					if (Main.toolTip.damage > 0)
					{
						int damage = Main.toolTip.damage;
						if (Main.toolTip.melee)
						{
							array[num4] = string.Concat((int)(Main.player[Main.myPlayer].meleeDamage * (float)damage));
							string[] array5;
							IntPtr intPtr;
							(array5 = array)[(int)(intPtr = (IntPtr)num4)] = array5[(int)intPtr] + Lang.tip[2];
						}
						else
						{
							if (Main.toolTip.ranged)
							{
								array[num4] = string.Concat((int)(Main.player[Main.myPlayer].rangedDamage * (float)damage));
								string[] array6;
								IntPtr intPtr2;
								(array6 = array)[(int)(intPtr2 = (IntPtr)num4)] = array6[(int)intPtr2] + Lang.tip[3];
							}
							else
							{
								if (Main.toolTip.magic)
								{
									array[num4] = string.Concat((int)(Main.player[Main.myPlayer].magicDamage * (float)damage));
									string[] array7;
									IntPtr intPtr3;
									(array7 = array)[(int)(intPtr3 = (IntPtr)num4)] = array7[(int)intPtr3] + Lang.tip[4];
								}
								else
								{
									array[num4] = string.Concat(damage);
								}
							}
						}
						num4++;
						if (Main.toolTip.melee)
						{
							int num5 = Main.player[Main.myPlayer].meleeCrit - Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].crit + Main.toolTip.crit;
							array[num4] = num5 + Lang.tip[5];
							num4++;
						}
						else
						{
							if (Main.toolTip.ranged)
							{
								int num6 = Main.player[Main.myPlayer].rangedCrit - Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].crit + Main.toolTip.crit;
								array[num4] = num6 + Lang.tip[5];
								num4++;
							}
							else
							{
								if (Main.toolTip.magic)
								{
									int num7 = Main.player[Main.myPlayer].magicCrit - Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].crit + Main.toolTip.crit;
									array[num4] = num7 + Lang.tip[5];
									num4++;
								}
							}
						}
						if (Main.toolTip.useStyle > 0)
						{
							if (Main.toolTip.useAnimation <= 8)
							{
								array[num4] = Lang.tip[6];
							}
							else
							{
								if (Main.toolTip.useAnimation <= 20)
								{
									array[num4] = Lang.tip[7];
								}
								else
								{
									if (Main.toolTip.useAnimation <= 25)
									{
										array[num4] = Lang.tip[8];
									}
									else
									{
										if (Main.toolTip.useAnimation <= 30)
										{
											array[num4] = Lang.tip[9];
										}
										else
										{
											if (Main.toolTip.useAnimation <= 35)
											{
												array[num4] = Lang.tip[10];
											}
											else
											{
												if (Main.toolTip.useAnimation <= 45)
												{
													array[num4] = Lang.tip[11];
												}
												else
												{
													if (Main.toolTip.useAnimation <= 55)
													{
														array[num4] = Lang.tip[12];
													}
													else
													{
														array[num4] = Lang.tip[13];
													}
												}
											}
										}
									}
								}
							}
							num4++;
						}
						if (Main.toolTip.knockBack == 0f)
						{
							array[num4] = Lang.tip[14];
						}
						else
						{
							if ((double)Main.toolTip.knockBack <= 1.5)
							{
								array[num4] = Lang.tip[15];
							}
							else
							{
								if (Main.toolTip.knockBack <= 3f)
								{
									array[num4] = Lang.tip[16];
								}
								else
								{
									if (Main.toolTip.knockBack <= 4f)
									{
										array[num4] = Lang.tip[17];
									}
									else
									{
										if (Main.toolTip.knockBack <= 6f)
										{
											array[num4] = Lang.tip[18];
										}
										else
										{
											if (Main.toolTip.knockBack <= 7f)
											{
												array[num4] = Lang.tip[19];
											}
											else
											{
												if (Main.toolTip.knockBack <= 9f)
												{
													array[num4] = Lang.tip[20];
												}
												else
												{
													if (Main.toolTip.knockBack <= 11f)
													{
														array[num4] = Lang.tip[21];
													}
													else
													{
														array[num4] = Lang.tip[22];
													}
												}
											}
										}
									}
								}
							}
						}
						num4++;
					}
					if (Main.toolTip.headSlot > 0 || Main.toolTip.bodySlot > 0 || Main.toolTip.legSlot > 0 || Main.toolTip.accessory)
					{
						array[num4] = Lang.tip[23];
						num4++;
					}
					if (Main.toolTip.vanity)
					{
						array[num4] = Lang.tip[24];
						num4++;
					}
					if (Main.toolTip.defense > 0)
					{
						array[num4] = Main.toolTip.defense + Lang.tip[25];
						num4++;
					}
					if (Main.toolTip.pick > 0)
					{
						array[num4] = Main.toolTip.pick + Lang.tip[26];
						num4++;
					}
					if (Main.toolTip.axe > 0)
					{
						array[num4] = Main.toolTip.axe * 5 + Lang.tip[27];
						num4++;
					}
					if (Main.toolTip.hammer > 0)
					{
						array[num4] = Main.toolTip.hammer + Lang.tip[28];
						num4++;
					}
					if (Main.toolTip.healLife > 0)
					{
						array[num4] = string.Concat(new object[]
						{
							Lang.tip[29],
							" ",
							Main.toolTip.healLife,
							" ",
							Lang.tip[30]
						});
						num4++;
					}
					if (Main.toolTip.healMana > 0)
					{
						array[num4] = string.Concat(new object[]
						{
							Lang.tip[29],
							" ",
							Main.toolTip.healMana,
							" ",
							Lang.tip[31]
						});
						num4++;
					}
					if (Main.toolTip.mana > 0 && (Main.toolTip.type != 127 || !Main.player[Main.myPlayer].spaceGun))
					{
						array[num4] = string.Concat(new object[]
						{
							Lang.tip[32],
							" ",
							(int)((float)Main.toolTip.mana * Main.player[Main.myPlayer].manaCost),
							" ",
							Lang.tip[31]
						});
						num4++;
					}
					if (Main.toolTip.createWall > 0 || Main.toolTip.createTile > -1)
					{
						if (Main.toolTip.type != 213)
						{
							array[num4] = Lang.tip[33];
							num4++;
						}
					}
					else
					{
						if (Main.toolTip.ammo > 0)
						{
							array[num4] = Lang.tip[34];
							num4++;
						}
						else
						{
							if (Main.toolTip.consumable)
							{
								array[num4] = Lang.tip[35];
								num4++;
							}
						}
					}
					if (Main.toolTip.material)
					{
						array[num4] = Lang.tip[36];
						num4++;
					}
					if (Main.toolTip.toolTip != null)
					{
						array[num4] = Main.toolTip.toolTip;
						num4++;
					}
					if (Main.toolTip.toolTip2 != null)
					{
						array[num4] = Main.toolTip.toolTip2;
						num4++;
					}
					if (Main.toolTip.buffTime > 0)
					{
						string text;
						if (Main.toolTip.buffTime / 60 >= 60)
						{
							text = Math.Round((double)(Main.toolTip.buffTime / 60) / 60.0) + Lang.tip[37];
						}
						else
						{
							text = Math.Round((double)Main.toolTip.buffTime / 60.0) + Lang.tip[38];
						}
						array[num4] = text;
						num4++;
					}
					if (Main.toolTip.prefix > 0)
					{
						if (Main.cpItem == null || Main.cpItem.netID != Main.toolTip.netID)
						{
							Main.cpItem = new Item();
							Main.cpItem.netDefaults(Main.toolTip.netID);
						}
						if (Main.cpItem.damage != Main.toolTip.damage)
						{
							double num8 = (double)((float)Main.toolTip.damage - (float)Main.cpItem.damage);
							num8 = num8 / (double)((float)Main.cpItem.damage) * 100.0;
							num8 = Math.Round(num8);
							if (num8 > 0.0)
							{
								array[num4] = "+" + num8 + Lang.tip[39];
							}
							else
							{
								array[num4] = num8 + Lang.tip[39];
							}
							if (num8 < 0.0)
							{
								array3[num4] = true;
							}
							array2[num4] = true;
							num4++;
						}
						if (Main.cpItem.useAnimation != Main.toolTip.useAnimation)
						{
							double num9 = (double)((float)Main.toolTip.useAnimation - (float)Main.cpItem.useAnimation);
							num9 = num9 / (double)((float)Main.cpItem.useAnimation) * 100.0;
							num9 = Math.Round(num9);
							num9 *= -1.0;
							if (num9 > 0.0)
							{
								array[num4] = "+" + num9 + Lang.tip[40];
							}
							else
							{
								array[num4] = num9 + Lang.tip[40];
							}
							if (num9 < 0.0)
							{
								array3[num4] = true;
							}
							array2[num4] = true;
							num4++;
						}
						if (Main.cpItem.crit != Main.toolTip.crit)
						{
							double num10 = (double)((float)Main.toolTip.crit - (float)Main.cpItem.crit);
							if (num10 > 0.0)
							{
								array[num4] = "+" + num10 + Lang.tip[41];
							}
							else
							{
								array[num4] = num10 + Lang.tip[41];
							}
							if (num10 < 0.0)
							{
								array3[num4] = true;
							}
							array2[num4] = true;
							num4++;
						}
						if (Main.cpItem.mana != Main.toolTip.mana)
						{
							double num11 = (double)((float)Main.toolTip.mana - (float)Main.cpItem.mana);
							num11 = num11 / (double)((float)Main.cpItem.mana) * 100.0;
							num11 = Math.Round(num11);
							if (num11 > 0.0)
							{
								array[num4] = "+" + num11 + Lang.tip[42];
							}
							else
							{
								array[num4] = num11 + Lang.tip[42];
							}
							if (num11 > 0.0)
							{
								array3[num4] = true;
							}
							array2[num4] = true;
							num4++;
						}
						if (Main.cpItem.scale != Main.toolTip.scale)
						{
							double num12 = (double)(Main.toolTip.scale - Main.cpItem.scale);
							num12 = num12 / (double)Main.cpItem.scale * 100.0;
							num12 = Math.Round(num12);
							if (num12 > 0.0)
							{
								array[num4] = "+" + num12 + Lang.tip[43];
							}
							else
							{
								array[num4] = num12 + Lang.tip[43];
							}
							if (num12 < 0.0)
							{
								array3[num4] = true;
							}
							array2[num4] = true;
							num4++;
						}
						if (Main.cpItem.shootSpeed != Main.toolTip.shootSpeed)
						{
							double num13 = (double)(Main.toolTip.shootSpeed - Main.cpItem.shootSpeed);
							num13 = num13 / (double)Main.cpItem.shootSpeed * 100.0;
							num13 = Math.Round(num13);
							if (num13 > 0.0)
							{
								array[num4] = "+" + num13 + Lang.tip[44];
							}
							else
							{
								array[num4] = num13 + Lang.tip[44];
							}
							if (num13 < 0.0)
							{
								array3[num4] = true;
							}
							array2[num4] = true;
							num4++;
						}
						if (Main.cpItem.knockBack != Main.toolTip.knockBack)
						{
							double num14 = (double)(Main.toolTip.knockBack - Main.cpItem.knockBack);
							num14 = num14 / (double)Main.cpItem.knockBack * 100.0;
							num14 = Math.Round(num14);
							if (num14 > 0.0)
							{
								array[num4] = "+" + num14 + Lang.tip[45];
							}
							else
							{
								array[num4] = num14 + Lang.tip[45];
							}
							if (num14 < 0.0)
							{
								array3[num4] = true;
							}
							array2[num4] = true;
							num4++;
						}
						if (Main.toolTip.prefix == 62)
						{
							array[num4] = "+1" + Lang.tip[25];
							array2[num4] = true;
							num4++;
						}
						if (Main.toolTip.prefix == 63)
						{
							array[num4] = "+2" + Lang.tip[25];
							array2[num4] = true;
							num4++;
						}
						if (Main.toolTip.prefix == 64)
						{
							array[num4] = "+3" + Lang.tip[25];
							array2[num4] = true;
							num4++;
						}
						if (Main.toolTip.prefix == 65)
						{
							array[num4] = "+4" + Lang.tip[25];
							array2[num4] = true;
							num4++;
						}
						if (Main.toolTip.prefix == 66)
						{
							array[num4] = "+20 " + Lang.tip[31];
							array2[num4] = true;
							num4++;
						}
						if (Main.toolTip.prefix == 67)
						{
							array[num4] = "+1% " + Lang.tip[5];
							array2[num4] = true;
							num4++;
						}
						if (Main.toolTip.prefix == 68)
						{
							array[num4] = "+2% " + Lang.tip[5];
							array2[num4] = true;
							num4++;
						}
						if (Main.toolTip.prefix == 69)
						{
							array[num4] = "+1" + Lang.tip[39];
							array2[num4] = true;
							num4++;
						}
						if (Main.toolTip.prefix == 70)
						{
							array[num4] = "+2" + Lang.tip[39];
							array2[num4] = true;
							num4++;
						}
						if (Main.toolTip.prefix == 71)
						{
							array[num4] = "+3" + Lang.tip[39];
							array2[num4] = true;
							num4++;
						}
						if (Main.toolTip.prefix == 72)
						{
							array[num4] = "+4" + Lang.tip[39];
							array2[num4] = true;
							num4++;
						}
						if (Main.toolTip.prefix == 73)
						{
							array[num4] = "+1" + Lang.tip[46];
							array2[num4] = true;
							num4++;
						}
						if (Main.toolTip.prefix == 74)
						{
							array[num4] = "+2" + Lang.tip[46];
							array2[num4] = true;
							num4++;
						}
						if (Main.toolTip.prefix == 75)
						{
							array[num4] = "+3" + Lang.tip[46];
							array2[num4] = true;
							num4++;
						}
						if (Main.toolTip.prefix == 76)
						{
							array[num4] = "+4" + Lang.tip[46];
							array2[num4] = true;
							num4++;
						}
						if (Main.toolTip.prefix == 77)
						{
							array[num4] = "+1" + Lang.tip[47];
							array2[num4] = true;
							num4++;
						}
						if (Main.toolTip.prefix == 78)
						{
							array[num4] = "+2" + Lang.tip[47];
							array2[num4] = true;
							num4++;
						}
						if (Main.toolTip.prefix == 79)
						{
							array[num4] = "+3" + Lang.tip[47];
							array2[num4] = true;
							num4++;
						}
						if (Main.toolTip.prefix == 80)
						{
							array[num4] = "+4" + Lang.tip[47];
							array2[num4] = true;
							num4++;
						}
					}
					if (Main.toolTip.wornArmor && Main.player[Main.myPlayer].setBonus != "")
					{
						array[num4] = Lang.tip[48] + " " + Main.player[Main.myPlayer].setBonus;
						num4++;
					}
				}
				if (Main.npcShop > 0)
				{
					if (Main.toolTip.value > 0)
					{
						string text2 = "";
						int num15 = 0;
						int num16 = 0;
						int num17 = 0;
						int num18 = 0;
						int num19 = Main.toolTip.value * Main.toolTip.stack;
						if (!Main.toolTip.buy)
						{
							num19 = Main.toolTip.value / 5 * Main.toolTip.stack;
						}
						if (num19 < 1)
						{
							num19 = 1;
						}
						if (num19 >= 1000000)
						{
							num15 = num19 / 1000000;
							num19 -= num15 * 1000000;
						}
						if (num19 >= 10000)
						{
							num16 = num19 / 10000;
							num19 -= num16 * 10000;
						}
						if (num19 >= 100)
						{
							num17 = num19 / 100;
							num19 -= num17 * 100;
						}
						if (num19 >= 1)
						{
							num18 = num19;
						}
						if (num15 > 0)
						{
							object obj2 = text2;
							text2 = string.Concat(new object[]
							{
								obj2,
								num15,
								" ",
								Lang.inter[15]
							});
						}
						if (num16 > 0)
						{
							object obj3 = text2;
							text2 = string.Concat(new object[]
							{
								obj3,
								num16,
								" ",
								Lang.inter[16]
							});
						}
						if (num17 > 0)
						{
							object obj4 = text2;
							text2 = string.Concat(new object[]
							{
								obj4,
								num17,
								" ",
								Lang.inter[17]
							});
						}
						if (num18 > 0)
						{
							object obj = text2;
							text2 = string.Concat(new object[]
							{
								obj,
								num18,
								" ",
								Lang.inter[18]
							});
						}
						if (!Main.toolTip.buy)
						{
							array[num4] = Lang.tip[49] + " " + text2;
						}
						else
						{
							array[num4] = Lang.tip[50] + " " + text2;
						}
						num4++;
						num20 = (float)Main.mouseTextColor / 255f;
						if (num15 > 0)
						{
							color = new Color((int)((byte)(220f * num20)), (int)((byte)(220f * num20)), (int)((byte)(198f * num20)), (int)Main.mouseTextColor);
						}
						else
						{
							if (num16 > 0)
							{
								color = new Color((int)((byte)(224f * num20)), (int)((byte)(201f * num20)), (int)((byte)(92f * num20)), (int)Main.mouseTextColor);
							}
							else
							{
								if (num17 > 0)
								{
									color = new Color((int)((byte)(181f * num20)), (int)((byte)(192f * num20)), (int)((byte)(193f * num20)), (int)Main.mouseTextColor);
								}
								else
								{
									if (num18 > 0)
									{
										color = new Color((int)((byte)(246f * num20)), (int)((byte)(138f * num20)), (int)((byte)(96f * num20)), (int)Main.mouseTextColor);
									}
								}
							}
						}
					}
					else
					{
						num20 = (float)Main.mouseTextColor / 255f;
						array[num4] = Lang.tip[51];
						num4++;
						color = new Color((int)((byte)(120f * num20)), (int)((byte)(120f * num20)), (int)((byte)(120f * num20)), (int)Main.mouseTextColor);
					}
				}
				Vector2 vector = default(Vector2);
				int num21 = 0;
				for (int j = 0; j < num4; j++)
				{
					Vector2 vector2 = Main.fontMouseText.MeasureString(array[j]);
					if (vector2.X > vector.X)
					{
						vector.X = vector2.X;
					}
					vector.Y += vector2.Y + (float)num21;
				}
				if ((float)num + vector.X + 4f > (float)Main.screenWidth)
				{
					num = (int)((float)Main.screenWidth - vector.X - 4f);
				}
				if ((float)num2 + vector.Y + 4f > (float)Main.screenHeight)
				{
					num2 = (int)((float)Main.screenHeight - vector.Y - 4f);
				}
				int num22 = 0;
				num20 = (float)Main.mouseTextColor / 255f;
				for (int k = 0; k < num4; k++)
				{
					for (int l = 0; l < 5; l++)
					{
						int num23 = num;
						int num24 = num2 + num22;
						Color color2 = Color.Black;
						if (l == 0)
						{
							num23 -= 2;
						}
						else
						{
							if (l == 1)
							{
								num23 += 2;
							}
							else
							{
								if (l == 2)
								{
									num24 -= 2;
								}
								else
								{
									if (l == 3)
									{
										num24 += 2;
									}
									else
									{
										color2 = new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor);
										if (k == 0)
										{
											if (rare == -1)
											{
												color2 = new Color((int)((byte)(130f * num20)), (int)((byte)(130f * num20)), (int)((byte)(130f * num20)), (int)Main.mouseTextColor);
											}
											if (rare == 1)
											{
												color2 = new Color((int)((byte)(150f * num20)), (int)((byte)(150f * num20)), (int)((byte)(255f * num20)), (int)Main.mouseTextColor);
											}
											if (rare == 2)
											{
												color2 = new Color((int)((byte)(150f * num20)), (int)((byte)(255f * num20)), (int)((byte)(150f * num20)), (int)Main.mouseTextColor);
											}
											if (rare == 3)
											{
												color2 = new Color((int)((byte)(255f * num20)), (int)((byte)(200f * num20)), (int)((byte)(150f * num20)), (int)Main.mouseTextColor);
											}
											if (rare == 4)
											{
												color2 = new Color((int)((byte)(255f * num20)), (int)((byte)(150f * num20)), (int)((byte)(150f * num20)), (int)Main.mouseTextColor);
											}
											if (rare == 5)
											{
												color2 = new Color((int)((byte)(255f * num20)), (int)((byte)(150f * num20)), (int)((byte)(255f * num20)), (int)Main.mouseTextColor);
											}
											if (rare == 6)
											{
												color2 = new Color((int)((byte)(210f * num20)), (int)((byte)(160f * num20)), (int)((byte)(255f * num20)), (int)Main.mouseTextColor);
											}
											if (diff == 1)
											{
												color2 = new Color((int)((byte)((float)Main.mcColor.R * num20)), (int)((byte)((float)Main.mcColor.G * num20)), (int)((byte)((float)Main.mcColor.B * num20)), (int)Main.mouseTextColor);
											}
											if (diff == 2)
											{
												color2 = new Color((int)((byte)((float)Main.hcColor.R * num20)), (int)((byte)((float)Main.hcColor.G * num20)), (int)((byte)((float)Main.hcColor.B * num20)), (int)Main.mouseTextColor);
											}
										}
										else
										{
											if (array2[k])
											{
												if (array3[k])
												{
													color2 = new Color((int)((byte)(190f * num20)), (int)((byte)(120f * num20)), (int)((byte)(120f * num20)), (int)Main.mouseTextColor);
												}
												else
												{
													color2 = new Color((int)((byte)(120f * num20)), (int)((byte)(190f * num20)), (int)((byte)(120f * num20)), (int)Main.mouseTextColor);
												}
											}
											else
											{
												if (k == num4 - 1)
												{
													color2 = color;
												}
											}
										}
									}
								}
							}
						}
						this.spriteBatch.DrawString(Main.fontMouseText, array[k], new Vector2((float)num23, (float)num24), color2, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
					}
					num22 += (int)(Main.fontMouseText.MeasureString(array[k]).Y + (float)num21);
				}
				return;
			}
			if (Main.buffString != "" && Main.buffString != null)
			{
				for (int m = 0; m < 5; m++)
				{
					int num25 = num;
					int num26 = num2 + (int)Main.fontMouseText.MeasureString(Main.buffString).Y;
					Color black = Color.Black;
					if (m == 0)
					{
						num25 -= 2;
					}
					else
					{
						if (m == 1)
						{
							num25 += 2;
						}
						else
						{
							if (m == 2)
							{
								num26 -= 2;
							}
							else
							{
								if (m == 3)
								{
									num26 += 2;
								}
								else
								{
									black = new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor);
								}
							}
						}
					}
					this.spriteBatch.DrawString(Main.fontMouseText, Main.buffString, new Vector2((float)num25, (float)num26), black, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
				}
			}
			Vector2 vector3 = Main.fontMouseText.MeasureString(cursorText);
			if ((float)num + vector3.X + 4f > (float)Main.screenWidth)
			{
				num = (int)((float)Main.screenWidth - vector3.X - 4f);
			}
			if ((float)num2 + vector3.Y + 4f > (float)Main.screenHeight)
			{
				num2 = (int)((float)Main.screenHeight - vector3.Y - 4f);
			}
			this.spriteBatch.DrawString(Main.fontMouseText, cursorText, new Vector2((float)num, (float)(num2 - 2)), Color.Black, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
			this.spriteBatch.DrawString(Main.fontMouseText, cursorText, new Vector2((float)num, (float)(num2 + 2)), Color.Black, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
			this.spriteBatch.DrawString(Main.fontMouseText, cursorText, new Vector2((float)(num - 2), (float)num2), Color.Black, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
			this.spriteBatch.DrawString(Main.fontMouseText, cursorText, new Vector2((float)(num + 2), (float)num2), Color.Black, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
			num20 = (float)Main.mouseTextColor / 255f;
			Color color3 = new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor);
			if (rare == -1)
			{
				color3 = new Color((int)((byte)(130f * num20)), (int)((byte)(130f * num20)), (int)((byte)(130f * num20)), (int)Main.mouseTextColor);
			}
			if (rare == 6)
			{
				color3 = new Color((int)((byte)(210f * num20)), (int)((byte)(160f * num20)), (int)((byte)(255f * num20)), (int)Main.mouseTextColor);
			}
			if (rare == 1)
			{
				color3 = new Color((int)((byte)(150f * num20)), (int)((byte)(150f * num20)), (int)((byte)(255f * num20)), (int)Main.mouseTextColor);
			}
			if (rare == 2)
			{
				color3 = new Color((int)((byte)(150f * num20)), (int)((byte)(255f * num20)), (int)((byte)(150f * num20)), (int)Main.mouseTextColor);
			}
			if (rare == 3)
			{
				color3 = new Color((int)((byte)(255f * num20)), (int)((byte)(200f * num20)), (int)((byte)(150f * num20)), (int)Main.mouseTextColor);
			}
			if (rare == 4)
			{
				color3 = new Color((int)((byte)(255f * num20)), (int)((byte)(150f * num20)), (int)((byte)(150f * num20)), (int)Main.mouseTextColor);
			}
			if (rare == 5)
			{
				color3 = new Color((int)((byte)(255f * num20)), (int)((byte)(150f * num20)), (int)((byte)(255f * num20)), (int)Main.mouseTextColor);
			}
			if (diff == 1)
			{
				color3 = new Color((int)((byte)((float)Main.mcColor.R * num20)), (int)((byte)((float)Main.mcColor.G * num20)), (int)((byte)((float)Main.mcColor.B * num20)), (int)Main.mouseTextColor);
			}
			if (diff == 2)
			{
				color3 = new Color((int)((byte)((float)Main.hcColor.R * num20)), (int)((byte)((float)Main.hcColor.G * num20)), (int)((byte)((float)Main.hcColor.B * num20)), (int)Main.mouseTextColor);
			}
			this.spriteBatch.DrawString(Main.fontMouseText, cursorText, new Vector2((float)num, (float)num2), color3, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
		}
		protected void DrawFPS()
		{
			if (Main.showFrameRate)
			{
				string text = string.Concat(Main.frameRate);
				object obj = text;
				text = string.Concat(new object[]
				{
					obj,
					" (",
					(int)(Main.gfxQuality * 100f),
					"%)"
				});
				int num = 4;
				if (!Main.gameMenu)
				{
					num = Main.screenHeight - 24;
				}
				this.spriteBatch.DrawString(Main.fontMouseText, text + " " + Main.debugWords, new Vector2(4f, (float)num), new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
			}
		}
		public static Color shine(Color newColor, int type)
		{
			int num = (int)newColor.R;
			int num2 = (int)newColor.R;
			int num3 = (int)newColor.R;
			float num4 = 0.6f;
			if (type == 25)
			{
				num = (int)((float)newColor.R * 0.95f);
				num2 = (int)((float)newColor.G * 0.85f);
				num3 = (int)((double)((float)newColor.B) * 1.1);
			}
			else
			{
				if (type == 117)
				{
					num = (int)((float)newColor.R * 1.1f);
					num2 = (int)((float)newColor.G * 1f);
					num3 = (int)((double)((float)newColor.B) * 1.2);
				}
				else
				{
					num = (int)((float)newColor.R * (1f + num4));
					num2 = (int)((float)newColor.G * (1f + num4));
					num3 = (int)((float)newColor.B * (1f + num4));
				}
			}
			if (num > 255)
			{
				num = 255;
			}
			if (num2 > 255)
			{
				num2 = 255;
			}
			if (num3 > 255)
			{
				num3 = 255;
			}
			newColor.R = (byte)num;
			newColor.G = (byte)num2;
			newColor.B = (byte)num3;
			return new Color((int)((byte)num), (int)((byte)num2), (int)((byte)num3), (int)newColor.A);
		}
		protected void DrawTiles(bool solidOnly = true)
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			int num = (int)(255f * (1f - Main.gfxQuality) + 30f * Main.gfxQuality);
			int num2 = (int)(50f * (1f - Main.gfxQuality) + 2f * Main.gfxQuality);
			Vector2 value = new Vector2((float)Main.offScreenRange, (float)Main.offScreenRange);
			if (Main.drawToScreen)
			{
				value = default(Vector2);
			}
			int num3 = 0;
			int[] array = new int[1000];
			int[] array2 = new int[1000];
			int num4 = (int)((Main.screenPosition.X - value.X) / 16f - 1f);
			int num5 = (int)((Main.screenPosition.X + (float)Main.screenWidth + value.X) / 16f) + 2;
			int num6 = (int)((Main.screenPosition.Y - value.Y) / 16f - 1f);
			int num7 = (int)((Main.screenPosition.Y + (float)Main.screenHeight + value.Y) / 16f) + 5;
			if (num4 < 0)
			{
				num4 = 0;
			}
			if (num5 > Main.maxTilesX)
			{
				num5 = Main.maxTilesX;
			}
			if (num6 < 0)
			{
				num6 = 0;
			}
			if (num7 > Main.maxTilesY)
			{
				num7 = Main.maxTilesY;
			}
			for (int i = num6; i < num7 + 4; i++)
			{
				for (int j = num4 - 2; j < num5 + 2; j++)
				{
					if (Main.tile[j, i] == null)
					{
						Main.tile[j, i] = new Tile();
					}
					bool flag = Main.tileSolid[(int)Main.tile[j, i].type];
					if (Main.tile[j, i].type == 11)
					{
						flag = true;
					}
					if (Main.tile[j, i].active && flag == solidOnly)
					{
						Color color = Lighting.GetColor(j, i);
						int num8 = 0;
						if (Main.tile[j, i].type == 78 || Main.tile[j, i].type == 85)
						{
							num8 = 2;
						}
						if (Main.tile[j, i].type == 33 || Main.tile[j, i].type == 49)
						{
							num8 = -4;
						}
						int height;
						if (Main.tile[j, i].type == 3 || Main.tile[j, i].type == 4 || Main.tile[j, i].type == 5 || Main.tile[j, i].type == 24 || Main.tile[j, i].type == 33 || Main.tile[j, i].type == 49 || Main.tile[j, i].type == 61 || Main.tile[j, i].type == 71 || Main.tile[j, i].type == 110)
						{
							height = 20;
						}
						else
						{
							if (Main.tile[j, i].type == 15 || Main.tile[j, i].type == 14 || Main.tile[j, i].type == 16 || Main.tile[j, i].type == 17 || Main.tile[j, i].type == 18 || Main.tile[j, i].type == 20 || Main.tile[j, i].type == 21 || Main.tile[j, i].type == 26 || Main.tile[j, i].type == 27 || Main.tile[j, i].type == 32 || Main.tile[j, i].type == 69 || Main.tile[j, i].type == 72 || Main.tile[j, i].type == 77 || Main.tile[j, i].type == 80)
							{
								height = 18;
							}
							else
							{
								if (Main.tile[j, i].type == 137)
								{
									height = 18;
								}
								else
								{
									if (Main.tile[j, i].type == 135)
									{
										num8 = 2;
										height = 18;
									}
									else
									{
										if (Main.tile[j, i].type == 132)
										{
											num8 = 2;
											height = 18;
										}
										else
										{
											height = 16;
										}
									}
								}
							}
						}
						int num9;
						if (Main.tile[j, i].type == 4 || Main.tile[j, i].type == 5)
						{
							num9 = 20;
						}
						else
						{
							num9 = 16;
						}
						if (Main.tile[j, i].type == 73 || Main.tile[j, i].type == 74 || Main.tile[j, i].type == 113)
						{
							num8 -= 12;
							height = 32;
						}
						if (Main.tile[j, i].type == 81)
						{
							num8 -= 8;
							height = 26;
							num9 = 24;
						}
						if (Main.tile[j, i].type == 105)
						{
							num8 = 2;
						}
						if (Main.tile[j, i].type == 124)
						{
							height = 18;
						}
						if (Main.tile[j, i].type == 137)
						{
							height = 18;
						}
						if (Main.tile[j, i].type == 138)
						{
							height = 18;
						}
						if (Main.tile[j, i].type == 139 || Main.tile[j, i].type == 142 || Main.tile[j, i].type == 143)
						{
							num8 = 2;
						}
						if (Main.player[Main.myPlayer].findTreasure && (Main.tile[j, i].type == 6 || Main.tile[j, i].type == 7 || Main.tile[j, i].type == 8 || Main.tile[j, i].type == 9 || Main.tile[j, i].type == 12 || Main.tile[j, i].type == 21 || Main.tile[j, i].type == 22 || Main.tile[j, i].type == 28 || Main.tile[j, i].type == 107 || Main.tile[j, i].type == 108 || Main.tile[j, i].type == 111 || (Main.tile[j, i].type >= 63 && Main.tile[j, i].type <= 68) || Main.tileAlch[(int)Main.tile[j, i].type]))
						{
							if (color.R < Main.mouseTextColor / 2)
							{
								color.R = (byte)(Main.mouseTextColor / 2);
							}
							if (color.G < 70)
							{
								color.G = 70;
							}
							if (color.B < 210)
							{
								color.B = 210;
							}
							color.A = Main.mouseTextColor;
							if (!Main.gamePaused && base.IsActive && Main.rand.Next(150) == 0)
							{
								int num10 = Dust.NewDust(new Vector2((float)(j * 16), (float)(i * 16)), 16, 16, 15, 0f, 0f, 150, default(Color), 0.8f);
								Main.dust[num10].velocity *= 0.1f;
								Main.dust[num10].noLight = true;
							}
						}
						if (!Main.gamePaused && base.IsActive)
						{
							if (Main.tile[j, i].type == 4 && Main.rand.Next(40) == 0 && Main.tile[j, i].frameX < 66)
							{
								int num11 = (int)(Main.tile[j, i].frameY / 22);
								if (num11 == 0)
								{
									num11 = 6;
								}
								else
								{
									if (num11 == 8)
									{
										num11 = 75;
									}
									else
									{
										num11 = 58 + num11;
									}
								}
								if (Main.tile[j, i].frameX == 22)
								{
									Dust.NewDust(new Vector2((float)(j * 16 + 6), (float)(i * 16)), 4, 4, num11, 0f, 0f, 100, default(Color), 1f);
								}
								if (Main.tile[j, i].frameX == 44)
								{
									Dust.NewDust(new Vector2((float)(j * 16 + 2), (float)(i * 16)), 4, 4, num11, 0f, 0f, 100, default(Color), 1f);
								}
								else
								{
									Dust.NewDust(new Vector2((float)(j * 16 + 4), (float)(i * 16)), 4, 4, num11, 0f, 0f, 100, default(Color), 1f);
								}
							}
							if (Main.tile[j, i].type == 33 && Main.rand.Next(40) == 0 && Main.tile[j, i].frameX == 0)
							{
								Dust.NewDust(new Vector2((float)(j * 16 + 4), (float)(i * 16 - 4)), 4, 4, 6, 0f, 0f, 100, default(Color), 1f);
							}
							if (Main.tile[j, i].type == 93 && Main.rand.Next(40) == 0 && Main.tile[j, i].frameX == 0 && Main.tile[j, i].frameY == 0)
							{
								Dust.NewDust(new Vector2((float)(j * 16 + 4), (float)(i * 16 + 2)), 4, 4, 6, 0f, 0f, 100, default(Color), 1f);
							}
							if (Main.tile[j, i].type == 100 && Main.rand.Next(40) == 0 && Main.tile[j, i].frameX < 36 && Main.tile[j, i].frameY == 0)
							{
								if (Main.tile[j, i].frameX == 0)
								{
									if (Main.rand.Next(3) == 0)
									{
										Dust.NewDust(new Vector2((float)(j * 16 + 4), (float)(i * 16 + 2)), 4, 4, 6, 0f, 0f, 100, default(Color), 1f);
									}
									else
									{
										Dust.NewDust(new Vector2((float)(j * 16 + 14), (float)(i * 16 + 2)), 4, 4, 6, 0f, 0f, 100, default(Color), 1f);
									}
								}
								else
								{
									if (Main.rand.Next(3) == 0)
									{
										Dust.NewDust(new Vector2((float)(j * 16 + 6), (float)(i * 16 + 2)), 4, 4, 6, 0f, 0f, 100, default(Color), 1f);
									}
									else
									{
										Dust.NewDust(new Vector2((float)(j * 16), (float)(i * 16 + 2)), 4, 4, 6, 0f, 0f, 100, default(Color), 1f);
									}
								}
							}
							if (Main.tile[j, i].type == 98 && Main.rand.Next(40) == 0 && Main.tile[j, i].frameY == 0 && Main.tile[j, i].frameX == 0)
							{
								Dust.NewDust(new Vector2((float)(j * 16 + 12), (float)(i * 16 + 2)), 4, 4, 6, 0f, 0f, 100, default(Color), 1f);
							}
							if (Main.tile[j, i].type == 49 && Main.rand.Next(20) == 0)
							{
								Dust.NewDust(new Vector2((float)(j * 16 + 4), (float)(i * 16 - 4)), 4, 4, 29, 0f, 0f, 100, default(Color), 1f);
							}
							if ((Main.tile[j, i].type == 34 || Main.tile[j, i].type == 35 || Main.tile[j, i].type == 36) && Main.rand.Next(40) == 0 && Main.tile[j, i].frameX < 54 && Main.tile[j, i].frameY == 18 && (Main.tile[j, i].frameX == 0 || Main.tile[j, i].frameX == 36))
							{
								Dust.NewDust(new Vector2((float)(j * 16), (float)(i * 16 + 2)), 14, 6, 6, 0f, 0f, 100, default(Color), 1f);
							}
							if (Main.tile[j, i].type == 22 && Main.rand.Next(400) == 0)
							{
								Dust.NewDust(new Vector2((float)(j * 16), (float)(i * 16)), 16, 16, 14, 0f, 0f, 0, default(Color), 1f);
							}
							else
							{
								if ((Main.tile[j, i].type == 23 || Main.tile[j, i].type == 24 || Main.tile[j, i].type == 32) && Main.rand.Next(500) == 0)
								{
									Dust.NewDust(new Vector2((float)(j * 16), (float)(i * 16)), 16, 16, 14, 0f, 0f, 0, default(Color), 1f);
								}
								else
								{
									if (Main.tile[j, i].type == 25 && Main.rand.Next(700) == 0)
									{
										Dust.NewDust(new Vector2((float)(j * 16), (float)(i * 16)), 16, 16, 14, 0f, 0f, 0, default(Color), 1f);
									}
									else
									{
										if (Main.tile[j, i].type == 112 && Main.rand.Next(700) == 0)
										{
											Dust.NewDust(new Vector2((float)(j * 16), (float)(i * 16)), 16, 16, 14, 0f, 0f, 0, default(Color), 1f);
										}
										else
										{
											if (Main.tile[j, i].type == 31 && Main.rand.Next(20) == 0)
											{
												Dust.NewDust(new Vector2((float)(j * 16), (float)(i * 16)), 16, 16, 14, 0f, 0f, 100, default(Color), 1f);
											}
											else
											{
												if (Main.tile[j, i].type == 26 && Main.rand.Next(20) == 0)
												{
													Dust.NewDust(new Vector2((float)(j * 16), (float)(i * 16)), 16, 16, 14, 0f, 0f, 100, default(Color), 1f);
												}
												else
												{
													if ((Main.tile[j, i].type == 71 || Main.tile[j, i].type == 72) && Main.rand.Next(500) == 0)
													{
														Dust.NewDust(new Vector2((float)(j * 16), (float)(i * 16)), 16, 16, 41, 0f, 0f, 250, default(Color), 0.8f);
													}
													else
													{
														if ((Main.tile[j, i].type == 17 || Main.tile[j, i].type == 77 || Main.tile[j, i].type == 133) && Main.rand.Next(40) == 0)
														{
															if (Main.tile[j, i].frameX == 18 & Main.tile[j, i].frameY == 18)
															{
																Dust.NewDust(new Vector2((float)(j * 16 + 2), (float)(i * 16)), 8, 6, 6, 0f, 0f, 100, default(Color), 1f);
															}
														}
														else
														{
															if (Main.tile[j, i].type == 37 && Main.rand.Next(250) == 0)
															{
																int num12 = Dust.NewDust(new Vector2((float)(j * 16), (float)(i * 16)), 16, 16, 6, 0f, 0f, 0, default(Color), (float)Main.rand.Next(3));
																if (Main.dust[num12].scale > 1f)
																{
																	Main.dust[num12].noGravity = true;
																}
															}
															else
															{
																if ((Main.tile[j, i].type == 58 || Main.tile[j, i].type == 76) && Main.rand.Next(250) == 0)
																{
																	int num13 = Dust.NewDust(new Vector2((float)(j * 16), (float)(i * 16)), 16, 16, 6, 0f, 0f, 0, default(Color), (float)Main.rand.Next(3));
																	if (Main.dust[num13].scale > 1f)
																	{
																		Main.dust[num13].noGravity = true;
																	}
																	Main.dust[num13].noLight = true;
																}
																else
																{
																	if (Main.tile[j, i].type == 61)
																	{
																		if (Main.tile[j, i].frameX == 144)
																		{
																			if (Main.rand.Next(60) == 0)
																			{
																				int num14 = Dust.NewDust(new Vector2((float)(j * 16), (float)(i * 16)), 16, 16, 44, 0f, 0f, 250, default(Color), 0.4f);
																				Main.dust[num14].fadeIn = 0.7f;
																			}
																			color.A = (byte)(245f - (float)Main.mouseTextColor * 1.5f);
																			color.R = (byte)(245f - (float)Main.mouseTextColor * 1.5f);
																			color.B = (byte)(245f - (float)Main.mouseTextColor * 1.5f);
																			color.G = (byte)(245f - (float)Main.mouseTextColor * 1.5f);
																		}
																	}
																	else
																	{
																		if (Main.tileShine[(int)Main.tile[j, i].type] > 0 && (color.R > 20 || color.B > 20 || color.G > 20))
																		{
																			int num15 = (int)color.R;
																			if ((int)color.G > num15)
																			{
																				num15 = (int)color.G;
																			}
																			if ((int)color.B > num15)
																			{
																				num15 = (int)color.B;
																			}
																			num15 /= 30;
																			if (Main.rand.Next(Main.tileShine[(int)Main.tile[j, i].type]) < num15 && (Main.tile[j, i].type != 21 || (Main.tile[j, i].frameX >= 36 && Main.tile[j, i].frameX < 180)))
																			{
																				int num16 = Dust.NewDust(new Vector2((float)(j * 16), (float)(i * 16)), 16, 16, 43, 0f, 0f, 254, default(Color), 0.5f);
																				Main.dust[num16].velocity *= 0f;
																			}
																		}
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
						if (Main.tile[j, i].type == 128 && Main.tile[j, i].frameX >= 100)
						{
							array[num3] = j;
							array2[num3] = i;
							num3++;
						}
						if (Main.tile[j, i].type == 5 && Main.tile[j, i].frameY >= 198 && Main.tile[j, i].frameX >= 22)
						{
							array[num3] = j;
							array2[num3] = i;
							num3++;
						}
						if (Main.tile[j, i].type == 72 && Main.tile[j, i].frameX >= 36)
						{
							int num17 = 0;
							if (Main.tile[j, i].frameY == 18)
							{
								num17 = 1;
							}
							else
							{
								if (Main.tile[j, i].frameY == 36)
								{
									num17 = 2;
								}
							}
							this.spriteBatch.Draw(Main.shroomCapTexture, new Vector2((float)(j * 16 - (int)Main.screenPosition.X - 22), (float)(i * 16 - (int)Main.screenPosition.Y - 26)) + value, new Rectangle?(new Rectangle(num17 * 62, 0, 60, 42)), Lighting.GetColor(j, i), 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
						}
						if (color.R > 1 || color.G > 1 || color.B > 1)
						{
							if (Main.tile[j - 1, i] == null)
							{
								Main.tile[j - 1, i] = new Tile();
							}
							if (Main.tile[j + 1, i] == null)
							{
								Main.tile[j + 1, i] = new Tile();
							}
							if (Main.tile[j, i - 1] == null)
							{
								Main.tile[j, i - 1] = new Tile();
							}
							if (Main.tile[j, i + 1] == null)
							{
								Main.tile[j, i + 1] = new Tile();
							}
							if (solidOnly && flag && !Main.tileSolidTop[(int)Main.tile[j, i].type] && (Main.tile[j - 1, i].liquid > 0 || Main.tile[j + 1, i].liquid > 0 || Main.tile[j, i - 1].liquid > 0 || Main.tile[j, i + 1].liquid > 0))
							{
								Color color2 = Lighting.GetColor(j, i);
								int num18 = 0;
								bool flag2 = false;
								bool flag3 = false;
								bool flag4 = false;
								bool flag5 = false;
								int num19 = 0;
								bool flag6 = false;
								if ((int)Main.tile[j - 1, i].liquid > num18)
								{
									num18 = (int)Main.tile[j - 1, i].liquid;
									flag2 = true;
								}
								else
								{
									if (Main.tile[j - 1, i].liquid > 0)
									{
										flag2 = true;
									}
								}
								if ((int)Main.tile[j + 1, i].liquid > num18)
								{
									num18 = (int)Main.tile[j + 1, i].liquid;
									flag3 = true;
								}
								else
								{
									if (Main.tile[j + 1, i].liquid > 0)
									{
										num18 = (int)Main.tile[j + 1, i].liquid;
										flag3 = true;
									}
								}
								if (Main.tile[j, i - 1].liquid > 0)
								{
									flag4 = true;
								}
								if (Main.tile[j, i + 1].liquid > 240)
								{
									flag5 = true;
								}
								if (Main.tile[j - 1, i].liquid > 0)
								{
									if (Main.tile[j - 1, i].lava)
									{
										num19 = 1;
									}
									else
									{
										flag6 = true;
									}
								}
								if (Main.tile[j + 1, i].liquid > 0)
								{
									if (Main.tile[j + 1, i].lava)
									{
										num19 = 1;
									}
									else
									{
										flag6 = true;
									}
								}
								if (Main.tile[j, i - 1].liquid > 0)
								{
									if (Main.tile[j, i - 1].lava)
									{
										num19 = 1;
									}
									else
									{
										flag6 = true;
									}
								}
								if (Main.tile[j, i + 1].liquid > 0)
								{
									if (Main.tile[j, i + 1].lava)
									{
										num19 = 1;
									}
									else
									{
										flag6 = true;
									}
								}
								if (!flag6 || num19 != 1)
								{
									Vector2 value2 = new Vector2((float)(j * 16), (float)(i * 16));
									Rectangle value3 = new Rectangle(0, 4, 16, 16);
									if (flag5 && (flag2 || flag3))
									{
										flag2 = true;
										flag3 = true;
									}
									if ((!flag4 || (!flag2 && !flag3)) && (!flag5 || !flag4))
									{
										if (flag4)
										{
											value3 = new Rectangle(0, 4, 16, 4);
										}
										else
										{
											if (flag5 && !flag2 && !flag3)
											{
												value2 = new Vector2((float)(j * 16), (float)(i * 16 + 12));
												value3 = new Rectangle(0, 4, 16, 4);
											}
											else
											{
												float num20 = (float)(256 - num18);
												num20 /= 32f;
												if (flag2 && flag3)
												{
													value2 = new Vector2((float)(j * 16), (float)(i * 16 + (int)num20 * 2));
													value3 = new Rectangle(0, 4, 16, 16 - (int)num20 * 2);
												}
												else
												{
													if (flag2)
													{
														value2 = new Vector2((float)(j * 16), (float)(i * 16 + (int)num20 * 2));
														value3 = new Rectangle(0, 4, 4, 16 - (int)num20 * 2);
													}
													else
													{
														value2 = new Vector2((float)(j * 16 + 12), (float)(i * 16 + (int)num20 * 2));
														value3 = new Rectangle(0, 4, 4, 16 - (int)num20 * 2);
													}
												}
											}
										}
									}
									float num21 = 0.5f;
									if (num19 == 1)
									{
										num21 *= 1.6f;
									}
									if ((double)i < Main.worldSurface || num21 > 1f)
									{
										num21 = 1f;
									}
									float num22 = (float)color2.R * num21;
									float num23 = (float)color2.G * num21;
									float num24 = (float)color2.B * num21;
									float num25 = (float)color2.A * num21;
									color2 = new Color((int)((byte)num22), (int)((byte)num23), (int)((byte)num24), (int)((byte)num25));
									this.spriteBatch.Draw(Main.liquidTexture[num19], value2 - Main.screenPosition + value, new Rectangle?(value3), color2, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
								}
							}
							if (Main.tile[j, i].type == 51)
							{
								Color color3 = Lighting.GetColor(j, i);
								float num26 = 0.5f;
								float num27 = (float)color3.R * num26;
								float num28 = (float)color3.G * num26;
								float num29 = (float)color3.B * num26;
								float num30 = (float)color3.A * num26;
								color3 = new Color((int)((byte)num27), (int)((byte)num28), (int)((byte)num29), (int)((byte)num30));
								this.spriteBatch.Draw(Main.tileTexture[(int)Main.tile[j, i].type], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num9 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.Y + num8)) + value, new Rectangle?(new Rectangle((int)Main.tile[j, i].frameX, (int)Main.tile[j, i].frameY, num9, height)), color3, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
							}
							else
							{
								if (Main.tile[j, i].type == 129)
								{
									this.spriteBatch.Draw(Main.tileTexture[(int)Main.tile[j, i].type], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num9 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.Y + num8)) + value, new Rectangle?(new Rectangle((int)Main.tile[j, i].frameX, (int)Main.tile[j, i].frameY, num9, height)), new Color(200, 200, 200, 0), 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
								}
								else
								{
									if (Main.tileAlch[(int)Main.tile[j, i].type])
									{
										height = 20;
										num8 = -1;
										int num31 = (int)Main.tile[j, i].type;
										int num32 = (int)(Main.tile[j, i].frameX / 18);
										if (num31 > 82)
										{
											if (num32 == 0 && Main.dayTime)
											{
												num31 = 84;
											}
											if (num32 == 1 && !Main.dayTime)
											{
												num31 = 84;
											}
											if (num32 == 3 && Main.bloodMoon)
											{
												num31 = 84;
											}
										}
										if (num31 == 84)
										{
											if (num32 == 0 && Main.rand.Next(100) == 0)
											{
												int num33 = Dust.NewDust(new Vector2((float)(j * 16), (float)(i * 16 - 4)), 16, 16, 19, 0f, 0f, 160, default(Color), 0.1f);
												Dust expr_1F90_cp_0 = Main.dust[num33];
												expr_1F90_cp_0.velocity.X = expr_1F90_cp_0.velocity.X / 2f;
												Dust expr_1FAE_cp_0 = Main.dust[num33];
												expr_1FAE_cp_0.velocity.Y = expr_1FAE_cp_0.velocity.Y / 2f;
												Main.dust[num33].noGravity = true;
												Main.dust[num33].fadeIn = 1f;
											}
											if (num32 == 1 && Main.rand.Next(100) == 0)
											{
												Dust.NewDust(new Vector2((float)(j * 16), (float)(i * 16)), 16, 16, 41, 0f, 0f, 250, default(Color), 0.8f);
											}
											if (num32 == 3)
											{
												if (Main.rand.Next(200) == 0)
												{
													int num34 = Dust.NewDust(new Vector2((float)(j * 16), (float)(i * 16)), 16, 16, 14, 0f, 0f, 100, default(Color), 0.2f);
													Main.dust[num34].fadeIn = 1.2f;
												}
												if (Main.rand.Next(75) == 0)
												{
													int num35 = Dust.NewDust(new Vector2((float)(j * 16), (float)(i * 16)), 16, 16, 27, 0f, 0f, 100, default(Color), 1f);
													Dust expr_20E5_cp_0 = Main.dust[num35];
													expr_20E5_cp_0.velocity.X = expr_20E5_cp_0.velocity.X / 2f;
													Dust expr_2103_cp_0 = Main.dust[num35];
													expr_2103_cp_0.velocity.Y = expr_2103_cp_0.velocity.Y / 2f;
												}
											}
											if (num32 == 4 && Main.rand.Next(150) == 0)
											{
												int num36 = Dust.NewDust(new Vector2((float)(j * 16), (float)(i * 16)), 16, 8, 16, 0f, 0f, 0, default(Color), 1f);
												Dust expr_2174_cp_0 = Main.dust[num36];
												expr_2174_cp_0.velocity.X = expr_2174_cp_0.velocity.X / 3f;
												Dust expr_2192_cp_0 = Main.dust[num36];
												expr_2192_cp_0.velocity.Y = expr_2192_cp_0.velocity.Y / 3f;
												Dust expr_21B0_cp_0 = Main.dust[num36];
												expr_21B0_cp_0.velocity.Y = expr_21B0_cp_0.velocity.Y - 0.7f;
												Main.dust[num36].alpha = 50;
												Main.dust[num36].scale *= 0.1f;
												Main.dust[num36].fadeIn = 0.9f;
												Main.dust[num36].noGravity = true;
											}
											if (num32 == 5)
											{
												if (Main.rand.Next(40) == 0)
												{
													int num37 = Dust.NewDust(new Vector2((float)(j * 16), (float)(i * 16 - 6)), 16, 16, 6, 0f, 0f, 0, default(Color), 1.5f);
													Dust expr_2265_cp_0 = Main.dust[num37];
													expr_2265_cp_0.velocity.Y = expr_2265_cp_0.velocity.Y - 2f;
													Main.dust[num37].noGravity = true;
												}
												color.A = (byte)(Main.mouseTextColor / 2);
												color.G = Main.mouseTextColor;
												color.B = Main.mouseTextColor;
											}
										}
										this.spriteBatch.Draw(Main.tileTexture[num31], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num9 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.Y + num8)) + value, new Rectangle?(new Rectangle((int)Main.tile[j, i].frameX, (int)Main.tile[j, i].frameY, num9, height)), color, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
									}
									else
									{
										if (Main.tile[j, i].type == 80)
										{
											bool flag7 = false;
											bool flag8 = false;
											int num38 = j;
											if (Main.tile[j, i].frameX == 36)
											{
												num38--;
											}
											if (Main.tile[j, i].frameX == 54)
											{
												num38++;
											}
											if (Main.tile[j, i].frameX == 108)
											{
												if (Main.tile[j, i].frameY == 16)
												{
													num38--;
												}
												else
												{
													num38++;
												}
											}
											int num39 = i;
											bool flag9 = false;
											if (Main.tile[num38, num39].type == 80 && Main.tile[num38, num39].active)
											{
												flag9 = true;
											}
											while (!Main.tile[num38, num39].active || !Main.tileSolid[(int)Main.tile[num38, num39].type] || !flag9)
											{
												if (Main.tile[num38, num39].type == 80 && Main.tile[num38, num39].active)
												{
													flag9 = true;
												}
												num39++;
												if (num39 > i + 20)
												{
													break;
												}
											}
											if (Main.tile[num38, num39].type == 112)
											{
												flag7 = true;
											}
											if (Main.tile[num38, num39].type == 116)
											{
												flag8 = true;
											}
											if (flag7)
											{
												this.spriteBatch.Draw(Main.evilCactusTexture, new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num9 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.Y + num8)) + value, new Rectangle?(new Rectangle((int)Main.tile[j, i].frameX, (int)Main.tile[j, i].frameY, num9, height)), color, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
											}
											else
											{
												if (flag8)
												{
													this.spriteBatch.Draw(Main.goodCactusTexture, new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num9 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.Y + num8)) + value, new Rectangle?(new Rectangle((int)Main.tile[j, i].frameX, (int)Main.tile[j, i].frameY, num9, height)), color, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
												}
												else
												{
													this.spriteBatch.Draw(Main.tileTexture[(int)Main.tile[j, i].type], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num9 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.Y + num8)) + value, new Rectangle?(new Rectangle((int)Main.tile[j, i].frameX, (int)Main.tile[j, i].frameY, num9, height)), color, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
												}
											}
										}
										else
										{
											if (Lighting.lightMode < 2 && Main.tileSolid[(int)Main.tile[j, i].type] && Main.tile[j, i].type != 137)
											{
												if ((int)color.R > num || (double)color.G > (double)num * 1.1 || (double)color.B > (double)num * 1.2)
												{
													for (int k = 0; k < 9; k++)
													{
														int num40 = 0;
														int num41 = 0;
														int width = 4;
														int height2 = 4;
														Color color4 = color;
														Color color5 = color;
														if (k == 0)
														{
															color5 = Lighting.GetColor(j - 1, i - 1);
														}
														if (k == 1)
														{
															width = 8;
															num40 = 4;
															color5 = Lighting.GetColor(j, i - 1);
														}
														if (k == 2)
														{
															color5 = Lighting.GetColor(j + 1, i - 1);
															num40 = 12;
														}
														if (k == 3)
														{
															color5 = Lighting.GetColor(j - 1, i);
															height2 = 8;
															num41 = 4;
														}
														if (k == 4)
														{
															width = 8;
															height2 = 8;
															num40 = 4;
															num41 = 4;
														}
														if (k == 5)
														{
															num40 = 12;
															num41 = 4;
															height2 = 8;
															color5 = Lighting.GetColor(j + 1, i);
														}
														if (k == 6)
														{
															color5 = Lighting.GetColor(j - 1, i + 1);
															num41 = 12;
														}
														if (k == 7)
														{
															width = 8;
															height2 = 4;
															num40 = 4;
															num41 = 12;
															color5 = Lighting.GetColor(j, i + 1);
														}
														if (k == 8)
														{
															color5 = Lighting.GetColor(j + 1, i + 1);
															num40 = 12;
															num41 = 12;
														}
														color4.R = (byte)((color.R + color5.R) / 2);
														color4.G = (byte)((color.G + color5.G) / 2);
														color4.B = (byte)((color.B + color5.B) / 2);
														if (Main.tileShine2[(int)Main.tile[j, i].type])
														{
															color4 = Main.shine(color4, (int)Main.tile[j, i].type);
														}
														this.spriteBatch.Draw(Main.tileTexture[(int)Main.tile[j, i].type], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num9 - 16f) / 2f + (float)num40, (float)(i * 16 - (int)Main.screenPosition.Y + num8 + num41)) + value, new Rectangle?(new Rectangle((int)Main.tile[j, i].frameX + num40, (int)Main.tile[j, i].frameY + num41, width, height2)), color4, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
													}
												}
												else
												{
													if ((int)color.R > num2 || (double)color.G > (double)num2 * 1.1 || (double)color.B > (double)num2 * 1.2)
													{
														for (int l = 0; l < 4; l++)
														{
															int num42 = 0;
															int num43 = 0;
															Color color6 = color;
															Color color7 = color;
															if (l == 0)
															{
																if (Lighting.Brighter(j, i - 1, j - 1, i))
																{
																	color7 = Lighting.GetColor(j - 1, i);
																}
																else
																{
																	color7 = Lighting.GetColor(j, i - 1);
																}
															}
															if (l == 1)
															{
																if (Lighting.Brighter(j, i - 1, j + 1, i))
																{
																	color7 = Lighting.GetColor(j + 1, i);
																}
																else
																{
																	color7 = Lighting.GetColor(j, i - 1);
																}
																num42 = 8;
															}
															if (l == 2)
															{
																if (Lighting.Brighter(j, i + 1, j - 1, i))
																{
																	color7 = Lighting.GetColor(j - 1, i);
																}
																else
																{
																	color7 = Lighting.GetColor(j, i + 1);
																}
																num43 = 8;
															}
															if (l == 3)
															{
																if (Lighting.Brighter(j, i + 1, j + 1, i))
																{
																	color7 = Lighting.GetColor(j + 1, i);
																}
																else
																{
																	color7 = Lighting.GetColor(j, i + 1);
																}
																num42 = 8;
																num43 = 8;
															}
															color6.R = (byte)((color.R + color7.R) / 2);
                                                            color6.G = (byte)((color.G + color7.G) / 2);
                                                            color6.B = (byte)((color.B + color7.B) / 2);
															if (Main.tileShine2[(int)Main.tile[j, i].type])
															{
																color6 = Main.shine(color6, (int)Main.tile[j, i].type);
															}
															this.spriteBatch.Draw(Main.tileTexture[(int)Main.tile[j, i].type], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num9 - 16f) / 2f + (float)num42, (float)(i * 16 - (int)Main.screenPosition.Y + num8 + num43)) + value, new Rectangle?(new Rectangle((int)Main.tile[j, i].frameX + num42, (int)Main.tile[j, i].frameY + num43, 8, 8)), color6, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
														}
													}
													else
													{
														if (Main.tileShine2[(int)Main.tile[j, i].type])
														{
															color = Main.shine(color, (int)Main.tile[j, i].type);
														}
														this.spriteBatch.Draw(Main.tileTexture[(int)Main.tile[j, i].type], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num9 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.Y + num8)) + value, new Rectangle?(new Rectangle((int)Main.tile[j, i].frameX, (int)Main.tile[j, i].frameY, num9, height)), color, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
													}
												}
											}
											else
											{
												if (Lighting.lightMode < 2 && Main.tileShine2[(int)Main.tile[j, i].type])
												{
													if (Main.tile[j, i].type == 21)
													{
														if (Main.tile[j, i].frameX >= 36 && Main.tile[j, i].frameX < 178)
														{
															color = Main.shine(color, (int)Main.tile[j, i].type);
														}
													}
													else
													{
														color = Main.shine(color, (int)Main.tile[j, i].type);
													}
												}
												if (Main.tile[j, i].type == 128)
												{
													int m;
													for (m = (int)Main.tile[j, i].frameX; m >= 100; m -= 100)
													{
													}
													this.spriteBatch.Draw(Main.tileTexture[(int)Main.tile[j, i].type], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num9 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.Y + num8)) + value, new Rectangle?(new Rectangle(m, (int)Main.tile[j, i].frameY, num9, height)), color, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
												}
												else
												{
													this.spriteBatch.Draw(Main.tileTexture[(int)Main.tile[j, i].type], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num9 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.Y + num8)) + value, new Rectangle?(new Rectangle((int)Main.tile[j, i].frameX, (int)Main.tile[j, i].frameY, num9, height)), color, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
													if (Main.tile[j, i].type == 139)
													{
														this.spriteBatch.Draw(Main.MusicBoxTexture, new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num9 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.Y + num8)) + value, new Rectangle?(new Rectangle((int)Main.tile[j, i].frameX, (int)Main.tile[j, i].frameY, num9, height)), new Color(200, 200, 200, 0), 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
													}
													if (Main.tile[j, i].type == 144)
													{
														this.spriteBatch.Draw(Main.timerTexture, new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num9 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.Y + num8)) + value, new Rectangle?(new Rectangle((int)Main.tile[j, i].frameX, (int)Main.tile[j, i].frameY, num9, height)), new Color(200, 200, 200, 0), 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			for (int n = 0; n < num3; n++)
			{
				int num44 = array[n];
				int num45 = array2[n];
				if (Main.tile[num44, num45].type == 128 && Main.tile[num44, num45].frameX >= 100)
				{
					int num46 = (int)(Main.tile[num44, num45].frameY / 18);
					int num47 = (int)Main.tile[num44, num45].frameX;
					int num48 = 0;
					while (num47 >= 100)
					{
						num48++;
						num47 -= 100;
					}
					int num49 = -4;
					SpriteEffects effects = SpriteEffects.FlipHorizontally;
					if (num47 >= 36)
					{
						effects = SpriteEffects.None;
						num49 = -4;
					}
					if (num46 == 0)
					{
						this.spriteBatch.Draw(Main.armorHeadTexture[num48], new Vector2((float)(num44 * 16 - (int)Main.screenPosition.X + num49), (float)(num45 * 16 - (int)Main.screenPosition.Y - 12)) + value, new Rectangle?(new Rectangle(0, 0, 40, 36)), Lighting.GetColor(num44, num45), 0f, default(Vector2), 1f, effects, 0f);
					}
					else
					{
						if (num46 == 1)
						{
							this.spriteBatch.Draw(Main.armorBodyTexture[num48], new Vector2((float)(num44 * 16 - (int)Main.screenPosition.X + num49), (float)(num45 * 16 - (int)Main.screenPosition.Y - 28)) + value, new Rectangle?(new Rectangle(0, 0, 40, 54)), Lighting.GetColor(num44, num45), 0f, default(Vector2), 1f, effects, 0f);
						}
						else
						{
							if (num46 == 2)
							{
								this.spriteBatch.Draw(Main.armorLegTexture[num48], new Vector2((float)(num44 * 16 - (int)Main.screenPosition.X + num49), (float)(num45 * 16 - (int)Main.screenPosition.Y - 44)) + value, new Rectangle?(new Rectangle(0, 0, 40, 54)), Lighting.GetColor(num44, num45), 0f, default(Vector2), 1f, effects, 0f);
							}
						}
					}
				}
				try
				{
					if (Main.tile[num44, num45].type == 5 && Main.tile[num44, num45].frameY >= 198 && Main.tile[num44, num45].frameX >= 22)
					{
						int num50 = 0;
						if (Main.tile[num44, num45].frameX == 22)
						{
							if (Main.tile[num44, num45].frameY == 220)
							{
								num50 = 1;
							}
							else
							{
								if (Main.tile[num44, num45].frameY == 242)
								{
									num50 = 2;
								}
							}
							int num51 = 0;
							int num52 = 80;
							int num53 = 80;
							int num54 = 32;
							int num55 = 0;
							int num56 = num45;
							while (num56 < num45 + 100)
							{
								if (Main.tile[num44, num56].type == 2)
								{
									num51 = 0;
									break;
								}
								if (Main.tile[num44, num56].type == 23)
								{
									num51 = 1;
									break;
								}
								if (Main.tile[num44, num56].type == 60)
								{
									num51 = 2;
									num52 = 114;
									num53 = 96;
									num54 = 48;
									break;
								}
								if (Main.tile[num44, num56].type == 147)
								{
									num51 = 4;
									break;
								}
								if (Main.tile[num44, num56].type == 109)
								{
									num51 = 3;
									num53 = 140;
									if (num44 % 3 == 1)
									{
										num50 += 3;
										break;
									}
									if (num44 % 3 == 2)
									{
										num50 += 6;
										break;
									}
									break;
								}
								else
								{
									num56++;
								}
							}
							this.spriteBatch.Draw(Main.treeTopTexture[num51], new Vector2((float)(num44 * 16 - (int)Main.screenPosition.X - num54), (float)(num45 * 16 - (int)Main.screenPosition.Y - num53 + 16 + num55)) + value, new Rectangle?(new Rectangle(num50 * (num52 + 2), 0, num52, num53)), Lighting.GetColor(num44, num45), 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
						}
						else
						{
							if (Main.tile[num44, num45].frameX == 44)
							{
								if (Main.tile[num44, num45].frameY == 220)
								{
									num50 = 1;
								}
								else
								{
									if (Main.tile[num44, num45].frameY == 242)
									{
										num50 = 2;
									}
								}
								int num57 = 0;
								int num58 = num45;
								while (num58 < num45 + 100)
								{
									if (Main.tile[num44 + 1, num58].type == 2)
									{
										num57 = 0;
										break;
									}
									if (Main.tile[num44 + 1, num58].type == 23)
									{
										num57 = 1;
										break;
									}
									if (Main.tile[num44 + 1, num58].type == 60)
									{
										num57 = 2;
										break;
									}
									if (Main.tile[num44 + 1, num58].type == 147)
									{
										num57 = 4;
										break;
									}
									if (Main.tile[num44 + 1, num58].type == 109)
									{
										num57 = 3;
										if (num44 % 3 == 1)
										{
											num50 += 3;
											break;
										}
										if (num44 % 3 == 2)
										{
											num50 += 6;
											break;
										}
										break;
									}
									else
									{
										num58++;
									}
								}
								this.spriteBatch.Draw(Main.treeBranchTexture[num57], new Vector2((float)(num44 * 16 - (int)Main.screenPosition.X - 24), (float)(num45 * 16 - (int)Main.screenPosition.Y - 12)) + value, new Rectangle?(new Rectangle(0, num50 * 42, 40, 40)), Lighting.GetColor(num44, num45), 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
							}
							else
							{
								if (Main.tile[num44, num45].frameX == 66)
								{
									if (Main.tile[num44, num45].frameY == 220)
									{
										num50 = 1;
									}
									else
									{
										if (Main.tile[num44, num45].frameY == 242)
										{
											num50 = 2;
										}
									}
									int num59 = 0;
									int num60 = num45;
									while (num60 < num45 + 100)
									{
										if (Main.tile[num44 - 1, num60].type == 2)
										{
											num59 = 0;
											break;
										}
										if (Main.tile[num44 - 1, num60].type == 23)
										{
											num59 = 1;
											break;
										}
										if (Main.tile[num44 - 1, num60].type == 60)
										{
											num59 = 2;
											break;
										}
										if (Main.tile[num44 - 1, num60].type == 147)
										{
											num59 = 4;
											break;
										}
										if (Main.tile[num44 - 1, num60].type == 109)
										{
											num59 = 3;
											if (num44 % 3 == 1)
											{
												num50 += 3;
												break;
											}
											if (num44 % 3 == 2)
											{
												num50 += 6;
												break;
											}
											break;
										}
										else
										{
											num60++;
										}
									}
									this.spriteBatch.Draw(Main.treeBranchTexture[num59], new Vector2((float)(num44 * 16 - (int)Main.screenPosition.X), (float)(num45 * 16 - (int)Main.screenPosition.Y - 12)) + value, new Rectangle?(new Rectangle(42, num50 * 42, 40, 40)), Lighting.GetColor(num44, num45), 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
								}
							}
						}
					}
				}
				catch
				{
				}
			}
			if (solidOnly)
			{
				Main.renderTimer[0] = (float)stopwatch.ElapsedMilliseconds;
				return;
			}
			Main.renderTimer[1] = (float)stopwatch.ElapsedMilliseconds;
		}
		protected void DrawWater(bool bg = false)
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			Vector2 value = new Vector2((float)Main.offScreenRange, (float)Main.offScreenRange);
			if (Main.drawToScreen)
			{
				value = default(Vector2);
			}
			int num = (int)(255f * (1f - Main.gfxQuality) + 40f * Main.gfxQuality);
			int num2 = (int)(255f * (1f - Main.gfxQuality) + 140f * Main.gfxQuality);
			float num3 = (float)Main.evilTiles / 350f;
			if (num3 > 1f)
			{
				num3 = 1f;
			}
			if (num3 < 0f)
			{
				num3 = 0f;
			}
			float num4 = (255f - 100f * num3) / 255f;
			float num5 = (255f - 50f * num3) / 255f;
			int num6 = (int)((Main.screenPosition.X - value.X) / 16f - 1f);
			int num7 = (int)((Main.screenPosition.X + (float)Main.screenWidth + value.X) / 16f) + 2;
			int num8 = (int)((Main.screenPosition.Y - value.Y) / 16f - 1f);
			int num9 = (int)((Main.screenPosition.Y + (float)Main.screenHeight + value.Y) / 16f) + 5;
			if (num6 < 5)
			{
				num6 = 5;
			}
			if (num7 > Main.maxTilesX - 5)
			{
				num7 = Main.maxTilesX - 5;
			}
			if (num8 < 5)
			{
				num8 = 5;
			}
			if (num9 > Main.maxTilesY - 5)
			{
				num9 = Main.maxTilesY - 5;
			}
			for (int i = num8; i < num9 + 4; i++)
			{
				for (int j = num6 - 2; j < num7 + 2; j++)
				{
					if (Main.tile[j, i] == null)
					{
						Main.tile[j, i] = new Tile();
					}
					if (Main.tile[j, i].liquid > 0 && (!Main.tile[j, i].active || !Main.tileSolid[(int)Main.tile[j, i].type] || Main.tileSolidTop[(int)Main.tile[j, i].type]) && (Lighting.Brightness(j, i) > 0f || bg))
					{
						Color color = Lighting.GetColor(j, i);
						float num10 = (float)(256 - (int)Main.tile[j, i].liquid);
						num10 /= 32f;
						int num11 = 0;
						if (Main.tile[j, i].lava)
						{
							num11 = 1;
						}
						float num12 = 0.5f;
						if (bg)
						{
							num12 = 1f;
						}
						Vector2 value2 = new Vector2((float)(j * 16), (float)(i * 16 + (int)num10 * 2));
						Rectangle value3 = new Rectangle(0, 0, 16, 16 - (int)num10 * 2);
						if (Main.tile[j, i + 1].liquid < 245 && (!Main.tile[j, i + 1].active || !Main.tileSolid[(int)Main.tile[j, i + 1].type] || Main.tileSolidTop[(int)Main.tile[j, i + 1].type]))
						{
							float num13 = (float)(256 - (int)Main.tile[j, i + 1].liquid);
							num13 /= 32f;
							num12 = 0.5f * (8f - num10) / 4f;
							if ((double)num12 > 0.55)
							{
								num12 = 0.55f;
							}
							if ((double)num12 < 0.35)
							{
								num12 = 0.35f;
							}
							float num14 = num10 / 2f;
							if (Main.tile[j, i + 1].liquid < 200)
							{
								if (bg)
								{
									goto IL_CF9;
								}
								if (Main.tile[j, i - 1].liquid > 0 && Main.tile[j, i - 1].liquid > 0)
								{
									value3 = new Rectangle(0, 4, 16, 16);
									num12 = 0.5f;
								}
								else
								{
									if (Main.tile[j, i - 1].liquid > 0)
									{
										value2 = new Vector2((float)(j * 16), (float)(i * 16 + 4));
										value3 = new Rectangle(0, 4, 16, 12);
										num12 = 0.5f;
									}
									else
									{
										if (Main.tile[j, i + 1].liquid > 0)
										{
											value2 = new Vector2((float)(j * 16), (float)(i * 16 + (int)num10 * 2 + (int)num13 * 2));
											value3 = new Rectangle(0, 4, 16, 16 - (int)num10 * 2);
										}
										else
										{
											value2 = new Vector2((float)(j * 16 + (int)num14), (float)(i * 16 + (int)num14 * 2 + (int)num13 * 2));
											value3 = new Rectangle(0, 4, 16 - (int)num14 * 2, 16 - (int)num14 * 2);
										}
									}
								}
							}
							else
							{
								num12 = 0.5f;
								value3 = new Rectangle(0, 4, 16, 16 - (int)num10 * 2 + (int)num13 * 2);
							}
						}
						else
						{
							if (Main.tile[j, i - 1].liquid > 32)
							{
								value3 = new Rectangle(0, 4, value3.Width, value3.Height);
							}
							else
							{
								if (num10 < 1f && Main.tile[j, i - 1].active && Main.tileSolid[(int)Main.tile[j, i - 1].type] && !Main.tileSolidTop[(int)Main.tile[j, i - 1].type])
								{
									value2 = new Vector2((float)(j * 16), (float)(i * 16));
									value3 = new Rectangle(0, 4, 16, 16);
								}
								else
								{
									bool flag = true;
									int num15 = i + 1;
									while (num15 < i + 6 && (!Main.tile[j, num15].active || !Main.tileSolid[(int)Main.tile[j, num15].type] || Main.tileSolidTop[(int)Main.tile[j, num15].type]))
									{
										if (Main.tile[j, num15].liquid < 200)
										{
											flag = false;
											break;
										}
										num15++;
									}
									if (!flag)
									{
										num12 = 0.5f;
										value3 = new Rectangle(0, 4, 16, 16);
									}
									else
									{
										if (Main.tile[j, i - 1].liquid > 0)
										{
											value3 = new Rectangle(0, 2, value3.Width, value3.Height);
										}
									}
								}
							}
						}
						if (Main.tile[j, i].lava)
						{
							num12 *= 1.8f;
							if (num12 > 1f)
							{
								num12 = 1f;
							}
							if (base.IsActive && !Main.gamePaused && Dust.lavaBubbles < 200)
							{
								if (Main.tile[j, i].liquid > 200 && Main.rand.Next(700) == 0)
								{
									Dust.NewDust(new Vector2((float)(j * 16), (float)(i * 16)), 16, 16, 35, 0f, 0f, 0, default(Color), 1f);
								}
								if (value3.Y == 0 && Main.rand.Next(350) == 0)
								{
									int num16 = Dust.NewDust(new Vector2((float)(j * 16), (float)(i * 16) + num10 * 2f - 8f), 16, 8, 35, 0f, 0f, 50, default(Color), 1.5f);
									Main.dust[num16].velocity *= 0.8f;
									Dust expr_7DC_cp_0 = Main.dust[num16];
									expr_7DC_cp_0.velocity.X = expr_7DC_cp_0.velocity.X * 2f;
									Dust expr_7FA_cp_0 = Main.dust[num16];
									expr_7FA_cp_0.velocity.Y = expr_7FA_cp_0.velocity.Y - (float)Main.rand.Next(1, 7) * 0.1f;
									if (Main.rand.Next(10) == 0)
									{
										Dust expr_834_cp_0 = Main.dust[num16];
										expr_834_cp_0.velocity.Y = expr_834_cp_0.velocity.Y * (float)Main.rand.Next(2, 5);
									}
									Main.dust[num16].noGravity = true;
								}
							}
						}
						float num17 = (float)color.R * num12;
						float num18 = (float)color.G * num12;
						float num19 = (float)color.B * num12;
						float num20 = (float)color.A * num12;
						if (num11 == 0)
						{
							num19 *= num4;
						}
						else
						{
							num17 *= num5;
						}
						color = new Color((int)((byte)num17), (int)((byte)num18), (int)((byte)num19), (int)((byte)num20));
						if (Lighting.lightMode < 2 && !bg)
						{
							Color color2 = color;
							if ((num11 == 0 && ((int)color2.R > num || (double)color2.G > (double)num * 1.1 || (double)color2.B > (double)num * 1.2)) || (int)color2.R > num2 || (double)color2.G > (double)num2 * 1.1 || (double)color2.B > (double)num2 * 1.2)
							{
								for (int k = 0; k < 4; k++)
								{
									int num21 = 0;
									int num22 = 0;
									int width = 8;
									int height = 8;
									Color color3 = color2;
									Color color4 = Lighting.GetColor(j, i);
									if (k == 0)
									{
										if (Lighting.Brighter(j, i - 1, j - 1, i))
										{
											if (!Main.tile[j - 1, i].active)
											{
												color4 = Lighting.GetColor(j - 1, i);
											}
											else
											{
												if (!Main.tile[j, i - 1].active)
												{
													color4 = Lighting.GetColor(j, i - 1);
												}
											}
										}
										if (value3.Height < 8)
										{
											height = value3.Height;
										}
									}
									if (k == 1)
									{
										if (Lighting.Brighter(j, i - 1, j + 1, i))
										{
											if (!Main.tile[j + 1, i].active)
											{
												color4 = Lighting.GetColor(j + 1, i);
											}
											else
											{
												if (!Main.tile[j, i - 1].active)
												{
													color4 = Lighting.GetColor(j, i - 1);
												}
											}
										}
										num21 = 8;
										if (value3.Height < 8)
										{
											height = value3.Height;
										}
									}
									if (k == 2)
									{
										if (Lighting.Brighter(j, i + 1, j - 1, i))
										{
											if (!Main.tile[j - 1, i].active)
											{
												color4 = Lighting.GetColor(j - 1, i);
											}
											else
											{
												if (!Main.tile[j, i + 1].active)
												{
													color4 = Lighting.GetColor(j, i + 1);
												}
											}
										}
										num22 = 8;
										height = 8 - (16 - value3.Height);
									}
									if (k == 3)
									{
										if (Lighting.Brighter(j, i + 1, j + 1, i))
										{
											if (!Main.tile[j + 1, i].active)
											{
												color4 = Lighting.GetColor(j + 1, i);
											}
											else
											{
												if (!Main.tile[j, i + 1].active)
												{
													color4 = Lighting.GetColor(j, i + 1);
												}
											}
										}
										num21 = 8;
										num22 = 8;
										height = 8 - (16 - value3.Height);
									}
									num17 = (float)color4.R * num12;
									num18 = (float)color4.G * num12;
									num19 = (float)color4.B * num12;
									num20 = (float)color4.A * num12;
									color4 = new Color((int)((byte)num17), (int)((byte)num18), (int)((byte)num19), (int)((byte)num20));
									color3.R = (byte)((color2.R + color4.R) / 2);
                                    color3.G = (byte)((color2.G + color4.G) / 2);
                                    color3.B = (byte)((color2.B + color4.B) / 2);
                                    color3.A = (byte)((color2.A + color4.A) / 2);
									this.spriteBatch.Draw(Main.liquidTexture[num11], value2 - Main.screenPosition + new Vector2((float)num21, (float)num22) + value, new Rectangle?(new Rectangle(value3.X + num21, value3.Y + num22, width, height)), color3, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
								}
							}
							else
							{
								this.spriteBatch.Draw(Main.liquidTexture[num11], value2 - Main.screenPosition + value, new Rectangle?(value3), color, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
							}
						}
						else
						{
							this.spriteBatch.Draw(Main.liquidTexture[num11], value2 - Main.screenPosition + value, new Rectangle?(value3), color, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
						}
					}
					IL_CF9:;
				}
			}
			if (!bg)
			{
				Main.renderTimer[4] = (float)stopwatch.ElapsedMilliseconds;
			}
		}
		protected void DrawGore()
		{
			for (int i = 0; i < 200; i++)
			{
				if (Main.gore[i].active && Main.gore[i].type > 0)
				{
					Color alpha = Main.gore[i].GetAlpha(Lighting.GetColor((int)((double)Main.gore[i].position.X + (double)Main.goreTexture[Main.gore[i].type].Width * 0.5) / 16, (int)(((double)Main.gore[i].position.Y + (double)Main.goreTexture[Main.gore[i].type].Height * 0.5) / 16.0)));
					this.spriteBatch.Draw(Main.goreTexture[Main.gore[i].type], new Vector2(Main.gore[i].position.X - Main.screenPosition.X + (float)(Main.goreTexture[Main.gore[i].type].Width / 2), Main.gore[i].position.Y - Main.screenPosition.Y + (float)(Main.goreTexture[Main.gore[i].type].Height / 2)), new Rectangle?(new Rectangle(0, 0, Main.goreTexture[Main.gore[i].type].Width, Main.goreTexture[Main.gore[i].type].Height)), alpha, Main.gore[i].rotation, new Vector2((float)(Main.goreTexture[Main.gore[i].type].Width / 2), (float)(Main.goreTexture[Main.gore[i].type].Height / 2)), Main.gore[i].scale, SpriteEffects.None, 0f);
				}
			}
		}
		protected void DrawNPCs(bool behindTiles = false)
		{
			bool flag = false;
			Rectangle rectangle = new Rectangle((int)Main.screenPosition.X - 300, (int)Main.screenPosition.Y - 300, Main.screenWidth + 600, Main.screenHeight + 600);
			for (int i = 199; i >= 0; i--)
			{
				if (Main.npc[i].active && Main.npc[i].type > 0 && Main.npc[i].behindTiles == behindTiles)
				{
					if ((Main.npc[i].type == 125 || Main.npc[i].type == 126) && !flag)
					{
						flag = true;
						for (int j = 0; j < 200; j++)
						{
							if (Main.npc[j].active && i != j && (Main.npc[j].type == 125 || Main.npc[j].type == 126))
							{
								float num = Main.npc[j].position.X + (float)Main.npc[j].width * 0.5f;
								float num2 = Main.npc[j].position.Y + (float)Main.npc[j].height * 0.5f;
								Vector2 vector = new Vector2(Main.npc[i].position.X + (float)Main.npc[i].width * 0.5f, Main.npc[i].position.Y + (float)Main.npc[i].height * 0.5f);
								float num3 = num - vector.X;
								float num4 = num2 - vector.Y;
								float rotation = (float)Math.Atan2((double)num4, (double)num3) - 1.57f;
								bool flag2 = true;
								float num5 = (float)Math.Sqrt((double)(num3 * num3 + num4 * num4));
								if (num5 > 2000f)
								{
									flag2 = false;
								}
								while (flag2)
								{
									num5 = (float)Math.Sqrt((double)(num3 * num3 + num4 * num4));
									if (num5 < 40f)
									{
										flag2 = false;
									}
									else
									{
										num5 = (float)Main.chain12Texture.Height / num5;
										num3 *= num5;
										num4 *= num5;
										vector.X += num3;
										vector.Y += num4;
										num3 = num - vector.X;
										num4 = num2 - vector.Y;
										Color color = Lighting.GetColor((int)vector.X / 16, (int)(vector.Y / 16f));
										this.spriteBatch.Draw(Main.chain12Texture, new Vector2(vector.X - Main.screenPosition.X, vector.Y - Main.screenPosition.Y), new Rectangle?(new Rectangle(0, 0, Main.chain12Texture.Width, Main.chain12Texture.Height)), color, rotation, new Vector2((float)Main.chain12Texture.Width * 0.5f, (float)Main.chain12Texture.Height * 0.5f), 1f, SpriteEffects.None, 0f);
									}
								}
							}
						}
					}
					if (rectangle.Intersects(new Rectangle((int)Main.npc[i].position.X, (int)Main.npc[i].position.Y, Main.npc[i].width, Main.npc[i].height)))
					{
						if (Main.npc[i].type == 101)
						{
							bool flag3 = true;
							Vector2 vector2 = new Vector2(Main.npc[i].position.X + (float)(Main.npc[i].width / 2), Main.npc[i].position.Y + (float)(Main.npc[i].height / 2));
							float num6 = Main.npc[i].ai[0] * 16f + 8f - vector2.X;
							float num7 = Main.npc[i].ai[1] * 16f + 8f - vector2.Y;
							float rotation2 = (float)Math.Atan2((double)num7, (double)num6) - 1.57f;
							bool flag4 = true;
							while (flag4)
							{
								float num8 = 0.75f;
								int height = 28;
								float num9 = (float)Math.Sqrt((double)(num6 * num6 + num7 * num7));
								if (num9 < 28f * num8)
								{
									height = (int)num9 - 40 + 28;
									flag4 = false;
								}
								num9 = 20f * num8 / num9;
								num6 *= num9;
								num7 *= num9;
								vector2.X += num6;
								vector2.Y += num7;
								num6 = Main.npc[i].ai[0] * 16f + 8f - vector2.X;
								num7 = Main.npc[i].ai[1] * 16f + 8f - vector2.Y;
								Color color2 = Lighting.GetColor((int)vector2.X / 16, (int)(vector2.Y / 16f));
								if (!flag3)
								{
									flag3 = true;
									this.spriteBatch.Draw(Main.chain10Texture, new Vector2(vector2.X - Main.screenPosition.X, vector2.Y - Main.screenPosition.Y), new Rectangle?(new Rectangle(0, 0, Main.chain10Texture.Width, height)), color2, rotation2, new Vector2((float)Main.chain10Texture.Width * 0.5f, (float)Main.chain10Texture.Height * 0.5f), num8, SpriteEffects.None, 0f);
								}
								else
								{
									flag3 = false;
									this.spriteBatch.Draw(Main.chain11Texture, new Vector2(vector2.X - Main.screenPosition.X, vector2.Y - Main.screenPosition.Y), new Rectangle?(new Rectangle(0, 0, Main.chain10Texture.Width, height)), color2, rotation2, new Vector2((float)Main.chain10Texture.Width * 0.5f, (float)Main.chain10Texture.Height * 0.5f), num8, SpriteEffects.None, 0f);
								}
							}
						}
						else
						{
							if (Main.npc[i].aiStyle == 13)
							{
								Vector2 vector3 = new Vector2(Main.npc[i].position.X + (float)(Main.npc[i].width / 2), Main.npc[i].position.Y + (float)(Main.npc[i].height / 2));
								float num10 = Main.npc[i].ai[0] * 16f + 8f - vector3.X;
								float num11 = Main.npc[i].ai[1] * 16f + 8f - vector3.Y;
								float rotation3 = (float)Math.Atan2((double)num11, (double)num10) - 1.57f;
								bool flag5 = true;
								while (flag5)
								{
									int height2 = 28;
									float num12 = (float)Math.Sqrt((double)(num10 * num10 + num11 * num11));
									if (num12 < 40f)
									{
										height2 = (int)num12 - 40 + 28;
										flag5 = false;
									}
									num12 = 28f / num12;
									num10 *= num12;
									num11 *= num12;
									vector3.X += num10;
									vector3.Y += num11;
									num10 = Main.npc[i].ai[0] * 16f + 8f - vector3.X;
									num11 = Main.npc[i].ai[1] * 16f + 8f - vector3.Y;
									Color color3 = Lighting.GetColor((int)vector3.X / 16, (int)(vector3.Y / 16f));
									if (Main.npc[i].type == 56)
									{
										this.spriteBatch.Draw(Main.chain5Texture, new Vector2(vector3.X - Main.screenPosition.X, vector3.Y - Main.screenPosition.Y), new Rectangle?(new Rectangle(0, 0, Main.chain4Texture.Width, height2)), color3, rotation3, new Vector2((float)Main.chain4Texture.Width * 0.5f, (float)Main.chain4Texture.Height * 0.5f), 1f, SpriteEffects.None, 0f);
									}
									else
									{
										this.spriteBatch.Draw(Main.chain4Texture, new Vector2(vector3.X - Main.screenPosition.X, vector3.Y - Main.screenPosition.Y), new Rectangle?(new Rectangle(0, 0, Main.chain4Texture.Width, height2)), color3, rotation3, new Vector2((float)Main.chain4Texture.Width * 0.5f, (float)Main.chain4Texture.Height * 0.5f), 1f, SpriteEffects.None, 0f);
									}
								}
							}
						}
						if (Main.npc[i].type == 36)
						{
							Vector2 vector4 = new Vector2(Main.npc[i].position.X + (float)Main.npc[i].width * 0.5f - 5f * Main.npc[i].ai[0], Main.npc[i].position.Y + 20f);
							for (int k = 0; k < 2; k++)
							{
								float num13 = Main.npc[(int)Main.npc[i].ai[1]].position.X + (float)(Main.npc[(int)Main.npc[i].ai[1]].width / 2) - vector4.X;
								float num14 = Main.npc[(int)Main.npc[i].ai[1]].position.Y + (float)(Main.npc[(int)Main.npc[i].ai[1]].height / 2) - vector4.Y;
								float num15;
								if (k == 0)
								{
									num13 -= 200f * Main.npc[i].ai[0];
									num14 += 130f;
									num15 = (float)Math.Sqrt((double)(num13 * num13 + num14 * num14));
									num15 = 92f / num15;
									vector4.X += num13 * num15;
									vector4.Y += num14 * num15;
								}
								else
								{
									num13 -= 50f * Main.npc[i].ai[0];
									num14 += 80f;
									num15 = (float)Math.Sqrt((double)(num13 * num13 + num14 * num14));
									num15 = 60f / num15;
									vector4.X += num13 * num15;
									vector4.Y += num14 * num15;
								}
								float rotation4 = (float)Math.Atan2((double)num14, (double)num13) - 1.57f;
								Color color4 = Lighting.GetColor((int)vector4.X / 16, (int)(vector4.Y / 16f));
								this.spriteBatch.Draw(Main.boneArmTexture, new Vector2(vector4.X - Main.screenPosition.X, vector4.Y - Main.screenPosition.Y), new Rectangle?(new Rectangle(0, 0, Main.boneArmTexture.Width, Main.boneArmTexture.Height)), color4, rotation4, new Vector2((float)Main.boneArmTexture.Width * 0.5f, (float)Main.boneArmTexture.Height * 0.5f), 1f, SpriteEffects.None, 0f);
								if (k == 0)
								{
									vector4.X += num13 * num15 / 2f;
									vector4.Y += num14 * num15 / 2f;
								}
								else
								{
									if (base.IsActive)
									{
										vector4.X += num13 * num15 - 16f;
										vector4.Y += num14 * num15 - 6f;
										int num16 = Dust.NewDust(new Vector2(vector4.X, vector4.Y), 30, 10, 5, num13 * 0.02f, num14 * 0.02f, 0, default(Color), 2f);
										Main.dust[num16].noGravity = true;
									}
								}
							}
						}
						if (Main.npc[i].aiStyle >= 33 && Main.npc[i].aiStyle <= 36)
						{
							Vector2 vector5 = new Vector2(Main.npc[i].position.X + (float)Main.npc[i].width * 0.5f - 5f * Main.npc[i].ai[0], Main.npc[i].position.Y + 20f);
							for (int l = 0; l < 2; l++)
							{
								float num17 = Main.npc[(int)Main.npc[i].ai[1]].position.X + (float)(Main.npc[(int)Main.npc[i].ai[1]].width / 2) - vector5.X;
								float num18 = Main.npc[(int)Main.npc[i].ai[1]].position.Y + (float)(Main.npc[(int)Main.npc[i].ai[1]].height / 2) - vector5.Y;
								float num19;
								if (l == 0)
								{
									num17 -= 200f * Main.npc[i].ai[0];
									num18 += 130f;
									num19 = (float)Math.Sqrt((double)(num17 * num17 + num18 * num18));
									num19 = 92f / num19;
									vector5.X += num17 * num19;
									vector5.Y += num18 * num19;
								}
								else
								{
									num17 -= 50f * Main.npc[i].ai[0];
									num18 += 80f;
									num19 = (float)Math.Sqrt((double)(num17 * num17 + num18 * num18));
									num19 = 60f / num19;
									vector5.X += num17 * num19;
									vector5.Y += num18 * num19;
								}
								float rotation5 = (float)Math.Atan2((double)num18, (double)num17) - 1.57f;
								Color color5 = Lighting.GetColor((int)vector5.X / 16, (int)(vector5.Y / 16f));
								this.spriteBatch.Draw(Main.boneArm2Texture, new Vector2(vector5.X - Main.screenPosition.X, vector5.Y - Main.screenPosition.Y), new Rectangle?(new Rectangle(0, 0, Main.boneArmTexture.Width, Main.boneArmTexture.Height)), color5, rotation5, new Vector2((float)Main.boneArmTexture.Width * 0.5f, (float)Main.boneArmTexture.Height * 0.5f), 1f, SpriteEffects.None, 0f);
								if (l == 0)
								{
									vector5.X += num17 * num19 / 2f;
									vector5.Y += num18 * num19 / 2f;
								}
								else
								{
									if (base.IsActive)
									{
										vector5.X += num17 * num19 - 16f;
										vector5.Y += num18 * num19 - 6f;
										int num20 = Dust.NewDust(new Vector2(vector5.X, vector5.Y), 30, 10, 6, num17 * 0.02f, num18 * 0.02f, 0, default(Color), 2.5f);
										Main.dust[num20].noGravity = true;
									}
								}
							}
						}
						if (Main.npc[i].aiStyle == 20)
						{
							Vector2 vector6 = new Vector2(Main.npc[i].position.X + (float)(Main.npc[i].width / 2), Main.npc[i].position.Y + (float)(Main.npc[i].height / 2));
							float num21 = Main.npc[i].ai[1] - vector6.X;
							float num22 = Main.npc[i].ai[2] - vector6.Y;
							float num23 = (float)Math.Atan2((double)num22, (double)num21) - 1.57f;
							Main.npc[i].rotation = num23;
							bool flag6 = true;
							while (flag6)
							{
								int height3 = 12;
								float num24 = (float)Math.Sqrt((double)(num21 * num21 + num22 * num22));
								if (num24 < 20f)
								{
									height3 = (int)num24 - 20 + 12;
									flag6 = false;
								}
								num24 = 12f / num24;
								num21 *= num24;
								num22 *= num24;
								vector6.X += num21;
								vector6.Y += num22;
								num21 = Main.npc[i].ai[1] - vector6.X;
								num22 = Main.npc[i].ai[2] - vector6.Y;
								Color color6 = Lighting.GetColor((int)vector6.X / 16, (int)(vector6.Y / 16f));
								this.spriteBatch.Draw(Main.chainTexture, new Vector2(vector6.X - Main.screenPosition.X, vector6.Y - Main.screenPosition.Y), new Rectangle?(new Rectangle(0, 0, Main.chainTexture.Width, height3)), color6, num23, new Vector2((float)Main.chainTexture.Width * 0.5f, (float)Main.chainTexture.Height * 0.5f), 1f, SpriteEffects.None, 0f);
							}
							this.spriteBatch.Draw(Main.spikeBaseTexture, new Vector2(Main.npc[i].ai[1] - Main.screenPosition.X, Main.npc[i].ai[2] - Main.screenPosition.Y), new Rectangle?(new Rectangle(0, 0, Main.spikeBaseTexture.Width, Main.spikeBaseTexture.Height)), Lighting.GetColor((int)Main.npc[i].ai[1] / 16, (int)(Main.npc[i].ai[2] / 16f)), num23 - 0.75f, new Vector2((float)Main.spikeBaseTexture.Width * 0.5f, (float)Main.spikeBaseTexture.Height * 0.5f), 1f, SpriteEffects.None, 0f);
						}
						Color color7 = Lighting.GetColor((int)((double)Main.npc[i].position.X + (double)Main.npc[i].width * 0.5) / 16, (int)(((double)Main.npc[i].position.Y + (double)Main.npc[i].height * 0.5) / 16.0));
						if (behindTiles && Main.npc[i].type != 113 && Main.npc[i].type != 114)
						{
							int num25 = (int)((Main.npc[i].position.X - 8f) / 16f);
							int num26 = (int)((Main.npc[i].position.X + (float)Main.npc[i].width + 8f) / 16f);
							int num27 = (int)((Main.npc[i].position.Y - 8f) / 16f);
							int num28 = (int)((Main.npc[i].position.Y + (float)Main.npc[i].height + 8f) / 16f);
							for (int m = num25; m <= num26; m++)
							{
								for (int n = num27; n <= num28; n++)
								{
									if (Lighting.Brightness(m, n) == 0f)
									{
										color7 = Color.Black;
									}
								}
							}
						}
						float num29 = 1f;
						float g = 1f;
						float num30 = 1f;
						float a = 1f;
						if (Main.npc[i].poisoned)
						{
							if (Main.rand.Next(30) == 0)
							{
								int num31 = Dust.NewDust(Main.npc[i].position, Main.npc[i].width, Main.npc[i].height, 46, 0f, 0f, 120, default(Color), 0.2f);
								Main.dust[num31].noGravity = true;
								Main.dust[num31].fadeIn = 1.9f;
							}
							num29 *= 0.65f;
							num30 *= 0.75f;
							color7 = Main.buffColor(color7, num29, g, num30, a);
						}
						if (Main.npc[i].onFire)
						{
							if (Main.rand.Next(4) < 3)
							{
								int num32 = Dust.NewDust(new Vector2(Main.npc[i].position.X - 2f, Main.npc[i].position.Y - 2f), Main.npc[i].width + 4, Main.npc[i].height + 4, 6, Main.npc[i].velocity.X * 0.4f, Main.npc[i].velocity.Y * 0.4f, 100, default(Color), 3.5f);
								Main.dust[num32].noGravity = true;
								Main.dust[num32].velocity *= 1.8f;
								Dust expr_15D0_cp_0 = Main.dust[num32];
								expr_15D0_cp_0.velocity.Y = expr_15D0_cp_0.velocity.Y - 0.5f;
								if (Main.rand.Next(4) == 0)
								{
									Main.dust[num32].noGravity = false;
									Main.dust[num32].scale *= 0.5f;
								}
							}
							Lighting.addLight((int)(Main.npc[i].position.X / 16f), (int)(Main.npc[i].position.Y / 16f + 1f), 1f, 0.3f, 0.1f);
						}
						if (Main.npc[i].onFire2)
						{
							if (Main.rand.Next(4) < 3)
							{
								int num33 = Dust.NewDust(new Vector2(Main.npc[i].position.X - 2f, Main.npc[i].position.Y - 2f), Main.npc[i].width + 4, Main.npc[i].height + 4, 75, Main.npc[i].velocity.X * 0.4f, Main.npc[i].velocity.Y * 0.4f, 100, default(Color), 3.5f);
								Main.dust[num33].noGravity = true;
								Main.dust[num33].velocity *= 1.8f;
								Dust expr_1750_cp_0 = Main.dust[num33];
								expr_1750_cp_0.velocity.Y = expr_1750_cp_0.velocity.Y - 0.5f;
								if (Main.rand.Next(4) == 0)
								{
									Main.dust[num33].noGravity = false;
									Main.dust[num33].scale *= 0.5f;
								}
							}
							Lighting.addLight((int)(Main.npc[i].position.X / 16f), (int)(Main.npc[i].position.Y / 16f + 1f), 1f, 0.3f, 0.1f);
						}
						if (Main.player[Main.myPlayer].detectCreature && Main.npc[i].lifeMax > 1)
						{
							if (color7.R < 150)
							{
								color7.A = Main.mouseTextColor;
							}
							if (color7.R < 50)
							{
								color7.R = 50;
							}
							if (color7.G < 200)
							{
								color7.G = 200;
							}
							if (color7.B < 100)
							{
								color7.B = 100;
							}
							if (!Main.gamePaused && base.IsActive && Main.rand.Next(50) == 0)
							{
								int num34 = Dust.NewDust(new Vector2(Main.npc[i].position.X, Main.npc[i].position.Y), Main.npc[i].width, Main.npc[i].height, 15, 0f, 0f, 150, default(Color), 0.8f);
								Main.dust[num34].velocity *= 0.1f;
								Main.dust[num34].noLight = true;
							}
						}
						if (Main.npc[i].type == 50)
						{
							Vector2 vector7 = default(Vector2);
							float num35 = 0f;
							vector7.Y -= Main.npc[i].velocity.Y;
							vector7.X -= Main.npc[i].velocity.X * 2f;
							num35 += Main.npc[i].velocity.X * 0.05f;
							if (Main.npc[i].frame.Y == 120)
							{
								vector7.Y += 2f;
							}
							if (Main.npc[i].frame.Y == 360)
							{
								vector7.Y -= 2f;
							}
							if (Main.npc[i].frame.Y == 480)
							{
								vector7.Y -= 6f;
							}
							this.spriteBatch.Draw(Main.ninjaTexture, new Vector2(Main.npc[i].position.X - Main.screenPosition.X + (float)(Main.npc[i].width / 2) + vector7.X, Main.npc[i].position.Y - Main.screenPosition.Y + (float)(Main.npc[i].height / 2) + vector7.Y), new Rectangle?(new Rectangle(0, 0, Main.ninjaTexture.Width, Main.ninjaTexture.Height)), color7, num35, new Vector2((float)(Main.ninjaTexture.Width / 2), (float)(Main.ninjaTexture.Height / 2)), 1f, SpriteEffects.None, 0f);
						}
						if (Main.npc[i].type == 71)
						{
							Vector2 vector8 = default(Vector2);
							float num36 = 0f;
							vector8.Y -= Main.npc[i].velocity.Y * 0.3f;
							vector8.X -= Main.npc[i].velocity.X * 0.6f;
							num36 += Main.npc[i].velocity.X * 0.09f;
							if (Main.npc[i].frame.Y == 120)
							{
								vector8.Y += 2f;
							}
							if (Main.npc[i].frame.Y == 360)
							{
								vector8.Y -= 2f;
							}
							if (Main.npc[i].frame.Y == 480)
							{
								vector8.Y -= 6f;
							}
							this.spriteBatch.Draw(Main.itemTexture[327], new Vector2(Main.npc[i].position.X - Main.screenPosition.X + (float)(Main.npc[i].width / 2) + vector8.X, Main.npc[i].position.Y - Main.screenPosition.Y + (float)(Main.npc[i].height / 2) + vector8.Y), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[327].Width, Main.itemTexture[327].Height)), color7, num36, new Vector2((float)(Main.itemTexture[327].Width / 2), (float)(Main.itemTexture[327].Height / 2)), 1f, SpriteEffects.None, 0f);
						}
						if (Main.npc[i].type == 69)
						{
							this.spriteBatch.Draw(Main.antLionTexture, new Vector2(Main.npc[i].position.X - Main.screenPosition.X + (float)(Main.npc[i].width / 2), Main.npc[i].position.Y - Main.screenPosition.Y + (float)Main.npc[i].height + 14f), new Rectangle?(new Rectangle(0, 0, Main.antLionTexture.Width, Main.antLionTexture.Height)), color7, -Main.npc[i].rotation * 0.3f, new Vector2((float)(Main.antLionTexture.Width / 2), (float)(Main.antLionTexture.Height / 2)), 1f, SpriteEffects.None, 0f);
						}
						float num37 = 0f;
						float num38 = 0f;
						Vector2 origin = new Vector2((float)(Main.npcTexture[Main.npc[i].type].Width / 2), (float)(Main.npcTexture[Main.npc[i].type].Height / Main.npcFrameCount[Main.npc[i].type] / 2));
						if (Main.npc[i].type == 108 || Main.npc[i].type == 124)
						{
							num37 = 2f;
						}
						if (Main.npc[i].type == 4)
						{
							origin = new Vector2(55f, 107f);
						}
						else
						{
							if (Main.npc[i].type == 125)
							{
								origin = new Vector2(55f, 107f);
								num38 = 30f;
							}
							else
							{
								if (Main.npc[i].type == 126)
								{
									origin = new Vector2(55f, 107f);
									num38 = 30f;
								}
								else
								{
									if (Main.npc[i].type == 6)
									{
										num38 = 26f;
									}
									else
									{
										if (Main.npc[i].type == 94)
										{
											num38 = 14f;
										}
										else
										{
											if (Main.npc[i].type == 7 || Main.npc[i].type == 8 || Main.npc[i].type == 9)
											{
												num38 = 13f;
											}
											else
											{
												if (Main.npc[i].type == 98 || Main.npc[i].type == 99 || Main.npc[i].type == 100)
												{
													num38 = 13f;
												}
												else
												{
													if (Main.npc[i].type == 95 || Main.npc[i].type == 96 || Main.npc[i].type == 97)
													{
														num38 = 13f;
													}
													else
													{
														if (Main.npc[i].type == 10 || Main.npc[i].type == 11 || Main.npc[i].type == 12)
														{
															num38 = 8f;
														}
														else
														{
															if (Main.npc[i].type == 13 || Main.npc[i].type == 14 || Main.npc[i].type == 15)
															{
																num38 = 26f;
															}
															else
															{
																if (Main.npc[i].type == 48)
																{
																	num38 = 32f;
																}
																else
																{
																	if (Main.npc[i].type == 49 || Main.npc[i].type == 51)
																	{
																		num38 = 4f;
																	}
																	else
																	{
																		if (Main.npc[i].type == 60)
																		{
																			num38 = 10f;
																		}
																		else
																		{
																			if (Main.npc[i].type == 62 || Main.npc[i].type == 66)
																			{
																				num38 = 14f;
																			}
																			else
																			{
																				if (Main.npc[i].type == 63 || Main.npc[i].type == 64 || Main.npc[i].type == 103)
																				{
																					num38 = 4f;
																					origin.Y += 4f;
																				}
																				else
																				{
																					if (Main.npc[i].type == 65)
																					{
																						num38 = 14f;
																					}
																					else
																					{
																						if (Main.npc[i].type == 69)
																						{
																							num38 = 4f;
																							origin.Y += 8f;
																						}
																						else
																						{
																							if (Main.npc[i].type == 70)
																							{
																								num38 = -4f;
																							}
																							else
																							{
																								if (Main.npc[i].type == 72)
																								{
																									num38 = -2f;
																								}
																								else
																								{
																									if (Main.npc[i].type == 83 || Main.npc[i].type == 84)
																									{
																										num38 = 20f;
																									}
																									else
																									{
																										if (Main.npc[i].type == 39 || Main.npc[i].type == 40 || Main.npc[i].type == 41)
																										{
																											num38 = 26f;
																										}
																										else
																										{
																											if (Main.npc[i].type >= 87 && Main.npc[i].type <= 92)
																											{
																												num38 = 56f;
																											}
																											else
																											{
																												if (Main.npc[i].type >= 134 && Main.npc[i].type <= 136)
																												{
																													num38 = 30f;
																												}
																											}
																										}
																									}
																								}
																							}
																						}
																					}
																				}
																			}
																		}
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
						num38 *= Main.npc[i].scale;
						if (Main.npc[i].aiStyle == 10 || Main.npc[i].type == 72)
						{
							color7 = Color.White;
						}
						SpriteEffects effects = SpriteEffects.None;
						if (Main.npc[i].spriteDirection == 1)
						{
							effects = SpriteEffects.FlipHorizontally;
						}
						if (Main.npc[i].type == 83 || Main.npc[i].type == 84)
						{
							this.spriteBatch.Draw(Main.npcTexture[Main.npc[i].type], new Vector2(Main.npc[i].position.X - Main.screenPosition.X + (float)(Main.npc[i].width / 2) - (float)Main.npcTexture[Main.npc[i].type].Width * Main.npc[i].scale / 2f + origin.X * Main.npc[i].scale, Main.npc[i].position.Y - Main.screenPosition.Y + (float)Main.npc[i].height - (float)Main.npcTexture[Main.npc[i].type].Height * Main.npc[i].scale / (float)Main.npcFrameCount[Main.npc[i].type] + 4f + origin.Y * Main.npc[i].scale + num38 + num37), new Rectangle?(Main.npc[i].frame), Color.White, Main.npc[i].rotation, origin, Main.npc[i].scale, effects, 0f);
						}
						else
						{
							if (Main.npc[i].type >= 87 && Main.npc[i].type <= 92)
							{
								Color alpha = Main.npc[i].GetAlpha(color7);
								byte b = (byte)((Main.tileColor.R + Main.tileColor.G + Main.tileColor.B) / 3);
								if (alpha.R < b)
								{
									alpha.R = b;
								}
								if (alpha.G < b)
								{
									alpha.G = b;
								}
								if (alpha.B < b)
								{
									alpha.B = b;
								}
								this.spriteBatch.Draw(Main.npcTexture[Main.npc[i].type], new Vector2(Main.npc[i].position.X - Main.screenPosition.X + (float)(Main.npc[i].width / 2) - (float)Main.npcTexture[Main.npc[i].type].Width * Main.npc[i].scale / 2f + origin.X * Main.npc[i].scale, Main.npc[i].position.Y - Main.screenPosition.Y + (float)Main.npc[i].height - (float)Main.npcTexture[Main.npc[i].type].Height * Main.npc[i].scale / (float)Main.npcFrameCount[Main.npc[i].type] + 4f + origin.Y * Main.npc[i].scale + num38 + num37), new Rectangle?(Main.npc[i].frame), alpha, Main.npc[i].rotation, origin, Main.npc[i].scale, effects, 0f);
							}
							else
							{
								if (Main.npc[i].type == 94)
								{
									for (int num39 = 1; num39 < 6; num39 += 2)
									{
										Vector2 arg_260D_0 = Main.npc[i].oldPos[num39];
										Color alpha2 = Main.npc[i].GetAlpha(color7);
										alpha2.R = (byte)((int)alpha2.R * (10 - num39) / 15);
										alpha2.G = (byte)((int)alpha2.G * (10 - num39) / 15);
										alpha2.B = (byte)((int)alpha2.B * (10 - num39) / 15);
										alpha2.A = (byte)((int)alpha2.A * (10 - num39) / 15);
										this.spriteBatch.Draw(Main.npcTexture[Main.npc[i].type], new Vector2(Main.npc[i].oldPos[num39].X - Main.screenPosition.X + (float)(Main.npc[i].width / 2) - (float)Main.npcTexture[Main.npc[i].type].Width * Main.npc[i].scale / 2f + origin.X * Main.npc[i].scale, Main.npc[i].oldPos[num39].Y - Main.screenPosition.Y + (float)Main.npc[i].height - (float)Main.npcTexture[Main.npc[i].type].Height * Main.npc[i].scale / (float)Main.npcFrameCount[Main.npc[i].type] + 4f + origin.Y * Main.npc[i].scale + num38), new Rectangle?(Main.npc[i].frame), alpha2, Main.npc[i].rotation, origin, Main.npc[i].scale, effects, 0f);
									}
								}
								if (Main.npc[i].type == 125 || Main.npc[i].type == 126 || Main.npc[i].type == 127 || Main.npc[i].type == 128 || Main.npc[i].type == 129 || Main.npc[i].type == 130 || Main.npc[i].type == 131 || Main.npc[i].type == 139 || Main.npc[i].type == 140)
								{
									for (int num40 = 9; num40 >= 0; num40 -= 2)
									{
										Vector2 arg_28AB_0 = Main.npc[i].oldPos[num40];
										Color alpha3 = Main.npc[i].GetAlpha(color7);
										alpha3.R = (byte)((int)alpha3.R * (10 - num40) / 20);
										alpha3.G = (byte)((int)alpha3.G * (10 - num40) / 20);
										alpha3.B = (byte)((int)alpha3.B * (10 - num40) / 20);
										alpha3.A = (byte)((int)alpha3.A * (10 - num40) / 20);
										this.spriteBatch.Draw(Main.npcTexture[Main.npc[i].type], new Vector2(Main.npc[i].oldPos[num40].X - Main.screenPosition.X + (float)(Main.npc[i].width / 2) - (float)Main.npcTexture[Main.npc[i].type].Width * Main.npc[i].scale / 2f + origin.X * Main.npc[i].scale, Main.npc[i].oldPos[num40].Y - Main.screenPosition.Y + (float)Main.npc[i].height - (float)Main.npcTexture[Main.npc[i].type].Height * Main.npc[i].scale / (float)Main.npcFrameCount[Main.npc[i].type] + 4f + origin.Y * Main.npc[i].scale + num38), new Rectangle?(Main.npc[i].frame), alpha3, Main.npc[i].rotation, origin, Main.npc[i].scale, effects, 0f);
									}
								}
								this.spriteBatch.Draw(Main.npcTexture[Main.npc[i].type], new Vector2(Main.npc[i].position.X - Main.screenPosition.X + (float)(Main.npc[i].width / 2) - (float)Main.npcTexture[Main.npc[i].type].Width * Main.npc[i].scale / 2f + origin.X * Main.npc[i].scale, Main.npc[i].position.Y - Main.screenPosition.Y + (float)Main.npc[i].height - (float)Main.npcTexture[Main.npc[i].type].Height * Main.npc[i].scale / (float)Main.npcFrameCount[Main.npc[i].type] + 4f + origin.Y * Main.npc[i].scale + num38 + num37), new Rectangle?(Main.npc[i].frame), Main.npc[i].GetAlpha(color7), Main.npc[i].rotation, origin, Main.npc[i].scale, effects, 0f);
								if (Main.npc[i].color != default(Color))
								{
									this.spriteBatch.Draw(Main.npcTexture[Main.npc[i].type], new Vector2(Main.npc[i].position.X - Main.screenPosition.X + (float)(Main.npc[i].width / 2) - (float)Main.npcTexture[Main.npc[i].type].Width * Main.npc[i].scale / 2f + origin.X * Main.npc[i].scale, Main.npc[i].position.Y - Main.screenPosition.Y + (float)Main.npc[i].height - (float)Main.npcTexture[Main.npc[i].type].Height * Main.npc[i].scale / (float)Main.npcFrameCount[Main.npc[i].type] + 4f + origin.Y * Main.npc[i].scale + num38 + num37), new Rectangle?(Main.npc[i].frame), Main.npc[i].GetColor(color7), Main.npc[i].rotation, origin, Main.npc[i].scale, effects, 0f);
								}
								if (Main.npc[i].confused)
								{
									this.spriteBatch.Draw(Main.confuseTexture, new Vector2(Main.npc[i].position.X - Main.screenPosition.X + (float)(Main.npc[i].width / 2) - (float)Main.npcTexture[Main.npc[i].type].Width * Main.npc[i].scale / 2f + origin.X * Main.npc[i].scale, Main.npc[i].position.Y - Main.screenPosition.Y + (float)Main.npc[i].height - (float)Main.npcTexture[Main.npc[i].type].Height * Main.npc[i].scale / (float)Main.npcFrameCount[Main.npc[i].type] + 4f + origin.Y * Main.npc[i].scale + num38 + num37 - (float)Main.confuseTexture.Height - 20f), new Rectangle?(new Rectangle(0, 0, Main.confuseTexture.Width, Main.confuseTexture.Height)), new Color(250, 250, 250, 70), Main.npc[i].velocity.X * -0.05f, new Vector2((float)(Main.confuseTexture.Width / 2), (float)(Main.confuseTexture.Height / 2)), Main.essScale + 0.2f, SpriteEffects.None, 0f);
								}
								if (Main.npc[i].type >= 134 && Main.npc[i].type <= 136 && color7 != Color.Black)
								{
									this.spriteBatch.Draw(Main.destTexture[Main.npc[i].type - 134], new Vector2(Main.npc[i].position.X - Main.screenPosition.X + (float)(Main.npc[i].width / 2) - (float)Main.npcTexture[Main.npc[i].type].Width * Main.npc[i].scale / 2f + origin.X * Main.npc[i].scale, Main.npc[i].position.Y - Main.screenPosition.Y + (float)Main.npc[i].height - (float)Main.npcTexture[Main.npc[i].type].Height * Main.npc[i].scale / (float)Main.npcFrameCount[Main.npc[i].type] + 4f + origin.Y * Main.npc[i].scale + num38 + num37), new Rectangle?(Main.npc[i].frame), new Color(255, 255, 255, 0), Main.npc[i].rotation, origin, Main.npc[i].scale, effects, 0f);
								}
								if (Main.npc[i].type == 125)
								{
									this.spriteBatch.Draw(Main.EyeLaserTexture, new Vector2(Main.npc[i].position.X - Main.screenPosition.X + (float)(Main.npc[i].width / 2) - (float)Main.npcTexture[Main.npc[i].type].Width * Main.npc[i].scale / 2f + origin.X * Main.npc[i].scale, Main.npc[i].position.Y - Main.screenPosition.Y + (float)Main.npc[i].height - (float)Main.npcTexture[Main.npc[i].type].Height * Main.npc[i].scale / (float)Main.npcFrameCount[Main.npc[i].type] + 4f + origin.Y * Main.npc[i].scale + num38 + num37), new Rectangle?(Main.npc[i].frame), new Color(255, 255, 255, 0), Main.npc[i].rotation, origin, Main.npc[i].scale, effects, 0f);
								}
								if (Main.npc[i].type == 139)
								{
									this.spriteBatch.Draw(Main.probeTexture, new Vector2(Main.npc[i].position.X - Main.screenPosition.X + (float)(Main.npc[i].width / 2) - (float)Main.npcTexture[Main.npc[i].type].Width * Main.npc[i].scale / 2f + origin.X * Main.npc[i].scale, Main.npc[i].position.Y - Main.screenPosition.Y + (float)Main.npc[i].height - (float)Main.npcTexture[Main.npc[i].type].Height * Main.npc[i].scale / (float)Main.npcFrameCount[Main.npc[i].type] + 4f + origin.Y * Main.npc[i].scale + num38 + num37), new Rectangle?(Main.npc[i].frame), new Color(255, 255, 255, 0), Main.npc[i].rotation, origin, Main.npc[i].scale, effects, 0f);
								}
								if (Main.npc[i].type == 127)
								{
									this.spriteBatch.Draw(Main.BoneEyesTexture, new Vector2(Main.npc[i].position.X - Main.screenPosition.X + (float)(Main.npc[i].width / 2) - (float)Main.npcTexture[Main.npc[i].type].Width * Main.npc[i].scale / 2f + origin.X * Main.npc[i].scale, Main.npc[i].position.Y - Main.screenPosition.Y + (float)Main.npc[i].height - (float)Main.npcTexture[Main.npc[i].type].Height * Main.npc[i].scale / (float)Main.npcFrameCount[Main.npc[i].type] + 4f + origin.Y * Main.npc[i].scale + num38 + num37), new Rectangle?(Main.npc[i].frame), new Color(200, 200, 200, 0), Main.npc[i].rotation, origin, Main.npc[i].scale, effects, 0f);
								}
								if (Main.npc[i].type == 131)
								{
									this.spriteBatch.Draw(Main.BoneLaserTexture, new Vector2(Main.npc[i].position.X - Main.screenPosition.X + (float)(Main.npc[i].width / 2) - (float)Main.npcTexture[Main.npc[i].type].Width * Main.npc[i].scale / 2f + origin.X * Main.npc[i].scale, Main.npc[i].position.Y - Main.screenPosition.Y + (float)Main.npc[i].height - (float)Main.npcTexture[Main.npc[i].type].Height * Main.npc[i].scale / (float)Main.npcFrameCount[Main.npc[i].type] + 4f + origin.Y * Main.npc[i].scale + num38 + num37), new Rectangle?(Main.npc[i].frame), new Color(200, 200, 200, 0), Main.npc[i].rotation, origin, Main.npc[i].scale, effects, 0f);
								}
								if (Main.npc[i].type == 120)
								{
									for (int num41 = 1; num41 < Main.npc[i].oldPos.Length; num41++)
									{
										Vector2 arg_3647_0 = Main.npc[i].oldPos[num41];
										Color color8 = default(Color);
										color8.R = (byte)(150 * (10 - num41) / 15);
										color8.G = (byte)(100 * (10 - num41) / 15);
										color8.B = (byte)(150 * (10 - num41) / 15);
										color8.A = (byte)(50 * (10 - num41) / 15);
										this.spriteBatch.Draw(Main.chaosTexture, new Vector2(Main.npc[i].oldPos[num41].X - Main.screenPosition.X + (float)(Main.npc[i].width / 2) - (float)Main.npcTexture[Main.npc[i].type].Width * Main.npc[i].scale / 2f + origin.X * Main.npc[i].scale, Main.npc[i].oldPos[num41].Y - Main.screenPosition.Y + (float)Main.npc[i].height - (float)Main.npcTexture[Main.npc[i].type].Height * Main.npc[i].scale / (float)Main.npcFrameCount[Main.npc[i].type] + 4f + origin.Y * Main.npc[i].scale + num38), new Rectangle?(Main.npc[i].frame), color8, Main.npc[i].rotation, origin, Main.npc[i].scale, effects, 0f);
									}
								}
								else
								{
									if (Main.npc[i].type == 137 || Main.npc[i].type == 138)
									{
										for (int num42 = 1; num42 < Main.npc[i].oldPos.Length; num42++)
										{
											Vector2 arg_3851_0 = Main.npc[i].oldPos[num42];
											Color color9 = default(Color);
											color9.R = (byte)(150 * (10 - num42) / 15);
											color9.G = (byte)(100 * (10 - num42) / 15);
											color9.B = (byte)(150 * (10 - num42) / 15);
											color9.A = (byte)(50 * (10 - num42) / 15);
											this.spriteBatch.Draw(Main.npcTexture[Main.npc[i].type], new Vector2(Main.npc[i].oldPos[num42].X - Main.screenPosition.X + (float)(Main.npc[i].width / 2) - (float)Main.npcTexture[Main.npc[i].type].Width * Main.npc[i].scale / 2f + origin.X * Main.npc[i].scale, Main.npc[i].oldPos[num42].Y - Main.screenPosition.Y + (float)Main.npc[i].height - (float)Main.npcTexture[Main.npc[i].type].Height * Main.npc[i].scale / (float)Main.npcFrameCount[Main.npc[i].type] + 4f + origin.Y * Main.npc[i].scale + num38), new Rectangle?(Main.npc[i].frame), color9, Main.npc[i].rotation, origin, Main.npc[i].scale, effects, 0f);
										}
									}
									else
									{
										if (Main.npc[i].type == 82)
										{
											this.spriteBatch.Draw(Main.wraithEyeTexture, new Vector2(Main.npc[i].position.X - Main.screenPosition.X + (float)(Main.npc[i].width / 2) - (float)Main.npcTexture[Main.npc[i].type].Width * Main.npc[i].scale / 2f + origin.X * Main.npc[i].scale, Main.npc[i].position.Y - Main.screenPosition.Y + (float)Main.npc[i].height - (float)Main.npcTexture[Main.npc[i].type].Height * Main.npc[i].scale / (float)Main.npcFrameCount[Main.npc[i].type] + 4f + origin.Y * Main.npc[i].scale + num38), new Rectangle?(Main.npc[i].frame), Color.White, Main.npc[i].rotation, origin, Main.npc[i].scale, effects, 0f);
											for (int num43 = 1; num43 < 10; num43++)
											{
												Color color10 = new Color(110 - num43 * 10, 110 - num43 * 10, 110 - num43 * 10, 110 - num43 * 10);
												this.spriteBatch.Draw(Main.wraithEyeTexture, new Vector2(Main.npc[i].position.X - Main.screenPosition.X + (float)(Main.npc[i].width / 2) - (float)Main.npcTexture[Main.npc[i].type].Width * Main.npc[i].scale / 2f + origin.X * Main.npc[i].scale, Main.npc[i].position.Y - Main.screenPosition.Y + (float)Main.npc[i].height - (float)Main.npcTexture[Main.npc[i].type].Height * Main.npc[i].scale / (float)Main.npcFrameCount[Main.npc[i].type] + 4f + origin.Y * Main.npc[i].scale + num38) - Main.npc[i].velocity * (float)num43 * 0.5f, new Rectangle?(Main.npc[i].frame), color10, Main.npc[i].rotation, origin, Main.npc[i].scale, effects, 0f);
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}
		protected void DrawProj(int i)
		{
			if (Main.projectile[i].type == 32)
			{
				Vector2 vector = new Vector2(Main.projectile[i].position.X + (float)Main.projectile[i].width * 0.5f, Main.projectile[i].position.Y + (float)Main.projectile[i].height * 0.5f);
				float num = Main.player[Main.projectile[i].owner].position.X + (float)(Main.player[Main.projectile[i].owner].width / 2) - vector.X;
				float num2 = Main.player[Main.projectile[i].owner].position.Y + (float)(Main.player[Main.projectile[i].owner].height / 2) - vector.Y;
				float rotation = (float)Math.Atan2((double)num2, (double)num) - 1.57f;
				bool flag = true;
				if (num == 0f && num2 == 0f)
				{
					flag = false;
				}
				else
				{
					float num3 = (float)Math.Sqrt((double)(num * num + num2 * num2));
					num3 = 8f / num3;
					num *= num3;
					num2 *= num3;
					vector.X -= num;
					vector.Y -= num2;
					num = Main.player[Main.projectile[i].owner].position.X + (float)(Main.player[Main.projectile[i].owner].width / 2) - vector.X;
					num2 = Main.player[Main.projectile[i].owner].position.Y + (float)(Main.player[Main.projectile[i].owner].height / 2) - vector.Y;
				}
				while (flag)
				{
					float num4 = (float)Math.Sqrt((double)(num * num + num2 * num2));
					if (num4 < 28f)
					{
						flag = false;
					}
					else
					{
						num4 = 28f / num4;
						num *= num4;
						num2 *= num4;
						vector.X += num;
						vector.Y += num2;
						num = Main.player[Main.projectile[i].owner].position.X + (float)(Main.player[Main.projectile[i].owner].width / 2) - vector.X;
						num2 = Main.player[Main.projectile[i].owner].position.Y + (float)(Main.player[Main.projectile[i].owner].height / 2) - vector.Y;
						Color color = Lighting.GetColor((int)vector.X / 16, (int)(vector.Y / 16f));
						this.spriteBatch.Draw(Main.chain5Texture, new Vector2(vector.X - Main.screenPosition.X, vector.Y - Main.screenPosition.Y), new Rectangle?(new Rectangle(0, 0, Main.chain5Texture.Width, Main.chain5Texture.Height)), color, rotation, new Vector2((float)Main.chain5Texture.Width * 0.5f, (float)Main.chain5Texture.Height * 0.5f), 1f, SpriteEffects.None, 0f);
					}
				}
			}
			else
			{
				if (Main.projectile[i].type == 73)
				{
					Vector2 vector2 = new Vector2(Main.projectile[i].position.X + (float)Main.projectile[i].width * 0.5f, Main.projectile[i].position.Y + (float)Main.projectile[i].height * 0.5f);
					float num5 = Main.player[Main.projectile[i].owner].position.X + (float)(Main.player[Main.projectile[i].owner].width / 2) - vector2.X;
					float num6 = Main.player[Main.projectile[i].owner].position.Y + (float)(Main.player[Main.projectile[i].owner].height / 2) - vector2.Y;
					float rotation2 = (float)Math.Atan2((double)num6, (double)num5) - 1.57f;
					bool flag2 = true;
					while (flag2)
					{
						float num7 = (float)Math.Sqrt((double)(num5 * num5 + num6 * num6));
						if (num7 < 25f)
						{
							flag2 = false;
						}
						else
						{
							num7 = 12f / num7;
							num5 *= num7;
							num6 *= num7;
							vector2.X += num5;
							vector2.Y += num6;
							num5 = Main.player[Main.projectile[i].owner].position.X + (float)(Main.player[Main.projectile[i].owner].width / 2) - vector2.X;
							num6 = Main.player[Main.projectile[i].owner].position.Y + (float)(Main.player[Main.projectile[i].owner].height / 2) - vector2.Y;
							Color color2 = Lighting.GetColor((int)vector2.X / 16, (int)(vector2.Y / 16f));
							this.spriteBatch.Draw(Main.chain8Texture, new Vector2(vector2.X - Main.screenPosition.X, vector2.Y - Main.screenPosition.Y), new Rectangle?(new Rectangle(0, 0, Main.chain8Texture.Width, Main.chain8Texture.Height)), color2, rotation2, new Vector2((float)Main.chain8Texture.Width * 0.5f, (float)Main.chain8Texture.Height * 0.5f), 1f, SpriteEffects.None, 0f);
						}
					}
				}
				else
				{
					if (Main.projectile[i].type == 74)
					{
						Vector2 vector3 = new Vector2(Main.projectile[i].position.X + (float)Main.projectile[i].width * 0.5f, Main.projectile[i].position.Y + (float)Main.projectile[i].height * 0.5f);
						float num8 = Main.player[Main.projectile[i].owner].position.X + (float)(Main.player[Main.projectile[i].owner].width / 2) - vector3.X;
						float num9 = Main.player[Main.projectile[i].owner].position.Y + (float)(Main.player[Main.projectile[i].owner].height / 2) - vector3.Y;
						float rotation3 = (float)Math.Atan2((double)num9, (double)num8) - 1.57f;
						bool flag3 = true;
						while (flag3)
						{
							float num10 = (float)Math.Sqrt((double)(num8 * num8 + num9 * num9));
							if (num10 < 25f)
							{
								flag3 = false;
							}
							else
							{
								num10 = 12f / num10;
								num8 *= num10;
								num9 *= num10;
								vector3.X += num8;
								vector3.Y += num9;
								num8 = Main.player[Main.projectile[i].owner].position.X + (float)(Main.player[Main.projectile[i].owner].width / 2) - vector3.X;
								num9 = Main.player[Main.projectile[i].owner].position.Y + (float)(Main.player[Main.projectile[i].owner].height / 2) - vector3.Y;
								Color color3 = Lighting.GetColor((int)vector3.X / 16, (int)(vector3.Y / 16f));
								this.spriteBatch.Draw(Main.chain9Texture, new Vector2(vector3.X - Main.screenPosition.X, vector3.Y - Main.screenPosition.Y), new Rectangle?(new Rectangle(0, 0, Main.chain8Texture.Width, Main.chain8Texture.Height)), color3, rotation3, new Vector2((float)Main.chain8Texture.Width * 0.5f, (float)Main.chain8Texture.Height * 0.5f), 1f, SpriteEffects.None, 0f);
							}
						}
					}
					else
					{
						if (Main.projectile[i].aiStyle == 7)
						{
							Vector2 vector4 = new Vector2(Main.projectile[i].position.X + (float)Main.projectile[i].width * 0.5f, Main.projectile[i].position.Y + (float)Main.projectile[i].height * 0.5f);
							float num11 = Main.player[Main.projectile[i].owner].position.X + (float)(Main.player[Main.projectile[i].owner].width / 2) - vector4.X;
							float num12 = Main.player[Main.projectile[i].owner].position.Y + (float)(Main.player[Main.projectile[i].owner].height / 2) - vector4.Y;
							float rotation4 = (float)Math.Atan2((double)num12, (double)num11) - 1.57f;
							bool flag4 = true;
							while (flag4)
							{
								float num13 = (float)Math.Sqrt((double)(num11 * num11 + num12 * num12));
								if (num13 < 25f)
								{
									flag4 = false;
								}
								else
								{
									num13 = 12f / num13;
									num11 *= num13;
									num12 *= num13;
									vector4.X += num11;
									vector4.Y += num12;
									num11 = Main.player[Main.projectile[i].owner].position.X + (float)(Main.player[Main.projectile[i].owner].width / 2) - vector4.X;
									num12 = Main.player[Main.projectile[i].owner].position.Y + (float)(Main.player[Main.projectile[i].owner].height / 2) - vector4.Y;
									Color color4 = Lighting.GetColor((int)vector4.X / 16, (int)(vector4.Y / 16f));
									this.spriteBatch.Draw(Main.chainTexture, new Vector2(vector4.X - Main.screenPosition.X, vector4.Y - Main.screenPosition.Y), new Rectangle?(new Rectangle(0, 0, Main.chainTexture.Width, Main.chainTexture.Height)), color4, rotation4, new Vector2((float)Main.chainTexture.Width * 0.5f, (float)Main.chainTexture.Height * 0.5f), 1f, SpriteEffects.None, 0f);
								}
							}
						}
						else
						{
							if (Main.projectile[i].aiStyle == 13)
							{
								float num14 = Main.projectile[i].position.X + 8f;
								float num15 = Main.projectile[i].position.Y + 2f;
								float num16 = Main.projectile[i].velocity.X;
								float num17 = Main.projectile[i].velocity.Y;
								float num18 = (float)Math.Sqrt((double)(num16 * num16 + num17 * num17));
								num18 = 20f / num18;
								if (Main.projectile[i].ai[0] == 0f)
								{
									num14 -= Main.projectile[i].velocity.X * num18;
									num15 -= Main.projectile[i].velocity.Y * num18;
								}
								else
								{
									num14 += Main.projectile[i].velocity.X * num18;
									num15 += Main.projectile[i].velocity.Y * num18;
								}
								Vector2 vector5 = new Vector2(num14, num15);
								num16 = Main.player[Main.projectile[i].owner].position.X + (float)(Main.player[Main.projectile[i].owner].width / 2) - vector5.X;
								num17 = Main.player[Main.projectile[i].owner].position.Y + (float)(Main.player[Main.projectile[i].owner].height / 2) - vector5.Y;
								float rotation5 = (float)Math.Atan2((double)num17, (double)num16) - 1.57f;
								if (Main.projectile[i].alpha == 0)
								{
									int num19 = -1;
									if (Main.projectile[i].position.X + (float)(Main.projectile[i].width / 2) < Main.player[Main.projectile[i].owner].position.X + (float)(Main.player[Main.projectile[i].owner].width / 2))
									{
										num19 = 1;
									}
									if (Main.player[Main.projectile[i].owner].direction == 1)
									{
										Main.player[Main.projectile[i].owner].itemRotation = (float)Math.Atan2((double)(num17 * (float)num19), (double)(num16 * (float)num19));
									}
									else
									{
										Main.player[Main.projectile[i].owner].itemRotation = (float)Math.Atan2((double)(num17 * (float)num19), (double)(num16 * (float)num19));
									}
								}
								bool flag5 = true;
								while (flag5)
								{
									float num20 = (float)Math.Sqrt((double)(num16 * num16 + num17 * num17));
									if (num20 < 25f)
									{
										flag5 = false;
									}
									else
									{
										num20 = 12f / num20;
										num16 *= num20;
										num17 *= num20;
										vector5.X += num16;
										vector5.Y += num17;
										num16 = Main.player[Main.projectile[i].owner].position.X + (float)(Main.player[Main.projectile[i].owner].width / 2) - vector5.X;
										num17 = Main.player[Main.projectile[i].owner].position.Y + (float)(Main.player[Main.projectile[i].owner].height / 2) - vector5.Y;
										Color color5 = Lighting.GetColor((int)vector5.X / 16, (int)(vector5.Y / 16f));
										this.spriteBatch.Draw(Main.chainTexture, new Vector2(vector5.X - Main.screenPosition.X, vector5.Y - Main.screenPosition.Y), new Rectangle?(new Rectangle(0, 0, Main.chainTexture.Width, Main.chainTexture.Height)), color5, rotation5, new Vector2((float)Main.chainTexture.Width * 0.5f, (float)Main.chainTexture.Height * 0.5f), 1f, SpriteEffects.None, 0f);
									}
								}
							}
							else
							{
								if (Main.projectile[i].aiStyle == 15)
								{
									Vector2 vector6 = new Vector2(Main.projectile[i].position.X + (float)Main.projectile[i].width * 0.5f, Main.projectile[i].position.Y + (float)Main.projectile[i].height * 0.5f);
									float num21 = Main.player[Main.projectile[i].owner].position.X + (float)(Main.player[Main.projectile[i].owner].width / 2) - vector6.X;
									float num22 = Main.player[Main.projectile[i].owner].position.Y + (float)(Main.player[Main.projectile[i].owner].height / 2) - vector6.Y;
									float rotation6 = (float)Math.Atan2((double)num22, (double)num21) - 1.57f;
									if (Main.projectile[i].alpha == 0)
									{
										int num23 = -1;
										if (Main.projectile[i].position.X + (float)(Main.projectile[i].width / 2) < Main.player[Main.projectile[i].owner].position.X + (float)(Main.player[Main.projectile[i].owner].width / 2))
										{
											num23 = 1;
										}
										if (Main.player[Main.projectile[i].owner].direction == 1)
										{
											Main.player[Main.projectile[i].owner].itemRotation = (float)Math.Atan2((double)(num22 * (float)num23), (double)(num21 * (float)num23));
										}
										else
										{
											Main.player[Main.projectile[i].owner].itemRotation = (float)Math.Atan2((double)(num22 * (float)num23), (double)(num21 * (float)num23));
										}
									}
									bool flag6 = true;
									while (flag6)
									{
										float num24 = (float)Math.Sqrt((double)(num21 * num21 + num22 * num22));
										if (num24 < 25f)
										{
											flag6 = false;
										}
										else
										{
											num24 = 12f / num24;
											num21 *= num24;
											num22 *= num24;
											vector6.X += num21;
											vector6.Y += num22;
											num21 = Main.player[Main.projectile[i].owner].position.X + (float)(Main.player[Main.projectile[i].owner].width / 2) - vector6.X;
											num22 = Main.player[Main.projectile[i].owner].position.Y + (float)(Main.player[Main.projectile[i].owner].height / 2) - vector6.Y;
											Color color6 = Lighting.GetColor((int)vector6.X / 16, (int)(vector6.Y / 16f));
											if (Main.projectile[i].type == 25)
											{
												this.spriteBatch.Draw(Main.chain2Texture, new Vector2(vector6.X - Main.screenPosition.X, vector6.Y - Main.screenPosition.Y), new Rectangle?(new Rectangle(0, 0, Main.chain2Texture.Width, Main.chain2Texture.Height)), color6, rotation6, new Vector2((float)Main.chain2Texture.Width * 0.5f, (float)Main.chain2Texture.Height * 0.5f), 1f, SpriteEffects.None, 0f);
											}
											else
											{
												if (Main.projectile[i].type == 35)
												{
													this.spriteBatch.Draw(Main.chain6Texture, new Vector2(vector6.X - Main.screenPosition.X, vector6.Y - Main.screenPosition.Y), new Rectangle?(new Rectangle(0, 0, Main.chain6Texture.Width, Main.chain6Texture.Height)), color6, rotation6, new Vector2((float)Main.chain6Texture.Width * 0.5f, (float)Main.chain6Texture.Height * 0.5f), 1f, SpriteEffects.None, 0f);
												}
												else
												{
													if (Main.projectile[i].type == 63)
													{
														this.spriteBatch.Draw(Main.chain7Texture, new Vector2(vector6.X - Main.screenPosition.X, vector6.Y - Main.screenPosition.Y), new Rectangle?(new Rectangle(0, 0, Main.chain7Texture.Width, Main.chain7Texture.Height)), color6, rotation6, new Vector2((float)Main.chain7Texture.Width * 0.5f, (float)Main.chain7Texture.Height * 0.5f), 1f, SpriteEffects.None, 0f);
													}
													else
													{
														this.spriteBatch.Draw(Main.chain3Texture, new Vector2(vector6.X - Main.screenPosition.X, vector6.Y - Main.screenPosition.Y), new Rectangle?(new Rectangle(0, 0, Main.chain3Texture.Width, Main.chain3Texture.Height)), color6, rotation6, new Vector2((float)Main.chain3Texture.Width * 0.5f, (float)Main.chain3Texture.Height * 0.5f), 1f, SpriteEffects.None, 0f);
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			Color newColor = Lighting.GetColor((int)((double)Main.projectile[i].position.X + (double)Main.projectile[i].width * 0.5) / 16, (int)(((double)Main.projectile[i].position.Y + (double)Main.projectile[i].height * 0.5) / 16.0));
			if (Main.projectile[i].hide)
			{
				newColor = Lighting.GetColor((int)((double)Main.player[Main.projectile[i].owner].position.X + (double)Main.player[Main.projectile[i].owner].width * 0.5) / 16, (int)(((double)Main.player[Main.projectile[i].owner].position.Y + (double)Main.player[Main.projectile[i].owner].height * 0.5) / 16.0));
			}
			if (Main.projectile[i].type == 14)
			{
				newColor = Color.White;
			}
			int num25 = 0;
			int num26 = 0;
			if (Main.projectile[i].type == 16)
			{
				num25 = 6;
			}
			if (Main.projectile[i].type == 17 || Main.projectile[i].type == 31)
			{
				num25 = 2;
			}
			if (Main.projectile[i].type == 25 || Main.projectile[i].type == 26 || Main.projectile[i].type == 35 || Main.projectile[i].type == 63)
			{
				num25 = 6;
				num26 -= 6;
			}
			if (Main.projectile[i].type == 28 || Main.projectile[i].type == 37 || Main.projectile[i].type == 75)
			{
				num25 = 8;
			}
			if (Main.projectile[i].type == 29)
			{
				num25 = 11;
			}
			if (Main.projectile[i].type == 43)
			{
				num25 = 4;
			}
			if (Main.projectile[i].type == 69 || Main.projectile[i].type == 70)
			{
				num25 = 4;
				num26 = 4;
			}
			float num27 = (float)(Main.projectileTexture[Main.projectile[i].type].Width - Main.projectile[i].width) * 0.5f + (float)Main.projectile[i].width * 0.5f;
			if (Main.projectile[i].type == 50 || Main.projectile[i].type == 53)
			{
				num26 = -8;
			}
			if (Main.projectile[i].type == 72 || Main.projectile[i].type == 86 || Main.projectile[i].type == 87)
			{
				num26 = -16;
				num25 = 8;
			}
			if (Main.projectile[i].type == 74)
			{
				num26 = -6;
			}
			if (Main.projectile[i].type == 99)
			{
				num25 = 1;
			}
			if (Main.projectile[i].type == 111)
			{
				num25 = 18;
				num26 = -16;
			}
			SpriteEffects effects = SpriteEffects.None;
			if (Main.projectile[i].spriteDirection == -1)
			{
				effects = SpriteEffects.FlipHorizontally;
			}
			if (Main.projFrames[Main.projectile[i].type] > 1)
			{
				int num28 = Main.projectileTexture[Main.projectile[i].type].Height / Main.projFrames[Main.projectile[i].type];
				int y = num28 * Main.projectile[i].frame;
				if (Main.projectile[i].type == 111)
				{
					int r = (int)Main.player[Main.projectile[i].owner].shirtColor.R;
					int g = (int)Main.player[Main.projectile[i].owner].shirtColor.G;
					int b = (int)Main.player[Main.projectile[i].owner].shirtColor.B;
					Color oldColor = new Color((int)((byte)r), (int)((byte)g), (int)((byte)b));
					newColor = Lighting.GetColor((int)((double)Main.projectile[i].position.X + (double)Main.projectile[i].width * 0.5) / 16, (int)(((double)Main.projectile[i].position.Y + (double)Main.projectile[i].height * 0.5) / 16.0), oldColor);
					this.spriteBatch.Draw(Main.projectileTexture[Main.projectile[i].type], new Vector2(Main.projectile[i].position.X - Main.screenPosition.X + num27 + (float)num26, Main.projectile[i].position.Y - Main.screenPosition.Y + (float)(Main.projectile[i].height / 2)), new Rectangle?(new Rectangle(0, y, Main.projectileTexture[Main.projectile[i].type].Width, num28)), Main.projectile[i].GetAlpha(newColor), Main.projectile[i].rotation, new Vector2(num27, (float)(Main.projectile[i].height / 2 + num25)), Main.projectile[i].scale, effects, 0f);
					return;
				}
				this.spriteBatch.Draw(Main.projectileTexture[Main.projectile[i].type], new Vector2(Main.projectile[i].position.X - Main.screenPosition.X + num27 + (float)num26, Main.projectile[i].position.Y - Main.screenPosition.Y + (float)(Main.projectile[i].height / 2)), new Rectangle?(new Rectangle(0, y, Main.projectileTexture[Main.projectile[i].type].Width, num28)), Main.projectile[i].GetAlpha(newColor), Main.projectile[i].rotation, new Vector2(num27, (float)(Main.projectile[i].height / 2 + num25)), Main.projectile[i].scale, effects, 0f);
				return;
			}
			else
			{
				if (Main.projectile[i].aiStyle == 19)
				{
					Vector2 origin = new Vector2(0f, 0f);
					if (Main.projectile[i].spriteDirection == -1)
					{
						origin.X = (float)Main.projectileTexture[Main.projectile[i].type].Width;
					}
					this.spriteBatch.Draw(Main.projectileTexture[Main.projectile[i].type], new Vector2(Main.projectile[i].position.X - Main.screenPosition.X + (float)(Main.projectile[i].width / 2), Main.projectile[i].position.Y - Main.screenPosition.Y + (float)(Main.projectile[i].height / 2)), new Rectangle?(new Rectangle(0, 0, Main.projectileTexture[Main.projectile[i].type].Width, Main.projectileTexture[Main.projectile[i].type].Height)), Main.projectile[i].GetAlpha(newColor), Main.projectile[i].rotation, origin, Main.projectile[i].scale, effects, 0f);
					return;
				}
				if (Main.projectile[i].type == 94 && Main.projectile[i].ai[1] > 6f)
				{
					for (int j = 0; j < 10; j++)
					{
						Color alpha = Main.projectile[i].GetAlpha(newColor);
						float num29 = (float)(9 - j) / 9f;
						alpha.R = (byte)((float)alpha.R * num29);
						alpha.G = (byte)((float)alpha.G * num29);
						alpha.B = (byte)((float)alpha.B * num29);
						alpha.A = (byte)((float)alpha.A * num29);
						float num30 = (float)(9 - j) / 9f;
						this.spriteBatch.Draw(Main.projectileTexture[Main.projectile[i].type], new Vector2(Main.projectile[i].oldPos[j].X - Main.screenPosition.X + num27 + (float)num26, Main.projectile[i].oldPos[j].Y - Main.screenPosition.Y + (float)(Main.projectile[i].height / 2)), new Rectangle?(new Rectangle(0, 0, Main.projectileTexture[Main.projectile[i].type].Width, Main.projectileTexture[Main.projectile[i].type].Height)), alpha, Main.projectile[i].rotation, new Vector2(num27, (float)(Main.projectile[i].height / 2 + num25)), num30 * Main.projectile[i].scale, effects, 0f);
					}
				}
				this.spriteBatch.Draw(Main.projectileTexture[Main.projectile[i].type], new Vector2(Main.projectile[i].position.X - Main.screenPosition.X + num27 + (float)num26, Main.projectile[i].position.Y - Main.screenPosition.Y + (float)(Main.projectile[i].height / 2)), new Rectangle?(new Rectangle(0, 0, Main.projectileTexture[Main.projectile[i].type].Width, Main.projectileTexture[Main.projectile[i].type].Height)), Main.projectile[i].GetAlpha(newColor), Main.projectile[i].rotation, new Vector2(num27, (float)(Main.projectile[i].height / 2 + num25)), Main.projectile[i].scale, effects, 0f);
				if (Main.projectile[i].type == 106)
				{
					this.spriteBatch.Draw(Main.lightDiscTexture, new Vector2(Main.projectile[i].position.X - Main.screenPosition.X + num27 + (float)num26, Main.projectile[i].position.Y - Main.screenPosition.Y + (float)(Main.projectile[i].height / 2)), new Rectangle?(new Rectangle(0, 0, Main.projectileTexture[Main.projectile[i].type].Width, Main.projectileTexture[Main.projectile[i].type].Height)), new Color(200, 200, 200, 0), Main.projectile[i].rotation, new Vector2(num27, (float)(Main.projectile[i].height / 2 + num25)), Main.projectile[i].scale, effects, 0f);
				}
				return;
			}
		}
		private static Color buffColor(Color newColor, float R, float G, float B, float A)
		{
			newColor.R = (byte)((float)newColor.R * R);
			newColor.G = (byte)((float)newColor.G * G);
			newColor.B = (byte)((float)newColor.B * B);
			newColor.A = (byte)((float)newColor.A * A);
			return newColor;
		}
		protected void DrawWoF()
		{
			if (Main.wof >= 0 && Main.player[Main.myPlayer].gross)
			{
				for (int i = 0; i < 255; i++)
				{
					if (Main.player[i].active && Main.player[i].tongued && !Main.player[i].dead)
					{
						float num = Main.npc[Main.wof].position.X + (float)(Main.npc[Main.wof].width / 2);
						float num2 = Main.npc[Main.wof].position.Y + (float)(Main.npc[Main.wof].height / 2);
						Vector2 vector = new Vector2(Main.player[i].position.X + (float)Main.player[i].width * 0.5f, Main.player[i].position.Y + (float)Main.player[i].height * 0.5f);
						float num3 = num - vector.X;
						float num4 = num2 - vector.Y;
						float rotation = (float)Math.Atan2((double)num4, (double)num3) - 1.57f;
						bool flag = true;
						while (flag)
						{
							float num5 = (float)Math.Sqrt((double)(num3 * num3 + num4 * num4));
							if (num5 < 40f)
							{
								flag = false;
							}
							else
							{
								num5 = (float)Main.chain12Texture.Height / num5;
								num3 *= num5;
								num4 *= num5;
								vector.X += num3;
								vector.Y += num4;
								num3 = num - vector.X;
								num4 = num2 - vector.Y;
								Color color = Lighting.GetColor((int)vector.X / 16, (int)(vector.Y / 16f));
								this.spriteBatch.Draw(Main.chain12Texture, new Vector2(vector.X - Main.screenPosition.X, vector.Y - Main.screenPosition.Y), new Rectangle?(new Rectangle(0, 0, Main.chain12Texture.Width, Main.chain12Texture.Height)), color, rotation, new Vector2((float)Main.chain12Texture.Width * 0.5f, (float)Main.chain12Texture.Height * 0.5f), 1f, SpriteEffects.None, 0f);
							}
						}
					}
				}
				for (int j = 0; j < 200; j++)
				{
					if (Main.npc[j].active && Main.npc[j].aiStyle == 29)
					{
						float num6 = Main.npc[Main.wof].position.X + (float)(Main.npc[Main.wof].width / 2);
						float num7 = Main.npc[Main.wof].position.Y;
						float num8 = (float)(Main.wofB - Main.wofT);
						bool flag2 = false;
						if (Main.npc[j].frameCounter > 7.0)
						{
							flag2 = true;
						}
						num7 = (float)Main.wofT + num8 * Main.npc[j].ai[0];
						Vector2 vector2 = new Vector2(Main.npc[j].position.X + (float)(Main.npc[j].width / 2), Main.npc[j].position.Y + (float)(Main.npc[j].height / 2));
						float num9 = num6 - vector2.X;
						float num10 = num7 - vector2.Y;
						float rotation2 = (float)Math.Atan2((double)num10, (double)num9) - 1.57f;
						bool flag3 = true;
						while (flag3)
						{
							SpriteEffects effects = SpriteEffects.None;
							if (flag2)
							{
								effects = SpriteEffects.FlipHorizontally;
								flag2 = false;
							}
							else
							{
								flag2 = true;
							}
							int height = 28;
							float num11 = (float)Math.Sqrt((double)(num9 * num9 + num10 * num10));
							if (num11 < 40f)
							{
								height = (int)num11 - 40 + 28;
								flag3 = false;
							}
							num11 = 28f / num11;
							num9 *= num11;
							num10 *= num11;
							vector2.X += num9;
							vector2.Y += num10;
							num9 = num6 - vector2.X;
							num10 = num7 - vector2.Y;
							Color color2 = Lighting.GetColor((int)vector2.X / 16, (int)(vector2.Y / 16f));
							this.spriteBatch.Draw(Main.chain12Texture, new Vector2(vector2.X - Main.screenPosition.X, vector2.Y - Main.screenPosition.Y), new Rectangle?(new Rectangle(0, 0, Main.chain4Texture.Width, height)), color2, rotation2, new Vector2((float)Main.chain4Texture.Width * 0.5f, (float)Main.chain4Texture.Height * 0.5f), 1f, effects, 0f);
						}
					}
				}
				int num12 = 140;
				float num13 = (float)Main.wofT;
				float num14 = (float)Main.wofB;
				num14 = Main.screenPosition.Y + (float)Main.screenHeight;
				float num15 = (float)((int)((num13 - Main.screenPosition.Y) / (float)num12) + 1);
				num15 *= (float)num12;
				if (num15 > 0f)
				{
					num13 -= num15;
				}
				float num16 = num13;
				float num17 = Main.npc[Main.wof].position.X;
				float num18 = num14 - num13;
				bool flag4 = true;
				SpriteEffects effects2 = SpriteEffects.None;
				if (Main.npc[Main.wof].spriteDirection == 1)
				{
					effects2 = SpriteEffects.FlipHorizontally;
				}
				if (Main.npc[Main.wof].direction > 0)
				{
					num17 -= 80f;
				}
				int num19 = 0;
				if (!Main.gamePaused)
				{
					Main.wofF++;
				}
				if (Main.wofF > 12)
				{
					num19 = 280;
					if (Main.wofF > 17)
					{
						Main.wofF = 0;
					}
				}
				else
				{
					if (Main.wofF > 6)
					{
						num19 = 140;
					}
				}
				while (flag4)
				{
					num18 = num14 - num16;
					if (num18 > (float)num12)
					{
						num18 = (float)num12;
					}
					bool flag5 = true;
					int num20 = 0;
					while (flag5)
					{
						int x = (int)(num17 + (float)(Main.wofTexture.Width / 2)) / 16;
						int y = (int)(num16 + (float)num20) / 16;
						this.spriteBatch.Draw(Main.wofTexture, new Vector2(num17 - Main.screenPosition.X, num16 + (float)num20 - Main.screenPosition.Y), new Rectangle?(new Rectangle(0, num19 + num20, Main.wofTexture.Width, 16)), Lighting.GetColor(x, y), 0f, default(Vector2), 1f, effects2, 0f);
						num20 += 16;
						if ((float)num20 >= num18)
						{
							flag5 = false;
						}
					}
					num16 += (float)num12;
					if (num16 >= num14)
					{
						flag4 = false;
					}
				}
			}
		}
		protected void DrawGhost(Player drawPlayer)
		{
			SpriteEffects effects;
			if (drawPlayer.direction == 1)
			{
				effects = SpriteEffects.None;
			}
			else
			{
				effects = SpriteEffects.FlipHorizontally;
			}
			Color immuneAlpha = drawPlayer.GetImmuneAlpha(Lighting.GetColor((int)((double)drawPlayer.position.X + (double)drawPlayer.width * 0.5) / 16, (int)((double)drawPlayer.position.Y + (double)drawPlayer.height * 0.5) / 16, new Color((int)(Main.mouseTextColor / 2 + 100), (int)(Main.mouseTextColor / 2 + 100), (int)(Main.mouseTextColor / 2 + 100), (int)(Main.mouseTextColor / 2 + 100))));
			Rectangle value = new Rectangle(0, Main.ghostTexture.Height / 4 * drawPlayer.ghostFrame, Main.ghostTexture.Width, Main.ghostTexture.Height / 4);
			Vector2 origin = new Vector2((float)value.Width * 0.5f, (float)value.Height * 0.5f);
			this.spriteBatch.Draw(Main.ghostTexture, new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X + (float)(value.Width / 2))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)(value.Height / 2)))), new Rectangle?(value), immuneAlpha, 0f, origin, 1f, effects, 0f);
		}
		protected void DrawPlayer(Player drawPlayer)
		{
			Color color = drawPlayer.GetImmuneAlpha(Lighting.GetColor((int)((double)drawPlayer.position.X + (double)drawPlayer.width * 0.5) / 16, (int)(((double)drawPlayer.position.Y + (double)drawPlayer.height * 0.25) / 16.0), Color.White));
			Color color2 = drawPlayer.GetImmuneAlpha(Lighting.GetColor((int)((double)drawPlayer.position.X + (double)drawPlayer.width * 0.5) / 16, (int)(((double)drawPlayer.position.Y + (double)drawPlayer.height * 0.25) / 16.0), drawPlayer.eyeColor));
			Color color3 = drawPlayer.GetImmuneAlpha(Lighting.GetColor((int)((double)drawPlayer.position.X + (double)drawPlayer.width * 0.5) / 16, (int)(((double)drawPlayer.position.Y + (double)drawPlayer.height * 0.25) / 16.0), drawPlayer.hairColor));
			Color color4 = drawPlayer.GetImmuneAlpha(Lighting.GetColor((int)((double)drawPlayer.position.X + (double)drawPlayer.width * 0.5) / 16, (int)(((double)drawPlayer.position.Y + (double)drawPlayer.height * 0.25) / 16.0), drawPlayer.skinColor));
			Color color5 = drawPlayer.GetImmuneAlpha(Lighting.GetColor((int)((double)drawPlayer.position.X + (double)drawPlayer.width * 0.5) / 16, (int)(((double)drawPlayer.position.Y + (double)drawPlayer.height * 0.5) / 16.0), drawPlayer.skinColor));
			Color immuneAlpha = drawPlayer.GetImmuneAlpha(Lighting.GetColor((int)((double)drawPlayer.position.X + (double)drawPlayer.width * 0.5) / 16, (int)(((double)drawPlayer.position.Y + (double)drawPlayer.height * 0.75) / 16.0), drawPlayer.skinColor));
			Color color6 = drawPlayer.GetImmuneAlpha2(Lighting.GetColor((int)((double)drawPlayer.position.X + (double)drawPlayer.width * 0.5) / 16, (int)(((double)drawPlayer.position.Y + (double)drawPlayer.height * 0.5) / 16.0), drawPlayer.shirtColor));
			Color color7 = drawPlayer.GetImmuneAlpha2(Lighting.GetColor((int)((double)drawPlayer.position.X + (double)drawPlayer.width * 0.5) / 16, (int)(((double)drawPlayer.position.Y + (double)drawPlayer.height * 0.5) / 16.0), drawPlayer.underShirtColor));
			Color color8 = drawPlayer.GetImmuneAlpha2(Lighting.GetColor((int)((double)drawPlayer.position.X + (double)drawPlayer.width * 0.5) / 16, (int)(((double)drawPlayer.position.Y + (double)drawPlayer.height * 0.75) / 16.0), drawPlayer.pantsColor));
			Color color9 = drawPlayer.GetImmuneAlpha2(Lighting.GetColor((int)((double)drawPlayer.position.X + (double)drawPlayer.width * 0.5) / 16, (int)(((double)drawPlayer.position.Y + (double)drawPlayer.height * 0.75) / 16.0), drawPlayer.shoeColor));
			Color color10 = drawPlayer.GetImmuneAlpha2(Lighting.GetColor((int)((double)drawPlayer.position.X + (double)drawPlayer.width * 0.5) / 16, (int)((double)drawPlayer.position.Y + (double)drawPlayer.height * 0.25) / 16, Color.White));
			Color color11 = drawPlayer.GetImmuneAlpha2(Lighting.GetColor((int)((double)drawPlayer.position.X + (double)drawPlayer.width * 0.5) / 16, (int)((double)drawPlayer.position.Y + (double)drawPlayer.height * 0.5) / 16, Color.White));
			Color color12 = drawPlayer.GetImmuneAlpha2(Lighting.GetColor((int)((double)drawPlayer.position.X + (double)drawPlayer.width * 0.5) / 16, (int)((double)drawPlayer.position.Y + (double)drawPlayer.height * 0.75) / 16, Color.White));
			if (drawPlayer.shadow > 0f)
			{
				immuneAlpha = new Color(0, 0, 0, 0);
				color5 = new Color(0, 0, 0, 0);
				color4 = new Color(0, 0, 0, 0);
				color3 = new Color(0, 0, 0, 0);
				color2 = new Color(0, 0, 0, 0);
				color = new Color(0, 0, 0, 0);
			}
			float num = 1f;
			float num2 = 1f;
			float num3 = 1f;
			float num4 = 1f;
			if (drawPlayer.poisoned)
			{
				if (Main.rand.Next(50) == 0)
				{
					int num5 = Dust.NewDust(drawPlayer.position, drawPlayer.width, drawPlayer.height, 46, 0f, 0f, 150, default(Color), 0.2f);
					Main.dust[num5].noGravity = true;
					Main.dust[num5].fadeIn = 1.9f;
				}
				num *= 0.65f;
				num3 *= 0.75f;
			}
			if (drawPlayer.onFire)
			{
				if (Main.rand.Next(4) == 0)
				{
					int num6 = Dust.NewDust(new Vector2(drawPlayer.position.X - 2f, drawPlayer.position.Y - 2f), drawPlayer.width + 4, drawPlayer.height + 4, 6, drawPlayer.velocity.X * 0.4f, drawPlayer.velocity.Y * 0.4f, 100, default(Color), 3f);
					Main.dust[num6].noGravity = true;
					Main.dust[num6].velocity *= 1.8f;
					Dust expr_662_cp_0 = Main.dust[num6];
					expr_662_cp_0.velocity.Y = expr_662_cp_0.velocity.Y - 0.5f;
				}
				num3 *= 0.6f;
				num2 *= 0.7f;
			}
			if (drawPlayer.onFire2)
			{
				if (Main.rand.Next(4) == 0)
				{
					int num7 = Dust.NewDust(new Vector2(drawPlayer.position.X - 2f, drawPlayer.position.Y - 2f), drawPlayer.width + 4, drawPlayer.height + 4, 75, drawPlayer.velocity.X * 0.4f, drawPlayer.velocity.Y * 0.4f, 100, default(Color), 3f);
					Main.dust[num7].noGravity = true;
					Main.dust[num7].velocity *= 1.8f;
					Dust expr_74D_cp_0 = Main.dust[num7];
					expr_74D_cp_0.velocity.Y = expr_74D_cp_0.velocity.Y - 0.5f;
				}
				num3 *= 0.6f;
				num2 *= 0.7f;
			}
			if (drawPlayer.noItems)
			{
				num2 *= 0.8f;
				num *= 0.65f;
			}
			if (drawPlayer.blind)
			{
				num2 *= 0.65f;
				num *= 0.7f;
			}
			if (drawPlayer.bleed)
			{
				num2 *= 0.9f;
				num3 *= 0.9f;
				if (!drawPlayer.dead && Main.rand.Next(30) == 0)
				{
					int num8 = Dust.NewDust(drawPlayer.position, drawPlayer.width, drawPlayer.height, 5, 0f, 0f, 0, default(Color), 1f);
					Dust expr_820_cp_0 = Main.dust[num8];
					expr_820_cp_0.velocity.Y = expr_820_cp_0.velocity.Y + 0.5f;
					Main.dust[num8].velocity *= 0.25f;
				}
			}
			if (num != 1f || num2 != 1f || num3 != 1f || num4 != 1f)
			{
				if (drawPlayer.onFire || drawPlayer.onFire2)
				{
					color = drawPlayer.GetImmuneAlpha(Color.White);
					color2 = drawPlayer.GetImmuneAlpha(drawPlayer.eyeColor);
					color3 = drawPlayer.GetImmuneAlpha(drawPlayer.hairColor);
					color4 = drawPlayer.GetImmuneAlpha(drawPlayer.skinColor);
					color5 = drawPlayer.GetImmuneAlpha(drawPlayer.skinColor);
					color6 = drawPlayer.GetImmuneAlpha(drawPlayer.shirtColor);
					color7 = drawPlayer.GetImmuneAlpha(drawPlayer.underShirtColor);
					color8 = drawPlayer.GetImmuneAlpha(drawPlayer.pantsColor);
					color9 = drawPlayer.GetImmuneAlpha(drawPlayer.shoeColor);
					color10 = drawPlayer.GetImmuneAlpha(Color.White);
					color11 = drawPlayer.GetImmuneAlpha(Color.White);
					color12 = drawPlayer.GetImmuneAlpha(Color.White);
				}
				else
				{
					color = Main.buffColor(color, num, num2, num3, num4);
					color2 = Main.buffColor(color2, num, num2, num3, num4);
					color3 = Main.buffColor(color3, num, num2, num3, num4);
					color4 = Main.buffColor(color4, num, num2, num3, num4);
					color5 = Main.buffColor(color5, num, num2, num3, num4);
					color6 = Main.buffColor(color6, num, num2, num3, num4);
					color7 = Main.buffColor(color7, num, num2, num3, num4);
					color8 = Main.buffColor(color8, num, num2, num3, num4);
					color9 = Main.buffColor(color9, num, num2, num3, num4);
					color10 = Main.buffColor(color10, num, num2, num3, num4);
					color11 = Main.buffColor(color11, num, num2, num3, num4);
					color12 = Main.buffColor(color12, num, num2, num3, num4);
				}
			}
			SpriteEffects effects;
			SpriteEffects effects2;
			if (drawPlayer.gravDir == 1f)
			{
				if (drawPlayer.direction == 1)
				{
					effects = SpriteEffects.None;
					effects2 = SpriteEffects.None;
				}
				else
				{
					effects = SpriteEffects.FlipHorizontally;
					effects2 = SpriteEffects.FlipHorizontally;
				}
				if (!drawPlayer.dead)
				{
					drawPlayer.legPosition.Y = 0f;
					drawPlayer.headPosition.Y = 0f;
					drawPlayer.bodyPosition.Y = 0f;
				}
			}
			else
			{
				if (drawPlayer.direction == 1)
				{
					effects = SpriteEffects.FlipVertically;
					effects2 = SpriteEffects.FlipVertically;
				}
				else
				{
					effects = (SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically);
					effects2 = (SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically);
				}
				if (!drawPlayer.dead)
				{
					drawPlayer.legPosition.Y = 6f;
					drawPlayer.headPosition.Y = 6f;
					drawPlayer.bodyPosition.Y = 6f;
				}
			}
			Vector2 vector = new Vector2((float)drawPlayer.legFrame.Width * 0.5f, (float)drawPlayer.legFrame.Height * 0.75f);
			Vector2 origin = new Vector2((float)drawPlayer.legFrame.Width * 0.5f, (float)drawPlayer.legFrame.Height * 0.5f);
			Vector2 vector2 = new Vector2((float)drawPlayer.legFrame.Width * 0.5f, (float)drawPlayer.legFrame.Height * 0.4f);
			if (drawPlayer.merman)
			{
				drawPlayer.headRotation = drawPlayer.velocity.Y * (float)drawPlayer.direction * 0.1f;
				if ((double)drawPlayer.headRotation < -0.3)
				{
					drawPlayer.headRotation = -0.3f;
				}
				if ((double)drawPlayer.headRotation > 0.3)
				{
					drawPlayer.headRotation = 0.3f;
				}
			}
			else
			{
				if (!drawPlayer.dead)
				{
					drawPlayer.headRotation = 0f;
				}
			}
			if (drawPlayer.wings > 0)
			{
				this.spriteBatch.Draw(Main.wingsTexture[drawPlayer.wings], new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X + (float)(drawPlayer.width / 2) - (float)(9 * drawPlayer.direction))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)(drawPlayer.height / 2) + 2f * drawPlayer.gravDir))), new Rectangle?(new Rectangle(0, Main.wingsTexture[drawPlayer.wings].Height / 4 * drawPlayer.wingFrame, Main.wingsTexture[drawPlayer.wings].Width, Main.wingsTexture[drawPlayer.wings].Height / 4)), color11, drawPlayer.bodyRotation, new Vector2((float)(Main.wingsTexture[drawPlayer.wings].Width / 2), (float)(Main.wingsTexture[drawPlayer.wings].Height / 8)), 1f, effects, 0f);
			}
			if (!drawPlayer.invis)
			{
				this.spriteBatch.Draw(Main.skinBodyTexture, new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f))) + drawPlayer.bodyPosition + new Vector2((float)(drawPlayer.bodyFrame.Width / 2), (float)(drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawPlayer.bodyFrame), color5, drawPlayer.bodyRotation, origin, 1f, effects, 0f);
				this.spriteBatch.Draw(Main.skinLegsTexture, new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f))) + drawPlayer.bodyPosition + new Vector2((float)(drawPlayer.bodyFrame.Width / 2), (float)(drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawPlayer.legFrame), immuneAlpha, drawPlayer.legRotation, origin, 1f, effects, 0f);
			}
			if (drawPlayer.legs > 0 && drawPlayer.legs < 25)
			{
				this.spriteBatch.Draw(Main.armorLegTexture[drawPlayer.legs], new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X - (float)(drawPlayer.legFrame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.legFrame.Height + 4f))) + drawPlayer.legPosition + vector, new Rectangle?(drawPlayer.legFrame), color12, drawPlayer.legRotation, vector, 1f, effects, 0f);
			}
			else
			{
				if (!drawPlayer.invis)
				{
					if (!drawPlayer.male)
					{
						this.spriteBatch.Draw(Main.femalePantsTexture, new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X - (float)(drawPlayer.legFrame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.legFrame.Height + 4f))) + drawPlayer.legPosition + vector, new Rectangle?(drawPlayer.legFrame), color8, drawPlayer.legRotation, vector, 1f, effects, 0f);
						this.spriteBatch.Draw(Main.femaleShoesTexture, new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X - (float)(drawPlayer.legFrame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.legFrame.Height + 4f))) + drawPlayer.legPosition + vector, new Rectangle?(drawPlayer.legFrame), color9, drawPlayer.legRotation, vector, 1f, effects, 0f);
					}
					else
					{
						this.spriteBatch.Draw(Main.playerPantsTexture, new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X - (float)(drawPlayer.legFrame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.legFrame.Height + 4f))) + drawPlayer.legPosition + vector, new Rectangle?(drawPlayer.legFrame), color8, drawPlayer.legRotation, vector, 1f, effects, 0f);
						this.spriteBatch.Draw(Main.playerShoesTexture, new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X - (float)(drawPlayer.legFrame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.legFrame.Height + 4f))) + drawPlayer.legPosition + vector, new Rectangle?(drawPlayer.legFrame), color9, drawPlayer.legRotation, vector, 1f, effects, 0f);
					}
				}
			}
			if (drawPlayer.body > 0 && drawPlayer.body < 26)
			{
				if (!drawPlayer.male)
				{
					this.spriteBatch.Draw(Main.femaleBodyTexture[drawPlayer.body], new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f))) + drawPlayer.bodyPosition + new Vector2((float)(drawPlayer.bodyFrame.Width / 2), (float)(drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawPlayer.bodyFrame), color11, drawPlayer.bodyRotation, origin, 1f, effects, 0f);
				}
				else
				{
					this.spriteBatch.Draw(Main.armorBodyTexture[drawPlayer.body], new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f))) + drawPlayer.bodyPosition + new Vector2((float)(drawPlayer.bodyFrame.Width / 2), (float)(drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawPlayer.bodyFrame), color11, drawPlayer.bodyRotation, origin, 1f, effects, 0f);
				}
				if ((drawPlayer.body == 10 || drawPlayer.body == 11 || drawPlayer.body == 12 || drawPlayer.body == 13 || drawPlayer.body == 14 || drawPlayer.body == 15 || drawPlayer.body == 16 || drawPlayer.body == 20) && !drawPlayer.invis)
				{
					this.spriteBatch.Draw(Main.playerHandsTexture, new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f))) + drawPlayer.bodyPosition + new Vector2((float)(drawPlayer.bodyFrame.Width / 2), (float)(drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawPlayer.bodyFrame), color5, drawPlayer.bodyRotation, origin, 1f, effects, 0f);
				}
			}
			else
			{
				if (!drawPlayer.invis)
				{
					if (!drawPlayer.male)
					{
						this.spriteBatch.Draw(Main.femaleUnderShirtTexture, new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f))) + drawPlayer.bodyPosition + new Vector2((float)(drawPlayer.bodyFrame.Width / 2), (float)(drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawPlayer.bodyFrame), color7, drawPlayer.bodyRotation, origin, 1f, effects, 0f);
						this.spriteBatch.Draw(Main.femaleShirtTexture, new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f))) + drawPlayer.bodyPosition + new Vector2((float)(drawPlayer.bodyFrame.Width / 2), (float)(drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawPlayer.bodyFrame), color6, drawPlayer.bodyRotation, origin, 1f, effects, 0f);
					}
					else
					{
						this.spriteBatch.Draw(Main.playerUnderShirtTexture, new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f))) + drawPlayer.bodyPosition + new Vector2((float)(drawPlayer.bodyFrame.Width / 2), (float)(drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawPlayer.bodyFrame), color7, drawPlayer.bodyRotation, origin, 1f, effects, 0f);
						this.spriteBatch.Draw(Main.playerShirtTexture, new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f))) + drawPlayer.bodyPosition + new Vector2((float)(drawPlayer.bodyFrame.Width / 2), (float)(drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawPlayer.bodyFrame), color6, drawPlayer.bodyRotation, origin, 1f, effects, 0f);
					}
					this.spriteBatch.Draw(Main.playerHandsTexture, new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f))) + drawPlayer.bodyPosition + new Vector2((float)(drawPlayer.bodyFrame.Width / 2), (float)(drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawPlayer.bodyFrame), color5, drawPlayer.bodyRotation, origin, 1f, effects, 0f);
				}
			}
			if (!drawPlayer.invis && drawPlayer.head != 38)
			{
				this.spriteBatch.Draw(Main.playerHeadTexture, new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f))) + drawPlayer.headPosition + vector2, new Rectangle?(drawPlayer.bodyFrame), color4, drawPlayer.headRotation, vector2, 1f, effects, 0f);
				this.spriteBatch.Draw(Main.playerEyeWhitesTexture, new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f))) + drawPlayer.headPosition + vector2, new Rectangle?(drawPlayer.bodyFrame), color, drawPlayer.headRotation, vector2, 1f, effects, 0f);
				this.spriteBatch.Draw(Main.playerEyesTexture, new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f))) + drawPlayer.headPosition + vector2, new Rectangle?(drawPlayer.bodyFrame), color2, drawPlayer.headRotation, vector2, 1f, effects, 0f);
			}
			if (drawPlayer.head == 10 || drawPlayer.head == 12 || drawPlayer.head == 28)
			{
				this.spriteBatch.Draw(Main.armorHeadTexture[drawPlayer.head], new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f))) + drawPlayer.headPosition + vector2, new Rectangle?(drawPlayer.bodyFrame), color10, drawPlayer.headRotation, vector2, 1f, effects, 0f);
				if (!drawPlayer.invis)
				{
					Rectangle bodyFrame = drawPlayer.bodyFrame;
					bodyFrame.Y -= 336;
					if (bodyFrame.Y < 0)
					{
						bodyFrame.Y = 0;
					}
					this.spriteBatch.Draw(Main.playerHairTexture[drawPlayer.hair], new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f))) + drawPlayer.headPosition + vector2, new Rectangle?(bodyFrame), color3, drawPlayer.headRotation, vector2, 1f, effects, 0f);
				}
			}
			if (drawPlayer.head == 14 || drawPlayer.head == 15 || drawPlayer.head == 16 || drawPlayer.head == 18 || drawPlayer.head == 21 || drawPlayer.head == 24 || drawPlayer.head == 25 || drawPlayer.head == 26 || drawPlayer.head == 40 || drawPlayer.head == 44)
			{
				Rectangle bodyFrame2 = drawPlayer.bodyFrame;
				bodyFrame2.Y -= 336;
				if (bodyFrame2.Y < 0)
				{
					bodyFrame2.Y = 0;
				}
				if (!drawPlayer.invis)
				{
					this.spriteBatch.Draw(Main.playerHairAltTexture[drawPlayer.hair], new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f))) + drawPlayer.headPosition + vector2, new Rectangle?(bodyFrame2), color3, drawPlayer.headRotation, vector2, 1f, effects, 0f);
				}
			}
			if (drawPlayer.head == 23)
			{
				Rectangle bodyFrame3 = drawPlayer.bodyFrame;
				bodyFrame3.Y -= 336;
				if (bodyFrame3.Y < 0)
				{
					bodyFrame3.Y = 0;
				}
				if (!drawPlayer.invis)
				{
					this.spriteBatch.Draw(Main.playerHairTexture[drawPlayer.hair], new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f))) + drawPlayer.headPosition + vector2, new Rectangle?(bodyFrame3), color3, drawPlayer.headRotation, vector2, 1f, effects, 0f);
				}
				this.spriteBatch.Draw(Main.armorHeadTexture[drawPlayer.head], new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f))) + drawPlayer.headPosition + vector2, new Rectangle?(drawPlayer.bodyFrame), color10, drawPlayer.headRotation, vector2, 1f, effects, 0f);
			}
			else
			{
				if (drawPlayer.head == 14)
				{
					Rectangle bodyFrame4 = drawPlayer.bodyFrame;
					int num9 = 0;
					if (bodyFrame4.Y == bodyFrame4.Height * 6)
					{
						bodyFrame4.Height -= 2;
					}
					else
					{
						if (bodyFrame4.Y == bodyFrame4.Height * 7)
						{
							num9 = -2;
						}
						else
						{
							if (bodyFrame4.Y == bodyFrame4.Height * 8)
							{
								num9 = -2;
							}
							else
							{
								if (bodyFrame4.Y == bodyFrame4.Height * 9)
								{
									num9 = -2;
								}
								else
								{
									if (bodyFrame4.Y == bodyFrame4.Height * 10)
									{
										num9 = -2;
									}
									else
									{
										if (bodyFrame4.Y == bodyFrame4.Height * 13)
										{
											bodyFrame4.Height -= 2;
										}
										else
										{
											if (bodyFrame4.Y == bodyFrame4.Height * 14)
											{
												num9 = -2;
											}
											else
											{
												if (bodyFrame4.Y == bodyFrame4.Height * 15)
												{
													num9 = -2;
												}
												else
												{
													if (bodyFrame4.Y == bodyFrame4.Height * 16)
													{
														num9 = -2;
													}
												}
											}
										}
									}
								}
							}
						}
					}
					bodyFrame4.Y += num9;
					this.spriteBatch.Draw(Main.armorHeadTexture[drawPlayer.head], new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f + (float)num9))) + drawPlayer.headPosition + vector2, new Rectangle?(bodyFrame4), color10, drawPlayer.headRotation, vector2, 1f, effects, 0f);
				}
				else
				{
					if (drawPlayer.head > 0 && drawPlayer.head < 45 && drawPlayer.head != 28)
					{
						this.spriteBatch.Draw(Main.armorHeadTexture[drawPlayer.head], new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f))) + drawPlayer.headPosition + vector2, new Rectangle?(drawPlayer.bodyFrame), color10, drawPlayer.headRotation, vector2, 1f, effects, 0f);
					}
					else
					{
						if (!drawPlayer.invis)
						{
							Rectangle bodyFrame5 = drawPlayer.bodyFrame;
							bodyFrame5.Y -= 336;
							if (bodyFrame5.Y < 0)
							{
								bodyFrame5.Y = 0;
							}
							this.spriteBatch.Draw(Main.playerHairTexture[drawPlayer.hair], new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f))) + drawPlayer.headPosition + vector2, new Rectangle?(bodyFrame5), color3, drawPlayer.headRotation, vector2, 1f, effects, 0f);
						}
					}
				}
			}
			if (drawPlayer.heldProj >= 0)
			{
				this.DrawProj(drawPlayer.heldProj);
			}
			Color color13 = Lighting.GetColor((int)((double)drawPlayer.position.X + (double)drawPlayer.width * 0.5) / 16, (int)(((double)drawPlayer.position.Y + (double)drawPlayer.height * 0.5) / 16.0));
			if ((drawPlayer.itemAnimation > 0 || drawPlayer.inventory[drawPlayer.selectedItem].holdStyle > 0) && drawPlayer.inventory[drawPlayer.selectedItem].type > 0 && !drawPlayer.dead && !drawPlayer.inventory[drawPlayer.selectedItem].noUseGraphic && (!drawPlayer.wet || !drawPlayer.inventory[drawPlayer.selectedItem].noWet))
			{
				if (drawPlayer.inventory[drawPlayer.selectedItem].useStyle == 5)
				{
					int num10 = 10;
					Vector2 vector3 = new Vector2((float)(Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Width / 2), (float)(Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Height / 2));
					if (drawPlayer.inventory[drawPlayer.selectedItem].type == 95)
					{
						num10 = 10;
						vector3.Y += 2f * drawPlayer.gravDir;
					}
					else
					{
						if (drawPlayer.inventory[drawPlayer.selectedItem].type == 96)
						{
							num10 = -5;
						}
						else
						{
							if (drawPlayer.inventory[drawPlayer.selectedItem].type == 98)
							{
								num10 = -5;
								vector3.Y -= 2f * drawPlayer.gravDir;
							}
							else
							{
								if (drawPlayer.inventory[drawPlayer.selectedItem].type == 534)
								{
									num10 = -2;
									vector3.Y += 1f * drawPlayer.gravDir;
								}
								else
								{
									if (drawPlayer.inventory[drawPlayer.selectedItem].type == 533)
									{
										num10 = -7;
										vector3.Y -= 2f * drawPlayer.gravDir;
									}
									else
									{
										if (drawPlayer.inventory[drawPlayer.selectedItem].type == 506)
										{
											num10 = 0;
											vector3.Y -= 2f * drawPlayer.gravDir;
										}
										else
										{
											if (drawPlayer.inventory[drawPlayer.selectedItem].type == 494 || drawPlayer.inventory[drawPlayer.selectedItem].type == 508)
											{
												num10 = -2;
											}
											else
											{
												if (drawPlayer.inventory[drawPlayer.selectedItem].type == 434)
												{
													num10 = 0;
													vector3.Y -= 2f * drawPlayer.gravDir;
												}
												else
												{
													if (drawPlayer.inventory[drawPlayer.selectedItem].type == 514)
													{
														num10 = 0;
														vector3.Y += 3f * drawPlayer.gravDir;
													}
													else
													{
														if (drawPlayer.inventory[drawPlayer.selectedItem].type == 435 || drawPlayer.inventory[drawPlayer.selectedItem].type == 436 || drawPlayer.inventory[drawPlayer.selectedItem].type == 481 || drawPlayer.inventory[drawPlayer.selectedItem].type == 578)
														{
															num10 = -2;
															vector3.Y -= 2f * drawPlayer.gravDir;
														}
														else
														{
															if (drawPlayer.inventory[drawPlayer.selectedItem].type == 197)
															{
																num10 = -5;
																vector3.Y += 4f * drawPlayer.gravDir;
															}
															else
															{
																if (drawPlayer.inventory[drawPlayer.selectedItem].type == 126)
																{
																	num10 = 4;
																	vector3.Y += 4f * drawPlayer.gravDir;
																}
																else
																{
																	if (drawPlayer.inventory[drawPlayer.selectedItem].type == 127)
																	{
																		num10 = 4;
																		vector3.Y += 2f * drawPlayer.gravDir;
																	}
																	else
																	{
																		if (drawPlayer.inventory[drawPlayer.selectedItem].type == 157)
																		{
																			num10 = 6;
																			vector3.Y += 2f * drawPlayer.gravDir;
																		}
																		else
																		{
																			if (drawPlayer.inventory[drawPlayer.selectedItem].type == 160)
																			{
																				num10 = -8;
																			}
																			else
																			{
																				if (drawPlayer.inventory[drawPlayer.selectedItem].type == 164 || drawPlayer.inventory[drawPlayer.selectedItem].type == 219)
																				{
																					num10 = 2;
																					vector3.Y += 4f * drawPlayer.gravDir;
																				}
																				else
																				{
																					if (drawPlayer.inventory[drawPlayer.selectedItem].type == 165 || drawPlayer.inventory[drawPlayer.selectedItem].type == 272)
																					{
																						num10 = 4;
																						vector3.Y += 4f * drawPlayer.gravDir;
																					}
																					else
																					{
																						if (drawPlayer.inventory[drawPlayer.selectedItem].type == 266)
																						{
																							num10 = 0;
																							vector3.Y += 2f * drawPlayer.gravDir;
																						}
																						else
																						{
																							if (drawPlayer.inventory[drawPlayer.selectedItem].type == 281)
																							{
																								num10 = 6;
																								vector3.Y -= 6f * drawPlayer.gravDir;
																							}
																						}
																					}
																				}
																			}
																		}
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
					Vector2 origin2 = new Vector2((float)(-(float)num10), (float)(Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Height / 2));
					if (drawPlayer.direction == -1)
					{
						origin2 = new Vector2((float)(Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Width + num10), (float)(Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Height / 2));
					}
					this.spriteBatch.Draw(Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type], new Vector2((float)((int)(drawPlayer.itemLocation.X - Main.screenPosition.X + vector3.X)), (float)((int)(drawPlayer.itemLocation.Y - Main.screenPosition.Y + vector3.Y))), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Width, Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Height)), drawPlayer.inventory[drawPlayer.selectedItem].GetAlpha(color13), drawPlayer.itemRotation, origin2, drawPlayer.inventory[drawPlayer.selectedItem].scale, effects2, 0f);
					if (drawPlayer.inventory[drawPlayer.selectedItem].color != default(Color))
					{
						this.spriteBatch.Draw(Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type], new Vector2((float)((int)(drawPlayer.itemLocation.X - Main.screenPosition.X + vector3.X)), (float)((int)(drawPlayer.itemLocation.Y - Main.screenPosition.Y + vector3.Y))), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Width, Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Height)), drawPlayer.inventory[drawPlayer.selectedItem].GetColor(color13), drawPlayer.itemRotation, origin2, drawPlayer.inventory[drawPlayer.selectedItem].scale, effects2, 0f);
					}
				}
				else
				{
					if (drawPlayer.gravDir == -1f)
					{
						this.spriteBatch.Draw(Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type], new Vector2((float)((int)(drawPlayer.itemLocation.X - Main.screenPosition.X)), (float)((int)(drawPlayer.itemLocation.Y - Main.screenPosition.Y))), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Width, Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Height)), drawPlayer.inventory[drawPlayer.selectedItem].GetAlpha(color13), drawPlayer.itemRotation, new Vector2((float)Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Width * 0.5f - (float)Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Width * 0.5f * (float)drawPlayer.direction, 0f), drawPlayer.inventory[drawPlayer.selectedItem].scale, effects2, 0f);
						if (drawPlayer.inventory[drawPlayer.selectedItem].color != default(Color))
						{
							this.spriteBatch.Draw(Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type], new Vector2((float)((int)(drawPlayer.itemLocation.X - Main.screenPosition.X)), (float)((int)(drawPlayer.itemLocation.Y - Main.screenPosition.Y))), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Width, Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Height)), drawPlayer.inventory[drawPlayer.selectedItem].GetColor(color13), drawPlayer.itemRotation, new Vector2((float)Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Width * 0.5f - (float)Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Width * 0.5f * (float)drawPlayer.direction, 0f), drawPlayer.inventory[drawPlayer.selectedItem].scale, effects2, 0f);
						}
					}
					else
					{
						if (drawPlayer.inventory[drawPlayer.selectedItem].type == 425 || drawPlayer.inventory[drawPlayer.selectedItem].type == 507)
						{
							if (drawPlayer.gravDir == 1f)
							{
								if (drawPlayer.direction == 1)
								{
									effects2 = SpriteEffects.FlipVertically;
								}
								else
								{
									effects2 = (SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically);
								}
							}
							else
							{
								if (drawPlayer.direction == 1)
								{
									effects2 = SpriteEffects.None;
								}
								else
								{
									effects2 = SpriteEffects.FlipHorizontally;
								}
							}
						}
						this.spriteBatch.Draw(Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type], new Vector2((float)((int)(drawPlayer.itemLocation.X - Main.screenPosition.X)), (float)((int)(drawPlayer.itemLocation.Y - Main.screenPosition.Y))), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Width, Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Height)), drawPlayer.inventory[drawPlayer.selectedItem].GetAlpha(color13), drawPlayer.itemRotation, new Vector2((float)Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Width * 0.5f - (float)Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Width * 0.5f * (float)drawPlayer.direction, (float)Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Height), drawPlayer.inventory[drawPlayer.selectedItem].scale, effects2, 0f);
						if (drawPlayer.inventory[drawPlayer.selectedItem].color != default(Color))
						{
							this.spriteBatch.Draw(Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type], new Vector2((float)((int)(drawPlayer.itemLocation.X - Main.screenPosition.X)), (float)((int)(drawPlayer.itemLocation.Y - Main.screenPosition.Y))), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Width, Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Height)), drawPlayer.inventory[drawPlayer.selectedItem].GetColor(color13), drawPlayer.itemRotation, new Vector2((float)Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Width * 0.5f - (float)Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Width * 0.5f * (float)drawPlayer.direction, (float)Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Height), drawPlayer.inventory[drawPlayer.selectedItem].scale, effects2, 0f);
						}
					}
				}
			}
			if (drawPlayer.body > 0 && drawPlayer.body < 26)
			{
				this.spriteBatch.Draw(Main.armorArmTexture[drawPlayer.body], new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f))) + drawPlayer.bodyPosition + new Vector2((float)(drawPlayer.bodyFrame.Width / 2), (float)(drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawPlayer.bodyFrame), color11, drawPlayer.bodyRotation, origin, 1f, effects, 0f);
				if ((drawPlayer.body == 10 || drawPlayer.body == 11 || drawPlayer.body == 12 || drawPlayer.body == 13 || drawPlayer.body == 14 || drawPlayer.body == 15 || drawPlayer.body == 16 || drawPlayer.body == 20) && !drawPlayer.invis)
				{
					this.spriteBatch.Draw(Main.playerHands2Texture, new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f))) + drawPlayer.bodyPosition + new Vector2((float)(drawPlayer.bodyFrame.Width / 2), (float)(drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawPlayer.bodyFrame), color5, drawPlayer.bodyRotation, origin, 1f, effects, 0f);
					return;
				}
			}
			else
			{
				if (!drawPlayer.invis)
				{
					if (!drawPlayer.male)
					{
						this.spriteBatch.Draw(Main.femaleUnderShirt2Texture, new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f))) + drawPlayer.bodyPosition + new Vector2((float)(drawPlayer.bodyFrame.Width / 2), (float)(drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawPlayer.bodyFrame), color7, drawPlayer.bodyRotation, origin, 1f, effects, 0f);
						this.spriteBatch.Draw(Main.femaleShirt2Texture, new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f))) + drawPlayer.bodyPosition + new Vector2((float)(drawPlayer.bodyFrame.Width / 2), (float)(drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawPlayer.bodyFrame), color6, drawPlayer.bodyRotation, origin, 1f, effects, 0f);
					}
					else
					{
						this.spriteBatch.Draw(Main.playerUnderShirt2Texture, new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f))) + drawPlayer.bodyPosition + new Vector2((float)(drawPlayer.bodyFrame.Width / 2), (float)(drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawPlayer.bodyFrame), color7, drawPlayer.bodyRotation, origin, 1f, effects, 0f);
					}
					this.spriteBatch.Draw(Main.playerHands2Texture, new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f))) + drawPlayer.bodyPosition + new Vector2((float)(drawPlayer.bodyFrame.Width / 2), (float)(drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawPlayer.bodyFrame), color5, drawPlayer.bodyRotation, origin, 1f, effects, 0f);
				}
			}
		}
		private static void HelpText()
		{
			bool flag = false;
			if (Main.player[Main.myPlayer].statLifeMax > 100)
			{
				flag = true;
			}
			bool flag2 = false;
			if (Main.player[Main.myPlayer].statManaMax > 0)
			{
				flag2 = true;
			}
			bool flag3 = true;
			bool flag4 = false;
			bool flag5 = false;
			bool flag6 = false;
			bool flag7 = false;
			bool flag8 = false;
			bool flag9 = false;
			for (int i = 0; i < 48; i++)
			{
				if (Main.player[Main.myPlayer].inventory[i].pick > 0 && Main.player[Main.myPlayer].inventory[i].name != "Copper Pickaxe")
				{
					flag3 = false;
				}
				if (Main.player[Main.myPlayer].inventory[i].axe > 0 && Main.player[Main.myPlayer].inventory[i].name != "Copper Axe")
				{
					flag3 = false;
				}
				if (Main.player[Main.myPlayer].inventory[i].hammer > 0)
				{
					flag3 = false;
				}
				if (Main.player[Main.myPlayer].inventory[i].type == 11 || Main.player[Main.myPlayer].inventory[i].type == 12 || Main.player[Main.myPlayer].inventory[i].type == 13 || Main.player[Main.myPlayer].inventory[i].type == 14)
				{
					flag4 = true;
				}
				if (Main.player[Main.myPlayer].inventory[i].type == 19 || Main.player[Main.myPlayer].inventory[i].type == 20 || Main.player[Main.myPlayer].inventory[i].type == 21 || Main.player[Main.myPlayer].inventory[i].type == 22)
				{
					flag5 = true;
				}
				if (Main.player[Main.myPlayer].inventory[i].type == 75)
				{
					flag6 = true;
				}
				if (Main.player[Main.myPlayer].inventory[i].type == 75)
				{
					flag7 = true;
				}
				if (Main.player[Main.myPlayer].inventory[i].type == 68 || Main.player[Main.myPlayer].inventory[i].type == 70)
				{
					flag8 = true;
				}
				if (Main.player[Main.myPlayer].inventory[i].type == 84)
				{
					flag9 = true;
				}
			}
			bool flag10 = false;
			bool flag11 = false;
			bool flag12 = false;
			bool flag13 = false;
			bool flag14 = false;
			bool flag15 = false;
			bool flag16 = false;
			bool flag17 = false;
			bool flag18 = false;
			for (int j = 0; j < 200; j++)
			{
				if (Main.npc[j].active)
				{
					if (Main.npc[j].type == 17)
					{
						flag10 = true;
					}
					if (Main.npc[j].type == 18)
					{
						flag11 = true;
					}
					if (Main.npc[j].type == 19)
					{
						flag13 = true;
					}
					if (Main.npc[j].type == 20)
					{
						flag12 = true;
					}
					if (Main.npc[j].type == 54)
					{
						flag18 = true;
					}
					if (Main.npc[j].type == 124)
					{
						flag15 = true;
					}
					if (Main.npc[j].type == 107)
					{
						flag14 = true;
					}
					if (Main.npc[j].type == 108)
					{
						flag16 = true;
					}
					if (Main.npc[j].type == 38)
					{
						flag17 = true;
					}
				}
			}
			while (true)
			{
				Main.helpText++;
				if (flag3)
				{
					if (Main.helpText == 1)
					{
						break;
					}
					if (Main.helpText == 2)
					{
						goto Block_31;
					}
					if (Main.helpText == 3)
					{
						goto Block_32;
					}
					if (Main.helpText == 4)
					{
						goto Block_33;
					}
					if (Main.helpText == 5)
					{
						goto Block_34;
					}
					if (Main.helpText == 6)
					{
						goto Block_35;
					}
				}
				if (flag3 && !flag4 && !flag5 && Main.helpText == 11)
				{
					goto Block_39;
				}
				if (flag3 && flag4 && !flag5)
				{
					if (Main.helpText == 21)
					{
						goto Block_43;
					}
					if (Main.helpText == 22)
					{
						goto Block_44;
					}
				}
				if (flag3 && flag5)
				{
					if (Main.helpText == 31)
					{
						goto Block_47;
					}
					if (Main.helpText == 32)
					{
						goto Block_48;
					}
				}
				if (!flag && Main.helpText == 41)
				{
					goto Block_50;
				}
				if (!flag2 && Main.helpText == 42)
				{
					goto Block_52;
				}
				if (!flag2 && !flag6 && Main.helpText == 43)
				{
					goto Block_55;
				}
				if (!flag10 && !flag11)
				{
					if (Main.helpText == 51)
					{
						goto Block_58;
					}
					if (Main.helpText == 52)
					{
						goto Block_59;
					}
					if (Main.helpText == 53)
					{
						goto Block_60;
					}
					if (Main.helpText == 54)
					{
						goto Block_61;
					}
				}
				if (!flag10 && Main.helpText == 61)
				{
					goto Block_63;
				}
				if (!flag11 && Main.helpText == 62)
				{
					goto Block_65;
				}
				if (!flag13 && Main.helpText == 63)
				{
					goto Block_67;
				}
				if (!flag12 && Main.helpText == 64)
				{
					goto Block_69;
				}
				if (!flag15 && Main.helpText == 65 && NPC.downedBoss3)
				{
					goto Block_72;
				}
				if (!flag18 && Main.helpText == 66 && NPC.downedBoss3)
				{
					goto Block_75;
				}
				if (!flag14 && Main.helpText == 67)
				{
					goto Block_77;
				}
				if (!flag17 && NPC.downedBoss2 && Main.helpText == 68)
				{
					goto Block_80;
				}
				if (!flag16 && Main.hardMode && Main.helpText == 69)
				{
					goto Block_83;
				}
				if (flag7 && Main.helpText == 71)
				{
					goto Block_85;
				}
				if (flag8 && Main.helpText == 72)
				{
					goto Block_87;
				}
				if ((flag7 || flag8) && Main.helpText == 80)
				{
					goto Block_89;
				}
				if (!flag9 && Main.helpText == 201 && !Main.hardMode && !NPC.downedBoss3 && !NPC.downedBoss2)
				{
					goto Block_94;
				}
				if (Main.helpText == 1000 && !NPC.downedBoss1 && !NPC.downedBoss2)
				{
					goto Block_97;
				}
				if (Main.helpText == 1001 && !NPC.downedBoss1 && !NPC.downedBoss2)
				{
					goto Block_100;
				}
				if (Main.helpText == 1002 && !NPC.downedBoss3)
				{
					goto Block_102;
				}
				if (Main.helpText == 1050 && !NPC.downedBoss1 && Main.player[Main.myPlayer].statLifeMax < 200)
				{
					goto Block_105;
				}
				if (Main.helpText == 1051 && !NPC.downedBoss1 && Main.player[Main.myPlayer].statDefense <= 10)
				{
					goto Block_108;
				}
				if (Main.helpText == 1052 && !NPC.downedBoss1 && Main.player[Main.myPlayer].statLifeMax >= 200 && Main.player[Main.myPlayer].statDefense > 10)
				{
					goto Block_112;
				}
				if (Main.helpText == 1053 && NPC.downedBoss1 && !NPC.downedBoss2 && Main.player[Main.myPlayer].statLifeMax < 300)
				{
					goto Block_116;
				}
				if (Main.helpText == 1054 && NPC.downedBoss1 && !NPC.downedBoss2 && Main.player[Main.myPlayer].statLifeMax >= 300)
				{
					goto Block_120;
				}
				if (Main.helpText == 1055 && NPC.downedBoss1 && !NPC.downedBoss2 && Main.player[Main.myPlayer].statLifeMax >= 300)
				{
					goto Block_124;
				}
				if (Main.helpText == 1056 && NPC.downedBoss1 && NPC.downedBoss2 && !NPC.downedBoss3)
				{
					goto Block_128;
				}
				if (Main.helpText == 1057 && NPC.downedBoss1 && NPC.downedBoss2 && NPC.downedBoss3 && !Main.hardMode && Main.player[Main.myPlayer].statLifeMax < 400)
				{
					goto Block_134;
				}
				if (Main.helpText == 1058 && NPC.downedBoss1 && NPC.downedBoss2 && NPC.downedBoss3 && !Main.hardMode && Main.player[Main.myPlayer].statLifeMax >= 400)
				{
					goto Block_140;
				}
				if (Main.helpText == 1059 && NPC.downedBoss1 && NPC.downedBoss2 && NPC.downedBoss3 && !Main.hardMode && Main.player[Main.myPlayer].statLifeMax >= 400)
				{
					goto Block_146;
				}
				if (Main.helpText == 1060 && NPC.downedBoss1 && NPC.downedBoss2 && NPC.downedBoss3 && !Main.hardMode && Main.player[Main.myPlayer].statLifeMax >= 400)
				{
					goto Block_152;
				}
				if (Main.helpText == 1061 && Main.hardMode)
				{
					goto Block_154;
				}
				if (Main.helpText == 1062 && Main.hardMode)
				{
					goto Block_156;
				}
				if (Main.helpText > 1100)
				{
					Main.helpText = 0;
				}
			}
			Main.npcChatText = Lang.dialog(177);
			return;
			Block_31:
			Main.npcChatText = Lang.dialog(178);
			return;
			Block_32:
			Main.npcChatText = Lang.dialog(179);
			return;
			Block_33:
			Main.npcChatText = Lang.dialog(180);
			return;
			Block_34:
			Main.npcChatText = Lang.dialog(181);
			return;
			Block_35:
			Main.npcChatText = Lang.dialog(182);
			return;
			Block_39:
			Main.npcChatText = Lang.dialog(183);
			return;
			Block_43:
			Main.npcChatText = Lang.dialog(184);
			return;
			Block_44:
			Main.npcChatText = Lang.dialog(185);
			return;
			Block_47:
			Main.npcChatText = Lang.dialog(186);
			return;
			Block_48:
			Main.npcChatText = Lang.dialog(187);
			return;
			Block_50:
			Main.npcChatText = Lang.dialog(188);
			return;
			Block_52:
			Main.npcChatText = Lang.dialog(189);
			return;
			Block_55:
			Main.npcChatText = Lang.dialog(190);
			return;
			Block_58:
			Main.npcChatText = Lang.dialog(191);
			return;
			Block_59:
			Main.npcChatText = Lang.dialog(192);
			return;
			Block_60:
			Main.npcChatText = Lang.dialog(193);
			return;
			Block_61:
			Main.npcChatText = Lang.dialog(194);
			return;
			Block_63:
			Main.npcChatText = Lang.dialog(195);
			return;
			Block_65:
			Main.npcChatText = Lang.dialog(196);
			return;
			Block_67:
			Main.npcChatText = Lang.dialog(197);
			return;
			Block_69:
			Main.npcChatText = Lang.dialog(198);
			return;
			Block_72:
			Main.npcChatText = Lang.dialog(199);
			return;
			Block_75:
			Main.npcChatText = Lang.dialog(200);
			return;
			Block_77:
			Main.npcChatText = Lang.dialog(201);
			return;
			Block_80:
			Main.npcChatText = Lang.dialog(202);
			return;
			Block_83:
			Main.npcChatText = Lang.dialog(203);
			return;
			Block_85:
			Main.npcChatText = Lang.dialog(204);
			return;
			Block_87:
			Main.npcChatText = Lang.dialog(205);
			return;
			Block_89:
			Main.npcChatText = Lang.dialog(206);
			return;
			Block_94:
			Main.npcChatText = Lang.dialog(207);
			return;
			Block_97:
			Main.npcChatText = Lang.dialog(208);
			return;
			Block_100:
			Main.npcChatText = Lang.dialog(209);
			return;
			Block_102:
			Main.npcChatText = Lang.dialog(210);
			return;
			Block_105:
			Main.npcChatText = Lang.dialog(211);
			return;
			Block_108:
			Main.npcChatText = Lang.dialog(212);
			return;
			Block_112:
			Main.npcChatText = Lang.dialog(213);
			return;
			Block_116:
			Main.npcChatText = Lang.dialog(214);
			return;
			Block_120:
			Main.npcChatText = Lang.dialog(215);
			return;
			Block_124:
			Main.npcChatText = Lang.dialog(216);
			return;
			Block_128:
			Main.npcChatText = Lang.dialog(217);
			return;
			Block_134:
			Main.npcChatText = Lang.dialog(218);
			return;
			Block_140:
			Main.npcChatText = Lang.dialog(219);
			return;
			Block_146:
			Main.npcChatText = Lang.dialog(220);
			return;
			Block_152:
			Main.npcChatText = Lang.dialog(221);
			return;
			Block_154:
			Main.npcChatText = Lang.dialog(222);
			return;
			Block_156:
			Main.npcChatText = Lang.dialog(223);
		}
		protected void DrawChat()
		{
			if (Main.player[Main.myPlayer].talkNPC < 0 && Main.player[Main.myPlayer].sign == -1)
			{
				Main.npcChatText = "";
				return;
			}
			if (Main.netMode == 0 && Main.autoPause && Main.player[Main.myPlayer].talkNPC >= 0)
			{
				if (Main.npc[Main.player[Main.myPlayer].talkNPC].type == 105)
				{
					Main.npc[Main.player[Main.myPlayer].talkNPC].Transform(107);
				}
				if (Main.npc[Main.player[Main.myPlayer].talkNPC].type == 106)
				{
					Main.npc[Main.player[Main.myPlayer].talkNPC].Transform(108);
				}
				if (Main.npc[Main.player[Main.myPlayer].talkNPC].type == 123)
				{
					Main.npc[Main.player[Main.myPlayer].talkNPC].Transform(124);
				}
			}
			Color color = new Color(200, 200, 200, 200);
			int num = (int)((Main.mouseTextColor * 2 + 255) / 3);
			Color color2 = new Color(num, num, num, num);
			int num2 = 10;
			int num3 = 0;
			string[] array = new string[num2];
			int num4 = 0;
			int num5 = 0;
			if (Main.npcChatText == null)
			{
				Main.npcChatText = "";
			}
			for (int i = 0; i < Main.npcChatText.Length; i++)
			{
				byte[] bytes = Encoding.ASCII.GetBytes(Main.npcChatText.Substring(i, 1));
				if (bytes[0] == 10)
				{
					array[num3] = Main.npcChatText.Substring(num4, i - num4);
					num3++;
					num4 = i + 1;
					num5 = i + 1;
				}
				else
				{
					if (Main.npcChatText.Substring(i, 1) == " " || i == Main.npcChatText.Length - 1)
					{
						if (Main.fontMouseText.MeasureString(Main.npcChatText.Substring(num4, i - num4)).X > 470f)
						{
							array[num3] = Main.npcChatText.Substring(num4, num5 - num4);
							num3++;
							num4 = num5 + 1;
						}
						num5 = i;
					}
				}
				if (num3 == 10)
				{
					Main.npcChatText = Main.npcChatText.Substring(0, i - 1);
					num4 = i - 1;
					num3 = 9;
					break;
				}
			}
			if (num3 < 10)
			{
				array[num3] = Main.npcChatText.Substring(num4, Main.npcChatText.Length - num4);
			}
			if (Main.editSign)
			{
				this.textBlinkerCount++;
				if (this.textBlinkerCount >= 20)
				{
					if (this.textBlinkerState == 0)
					{
						this.textBlinkerState = 1;
					}
					else
					{
						this.textBlinkerState = 0;
					}
					this.textBlinkerCount = 0;
				}
				if (this.textBlinkerState == 1)
				{
					string[] array2;
					IntPtr intPtr;
					(array2 = array)[(int)(intPtr = (IntPtr)num3)] = array2[(int)intPtr] + "|";
				}
			}
			num3++;
			this.spriteBatch.Draw(Main.chatBackTexture, new Vector2((float)(Main.screenWidth / 2 - Main.chatBackTexture.Width / 2), 100f), new Rectangle?(new Rectangle(0, 0, Main.chatBackTexture.Width, (num3 + 1) * 30)), color, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
			this.spriteBatch.Draw(Main.chatBackTexture, new Vector2((float)(Main.screenWidth / 2 - Main.chatBackTexture.Width / 2), (float)(100 + (num3 + 1) * 30)), new Rectangle?(new Rectangle(0, Main.chatBackTexture.Height - 30, Main.chatBackTexture.Width, 30)), color, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
			for (int j = 0; j < num3; j++)
			{
				for (int k = 0; k < 5; k++)
				{
					Color color3 = Color.Black;
					int num6 = 170 + (Main.screenWidth - 800) / 2;
					int num7 = 120 + j * 30;
					if (k == 0)
					{
						num6 -= 2;
					}
					if (k == 1)
					{
						num6 += 2;
					}
					if (k == 2)
					{
						num7 -= 2;
					}
					if (k == 3)
					{
						num7 += 2;
					}
					if (k == 4)
					{
						color3 = color2;
					}
					this.spriteBatch.DrawString(Main.fontMouseText, array[j], new Vector2((float)num6, (float)num7), color3, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
				}
			}
			num = (int)Main.mouseTextColor;
			color2 = new Color(num, (int)((double)num / 1.1), num / 2, num);
			string text = "";
			string text2 = "";
			int num8 = Main.player[Main.myPlayer].statLifeMax - Main.player[Main.myPlayer].statLife;
			for (int l = 0; l < 10; l++)
			{
				int num9 = Main.player[Main.myPlayer].buffType[l];
				if (Main.debuff[num9] && Main.player[Main.myPlayer].buffTime[l] > 0 && num9 != 28 && num9 != 34)
				{
					num8 += 1000;
				}
			}
			if (Main.player[Main.myPlayer].sign > -1)
			{
				if (Main.editSign)
				{
					text = Lang.inter[47];
				}
				else
				{
					text = Lang.inter[48];
				}
			}
			else
			{
				if (Main.npc[Main.player[Main.myPlayer].talkNPC].type == 20)
				{
					text = Lang.inter[28];
					text2 = Lang.inter[49];
				}
				else
				{
					if (Main.npc[Main.player[Main.myPlayer].talkNPC].type == 17 || Main.npc[Main.player[Main.myPlayer].talkNPC].type == 19 || Main.npc[Main.player[Main.myPlayer].talkNPC].type == 38 || Main.npc[Main.player[Main.myPlayer].talkNPC].type == 54 || Main.npc[Main.player[Main.myPlayer].talkNPC].type == 107 || Main.npc[Main.player[Main.myPlayer].talkNPC].type == 108 || Main.npc[Main.player[Main.myPlayer].talkNPC].type == 124 || Main.npc[Main.player[Main.myPlayer].talkNPC].type == 142)
					{
						text = Lang.inter[28];
						if (Main.npc[Main.player[Main.myPlayer].talkNPC].type == 107)
						{
							text2 = Lang.inter[19];
						}
					}
					else
					{
						if (Main.npc[Main.player[Main.myPlayer].talkNPC].type == 37)
						{
							if (!Main.dayTime)
							{
								text = Lang.inter[50];
							}
						}
						else
						{
							if (Main.npc[Main.player[Main.myPlayer].talkNPC].type == 22)
							{
								text = Lang.inter[51];
								text2 = Lang.inter[25];
							}
							else
							{
								if (Main.npc[Main.player[Main.myPlayer].talkNPC].type == 18)
								{
									string text3 = "";
									int num10 = 0;
									int num11 = 0;
									int num12 = 0;
									int num13 = 0;
									int num14 = num8;
									if (num14 > 0)
									{
										num14 = (int)((double)num14 * 0.75);
										if (num14 < 1)
										{
											num14 = 1;
										}
									}
									if (num14 < 0)
									{
										num14 = 0;
									}
									num8 = num14;
									if (num14 >= 1000000)
									{
										num10 = num14 / 1000000;
										num14 -= num10 * 1000000;
									}
									if (num14 >= 10000)
									{
										num11 = num14 / 10000;
										num14 -= num11 * 10000;
									}
									if (num14 >= 100)
									{
										num12 = num14 / 100;
										num14 -= num12 * 100;
									}
									if (num14 >= 1)
									{
										num13 = num14;
									}
									if (num10 > 0)
									{
										object obj = text3;
										text3 = string.Concat(new object[]
										{
											obj,
											num10,
											" ",
											Lang.inter[15],
											" "
										});
									}
									if (num11 > 0)
									{
										object obj2 = text3;
										text3 = string.Concat(new object[]
										{
											obj2,
											num11,
											" ",
											Lang.inter[16],
											" "
										});
									}
									if (num12 > 0)
									{
										object obj = text3;
										text3 = string.Concat(new object[]
										{
											obj,
											num12,
											" ",
											Lang.inter[17],
											" "
										});
									}
									if (num13 > 0)
									{
										object obj = text3;
										text3 = string.Concat(new object[]
										{
											obj,
											num13,
											" ",
											Lang.inter[18],
											" "
										});
									}
									float num15 = (float)Main.mouseTextColor / 255f;
									if (num10 > 0)
									{
										color2 = new Color((int)((byte)(220f * num15)), (int)((byte)(220f * num15)), (int)((byte)(198f * num15)), (int)Main.mouseTextColor);
									}
									else
									{
										if (num11 > 0)
										{
											color2 = new Color((int)((byte)(224f * num15)), (int)((byte)(201f * num15)), (int)((byte)(92f * num15)), (int)Main.mouseTextColor);
										}
										else
										{
											if (num12 > 0)
											{
												color2 = new Color((int)((byte)(181f * num15)), (int)((byte)(192f * num15)), (int)((byte)(193f * num15)), (int)Main.mouseTextColor);
											}
											else
											{
												if (num13 > 0)
												{
													color2 = new Color((int)((byte)(246f * num15)), (int)((byte)(138f * num15)), (int)((byte)(96f * num15)), (int)Main.mouseTextColor);
												}
											}
										}
									}
									text = Lang.inter[54] + " (" + text3 + ")";
									if (num14 == 0)
									{
										text = Lang.inter[54];
									}
								}
							}
						}
					}
				}
			}
			int num16 = 180 + (Main.screenWidth - 800) / 2;
			int num17 = 130 + num3 * 30;
			float scale = 0.9f;
			if (Main.mouseX > num16 && (float)Main.mouseX < (float)num16 + Main.fontMouseText.MeasureString(text).X && Main.mouseY > num17 && (float)Main.mouseY < (float)num17 + Main.fontMouseText.MeasureString(text).Y)
			{
				Main.player[Main.myPlayer].mouseInterface = true;
				scale = 1.1f;
				if (!Main.npcChatFocus2)
				{
					Main.PlaySound(12, -1, -1, 1);
				}
				Main.npcChatFocus2 = true;
				Main.player[Main.myPlayer].releaseUseItem = false;
			}
			else
			{
				if (Main.npcChatFocus2)
				{
					Main.PlaySound(12, -1, -1, 1);
				}
				Main.npcChatFocus2 = false;
			}
			for (int m = 0; m < 5; m++)
			{
				int num18 = num16;
				int num19 = num17;
				Color color4 = Color.Black;
				if (m == 0)
				{
					num18 -= 2;
				}
				if (m == 1)
				{
					num18 += 2;
				}
				if (m == 2)
				{
					num19 -= 2;
				}
				if (m == 3)
				{
					num19 += 2;
				}
				if (m == 4)
				{
					color4 = color2;
				}
				Vector2 vector = Main.fontMouseText.MeasureString(text);
				vector *= 0.5f;
				this.spriteBatch.DrawString(Main.fontMouseText, text, new Vector2((float)num18 + vector.X, (float)num19 + vector.Y), color4, 0f, vector, scale, SpriteEffects.None, 0f);
			}
			string text4 = Lang.inter[52];
			color2 = new Color(num, (int)((double)num / 1.1), num / 2, num);
			num16 = num16 + (int)Main.fontMouseText.MeasureString(text).X + 20;
			int num20 = num16 + (int)Main.fontMouseText.MeasureString(text4).X;
			num17 = 130 + num3 * 30;
			scale = 0.9f;
			if (Main.mouseX > num16 && (float)Main.mouseX < (float)num16 + Main.fontMouseText.MeasureString(text4).X && Main.mouseY > num17 && (float)Main.mouseY < (float)num17 + Main.fontMouseText.MeasureString(text4).Y)
			{
				scale = 1.1f;
				if (!Main.npcChatFocus1)
				{
					Main.PlaySound(12, -1, -1, 1);
				}
				Main.npcChatFocus1 = true;
				Main.player[Main.myPlayer].releaseUseItem = false;
				Main.player[Main.myPlayer].controlUseItem = false;
			}
			else
			{
				if (Main.npcChatFocus1)
				{
					Main.PlaySound(12, -1, -1, 1);
				}
				Main.npcChatFocus1 = false;
			}
			for (int n = 0; n < 5; n++)
			{
				int num21 = num16;
				int num22 = num17;
				Color color5 = Color.Black;
				if (n == 0)
				{
					num21 -= 2;
				}
				if (n == 1)
				{
					num21 += 2;
				}
				if (n == 2)
				{
					num22 -= 2;
				}
				if (n == 3)
				{
					num22 += 2;
				}
				if (n == 4)
				{
					color5 = color2;
				}
				Vector2 vector2 = Main.fontMouseText.MeasureString(text4);
				vector2 *= 0.5f;
				this.spriteBatch.DrawString(Main.fontMouseText, text4, new Vector2((float)num21 + vector2.X, (float)num22 + vector2.Y), color5, 0f, vector2, scale, SpriteEffects.None, 0f);
			}
			if (text2 != "")
			{
				num16 = num20 + (int)Main.fontMouseText.MeasureString(text2).X / 3;
				num17 = 130 + num3 * 30;
				scale = 0.9f;
				if (Main.mouseX > num16 && (float)Main.mouseX < (float)num16 + Main.fontMouseText.MeasureString(text2).X && Main.mouseY > num17 && (float)Main.mouseY < (float)num17 + Main.fontMouseText.MeasureString(text2).Y)
				{
					Main.player[Main.myPlayer].mouseInterface = true;
					scale = 1.1f;
					if (!Main.npcChatFocus3)
					{
						Main.PlaySound(12, -1, -1, 1);
					}
					Main.npcChatFocus3 = true;
					Main.player[Main.myPlayer].releaseUseItem = false;
				}
				else
				{
					if (Main.npcChatFocus3)
					{
						Main.PlaySound(12, -1, -1, 1);
					}
					Main.npcChatFocus3 = false;
				}
				for (int num23 = 0; num23 < 5; num23++)
				{
					int num24 = num16;
					int num25 = num17;
					Color color6 = Color.Black;
					if (num23 == 0)
					{
						num24 -= 2;
					}
					if (num23 == 1)
					{
						num24 += 2;
					}
					if (num23 == 2)
					{
						num25 -= 2;
					}
					if (num23 == 3)
					{
						num25 += 2;
					}
					if (num23 == 4)
					{
						color6 = color2;
					}
					Vector2 vector3 = Main.fontMouseText.MeasureString(text);
					vector3 *= 0.5f;
					this.spriteBatch.DrawString(Main.fontMouseText, text2, new Vector2((float)num24 + vector3.X, (float)num25 + vector3.Y), color6, 0f, vector3, scale, SpriteEffects.None, 0f);
				}
			}
			if (Main.mouseLeft && Main.mouseLeftRelease)
			{
				Main.mouseLeftRelease = false;
				Main.player[Main.myPlayer].releaseUseItem = false;
				Main.player[Main.myPlayer].mouseInterface = true;
				if (Main.npcChatFocus1)
				{
					Main.player[Main.myPlayer].talkNPC = -1;
					Main.player[Main.myPlayer].sign = -1;
					Main.editSign = false;
					Main.npcChatText = "";
					Main.PlaySound(11, -1, -1, 1);
					return;
				}
				if (Main.npcChatFocus2)
				{
					if (Main.player[Main.myPlayer].sign != -1)
					{
						if (!Main.editSign)
						{
							Main.PlaySound(12, -1, -1, 1);
							Main.editSign = true;
							Main.clrInput();
							return;
						}
						Main.PlaySound(12, -1, -1, 1);
						int num26 = Main.player[Main.myPlayer].sign;
						Sign.TextSign(num26, Main.npcChatText);
						Main.editSign = false;
						if (Main.netMode == 1)
						{
							NetMessage.SendData(47, -1, -1, "", num26, 0f, 0f, 0f, 0);
							return;
						}
					}
					else
					{
						if (Main.npc[Main.player[Main.myPlayer].talkNPC].type == 17)
						{
							Main.playerInventory = true;
							Main.npcChatText = "";
							Main.npcShop = 1;
							this.shop[Main.npcShop].SetupShop(Main.npcShop);
							Main.PlaySound(12, -1, -1, 1);
							return;
						}
						if (Main.npc[Main.player[Main.myPlayer].talkNPC].type == 19)
						{
							Main.playerInventory = true;
							Main.npcChatText = "";
							Main.npcShop = 2;
							this.shop[Main.npcShop].SetupShop(Main.npcShop);
							Main.PlaySound(12, -1, -1, 1);
							return;
						}
						if (Main.npc[Main.player[Main.myPlayer].talkNPC].type == 124)
						{
							Main.playerInventory = true;
							Main.npcChatText = "";
							Main.npcShop = 8;
							this.shop[Main.npcShop].SetupShop(Main.npcShop);
							Main.PlaySound(12, -1, -1, 1);
							return;
						}
						if (Main.npc[Main.player[Main.myPlayer].talkNPC].type == 142)
						{
							Main.playerInventory = true;
							Main.npcChatText = "";
							Main.npcShop = 9;
							this.shop[Main.npcShop].SetupShop(Main.npcShop);
							Main.PlaySound(12, -1, -1, 1);
							return;
						}
						if (Main.npc[Main.player[Main.myPlayer].talkNPC].type == 37)
						{
							if (Main.netMode == 0)
							{
								NPC.SpawnSkeletron();
							}
							else
							{
								NetMessage.SendData(51, -1, -1, "", Main.myPlayer, 1f, 0f, 0f, 0);
							}
							Main.npcChatText = "";
							return;
						}
						if (Main.npc[Main.player[Main.myPlayer].talkNPC].type == 20)
						{
							Main.playerInventory = true;
							Main.npcChatText = "";
							Main.npcShop = 3;
							this.shop[Main.npcShop].SetupShop(Main.npcShop);
							Main.PlaySound(12, -1, -1, 1);
							return;
						}
						if (Main.npc[Main.player[Main.myPlayer].talkNPC].type == 38)
						{
							Main.playerInventory = true;
							Main.npcChatText = "";
							Main.npcShop = 4;
							this.shop[Main.npcShop].SetupShop(Main.npcShop);
							Main.PlaySound(12, -1, -1, 1);
							return;
						}
						if (Main.npc[Main.player[Main.myPlayer].talkNPC].type == 54)
						{
							Main.playerInventory = true;
							Main.npcChatText = "";
							Main.npcShop = 5;
							this.shop[Main.npcShop].SetupShop(Main.npcShop);
							Main.PlaySound(12, -1, -1, 1);
							return;
						}
						if (Main.npc[Main.player[Main.myPlayer].talkNPC].type == 107)
						{
							Main.playerInventory = true;
							Main.npcChatText = "";
							Main.npcShop = 6;
							this.shop[Main.npcShop].SetupShop(Main.npcShop);
							Main.PlaySound(12, -1, -1, 1);
							return;
						}
						if (Main.npc[Main.player[Main.myPlayer].talkNPC].type == 108)
						{
							Main.playerInventory = true;
							Main.npcChatText = "";
							Main.npcShop = 7;
							this.shop[Main.npcShop].SetupShop(Main.npcShop);
							Main.PlaySound(12, -1, -1, 1);
							return;
						}
						if (Main.npc[Main.player[Main.myPlayer].talkNPC].type == 22)
						{
							Main.PlaySound(12, -1, -1, 1);
							Main.HelpText();
							return;
						}
						if (Main.npc[Main.player[Main.myPlayer].talkNPC].type == 18)
						{
							Main.PlaySound(12, -1, -1, 1);
							if (num8 > 0)
							{
								if (Main.player[Main.myPlayer].BuyItem(num8))
								{
									Main.PlaySound(2, -1, -1, 4);
									Main.player[Main.myPlayer].HealEffect(Main.player[Main.myPlayer].statLifeMax - Main.player[Main.myPlayer].statLife);
									if ((double)Main.player[Main.myPlayer].statLife < (double)Main.player[Main.myPlayer].statLifeMax * 0.25)
									{
										Main.npcChatText = Lang.dialog(227);
									}
									else
									{
										if ((double)Main.player[Main.myPlayer].statLife < (double)Main.player[Main.myPlayer].statLifeMax * 0.5)
										{
											Main.npcChatText = Lang.dialog(228);
										}
										else
										{
											if ((double)Main.player[Main.myPlayer].statLife < (double)Main.player[Main.myPlayer].statLifeMax * 0.75)
											{
												Main.npcChatText = Lang.dialog(229);
											}
											else
											{
												Main.npcChatText = Lang.dialog(230);
											}
										}
									}
									Main.player[Main.myPlayer].statLife = Main.player[Main.myPlayer].statLifeMax;
									for (int num27 = 0; num27 < 10; num27++)
									{
										int num28 = Main.player[Main.myPlayer].buffType[num27];
										if (Main.debuff[num28] && Main.player[Main.myPlayer].buffTime[num27] > 0 && num28 != 28 && num28 != 34)
										{
											Main.player[Main.myPlayer].DelBuff(num27);
										}
									}
									return;
								}
								int num29 = Main.rand.Next(3);
								if (num29 == 0)
								{
									Main.npcChatText = Lang.dialog(52);
								}
								if (num29 == 1)
								{
									Main.npcChatText = Lang.dialog(53);
								}
								if (num29 == 2)
								{
									Main.npcChatText = Lang.dialog(54);
									return;
								}
							}
							else
							{
								int num30 = Main.rand.Next(3);
								if (num30 == 0)
								{
									Main.npcChatText = Lang.dialog(55);
								}
								if (num30 == 1)
								{
									Main.npcChatText = Lang.dialog(56);
								}
								if (num30 == 2)
								{
									Main.npcChatText = Lang.dialog(57);
									return;
								}
							}
						}
					}
				}
				else
				{
					if (Main.npcChatFocus3 && Main.player[Main.myPlayer].talkNPC >= 0)
					{
						if (Main.npc[Main.player[Main.myPlayer].talkNPC].type == 20)
						{
							Main.PlaySound(12, -1, -1, 1);
							Main.npcChatText = Lang.evilGood();
							return;
						}
						if (Main.npc[Main.player[Main.myPlayer].talkNPC].type == 22)
						{
							Main.playerInventory = true;
							Main.npcChatText = "";
							Main.PlaySound(12, -1, -1, 1);
							Main.craftGuide = true;
							return;
						}
						if (Main.npc[Main.player[Main.myPlayer].talkNPC].type == 107)
						{
							Main.playerInventory = true;
							Main.npcChatText = "";
							Main.PlaySound(12, -1, -1, 1);
							Main.reforge = true;
						}
					}
				}
			}
		}
		private static bool AccCheck(Item newItem, int slot)
		{
			if (Main.player[Main.myPlayer].armor[slot].IsTheSameAs(newItem))
			{
				return false;
			}
			for (int i = 0; i < Main.player[Main.myPlayer].armor.Length; i++)
			{
				if (newItem.IsTheSameAs(Main.player[Main.myPlayer].armor[i]))
				{
					return true;
				}
			}
			return false;
		}
		public static Item armorSwap(Item newItem)
		{
			for (int i = 0; i < Main.player[Main.myPlayer].armor.Length; i++)
			{
				if (newItem.IsTheSameAs(Main.player[Main.myPlayer].armor[i]))
				{
					Main.accSlotCount = i;
				}
			}
			if (newItem.headSlot == -1 && newItem.bodySlot == -1 && newItem.legSlot == -1 && !newItem.accessory)
			{
				return newItem;
			}
			Item result = newItem;
			if (newItem.headSlot != -1)
			{
				result = (Item)Main.player[Main.myPlayer].armor[0].Clone();
				Main.player[Main.myPlayer].armor[0] = (Item)newItem.Clone();
			}
			else
			{
				if (newItem.bodySlot != -1)
				{
					result = (Item)Main.player[Main.myPlayer].armor[1].Clone();
					Main.player[Main.myPlayer].armor[1] = (Item)newItem.Clone();
				}
				else
				{
					if (newItem.legSlot != -1)
					{
						result = (Item)Main.player[Main.myPlayer].armor[2].Clone();
						Main.player[Main.myPlayer].armor[2] = (Item)newItem.Clone();
					}
					else
					{
						if (newItem.accessory)
						{
							for (int j = 3; j < 8; j++)
							{
								if (Main.player[Main.myPlayer].armor[j].type == 0)
								{
									Main.accSlotCount = j - 3;
									break;
								}
							}
							for (int k = 0; k < Main.player[Main.myPlayer].armor.Length; k++)
							{
								if (newItem.IsTheSameAs(Main.player[Main.myPlayer].armor[k]))
								{
									Main.accSlotCount = k - 3;
								}
							}
							if (Main.accSlotCount >= 5)
							{
								Main.accSlotCount = 0;
							}
							if (Main.accSlotCount < 0)
							{
								Main.accSlotCount = 4;
							}
							result = (Item)Main.player[Main.myPlayer].armor[3 + Main.accSlotCount].Clone();
							Main.player[Main.myPlayer].armor[3 + Main.accSlotCount] = (Item)newItem.Clone();
							Main.accSlotCount++;
							if (Main.accSlotCount >= 5)
							{
								Main.accSlotCount = 0;
							}
						}
					}
				}
			}
			Main.PlaySound(7, -1, -1, 1);
			Recipe.FindRecipes();
			return result;
		}
		public static void BankCoins()
		{
			for (int i = 0; i < 20; i++)
			{
				if (Main.player[Main.myPlayer].bank[i].type >= 71 && Main.player[Main.myPlayer].bank[i].type <= 73 && Main.player[Main.myPlayer].bank[i].stack == Main.player[Main.myPlayer].bank[i].maxStack)
				{
					Main.player[Main.myPlayer].bank[i].SetDefaults(Main.player[Main.myPlayer].bank[i].type + 1, false);
					for (int j = 0; j < 20; j++)
					{
						if (j != i && Main.player[Main.myPlayer].bank[j].type == Main.player[Main.myPlayer].bank[i].type && Main.player[Main.myPlayer].bank[j].stack < Main.player[Main.myPlayer].bank[j].maxStack)
						{
							Main.player[Main.myPlayer].bank[j].stack++;
							Main.player[Main.myPlayer].bank[i].SetDefaults(0, false);
							Main.BankCoins();
						}
					}
				}
			}
		}
		public static void ChestCoins()
		{
			for (int i = 0; i < 20; i++)
			{
				if (Main.chest[Main.player[Main.myPlayer].chest].item[i].type >= 71 && Main.chest[Main.player[Main.myPlayer].chest].item[i].type <= 73 && Main.chest[Main.player[Main.myPlayer].chest].item[i].stack == Main.chest[Main.player[Main.myPlayer].chest].item[i].maxStack)
				{
					Main.chest[Main.player[Main.myPlayer].chest].item[i].SetDefaults(Main.chest[Main.player[Main.myPlayer].chest].item[i].type + 1, false);
					for (int j = 0; j < 20; j++)
					{
						if (j != i && Main.chest[Main.player[Main.myPlayer].chest].item[j].type == Main.chest[Main.player[Main.myPlayer].chest].item[i].type && Main.chest[Main.player[Main.myPlayer].chest].item[j].stack < Main.chest[Main.player[Main.myPlayer].chest].item[j].maxStack)
						{
							if (Main.netMode == 1)
							{
								NetMessage.SendData(32, -1, -1, "", Main.player[Main.myPlayer].chest, (float)j, 0f, 0f, 0);
							}
							Main.chest[Main.player[Main.myPlayer].chest].item[j].stack++;
							Main.chest[Main.player[Main.myPlayer].chest].item[i].SetDefaults(0, false);
							Main.ChestCoins();
						}
					}
				}
			}
		}
		protected void DrawNPCHouse()
		{
			for (int i = 0; i < 200; i++)
			{
				if (Main.npc[i].active && Main.npc[i].townNPC && !Main.npc[i].homeless && Main.npc[i].homeTileX > 0 && Main.npc[i].homeTileY > 0 && Main.npc[i].type != 37)
				{
					int num = 1;
					int num2 = Main.npc[i].homeTileX;
					int num3 = Main.npc[i].homeTileY - 1;
					if (Main.tile[num2, num3] != null)
					{
						bool flag = false;
						while (!Main.tile[num2, num3].active || !Main.tileSolid[(int)Main.tile[num2, num3].type])
						{
							num3--;
							if (num3 < 10)
							{
								break;
							}
							if (Main.tile[num2, num3] == null)
							{
								flag = true;
								break;
							}
						}
						if (!flag)
						{
							int num4 = 8;
							int num5 = 18;
							if (Main.tile[num2, num3].type == 19)
							{
								num5 -= 8;
							}
							num3++;
							this.spriteBatch.Draw(Main.bannerTexture[num], new Vector2((float)(num2 * 16 - (int)Main.screenPosition.X + num4), (float)(num3 * 16 - (int)Main.screenPosition.Y + num5)), new Rectangle?(new Rectangle(0, 0, Main.bannerTexture[num].Width, Main.bannerTexture[num].Height)), Lighting.GetColor(num2, num3), 0f, new Vector2((float)(Main.bannerTexture[num].Width / 2), (float)(Main.bannerTexture[num].Height / 2)), 1f, SpriteEffects.None, 0f);
							int num6 = NPC.TypeToNum(Main.npc[i].type);
							float scale = 1f;
							float num7;
							if (Main.npcHeadTexture[num6].Width > Main.npcHeadTexture[num6].Height)
							{
								num7 = (float)Main.npcHeadTexture[num6].Width;
							}
							else
							{
								num7 = (float)Main.npcHeadTexture[num6].Height;
							}
							if (num7 > 24f)
							{
								scale = 24f / num7;
							}
							this.spriteBatch.Draw(Main.npcHeadTexture[num6], new Vector2((float)(num2 * 16 - (int)Main.screenPosition.X + num4), (float)(num3 * 16 - (int)Main.screenPosition.Y + num5 + 2)), new Rectangle?(new Rectangle(0, 0, Main.npcHeadTexture[num6].Width, Main.npcHeadTexture[num6].Height)), Lighting.GetColor(num2, num3), 0f, new Vector2((float)(Main.npcHeadTexture[num6].Width / 2), (float)(Main.npcHeadTexture[num6].Height / 2)), scale, SpriteEffects.None, 0f);
							num2 = num2 * 16 - (int)Main.screenPosition.X + num4 - Main.bannerTexture[num].Width / 2;
							num3 = num3 * 16 - (int)Main.screenPosition.Y + num5 - Main.bannerTexture[num].Height / 2;
							if (Main.mouseX >= num2 && Main.mouseX <= num2 + Main.bannerTexture[num].Width && Main.mouseY >= num3 && Main.mouseY <= num3 + Main.bannerTexture[num].Height)
							{
								this.MouseText(Main.npc[i].displayName + " the " + Main.npc[i].name, 0, 0);
								if (Main.mouseRightRelease && Main.mouseRight)
								{
									Main.mouseRightRelease = false;
									WorldGen.kickOut(i);
									Main.PlaySound(12, -1, -1, 1);
								}
							}
						}
					}
				}
			}
		}
		protected void DrawInterface()
		{
			if (this.showNPCs)
			{
				this.DrawNPCHouse();
			}
			if (Main.player[Main.myPlayer].selectedItem == 48 && Main.player[Main.myPlayer].itemAnimation > 0)
			{
				Main.mouseLeftRelease = false;
			}
			Main.mouseHC = false;
			if (Main.hideUI)
			{
				Main.maxQ = true;
				return;
			}
			if (Main.player[Main.myPlayer].rulerAcc)
			{
				int num = (int)((float)((int)(Main.screenPosition.X / 16f) * 16) - Main.screenPosition.X);
				int num2 = (int)((float)((int)(Main.screenPosition.Y / 16f) * 16) - Main.screenPosition.Y);
				int num3 = Main.screenWidth / Main.gridTexture.Width;
				int num4 = Main.screenHeight / Main.gridTexture.Height;
				for (int i = 0; i <= num3 + 1; i++)
				{
					for (int j = 0; j <= num4 + 1; j++)
					{
						this.spriteBatch.Draw(Main.gridTexture, new Vector2((float)(i * Main.gridTexture.Width + num), (float)(j * Main.gridTexture.Height + num2)), new Rectangle?(new Rectangle(0, 0, Main.gridTexture.Width, Main.gridTexture.Height)), new Color(100, 100, 100, 15), 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
					}
				}
			}
			if (Main.netDiag)
			{
				for (int k = 0; k < 4; k++)
				{
					string text = "";
					int num5 = 20;
					int num6 = 220;
					if (k == 0)
					{
						text = "RX Msgs: " + string.Format("{0:0,0}", Main.rxMsg);
						num6 += k * 20;
					}
					else
					{
						if (k == 1)
						{
							text = "RX Bytes: " + string.Format("{0:0,0}", Main.rxData);
							num6 += k * 20;
						}
						else
						{
							if (k == 2)
							{
								text = "TX Msgs: " + string.Format("{0:0,0}", Main.txMsg);
								num6 += k * 20;
							}
							else
							{
								if (k == 3)
								{
									text = "TX Bytes: " + string.Format("{0:0,0}", Main.txData);
									num6 += k * 20;
								}
							}
						}
					}
					this.spriteBatch.DrawString(Main.fontMouseText, text, new Vector2((float)num5, (float)num6), Color.White, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
				}
				for (int l = 0; l < Main.maxMsg; l++)
				{
					int num7 = 200;
					int num8 = 120;
					num8 += l * 15;
					string text2 = l + ": ";
					this.spriteBatch.DrawString(Main.fontMouseText, text2, new Vector2((float)num7, (float)num8), Color.White, 0f, default(Vector2), 0.8f, SpriteEffects.None, 0f);
					num7 += 30;
					text2 = "rx:" + string.Format("{0:0,0}", Main.rxMsgType[l]);
					this.spriteBatch.DrawString(Main.fontMouseText, text2, new Vector2((float)num7, (float)num8), Color.White, 0f, default(Vector2), 0.8f, SpriteEffects.None, 0f);
					num7 += 70;
					text2 = string.Format("{0:0,0}", Main.rxDataType[l]);
					this.spriteBatch.DrawString(Main.fontMouseText, text2, new Vector2((float)num7, (float)num8), Color.White, 0f, default(Vector2), 0.8f, SpriteEffects.None, 0f);
					num7 += 70;
					text2 = l + ": ";
					this.spriteBatch.DrawString(Main.fontMouseText, text2, new Vector2((float)num7, (float)num8), Color.White, 0f, default(Vector2), 0.8f, SpriteEffects.None, 0f);
					num7 += 30;
					text2 = "tx:" + string.Format("{0:0,0}", Main.txMsgType[l]);
					this.spriteBatch.DrawString(Main.fontMouseText, text2, new Vector2((float)num7, (float)num8), Color.White, 0f, default(Vector2), 0.8f, SpriteEffects.None, 0f);
					num7 += 70;
					text2 = string.Format("{0:0,0}", Main.txDataType[l]);
					this.spriteBatch.DrawString(Main.fontMouseText, text2, new Vector2((float)num7, (float)num8), Color.White, 0f, default(Vector2), 0.8f, SpriteEffects.None, 0f);
				}
			}
			if (Main.drawDiag)
			{
				for (int m = 0; m < 7; m++)
				{
					string text3 = "";
					int num9 = 20;
					int num10 = 220;
					num10 += m * 16;
					if (m == 0)
					{
						text3 = "Solid Tiles:";
					}
					if (m == 1)
					{
						text3 = "Misc. Tiles:";
					}
					if (m == 2)
					{
						text3 = "Walls Tiles:";
					}
					if (m == 3)
					{
						text3 = "Background Tiles:";
					}
					if (m == 4)
					{
						text3 = "Water Tiles:";
					}
					if (m == 5)
					{
						text3 = "Black Tiles:";
					}
					if (m == 6)
					{
						text3 = "Total Render:";
					}
					this.spriteBatch.DrawString(Main.fontMouseText, text3, new Vector2((float)num9, (float)num10), Color.White, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
				}
				for (int n = 0; n < 7; n++)
				{
					string text4 = "";
					int num11 = 180;
					int num12 = 220;
					num12 += n * 16;
					if (n == 0)
					{
						text4 = Main.renderTimer[n] + "ms";
					}
					if (n == 1)
					{
						text4 = Main.renderTimer[n] + "ms";
					}
					if (n == 2)
					{
						text4 = Main.renderTimer[n] + "ms";
					}
					if (n == 3)
					{
						text4 = Main.renderTimer[n] + "ms";
					}
					if (n == 4)
					{
						text4 = Main.renderTimer[n] + "ms";
					}
					if (n == 5)
					{
						text4 = Main.renderTimer[n] + "ms";
					}
					if (n == 6)
					{
						text4 = Main.renderTimer[0] + Main.renderTimer[1] + Main.renderTimer[2] + Main.renderTimer[3] + Main.renderTimer[4] + Main.renderTimer[5] + "ms";
					}
					this.spriteBatch.DrawString(Main.fontMouseText, text4, new Vector2((float)num11, (float)num12), Color.White, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
				}
				for (int num13 = 0; num13 < 6; num13++)
				{
					string text5 = "";
					int num14 = 20;
					int num15 = 346;
					num15 += num13 * 16;
					if (num13 == 0)
					{
						text5 = "Lighting Init:";
					}
					if (num13 == 1)
					{
						text5 = "Lighting Phase #1:";
					}
					if (num13 == 2)
					{
						text5 = "Lighting Phase #2:";
					}
					if (num13 == 3)
					{
						text5 = "Lighting Phase #3";
					}
					if (num13 == 4)
					{
						text5 = "Lighting Phase #4";
					}
					if (num13 == 5)
					{
						text5 = "Total Lighting:";
					}
					this.spriteBatch.DrawString(Main.fontMouseText, text5, new Vector2((float)num14, (float)num15), Color.White, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
				}
				for (int num16 = 0; num16 < 6; num16++)
				{
					string text6 = "";
					int num17 = 180;
					int num18 = 346;
					num18 += num16 * 16;
					if (num16 == 0)
					{
						text6 = Main.lightTimer[num16] + "ms";
					}
					if (num16 == 1)
					{
						text6 = Main.lightTimer[num16] + "ms";
					}
					if (num16 == 2)
					{
						text6 = Main.lightTimer[num16] + "ms";
					}
					if (num16 == 3)
					{
						text6 = Main.lightTimer[num16] + "ms";
					}
					if (num16 == 4)
					{
						text6 = Main.lightTimer[num16] + "ms";
					}
					if (num16 == 5)
					{
						text6 = Main.lightTimer[0] + Main.lightTimer[1] + Main.lightTimer[2] + Main.lightTimer[3] + Main.lightTimer[4] + "ms";
					}
					this.spriteBatch.DrawString(Main.fontMouseText, text6, new Vector2((float)num17, (float)num18), Color.White, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
				}
				int num19 = 5;
				for (int num20 = 0; num20 < num19; num20++)
				{
					int num21 = 20;
					int num22 = 456;
					num22 += num20 * 16;
					string text7 = "Render #" + num20 + ":";
					this.spriteBatch.DrawString(Main.fontMouseText, text7, new Vector2((float)num21, (float)num22), Color.White, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
				}
				for (int num23 = 0; num23 < num19; num23++)
				{
					int num24 = 180;
					int num25 = 456;
					num25 += num23 * 16;
					string text8 = Main.drawTimer[num23] + "ms";
					this.spriteBatch.DrawString(Main.fontMouseText, text8, new Vector2((float)num24, (float)num25), Color.White, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
				}
				for (int num26 = 0; num26 < num19; num26++)
				{
					int num27 = 230;
					int num28 = 456;
					num28 += num26 * 16;
					string text9 = Main.drawTimerMax[num26] + "ms";
					this.spriteBatch.DrawString(Main.fontMouseText, text9, new Vector2((float)num27, (float)num28), Color.White, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
				}
				int num29 = 20;
				int num30 = 456 + 16 * num19 + 16;
				string text10 = "Update:";
				this.spriteBatch.DrawString(Main.fontMouseText, text10, new Vector2((float)num29, (float)num30), Color.White, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
				num29 = 180;
				text10 = Main.upTimer + "ms";
				this.spriteBatch.DrawString(Main.fontMouseText, text10, new Vector2((float)num29, (float)num30), Color.White, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
				num29 = 230;
				text10 = Main.upTimerMax + "ms";
				this.spriteBatch.DrawString(Main.fontMouseText, text10, new Vector2((float)num29, (float)num30), Color.White, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
			}
			if (Main.signBubble)
			{
				int num31 = (int)((float)Main.signX - Main.screenPosition.X);
				int num32 = (int)((float)Main.signY - Main.screenPosition.Y);
				SpriteEffects effects = SpriteEffects.None;
				if ((float)Main.signX > Main.player[Main.myPlayer].position.X + (float)Main.player[Main.myPlayer].width)
				{
					effects = SpriteEffects.FlipHorizontally;
					num31 += -8 - Main.chat2Texture.Width;
				}
				else
				{
					num31 += 8;
				}
				num32 -= 22;
				this.spriteBatch.Draw(Main.chat2Texture, new Vector2((float)num31, (float)num32), new Rectangle?(new Rectangle(0, 0, Main.chat2Texture.Width, Main.chat2Texture.Height)), new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), 0f, default(Vector2), 1f, effects, 0f);
				Main.signBubble = false;
			}
			for (int num33 = 0; num33 < 255; num33++)
			{
				if (Main.player[num33].active && Main.myPlayer != num33 && !Main.player[num33].dead)
				{
					new Rectangle((int)((double)Main.player[num33].position.X + (double)Main.player[num33].width * 0.5 - 16.0), (int)(Main.player[num33].position.Y + (float)Main.player[num33].height - 48f), 32, 48);
					if (Main.player[Main.myPlayer].team > 0 && Main.player[Main.myPlayer].team == Main.player[num33].team)
					{
						new Rectangle((int)Main.screenPosition.X, (int)Main.screenPosition.Y, Main.screenWidth, Main.screenHeight);
						string text11 = Main.player[num33].name;
						if (Main.player[num33].statLife < Main.player[num33].statLifeMax)
						{
							object obj = text11;
							text11 = string.Concat(new object[]
							{
								obj,
								": ",
								Main.player[num33].statLife,
								"/",
								Main.player[num33].statLifeMax
							});
						}
						Vector2 position = Main.fontMouseText.MeasureString(text11);
						float num34 = 0f;
						if (Main.player[num33].chatShowTime > 0)
						{
							num34 = -position.Y;
						}
						float num35 = 0f;
						float num36 = (float)Main.mouseTextColor / 255f;
						Color color = new Color((int)((byte)((float)Main.teamColor[Main.player[num33].team].R * num36)), (int)((byte)((float)Main.teamColor[Main.player[num33].team].G * num36)), (int)((byte)((float)Main.teamColor[Main.player[num33].team].B * num36)), (int)Main.mouseTextColor);
						Vector2 vector = new Vector2((float)(Main.screenWidth / 2) + Main.screenPosition.X, (float)(Main.screenHeight / 2) + Main.screenPosition.Y);
						float num37 = Main.player[num33].position.X + (float)(Main.player[num33].width / 2) - vector.X;
						float num38 = Main.player[num33].position.Y - position.Y - 2f + num34 - vector.Y;
						float num39 = (float)Math.Sqrt((double)(num37 * num37 + num38 * num38));
						int num40 = Main.screenHeight;
						if (Main.screenHeight > Main.screenWidth)
						{
							num40 = Main.screenWidth;
						}
						num40 = num40 / 2 - 30;
						if (num40 < 100)
						{
							num40 = 100;
						}
						if (num39 < (float)num40)
						{
							position.X = Main.player[num33].position.X + (float)(Main.player[num33].width / 2) - position.X / 2f - Main.screenPosition.X;
							position.Y = Main.player[num33].position.Y - position.Y - 2f + num34 - Main.screenPosition.Y;
						}
						else
						{
							num35 = num39;
							num39 = (float)num40 / num39;
							position.X = (float)(Main.screenWidth / 2) + num37 * num39 - position.X / 2f;
							position.Y = (float)(Main.screenHeight / 2) + num38 * num39;
						}
						if (num35 > 0f)
						{
							string text12 = "(" + (int)(num35 / 16f * 2f) + " ft)";
							Vector2 position2 = Main.fontMouseText.MeasureString(text12);
							position2.X = position.X + Main.fontMouseText.MeasureString(text11).X / 2f - position2.X / 2f;
							position2.Y = position.Y + Main.fontMouseText.MeasureString(text11).Y / 2f - position2.Y / 2f - 20f;
							this.spriteBatch.DrawString(Main.fontMouseText, text12, new Vector2(position2.X - 2f, position2.Y), Color.Black, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
							this.spriteBatch.DrawString(Main.fontMouseText, text12, new Vector2(position2.X + 2f, position2.Y), Color.Black, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
							this.spriteBatch.DrawString(Main.fontMouseText, text12, new Vector2(position2.X, position2.Y - 2f), Color.Black, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
							this.spriteBatch.DrawString(Main.fontMouseText, text12, new Vector2(position2.X, position2.Y + 2f), Color.Black, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
							this.spriteBatch.DrawString(Main.fontMouseText, text12, position2, color, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
						}
						this.spriteBatch.DrawString(Main.fontMouseText, text11, new Vector2(position.X - 2f, position.Y), Color.Black, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
						this.spriteBatch.DrawString(Main.fontMouseText, text11, new Vector2(position.X + 2f, position.Y), Color.Black, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
						this.spriteBatch.DrawString(Main.fontMouseText, text11, new Vector2(position.X, position.Y - 2f), Color.Black, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
						this.spriteBatch.DrawString(Main.fontMouseText, text11, new Vector2(position.X, position.Y + 2f), Color.Black, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
						this.spriteBatch.DrawString(Main.fontMouseText, text11, position, color, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
					}
				}
			}
			if (Main.playerInventory)
			{
				Main.npcChatText = "";
				Main.player[Main.myPlayer].sign = -1;
			}
			if (Main.ignoreErrors)
			{
				try
				{
					if (Main.npcChatText != "" || Main.player[Main.myPlayer].sign != -1)
					{
						this.DrawChat();
					}
					goto IL_151C;
				}
				catch
				{
					goto IL_151C;
				}
			}
			if (Main.npcChatText != "" || Main.player[Main.myPlayer].sign != -1)
			{
				this.DrawChat();
			}
			IL_151C:
			Color color2 = new Color(220, 220, 220, 220);
			Main.invAlpha += Main.invDir * 0.2f;
			if (Main.invAlpha > 240f)
			{
				Main.invAlpha = 240f;
				Main.invDir = -1f;
			}
			if (Main.invAlpha < 180f)
			{
				Main.invAlpha = 180f;
				Main.invDir = 1f;
			}
			color2 = new Color((int)((byte)Main.invAlpha), (int)((byte)Main.invAlpha), (int)((byte)Main.invAlpha), (int)((byte)Main.invAlpha));
			bool flag = false;
			int rare = 0;
			int num41 = Main.screenWidth - 800;
			int num42 = Main.player[Main.myPlayer].statLifeMax / 20;
			if (num42 >= 10)
			{
				num42 = 10;
			}
			string text13 = string.Concat(new object[]
			{
				Lang.inter[0],
				" ",
				Main.player[Main.myPlayer].statLifeMax,
				"/",
				Main.player[Main.myPlayer].statLifeMax
			});
			Vector2 vector2 = Main.fontMouseText.MeasureString(text13);
			if (!Main.player[Main.myPlayer].ghost)
			{
				this.spriteBatch.DrawString(Main.fontMouseText, Lang.inter[0], new Vector2((float)(500 + 13 * num42) - vector2.X * 0.5f + (float)num41, 6f), new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
				this.spriteBatch.DrawString(Main.fontMouseText, Main.player[Main.myPlayer].statLife + "/" + Main.player[Main.myPlayer].statLifeMax, new Vector2((float)(500 + 13 * num42) + vector2.X * 0.5f + (float)num41, 6f), new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), 0f, new Vector2(Main.fontMouseText.MeasureString(Main.player[Main.myPlayer].statLife + "/" + Main.player[Main.myPlayer].statLifeMax).X, 0f), 1f, SpriteEffects.None, 0f);
			}
			int num43 = 20;
			for (int num44 = 1; num44 < Main.player[Main.myPlayer].statLifeMax / num43 + 1; num44++)
			{
				float num45 = 1f;
				bool flag2 = false;
				int num46;
				if (Main.player[Main.myPlayer].statLife >= num44 * num43)
				{
					num46 = 255;
					if (Main.player[Main.myPlayer].statLife == num44 * num43)
					{
						flag2 = true;
					}
				}
				else
				{
					float num47 = (float)(Main.player[Main.myPlayer].statLife - (num44 - 1) * num43) / (float)num43;
					num46 = (int)(30f + 225f * num47);
					if (num46 < 30)
					{
						num46 = 30;
					}
					num45 = num47 / 4f + 0.75f;
					if ((double)num45 < 0.75)
					{
						num45 = 0.75f;
					}
					if (num47 > 0f)
					{
						flag2 = true;
					}
				}
				if (flag2)
				{
					num45 += Main.cursorScale - 1f;
				}
				int num48 = 0;
				int num49 = 0;
				if (num44 > 10)
				{
					num48 -= 260;
					num49 += 26;
				}
				int a = (int)((double)((float)num46) * 0.9);
				if (!Main.player[Main.myPlayer].ghost)
				{
					this.spriteBatch.Draw(Main.heartTexture, new Vector2((float)(500 + 26 * (num44 - 1) + num48 + num41 + Main.heartTexture.Width / 2), 32f + ((float)Main.heartTexture.Height - (float)Main.heartTexture.Height * num45) / 2f + (float)num49 + (float)(Main.heartTexture.Height / 2)), new Rectangle?(new Rectangle(0, 0, Main.heartTexture.Width, Main.heartTexture.Height)), new Color(num46, num46, num46, a), 0f, new Vector2((float)(Main.heartTexture.Width / 2), (float)(Main.heartTexture.Height / 2)), num45, SpriteEffects.None, 0f);
				}
			}
			int num50 = 20;
			if (Main.player[Main.myPlayer].statManaMax2 > 0)
			{
				int arg_19F6_0 = Main.player[Main.myPlayer].statManaMax2 / 20;
				this.spriteBatch.DrawString(Main.fontMouseText, Lang.inter[2], new Vector2((float)(750 + num41), 6f), new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
				for (int num51 = 1; num51 < Main.player[Main.myPlayer].statManaMax2 / num50 + 1; num51++)
				{
					bool flag3 = false;
					float num52 = 1f;
					int num53;
					if (Main.player[Main.myPlayer].statMana >= num51 * num50)
					{
						num53 = 255;
						if (Main.player[Main.myPlayer].statMana == num51 * num50)
						{
							flag3 = true;
						}
					}
					else
					{
						float num54 = (float)(Main.player[Main.myPlayer].statMana - (num51 - 1) * num50) / (float)num50;
						num53 = (int)(30f + 225f * num54);
						if (num53 < 30)
						{
							num53 = 30;
						}
						num52 = num54 / 4f + 0.75f;
						if ((double)num52 < 0.75)
						{
							num52 = 0.75f;
						}
						if (num54 > 0f)
						{
							flag3 = true;
						}
					}
					if (flag3)
					{
						num52 += Main.cursorScale - 1f;
					}
					int a2 = (int)((double)((float)num53) * 0.9);
					this.spriteBatch.Draw(Main.manaTexture, new Vector2((float)(775 + num41), (float)(30 + Main.manaTexture.Height / 2) + ((float)Main.manaTexture.Height - (float)Main.manaTexture.Height * num52) / 2f + (float)(28 * (num51 - 1))), new Rectangle?(new Rectangle(0, 0, Main.manaTexture.Width, Main.manaTexture.Height)), new Color(num53, num53, num53, a2), 0f, new Vector2((float)(Main.manaTexture.Width / 2), (float)(Main.manaTexture.Height / 2)), num52, SpriteEffects.None, 0f);
				}
			}
			if (Main.player[Main.myPlayer].breath < Main.player[Main.myPlayer].breathMax && !Main.player[Main.myPlayer].ghost)
			{
				int num55 = 76;
				int arg_1C5F_0 = Main.player[Main.myPlayer].breathMax / 20;
				this.spriteBatch.DrawString(Main.fontMouseText, Lang.inter[1], new Vector2((float)(500 + 13 * num42) - Main.fontMouseText.MeasureString(Lang.inter[1]).X * 0.5f + (float)num41, (float)(6 + num55)), new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
				int num56 = 20;
				for (int num57 = 1; num57 < Main.player[Main.myPlayer].breathMax / num56 + 1; num57++)
				{
					float num58 = 1f;
					int num59;
					if (Main.player[Main.myPlayer].breath >= num57 * num56)
					{
						num59 = 255;
					}
					else
					{
						float num60 = (float)(Main.player[Main.myPlayer].breath - (num57 - 1) * num56) / (float)num56;
						num59 = (int)(30f + 225f * num60);
						if (num59 < 30)
						{
							num59 = 30;
						}
						num58 = num60 / 4f + 0.75f;
						if ((double)num58 < 0.75)
						{
							num58 = 0.75f;
						}
					}
					int num61 = 0;
					int num62 = 0;
					if (num57 > 10)
					{
						num61 -= 260;
						num62 += 26;
					}
					this.spriteBatch.Draw(Main.bubbleTexture, new Vector2((float)(500 + 26 * (num57 - 1) + num61 + num41), 32f + ((float)Main.bubbleTexture.Height - (float)Main.bubbleTexture.Height * num58) / 2f + (float)num62 + (float)num55), new Rectangle?(new Rectangle(0, 0, Main.bubbleTexture.Width, Main.bubbleTexture.Height)), new Color(num59, num59, num59, num59), 0f, default(Vector2), num58, SpriteEffects.None, 0f);
				}
			}
			Main.buffString = "";
			if (!Main.playerInventory)
			{
				int num63 = -1;
				for (int num64 = 0; num64 < 10; num64++)
				{
					if (Main.player[Main.myPlayer].buffType[num64] > 0)
					{
						int num65 = Main.player[Main.myPlayer].buffType[num64];
						int num66 = 32 + num64 * 38;
						int num67 = 76;
						Color color3 = new Color(Main.buffAlpha[num64], Main.buffAlpha[num64], Main.buffAlpha[num64], Main.buffAlpha[num64]);
						this.spriteBatch.Draw(Main.buffTexture[num65], new Vector2((float)num66, (float)num67), new Rectangle?(new Rectangle(0, 0, Main.buffTexture[num65].Width, Main.buffTexture[num65].Height)), color3, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
						if (num65 != 28 && num65 != 34 && num65 != 37 && num65 != 38 && num65 != 40)
						{
							string text14;
							if (Main.player[Main.myPlayer].buffTime[num64] / 60 >= 60)
							{
								text14 = Math.Round((double)(Main.player[Main.myPlayer].buffTime[num64] / 60) / 60.0) + " m";
							}
							else
							{
								text14 = Math.Round((double)Main.player[Main.myPlayer].buffTime[num64] / 60.0) + " s";
							}
							this.spriteBatch.DrawString(Main.fontItemStack, text14, new Vector2((float)num66, (float)(num67 + Main.buffTexture[num65].Height)), color3, 0f, default(Vector2), 0.8f, SpriteEffects.None, 0f);
						}
						if (Main.mouseX < num66 + Main.buffTexture[num65].Width && Main.mouseY < num67 + Main.buffTexture[num65].Height && Main.mouseX > num66 && Main.mouseY > num67)
						{
							num63 = num64;
							Main.buffAlpha[num64] += 0.1f;
							if (Main.mouseRight && Main.mouseRightRelease && !Main.debuff[num65])
							{
								Main.PlaySound(12, -1, -1, 1);
								Main.player[Main.myPlayer].DelBuff(num64);
							}
						}
						else
						{
							Main.buffAlpha[num64] -= 0.05f;
						}
						if (Main.buffAlpha[num64] > 1f)
						{
							Main.buffAlpha[num64] = 1f;
						}
						else
						{
							if ((double)Main.buffAlpha[num64] < 0.4)
							{
								Main.buffAlpha[num64] = 0.4f;
							}
						}
					}
					else
					{
						Main.buffAlpha[num64] = 0.4f;
					}
				}
				if (num63 >= 0)
				{
					int num68 = Main.player[Main.myPlayer].buffType[num63];
					if (num68 > 0)
					{
						Main.buffString = Main.buffTip[num68];
						this.MouseText(Main.buffName[num68], 0, 0);
					}
				}
			}
			if (Main.player[Main.myPlayer].dead)
			{
				Main.playerInventory = false;
			}
			if (!Main.playerInventory)
			{
				Main.player[Main.myPlayer].chest = -1;
				if (Main.craftGuide)
				{
					Main.craftGuide = false;
					Recipe.FindRecipes();
				}
				Main.reforge = false;
			}
			string text15 = "";
			if (Main.playerInventory)
			{
				if (Main.netMode == 1)
				{
					int num69 = 675 + Main.screenWidth - 800;
					int num70 = 114;
					if (Main.player[Main.myPlayer].hostile)
					{
						this.spriteBatch.Draw(Main.itemTexture[4], new Vector2((float)(num69 - 2), (float)num70), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[4].Width, Main.itemTexture[4].Height)), Main.teamColor[Main.player[Main.myPlayer].team], 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
						this.spriteBatch.Draw(Main.itemTexture[4], new Vector2((float)(num69 + 2), (float)num70), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[4].Width, Main.itemTexture[4].Height)), Main.teamColor[Main.player[Main.myPlayer].team], 0f, default(Vector2), 1f, SpriteEffects.FlipHorizontally, 0f);
					}
					else
					{
						this.spriteBatch.Draw(Main.itemTexture[4], new Vector2((float)(num69 - 16), (float)(num70 + 14)), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[4].Width, Main.itemTexture[4].Height)), Main.teamColor[Main.player[Main.myPlayer].team], -0.785f, default(Vector2), 1f, SpriteEffects.None, 0f);
						this.spriteBatch.Draw(Main.itemTexture[4], new Vector2((float)(num69 + 2), (float)(num70 + 14)), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[4].Width, Main.itemTexture[4].Height)), Main.teamColor[Main.player[Main.myPlayer].team], -0.785f, default(Vector2), 1f, SpriteEffects.None, 0f);
					}
					if (Main.mouseX > num69 && Main.mouseX < num69 + 34 && Main.mouseY > num70 - 2 && Main.mouseY < num70 + 34)
					{
						Main.player[Main.myPlayer].mouseInterface = true;
						if (Main.mouseLeft && Main.mouseLeftRelease && Main.teamCooldown == 0)
						{
							Main.teamCooldown = Main.teamCooldownLen;
							Main.PlaySound(12, -1, -1, 1);
							if (Main.player[Main.myPlayer].hostile)
							{
								Main.player[Main.myPlayer].hostile = false;
							}
							else
							{
								Main.player[Main.myPlayer].hostile = true;
							}
							NetMessage.SendData(30, -1, -1, "", Main.myPlayer, 0f, 0f, 0f, 0);
						}
					}
					num69 -= 3;
					Rectangle value = new Rectangle(Main.mouseX, Main.mouseY, 1, 1);
					int width = Main.teamTexture.Width;
					int height = Main.teamTexture.Height;
					for (int num71 = 0; num71 < 5; num71++)
					{
						Rectangle rectangle = default(Rectangle);
						if (num71 == 0)
						{
							rectangle = new Rectangle(num69 + 50, num70 - 20, width, height);
						}
						if (num71 == 1)
						{
							rectangle = new Rectangle(num69 + 40, num70, width, height);
						}
						if (num71 == 2)
						{
							rectangle = new Rectangle(num69 + 60, num70, width, height);
						}
						if (num71 == 3)
						{
							rectangle = new Rectangle(num69 + 40, num70 + 20, width, height);
						}
						if (num71 == 4)
						{
							rectangle = new Rectangle(num69 + 60, num70 + 20, width, height);
						}
						if (rectangle.Intersects(value))
						{
							Main.player[Main.myPlayer].mouseInterface = true;
							if (Main.mouseLeft && Main.mouseLeftRelease && Main.player[Main.myPlayer].team != num71 && Main.teamCooldown == 0)
							{
								Main.teamCooldown = Main.teamCooldownLen;
								Main.PlaySound(12, -1, -1, 1);
								Main.player[Main.myPlayer].team = num71;
								NetMessage.SendData(45, -1, -1, "", Main.myPlayer, 0f, 0f, 0f, 0);
							}
						}
					}
					this.spriteBatch.Draw(Main.teamTexture, new Vector2((float)(num69 + 50), (float)(num70 - 20)), new Rectangle?(new Rectangle(0, 0, Main.teamTexture.Width, Main.teamTexture.Height)), Main.teamColor[0], 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
					this.spriteBatch.Draw(Main.teamTexture, new Vector2((float)(num69 + 40), (float)num70), new Rectangle?(new Rectangle(0, 0, Main.teamTexture.Width, Main.teamTexture.Height)), Main.teamColor[1], 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
					this.spriteBatch.Draw(Main.teamTexture, new Vector2((float)(num69 + 60), (float)num70), new Rectangle?(new Rectangle(0, 0, Main.teamTexture.Width, Main.teamTexture.Height)), Main.teamColor[2], 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
					this.spriteBatch.Draw(Main.teamTexture, new Vector2((float)(num69 + 40), (float)(num70 + 20)), new Rectangle?(new Rectangle(0, 0, Main.teamTexture.Width, Main.teamTexture.Height)), Main.teamColor[3], 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
					this.spriteBatch.Draw(Main.teamTexture, new Vector2((float)(num69 + 60), (float)(num70 + 20)), new Rectangle?(new Rectangle(0, 0, Main.teamTexture.Width, Main.teamTexture.Height)), Main.teamColor[4], 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
				}
				bool flag4 = false;
				Main.inventoryScale = 0.85f;
				int num72 = 448;
				int num73 = 210;
				Color white = new Color(150, 150, 150, 150);
				if (Main.mouseX >= num72 && (float)Main.mouseX <= (float)num72 + (float)Main.inventoryBackTexture.Width * Main.inventoryScale && Main.mouseY >= num73 && (float)Main.mouseY <= (float)num73 + (float)Main.inventoryBackTexture.Height * Main.inventoryScale)
				{
					Main.player[Main.myPlayer].mouseInterface = true;
					if (Main.mouseLeftRelease && Main.mouseLeft)
					{
						if (Main.mouseItem.type != 0)
						{
							Main.trashItem.SetDefaults(0, false);
						}
						Item item = Main.mouseItem;
						Main.mouseItem = Main.trashItem;
						Main.trashItem = item;
						if (Main.trashItem.type == 0 || Main.trashItem.stack < 1)
						{
							Main.trashItem = new Item();
						}
						if (Main.mouseItem.IsTheSameAs(Main.trashItem) && Main.trashItem.stack != Main.trashItem.maxStack && Main.mouseItem.stack != Main.mouseItem.maxStack)
						{
							if (Main.mouseItem.stack + Main.trashItem.stack <= Main.mouseItem.maxStack)
							{
								Main.trashItem.stack += Main.mouseItem.stack;
								Main.mouseItem.stack = 0;
							}
							else
							{
								int num74 = Main.mouseItem.maxStack - Main.trashItem.stack;
								Main.trashItem.stack += num74;
								Main.mouseItem.stack -= num74;
							}
						}
						if (Main.mouseItem.type == 0 || Main.mouseItem.stack < 1)
						{
							Main.mouseItem = new Item();
						}
						if (Main.mouseItem.type > 0 || Main.trashItem.type > 0)
						{
							Main.PlaySound(7, -1, -1, 1);
						}
					}
					if (!flag4)
					{
						text15 = Main.trashItem.name;
						if (Main.trashItem.stack > 1)
						{
							object obj = text15;
							text15 = string.Concat(new object[]
							{
								obj,
								" (",
								Main.trashItem.stack,
								")"
							});
						}
						Main.toolTip = (Item)Main.trashItem.Clone();
						if (text15 == null)
						{
							text15 = Lang.inter[3];
						}
					}
					else
					{
						text15 = Lang.inter[3];
					}
				}
				this.spriteBatch.Draw(Main.inventoryBack7Texture, new Vector2((float)num72, (float)num73), new Rectangle?(new Rectangle(0, 0, Main.inventoryBackTexture.Width, Main.inventoryBackTexture.Height)), color2, 0f, default(Vector2), Main.inventoryScale, SpriteEffects.None, 0f);
				white = Color.White;
				if (Main.trashItem.type == 0 || Main.trashItem.stack == 0 || flag4)
				{
					white = new Color(100, 100, 100, 100);
					float num75 = Main.inventoryScale;
					this.spriteBatch.Draw(Main.trashTexture, new Vector2((float)num72 + 26f * Main.inventoryScale - (float)Main.trashTexture.Width * 0.5f * num75, (float)num73 + 26f * Main.inventoryScale - (float)Main.trashTexture.Height * 0.5f * num75), new Rectangle?(new Rectangle(0, 0, Main.trashTexture.Width, Main.trashTexture.Height)), white, 0f, default(Vector2), num75, SpriteEffects.None, 0f);
				}
				else
				{
					float num76 = 1f;
					if (Main.itemTexture[Main.trashItem.type].Width > 32 || Main.itemTexture[Main.trashItem.type].Height > 32)
					{
						if (Main.itemTexture[Main.trashItem.type].Width > Main.itemTexture[Main.trashItem.type].Height)
						{
							num76 = 32f / (float)Main.itemTexture[Main.trashItem.type].Width;
						}
						else
						{
							num76 = 32f / (float)Main.itemTexture[Main.trashItem.type].Height;
						}
					}
					num76 *= Main.inventoryScale;
					this.spriteBatch.Draw(Main.itemTexture[Main.trashItem.type], new Vector2((float)num72 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.trashItem.type].Width * 0.5f * num76, (float)num73 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.trashItem.type].Height * 0.5f * num76), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[Main.trashItem.type].Width, Main.itemTexture[Main.trashItem.type].Height)), Main.trashItem.GetAlpha(white), 0f, default(Vector2), num76, SpriteEffects.None, 0f);
					if (Main.trashItem.color != default(Color))
					{
						this.spriteBatch.Draw(Main.itemTexture[Main.trashItem.type], new Vector2((float)num72 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.trashItem.type].Width * 0.5f * num76, (float)num73 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.trashItem.type].Height * 0.5f * num76), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[Main.trashItem.type].Width, Main.itemTexture[Main.trashItem.type].Height)), Main.trashItem.GetColor(white), 0f, default(Vector2), num76, SpriteEffects.None, 0f);
					}
					if (Main.trashItem.stack > 1)
					{
						this.spriteBatch.DrawString(Main.fontItemStack, string.Concat(Main.trashItem.stack), new Vector2((float)num72 + 10f * Main.inventoryScale, (float)num73 + 26f * Main.inventoryScale), white, 0f, default(Vector2), num76, SpriteEffects.None, 0f);
					}
				}
				this.spriteBatch.DrawString(Main.fontMouseText, Lang.inter[4], new Vector2(40f, 0f), new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
				Main.inventoryScale = 0.85f;
				if (Main.mouseX > 20 && Main.mouseX < (int)(20f + 560f * Main.inventoryScale) && Main.mouseY > 20 && Main.mouseY < (int)(20f + 224f * Main.inventoryScale))
				{
					Main.player[Main.myPlayer].mouseInterface = true;
				}
				for (int num77 = 0; num77 < 10; num77++)
				{
					for (int num78 = 0; num78 < 4; num78++)
					{
						int num79 = (int)(20f + (float)(num77 * 56) * Main.inventoryScale);
						int num80 = (int)(20f + (float)(num78 * 56) * Main.inventoryScale);
						int num81 = num77 + num78 * 10;
						Color white2 = new Color(100, 100, 100, 100);
						if (Main.mouseX >= num79 && (float)Main.mouseX <= (float)num79 + (float)Main.inventoryBackTexture.Width * Main.inventoryScale && Main.mouseY >= num80 && (float)Main.mouseY <= (float)num80 + (float)Main.inventoryBackTexture.Height * Main.inventoryScale)
						{
							Main.player[Main.myPlayer].mouseInterface = true;
							if (Main.mouseLeftRelease && Main.mouseLeft)
							{
								if (Main.keyState.IsKeyDown(Keys.LeftShift))
								{
									if (Main.player[Main.myPlayer].inventory[num81].type > 0)
									{
										if (Main.npcShop > 0)
										{
											if (Main.player[Main.myPlayer].SellItem(Main.player[Main.myPlayer].inventory[num81].value, Main.player[Main.myPlayer].inventory[num81].stack))
											{
												this.shop[Main.npcShop].AddShop(Main.player[Main.myPlayer].inventory[num81]);
												Main.player[Main.myPlayer].inventory[num81].SetDefaults(0, false);
												Main.PlaySound(18, -1, -1, 1);
											}
											else
											{
												if (Main.player[Main.myPlayer].inventory[num81].value == 0)
												{
													this.shop[Main.npcShop].AddShop(Main.player[Main.myPlayer].inventory[num81]);
													Main.player[Main.myPlayer].inventory[num81].SetDefaults(0, false);
													Main.PlaySound(7, -1, -1, 1);
												}
											}
										}
										else
										{
											Recipe.FindRecipes();
											Main.PlaySound(7, -1, -1, 1);
											Main.trashItem = (Item)Main.player[Main.myPlayer].inventory[num81].Clone();
											Main.player[Main.myPlayer].inventory[num81].SetDefaults(0, false);
										}
									}
								}
								else
								{
									if (Main.player[Main.myPlayer].selectedItem != num81 || Main.player[Main.myPlayer].itemAnimation <= 0)
									{
										Item item2 = Main.mouseItem;
										Main.mouseItem = Main.player[Main.myPlayer].inventory[num81];
										Main.player[Main.myPlayer].inventory[num81] = item2;
										if (Main.player[Main.myPlayer].inventory[num81].type == 0 || Main.player[Main.myPlayer].inventory[num81].stack < 1)
										{
											Main.player[Main.myPlayer].inventory[num81] = new Item();
										}
										if (Main.mouseItem.IsTheSameAs(Main.player[Main.myPlayer].inventory[num81]) && Main.player[Main.myPlayer].inventory[num81].stack != Main.player[Main.myPlayer].inventory[num81].maxStack && Main.mouseItem.stack != Main.mouseItem.maxStack)
										{
											if (Main.mouseItem.stack + Main.player[Main.myPlayer].inventory[num81].stack <= Main.mouseItem.maxStack)
											{
												Main.player[Main.myPlayer].inventory[num81].stack += Main.mouseItem.stack;
												Main.mouseItem.stack = 0;
											}
											else
											{
												int num82 = Main.mouseItem.maxStack - Main.player[Main.myPlayer].inventory[num81].stack;
												Main.player[Main.myPlayer].inventory[num81].stack += num82;
												Main.mouseItem.stack -= num82;
											}
										}
										if (Main.mouseItem.type == 0 || Main.mouseItem.stack < 1)
										{
											Main.mouseItem = new Item();
										}
										if (Main.mouseItem.type > 0 || Main.player[Main.myPlayer].inventory[num81].type > 0)
										{
											Recipe.FindRecipes();
											Main.PlaySound(7, -1, -1, 1);
										}
									}
								}
							}
							else
							{
								if (Main.mouseRight && Main.mouseRightRelease && (Main.player[Main.myPlayer].inventory[num81].type == 599 || Main.player[Main.myPlayer].inventory[num81].type == 600 || Main.player[Main.myPlayer].inventory[num81].type == 601))
								{
									Main.PlaySound(7, -1, -1, 1);
									Main.stackSplit = 30;
									Main.mouseRightRelease = false;
									int num83 = Main.rand.Next(14);
									if (num83 == 0 && Main.hardMode)
									{
										Main.player[Main.myPlayer].inventory[num81].SetDefaults(602, false);
									}
									else
									{
										if (num83 <= 7)
										{
											Main.player[Main.myPlayer].inventory[num81].SetDefaults(586, false);
											Main.player[Main.myPlayer].inventory[num81].stack = Main.rand.Next(20, 50);
										}
										else
										{
											Main.player[Main.myPlayer].inventory[num81].SetDefaults(591, false);
											Main.player[Main.myPlayer].inventory[num81].stack = Main.rand.Next(20, 50);
										}
									}
								}
								else
								{
									if (Main.mouseRight && Main.mouseRightRelease && Main.player[Main.myPlayer].inventory[num81].maxStack == 1)
									{
										Main.player[Main.myPlayer].inventory[num81] = Main.armorSwap(Main.player[Main.myPlayer].inventory[num81]);
									}
									else
									{
										if (Main.stackSplit <= 1 && Main.mouseRight && Main.player[Main.myPlayer].inventory[num81].maxStack > 1 && Main.player[Main.myPlayer].inventory[num81].type > 0 && (Main.mouseItem.IsTheSameAs(Main.player[Main.myPlayer].inventory[num81]) || Main.mouseItem.type == 0) && (Main.mouseItem.stack < Main.mouseItem.maxStack || Main.mouseItem.type == 0))
										{
											if (Main.mouseItem.type == 0)
											{
												Main.mouseItem = (Item)Main.player[Main.myPlayer].inventory[num81].Clone();
												Main.mouseItem.stack = 0;
											}
											Main.mouseItem.stack++;
											Main.player[Main.myPlayer].inventory[num81].stack--;
											if (Main.player[Main.myPlayer].inventory[num81].stack <= 0)
											{
												Main.player[Main.myPlayer].inventory[num81] = new Item();
											}
											Recipe.FindRecipes();
											Main.soundInstanceMenuTick.Stop();
											Main.soundInstanceMenuTick = Main.soundMenuTick.CreateInstance();
											Main.PlaySound(12, -1, -1, 1);
											if (Main.stackSplit == 0)
											{
												Main.stackSplit = 15;
											}
											else
											{
												Main.stackSplit = Main.stackDelay;
											}
										}
									}
								}
							}
							text15 = Main.player[Main.myPlayer].inventory[num81].name;
							Main.toolTip = (Item)Main.player[Main.myPlayer].inventory[num81].Clone();
							if (Main.player[Main.myPlayer].inventory[num81].stack > 1)
							{
								object obj = text15;
								text15 = string.Concat(new object[]
								{
									obj,
									" (",
									Main.player[Main.myPlayer].inventory[num81].stack,
									")"
								});
							}
						}
						if (num78 != 0)
						{
							this.spriteBatch.Draw(Main.inventoryBackTexture, new Vector2((float)num79, (float)num80), new Rectangle?(new Rectangle(0, 0, Main.inventoryBackTexture.Width, Main.inventoryBackTexture.Height)), color2, 0f, default(Vector2), Main.inventoryScale, SpriteEffects.None, 0f);
						}
						else
						{
							this.spriteBatch.Draw(Main.inventoryBack9Texture, new Vector2((float)num79, (float)num80), new Rectangle?(new Rectangle(0, 0, Main.inventoryBackTexture.Width, Main.inventoryBackTexture.Height)), color2, 0f, default(Vector2), Main.inventoryScale, SpriteEffects.None, 0f);
						}
						white2 = Color.White;
						if (Main.player[Main.myPlayer].inventory[num81].type > 0 && Main.player[Main.myPlayer].inventory[num81].stack > 0)
						{
							float num84 = 1f;
							if (Main.itemTexture[Main.player[Main.myPlayer].inventory[num81].type].Width > 32 || Main.itemTexture[Main.player[Main.myPlayer].inventory[num81].type].Height > 32)
							{
								if (Main.itemTexture[Main.player[Main.myPlayer].inventory[num81].type].Width > Main.itemTexture[Main.player[Main.myPlayer].inventory[num81].type].Height)
								{
									num84 = 32f / (float)Main.itemTexture[Main.player[Main.myPlayer].inventory[num81].type].Width;
								}
								else
								{
									num84 = 32f / (float)Main.itemTexture[Main.player[Main.myPlayer].inventory[num81].type].Height;
								}
							}
							num84 *= Main.inventoryScale;
							this.spriteBatch.Draw(Main.itemTexture[Main.player[Main.myPlayer].inventory[num81].type], new Vector2((float)num79 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.player[Main.myPlayer].inventory[num81].type].Width * 0.5f * num84, (float)num80 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.player[Main.myPlayer].inventory[num81].type].Height * 0.5f * num84), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[Main.player[Main.myPlayer].inventory[num81].type].Width, Main.itemTexture[Main.player[Main.myPlayer].inventory[num81].type].Height)), Main.player[Main.myPlayer].inventory[num81].GetAlpha(white2), 0f, default(Vector2), num84, SpriteEffects.None, 0f);
							if (Main.player[Main.myPlayer].inventory[num81].color != default(Color))
							{
								this.spriteBatch.Draw(Main.itemTexture[Main.player[Main.myPlayer].inventory[num81].type], new Vector2((float)num79 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.player[Main.myPlayer].inventory[num81].type].Width * 0.5f * num84, (float)num80 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.player[Main.myPlayer].inventory[num81].type].Height * 0.5f * num84), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[Main.player[Main.myPlayer].inventory[num81].type].Width, Main.itemTexture[Main.player[Main.myPlayer].inventory[num81].type].Height)), Main.player[Main.myPlayer].inventory[num81].GetColor(white2), 0f, default(Vector2), num84, SpriteEffects.None, 0f);
							}
							if (Main.player[Main.myPlayer].inventory[num81].stack > 1)
							{
								this.spriteBatch.DrawString(Main.fontItemStack, string.Concat(Main.player[Main.myPlayer].inventory[num81].stack), new Vector2((float)num79 + 10f * Main.inventoryScale, (float)num80 + 26f * Main.inventoryScale), white2, 0f, default(Vector2), num84, SpriteEffects.None, 0f);
							}
						}
						if (num78 == 0)
						{
							string text16 = string.Concat(num81 + 1);
							if (text16 == "10")
							{
								text16 = "0";
							}
							Color color4 = color2;
							if (Main.player[Main.myPlayer].selectedItem == num81)
							{
								color4.R = 0;
								color4.B = 0;
								color4.G = 255;
								color4.A = 50;
							}
							this.spriteBatch.DrawString(Main.fontItemStack, text16, new Vector2((float)(num79 + 6), (float)(num80 + 4)), color4, 0f, default(Vector2), Main.inventoryScale * 0.8f, SpriteEffects.None, 0f);
						}
					}
				}
				int num85 = 0;
				int num86 = 2;
				int num87 = 32;
				if (!Main.player[Main.myPlayer].hbLocked)
				{
					num85 = 1;
				}
				this.spriteBatch.Draw(Main.HBLockTexture[num85], new Vector2((float)num86, (float)num87), new Rectangle?(new Rectangle(0, 0, Main.HBLockTexture[num85].Width, Main.HBLockTexture[num85].Height)), color2, 0f, default(Vector2), 0.9f, SpriteEffects.None, 0f);
				if (Main.mouseX > num86 && (float)Main.mouseX < (float)num86 + (float)Main.HBLockTexture[num85].Width * 0.9f && Main.mouseY > num87 && (float)Main.mouseY < (float)num87 + (float)Main.HBLockTexture[num85].Height * 0.9f)
				{
					Main.player[Main.myPlayer].mouseInterface = true;
					if (!Main.player[Main.myPlayer].hbLocked)
					{
						this.MouseText(Lang.inter[5], 0, 0);
						flag = true;
					}
					else
					{
						this.MouseText(Lang.inter[6], 0, 0);
						flag = true;
					}
					if (Main.mouseLeft && Main.mouseLeftRelease)
					{
						Main.PlaySound(22, -1, -1, 1);
						if (!Main.player[Main.myPlayer].hbLocked)
						{
							Main.player[Main.myPlayer].hbLocked = true;
						}
						else
						{
							Main.player[Main.myPlayer].hbLocked = false;
						}
					}
				}
				if (Main.armorHide)
				{
					Main.armorAlpha -= 0.1f;
					if (Main.armorAlpha < 0f)
					{
						Main.armorAlpha = 0f;
					}
				}
				else
				{
					Main.armorAlpha += 0.025f;
					if (Main.armorAlpha > 1f)
					{
						Main.armorAlpha = 1f;
					}
				}
				Color color5 = new Color((int)((byte)((float)Main.mouseTextColor * Main.armorAlpha)), (int)((byte)((float)Main.mouseTextColor * Main.armorAlpha)), (int)((byte)((float)Main.mouseTextColor * Main.armorAlpha)), (int)((byte)((float)Main.mouseTextColor * Main.armorAlpha)));
				Main.armorHide = false;
				int num88 = 1;
				int num89 = Main.screenWidth - 152;
				int num90 = 128;
				if (Main.netMode == 0)
				{
					num89 += 72;
				}
				if (this.showNPCs)
				{
					num88 = 0;
				}
				this.spriteBatch.Draw(Main.npcToggleTexture[num88], new Vector2((float)num89, (float)num90), new Rectangle?(new Rectangle(0, 0, Main.npcToggleTexture[num88].Width, Main.npcToggleTexture[num88].Height)), Color.White, 0f, default(Vector2), 0.9f, SpriteEffects.None, 0f);
				if (Main.mouseX > num89 && (float)Main.mouseX < (float)num89 + (float)Main.npcToggleTexture[num88].Width * 0.9f && Main.mouseY > num90 && (float)Main.mouseY < (float)num90 + (float)Main.npcToggleTexture[num88].Height * 0.9f)
				{
					Main.player[Main.myPlayer].mouseInterface = true;
					if (Main.mouseLeft && Main.mouseLeftRelease)
					{
						Main.PlaySound(12, -1, -1, 1);
						if (!this.showNPCs)
						{
							this.showNPCs = true;
						}
						else
						{
							this.showNPCs = false;
						}
					}
				}
				if (this.showNPCs)
				{
					this.spriteBatch.DrawString(Main.fontMouseText, Lang.inter[7], new Vector2((float)(Main.screenWidth - 64 - 28 - 3), 152f), new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), 0f, default(Vector2), 0.8f, SpriteEffects.None, 0f);
					if (Main.mouseX > Main.screenWidth - 64 - 28 && Main.mouseX < (int)((float)(Main.screenWidth - 64 - 28) + 56f * Main.inventoryScale) && Main.mouseY > 174 && Main.mouseY < (int)(174f + 448f * Main.inventoryScale))
					{
						Main.player[Main.myPlayer].mouseInterface = true;
					}
					int num91 = 0;
					string text17 = "";
					for (int num92 = 0; num92 < 12; num92++)
					{
						bool flag5 = false;
						int num93 = 0;
						if (num92 == 0)
						{
							flag5 = true;
						}
						else
						{
							for (int num94 = 0; num94 < 200; num94++)
							{
								if (Main.npc[num94].active && NPC.TypeToNum(Main.npc[num94].type) == num92)
								{
									flag5 = true;
									num93 = num94;
									break;
								}
							}
						}
						if (flag5)
						{
							int num95 = Main.screenWidth - 64 - 28;
							int num96 = (int)(174f + (float)(num91 * 56) * Main.inventoryScale);
							Color white3 = new Color(100, 100, 100, 100);
							if (Main.screenHeight < 768 && num91 > 5)
							{
								num96 -= (int)(280f * Main.inventoryScale);
								num95 -= 48;
							}
							if (Main.mouseX >= num95 && (float)Main.mouseX <= (float)num95 + (float)Main.inventoryBackTexture.Width * Main.inventoryScale && Main.mouseY >= num96 && (float)Main.mouseY <= (float)num96 + (float)Main.inventoryBackTexture.Height * Main.inventoryScale)
							{
								flag = true;
								if (num92 == 0)
								{
									text17 = Lang.inter[8];
								}
								else
								{
									if (num92 == 11)
									{
										text17 = Main.npc[num93].displayName;
									}
									else
									{
										text17 = Main.npc[num93].displayName + " the " + Main.npc[num93].name;
									}
								}
								Main.player[Main.myPlayer].mouseInterface = true;
								if (Main.mouseLeftRelease && Main.mouseLeft && Main.mouseItem.type == 0)
								{
									Main.PlaySound(12, -1, -1, 1);
									this.mouseNPC = num92;
									Main.mouseLeftRelease = false;
								}
							}
							this.spriteBatch.Draw(Main.inventoryBack11Texture, new Vector2((float)num95, (float)num96), new Rectangle?(new Rectangle(0, 0, Main.inventoryBackTexture.Width, Main.inventoryBackTexture.Height)), color2, 0f, default(Vector2), Main.inventoryScale, SpriteEffects.None, 0f);
							white3 = Color.White;
							int num97 = num92;
							float scale = 1f;
							float num98;
							if (Main.npcHeadTexture[num97].Width > Main.npcHeadTexture[num97].Height)
							{
								num98 = (float)Main.npcHeadTexture[num97].Width;
							}
							else
							{
								num98 = (float)Main.npcHeadTexture[num97].Height;
							}
							if (num98 > 36f)
							{
								scale = 36f / num98;
							}
							this.spriteBatch.Draw(Main.npcHeadTexture[num97], new Vector2((float)num95 + 26f * Main.inventoryScale, (float)num96 + 26f * Main.inventoryScale), new Rectangle?(new Rectangle(0, 0, Main.npcHeadTexture[num97].Width, Main.npcHeadTexture[num97].Height)), white3, 0f, new Vector2((float)(Main.npcHeadTexture[num97].Width / 2), (float)(Main.npcHeadTexture[num97].Height / 2)), scale, SpriteEffects.None, 0f);
							num91++;
						}
					}
					if (text17 != "" && Main.mouseItem.type == 0)
					{
						this.MouseText(text17, 0, 0);
					}
				}
				else
				{
					Vector2 vector3 = Main.fontMouseText.MeasureString("Equip");
					Vector2 vector4 = Main.fontMouseText.MeasureString(Lang.inter[45]);
					float num99 = vector3.X / vector4.X;
					this.spriteBatch.DrawString(Main.fontMouseText, Lang.inter[45], new Vector2((float)(Main.screenWidth - 64 - 28 + 4), 152f + (vector3.Y - vector3.Y * num99) / 2f), new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), 0f, default(Vector2), 0.8f * num99, SpriteEffects.None, 0f);
					if (Main.mouseX > Main.screenWidth - 64 - 28 && Main.mouseX < (int)((float)(Main.screenWidth - 64 - 28) + 56f * Main.inventoryScale) && Main.mouseY > 174 && Main.mouseY < (int)(174f + 448f * Main.inventoryScale))
					{
						Main.player[Main.myPlayer].mouseInterface = true;
					}
					for (int num100 = 0; num100 < 8; num100++)
					{
						int num101 = Main.screenWidth - 64 - 28;
						int num102 = (int)(174f + (float)(num100 * 56) * Main.inventoryScale);
						Color white4 = new Color(100, 100, 100, 100);
						string text18 = "";
						if (num100 == 3)
						{
							text18 = Lang.inter[9];
						}
						if (num100 == 7)
						{
							text18 = Main.player[Main.myPlayer].statDefense + " " + Lang.inter[10];
						}
						Vector2 vector5 = Main.fontMouseText.MeasureString(text18);
						this.spriteBatch.DrawString(Main.fontMouseText, text18, new Vector2((float)num101 - vector5.X - 10f, (float)num102 + (float)Main.inventoryBackTexture.Height * 0.5f - vector5.Y * 0.5f), color5, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
						if (Main.mouseX >= num101 && (float)Main.mouseX <= (float)num101 + (float)Main.inventoryBackTexture.Width * Main.inventoryScale && Main.mouseY >= num102 && (float)Main.mouseY <= (float)num102 + (float)Main.inventoryBackTexture.Height * Main.inventoryScale)
						{
							Main.armorHide = true;
							Main.player[Main.myPlayer].mouseInterface = true;
							if (Main.mouseLeftRelease && Main.mouseLeft && (Main.mouseItem.type == 0 || (Main.mouseItem.headSlot > -1 && num100 == 0) || (Main.mouseItem.bodySlot > -1 && num100 == 1) || (Main.mouseItem.legSlot > -1 && num100 == 2) || (Main.mouseItem.accessory && num100 > 2 && !Main.AccCheck(Main.mouseItem, num100))))
							{
								Item item3 = Main.mouseItem;
								Main.mouseItem = Main.player[Main.myPlayer].armor[num100];
								Main.player[Main.myPlayer].armor[num100] = item3;
								if (Main.player[Main.myPlayer].armor[num100].type == 0 || Main.player[Main.myPlayer].armor[num100].stack < 1)
								{
									Main.player[Main.myPlayer].armor[num100] = new Item();
								}
								if (Main.mouseItem.type == 0 || Main.mouseItem.stack < 1)
								{
									Main.mouseItem = new Item();
								}
								if (Main.mouseItem.type > 0 || Main.player[Main.myPlayer].armor[num100].type > 0)
								{
									Recipe.FindRecipes();
									Main.PlaySound(7, -1, -1, 1);
								}
							}
							text15 = Main.player[Main.myPlayer].armor[num100].name;
							Main.toolTip = (Item)Main.player[Main.myPlayer].armor[num100].Clone();
							if (num100 <= 2)
							{
								Main.toolTip.wornArmor = true;
							}
							if (Main.player[Main.myPlayer].armor[num100].stack > 1)
							{
								object obj = text15;
								text15 = string.Concat(new object[]
								{
									obj,
									" (",
									Main.player[Main.myPlayer].armor[num100].stack,
									")"
								});
							}
						}
						this.spriteBatch.Draw(Main.inventoryBack3Texture, new Vector2((float)num101, (float)num102), new Rectangle?(new Rectangle(0, 0, Main.inventoryBackTexture.Width, Main.inventoryBackTexture.Height)), color2, 0f, default(Vector2), Main.inventoryScale, SpriteEffects.None, 0f);
						white4 = Color.White;
						if (Main.player[Main.myPlayer].armor[num100].type > 0 && Main.player[Main.myPlayer].armor[num100].stack > 0)
						{
							float num103 = 1f;
							if (Main.itemTexture[Main.player[Main.myPlayer].armor[num100].type].Width > 32 || Main.itemTexture[Main.player[Main.myPlayer].armor[num100].type].Height > 32)
							{
								if (Main.itemTexture[Main.player[Main.myPlayer].armor[num100].type].Width > Main.itemTexture[Main.player[Main.myPlayer].armor[num100].type].Height)
								{
									num103 = 32f / (float)Main.itemTexture[Main.player[Main.myPlayer].armor[num100].type].Width;
								}
								else
								{
									num103 = 32f / (float)Main.itemTexture[Main.player[Main.myPlayer].armor[num100].type].Height;
								}
							}
							num103 *= Main.inventoryScale;
							this.spriteBatch.Draw(Main.itemTexture[Main.player[Main.myPlayer].armor[num100].type], new Vector2((float)num101 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.player[Main.myPlayer].armor[num100].type].Width * 0.5f * num103, (float)num102 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.player[Main.myPlayer].armor[num100].type].Height * 0.5f * num103), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[Main.player[Main.myPlayer].armor[num100].type].Width, Main.itemTexture[Main.player[Main.myPlayer].armor[num100].type].Height)), Main.player[Main.myPlayer].armor[num100].GetAlpha(white4), 0f, default(Vector2), num103, SpriteEffects.None, 0f);
							if (Main.player[Main.myPlayer].armor[num100].color != default(Color))
							{
								this.spriteBatch.Draw(Main.itemTexture[Main.player[Main.myPlayer].armor[num100].type], new Vector2((float)num101 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.player[Main.myPlayer].armor[num100].type].Width * 0.5f * num103, (float)num102 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.player[Main.myPlayer].armor[num100].type].Height * 0.5f * num103), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[Main.player[Main.myPlayer].armor[num100].type].Width, Main.itemTexture[Main.player[Main.myPlayer].armor[num100].type].Height)), Main.player[Main.myPlayer].armor[num100].GetColor(white4), 0f, default(Vector2), num103, SpriteEffects.None, 0f);
							}
							if (Main.player[Main.myPlayer].armor[num100].stack > 1)
							{
								this.spriteBatch.DrawString(Main.fontItemStack, string.Concat(Main.player[Main.myPlayer].armor[num100].stack), new Vector2((float)num101 + 10f * Main.inventoryScale, (float)num102 + 26f * Main.inventoryScale), white4, 0f, default(Vector2), num103, SpriteEffects.None, 0f);
							}
						}
					}
					Vector2 vector6 = Main.fontMouseText.MeasureString("Social");
					Vector2 vector7 = Main.fontMouseText.MeasureString(Lang.inter[11]);
					float num104 = vector6.X / vector7.X;
					this.spriteBatch.DrawString(Main.fontMouseText, Lang.inter[11], new Vector2((float)(Main.screenWidth - 64 - 28 - 44), 152f + (vector6.Y - vector6.Y * num104) / 2f), new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), 0f, default(Vector2), 0.8f * num104, SpriteEffects.None, 0f);
					if (Main.mouseX > Main.screenWidth - 64 - 28 - 47 && Main.mouseX < (int)((float)(Main.screenWidth - 64 - 20 - 47) + 56f * Main.inventoryScale) && Main.mouseY > 174 && Main.mouseY < (int)(174f + 168f * Main.inventoryScale))
					{
						Main.player[Main.myPlayer].mouseInterface = true;
					}
					for (int num105 = 8; num105 < 11; num105++)
					{
						int num106 = Main.screenWidth - 64 - 28 - 47;
						int num107 = (int)(174f + (float)((num105 - 8) * 56) * Main.inventoryScale);
						Color white5 = new Color(100, 100, 100, 100);
						string text19 = "";
						if (num105 == 8)
						{
							text19 = Lang.inter[12];
						}
						else
						{
							if (num105 == 9)
							{
								text19 = Lang.inter[13];
							}
							else
							{
								if (num105 == 10)
								{
									text19 = Lang.inter[14];
								}
							}
						}
						Vector2 vector8 = Main.fontMouseText.MeasureString(text19);
						this.spriteBatch.DrawString(Main.fontMouseText, text19, new Vector2((float)num106 - vector8.X - 10f, (float)num107 + (float)Main.inventoryBackTexture.Height * 0.5f - vector8.Y * 0.5f), color5, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
						if (Main.mouseX >= num106 && (float)Main.mouseX <= (float)num106 + (float)Main.inventoryBackTexture.Width * Main.inventoryScale && Main.mouseY >= num107 && (float)Main.mouseY <= (float)num107 + (float)Main.inventoryBackTexture.Height * Main.inventoryScale)
						{
							Main.player[Main.myPlayer].mouseInterface = true;
							Main.armorHide = true;
							if (Main.mouseLeftRelease && Main.mouseLeft)
							{
								if (Main.mouseItem.type == 0 || (Main.mouseItem.headSlot > -1 && num105 == 8) || (Main.mouseItem.bodySlot > -1 && num105 == 9) || (Main.mouseItem.legSlot > -1 && num105 == 10))
								{
									Item item4 = Main.mouseItem;
									Main.mouseItem = Main.player[Main.myPlayer].armor[num105];
									Main.player[Main.myPlayer].armor[num105] = item4;
									if (Main.player[Main.myPlayer].armor[num105].type == 0 || Main.player[Main.myPlayer].armor[num105].stack < 1)
									{
										Main.player[Main.myPlayer].armor[num105] = new Item();
									}
									if (Main.mouseItem.type == 0 || Main.mouseItem.stack < 1)
									{
										Main.mouseItem = new Item();
									}
									if (Main.mouseItem.type > 0 || Main.player[Main.myPlayer].armor[num105].type > 0)
									{
										Recipe.FindRecipes();
										Main.PlaySound(7, -1, -1, 1);
									}
								}
							}
							else
							{
								if (Main.mouseRight && Main.mouseRightRelease && Main.player[Main.myPlayer].armor[num105].maxStack == 1)
								{
									Main.player[Main.myPlayer].armor[num105] = Main.armorSwap(Main.player[Main.myPlayer].armor[num105]);
								}
							}
							text15 = Main.player[Main.myPlayer].armor[num105].name;
							Main.toolTip = (Item)Main.player[Main.myPlayer].armor[num105].Clone();
							Main.toolTip.social = true;
							if (num105 <= 2)
							{
								Main.toolTip.wornArmor = true;
							}
							if (Main.player[Main.myPlayer].armor[num105].stack > 1)
							{
								object obj = text15;
								text15 = string.Concat(new object[]
								{
									obj,
									" (",
									Main.player[Main.myPlayer].armor[num105].stack,
									")"
								});
							}
						}
						this.spriteBatch.Draw(Main.inventoryBack8Texture, new Vector2((float)num106, (float)num107), new Rectangle?(new Rectangle(0, 0, Main.inventoryBackTexture.Width, Main.inventoryBackTexture.Height)), color2, 0f, default(Vector2), Main.inventoryScale, SpriteEffects.None, 0f);
						white5 = Color.White;
						if (Main.player[Main.myPlayer].armor[num105].type > 0 && Main.player[Main.myPlayer].armor[num105].stack > 0)
						{
							float num108 = 1f;
							if (Main.itemTexture[Main.player[Main.myPlayer].armor[num105].type].Width > 32 || Main.itemTexture[Main.player[Main.myPlayer].armor[num105].type].Height > 32)
							{
								if (Main.itemTexture[Main.player[Main.myPlayer].armor[num105].type].Width > Main.itemTexture[Main.player[Main.myPlayer].armor[num105].type].Height)
								{
									num108 = 32f / (float)Main.itemTexture[Main.player[Main.myPlayer].armor[num105].type].Width;
								}
								else
								{
									num108 = 32f / (float)Main.itemTexture[Main.player[Main.myPlayer].armor[num105].type].Height;
								}
							}
							num108 *= Main.inventoryScale;
							this.spriteBatch.Draw(Main.itemTexture[Main.player[Main.myPlayer].armor[num105].type], new Vector2((float)num106 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.player[Main.myPlayer].armor[num105].type].Width * 0.5f * num108, (float)num107 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.player[Main.myPlayer].armor[num105].type].Height * 0.5f * num108), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[Main.player[Main.myPlayer].armor[num105].type].Width, Main.itemTexture[Main.player[Main.myPlayer].armor[num105].type].Height)), Main.player[Main.myPlayer].armor[num105].GetAlpha(white5), 0f, default(Vector2), num108, SpriteEffects.None, 0f);
							if (Main.player[Main.myPlayer].armor[num105].color != default(Color))
							{
								this.spriteBatch.Draw(Main.itemTexture[Main.player[Main.myPlayer].armor[num105].type], new Vector2((float)num106 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.player[Main.myPlayer].armor[num105].type].Width * 0.5f * num108, (float)num107 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.player[Main.myPlayer].armor[num105].type].Height * 0.5f * num108), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[Main.player[Main.myPlayer].armor[num105].type].Width, Main.itemTexture[Main.player[Main.myPlayer].armor[num105].type].Height)), Main.player[Main.myPlayer].armor[num105].GetColor(white5), 0f, default(Vector2), num108, SpriteEffects.None, 0f);
							}
							if (Main.player[Main.myPlayer].armor[num105].stack > 1)
							{
								this.spriteBatch.DrawString(Main.fontItemStack, string.Concat(Main.player[Main.myPlayer].armor[num105].stack), new Vector2((float)num106 + 10f * Main.inventoryScale, (float)num107 + 26f * Main.inventoryScale), white5, 0f, default(Vector2), num108, SpriteEffects.None, 0f);
							}
						}
					}
				}
				int num109 = (Main.screenHeight - 600) / 2;
				int num110 = (int)((float)Main.screenHeight / 600f * 250f);
				if (Main.craftingHide)
				{
					Main.craftingAlpha -= 0.1f;
					if (Main.craftingAlpha < 0f)
					{
						Main.craftingAlpha = 0f;
					}
				}
				else
				{
					Main.craftingAlpha += 0.025f;
					if (Main.craftingAlpha > 1f)
					{
						Main.craftingAlpha = 1f;
					}
				}
				Color color6 = new Color((int)((byte)((float)Main.mouseTextColor * Main.craftingAlpha)), (int)((byte)((float)Main.mouseTextColor * Main.craftingAlpha)), (int)((byte)((float)Main.mouseTextColor * Main.craftingAlpha)), (int)((byte)((float)Main.mouseTextColor * Main.craftingAlpha)));
				Main.craftingHide = false;
				if (Main.reforge)
				{
					if (Main.mouseReforge)
					{
						if ((double)Main.reforgeScale < 1.3)
						{
							Main.reforgeScale += 0.02f;
						}
					}
					else
					{
						if (Main.reforgeScale > 1f)
						{
							Main.reforgeScale -= 0.02f;
						}
					}
					if (Main.player[Main.myPlayer].chest != -1 || Main.npcShop != 0 || Main.player[Main.myPlayer].talkNPC == -1 || Main.craftGuide)
					{
						Main.reforge = false;
						Main.player[Main.myPlayer].dropItemCheck();
						Recipe.FindRecipes();
					}
					else
					{
						int num111 = 101;
						int num112 = 241;
						string text20 = Lang.inter[46] + ": ";
						if (Main.reforgeItem.type > 0)
						{
							int value2 = Main.reforgeItem.value;
							string text21 = "";
							int num113 = 0;
							int num114 = 0;
							int num115 = 0;
							int num116 = 0;
							int num117 = value2;
							if (num117 < 1)
							{
								num117 = 1;
							}
							if (num117 >= 1000000)
							{
								num113 = num117 / 1000000;
								num117 -= num113 * 1000000;
							}
							if (num117 >= 10000)
							{
								num114 = num117 / 10000;
								num117 -= num114 * 10000;
							}
							if (num117 >= 100)
							{
								num115 = num117 / 100;
								num117 -= num115 * 100;
							}
							if (num117 >= 1)
							{
								num116 = num117;
							}
							if (num113 > 0)
							{
								object obj = text21;
								text21 = string.Concat(new object[]
								{
									obj,
									num113,
									" ",
									Lang.inter[15],
									" "
								});
							}
							if (num114 > 0)
							{
								object obj = text21;
								text21 = string.Concat(new object[]
								{
									obj,
									num114,
									" ",
									Lang.inter[16],
									" "
								});
							}
							if (num115 > 0)
							{
								object obj = text21;
								text21 = string.Concat(new object[]
								{
									obj,
									num115,
									" ",
									Lang.inter[17],
									" "
								});
							}
							if (num116 > 0)
							{
								object obj = text21;
								text21 = string.Concat(new object[]
								{
									obj,
									num116,
									" ",
									Lang.inter[18],
									" "
								});
							}
							float num118 = (float)Main.mouseTextColor / 255f;
							Color white6 = Color.White;
							if (num113 > 0)
							{
								white6 = new Color((int)((byte)(220f * num118)), (int)((byte)(220f * num118)), (int)((byte)(198f * num118)), (int)Main.mouseTextColor);
							}
							else
							{
								if (num114 > 0)
								{
									white6 = new Color((int)((byte)(224f * num118)), (int)((byte)(201f * num118)), (int)((byte)(92f * num118)), (int)Main.mouseTextColor);
								}
								else
								{
									if (num115 > 0)
									{
										white6 = new Color((int)((byte)(181f * num118)), (int)((byte)(192f * num118)), (int)((byte)(193f * num118)), (int)Main.mouseTextColor);
									}
									else
									{
										if (num116 > 0)
										{
											white6 = new Color((int)((byte)(246f * num118)), (int)((byte)(138f * num118)), (int)((byte)(96f * num118)), (int)Main.mouseTextColor);
										}
									}
								}
							}
							this.spriteBatch.DrawString(Main.fontMouseText, text21, new Vector2((float)(num111 + 50) + Main.fontMouseText.MeasureString(text20).X, (float)num112), white6, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
							int num119 = num111 + 70;
							int num120 = num112 + 40;
							this.spriteBatch.Draw(Main.reforgeTexture, new Vector2((float)num119, (float)num120), new Rectangle?(new Rectangle(0, 0, Main.reforgeTexture.Width, Main.reforgeTexture.Height)), Color.White, 0f, new Vector2((float)(Main.reforgeTexture.Width / 2), (float)(Main.reforgeTexture.Height / 2)), Main.reforgeScale, SpriteEffects.None, 0f);
							if (Main.mouseX > num119 - Main.reforgeTexture.Width / 2 && Main.mouseX < num119 + Main.reforgeTexture.Width / 2 && Main.mouseY > num120 - Main.reforgeTexture.Height / 2 && Main.mouseY < num120 + Main.reforgeTexture.Height / 2)
							{
								text15 = Lang.inter[19];
								if (!Main.mouseReforge)
								{
									Main.PlaySound(12, -1, -1, 1);
								}
								Main.mouseReforge = true;
								Main.player[Main.myPlayer].mouseInterface = true;
								if (Main.mouseLeftRelease && Main.mouseLeft && Main.player[Main.myPlayer].BuyItem(value2))
								{
									Main.reforgeItem.SetDefaults(Main.reforgeItem.name);
									Main.reforgeItem.Prefix(-2);
									Main.reforgeItem.position.X = Main.player[Main.myPlayer].position.X + (float)(Main.player[Main.myPlayer].width / 2) - (float)(Main.reforgeItem.width / 2);
									Main.reforgeItem.position.Y = Main.player[Main.myPlayer].position.Y + (float)(Main.player[Main.myPlayer].height / 2) - (float)(Main.reforgeItem.height / 2);
									ItemText.NewText(Main.reforgeItem, Main.reforgeItem.stack);
									Main.PlaySound(2, -1, -1, 37);
								}
							}
							else
							{
								Main.mouseReforge = false;
							}
						}
						else
						{
							text20 = Lang.inter[20];
						}
						this.spriteBatch.DrawString(Main.fontMouseText, text20, new Vector2((float)(num111 + 50), (float)num112), new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
						Color white7 = new Color(100, 100, 100, 100);
						if (Main.mouseX >= num111 && (float)Main.mouseX <= (float)num111 + (float)Main.inventoryBackTexture.Width * Main.inventoryScale && Main.mouseY >= num112 && (float)Main.mouseY <= (float)num112 + (float)Main.inventoryBackTexture.Height * Main.inventoryScale)
						{
							Main.player[Main.myPlayer].mouseInterface = true;
							Main.craftingHide = true;
							if (Main.mouseItem.Prefix(-3) || Main.mouseItem.type == 0)
							{
								if (Main.mouseLeftRelease && Main.mouseLeft)
								{
									Item item5 = Main.mouseItem;
									Main.mouseItem = Main.reforgeItem;
									Main.reforgeItem = item5;
									if (Main.reforgeItem.type == 0 || Main.reforgeItem.stack < 1)
									{
										Main.reforgeItem = new Item();
									}
									if (Main.mouseItem.IsTheSameAs(Main.reforgeItem) && Main.reforgeItem.stack != Main.reforgeItem.maxStack && Main.mouseItem.stack != Main.mouseItem.maxStack)
									{
										if (Main.mouseItem.stack + Main.reforgeItem.stack <= Main.mouseItem.maxStack)
										{
											Main.reforgeItem.stack += Main.mouseItem.stack;
											Main.mouseItem.stack = 0;
										}
										else
										{
											int num121 = Main.mouseItem.maxStack - Main.reforgeItem.stack;
											Main.reforgeItem.stack += num121;
											Main.mouseItem.stack -= num121;
										}
									}
									if (Main.mouseItem.type == 0 || Main.mouseItem.stack < 1)
									{
										Main.mouseItem = new Item();
									}
									if (Main.mouseItem.type > 0 || Main.reforgeItem.type > 0)
									{
										Recipe.FindRecipes();
										Main.PlaySound(7, -1, -1, 1);
									}
								}
								else
								{
									if (Main.stackSplit <= 1 && Main.mouseRight && (Main.mouseItem.IsTheSameAs(Main.reforgeItem) || Main.mouseItem.type == 0) && (Main.mouseItem.stack < Main.mouseItem.maxStack || Main.mouseItem.type == 0))
									{
										if (Main.mouseItem.type == 0)
										{
											Main.mouseItem = (Item)Main.reforgeItem.Clone();
											Main.mouseItem.stack = 0;
										}
										Main.mouseItem.stack++;
										Main.reforgeItem.stack--;
										if (Main.reforgeItem.stack <= 0)
										{
											Main.reforgeItem = new Item();
										}
										Recipe.FindRecipes();
										Main.soundInstanceMenuTick.Stop();
										Main.soundInstanceMenuTick = Main.soundMenuTick.CreateInstance();
										Main.PlaySound(12, -1, -1, 1);
										if (Main.stackSplit == 0)
										{
											Main.stackSplit = 15;
										}
										else
										{
											Main.stackSplit = Main.stackDelay;
										}
									}
								}
							}
							text15 = Main.reforgeItem.name;
							Main.toolTip = (Item)Main.reforgeItem.Clone();
							if (Main.reforgeItem.stack > 1)
							{
								object obj = text15;
								text15 = string.Concat(new object[]
								{
									obj,
									" (",
									Main.reforgeItem.stack,
									")"
								});
							}
						}
						this.spriteBatch.Draw(Main.inventoryBack4Texture, new Vector2((float)num111, (float)num112), new Rectangle?(new Rectangle(0, 0, Main.inventoryBackTexture.Width, Main.inventoryBackTexture.Height)), color2, 0f, default(Vector2), Main.inventoryScale, SpriteEffects.None, 0f);
						white7 = Color.White;
						if (Main.reforgeItem.type > 0 && Main.reforgeItem.stack > 0)
						{
							float num122 = 1f;
							if (Main.itemTexture[Main.reforgeItem.type].Width > 32 || Main.itemTexture[Main.reforgeItem.type].Height > 32)
							{
								if (Main.itemTexture[Main.reforgeItem.type].Width > Main.itemTexture[Main.reforgeItem.type].Height)
								{
									num122 = 32f / (float)Main.itemTexture[Main.reforgeItem.type].Width;
								}
								else
								{
									num122 = 32f / (float)Main.itemTexture[Main.reforgeItem.type].Height;
								}
							}
							num122 *= Main.inventoryScale;
							this.spriteBatch.Draw(Main.itemTexture[Main.reforgeItem.type], new Vector2((float)num111 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.reforgeItem.type].Width * 0.5f * num122, (float)num112 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.reforgeItem.type].Height * 0.5f * num122), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[Main.reforgeItem.type].Width, Main.itemTexture[Main.reforgeItem.type].Height)), Main.reforgeItem.GetAlpha(white7), 0f, default(Vector2), num122, SpriteEffects.None, 0f);
							if (Main.reforgeItem.color != default(Color))
							{
								this.spriteBatch.Draw(Main.itemTexture[Main.reforgeItem.type], new Vector2((float)num111 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.reforgeItem.type].Width * 0.5f * num122, (float)num112 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.reforgeItem.type].Height * 0.5f * num122), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[Main.reforgeItem.type].Width, Main.itemTexture[Main.reforgeItem.type].Height)), Main.reforgeItem.GetColor(white7), 0f, default(Vector2), num122, SpriteEffects.None, 0f);
							}
							if (Main.reforgeItem.stack > 1)
							{
								this.spriteBatch.DrawString(Main.fontItemStack, string.Concat(Main.reforgeItem.stack), new Vector2((float)num111 + 10f * Main.inventoryScale, (float)num112 + 26f * Main.inventoryScale), white7, 0f, default(Vector2), num122, SpriteEffects.None, 0f);
							}
						}
					}
				}
				else
				{
					if (Main.craftGuide)
					{
						if (Main.player[Main.myPlayer].chest != -1 || Main.npcShop != 0 || Main.player[Main.myPlayer].talkNPC == -1 || Main.reforge)
						{
							Main.craftGuide = false;
							Main.player[Main.myPlayer].dropItemCheck();
							Recipe.FindRecipes();
						}
						else
						{
							int num123 = 73;
							int num124 = 331;
							num124 += num109;
							string text22;
							if (Main.guideItem.type > 0)
							{
								text22 = Lang.inter[21] + " " + Main.guideItem.name;
								this.spriteBatch.DrawString(Main.fontMouseText, Lang.inter[22], new Vector2((float)num123, (float)(num124 + 118)), color6, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
								int num125 = Main.focusRecipe;
								int num126 = 0;
								int num127 = 0;
								while (num127 < Recipe.maxRequirements)
								{
									int num128 = (num127 + 1) * 26;
									if (Main.recipe[Main.availableRecipe[num125]].requiredTile[num127] == -1)
									{
										if (num127 == 0 && !Main.recipe[Main.availableRecipe[num125]].needWater)
										{
											this.spriteBatch.DrawString(Main.fontMouseText, Lang.inter[23], new Vector2((float)num123, (float)(num124 + 118 + num128)), color6, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
											break;
										}
										break;
									}
									else
									{
										num126++;
										this.spriteBatch.DrawString(Main.fontMouseText, Main.tileName[Main.recipe[Main.availableRecipe[num125]].requiredTile[num127]], new Vector2((float)num123, (float)(num124 + 118 + num128)), color6, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
										num127++;
									}
								}
								if (Main.recipe[Main.availableRecipe[num125]].needWater)
								{
									int num129 = (num126 + 1) * 26;
									this.spriteBatch.DrawString(Main.fontMouseText, Lang.inter[53], new Vector2((float)num123, (float)(num124 + 118 + num129)), color6, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
								}
							}
							else
							{
								text22 = Lang.inter[24];
							}
							this.spriteBatch.DrawString(Main.fontMouseText, text22, new Vector2((float)(num123 + 50), (float)(num124 + 12)), new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
							Color white8 = new Color(100, 100, 100, 100);
							if (Main.mouseX >= num123 && (float)Main.mouseX <= (float)num123 + (float)Main.inventoryBackTexture.Width * Main.inventoryScale && Main.mouseY >= num124 && (float)Main.mouseY <= (float)num124 + (float)Main.inventoryBackTexture.Height * Main.inventoryScale)
							{
								Main.player[Main.myPlayer].mouseInterface = true;
								Main.craftingHide = true;
								if (Main.mouseItem.material || Main.mouseItem.type == 0)
								{
									if (Main.mouseLeftRelease && Main.mouseLeft)
									{
										Item item6 = Main.mouseItem;
										Main.mouseItem = Main.guideItem;
										Main.guideItem = item6;
										if (Main.guideItem.type == 0 || Main.guideItem.stack < 1)
										{
											Main.guideItem = new Item();
										}
										if (Main.mouseItem.IsTheSameAs(Main.guideItem) && Main.guideItem.stack != Main.guideItem.maxStack && Main.mouseItem.stack != Main.mouseItem.maxStack)
										{
											if (Main.mouseItem.stack + Main.guideItem.stack <= Main.mouseItem.maxStack)
											{
												Main.guideItem.stack += Main.mouseItem.stack;
												Main.mouseItem.stack = 0;
											}
											else
											{
												int num130 = Main.mouseItem.maxStack - Main.guideItem.stack;
												Main.guideItem.stack += num130;
												Main.mouseItem.stack -= num130;
											}
										}
										if (Main.mouseItem.type == 0 || Main.mouseItem.stack < 1)
										{
											Main.mouseItem = new Item();
										}
										if (Main.mouseItem.type > 0 || Main.guideItem.type > 0)
										{
											Recipe.FindRecipes();
											Main.PlaySound(7, -1, -1, 1);
										}
									}
									else
									{
										if (Main.stackSplit <= 1 && Main.mouseRight && (Main.mouseItem.IsTheSameAs(Main.guideItem) || Main.mouseItem.type == 0) && (Main.mouseItem.stack < Main.mouseItem.maxStack || Main.mouseItem.type == 0))
										{
											if (Main.mouseItem.type == 0)
											{
												Main.mouseItem = (Item)Main.guideItem.Clone();
												Main.mouseItem.stack = 0;
											}
											Main.mouseItem.stack++;
											Main.guideItem.stack--;
											if (Main.guideItem.stack <= 0)
											{
												Main.guideItem = new Item();
											}
											Recipe.FindRecipes();
											Main.soundInstanceMenuTick.Stop();
											Main.soundInstanceMenuTick = Main.soundMenuTick.CreateInstance();
											Main.PlaySound(12, -1, -1, 1);
											if (Main.stackSplit == 0)
											{
												Main.stackSplit = 15;
											}
											else
											{
												Main.stackSplit = Main.stackDelay;
											}
										}
									}
								}
								text15 = Main.guideItem.name;
								Main.toolTip = (Item)Main.guideItem.Clone();
								if (Main.guideItem.stack > 1)
								{
									object obj = text15;
									text15 = string.Concat(new object[]
									{
										obj,
										" (",
										Main.guideItem.stack,
										")"
									});
								}
							}
							this.spriteBatch.Draw(Main.inventoryBack4Texture, new Vector2((float)num123, (float)num124), new Rectangle?(new Rectangle(0, 0, Main.inventoryBackTexture.Width, Main.inventoryBackTexture.Height)), color2, 0f, default(Vector2), Main.inventoryScale, SpriteEffects.None, 0f);
							white8 = Color.White;
							if (Main.guideItem.type > 0 && Main.guideItem.stack > 0)
							{
								float num131 = 1f;
								if (Main.itemTexture[Main.guideItem.type].Width > 32 || Main.itemTexture[Main.guideItem.type].Height > 32)
								{
									if (Main.itemTexture[Main.guideItem.type].Width > Main.itemTexture[Main.guideItem.type].Height)
									{
										num131 = 32f / (float)Main.itemTexture[Main.guideItem.type].Width;
									}
									else
									{
										num131 = 32f / (float)Main.itemTexture[Main.guideItem.type].Height;
									}
								}
								num131 *= Main.inventoryScale;
								this.spriteBatch.Draw(Main.itemTexture[Main.guideItem.type], new Vector2((float)num123 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.guideItem.type].Width * 0.5f * num131, (float)num124 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.guideItem.type].Height * 0.5f * num131), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[Main.guideItem.type].Width, Main.itemTexture[Main.guideItem.type].Height)), Main.guideItem.GetAlpha(white8), 0f, default(Vector2), num131, SpriteEffects.None, 0f);
								if (Main.guideItem.color != default(Color))
								{
									this.spriteBatch.Draw(Main.itemTexture[Main.guideItem.type], new Vector2((float)num123 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.guideItem.type].Width * 0.5f * num131, (float)num124 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.guideItem.type].Height * 0.5f * num131), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[Main.guideItem.type].Width, Main.itemTexture[Main.guideItem.type].Height)), Main.guideItem.GetColor(white8), 0f, default(Vector2), num131, SpriteEffects.None, 0f);
								}
								if (Main.guideItem.stack > 1)
								{
									this.spriteBatch.DrawString(Main.fontItemStack, string.Concat(Main.guideItem.stack), new Vector2((float)num123 + 10f * Main.inventoryScale, (float)num124 + 26f * Main.inventoryScale), white8, 0f, default(Vector2), num131, SpriteEffects.None, 0f);
								}
							}
						}
					}
				}
				if (!Main.reforge)
				{
					if (Main.numAvailableRecipes > 0)
					{
						this.spriteBatch.DrawString(Main.fontMouseText, Lang.inter[25], new Vector2(76f, (float)(414 + num109)), color6, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
					}
					for (int num132 = 0; num132 < Recipe.maxRecipes; num132++)
					{
						Main.inventoryScale = 100f / (Math.Abs(Main.availableRecipeY[num132]) + 100f);
						if ((double)Main.inventoryScale < 0.75)
						{
							Main.inventoryScale = 0.75f;
						}
						if (Main.availableRecipeY[num132] < (float)((num132 - Main.focusRecipe) * 65))
						{
							if (Main.availableRecipeY[num132] == 0f)
							{
								Main.PlaySound(12, -1, -1, 1);
							}
							Main.availableRecipeY[num132] += 6.5f;
						}
						else
						{
							if (Main.availableRecipeY[num132] > (float)((num132 - Main.focusRecipe) * 65))
							{
								if (Main.availableRecipeY[num132] == 0f)
								{
									Main.PlaySound(12, -1, -1, 1);
								}
								Main.availableRecipeY[num132] -= 6.5f;
							}
						}
						if (num132 < Main.numAvailableRecipes && Math.Abs(Main.availableRecipeY[num132]) <= (float)num110)
						{
							int num133 = (int)(46f - 26f * Main.inventoryScale);
							int num134 = (int)(410f + Main.availableRecipeY[num132] * Main.inventoryScale - 30f * Main.inventoryScale + (float)num109);
							double num135 = (double)(color2.A + 50);
							double num136 = 255.0;
							if (Math.Abs(Main.availableRecipeY[num132]) > (float)(num110 - 100))
							{
								num135 = (double)(150f * (100f - (Math.Abs(Main.availableRecipeY[num132]) - (float)(num110 - 100)))) * 0.01;
								num136 = (double)(255f * (100f - (Math.Abs(Main.availableRecipeY[num132]) - (float)(num110 - 100)))) * 0.01;
							}
							new Color((int)((byte)num135), (int)((byte)num135), (int)((byte)num135), (int)((byte)num135));
							Color color7 = new Color((int)((byte)num136), (int)((byte)num136), (int)((byte)num136), (int)((byte)num136));
							if (Main.mouseX >= num133 && (float)Main.mouseX <= (float)num133 + (float)Main.inventoryBackTexture.Width * Main.inventoryScale && Main.mouseY >= num134 && (float)Main.mouseY <= (float)num134 + (float)Main.inventoryBackTexture.Height * Main.inventoryScale)
							{
								Main.player[Main.myPlayer].mouseInterface = true;
								if (Main.focusRecipe == num132 && Main.guideItem.type == 0)
								{
									if (Main.mouseItem.type == 0 || (Main.mouseItem.IsTheSameAs(Main.recipe[Main.availableRecipe[num132]].createItem) && Main.mouseItem.stack + Main.recipe[Main.availableRecipe[num132]].createItem.stack <= Main.mouseItem.maxStack))
									{
										if (Main.mouseLeftRelease && Main.mouseLeft)
										{
											int stack = Main.mouseItem.stack;
											Main.mouseItem = (Item)Main.recipe[Main.availableRecipe[num132]].createItem.Clone();
											Main.mouseItem.Prefix(-1);
											Main.mouseItem.stack += stack;
											Main.mouseItem.position.X = Main.player[Main.myPlayer].position.X + (float)(Main.player[Main.myPlayer].width / 2) - (float)(Main.mouseItem.width / 2);
											Main.mouseItem.position.Y = Main.player[Main.myPlayer].position.Y + (float)(Main.player[Main.myPlayer].height / 2) - (float)(Main.mouseItem.height / 2);
											ItemText.NewText(Main.mouseItem, Main.recipe[Main.availableRecipe[num132]].createItem.stack);
											Main.recipe[Main.availableRecipe[num132]].Create();
											if (Main.mouseItem.type > 0 || Main.recipe[Main.availableRecipe[num132]].createItem.type > 0)
											{
												Main.PlaySound(7, -1, -1, 1);
											}
										}
										else
										{
											if (Main.stackSplit <= 1 && Main.mouseRight && (Main.mouseItem.stack < Main.mouseItem.maxStack || Main.mouseItem.type == 0))
											{
												if (Main.stackSplit == 0)
												{
													Main.stackSplit = 15;
												}
												else
												{
													Main.stackSplit = Main.stackDelay;
												}
												int stack2 = Main.mouseItem.stack;
												Main.mouseItem = (Item)Main.recipe[Main.availableRecipe[num132]].createItem.Clone();
												Main.mouseItem.stack += stack2;
												Main.mouseItem.position.X = Main.player[Main.myPlayer].position.X + (float)(Main.player[Main.myPlayer].width / 2) - (float)(Main.mouseItem.width / 2);
												Main.mouseItem.position.Y = Main.player[Main.myPlayer].position.Y + (float)(Main.player[Main.myPlayer].height / 2) - (float)(Main.mouseItem.height / 2);
												ItemText.NewText(Main.mouseItem, Main.recipe[Main.availableRecipe[num132]].createItem.stack);
												Main.recipe[Main.availableRecipe[num132]].Create();
												if (Main.mouseItem.type > 0 || Main.recipe[Main.availableRecipe[num132]].createItem.type > 0)
												{
													Main.PlaySound(7, -1, -1, 1);
												}
											}
										}
									}
								}
								else
								{
									if (Main.mouseLeftRelease && Main.mouseLeft)
									{
										Main.focusRecipe = num132;
									}
								}
								Main.craftingHide = true;
								text15 = Main.recipe[Main.availableRecipe[num132]].createItem.name;
								Main.toolTip = (Item)Main.recipe[Main.availableRecipe[num132]].createItem.Clone();
								if (Main.recipe[Main.availableRecipe[num132]].createItem.stack > 1)
								{
									object obj = text15;
									text15 = string.Concat(new object[]
									{
										obj,
										" (",
										Main.recipe[Main.availableRecipe[num132]].createItem.stack,
										")"
									});
								}
							}
							if (Main.numAvailableRecipes > 0)
							{
								num135 -= 50.0;
								if (num135 < 0.0)
								{
									num135 = 0.0;
								}
								this.spriteBatch.Draw(Main.inventoryBack4Texture, new Vector2((float)num133, (float)num134), new Rectangle?(new Rectangle(0, 0, Main.inventoryBackTexture.Width, Main.inventoryBackTexture.Height)), new Color((int)((byte)num135), (int)((byte)num135), (int)((byte)num135), (int)((byte)num135)), 0f, default(Vector2), Main.inventoryScale, SpriteEffects.None, 0f);
								if (Main.recipe[Main.availableRecipe[num132]].createItem.type > 0 && Main.recipe[Main.availableRecipe[num132]].createItem.stack > 0)
								{
									float num137 = 1f;
									if (Main.itemTexture[Main.recipe[Main.availableRecipe[num132]].createItem.type].Width > 32 || Main.itemTexture[Main.recipe[Main.availableRecipe[num132]].createItem.type].Height > 32)
									{
										if (Main.itemTexture[Main.recipe[Main.availableRecipe[num132]].createItem.type].Width > Main.itemTexture[Main.recipe[Main.availableRecipe[num132]].createItem.type].Height)
										{
											num137 = 32f / (float)Main.itemTexture[Main.recipe[Main.availableRecipe[num132]].createItem.type].Width;
										}
										else
										{
											num137 = 32f / (float)Main.itemTexture[Main.recipe[Main.availableRecipe[num132]].createItem.type].Height;
										}
									}
									num137 *= Main.inventoryScale;
									this.spriteBatch.Draw(Main.itemTexture[Main.recipe[Main.availableRecipe[num132]].createItem.type], new Vector2((float)num133 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.recipe[Main.availableRecipe[num132]].createItem.type].Width * 0.5f * num137, (float)num134 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.recipe[Main.availableRecipe[num132]].createItem.type].Height * 0.5f * num137), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[Main.recipe[Main.availableRecipe[num132]].createItem.type].Width, Main.itemTexture[Main.recipe[Main.availableRecipe[num132]].createItem.type].Height)), Main.recipe[Main.availableRecipe[num132]].createItem.GetAlpha(color7), 0f, default(Vector2), num137, SpriteEffects.None, 0f);
									if (Main.recipe[Main.availableRecipe[num132]].createItem.color != default(Color))
									{
										this.spriteBatch.Draw(Main.itemTexture[Main.recipe[Main.availableRecipe[num132]].createItem.type], new Vector2((float)num133 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.recipe[Main.availableRecipe[num132]].createItem.type].Width * 0.5f * num137, (float)num134 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.recipe[Main.availableRecipe[num132]].createItem.type].Height * 0.5f * num137), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[Main.recipe[Main.availableRecipe[num132]].createItem.type].Width, Main.itemTexture[Main.recipe[Main.availableRecipe[num132]].createItem.type].Height)), Main.recipe[Main.availableRecipe[num132]].createItem.GetColor(color7), 0f, default(Vector2), num137, SpriteEffects.None, 0f);
									}
									if (Main.recipe[Main.availableRecipe[num132]].createItem.stack > 1)
									{
										this.spriteBatch.DrawString(Main.fontItemStack, string.Concat(Main.recipe[Main.availableRecipe[num132]].createItem.stack), new Vector2((float)num133 + 10f * Main.inventoryScale, (float)num134 + 26f * Main.inventoryScale), color7, 0f, default(Vector2), num137, SpriteEffects.None, 0f);
									}
								}
							}
						}
					}
					if (Main.numAvailableRecipes > 0)
					{
						int num138 = 0;
						while (num138 < Recipe.maxRequirements && Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[num138].type != 0)
						{
							int num139 = 80 + num138 * 40;
							int num140 = 380 + num109;
							double num141 = (double)(color2.A + 50);
							Color white9 = Color.White;
							Color white10 = Color.White;
							num141 = (double)((float)(color2.A + 50) - Math.Abs(Main.availableRecipeY[Main.focusRecipe]) * 2f);
							double num142 = (double)(255f - Math.Abs(Main.availableRecipeY[Main.focusRecipe]) * 2f);
							if (num141 < 0.0)
							{
								num141 = 0.0;
							}
							if (num142 < 0.0)
							{
								num142 = 0.0;
							}
							white9.R = (byte)num141;
							white9.G = (byte)num141;
							white9.B = (byte)num141;
							white9.A = (byte)num141;
							white10.R = (byte)num142;
							white10.G = (byte)num142;
							white10.B = (byte)num142;
							white10.A = (byte)num142;
							Main.inventoryScale = 0.6f;
							if (num141 == 0.0)
							{
								break;
							}
							if (Main.mouseX >= num139 && (float)Main.mouseX <= (float)num139 + (float)Main.inventoryBackTexture.Width * Main.inventoryScale && Main.mouseY >= num140 && (float)Main.mouseY <= (float)num140 + (float)Main.inventoryBackTexture.Height * Main.inventoryScale)
							{
								Main.craftingHide = true;
								Main.player[Main.myPlayer].mouseInterface = true;
								text15 = Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[num138].name;
								Main.toolTip = (Item)Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[num138].Clone();
								if (Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[num138].stack > 1)
								{
									object obj = text15;
									text15 = string.Concat(new object[]
									{
										obj,
										" (",
										Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[num138].stack,
										")"
									});
								}
							}
							num141 -= 50.0;
							if (num141 < 0.0)
							{
								num141 = 0.0;
							}
							this.spriteBatch.Draw(Main.inventoryBack4Texture, new Vector2((float)num139, (float)num140), new Rectangle?(new Rectangle(0, 0, Main.inventoryBackTexture.Width, Main.inventoryBackTexture.Height)), new Color((int)((byte)num141), (int)((byte)num141), (int)((byte)num141), (int)((byte)num141)), 0f, default(Vector2), Main.inventoryScale, SpriteEffects.None, 0f);
							if (Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[num138].type > 0 && Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[num138].stack > 0)
							{
								float num143 = 1f;
								if (Main.itemTexture[Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[num138].type].Width > 32 || Main.itemTexture[Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[num138].type].Height > 32)
								{
									if (Main.itemTexture[Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[num138].type].Width > Main.itemTexture[Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[num138].type].Height)
									{
										num143 = 32f / (float)Main.itemTexture[Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[num138].type].Width;
									}
									else
									{
										num143 = 32f / (float)Main.itemTexture[Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[num138].type].Height;
									}
								}
								num143 *= Main.inventoryScale;
								this.spriteBatch.Draw(Main.itemTexture[Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[num138].type], new Vector2((float)num139 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[num138].type].Width * 0.5f * num143, (float)num140 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[num138].type].Height * 0.5f * num143), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[num138].type].Width, Main.itemTexture[Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[num138].type].Height)), Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[num138].GetAlpha(white10), 0f, default(Vector2), num143, SpriteEffects.None, 0f);
								if (Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[num138].color != default(Color))
								{
									this.spriteBatch.Draw(Main.itemTexture[Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[num138].type], new Vector2((float)num139 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[num138].type].Width * 0.5f * num143, (float)num140 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[num138].type].Height * 0.5f * num143), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[num138].type].Width, Main.itemTexture[Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[num138].type].Height)), Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[num138].GetColor(white10), 0f, default(Vector2), num143, SpriteEffects.None, 0f);
								}
								if (Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[num138].stack > 1)
								{
									this.spriteBatch.DrawString(Main.fontItemStack, string.Concat(Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[num138].stack), new Vector2((float)num139 + 10f * Main.inventoryScale, (float)num140 + 26f * Main.inventoryScale), white10, 0f, default(Vector2), num143, SpriteEffects.None, 0f);
								}
							}
							num138++;
						}
					}
				}
				Vector2 vector9 = Main.fontMouseText.MeasureString("Coins");
				Vector2 vector10 = Main.fontMouseText.MeasureString(Lang.inter[26]);
				float num144 = vector9.X / vector10.X;
				this.spriteBatch.DrawString(Main.fontMouseText, Lang.inter[26], new Vector2(496f, 84f + (vector9.Y - vector9.Y * num144) / 2f), new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), 0f, default(Vector2), 0.75f * num144, SpriteEffects.None, 0f);
				Main.inventoryScale = 0.6f;
				for (int num145 = 0; num145 < 4; num145++)
				{
					int num146 = 497;
					int num147 = (int)(85f + (float)(num145 * 56) * Main.inventoryScale + 20f);
					int num148 = num145 + 40;
					Color white11 = new Color(100, 100, 100, 100);
					if (Main.mouseX >= num146 && (float)Main.mouseX <= (float)num146 + (float)Main.inventoryBackTexture.Width * Main.inventoryScale && Main.mouseY >= num147 && (float)Main.mouseY <= (float)num147 + (float)Main.inventoryBackTexture.Height * Main.inventoryScale)
					{
						Main.player[Main.myPlayer].mouseInterface = true;
						if (Main.mouseLeftRelease && Main.mouseLeft)
						{
							if (Main.keyState.IsKeyDown(Keys.LeftShift))
							{
								if (Main.player[Main.myPlayer].inventory[num148].type > 0)
								{
									if (Main.npcShop > 0)
									{
										if (Main.player[Main.myPlayer].SellItem(Main.player[Main.myPlayer].inventory[num148].value, Main.player[Main.myPlayer].inventory[num148].stack))
										{
											this.shop[Main.npcShop].AddShop(Main.player[Main.myPlayer].inventory[num148]);
											Main.player[Main.myPlayer].inventory[num148].SetDefaults(0, false);
											Main.PlaySound(18, -1, -1, 1);
										}
										else
										{
											if (Main.player[Main.myPlayer].inventory[num148].value == 0)
											{
												this.shop[Main.npcShop].AddShop(Main.player[Main.myPlayer].inventory[num148]);
												Main.player[Main.myPlayer].inventory[num148].SetDefaults(0, false);
												Main.PlaySound(7, -1, -1, 1);
											}
										}
									}
									else
									{
										Recipe.FindRecipes();
										Main.PlaySound(7, -1, -1, 1);
										Main.trashItem = (Item)Main.player[Main.myPlayer].inventory[num148].Clone();
										Main.player[Main.myPlayer].inventory[num148].SetDefaults(0, false);
									}
								}
							}
							else
							{
								if ((Main.player[Main.myPlayer].selectedItem != num148 || Main.player[Main.myPlayer].itemAnimation <= 0) && (Main.mouseItem.type == 0 || Main.mouseItem.type == 71 || Main.mouseItem.type == 72 || Main.mouseItem.type == 73 || Main.mouseItem.type == 74))
								{
									Item item7 = Main.mouseItem;
									Main.mouseItem = Main.player[Main.myPlayer].inventory[num148];
									Main.player[Main.myPlayer].inventory[num148] = item7;
									if (Main.player[Main.myPlayer].inventory[num148].type == 0 || Main.player[Main.myPlayer].inventory[num148].stack < 1)
									{
										Main.player[Main.myPlayer].inventory[num148] = new Item();
									}
									if (Main.mouseItem.IsTheSameAs(Main.player[Main.myPlayer].inventory[num148]) && Main.player[Main.myPlayer].inventory[num148].stack != Main.player[Main.myPlayer].inventory[num148].maxStack && Main.mouseItem.stack != Main.mouseItem.maxStack)
									{
										if (Main.mouseItem.stack + Main.player[Main.myPlayer].inventory[num148].stack <= Main.mouseItem.maxStack)
										{
											Main.player[Main.myPlayer].inventory[num148].stack += Main.mouseItem.stack;
											Main.mouseItem.stack = 0;
										}
										else
										{
											int num149 = Main.mouseItem.maxStack - Main.player[Main.myPlayer].inventory[num148].stack;
											Main.player[Main.myPlayer].inventory[num148].stack += num149;
											Main.mouseItem.stack -= num149;
										}
									}
									if (Main.mouseItem.type == 0 || Main.mouseItem.stack < 1)
									{
										Main.mouseItem = new Item();
									}
									if (Main.mouseItem.type > 0 || Main.player[Main.myPlayer].inventory[num148].type > 0)
									{
										Main.PlaySound(7, -1, -1, 1);
									}
									Recipe.FindRecipes();
								}
							}
						}
						else
						{
							if (Main.stackSplit <= 1 && Main.mouseRight && (Main.mouseItem.IsTheSameAs(Main.player[Main.myPlayer].inventory[num148]) || Main.mouseItem.type == 0) && (Main.mouseItem.stack < Main.mouseItem.maxStack || Main.mouseItem.type == 0))
							{
								if (Main.mouseItem.type == 0)
								{
									Main.mouseItem = (Item)Main.player[Main.myPlayer].inventory[num148].Clone();
									Main.mouseItem.stack = 0;
								}
								Main.mouseItem.stack++;
								Main.player[Main.myPlayer].inventory[num148].stack--;
								if (Main.player[Main.myPlayer].inventory[num148].stack <= 0)
								{
									Main.player[Main.myPlayer].inventory[num148] = new Item();
								}
								Recipe.FindRecipes();
								Main.soundInstanceMenuTick.Stop();
								Main.soundInstanceMenuTick = Main.soundMenuTick.CreateInstance();
								Main.PlaySound(12, -1, -1, 1);
								if (Main.stackSplit == 0)
								{
									Main.stackSplit = 15;
								}
								else
								{
									Main.stackSplit = Main.stackDelay;
								}
							}
						}
						text15 = Main.player[Main.myPlayer].inventory[num148].name;
						Main.toolTip = (Item)Main.player[Main.myPlayer].inventory[num148].Clone();
						if (Main.player[Main.myPlayer].inventory[num148].stack > 1)
						{
							object obj = text15;
							text15 = string.Concat(new object[]
							{
								obj,
								" (",
								Main.player[Main.myPlayer].inventory[num148].stack,
								")"
							});
						}
					}
					this.spriteBatch.Draw(Main.inventoryBackTexture, new Vector2((float)num146, (float)num147), new Rectangle?(new Rectangle(0, 0, Main.inventoryBackTexture.Width, Main.inventoryBackTexture.Height)), color2, 0f, default(Vector2), Main.inventoryScale, SpriteEffects.None, 0f);
					white11 = Color.White;
					if (Main.player[Main.myPlayer].inventory[num148].type > 0 && Main.player[Main.myPlayer].inventory[num148].stack > 0)
					{
						float num150 = 1f;
						if (Main.itemTexture[Main.player[Main.myPlayer].inventory[num148].type].Width > 32 || Main.itemTexture[Main.player[Main.myPlayer].inventory[num148].type].Height > 32)
						{
							if (Main.itemTexture[Main.player[Main.myPlayer].inventory[num148].type].Width > Main.itemTexture[Main.player[Main.myPlayer].inventory[num148].type].Height)
							{
								num150 = 32f / (float)Main.itemTexture[Main.player[Main.myPlayer].inventory[num148].type].Width;
							}
							else
							{
								num150 = 32f / (float)Main.itemTexture[Main.player[Main.myPlayer].inventory[num148].type].Height;
							}
						}
						num150 *= Main.inventoryScale;
						this.spriteBatch.Draw(Main.itemTexture[Main.player[Main.myPlayer].inventory[num148].type], new Vector2((float)num146 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.player[Main.myPlayer].inventory[num148].type].Width * 0.5f * num150, (float)num147 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.player[Main.myPlayer].inventory[num148].type].Height * 0.5f * num150), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[Main.player[Main.myPlayer].inventory[num148].type].Width, Main.itemTexture[Main.player[Main.myPlayer].inventory[num148].type].Height)), Main.player[Main.myPlayer].inventory[num148].GetAlpha(white11), 0f, default(Vector2), num150, SpriteEffects.None, 0f);
						if (Main.player[Main.myPlayer].inventory[num148].color != default(Color))
						{
							this.spriteBatch.Draw(Main.itemTexture[Main.player[Main.myPlayer].inventory[num148].type], new Vector2((float)num146 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.player[Main.myPlayer].inventory[num148].type].Width * 0.5f * num150, (float)num147 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.player[Main.myPlayer].inventory[num148].type].Height * 0.5f * num150), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[Main.player[Main.myPlayer].inventory[num148].type].Width, Main.itemTexture[Main.player[Main.myPlayer].inventory[num148].type].Height)), Main.player[Main.myPlayer].inventory[num148].GetColor(white11), 0f, default(Vector2), num150, SpriteEffects.None, 0f);
						}
						if (Main.player[Main.myPlayer].inventory[num148].stack > 1)
						{
							this.spriteBatch.DrawString(Main.fontItemStack, string.Concat(Main.player[Main.myPlayer].inventory[num148].stack), new Vector2((float)num146 + 10f * Main.inventoryScale, (float)num147 + 26f * Main.inventoryScale), white11, 0f, default(Vector2), num150, SpriteEffects.None, 0f);
						}
					}
				}
				Vector2 vector11 = Main.fontMouseText.MeasureString("Ammo");
				Vector2 vector12 = Main.fontMouseText.MeasureString(Lang.inter[27]);
				float num151 = vector11.X / vector12.X;
				this.spriteBatch.DrawString(Main.fontMouseText, Lang.inter[27], new Vector2(532f, 84f + (vector11.Y - vector11.Y * num151) / 2f), new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), 0f, default(Vector2), 0.75f * num151, SpriteEffects.None, 0f);
				Main.inventoryScale = 0.6f;
				for (int num152 = 0; num152 < 4; num152++)
				{
					int num153 = 534;
					int num154 = (int)(85f + (float)(num152 * 56) * Main.inventoryScale + 20f);
					int num155 = 44 + num152;
					Color white12 = new Color(100, 100, 100, 100);
					if (Main.mouseX >= num153 && (float)Main.mouseX <= (float)num153 + (float)Main.inventoryBackTexture.Width * Main.inventoryScale && Main.mouseY >= num154 && (float)Main.mouseY <= (float)num154 + (float)Main.inventoryBackTexture.Height * Main.inventoryScale)
					{
						Main.player[Main.myPlayer].mouseInterface = true;
						if (Main.mouseLeftRelease && Main.mouseLeft)
						{
							if (Main.keyState.IsKeyDown(Keys.LeftShift))
							{
								if (Main.player[Main.myPlayer].inventory[num155].type > 0)
								{
									if (Main.npcShop > 0)
									{
										if (Main.player[Main.myPlayer].SellItem(Main.player[Main.myPlayer].inventory[num155].value, Main.player[Main.myPlayer].inventory[num155].stack))
										{
											this.shop[Main.npcShop].AddShop(Main.player[Main.myPlayer].inventory[num155]);
											Main.player[Main.myPlayer].inventory[num155].SetDefaults(0, false);
											Main.PlaySound(18, -1, -1, 1);
										}
										else
										{
											if (Main.player[Main.myPlayer].inventory[num155].value == 0)
											{
												this.shop[Main.npcShop].AddShop(Main.player[Main.myPlayer].inventory[num155]);
												Main.player[Main.myPlayer].inventory[num155].SetDefaults(0, false);
												Main.PlaySound(7, -1, -1, 1);
											}
										}
									}
									else
									{
										Recipe.FindRecipes();
										Main.PlaySound(7, -1, -1, 1);
										Main.trashItem = (Item)Main.player[Main.myPlayer].inventory[num155].Clone();
										Main.player[Main.myPlayer].inventory[num155].SetDefaults(0, false);
									}
								}
							}
							else
							{
								if ((Main.player[Main.myPlayer].selectedItem != num155 || Main.player[Main.myPlayer].itemAnimation <= 0) && (Main.mouseItem.type == 0 || Main.mouseItem.ammo > 0 || Main.mouseItem.type == 530))
								{
									Item item8 = Main.mouseItem;
									Main.mouseItem = Main.player[Main.myPlayer].inventory[num155];
									Main.player[Main.myPlayer].inventory[num155] = item8;
									if (Main.player[Main.myPlayer].inventory[num155].type == 0 || Main.player[Main.myPlayer].inventory[num155].stack < 1)
									{
										Main.player[Main.myPlayer].inventory[num155] = new Item();
									}
									if (Main.mouseItem.IsTheSameAs(Main.player[Main.myPlayer].inventory[num155]) && Main.player[Main.myPlayer].inventory[num155].stack != Main.player[Main.myPlayer].inventory[num155].maxStack && Main.mouseItem.stack != Main.mouseItem.maxStack)
									{
										if (Main.mouseItem.stack + Main.player[Main.myPlayer].inventory[num155].stack <= Main.mouseItem.maxStack)
										{
											Main.player[Main.myPlayer].inventory[num155].stack += Main.mouseItem.stack;
											Main.mouseItem.stack = 0;
										}
										else
										{
											int num156 = Main.mouseItem.maxStack - Main.player[Main.myPlayer].inventory[num155].stack;
											Main.player[Main.myPlayer].inventory[num155].stack += num156;
											Main.mouseItem.stack -= num156;
										}
									}
									if (Main.mouseItem.type == 0 || Main.mouseItem.stack < 1)
									{
										Main.mouseItem = new Item();
									}
									if (Main.mouseItem.type > 0 || Main.player[Main.myPlayer].inventory[num155].type > 0)
									{
										Main.PlaySound(7, -1, -1, 1);
									}
									Recipe.FindRecipes();
								}
							}
						}
						else
						{
							if (Main.stackSplit <= 1 && Main.mouseRight && (Main.mouseItem.IsTheSameAs(Main.player[Main.myPlayer].inventory[num155]) || Main.mouseItem.type == 0) && (Main.mouseItem.stack < Main.mouseItem.maxStack || Main.mouseItem.type == 0))
							{
								if (Main.mouseItem.type == 0)
								{
									Main.mouseItem = (Item)Main.player[Main.myPlayer].inventory[num155].Clone();
									Main.mouseItem.stack = 0;
								}
								Main.mouseItem.stack++;
								Main.player[Main.myPlayer].inventory[num155].stack--;
								if (Main.player[Main.myPlayer].inventory[num155].stack <= 0)
								{
									Main.player[Main.myPlayer].inventory[num155] = new Item();
								}
								Recipe.FindRecipes();
								Main.soundInstanceMenuTick.Stop();
								Main.soundInstanceMenuTick = Main.soundMenuTick.CreateInstance();
								Main.PlaySound(12, -1, -1, 1);
								if (Main.stackSplit == 0)
								{
									Main.stackSplit = 15;
								}
								else
								{
									Main.stackSplit = Main.stackDelay;
								}
							}
						}
						text15 = Main.player[Main.myPlayer].inventory[num155].name;
						Main.toolTip = (Item)Main.player[Main.myPlayer].inventory[num155].Clone();
						if (Main.player[Main.myPlayer].inventory[num155].stack > 1)
						{
							object obj = text15;
							text15 = string.Concat(new object[]
							{
								obj,
								" (",
								Main.player[Main.myPlayer].inventory[num155].stack,
								")"
							});
						}
					}
					this.spriteBatch.Draw(Main.inventoryBackTexture, new Vector2((float)num153, (float)num154), new Rectangle?(new Rectangle(0, 0, Main.inventoryBackTexture.Width, Main.inventoryBackTexture.Height)), color2, 0f, default(Vector2), Main.inventoryScale, SpriteEffects.None, 0f);
					white12 = Color.White;
					if (Main.player[Main.myPlayer].inventory[num155].type > 0 && Main.player[Main.myPlayer].inventory[num155].stack > 0)
					{
						float num157 = 1f;
						if (Main.itemTexture[Main.player[Main.myPlayer].inventory[num155].type].Width > 32 || Main.itemTexture[Main.player[Main.myPlayer].inventory[num155].type].Height > 32)
						{
							if (Main.itemTexture[Main.player[Main.myPlayer].inventory[num155].type].Width > Main.itemTexture[Main.player[Main.myPlayer].inventory[num155].type].Height)
							{
								num157 = 32f / (float)Main.itemTexture[Main.player[Main.myPlayer].inventory[num155].type].Width;
							}
							else
							{
								num157 = 32f / (float)Main.itemTexture[Main.player[Main.myPlayer].inventory[num155].type].Height;
							}
						}
						num157 *= Main.inventoryScale;
						this.spriteBatch.Draw(Main.itemTexture[Main.player[Main.myPlayer].inventory[num155].type], new Vector2((float)num153 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.player[Main.myPlayer].inventory[num155].type].Width * 0.5f * num157, (float)num154 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.player[Main.myPlayer].inventory[num155].type].Height * 0.5f * num157), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[Main.player[Main.myPlayer].inventory[num155].type].Width, Main.itemTexture[Main.player[Main.myPlayer].inventory[num155].type].Height)), Main.player[Main.myPlayer].inventory[num155].GetAlpha(white12), 0f, default(Vector2), num157, SpriteEffects.None, 0f);
						if (Main.player[Main.myPlayer].inventory[num155].color != default(Color))
						{
							this.spriteBatch.Draw(Main.itemTexture[Main.player[Main.myPlayer].inventory[num155].type], new Vector2((float)num153 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.player[Main.myPlayer].inventory[num155].type].Width * 0.5f * num157, (float)num154 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.player[Main.myPlayer].inventory[num155].type].Height * 0.5f * num157), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[Main.player[Main.myPlayer].inventory[num155].type].Width, Main.itemTexture[Main.player[Main.myPlayer].inventory[num155].type].Height)), Main.player[Main.myPlayer].inventory[num155].GetColor(white12), 0f, default(Vector2), num157, SpriteEffects.None, 0f);
						}
						if (Main.player[Main.myPlayer].inventory[num155].stack > 1)
						{
							this.spriteBatch.DrawString(Main.fontItemStack, string.Concat(Main.player[Main.myPlayer].inventory[num155].stack), new Vector2((float)num153 + 10f * Main.inventoryScale, (float)num154 + 26f * Main.inventoryScale), white12, 0f, default(Vector2), num157, SpriteEffects.None, 0f);
						}
					}
				}
				if (Main.npcShop > 0 && (!Main.playerInventory || Main.player[Main.myPlayer].talkNPC == -1))
				{
					Main.npcShop = 0;
				}
				if (Main.npcShop > 0)
				{
					this.spriteBatch.DrawString(Main.fontMouseText, Lang.inter[28], new Vector2(284f, 210f), new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
					Main.inventoryScale = 0.75f;
					if (Main.mouseX > 73 && Main.mouseX < (int)(73f + 280f * Main.inventoryScale) && Main.mouseY > 210 && Main.mouseY < (int)(210f + 224f * Main.inventoryScale))
					{
						Main.player[Main.myPlayer].mouseInterface = true;
					}
					for (int num158 = 0; num158 < 5; num158++)
					{
						for (int num159 = 0; num159 < 4; num159++)
						{
							int num160 = (int)(73f + (float)(num158 * 56) * Main.inventoryScale);
							int num161 = (int)(210f + (float)(num159 * 56) * Main.inventoryScale);
							int num162 = num158 + num159 * 5;
							Color white13 = new Color(100, 100, 100, 100);
							if (Main.mouseX >= num160 && (float)Main.mouseX <= (float)num160 + (float)Main.inventoryBackTexture.Width * Main.inventoryScale && Main.mouseY >= num161 && (float)Main.mouseY <= (float)num161 + (float)Main.inventoryBackTexture.Height * Main.inventoryScale)
							{
								Main.player[Main.myPlayer].mouseInterface = true;
								if (Main.mouseLeftRelease && Main.mouseLeft)
								{
									if (Main.mouseItem.type == 0)
									{
										if ((Main.player[Main.myPlayer].selectedItem != num162 || Main.player[Main.myPlayer].itemAnimation <= 0) && Main.player[Main.myPlayer].BuyItem(this.shop[Main.npcShop].item[num162].value))
										{
											if (this.shop[Main.npcShop].item[num162].buyOnce)
											{
												int prefix = (int)this.shop[Main.npcShop].item[num162].prefix;
												Main.mouseItem.netDefaults(this.shop[Main.npcShop].item[num162].netID);
												Main.mouseItem.Prefix(prefix);
											}
											else
											{
												Main.mouseItem.netDefaults(this.shop[Main.npcShop].item[num162].netID);
												Main.mouseItem.Prefix(-1);
											}
											Main.mouseItem.position.X = Main.player[Main.myPlayer].position.X + (float)(Main.player[Main.myPlayer].width / 2) - (float)(Main.mouseItem.width / 2);
											Main.mouseItem.position.Y = Main.player[Main.myPlayer].position.Y + (float)(Main.player[Main.myPlayer].height / 2) - (float)(Main.mouseItem.height / 2);
											ItemText.NewText(Main.mouseItem, Main.mouseItem.stack);
											if (this.shop[Main.npcShop].item[num162].buyOnce)
											{
												this.shop[Main.npcShop].item[num162].stack--;
												if (this.shop[Main.npcShop].item[num162].stack <= 0)
												{
													this.shop[Main.npcShop].item[num162].SetDefaults(0, false);
												}
											}
											Main.PlaySound(18, -1, -1, 1);
										}
									}
									else
									{
										if (this.shop[Main.npcShop].item[num162].type == 0)
										{
											if (Main.player[Main.myPlayer].SellItem(Main.mouseItem.value, Main.mouseItem.stack))
											{
												this.shop[Main.npcShop].AddShop(Main.mouseItem);
												Main.mouseItem.stack = 0;
												Main.mouseItem.type = 0;
												Main.PlaySound(18, -1, -1, 1);
											}
											else
											{
												if (Main.mouseItem.value == 0)
												{
													this.shop[Main.npcShop].AddShop(Main.mouseItem);
													Main.mouseItem.stack = 0;
													Main.mouseItem.type = 0;
													Main.PlaySound(7, -1, -1, 1);
												}
											}
										}
									}
								}
								else
								{
									if (Main.stackSplit <= 1 && Main.mouseRight && (Main.mouseItem.IsTheSameAs(this.shop[Main.npcShop].item[num162]) || Main.mouseItem.type == 0) && (Main.mouseItem.stack < Main.mouseItem.maxStack || Main.mouseItem.type == 0) && Main.player[Main.myPlayer].BuyItem(this.shop[Main.npcShop].item[num162].value))
									{
										Main.PlaySound(18, -1, -1, 1);
										if (Main.mouseItem.type == 0)
										{
											Main.mouseItem.netDefaults(this.shop[Main.npcShop].item[num162].netID);
											Main.mouseItem.stack = 0;
										}
										Main.mouseItem.stack++;
										if (Main.stackSplit == 0)
										{
											Main.stackSplit = 15;
										}
										else
										{
											Main.stackSplit = Main.stackDelay;
										}
										if (this.shop[Main.npcShop].item[num162].buyOnce)
										{
											this.shop[Main.npcShop].item[num162].stack--;
											if (this.shop[Main.npcShop].item[num162].stack <= 0)
											{
												this.shop[Main.npcShop].item[num162].SetDefaults(0, false);
											}
										}
									}
								}
								text15 = this.shop[Main.npcShop].item[num162].name;
								Main.toolTip = (Item)this.shop[Main.npcShop].item[num162].Clone();
								Main.toolTip.buy = true;
								if (this.shop[Main.npcShop].item[num162].stack > 1)
								{
									object obj = text15;
									text15 = string.Concat(new object[]
									{
										obj,
										" (",
										this.shop[Main.npcShop].item[num162].stack,
										")"
									});
								}
							}
							this.spriteBatch.Draw(Main.inventoryBack6Texture, new Vector2((float)num160, (float)num161), new Rectangle?(new Rectangle(0, 0, Main.inventoryBackTexture.Width, Main.inventoryBackTexture.Height)), color2, 0f, default(Vector2), Main.inventoryScale, SpriteEffects.None, 0f);
							white13 = Color.White;
							if (this.shop[Main.npcShop].item[num162].type > 0 && this.shop[Main.npcShop].item[num162].stack > 0)
							{
								float num163 = 1f;
								if (Main.itemTexture[this.shop[Main.npcShop].item[num162].type].Width > 32 || Main.itemTexture[this.shop[Main.npcShop].item[num162].type].Height > 32)
								{
									if (Main.itemTexture[this.shop[Main.npcShop].item[num162].type].Width > Main.itemTexture[this.shop[Main.npcShop].item[num162].type].Height)
									{
										num163 = 32f / (float)Main.itemTexture[this.shop[Main.npcShop].item[num162].type].Width;
									}
									else
									{
										num163 = 32f / (float)Main.itemTexture[this.shop[Main.npcShop].item[num162].type].Height;
									}
								}
								num163 *= Main.inventoryScale;
								this.spriteBatch.Draw(Main.itemTexture[this.shop[Main.npcShop].item[num162].type], new Vector2((float)num160 + 26f * Main.inventoryScale - (float)Main.itemTexture[this.shop[Main.npcShop].item[num162].type].Width * 0.5f * num163, (float)num161 + 26f * Main.inventoryScale - (float)Main.itemTexture[this.shop[Main.npcShop].item[num162].type].Height * 0.5f * num163), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[this.shop[Main.npcShop].item[num162].type].Width, Main.itemTexture[this.shop[Main.npcShop].item[num162].type].Height)), this.shop[Main.npcShop].item[num162].GetAlpha(white13), 0f, default(Vector2), num163, SpriteEffects.None, 0f);
								if (this.shop[Main.npcShop].item[num162].color != default(Color))
								{
									this.spriteBatch.Draw(Main.itemTexture[this.shop[Main.npcShop].item[num162].type], new Vector2((float)num160 + 26f * Main.inventoryScale - (float)Main.itemTexture[this.shop[Main.npcShop].item[num162].type].Width * 0.5f * num163, (float)num161 + 26f * Main.inventoryScale - (float)Main.itemTexture[this.shop[Main.npcShop].item[num162].type].Height * 0.5f * num163), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[this.shop[Main.npcShop].item[num162].type].Width, Main.itemTexture[this.shop[Main.npcShop].item[num162].type].Height)), this.shop[Main.npcShop].item[num162].GetColor(white13), 0f, default(Vector2), num163, SpriteEffects.None, 0f);
								}
								if (this.shop[Main.npcShop].item[num162].stack > 1)
								{
									this.spriteBatch.DrawString(Main.fontItemStack, string.Concat(this.shop[Main.npcShop].item[num162].stack), new Vector2((float)num160 + 10f * Main.inventoryScale, (float)num161 + 26f * Main.inventoryScale), white13, 0f, default(Vector2), num163, SpriteEffects.None, 0f);
								}
							}
						}
					}
				}
				if (Main.player[Main.myPlayer].chest > -1 && Main.tile[Main.player[Main.myPlayer].chestX, Main.player[Main.myPlayer].chestY].type != 21)
				{
					Main.player[Main.myPlayer].chest = -1;
				}
				if (Main.player[Main.myPlayer].chest != -1)
				{
					Main.inventoryScale = 0.75f;
					if (Main.mouseX > 73 && Main.mouseX < (int)(73f + 280f * Main.inventoryScale) && Main.mouseY > 210 && Main.mouseY < (int)(210f + 224f * Main.inventoryScale))
					{
						Main.player[Main.myPlayer].mouseInterface = true;
					}
					for (int num164 = 0; num164 < 3; num164++)
					{
						int num165 = 286;
						int num166 = 250;
						float num167 = this.chestLootScale;
						string text23 = Lang.inter[29];
						if (num164 == 1)
						{
							num166 += 26;
							num167 = this.chestDepositScale;
							text23 = Lang.inter[30];
						}
						else
						{
							if (num164 == 2)
							{
								num166 += 52;
								num167 = this.chestStackScale;
								text23 = Lang.inter[31];
							}
						}
						Vector2 vector13 = Main.fontMouseText.MeasureString(text23) / 2f;
						Color color8 = new Color((int)((byte)((float)Main.mouseTextColor * num167)), (int)((byte)((float)Main.mouseTextColor * num167)), (int)((byte)((float)Main.mouseTextColor * num167)), (int)((byte)((float)Main.mouseTextColor * num167)));
						num165 += (int)(vector13.X * num167);
						this.spriteBatch.DrawString(Main.fontMouseText, text23, new Vector2((float)num165, (float)num166), color8, 0f, vector13, num167, SpriteEffects.None, 0f);
						vector13 *= num167;
						if ((float)Main.mouseX > (float)num165 - vector13.X && (float)Main.mouseX < (float)num165 + vector13.X && (float)Main.mouseY > (float)num166 - vector13.Y && (float)Main.mouseY < (float)num166 + vector13.Y)
						{
							if (num164 == 0)
							{
								if (!this.chestLootHover)
								{
									Main.PlaySound(12, -1, -1, 1);
								}
								this.chestLootHover = true;
							}
							else
							{
								if (num164 == 1)
								{
									if (!this.chestDepositHover)
									{
										Main.PlaySound(12, -1, -1, 1);
									}
									this.chestDepositHover = true;
								}
								else
								{
									if (!this.chestStackHover)
									{
										Main.PlaySound(12, -1, -1, 1);
									}
									this.chestStackHover = true;
								}
							}
							Main.player[Main.myPlayer].mouseInterface = true;
							num167 += 0.05f;
							if (Main.mouseLeft && Main.mouseLeftRelease)
							{
								if (num164 == 0)
								{
									if (Main.player[Main.myPlayer].chest > -1)
									{
										for (int num168 = 0; num168 < 20; num168++)
										{
											if (Main.chest[Main.player[Main.myPlayer].chest].item[num168].type > 0)
											{
												Main.chest[Main.player[Main.myPlayer].chest].item[num168] = Main.player[Main.myPlayer].GetItem(Main.myPlayer, Main.chest[Main.player[Main.myPlayer].chest].item[num168]);
												if (Main.netMode == 1)
												{
													NetMessage.SendData(32, -1, -1, "", Main.player[Main.myPlayer].chest, (float)num168, 0f, 0f, 0);
												}
											}
										}
									}
									else
									{
										if (Main.player[Main.myPlayer].chest == -3)
										{
											for (int num169 = 0; num169 < 20; num169++)
											{
												if (Main.player[Main.myPlayer].bank2[num169].type > 0)
												{
													Main.player[Main.myPlayer].bank2[num169] = Main.player[Main.myPlayer].GetItem(Main.myPlayer, Main.player[Main.myPlayer].bank2[num169]);
												}
											}
										}
										else
										{
											for (int num170 = 0; num170 < 20; num170++)
											{
												if (Main.player[Main.myPlayer].bank[num170].type > 0)
												{
													Main.player[Main.myPlayer].bank[num170] = Main.player[Main.myPlayer].GetItem(Main.myPlayer, Main.player[Main.myPlayer].bank[num170]);
												}
											}
										}
									}
								}
								else
								{
									if (num164 == 1)
									{
										for (int num171 = 40; num171 >= 10; num171--)
										{
											if (Main.player[Main.myPlayer].inventory[num171].stack > 0 && Main.player[Main.myPlayer].inventory[num171].type > 0)
											{
												if (Main.player[Main.myPlayer].inventory[num171].maxStack > 1)
												{
													for (int num172 = 0; num172 < 20; num172++)
													{
														if (Main.player[Main.myPlayer].chest > -1)
														{
															if (Main.chest[Main.player[Main.myPlayer].chest].item[num172].stack < Main.chest[Main.player[Main.myPlayer].chest].item[num172].maxStack && Main.player[Main.myPlayer].inventory[num171].IsTheSameAs(Main.chest[Main.player[Main.myPlayer].chest].item[num172]))
															{
																int num173 = Main.player[Main.myPlayer].inventory[num171].stack;
																if (Main.player[Main.myPlayer].inventory[num171].stack + Main.chest[Main.player[Main.myPlayer].chest].item[num172].stack > Main.chest[Main.player[Main.myPlayer].chest].item[num172].maxStack)
																{
																	num173 = Main.chest[Main.player[Main.myPlayer].chest].item[num172].maxStack - Main.chest[Main.player[Main.myPlayer].chest].item[num172].stack;
																}
																Main.player[Main.myPlayer].inventory[num171].stack -= num173;
																Main.chest[Main.player[Main.myPlayer].chest].item[num172].stack += num173;
																Main.ChestCoins();
																Main.PlaySound(7, -1, -1, 1);
																if (Main.player[Main.myPlayer].inventory[num171].stack <= 0)
																{
																	Main.player[Main.myPlayer].inventory[num171].SetDefaults(0, false);
																	if (Main.netMode == 1)
																	{
																		NetMessage.SendData(32, -1, -1, "", Main.player[Main.myPlayer].chest, (float)num172, 0f, 0f, 0);
																		break;
																	}
																	break;
																}
																else
																{
																	if (Main.chest[Main.player[Main.myPlayer].chest].item[num172].type == 0)
																	{
																		Main.chest[Main.player[Main.myPlayer].chest].item[num172] = (Item)Main.player[Main.myPlayer].inventory[num171].Clone();
																		Main.player[Main.myPlayer].inventory[num171].SetDefaults(0, false);
																	}
																	if (Main.netMode == 1)
																	{
																		NetMessage.SendData(32, -1, -1, "", Main.player[Main.myPlayer].chest, (float)num172, 0f, 0f, 0);
																	}
																}
															}
														}
														else
														{
															if (Main.player[Main.myPlayer].chest == -3)
															{
																if (Main.player[Main.myPlayer].bank2[num172].stack < Main.player[Main.myPlayer].bank2[num172].maxStack && Main.player[Main.myPlayer].inventory[num171].IsTheSameAs(Main.player[Main.myPlayer].bank2[num172]))
																{
																	int num174 = Main.player[Main.myPlayer].inventory[num171].stack;
																	if (Main.player[Main.myPlayer].inventory[num171].stack + Main.player[Main.myPlayer].bank2[num172].stack > Main.player[Main.myPlayer].bank2[num172].maxStack)
																	{
																		num174 = Main.player[Main.myPlayer].bank2[num172].maxStack - Main.player[Main.myPlayer].bank2[num172].stack;
																	}
																	Main.player[Main.myPlayer].inventory[num171].stack -= num174;
																	Main.player[Main.myPlayer].bank2[num172].stack += num174;
																	Main.PlaySound(7, -1, -1, 1);
																	Main.BankCoins();
																	if (Main.player[Main.myPlayer].inventory[num171].stack <= 0)
																	{
																		Main.player[Main.myPlayer].inventory[num171].SetDefaults(0, false);
																		break;
																	}
																	if (Main.player[Main.myPlayer].bank2[num172].type == 0)
																	{
																		Main.player[Main.myPlayer].bank2[num172] = (Item)Main.player[Main.myPlayer].inventory[num171].Clone();
																		Main.player[Main.myPlayer].inventory[num171].SetDefaults(0, false);
																	}
																}
															}
															else
															{
																if (Main.player[Main.myPlayer].bank[num172].stack < Main.player[Main.myPlayer].bank[num172].maxStack && Main.player[Main.myPlayer].inventory[num171].IsTheSameAs(Main.player[Main.myPlayer].bank[num172]))
																{
																	int num175 = Main.player[Main.myPlayer].inventory[num171].stack;
																	if (Main.player[Main.myPlayer].inventory[num171].stack + Main.player[Main.myPlayer].bank[num172].stack > Main.player[Main.myPlayer].bank[num172].maxStack)
																	{
																		num175 = Main.player[Main.myPlayer].bank[num172].maxStack - Main.player[Main.myPlayer].bank[num172].stack;
																	}
																	Main.player[Main.myPlayer].inventory[num171].stack -= num175;
																	Main.player[Main.myPlayer].bank[num172].stack += num175;
																	Main.PlaySound(7, -1, -1, 1);
																	Main.BankCoins();
																	if (Main.player[Main.myPlayer].inventory[num171].stack <= 0)
																	{
																		Main.player[Main.myPlayer].inventory[num171].SetDefaults(0, false);
																		break;
																	}
																	if (Main.player[Main.myPlayer].bank[num172].type == 0)
																	{
																		Main.player[Main.myPlayer].bank[num172] = (Item)Main.player[Main.myPlayer].inventory[num171].Clone();
																		Main.player[Main.myPlayer].inventory[num171].SetDefaults(0, false);
																	}
																}
															}
														}
													}
												}
												if (Main.player[Main.myPlayer].inventory[num171].stack > 0)
												{
													if (Main.player[Main.myPlayer].chest > -1)
													{
														int num176 = 0;
														while (num176 < 20)
														{
															if (Main.chest[Main.player[Main.myPlayer].chest].item[num176].stack == 0)
															{
																Main.PlaySound(7, -1, -1, 1);
																Main.chest[Main.player[Main.myPlayer].chest].item[num176] = (Item)Main.player[Main.myPlayer].inventory[num171].Clone();
																Main.player[Main.myPlayer].inventory[num171].SetDefaults(0, false);
																if (Main.netMode == 1)
																{
																	NetMessage.SendData(32, -1, -1, "", Main.player[Main.myPlayer].chest, (float)num176, 0f, 0f, 0);
																	break;
																}
																break;
															}
															else
															{
																num176++;
															}
														}
													}
													else
													{
														if (Main.player[Main.myPlayer].chest == -3)
														{
															for (int num177 = 0; num177 < 20; num177++)
															{
																if (Main.player[Main.myPlayer].bank2[num177].stack == 0)
																{
																	Main.PlaySound(7, -1, -1, 1);
																	Main.player[Main.myPlayer].bank2[num177] = (Item)Main.player[Main.myPlayer].inventory[num171].Clone();
																	Main.player[Main.myPlayer].inventory[num171].SetDefaults(0, false);
																	break;
																}
															}
														}
														else
														{
															for (int num178 = 0; num178 < 20; num178++)
															{
																if (Main.player[Main.myPlayer].bank[num178].stack == 0)
																{
																	Main.PlaySound(7, -1, -1, 1);
																	Main.player[Main.myPlayer].bank[num178] = (Item)Main.player[Main.myPlayer].inventory[num171].Clone();
																	Main.player[Main.myPlayer].inventory[num171].SetDefaults(0, false);
																	break;
																}
															}
														}
													}
												}
											}
										}
									}
									else
									{
										if (Main.player[Main.myPlayer].chest > -1)
										{
											for (int num179 = 0; num179 < 20; num179++)
											{
												if (Main.chest[Main.player[Main.myPlayer].chest].item[num179].type > 0 && Main.chest[Main.player[Main.myPlayer].chest].item[num179].stack < Main.chest[Main.player[Main.myPlayer].chest].item[num179].maxStack)
												{
													for (int num180 = 0; num180 < 48; num180++)
													{
														if (Main.chest[Main.player[Main.myPlayer].chest].item[num179].IsTheSameAs(Main.player[Main.myPlayer].inventory[num180]))
														{
															int num181 = Main.player[Main.myPlayer].inventory[num180].stack;
															if (Main.chest[Main.player[Main.myPlayer].chest].item[num179].stack + num181 > Main.chest[Main.player[Main.myPlayer].chest].item[num179].maxStack)
															{
																num181 = Main.chest[Main.player[Main.myPlayer].chest].item[num179].maxStack - Main.chest[Main.player[Main.myPlayer].chest].item[num179].stack;
															}
															Main.PlaySound(7, -1, -1, 1);
															Main.chest[Main.player[Main.myPlayer].chest].item[num179].stack += num181;
															Main.player[Main.myPlayer].inventory[num180].stack -= num181;
															Main.ChestCoins();
															if (Main.player[Main.myPlayer].inventory[num180].stack == 0)
															{
																Main.player[Main.myPlayer].inventory[num180].SetDefaults(0, false);
															}
															else
															{
																if (Main.chest[Main.player[Main.myPlayer].chest].item[num179].type == 0)
																{
																	Main.chest[Main.player[Main.myPlayer].chest].item[num179] = (Item)Main.player[Main.myPlayer].inventory[num180].Clone();
																	Main.player[Main.myPlayer].inventory[num180].SetDefaults(0, false);
																}
															}
															if (Main.netMode == 1)
															{
																NetMessage.SendData(32, -1, -1, "", Main.player[Main.myPlayer].chest, (float)num179, 0f, 0f, 0);
															}
														}
													}
												}
											}
										}
										else
										{
											if (Main.player[Main.myPlayer].chest == -3)
											{
												for (int num182 = 0; num182 < 20; num182++)
												{
													if (Main.player[Main.myPlayer].bank2[num182].type > 0 && Main.player[Main.myPlayer].bank2[num182].stack < Main.player[Main.myPlayer].bank2[num182].maxStack)
													{
														for (int num183 = 0; num183 < 48; num183++)
														{
															if (Main.player[Main.myPlayer].bank2[num182].IsTheSameAs(Main.player[Main.myPlayer].inventory[num183]))
															{
																int num184 = Main.player[Main.myPlayer].inventory[num183].stack;
																if (Main.player[Main.myPlayer].bank2[num182].stack + num184 > Main.player[Main.myPlayer].bank2[num182].maxStack)
																{
																	num184 = Main.player[Main.myPlayer].bank2[num182].maxStack - Main.player[Main.myPlayer].bank2[num182].stack;
																}
																Main.PlaySound(7, -1, -1, 1);
																Main.player[Main.myPlayer].bank2[num182].stack += num184;
																Main.player[Main.myPlayer].inventory[num183].stack -= num184;
																Main.BankCoins();
																if (Main.player[Main.myPlayer].inventory[num183].stack == 0)
																{
																	Main.player[Main.myPlayer].inventory[num183].SetDefaults(0, false);
																}
																else
																{
																	if (Main.player[Main.myPlayer].bank2[num182].type == 0)
																	{
																		Main.player[Main.myPlayer].bank2[num182] = (Item)Main.player[Main.myPlayer].inventory[num183].Clone();
																		Main.player[Main.myPlayer].inventory[num183].SetDefaults(0, false);
																	}
																}
															}
														}
													}
												}
											}
											else
											{
												for (int num185 = 0; num185 < 20; num185++)
												{
													if (Main.player[Main.myPlayer].bank[num185].type > 0 && Main.player[Main.myPlayer].bank[num185].stack < Main.player[Main.myPlayer].bank[num185].maxStack)
													{
														for (int num186 = 0; num186 < 48; num186++)
														{
															if (Main.player[Main.myPlayer].bank[num185].IsTheSameAs(Main.player[Main.myPlayer].inventory[num186]))
															{
																int num187 = Main.player[Main.myPlayer].inventory[num186].stack;
																if (Main.player[Main.myPlayer].bank[num185].stack + num187 > Main.player[Main.myPlayer].bank[num185].maxStack)
																{
																	num187 = Main.player[Main.myPlayer].bank[num185].maxStack - Main.player[Main.myPlayer].bank[num185].stack;
																}
																Main.PlaySound(7, -1, -1, 1);
																Main.player[Main.myPlayer].bank[num185].stack += num187;
																Main.player[Main.myPlayer].inventory[num186].stack -= num187;
																Main.BankCoins();
																if (Main.player[Main.myPlayer].inventory[num186].stack == 0)
																{
																	Main.player[Main.myPlayer].inventory[num186].SetDefaults(0, false);
																}
																else
																{
																	if (Main.player[Main.myPlayer].bank[num185].type == 0)
																	{
																		Main.player[Main.myPlayer].bank[num185] = (Item)Main.player[Main.myPlayer].inventory[num186].Clone();
																		Main.player[Main.myPlayer].inventory[num186].SetDefaults(0, false);
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
								Recipe.FindRecipes();
							}
						}
						else
						{
							num167 -= 0.05f;
							if (num164 == 0)
							{
								this.chestLootHover = false;
							}
							else
							{
								if (num164 == 1)
								{
									this.chestDepositHover = false;
								}
								else
								{
									this.chestStackHover = false;
								}
							}
						}
						if ((double)num167 < 0.75)
						{
							num167 = 0.75f;
						}
						if (num167 > 1f)
						{
							num167 = 1f;
						}
						if (num164 == 0)
						{
							this.chestLootScale = num167;
						}
						else
						{
							if (num164 == 1)
							{
								this.chestDepositScale = num167;
							}
							else
							{
								this.chestStackScale = num167;
							}
						}
					}
				}
				else
				{
					this.chestLootScale = 0.75f;
					this.chestDepositScale = 0.75f;
					this.chestStackScale = 0.75f;
					this.chestLootHover = false;
					this.chestDepositHover = false;
					this.chestStackHover = false;
				}
				if (Main.player[Main.myPlayer].chest > -1)
				{
					this.spriteBatch.DrawString(Main.fontMouseText, Main.chestText, new Vector2(284f, 210f), new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
					Main.inventoryScale = 0.75f;
					if (Main.mouseX > 73 && Main.mouseX < (int)(73f + 280f * Main.inventoryScale) && Main.mouseY > 210 && Main.mouseY < (int)(210f + 224f * Main.inventoryScale))
					{
						Main.player[Main.myPlayer].mouseInterface = true;
					}
					for (int num188 = 0; num188 < 5; num188++)
					{
						for (int num189 = 0; num189 < 4; num189++)
						{
							int num190 = (int)(73f + (float)(num188 * 56) * Main.inventoryScale);
							int num191 = (int)(210f + (float)(num189 * 56) * Main.inventoryScale);
							int num192 = num188 + num189 * 5;
							Color white14 = new Color(100, 100, 100, 100);
							if (Main.mouseX >= num190 && (float)Main.mouseX <= (float)num190 + (float)Main.inventoryBackTexture.Width * Main.inventoryScale && Main.mouseY >= num191 && (float)Main.mouseY <= (float)num191 + (float)Main.inventoryBackTexture.Height * Main.inventoryScale)
							{
								Main.player[Main.myPlayer].mouseInterface = true;
								if (Main.mouseLeftRelease && Main.mouseLeft)
								{
									if (Main.player[Main.myPlayer].selectedItem != num192 || Main.player[Main.myPlayer].itemAnimation <= 0)
									{
										Item item9 = Main.mouseItem;
										Main.mouseItem = Main.chest[Main.player[Main.myPlayer].chest].item[num192];
										Main.chest[Main.player[Main.myPlayer].chest].item[num192] = item9;
										if (Main.chest[Main.player[Main.myPlayer].chest].item[num192].type == 0 || Main.chest[Main.player[Main.myPlayer].chest].item[num192].stack < 1)
										{
											Main.chest[Main.player[Main.myPlayer].chest].item[num192] = new Item();
										}
										if (Main.mouseItem.IsTheSameAs(Main.chest[Main.player[Main.myPlayer].chest].item[num192]) && Main.chest[Main.player[Main.myPlayer].chest].item[num192].stack != Main.chest[Main.player[Main.myPlayer].chest].item[num192].maxStack && Main.mouseItem.stack != Main.mouseItem.maxStack)
										{
											if (Main.mouseItem.stack + Main.chest[Main.player[Main.myPlayer].chest].item[num192].stack <= Main.mouseItem.maxStack)
											{
												Main.chest[Main.player[Main.myPlayer].chest].item[num192].stack += Main.mouseItem.stack;
												Main.mouseItem.stack = 0;
											}
											else
											{
												int num193 = Main.mouseItem.maxStack - Main.chest[Main.player[Main.myPlayer].chest].item[num192].stack;
												Main.chest[Main.player[Main.myPlayer].chest].item[num192].stack += num193;
												Main.mouseItem.stack -= num193;
											}
										}
										if (Main.mouseItem.type == 0 || Main.mouseItem.stack < 1)
										{
											Main.mouseItem = new Item();
										}
										if (Main.mouseItem.type > 0 || Main.chest[Main.player[Main.myPlayer].chest].item[num192].type > 0)
										{
											Recipe.FindRecipes();
											Main.PlaySound(7, -1, -1, 1);
										}
										if (Main.netMode == 1)
										{
											NetMessage.SendData(32, -1, -1, "", Main.player[Main.myPlayer].chest, (float)num192, 0f, 0f, 0);
										}
									}
								}
								else
								{
									if (Main.mouseRight && Main.mouseRightRelease && Main.chest[Main.player[Main.myPlayer].chest].item[num192].maxStack == 1)
									{
										Main.chest[Main.player[Main.myPlayer].chest].item[num192] = Main.armorSwap(Main.chest[Main.player[Main.myPlayer].chest].item[num192]);
										if (Main.netMode == 1)
										{
											NetMessage.SendData(32, -1, -1, "", Main.player[Main.myPlayer].chest, (float)num192, 0f, 0f, 0);
										}
									}
									else
									{
										if (Main.stackSplit <= 1 && Main.mouseRight && Main.chest[Main.player[Main.myPlayer].chest].item[num192].maxStack > 1 && (Main.mouseItem.IsTheSameAs(Main.chest[Main.player[Main.myPlayer].chest].item[num192]) || Main.mouseItem.type == 0) && (Main.mouseItem.stack < Main.mouseItem.maxStack || Main.mouseItem.type == 0))
										{
											if (Main.mouseItem.type == 0)
											{
												Main.mouseItem = (Item)Main.chest[Main.player[Main.myPlayer].chest].item[num192].Clone();
												Main.mouseItem.stack = 0;
											}
											Main.mouseItem.stack++;
											Main.chest[Main.player[Main.myPlayer].chest].item[num192].stack--;
											if (Main.chest[Main.player[Main.myPlayer].chest].item[num192].stack <= 0)
											{
												Main.chest[Main.player[Main.myPlayer].chest].item[num192] = new Item();
											}
											Recipe.FindRecipes();
											Main.soundInstanceMenuTick.Stop();
											Main.soundInstanceMenuTick = Main.soundMenuTick.CreateInstance();
											Main.PlaySound(12, -1, -1, 1);
											if (Main.stackSplit == 0)
											{
												Main.stackSplit = 15;
											}
											else
											{
												Main.stackSplit = Main.stackDelay;
											}
											if (Main.netMode == 1)
											{
												NetMessage.SendData(32, -1, -1, "", Main.player[Main.myPlayer].chest, (float)num192, 0f, 0f, 0);
											}
										}
									}
								}
								text15 = Main.chest[Main.player[Main.myPlayer].chest].item[num192].name;
								Main.toolTip = (Item)Main.chest[Main.player[Main.myPlayer].chest].item[num192].Clone();
								if (Main.chest[Main.player[Main.myPlayer].chest].item[num192].stack > 1)
								{
									object obj = text15;
									text15 = string.Concat(new object[]
									{
										obj,
										" (",
										Main.chest[Main.player[Main.myPlayer].chest].item[num192].stack,
										")"
									});
								}
							}
							this.spriteBatch.Draw(Main.inventoryBack5Texture, new Vector2((float)num190, (float)num191), new Rectangle?(new Rectangle(0, 0, Main.inventoryBackTexture.Width, Main.inventoryBackTexture.Height)), color2, 0f, default(Vector2), Main.inventoryScale, SpriteEffects.None, 0f);
							white14 = Color.White;
							if (Main.chest[Main.player[Main.myPlayer].chest].item[num192].type > 0 && Main.chest[Main.player[Main.myPlayer].chest].item[num192].stack > 0)
							{
								float num194 = 1f;
								if (Main.itemTexture[Main.chest[Main.player[Main.myPlayer].chest].item[num192].type].Width > 32 || Main.itemTexture[Main.chest[Main.player[Main.myPlayer].chest].item[num192].type].Height > 32)
								{
									if (Main.itemTexture[Main.chest[Main.player[Main.myPlayer].chest].item[num192].type].Width > Main.itemTexture[Main.chest[Main.player[Main.myPlayer].chest].item[num192].type].Height)
									{
										num194 = 32f / (float)Main.itemTexture[Main.chest[Main.player[Main.myPlayer].chest].item[num192].type].Width;
									}
									else
									{
										num194 = 32f / (float)Main.itemTexture[Main.chest[Main.player[Main.myPlayer].chest].item[num192].type].Height;
									}
								}
								num194 *= Main.inventoryScale;
								this.spriteBatch.Draw(Main.itemTexture[Main.chest[Main.player[Main.myPlayer].chest].item[num192].type], new Vector2((float)num190 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.chest[Main.player[Main.myPlayer].chest].item[num192].type].Width * 0.5f * num194, (float)num191 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.chest[Main.player[Main.myPlayer].chest].item[num192].type].Height * 0.5f * num194), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[Main.chest[Main.player[Main.myPlayer].chest].item[num192].type].Width, Main.itemTexture[Main.chest[Main.player[Main.myPlayer].chest].item[num192].type].Height)), Main.chest[Main.player[Main.myPlayer].chest].item[num192].GetAlpha(white14), 0f, default(Vector2), num194, SpriteEffects.None, 0f);
								if (Main.chest[Main.player[Main.myPlayer].chest].item[num192].color != default(Color))
								{
									this.spriteBatch.Draw(Main.itemTexture[Main.chest[Main.player[Main.myPlayer].chest].item[num192].type], new Vector2((float)num190 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.chest[Main.player[Main.myPlayer].chest].item[num192].type].Width * 0.5f * num194, (float)num191 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.chest[Main.player[Main.myPlayer].chest].item[num192].type].Height * 0.5f * num194), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[Main.chest[Main.player[Main.myPlayer].chest].item[num192].type].Width, Main.itemTexture[Main.chest[Main.player[Main.myPlayer].chest].item[num192].type].Height)), Main.chest[Main.player[Main.myPlayer].chest].item[num192].GetColor(white14), 0f, default(Vector2), num194, SpriteEffects.None, 0f);
								}
								if (Main.chest[Main.player[Main.myPlayer].chest].item[num192].stack > 1)
								{
									this.spriteBatch.DrawString(Main.fontItemStack, string.Concat(Main.chest[Main.player[Main.myPlayer].chest].item[num192].stack), new Vector2((float)num190 + 10f * Main.inventoryScale, (float)num191 + 26f * Main.inventoryScale), white14, 0f, default(Vector2), num194, SpriteEffects.None, 0f);
								}
							}
						}
					}
				}
				if (Main.player[Main.myPlayer].chest == -2)
				{
					this.spriteBatch.DrawString(Main.fontMouseText, Lang.inter[32], new Vector2(284f, 210f), new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
					Main.inventoryScale = 0.75f;
					if (Main.mouseX > 73 && Main.mouseX < (int)(73f + 280f * Main.inventoryScale) && Main.mouseY > 210 && Main.mouseY < (int)(210f + 224f * Main.inventoryScale))
					{
						Main.player[Main.myPlayer].mouseInterface = true;
					}
					for (int num195 = 0; num195 < 5; num195++)
					{
						for (int num196 = 0; num196 < 4; num196++)
						{
							int num197 = (int)(73f + (float)(num195 * 56) * Main.inventoryScale);
							int num198 = (int)(210f + (float)(num196 * 56) * Main.inventoryScale);
							int num199 = num195 + num196 * 5;
							Color white15 = new Color(100, 100, 100, 100);
							if (Main.mouseX >= num197 && (float)Main.mouseX <= (float)num197 + (float)Main.inventoryBackTexture.Width * Main.inventoryScale && Main.mouseY >= num198 && (float)Main.mouseY <= (float)num198 + (float)Main.inventoryBackTexture.Height * Main.inventoryScale)
							{
								Main.player[Main.myPlayer].mouseInterface = true;
								if (Main.mouseLeftRelease && Main.mouseLeft)
								{
									if (Main.player[Main.myPlayer].selectedItem != num199 || Main.player[Main.myPlayer].itemAnimation <= 0)
									{
										Item item10 = Main.mouseItem;
										Main.mouseItem = Main.player[Main.myPlayer].bank[num199];
										Main.player[Main.myPlayer].bank[num199] = item10;
										if (Main.player[Main.myPlayer].bank[num199].type == 0 || Main.player[Main.myPlayer].bank[num199].stack < 1)
										{
											Main.player[Main.myPlayer].bank[num199] = new Item();
										}
										if (Main.mouseItem.IsTheSameAs(Main.player[Main.myPlayer].bank[num199]) && Main.player[Main.myPlayer].bank[num199].stack != Main.player[Main.myPlayer].bank[num199].maxStack && Main.mouseItem.stack != Main.mouseItem.maxStack)
										{
											if (Main.mouseItem.stack + Main.player[Main.myPlayer].bank[num199].stack <= Main.mouseItem.maxStack)
											{
												Main.player[Main.myPlayer].bank[num199].stack += Main.mouseItem.stack;
												Main.mouseItem.stack = 0;
											}
											else
											{
												int num200 = Main.mouseItem.maxStack - Main.player[Main.myPlayer].bank[num199].stack;
												Main.player[Main.myPlayer].bank[num199].stack += num200;
												Main.mouseItem.stack -= num200;
											}
										}
										if (Main.mouseItem.type == 0 || Main.mouseItem.stack < 1)
										{
											Main.mouseItem = new Item();
										}
										if (Main.mouseItem.type > 0 || Main.player[Main.myPlayer].bank[num199].type > 0)
										{
											Recipe.FindRecipes();
											Main.PlaySound(7, -1, -1, 1);
										}
									}
								}
								else
								{
									if (Main.mouseRight && Main.mouseRightRelease && Main.player[Main.myPlayer].bank[num199].maxStack == 1)
									{
										Main.player[Main.myPlayer].bank[num199] = Main.armorSwap(Main.player[Main.myPlayer].bank[num199]);
									}
									else
									{
										if (Main.stackSplit <= 1 && Main.mouseRight && Main.player[Main.myPlayer].bank[num199].maxStack > 1 && (Main.mouseItem.IsTheSameAs(Main.player[Main.myPlayer].bank[num199]) || Main.mouseItem.type == 0) && (Main.mouseItem.stack < Main.mouseItem.maxStack || Main.mouseItem.type == 0))
										{
											if (Main.mouseItem.type == 0)
											{
												Main.mouseItem = (Item)Main.player[Main.myPlayer].bank[num199].Clone();
												Main.mouseItem.stack = 0;
											}
											Main.mouseItem.stack++;
											Main.player[Main.myPlayer].bank[num199].stack--;
											if (Main.player[Main.myPlayer].bank[num199].stack <= 0)
											{
												Main.player[Main.myPlayer].bank[num199] = new Item();
											}
											Recipe.FindRecipes();
											Main.soundInstanceMenuTick.Stop();
											Main.soundInstanceMenuTick = Main.soundMenuTick.CreateInstance();
											Main.PlaySound(12, -1, -1, 1);
											if (Main.stackSplit == 0)
											{
												Main.stackSplit = 15;
											}
											else
											{
												Main.stackSplit = Main.stackDelay;
											}
										}
									}
								}
								text15 = Main.player[Main.myPlayer].bank[num199].name;
								Main.toolTip = (Item)Main.player[Main.myPlayer].bank[num199].Clone();
								if (Main.player[Main.myPlayer].bank[num199].stack > 1)
								{
									object obj = text15;
									text15 = string.Concat(new object[]
									{
										obj,
										" (",
										Main.player[Main.myPlayer].bank[num199].stack,
										")"
									});
								}
							}
							this.spriteBatch.Draw(Main.inventoryBack2Texture, new Vector2((float)num197, (float)num198), new Rectangle?(new Rectangle(0, 0, Main.inventoryBackTexture.Width, Main.inventoryBackTexture.Height)), color2, 0f, default(Vector2), Main.inventoryScale, SpriteEffects.None, 0f);
							white15 = Color.White;
							if (Main.player[Main.myPlayer].bank[num199].type > 0 && Main.player[Main.myPlayer].bank[num199].stack > 0)
							{
								float num201 = 1f;
								if (Main.itemTexture[Main.player[Main.myPlayer].bank[num199].type].Width > 32 || Main.itemTexture[Main.player[Main.myPlayer].bank[num199].type].Height > 32)
								{
									if (Main.itemTexture[Main.player[Main.myPlayer].bank[num199].type].Width > Main.itemTexture[Main.player[Main.myPlayer].bank[num199].type].Height)
									{
										num201 = 32f / (float)Main.itemTexture[Main.player[Main.myPlayer].bank[num199].type].Width;
									}
									else
									{
										num201 = 32f / (float)Main.itemTexture[Main.player[Main.myPlayer].bank[num199].type].Height;
									}
								}
								num201 *= Main.inventoryScale;
								this.spriteBatch.Draw(Main.itemTexture[Main.player[Main.myPlayer].bank[num199].type], new Vector2((float)num197 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.player[Main.myPlayer].bank[num199].type].Width * 0.5f * num201, (float)num198 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.player[Main.myPlayer].bank[num199].type].Height * 0.5f * num201), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[Main.player[Main.myPlayer].bank[num199].type].Width, Main.itemTexture[Main.player[Main.myPlayer].bank[num199].type].Height)), Main.player[Main.myPlayer].bank[num199].GetAlpha(white15), 0f, default(Vector2), num201, SpriteEffects.None, 0f);
								if (Main.player[Main.myPlayer].bank[num199].color != default(Color))
								{
									this.spriteBatch.Draw(Main.itemTexture[Main.player[Main.myPlayer].bank[num199].type], new Vector2((float)num197 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.player[Main.myPlayer].bank[num199].type].Width * 0.5f * num201, (float)num198 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.player[Main.myPlayer].bank[num199].type].Height * 0.5f * num201), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[Main.player[Main.myPlayer].bank[num199].type].Width, Main.itemTexture[Main.player[Main.myPlayer].bank[num199].type].Height)), Main.player[Main.myPlayer].bank[num199].GetColor(white15), 0f, default(Vector2), num201, SpriteEffects.None, 0f);
								}
								if (Main.player[Main.myPlayer].bank[num199].stack > 1)
								{
									this.spriteBatch.DrawString(Main.fontItemStack, string.Concat(Main.player[Main.myPlayer].bank[num199].stack), new Vector2((float)num197 + 10f * Main.inventoryScale, (float)num198 + 26f * Main.inventoryScale), white15, 0f, default(Vector2), num201, SpriteEffects.None, 0f);
								}
							}
						}
					}
				}
				if (Main.player[Main.myPlayer].chest == -3)
				{
					this.spriteBatch.DrawString(Main.fontMouseText, Lang.inter[33], new Vector2(284f, 210f), new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
					Main.inventoryScale = 0.75f;
					if (Main.mouseX > 73 && Main.mouseX < (int)(73f + 280f * Main.inventoryScale) && Main.mouseY > 210 && Main.mouseY < (int)(210f + 224f * Main.inventoryScale))
					{
						Main.player[Main.myPlayer].mouseInterface = true;
					}
					for (int num202 = 0; num202 < 5; num202++)
					{
						for (int num203 = 0; num203 < 4; num203++)
						{
							int num204 = (int)(73f + (float)(num202 * 56) * Main.inventoryScale);
							int num205 = (int)(210f + (float)(num203 * 56) * Main.inventoryScale);
							int num206 = num202 + num203 * 5;
							Color white16 = new Color(100, 100, 100, 100);
							if (Main.mouseX >= num204 && (float)Main.mouseX <= (float)num204 + (float)Main.inventoryBackTexture.Width * Main.inventoryScale && Main.mouseY >= num205 && (float)Main.mouseY <= (float)num205 + (float)Main.inventoryBackTexture.Height * Main.inventoryScale)
							{
								Main.player[Main.myPlayer].mouseInterface = true;
								if (Main.mouseLeftRelease && Main.mouseLeft)
								{
									if (Main.player[Main.myPlayer].selectedItem != num206 || Main.player[Main.myPlayer].itemAnimation <= 0)
									{
										Item item11 = Main.mouseItem;
										Main.mouseItem = Main.player[Main.myPlayer].bank2[num206];
										Main.player[Main.myPlayer].bank2[num206] = item11;
										if (Main.player[Main.myPlayer].bank2[num206].type == 0 || Main.player[Main.myPlayer].bank2[num206].stack < 1)
										{
											Main.player[Main.myPlayer].bank2[num206] = new Item();
										}
										if (Main.mouseItem.IsTheSameAs(Main.player[Main.myPlayer].bank2[num206]) && Main.player[Main.myPlayer].bank2[num206].stack != Main.player[Main.myPlayer].bank2[num206].maxStack && Main.mouseItem.stack != Main.mouseItem.maxStack)
										{
											if (Main.mouseItem.stack + Main.player[Main.myPlayer].bank2[num206].stack <= Main.mouseItem.maxStack)
											{
												Main.player[Main.myPlayer].bank2[num206].stack += Main.mouseItem.stack;
												Main.mouseItem.stack = 0;
											}
											else
											{
												int num207 = Main.mouseItem.maxStack - Main.player[Main.myPlayer].bank2[num206].stack;
												Main.player[Main.myPlayer].bank2[num206].stack += num207;
												Main.mouseItem.stack -= num207;
											}
										}
										if (Main.mouseItem.type == 0 || Main.mouseItem.stack < 1)
										{
											Main.mouseItem = new Item();
										}
										if (Main.mouseItem.type > 0 || Main.player[Main.myPlayer].bank2[num206].type > 0)
										{
											Recipe.FindRecipes();
											Main.PlaySound(7, -1, -1, 1);
										}
									}
								}
								else
								{
									if (Main.mouseRight && Main.mouseRightRelease && Main.player[Main.myPlayer].bank2[num206].maxStack == 1)
									{
										Main.player[Main.myPlayer].bank2[num206] = Main.armorSwap(Main.player[Main.myPlayer].bank2[num206]);
									}
									else
									{
										if (Main.stackSplit <= 1 && Main.mouseRight && Main.player[Main.myPlayer].bank2[num206].maxStack > 1 && (Main.mouseItem.IsTheSameAs(Main.player[Main.myPlayer].bank2[num206]) || Main.mouseItem.type == 0) && (Main.mouseItem.stack < Main.mouseItem.maxStack || Main.mouseItem.type == 0))
										{
											if (Main.mouseItem.type == 0)
											{
												Main.mouseItem = (Item)Main.player[Main.myPlayer].bank2[num206].Clone();
												Main.mouseItem.stack = 0;
											}
											Main.mouseItem.stack++;
											Main.player[Main.myPlayer].bank2[num206].stack--;
											if (Main.player[Main.myPlayer].bank2[num206].stack <= 0)
											{
												Main.player[Main.myPlayer].bank2[num206] = new Item();
											}
											Recipe.FindRecipes();
											Main.soundInstanceMenuTick.Stop();
											Main.soundInstanceMenuTick = Main.soundMenuTick.CreateInstance();
											Main.PlaySound(12, -1, -1, 1);
											if (Main.stackSplit == 0)
											{
												Main.stackSplit = 15;
											}
											else
											{
												Main.stackSplit = Main.stackDelay;
											}
										}
									}
								}
								text15 = Main.player[Main.myPlayer].bank2[num206].name;
								Main.toolTip = (Item)Main.player[Main.myPlayer].bank2[num206].Clone();
								if (Main.player[Main.myPlayer].bank2[num206].stack > 1)
								{
									object obj = text15;
									text15 = string.Concat(new object[]
									{
										obj,
										" (",
										Main.player[Main.myPlayer].bank2[num206].stack,
										")"
									});
								}
							}
							this.spriteBatch.Draw(Main.inventoryBack2Texture, new Vector2((float)num204, (float)num205), new Rectangle?(new Rectangle(0, 0, Main.inventoryBackTexture.Width, Main.inventoryBackTexture.Height)), color2, 0f, default(Vector2), Main.inventoryScale, SpriteEffects.None, 0f);
							white16 = Color.White;
							if (Main.player[Main.myPlayer].bank2[num206].type > 0 && Main.player[Main.myPlayer].bank2[num206].stack > 0)
							{
								float num208 = 1f;
								if (Main.itemTexture[Main.player[Main.myPlayer].bank2[num206].type].Width > 32 || Main.itemTexture[Main.player[Main.myPlayer].bank2[num206].type].Height > 32)
								{
									if (Main.itemTexture[Main.player[Main.myPlayer].bank2[num206].type].Width > Main.itemTexture[Main.player[Main.myPlayer].bank2[num206].type].Height)
									{
										num208 = 32f / (float)Main.itemTexture[Main.player[Main.myPlayer].bank2[num206].type].Width;
									}
									else
									{
										num208 = 32f / (float)Main.itemTexture[Main.player[Main.myPlayer].bank2[num206].type].Height;
									}
								}
								num208 *= Main.inventoryScale;
								this.spriteBatch.Draw(Main.itemTexture[Main.player[Main.myPlayer].bank2[num206].type], new Vector2((float)num204 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.player[Main.myPlayer].bank2[num206].type].Width * 0.5f * num208, (float)num205 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.player[Main.myPlayer].bank2[num206].type].Height * 0.5f * num208), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[Main.player[Main.myPlayer].bank2[num206].type].Width, Main.itemTexture[Main.player[Main.myPlayer].bank2[num206].type].Height)), Main.player[Main.myPlayer].bank2[num206].GetAlpha(white16), 0f, default(Vector2), num208, SpriteEffects.None, 0f);
								if (Main.player[Main.myPlayer].bank2[num206].color != default(Color))
								{
									this.spriteBatch.Draw(Main.itemTexture[Main.player[Main.myPlayer].bank2[num206].type], new Vector2((float)num204 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.player[Main.myPlayer].bank2[num206].type].Width * 0.5f * num208, (float)num205 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.player[Main.myPlayer].bank2[num206].type].Height * 0.5f * num208), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[Main.player[Main.myPlayer].bank2[num206].type].Width, Main.itemTexture[Main.player[Main.myPlayer].bank2[num206].type].Height)), Main.player[Main.myPlayer].bank2[num206].GetColor(white16), 0f, default(Vector2), num208, SpriteEffects.None, 0f);
								}
								if (Main.player[Main.myPlayer].bank2[num206].stack > 1)
								{
									this.spriteBatch.DrawString(Main.fontItemStack, string.Concat(Main.player[Main.myPlayer].bank2[num206].stack), new Vector2((float)num204 + 10f * Main.inventoryScale, (float)num205 + 26f * Main.inventoryScale), white16, 0f, default(Vector2), num208, SpriteEffects.None, 0f);
								}
							}
						}
					}
				}
			}
			else
			{
				if (Main.npcChatText == null || Main.npcChatText == "")
				{
					bool flag6 = false;
					bool flag7 = false;
					bool flag8 = false;
					for (int num209 = 0; num209 < 3; num209++)
					{
						string text24 = "";
						if (Main.player[Main.myPlayer].accCompass > 0 && !flag8)
						{
							int num210 = (int)((Main.player[Main.myPlayer].position.X + (float)(Main.player[Main.myPlayer].width / 2)) * 2f / 16f - (float)Main.maxTilesX);
							if (num210 > 0)
							{
								text24 = "Position: " + num210 + " feet east";
								if (num210 == 1)
								{
									text24 = "Position: " + num210 + " foot east";
								}
							}
							else
							{
								if (num210 < 0)
								{
									num210 *= -1;
									text24 = "Position: " + num210 + " feet west";
									if (num210 == 1)
									{
										text24 = "Position: " + num210 + " foot west";
									}
								}
								else
								{
									text24 = "Position: center";
								}
							}
							flag8 = true;
						}
						else
						{
							if (Main.player[Main.myPlayer].accDepthMeter > 0 && !flag7)
							{
								int num211 = (int)((double)((Main.player[Main.myPlayer].position.Y + (float)Main.player[Main.myPlayer].height) * 2f / 16f) - Main.worldSurface * 2.0);
								if (num211 > 0)
								{
									text24 = "Depth: " + num211 + " feet below";
									if (num211 == 1)
									{
										text24 = "Depth: " + num211 + " foot below";
									}
								}
								else
								{
									if (num211 < 0)
									{
										num211 *= -1;
										text24 = "Depth: " + num211 + " feet above";
										if (num211 == 1)
										{
											text24 = "Depth: " + num211 + " foot above";
										}
									}
									else
									{
										text24 = "Depth: Level";
									}
								}
								flag7 = true;
							}
							else
							{
								if (Main.player[Main.myPlayer].accWatch > 0 && !flag6)
								{
									string text25 = "AM";
									double num212 = Main.time;
									if (!Main.dayTime)
									{
										num212 += 54000.0;
									}
									num212 = num212 / 86400.0 * 24.0;
									double num213 = 7.5;
									num212 = num212 - num213 - 12.0;
									if (num212 < 0.0)
									{
										num212 += 24.0;
									}
									if (num212 >= 12.0)
									{
										text25 = "PM";
									}
									int num214 = (int)num212;
									double num215 = num212 - (double)num214;
									num215 = (double)((int)(num215 * 60.0));
									string text26 = string.Concat(num215);
									if (num215 < 10.0)
									{
										text26 = "0" + text26;
									}
									if (num214 > 12)
									{
										num214 -= 12;
									}
									if (num214 == 0)
									{
										num214 = 12;
									}
									if (Main.player[Main.myPlayer].accWatch == 1)
									{
										text26 = "00";
									}
									else
									{
										if (Main.player[Main.myPlayer].accWatch == 2)
										{
											if (num215 < 30.0)
											{
												text26 = "00";
											}
											else
											{
												text26 = "30";
											}
										}
									}
									text24 = string.Concat(new object[]
									{
										Lang.inter[34],
										": ",
										num214,
										":",
										text26,
										" ",
										text25
									});
									flag6 = true;
								}
							}
						}
						if (text24 != "")
						{
							for (int num216 = 0; num216 < 5; num216++)
							{
								int num217 = 0;
								int num218 = 0;
								Color black = Color.Black;
								if (num216 == 0)
								{
									num217 = -2;
								}
								if (num216 == 1)
								{
									num217 = 2;
								}
								if (num216 == 2)
								{
									num218 = -2;
								}
								if (num216 == 3)
								{
									num218 = 2;
								}
								if (num216 == 4)
								{
									black = new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor);
								}
								this.spriteBatch.DrawString(Main.fontMouseText, text24, new Vector2((float)(22 + num217), (float)(110 + 22 * num209 + num218 + 48)), black, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
							}
						}
					}
				}
			}
			if (Main.playerInventory || Main.player[Main.myPlayer].ghost)
			{
				string text27 = Lang.inter[35];
				Vector2 vector14 = Main.fontMouseText.MeasureString("Save & Exit");
				Vector2 vector15 = Main.fontMouseText.MeasureString(Lang.inter[35]);
				if (Main.netMode != 0)
				{
					text27 = Lang.inter[36];
					vector14 = Main.fontMouseText.MeasureString("Disconnect");
					vector15 = Main.fontMouseText.MeasureString(Lang.inter[36]);
				}
				Vector2 vector16 = Main.fontDeathText.MeasureString(text27);
				int num219 = Main.screenWidth - 110;
				int num220 = Main.screenHeight - 20;
				float num221 = vector14.X / vector15.X;
				if (Main.mouseExit)
				{
					if (Main.exitScale < 1f)
					{
						Main.exitScale += 0.02f;
					}
				}
				else
				{
					if ((double)Main.exitScale > 0.8)
					{
						Main.exitScale -= 0.02f;
					}
				}
				for (int num222 = 0; num222 < 5; num222++)
				{
					int num223 = 0;
					int num224 = 0;
					Color color9 = Color.Black;
					if (num222 == 0)
					{
						num223 = -2;
					}
					if (num222 == 1)
					{
						num223 = 2;
					}
					if (num222 == 2)
					{
						num224 = -2;
					}
					if (num222 == 3)
					{
						num224 = 2;
					}
					if (num222 == 4)
					{
						color9 = Color.White;
					}
					this.spriteBatch.DrawString(Main.fontDeathText, text27, new Vector2((float)(num219 + num223), (float)(num220 + num224)), color9, 0f, new Vector2(vector16.X / 2f, vector16.Y / 2f), (Main.exitScale - 0.2f) * num221, SpriteEffects.None, 0f);
				}
				if ((float)Main.mouseX > (float)num219 - vector16.X / 2f && (float)Main.mouseX < (float)num219 + vector16.X / 2f && (float)Main.mouseY > (float)num220 - vector16.Y / 2f && (float)Main.mouseY < (float)num220 + vector16.Y / 2f - 10f)
				{
					if (!Main.mouseExit)
					{
						Main.PlaySound(12, -1, -1, 1);
					}
					Main.mouseExit = true;
					Main.player[Main.myPlayer].mouseInterface = true;
					if (Main.mouseLeftRelease && Main.mouseLeft)
					{
						Main.menuMode = 10;
						WorldGen.SaveAndQuit();
					}
				}
				else
				{
					Main.mouseExit = false;
				}
			}
			if (!Main.playerInventory && !Main.player[Main.myPlayer].ghost)
			{
				string text28 = Lang.inter[37];
				if (Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].name != "" && Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].name != null)
				{
					text28 = Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].AffixName();
				}
				Vector2 vector17 = Main.fontMouseText.MeasureString(text28) / 2f;
				this.spriteBatch.DrawString(Main.fontMouseText, text28, new Vector2(236f - vector17.X, 0f), new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
				int num225 = 20;
				for (int num226 = 0; num226 < 10; num226++)
				{
					if (num226 == Main.player[Main.myPlayer].selectedItem)
					{
						if (Main.hotbarScale[num226] < 1f)
						{
							Main.hotbarScale[num226] += 0.05f;
						}
					}
					else
					{
						if ((double)Main.hotbarScale[num226] > 0.75)
						{
							Main.hotbarScale[num226] -= 0.05f;
						}
					}
					float num227 = Main.hotbarScale[num226];
					int num228 = (int)(20f + 22f * (1f - num227));
					int a3 = (int)(75f + 150f * num227);
					Color color10 = new Color(255, 255, 255, a3);
					this.spriteBatch.Draw(Main.inventoryBackTexture, new Vector2((float)num225, (float)num228), new Rectangle?(new Rectangle(0, 0, Main.inventoryBackTexture.Width, Main.inventoryBackTexture.Height)), new Color(100, 100, 100, 100), 0f, default(Vector2), num227, SpriteEffects.None, 0f);
					if (!Main.player[Main.myPlayer].hbLocked && Main.mouseX >= num225 && (float)Main.mouseX <= (float)num225 + (float)Main.inventoryBackTexture.Width * Main.hotbarScale[num226] && Main.mouseY >= num228 && (float)Main.mouseY <= (float)num228 + (float)Main.inventoryBackTexture.Height * Main.hotbarScale[num226] && !Main.player[Main.myPlayer].channel)
					{
						Main.player[Main.myPlayer].mouseInterface = true;
						if (Main.mouseLeft && !Main.player[Main.myPlayer].hbLocked)
						{
							Main.player[Main.myPlayer].changeItem = num226;
						}
						Main.player[Main.myPlayer].showItemIcon = false;
						text15 = Main.player[Main.myPlayer].inventory[num226].AffixName();
						if (Main.player[Main.myPlayer].inventory[num226].stack > 1)
						{
							object obj = text15;
							text15 = string.Concat(new object[]
							{
								obj,
								" (",
								Main.player[Main.myPlayer].inventory[num226].stack,
								")"
							});
						}
						rare = Main.player[Main.myPlayer].inventory[num226].rare;
					}
					if (Main.player[Main.myPlayer].inventory[num226].type > 0 && Main.player[Main.myPlayer].inventory[num226].stack > 0)
					{
						float num229 = 1f;
						if (Main.itemTexture[Main.player[Main.myPlayer].inventory[num226].type].Width > 32 || Main.itemTexture[Main.player[Main.myPlayer].inventory[num226].type].Height > 32)
						{
							if (Main.itemTexture[Main.player[Main.myPlayer].inventory[num226].type].Width > Main.itemTexture[Main.player[Main.myPlayer].inventory[num226].type].Height)
							{
								num229 = 32f / (float)Main.itemTexture[Main.player[Main.myPlayer].inventory[num226].type].Width;
							}
							else
							{
								num229 = 32f / (float)Main.itemTexture[Main.player[Main.myPlayer].inventory[num226].type].Height;
							}
						}
						num229 *= num227;
						this.spriteBatch.Draw(Main.itemTexture[Main.player[Main.myPlayer].inventory[num226].type], new Vector2((float)num225 + 26f * num227 - (float)Main.itemTexture[Main.player[Main.myPlayer].inventory[num226].type].Width * 0.5f * num229, (float)num228 + 26f * num227 - (float)Main.itemTexture[Main.player[Main.myPlayer].inventory[num226].type].Height * 0.5f * num229), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[Main.player[Main.myPlayer].inventory[num226].type].Width, Main.itemTexture[Main.player[Main.myPlayer].inventory[num226].type].Height)), Main.player[Main.myPlayer].inventory[num226].GetAlpha(color10), 0f, default(Vector2), num229, SpriteEffects.None, 0f);
						if (Main.player[Main.myPlayer].inventory[num226].color != default(Color))
						{
							this.spriteBatch.Draw(Main.itemTexture[Main.player[Main.myPlayer].inventory[num226].type], new Vector2((float)num225 + 26f * num227 - (float)Main.itemTexture[Main.player[Main.myPlayer].inventory[num226].type].Width * 0.5f * num229, (float)num228 + 26f * num227 - (float)Main.itemTexture[Main.player[Main.myPlayer].inventory[num226].type].Height * 0.5f * num229), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[Main.player[Main.myPlayer].inventory[num226].type].Width, Main.itemTexture[Main.player[Main.myPlayer].inventory[num226].type].Height)), Main.player[Main.myPlayer].inventory[num226].GetColor(color10), 0f, default(Vector2), num229, SpriteEffects.None, 0f);
						}
						if (Main.player[Main.myPlayer].inventory[num226].stack > 1)
						{
							this.spriteBatch.DrawString(Main.fontItemStack, string.Concat(Main.player[Main.myPlayer].inventory[num226].stack), new Vector2((float)num225 + 10f * num227, (float)num228 + 26f * num227), color10, 0f, default(Vector2), num229, SpriteEffects.None, 0f);
						}
						if (Main.player[Main.myPlayer].inventory[num226].useAmmo > 0)
						{
							int useAmmo = Main.player[Main.myPlayer].inventory[num226].useAmmo;
							int num230 = 0;
							for (int num231 = 0; num231 < 48; num231++)
							{
								if (Main.player[Main.myPlayer].inventory[num231].ammo == useAmmo)
								{
									num230 += Main.player[Main.myPlayer].inventory[num231].stack;
								}
							}
							this.spriteBatch.DrawString(Main.fontItemStack, string.Concat(num230), new Vector2((float)num225 + 8f * num227, (float)num228 + 30f * num227), color10, 0f, default(Vector2), num227 * 0.8f, SpriteEffects.None, 0f);
						}
						else
						{
							if (Main.player[Main.myPlayer].inventory[num226].type == 509)
							{
								int num232 = 0;
								for (int num233 = 0; num233 < 48; num233++)
								{
									if (Main.player[Main.myPlayer].inventory[num233].type == 530)
									{
										num232 += Main.player[Main.myPlayer].inventory[num233].stack;
									}
								}
								this.spriteBatch.DrawString(Main.fontItemStack, string.Concat(num232), new Vector2((float)num225 + 8f * num227, (float)num228 + 30f * num227), color10, 0f, default(Vector2), num227 * 0.8f, SpriteEffects.None, 0f);
							}
						}
						string text29 = string.Concat(num226 + 1);
						if (text29 == "10")
						{
							text29 = "0";
						}
						this.spriteBatch.DrawString(Main.fontItemStack, text29, new Vector2((float)num225 + 8f * Main.hotbarScale[num226], (float)num228 + 4f * Main.hotbarScale[num226]), new Color((int)(color10.R / 2), (int)(color10.G / 2), (int)(color10.B / 2), (int)(color10.A / 2)), 0f, default(Vector2), num229, SpriteEffects.None, 0f);
						if (Main.player[Main.myPlayer].inventory[num226].potion)
						{
							Color alpha = Main.player[Main.myPlayer].inventory[num226].GetAlpha(color10);
							float num234 = (float)Main.player[Main.myPlayer].potionDelay / (float)Main.player[Main.myPlayer].potionDelayTime;
							float num235 = (float)alpha.R * num234;
							float num236 = (float)alpha.G * num234;
							float num237 = (float)alpha.B * num234;
							float num238 = (float)alpha.A * num234;
							alpha = new Color((int)((byte)num235), (int)((byte)num236), (int)((byte)num237), (int)((byte)num238));
							this.spriteBatch.Draw(Main.cdTexture, new Vector2((float)num225 + 26f * Main.hotbarScale[num226] - (float)Main.cdTexture.Width * 0.5f * num229, (float)num228 + 26f * Main.hotbarScale[num226] - (float)Main.cdTexture.Height * 0.5f * num229), new Rectangle?(new Rectangle(0, 0, Main.cdTexture.Width, Main.cdTexture.Height)), alpha, 0f, default(Vector2), num229, SpriteEffects.None, 0f);
						}
					}
					num225 += (int)((float)Main.inventoryBackTexture.Width * Main.hotbarScale[num226]) + 4;
				}
			}
			if (Main.mouseItem.stack <= 0)
			{
				Main.mouseItem.type = 0;
			}
			if (text15 != null && text15 != "" && Main.mouseItem.type == 0)
			{
				Main.player[Main.myPlayer].showItemIcon = false;
				this.MouseText(text15, rare, 0);
				flag = true;
			}
			if (Main.chatMode)
			{
				this.textBlinkerCount++;
				if (this.textBlinkerCount >= 20)
				{
					if (this.textBlinkerState == 0)
					{
						this.textBlinkerState = 1;
					}
					else
					{
						this.textBlinkerState = 0;
					}
					this.textBlinkerCount = 0;
				}
				string text30 = Main.chatText;
				if (this.textBlinkerState == 1)
				{
					text30 += "|";
				}
				this.spriteBatch.Draw(Main.textBackTexture, new Vector2(78f, (float)(Main.screenHeight - 36)), new Rectangle?(new Rectangle(0, 0, Main.textBackTexture.Width, Main.textBackTexture.Height)), new Color(100, 100, 100, 100), 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
				for (int num239 = 0; num239 < 5; num239++)
				{
					int num240 = 0;
					int num241 = 0;
					Color black2 = Color.Black;
					if (num239 == 0)
					{
						num240 = -2;
					}
					if (num239 == 1)
					{
						num240 = 2;
					}
					if (num239 == 2)
					{
						num241 = -2;
					}
					if (num239 == 3)
					{
						num241 = 2;
					}
					if (num239 == 4)
					{
						black2 = new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor);
					}
					this.spriteBatch.DrawString(Main.fontMouseText, text30, new Vector2((float)(88 + num240), (float)(Main.screenHeight - 30 + num241)), black2, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
				}
			}
			for (int num242 = 0; num242 < Main.numChatLines; num242++)
			{
				if (Main.chatMode || Main.chatLine[num242].showTime > 0)
				{
					float num243 = (float)Main.mouseTextColor / 255f;
					for (int num244 = 0; num244 < 5; num244++)
					{
						int num245 = 0;
						int num246 = 0;
						Color black3 = Color.Black;
						if (num244 == 0)
						{
							num245 = -2;
						}
						if (num244 == 1)
						{
							num245 = 2;
						}
						if (num244 == 2)
						{
							num246 = -2;
						}
						if (num244 == 3)
						{
							num246 = 2;
						}
						if (num244 == 4)
						{
							black3 = new Color((int)((byte)((float)Main.chatLine[num242].color.R * num243)), (int)((byte)((float)Main.chatLine[num242].color.G * num243)), (int)((byte)((float)Main.chatLine[num242].color.B * num243)), (int)Main.mouseTextColor);
						}
						this.spriteBatch.DrawString(Main.fontMouseText, Main.chatLine[num242].text, new Vector2((float)(88 + num245), (float)(Main.screenHeight - 30 + num246 - 28 - num242 * 21)), black3, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
					}
				}
			}
			if (Main.player[Main.myPlayer].dead)
			{
				string text31 = Lang.inter[38];
				this.spriteBatch.DrawString(Main.fontDeathText, text31, new Vector2((float)(Main.screenWidth / 2 - text31.Length * 10), (float)(Main.screenHeight / 2 - 20)), Main.player[Main.myPlayer].GetDeathAlpha(new Color(0, 0, 0, 0)), 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
			}
			this.spriteBatch.Draw(Main.cursorTexture, new Vector2((float)(Main.mouseX + 1), (float)(Main.mouseY + 1)), new Rectangle?(new Rectangle(0, 0, Main.cursorTexture.Width, Main.cursorTexture.Height)), new Color((int)((float)Main.cursorColor.R * 0.2f), (int)((float)Main.cursorColor.G * 0.2f), (int)((float)Main.cursorColor.B * 0.2f), (int)((float)Main.cursorColor.A * 0.5f)), 0f, default(Vector2), Main.cursorScale * 1.1f, SpriteEffects.None, 0f);
			this.spriteBatch.Draw(Main.cursorTexture, new Vector2((float)Main.mouseX, (float)Main.mouseY), new Rectangle?(new Rectangle(0, 0, Main.cursorTexture.Width, Main.cursorTexture.Height)), Main.cursorColor, 0f, default(Vector2), Main.cursorScale, SpriteEffects.None, 0f);
			if (Main.mouseItem.type > 0 && Main.mouseItem.stack > 0)
			{
				this.mouseNPC = -1;
				Main.player[Main.myPlayer].showItemIcon = false;
				Main.player[Main.myPlayer].showItemIcon2 = 0;
				flag = true;
				float num247 = 1f;
				if (Main.itemTexture[Main.mouseItem.type].Width > 32 || Main.itemTexture[Main.mouseItem.type].Height > 32)
				{
					if (Main.itemTexture[Main.mouseItem.type].Width > Main.itemTexture[Main.mouseItem.type].Height)
					{
						num247 = 32f / (float)Main.itemTexture[Main.mouseItem.type].Width;
					}
					else
					{
						num247 = 32f / (float)Main.itemTexture[Main.mouseItem.type].Height;
					}
				}
				float num248 = 1f;
				num248 *= Main.cursorScale;
				Color white17 = Color.White;
				num247 *= num248;
				this.spriteBatch.Draw(Main.itemTexture[Main.mouseItem.type], new Vector2((float)Main.mouseX + 26f * num248 - (float)Main.itemTexture[Main.mouseItem.type].Width * 0.5f * num247, (float)Main.mouseY + 26f * num248 - (float)Main.itemTexture[Main.mouseItem.type].Height * 0.5f * num247), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[Main.mouseItem.type].Width, Main.itemTexture[Main.mouseItem.type].Height)), Main.mouseItem.GetAlpha(white17), 0f, default(Vector2), num247, SpriteEffects.None, 0f);
				if (Main.mouseItem.color != default(Color))
				{
					this.spriteBatch.Draw(Main.itemTexture[Main.mouseItem.type], new Vector2((float)Main.mouseX + 26f * num248 - (float)Main.itemTexture[Main.mouseItem.type].Width * 0.5f * num247, (float)Main.mouseY + 26f * num248 - (float)Main.itemTexture[Main.mouseItem.type].Height * 0.5f * num247), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[Main.mouseItem.type].Width, Main.itemTexture[Main.mouseItem.type].Height)), Main.mouseItem.GetColor(white17), 0f, default(Vector2), num247, SpriteEffects.None, 0f);
				}
				if (Main.mouseItem.stack > 1)
				{
					this.spriteBatch.DrawString(Main.fontItemStack, string.Concat(Main.mouseItem.stack), new Vector2((float)Main.mouseX + 10f * num248, (float)Main.mouseY + 26f * num248), white17, 0f, default(Vector2), num247, SpriteEffects.None, 0f);
				}
			}
			else
			{
				if (this.mouseNPC > -1)
				{
					Main.player[Main.myPlayer].mouseInterface = true;
					flag = false;
					float num249 = 1f;
					num249 *= Main.cursorScale;
					this.spriteBatch.Draw(Main.npcHeadTexture[this.mouseNPC], new Vector2((float)Main.mouseX + 26f * num249 - (float)Main.npcHeadTexture[this.mouseNPC].Width * 0.5f * num249, (float)Main.mouseY + 26f * num249 - (float)Main.npcHeadTexture[this.mouseNPC].Height * 0.5f * num249), new Rectangle?(new Rectangle(0, 0, Main.npcHeadTexture[this.mouseNPC].Width, Main.npcHeadTexture[this.mouseNPC].Height)), Color.White, 0f, default(Vector2), num249, SpriteEffects.None, 0f);
					if (Main.mouseRight && Main.mouseRightRelease)
					{
						Main.PlaySound(12, -1, -1, 1);
						this.mouseNPC = -1;
					}
					if (Main.mouseLeft && Main.mouseLeftRelease)
					{
						if (this.mouseNPC == 0)
						{
							int x = (int)(((float)Main.mouseX + Main.screenPosition.X) / 16f);
							int y = (int)(((float)Main.mouseY + Main.screenPosition.Y) / 16f);
							int n2 = -1;
							if (WorldGen.MoveNPC(x, y, n2))
							{
								Main.NewText(Lang.inter[39], 255, 240, 20);
							}
						}
						else
						{
							int num250 = 0;
							for (int num251 = 0; num251 < 200; num251++)
							{
								if (Main.npc[num251].active && Main.npc[num251].type == NPC.NumToType(this.mouseNPC))
								{
									num250 = num251;
									break;
								}
							}
							if (num250 >= 0)
							{
								int x2 = (int)(((float)Main.mouseX + Main.screenPosition.X) / 16f);
								int y2 = (int)(((float)Main.mouseY + Main.screenPosition.Y) / 16f);
								if (WorldGen.MoveNPC(x2, y2, num250))
								{
									this.mouseNPC = -1;
									WorldGen.moveRoom(x2, y2, num250);
									Main.PlaySound(12, -1, -1, 1);
								}
							}
							else
							{
								this.mouseNPC = 0;
							}
						}
					}
				}
			}
			Rectangle rectangle2 = new Rectangle((int)((float)Main.mouseX + Main.screenPosition.X), (int)((float)Main.mouseY + Main.screenPosition.Y), 1, 1);
			if (!flag)
			{
				int num252 = 26 * Main.player[Main.myPlayer].statLifeMax / num43;
				int num253 = 0;
				if (Main.player[Main.myPlayer].statLifeMax > 200)
				{
					num252 = 260;
					num253 += 26;
				}
				if (Main.mouseX > 500 + num41 && Main.mouseX < 500 + num252 + num41 && Main.mouseY > 32 && Main.mouseY < 32 + Main.heartTexture.Height + num253)
				{
					Main.player[Main.myPlayer].showItemIcon = false;
					string cursorText = Main.player[Main.myPlayer].statLife + "/" + Main.player[Main.myPlayer].statLifeMax;
					this.MouseText(cursorText, 0, 0);
					flag = true;
				}
			}
			if (!flag)
			{
				int num254 = 24;
				int num255 = 28 * Main.player[Main.myPlayer].statManaMax2 / num50;
				if (Main.mouseX > 762 + num41 && Main.mouseX < 762 + num254 + num41 && Main.mouseY > 30 && Main.mouseY < 30 + num255)
				{
					Main.player[Main.myPlayer].showItemIcon = false;
					string cursorText2 = Main.player[Main.myPlayer].statMana + "/" + Main.player[Main.myPlayer].statManaMax2;
					this.MouseText(cursorText2, 0, 0);
					flag = true;
				}
			}
			if (!flag)
			{
				for (int num256 = 0; num256 < 200; num256++)
				{
					if (Main.item[num256].active)
					{
						Rectangle value3 = new Rectangle((int)((double)Main.item[num256].position.X + (double)Main.item[num256].width * 0.5 - (double)Main.itemTexture[Main.item[num256].type].Width * 0.5), (int)(Main.item[num256].position.Y + (float)Main.item[num256].height - (float)Main.itemTexture[Main.item[num256].type].Height), Main.itemTexture[Main.item[num256].type].Width, Main.itemTexture[Main.item[num256].type].Height);
						if (rectangle2.Intersects(value3))
						{
							Main.player[Main.myPlayer].showItemIcon = false;
							string text32 = Main.item[num256].AffixName();
							if (Main.item[num256].stack > 1)
							{
								object obj = text32;
								text32 = string.Concat(new object[]
								{
									obj,
									" (",
									Main.item[num256].stack,
									")"
								});
							}
							if (Main.item[num256].owner < 255 && Main.showItemOwner)
							{
								text32 = text32 + " <" + Main.player[Main.item[num256].owner].name + ">";
							}
							rare = Main.item[num256].rare;
							this.MouseText(text32, rare, 0);
							flag = true;
							break;
						}
					}
				}
			}
			for (int num257 = 0; num257 < 255; num257++)
			{
				if (Main.player[num257].active && Main.myPlayer != num257 && !Main.player[num257].dead)
				{
					Rectangle value4 = new Rectangle((int)((double)Main.player[num257].position.X + (double)Main.player[num257].width * 0.5 - 16.0), (int)(Main.player[num257].position.Y + (float)Main.player[num257].height - 48f), 32, 48);
					if (!flag && rectangle2.Intersects(value4))
					{
						Main.player[Main.myPlayer].showItemIcon = false;
						int num258 = Main.player[num257].statLife;
						if (num258 < 0)
						{
							num258 = 0;
						}
						string text33 = string.Concat(new object[]
						{
							Main.player[num257].name,
							": ",
							num258,
							"/",
							Main.player[num257].statLifeMax
						});
						if (Main.player[num257].hostile)
						{
							text33 += " (PvP)";
						}
						this.MouseText(text33, 0, Main.player[num257].difficulty);
					}
				}
			}
			if (!flag)
			{
				for (int num259 = 0; num259 < 200; num259++)
				{
					if (Main.npc[num259].active)
					{
						Rectangle value5 = new Rectangle((int)((double)Main.npc[num259].position.X + (double)Main.npc[num259].width * 0.5 - (double)Main.npcTexture[Main.npc[num259].type].Width * 0.5), (int)(Main.npc[num259].position.Y + (float)Main.npc[num259].height - (float)(Main.npcTexture[Main.npc[num259].type].Height / Main.npcFrameCount[Main.npc[num259].type])), Main.npcTexture[Main.npc[num259].type].Width, Main.npcTexture[Main.npc[num259].type].Height / Main.npcFrameCount[Main.npc[num259].type]);
						if (Main.npc[num259].type >= 87 && Main.npc[num259].type <= 92)
						{
							value5 = new Rectangle((int)((double)Main.npc[num259].position.X + (double)Main.npc[num259].width * 0.5 - 32.0), (int)((double)Main.npc[num259].position.Y + (double)Main.npc[num259].height * 0.5 - 32.0), 64, 64);
						}
						if (rectangle2.Intersects(value5) && (Main.npc[num259].type != 85 || Main.npc[num259].ai[0] != 0f))
						{
							bool flag9 = false;
							if (Main.npc[num259].townNPC || Main.npc[num259].type == 105 || Main.npc[num259].type == 106 || Main.npc[num259].type == 123)
							{
								Rectangle rectangle3 = new Rectangle((int)(Main.player[Main.myPlayer].position.X + (float)(Main.player[Main.myPlayer].width / 2) - (float)(Player.tileRangeX * 16)), (int)(Main.player[Main.myPlayer].position.Y + (float)(Main.player[Main.myPlayer].height / 2) - (float)(Player.tileRangeY * 16)), Player.tileRangeX * 16 * 2, Player.tileRangeY * 16 * 2);
								Rectangle value6 = new Rectangle((int)Main.npc[num259].position.X, (int)Main.npc[num259].position.Y, Main.npc[num259].width, Main.npc[num259].height);
								if (rectangle3.Intersects(value6))
								{
									flag9 = true;
								}
							}
							if (flag9 && !Main.player[Main.myPlayer].dead)
							{
								int num260 = -(Main.npc[num259].width / 2 + 8);
								SpriteEffects effects2 = SpriteEffects.None;
								if (Main.npc[num259].spriteDirection == -1)
								{
									effects2 = SpriteEffects.FlipHorizontally;
									num260 = Main.npc[num259].width / 2 + 8;
								}
								this.spriteBatch.Draw(Main.chatTexture, new Vector2(Main.npc[num259].position.X + (float)(Main.npc[num259].width / 2) - Main.screenPosition.X - (float)(Main.chatTexture.Width / 2) - (float)num260, Main.npc[num259].position.Y - (float)Main.chatTexture.Height - Main.screenPosition.Y), new Rectangle?(new Rectangle(0, 0, Main.chatTexture.Width, Main.chatTexture.Height)), new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), 0f, default(Vector2), 1f, effects2, 0f);
								if (Main.mouseRight && Main.npcChatRelease)
								{
									Main.npcChatRelease = false;
									if (Main.player[Main.myPlayer].talkNPC != num259)
									{
										Main.npcShop = 0;
										Main.craftGuide = false;
										Main.player[Main.myPlayer].dropItemCheck();
										Recipe.FindRecipes();
										Main.player[Main.myPlayer].sign = -1;
										Main.editSign = false;
										Main.player[Main.myPlayer].talkNPC = num259;
										Main.playerInventory = false;
										Main.player[Main.myPlayer].chest = -1;
										Main.npcChatText = Main.npc[num259].GetChat();
										Main.PlaySound(24, -1, -1, 1);
									}
								}
							}
							Main.player[Main.myPlayer].showItemIcon = false;
							string text34 = Main.npc[num259].displayName;
							int num261 = num259;
							if (Main.npc[num259].realLife >= 0)
							{
								num261 = Main.npc[num259].realLife;
							}
							if (Main.npc[num261].lifeMax > 1 && !Main.npc[num261].dontTakeDamage)
							{
								object obj = text34;
								text34 = string.Concat(new object[]
								{
									obj,
									": ",
									Main.npc[num261].life,
									"/",
									Main.npc[num261].lifeMax
								});
							}
							this.MouseText(text34, 0, 0);
							break;
						}
					}
				}
			}
			if (Main.mouseRight)
			{
				Main.npcChatRelease = false;
			}
			else
			{
				Main.npcChatRelease = true;
			}
			if (Main.player[Main.myPlayer].showItemIcon && (Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].type > 0 || Main.player[Main.myPlayer].showItemIcon2 > 0))
			{
				int num262 = Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].type;
				Color color11 = Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].GetAlpha(Color.White);
				Color color12 = Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].GetColor(Color.White);
				if (Main.player[Main.myPlayer].showItemIcon2 > 0)
				{
					num262 = Main.player[Main.myPlayer].showItemIcon2;
					color11 = Color.White;
					color12 = default(Color);
				}
				float scale2 = Main.cursorScale;
				this.spriteBatch.Draw(Main.itemTexture[num262], new Vector2((float)(Main.mouseX + 10), (float)(Main.mouseY + 10)), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[num262].Width, Main.itemTexture[num262].Height)), color11, 0f, default(Vector2), scale2, SpriteEffects.None, 0f);
				if (Main.player[Main.myPlayer].showItemIcon2 == 0 && Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].color != default(Color))
				{
					this.spriteBatch.Draw(Main.itemTexture[Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].type], new Vector2((float)(Main.mouseX + 10), (float)(Main.mouseY + 10)), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].type].Width, Main.itemTexture[Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].type].Height)), color12, 0f, default(Vector2), scale2, SpriteEffects.None, 0f);
				}
			}
			Main.player[Main.myPlayer].showItemIcon = false;
			Main.player[Main.myPlayer].showItemIcon2 = 0;
		}
		protected void QuitGame()
		{
			Steam.Kill();
			base.Exit();
		}
		protected Color randColor()
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			while (num + num3 + num2 <= 150)
			{
				num = Main.rand.Next(256);
				num2 = Main.rand.Next(256);
				num3 = Main.rand.Next(256);
			}
			return new Color(num, num2, num3, 255);
		}
		protected void DrawMenu()
		{
			Main.render = false;
			Star.UpdateStars();
			Cloud.UpdateClouds();
			Main.holyTiles = 0;
			Main.evilTiles = 0;
			Main.jungleTiles = 0;
			Main.chatMode = false;
			for (int i = 0; i < Main.numChatLines; i++)
			{
				Main.chatLine[i] = new ChatLine();
			}
			this.DrawFPS();
			Main.screenLastPosition = Main.screenPosition;
			Main.screenPosition.Y = (float)(Main.worldSurface * 16.0 - (double)Main.screenHeight);
			if (Main.grabSky)
			{
				Main.screenPosition.X = Main.screenPosition.X + (float)(Main.mouseX - Main.screenWidth / 2) * 0.02f;
			}
			else
			{
				Main.screenPosition.X = Main.screenPosition.X + 2f;
			}
			if (Main.screenPosition.X > 2.14748352E+09f)
			{
				Main.screenPosition.X = 0f;
			}
			if (Main.screenPosition.X < -2.14748352E+09f)
			{
				Main.screenPosition.X = 0f;
			}
			Main.background = 0;
			byte b = (byte)((255 + Main.tileColor.R * 2) / 3);
			Color color = new Color((int)b, (int)b, (int)b, 255);
			this.logoRotation += this.logoRotationSpeed * 3E-05f;
			if ((double)this.logoRotation > 0.1)
			{
				this.logoRotationDirection = -1f;
			}
			else
			{
				if ((double)this.logoRotation < -0.1)
				{
					this.logoRotationDirection = 1f;
				}
			}
			if (this.logoRotationSpeed < 20f & this.logoRotationDirection == 1f)
			{
				this.logoRotationSpeed += 1f;
			}
			else
			{
				if (this.logoRotationSpeed > -20f & this.logoRotationDirection == -1f)
				{
					this.logoRotationSpeed -= 1f;
				}
			}
			this.logoScale += this.logoScaleSpeed * 1E-05f;
			if ((double)this.logoScale > 1.1)
			{
				this.logoScaleDirection = -1f;
			}
			else
			{
				if ((double)this.logoScale < 0.9)
				{
					this.logoScaleDirection = 1f;
				}
			}
			if (this.logoScaleSpeed < 50f & this.logoScaleDirection == 1f)
			{
				this.logoScaleSpeed += 1f;
			}
			else
			{
				if (this.logoScaleSpeed > -50f & this.logoScaleDirection == -1f)
				{
					this.logoScaleSpeed -= 1f;
				}
			}
			Color color2 = new Color((int)((byte)((float)color.R * ((float)Main.LogoA / 255f))), (int)((byte)((float)color.G * ((float)Main.LogoA / 255f))), (int)((byte)((float)color.B * ((float)Main.LogoA / 255f))), (int)((byte)((float)color.A * ((float)Main.LogoA / 255f))));
			Color color3 = new Color((int)((byte)((float)color.R * ((float)Main.LogoB / 255f))), (int)((byte)((float)color.G * ((float)Main.LogoB / 255f))), (int)((byte)((float)color.B * ((float)Main.LogoB / 255f))), (int)((byte)((float)color.A * ((float)Main.LogoB / 255f))));
			Main.LogoT = false;
			if (!Main.LogoT)
			{
				this.spriteBatch.Draw(Main.logoTexture, new Vector2((float)(Main.screenWidth / 2), 100f), new Rectangle?(new Rectangle(0, 0, Main.logoTexture.Width, Main.logoTexture.Height)), color2, this.logoRotation, new Vector2((float)(Main.logoTexture.Width / 2), (float)(Main.logoTexture.Height / 2)), this.logoScale, SpriteEffects.None, 0f);
			}
			else
			{
				this.spriteBatch.Draw(Main.logo3Texture, new Vector2((float)(Main.screenWidth / 2), 100f), new Rectangle?(new Rectangle(0, 0, Main.logoTexture.Width, Main.logoTexture.Height)), color2, this.logoRotation, new Vector2((float)(Main.logoTexture.Width / 2), (float)(Main.logoTexture.Height / 2)), this.logoScale, SpriteEffects.None, 0f);
			}
			this.spriteBatch.Draw(Main.logo2Texture, new Vector2((float)(Main.screenWidth / 2), 100f), new Rectangle?(new Rectangle(0, 0, Main.logoTexture.Width, Main.logoTexture.Height)), color3, this.logoRotation, new Vector2((float)(Main.logoTexture.Width / 2), (float)(Main.logoTexture.Height / 2)), this.logoScale, SpriteEffects.None, 0f);
			if (Main.dayTime)
			{
				Main.LogoA += 2;
				if (Main.LogoA > 255)
				{
					Main.LogoA = 255;
				}
				Main.LogoB--;
				if (Main.LogoB < 0)
				{
					Main.LogoB = 0;
				}
			}
			else
			{
				Main.LogoB += 2;
				if (Main.LogoB > 255)
				{
					Main.LogoB = 255;
				}
				Main.LogoA--;
				if (Main.LogoA < 0)
				{
					Main.LogoA = 0;
					Main.LogoT = true;
				}
			}
			int num = 250;
			int num2 = Main.screenWidth / 2;
			int num3 = 80;
			int num4 = 0;
			int num5 = Main.menuMode;
			int num6 = -1;
			int num7 = 0;
			int num8 = 0;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			int num9 = 0;
			bool[] array = new bool[Main.maxMenuItems];
			bool[] array2 = new bool[Main.maxMenuItems];
			int[] array3 = new int[Main.maxMenuItems];
			int[] array4 = new int[Main.maxMenuItems];
			byte[] array5 = new byte[Main.maxMenuItems];
			float[] array6 = new float[Main.maxMenuItems];
			bool[] array7 = new bool[Main.maxMenuItems];
			for (int j = 0; j < Main.maxMenuItems; j++)
			{
				array[j] = false;
				array2[j] = false;
				array3[j] = 0;
				array4[j] = 0;
				array6[j] = 1f;
			}
			string[] array8 = new string[Main.maxMenuItems];
			if (Main.menuMode == -1)
			{
				Main.menuMode = 0;
			}
			if (Main.menuMode == 1212)
			{
				if (this.focusMenu == 2)
				{
					array8[0] = "Wählen Sie die Sprache";
				}
				else
				{
					if (this.focusMenu == 3)
					{
						array8[0] = "Selezionare la lingua";
					}
					else
					{
						if (this.focusMenu == 4)
						{
							array8[0] = "Sélectionnez la langue";
						}
						else
						{
							if (this.focusMenu == 5)
							{
								array8[0] = "Seleccione el idioma";
							}
							else
							{
								array8[0] = "Select language";
							}
						}
					}
				}
				num3 = 50;
				num = 200;
				array3[1] = 25;
				array3[2] = 25;
				array3[3] = 25;
				array3[4] = 25;
				array3[5] = 25;
				array[0] = true;
				array8[1] = "English";
				array8[2] = "Deutsch";
				array8[3] = "Italiano";
				array8[4] = "Française";
				array8[5] = "Español";
				num4 = 6;
				if (this.selectedMenu >= 1)
				{
					Lang.lang = this.selectedMenu;
					Lang.setLang();
					Main.menuMode = 0;
					Main.PlaySound(10, -1, -1, 1);
					this.SaveSettings();
				}
			}
			else
			{
				if (Main.menuMode == 1213)
				{
					if (this.focusMenu == 1)
					{
						array8[0] = "Select language";
					}
					else
					{
						if (this.focusMenu == 2)
						{
							array8[0] = "Wählen Sie die Sprache";
						}
						else
						{
							if (this.focusMenu == 3)
							{
								array8[0] = "Selezionare la lingua";
							}
							else
							{
								if (this.focusMenu == 4)
								{
									array8[0] = "Sélectionnez la langue";
								}
								else
								{
									if (this.focusMenu == 5)
									{
										array8[0] = "Seleccione el idioma";
									}
									else
									{
										array8[0] = Lang.menu[102];
									}
								}
							}
						}
					}
					num3 = 48;
					num = 180;
					array3[1] = 25;
					array3[2] = 25;
					array3[3] = 25;
					array3[4] = 25;
					array3[5] = 25;
					array3[6] = 50;
					array[0] = true;
					array8[1] = "English";
					array8[2] = "Deutsch";
					array8[3] = "Italiano";
					array8[4] = "Française";
					array8[5] = "Español";
					array8[6] = Lang.menu[5];
					num4 = 7;
					if (this.selectedMenu == 6)
					{
						Main.menuMode = 11;
						Main.PlaySound(11, -1, -1, 1);
					}
					else
					{
						if (this.selectedMenu >= 1)
						{
							Lang.lang = this.selectedMenu;
							Lang.setLang();
							Main.PlaySound(12, -1, -1, 1);
							this.SaveSettings();
						}
					}
				}
				else
				{
					if (Main.netMode == 2)
					{
						bool flag4 = true;
						for (int k = 0; k < 8; k++)
						{
							if (k < 255)
							{
								try
								{
									array8[k] = Netplay.serverSock[k].statusText;
									if (Netplay.serverSock[k].active && Main.showSpam)
									{
										string[] array9;
										string[] expr_89D = array9 = array8;
										IntPtr intPtr;
										int expr_8A2 = (int)(intPtr = (IntPtr)k);
										object obj = array9[(int)intPtr];
										expr_89D[expr_8A2] = string.Concat(new object[]
										{
											obj,
											" (",
											NetMessage.buffer[k].spamCount,
											")"
										});
									}
								}
								catch
								{
									array8[k] = "";
								}
								array[k] = true;
								if (array8[k] != "" && array8[k] != null)
								{
									flag4 = false;
								}
							}
						}
						if (flag4)
						{
							array8[0] = Lang.menu[0];
							array8[1] = Lang.menu[1] + Netplay.serverPort + ".";
						}
						num4 = 11;
						array8[9] = Main.statusText;
						array[9] = true;
						num = 170;
						num3 = 30;
						array3[10] = 20;
						array3[10] = 40;
						array8[10] = Lang.menu[2];
						if (this.selectedMenu == 10)
						{
							Netplay.disconnect = true;
							Main.PlaySound(11, -1, -1, 1);
						}
					}
					else
					{
						if (Main.menuMode == 31)
						{
							string password = Netplay.password;
							Netplay.password = Main.GetInputText(Netplay.password);
							if (password != Netplay.password)
							{
								Main.PlaySound(12, -1, -1, 1);
							}
							array8[0] = Lang.menu[3];
							this.textBlinkerCount++;
							if (this.textBlinkerCount >= 20)
							{
								if (this.textBlinkerState == 0)
								{
									this.textBlinkerState = 1;
								}
								else
								{
									this.textBlinkerState = 0;
								}
								this.textBlinkerCount = 0;
							}
							array8[1] = Netplay.password;
							if (this.textBlinkerState == 1)
							{
								string[] array9;
								(array9 = array8)[1] = array9[1] + "|";
								array4[1] = 1;
							}
							else
							{
								string[] array9;
								(array9 = array8)[1] = array9[1] + " ";
							}
							array[0] = true;
							array[1] = true;
							array3[1] = -20;
							array3[2] = 20;
							array8[2] = Lang.menu[4];
							array8[3] = Lang.menu[5];
							num4 = 4;
							if (this.selectedMenu == 3)
							{
								Main.PlaySound(11, -1, -1, 1);
								Main.menuMode = 0;
								Netplay.disconnect = true;
								Netplay.password = "";
							}
							else
							{
								if (this.selectedMenu == 2 || Main.inputTextEnter)
								{
									NetMessage.SendData(38, -1, -1, Netplay.password, 0, 0f, 0f, 0f, 0);
									Main.menuMode = 14;
								}
							}
						}
						else
						{
							if (Main.netMode == 1 || Main.menuMode == 14)
							{
								num4 = 2;
								array8[0] = Main.statusText;
								array[0] = true;
								num = 300;
								array8[1] = Lang.menu[6];
								if (this.selectedMenu != 1)
								{
									goto IL_3B38;
								}
								Netplay.disconnect = true;
								Netplay.clientSock.tcpClient.Close();
								Main.PlaySound(11, -1, -1, 1);
								Main.menuMode = 0;
								Main.netMode = 0;
								try
								{
									this.tServer.Kill();
									goto IL_3B38;
								}
								catch
								{
									goto IL_3B38;
								}
							}
							if (Main.menuMode == 30)
							{
								string password2 = Netplay.password;
								Netplay.password = Main.GetInputText(Netplay.password);
								if (password2 != Netplay.password)
								{
									Main.PlaySound(12, -1, -1, 1);
								}
								array8[0] = Lang.menu[7];
								this.textBlinkerCount++;
								if (this.textBlinkerCount >= 20)
								{
									if (this.textBlinkerState == 0)
									{
										this.textBlinkerState = 1;
									}
									else
									{
										this.textBlinkerState = 0;
									}
									this.textBlinkerCount = 0;
								}
								array8[1] = Netplay.password;
								if (this.textBlinkerState == 1)
								{
									string[] array9;
									(array9 = array8)[1] = array9[1] + "|";
									array4[1] = 1;
								}
								else
								{
									string[] array9;
									(array9 = array8)[1] = array9[1] + " ";
								}
								array[0] = true;
								array[1] = true;
								array3[1] = -20;
								array3[2] = 20;
								array8[2] = Lang.menu[4];
								array8[3] = Lang.menu[5];
								num4 = 4;
								if (this.selectedMenu == 3)
								{
									Main.PlaySound(11, -1, -1, 1);
									Main.menuMode = 6;
									Netplay.password = "";
								}
								else
								{
									if (this.selectedMenu == 2 || Main.inputTextEnter || Main.autoPass)
									{
										this.tServer.StartInfo.FileName = "TerrariaServer.exe";
										this.tServer.StartInfo.Arguments = string.Concat(new object[]
										{
											"-autoshutdown -world \"",
											Main.worldPathName,
											"\" -password \"",
											Netplay.password,
											"\" -lang ",
											Lang.lang
										});
										if (Main.libPath != "")
										{
											ProcessStartInfo expr_D47 = this.tServer.StartInfo;
											expr_D47.Arguments = expr_D47.Arguments + " -loadlib " + Main.libPath;
										}
										this.tServer.StartInfo.UseShellExecute = false;
										this.tServer.StartInfo.CreateNoWindow = true;
										this.tServer.Start();
										Netplay.SetIP("127.0.0.1");
										Main.autoPass = true;
										Main.statusText = Lang.menu[8];
										Netplay.StartClient();
										Main.menuMode = 10;
									}
								}
							}
							else
							{
								if (Main.menuMode == 15)
								{
									num4 = 2;
									array8[0] = Main.statusText;
									array[0] = true;
									num = 80;
									num3 = 400;
									array8[1] = Lang.menu[5];
									if (this.selectedMenu == 1)
									{
										Netplay.disconnect = true;
										Main.PlaySound(11, -1, -1, 1);
										Main.menuMode = 0;
										Main.netMode = 0;
									}
								}
								else
								{
									if (Main.menuMode == 200)
									{
										num4 = 3;
										array8[0] = Lang.menu[9];
										array[0] = true;
										num -= 30;
										array3[1] = 70;
										array3[2] = 50;
										array8[1] = Lang.menu[10];
										array8[2] = Lang.menu[6];
										if (this.selectedMenu == 1)
										{
											if (File.Exists(Main.worldPathName + ".bak"))
											{
												File.Copy(Main.worldPathName + ".bak", Main.worldPathName, true);
												File.Delete(Main.worldPathName + ".bak");
												Main.PlaySound(10, -1, -1, 1);
												WorldGen.playWorld();
												Main.menuMode = 10;
											}
											else
											{
												Main.PlaySound(11, -1, -1, 1);
												Main.menuMode = 0;
												Main.netMode = 0;
											}
										}
										if (this.selectedMenu == 2)
										{
											Main.PlaySound(11, -1, -1, 1);
											Main.menuMode = 0;
											Main.netMode = 0;
										}
									}
									else
									{
										if (Main.menuMode == 201)
										{
											num4 = 3;
											array8[0] = Lang.menu[9];
											array[0] = true;
											array[1] = true;
											num -= 30;
											array3[1] = -30;
											array3[2] = 50;
											array8[1] = Lang.menu[11];
											array8[2] = Lang.menu[5];
											if (this.selectedMenu == 2)
											{
												Main.PlaySound(11, -1, -1, 1);
												Main.menuMode = 0;
												Main.netMode = 0;
											}
										}
										else
										{
											if (Main.menuMode == 10)
											{
												num4 = 1;
												array8[0] = Main.statusText;
												array[0] = true;
												num = 300;
											}
											else
											{
												if (Main.menuMode == 100)
												{
													num4 = 1;
													array8[0] = Main.statusText;
													array[0] = true;
													num = 300;
												}
												else
												{
													if (Main.menuMode == 0)
													{
														Main.menuMultiplayer = false;
														Main.menuServer = false;
														Main.netMode = 0;
														array8[0] = Lang.menu[12];
														array8[1] = Lang.menu[13];
														array8[2] = Lang.menu[14];
														array8[3] = Lang.menu[15];
														num4 = 4;
														if (this.selectedMenu == 3)
														{
															this.QuitGame();
														}
														if (this.selectedMenu == 1)
														{
															Main.PlaySound(10, -1, -1, 1);
															Main.menuMode = 12;
														}
														if (this.selectedMenu == 2)
														{
															Main.PlaySound(10, -1, -1, 1);
															Main.menuMode = 11;
														}
														if (this.selectedMenu == 0)
														{
															Main.PlaySound(10, -1, -1, 1);
															Main.menuMode = 1;
															Main.LoadPlayers();
														}
													}
													else
													{
														if (Main.menuMode == 1)
														{
															Main.myPlayer = 0;
															num = 190;
															num3 = 50;
															array8[5] = Lang.menu[16];
															array8[6] = Lang.menu[17];
															if (Main.numLoadPlayers == 5)
															{
																array2[5] = true;
																array8[5] = "";
															}
															else
															{
																if (Main.numLoadPlayers == 0)
																{
																	array2[6] = true;
																	array8[6] = "";
																}
															}
															array8[7] = Lang.menu[5];
															for (int l = 0; l < 5; l++)
															{
																if (l < Main.numLoadPlayers)
																{
																	array8[l] = Main.loadPlayer[l].name;
																	array5[l] = Main.loadPlayer[l].difficulty;
																}
																else
																{
																	array8[l] = null;
																}
															}
															num4 = 8;
															if (this.focusMenu >= 0 && this.focusMenu < Main.numLoadPlayers)
															{
																num6 = this.focusMenu;
																Vector2 vector = Main.fontDeathText.MeasureString(array8[num6]);
																num7 = (int)((double)(Main.screenWidth / 2) + (double)vector.X * 0.5 + 10.0);
																num8 = num + num3 * this.focusMenu + 4;
															}
															if (this.selectedMenu == 7)
															{
																Main.autoJoin = false;
																Main.autoPass = false;
																Main.PlaySound(11, -1, -1, 1);
																if (Main.menuMultiplayer)
																{
																	Main.menuMode = 12;
																	Main.menuMultiplayer = false;
																	Main.menuServer = false;
																}
																else
																{
																	Main.menuMode = 0;
																}
															}
															else
															{
																if (this.selectedMenu == 5)
																{
																	Main.loadPlayer[Main.numLoadPlayers] = new Player();
																	Main.loadPlayer[Main.numLoadPlayers].inventory[0].SetDefaults("Copper Shortsword");
																	Main.loadPlayer[Main.numLoadPlayers].inventory[0].Prefix(-1);
																	Main.loadPlayer[Main.numLoadPlayers].inventory[1].SetDefaults("Copper Pickaxe");
																	Main.loadPlayer[Main.numLoadPlayers].inventory[1].Prefix(-1);
																	Main.loadPlayer[Main.numLoadPlayers].inventory[2].SetDefaults("Copper Axe");
																	Main.loadPlayer[Main.numLoadPlayers].inventory[2].Prefix(-1);
																	Main.PlaySound(10, -1, -1, 1);
																	Main.menuMode = 2;
																}
																else
																{
																	if (this.selectedMenu == 6)
																	{
																		Main.PlaySound(10, -1, -1, 1);
																		Main.menuMode = 4;
																	}
																	else
																	{
																		if (this.selectedMenu >= 0)
																		{
																			if (Main.menuMultiplayer)
																			{
																				this.selectedPlayer = this.selectedMenu;
																				Main.player[Main.myPlayer] = (Player)Main.loadPlayer[this.selectedPlayer].Clone();
																				Main.playerPathName = Main.loadPlayerPath[this.selectedPlayer];
																				Main.PlaySound(10, -1, -1, 1);
																				if (Main.autoJoin)
																				{
																					if (Netplay.SetIP(Main.getIP))
																					{
																						Main.menuMode = 10;
																						Netplay.StartClient();
																					}
																					else
																					{
																						if (Netplay.SetIP2(Main.getIP))
																						{
																							Main.menuMode = 10;
																							Netplay.StartClient();
																						}
																					}
																					Main.autoJoin = false;
																				}
																				else
																				{
																					if (Main.menuServer)
																					{
																						Main.LoadWorlds();
																						Main.menuMode = 6;
																					}
																					else
																					{
																						Main.menuMode = 13;
																						Main.clrInput();
																					}
																				}
																			}
																			else
																			{
																				Main.myPlayer = 0;
																				this.selectedPlayer = this.selectedMenu;
																				Main.player[Main.myPlayer] = (Player)Main.loadPlayer[this.selectedPlayer].Clone();
																				Main.playerPathName = Main.loadPlayerPath[this.selectedPlayer];
																				Main.LoadWorlds();
																				Main.PlaySound(10, -1, -1, 1);
																				Main.menuMode = 6;
																			}
																		}
																	}
																}
															}
														}
														else
														{
															if (Main.menuMode == 2)
															{
																if (this.selectedMenu == 0)
																{
																	Main.menuMode = 17;
																	Main.PlaySound(10, -1, -1, 1);
																	this.selColor = Main.loadPlayer[Main.numLoadPlayers].hairColor;
																}
																if (this.selectedMenu == 1)
																{
																	Main.menuMode = 18;
																	Main.PlaySound(10, -1, -1, 1);
																	this.selColor = Main.loadPlayer[Main.numLoadPlayers].eyeColor;
																}
																if (this.selectedMenu == 2)
																{
																	Main.menuMode = 19;
																	Main.PlaySound(10, -1, -1, 1);
																	this.selColor = Main.loadPlayer[Main.numLoadPlayers].skinColor;
																}
																if (this.selectedMenu == 3)
																{
																	Main.menuMode = 20;
																	Main.PlaySound(10, -1, -1, 1);
																}
																array8[0] = Lang.menu[18];
																array8[1] = Lang.menu[19];
																array8[2] = Lang.menu[20];
																array8[3] = Lang.menu[21];
																num = 220;
																for (int m = 0; m < 9; m++)
																{
																	if (m < 6)
																	{
																		array6[m] = 0.75f;
																	}
																	else
																	{
																		array6[m] = 0.9f;
																	}
																}
																num3 = 38;
																array3[6] = 6;
																array3[7] = 12;
																array3[8] = 18;
																num6 = Main.numLoadPlayers;
																num7 = Main.screenWidth / 2 - 16;
																num8 = 176;
																if (Main.loadPlayer[num6].male)
																{
																	array8[4] = Lang.menu[22];
																}
																else
																{
																	array8[4] = Lang.menu[23];
																}
																if (this.selectedMenu == 4)
																{
																	if (Main.loadPlayer[num6].male)
																	{
																		Main.PlaySound(20, -1, -1, 1);
																		Main.loadPlayer[num6].male = false;
																	}
																	else
																	{
																		Main.PlaySound(1, -1, -1, 1);
																		Main.loadPlayer[num6].male = true;
																	}
																}
																if (Main.loadPlayer[num6].difficulty == 2)
																{
																	array8[5] = Lang.menu[24];
																	array5[5] = Main.loadPlayer[num6].difficulty;
																}
																else
																{
																	if (Main.loadPlayer[num6].difficulty == 1)
																	{
																		array8[5] = Lang.menu[25];
																		array5[5] = Main.loadPlayer[num6].difficulty;
																	}
																	else
																	{
																		array8[5] = Lang.menu[26];
																	}
																}
																if (this.selectedMenu == 5)
																{
																	Main.PlaySound(10, -1, -1, 1);
																	Main.menuMode = 222;
																}
																if (this.selectedMenu == 7)
																{
																	Main.PlaySound(12, -1, -1, 1);
																	Main.loadPlayer[num6].hair = Main.rand.Next(36);
																	Main.loadPlayer[num6].eyeColor = this.randColor();
																	while ((int)(Main.loadPlayer[num6].eyeColor.R + Main.loadPlayer[num6].eyeColor.G + Main.loadPlayer[num6].eyeColor.B) > 300)
																	{
																		Main.loadPlayer[num6].eyeColor = this.randColor();
																	}
																	Main.loadPlayer[num6].hairColor = this.randColor();
																	Main.loadPlayer[num6].pantsColor = this.randColor();
																	Main.loadPlayer[num6].shirtColor = this.randColor();
																	Main.loadPlayer[num6].shoeColor = this.randColor();
																	Main.loadPlayer[num6].skinColor = this.randColor();
																	float num10 = (float)Main.rand.Next(60, 120) * 0.01f;
																	if (num10 > 1f)
																	{
																		num10 = 1f;
																	}
																	Main.loadPlayer[num6].skinColor.R = (byte)((float)Main.rand.Next(240, 255) * num10);
																	Main.loadPlayer[num6].skinColor.G = (byte)((float)Main.rand.Next(110, 140) * num10);
																	Main.loadPlayer[num6].skinColor.B = (byte)((float)Main.rand.Next(75, 110) * num10);
																	Main.loadPlayer[num6].underShirtColor = this.randColor();
																	int num11 = Main.loadPlayer[num6].hair + 1;
																	if (num11 == 5 || num11 == 6 || num11 == 7 || num11 == 10 || num11 == 12 || num11 == 19 || num11 == 22 || num11 == 23 || num11 == 26 || num11 == 27 || num11 == 30 || num11 == 33)
																	{
																		Main.loadPlayer[num6].male = false;
																	}
																	else
																	{
																		Main.loadPlayer[num6].male = true;
																	}
																}
																array8[7] = Lang.menu[27];
																array8[6] = Lang.menu[28];
																array8[8] = Lang.menu[5];
																num4 = 9;
																if (this.selectedMenu == 8)
																{
																	Main.PlaySound(11, -1, -1, 1);
																	Main.menuMode = 1;
																}
																else
																{
																	if (this.selectedMenu == 6)
																	{
																		Main.PlaySound(10, -1, -1, 1);
																		Main.loadPlayer[Main.numLoadPlayers].name = "";
																		Main.menuMode = 3;
																		Main.clrInput();
																	}
																}
															}
															else
															{
																if (Main.menuMode == 222)
																{
																	if (this.focusMenu == 3)
																	{
																		array8[0] = Lang.menu[29];
																	}
																	else
																	{
																		if (this.focusMenu == 2)
																		{
																			array8[0] = Lang.menu[30];
																		}
																		else
																		{
																			if (this.focusMenu == 1)
																			{
																				array8[0] = Lang.menu[31];
																			}
																			else
																			{
																				array8[0] = Lang.menu[32];
																			}
																		}
																	}
																	num3 = 50;
																	array3[1] = 25;
																	array3[2] = 25;
																	array3[3] = 25;
																	array[0] = true;
																	array8[1] = Lang.menu[26];
																	array8[2] = Lang.menu[25];
																	array5[2] = 1;
																	array8[3] = Lang.menu[24];
																	array5[3] = 2;
																	num4 = 4;
																	if (this.selectedMenu == 1)
																	{
																		Main.loadPlayer[Main.numLoadPlayers].difficulty = 0;
																		Main.menuMode = 2;
																	}
																	else
																	{
																		if (this.selectedMenu == 2)
																		{
																			Main.menuMode = 2;
																			Main.loadPlayer[Main.numLoadPlayers].difficulty = 1;
																		}
																		else
																		{
																			if (this.selectedMenu == 3)
																			{
																				Main.loadPlayer[Main.numLoadPlayers].difficulty = 2;
																				Main.menuMode = 2;
																			}
																		}
																	}
																}
																else
																{
																	if (Main.menuMode == 20)
																	{
																		if (this.selectedMenu == 0)
																		{
																			Main.menuMode = 21;
																			Main.PlaySound(10, -1, -1, 1);
																			this.selColor = Main.loadPlayer[Main.numLoadPlayers].shirtColor;
																		}
																		if (this.selectedMenu == 1)
																		{
																			Main.menuMode = 22;
																			Main.PlaySound(10, -1, -1, 1);
																			this.selColor = Main.loadPlayer[Main.numLoadPlayers].underShirtColor;
																		}
																		if (this.selectedMenu == 2)
																		{
																			Main.menuMode = 23;
																			Main.PlaySound(10, -1, -1, 1);
																			this.selColor = Main.loadPlayer[Main.numLoadPlayers].pantsColor;
																		}
																		if (this.selectedMenu == 3)
																		{
																			this.selColor = Main.loadPlayer[Main.numLoadPlayers].shoeColor;
																			Main.menuMode = 24;
																			Main.PlaySound(10, -1, -1, 1);
																		}
																		array8[0] = Lang.menu[33];
																		array8[1] = Lang.menu[34];
																		array8[2] = Lang.menu[35];
																		array8[3] = Lang.menu[36];
																		num = 260;
																		num3 = 50;
																		array3[5] = 20;
																		array8[5] = Lang.menu[5];
																		num4 = 6;
																		num6 = Main.numLoadPlayers;
																		num7 = Main.screenWidth / 2 - 16;
																		num8 = 210;
																		if (this.selectedMenu == 5)
																		{
																			Main.PlaySound(11, -1, -1, 1);
																			Main.menuMode = 2;
																		}
																	}
																	else
																	{
																		if (Main.menuMode == 17)
																		{
																			num6 = Main.numLoadPlayers;
																			num7 = Main.screenWidth / 2 - 16;
																			num8 = 210;
																			flag = true;
																			num9 = 390;
																			num = 260;
																			num3 = 60;
																			Main.loadPlayer[num6].hairColor = this.selColor;
																			num4 = 3;
																			array8[0] = Lang.menu[37] + " " + (Main.loadPlayer[num6].hair + 1);
																			array8[1] = Lang.menu[38];
																			array[1] = true;
																			array3[2] = 150;
																			array3[1] = 10;
																			array8[2] = Lang.menu[5];
																			if (this.selectedMenu == 0)
																			{
																				Main.PlaySound(12, -1, -1, 1);
																				Main.loadPlayer[num6].hair++;
																				if (Main.loadPlayer[num6].hair >= 36)
																				{
																					Main.loadPlayer[num6].hair = 0;
																				}
																			}
																			else
																			{
																				if (this.selectedMenu2 == 0)
																				{
																					Main.PlaySound(12, -1, -1, 1);
																					Main.loadPlayer[num6].hair--;
																					if (Main.loadPlayer[num6].hair < 0)
																					{
																						Main.loadPlayer[num6].hair = 35;
																					}
																				}
																			}
																			if (this.selectedMenu == 2)
																			{
																				Main.menuMode = 2;
																				Main.PlaySound(11, -1, -1, 1);
																			}
																		}
																		else
																		{
																			if (Main.menuMode == 18)
																			{
																				num6 = Main.numLoadPlayers;
																				num7 = Main.screenWidth / 2 - 16;
																				num8 = 210;
																				flag = true;
																				num9 = 370;
																				num = 240;
																				num3 = 60;
																				Main.loadPlayer[num6].eyeColor = this.selColor;
																				num4 = 3;
																				array8[0] = "";
																				array8[1] = Lang.menu[39];
																				array[1] = true;
																				array3[2] = 170;
																				array3[1] = 10;
																				array8[2] = Lang.menu[5];
																				if (this.selectedMenu == 2)
																				{
																					Main.menuMode = 2;
																					Main.PlaySound(11, -1, -1, 1);
																				}
																			}
																			else
																			{
																				if (Main.menuMode == 19)
																				{
																					num6 = Main.numLoadPlayers;
																					num7 = Main.screenWidth / 2 - 16;
																					num8 = 210;
																					flag = true;
																					num9 = 370;
																					num = 240;
																					num3 = 60;
																					Main.loadPlayer[num6].skinColor = this.selColor;
																					num4 = 3;
																					array8[0] = "";
																					array8[1] = Lang.menu[40];
																					array[1] = true;
																					array3[2] = 170;
																					array3[1] = 10;
																					array8[2] = Lang.menu[5];
																					if (this.selectedMenu == 2)
																					{
																						Main.menuMode = 2;
																						Main.PlaySound(11, -1, -1, 1);
																					}
																				}
																				else
																				{
																					if (Main.menuMode == 21)
																					{
																						num6 = Main.numLoadPlayers;
																						num7 = Main.screenWidth / 2 - 16;
																						num8 = 210;
																						flag = true;
																						num9 = 370;
																						num = 240;
																						num3 = 60;
																						Main.loadPlayer[num6].shirtColor = this.selColor;
																						num4 = 3;
																						array8[0] = "";
																						array8[1] = Lang.menu[41];
																						array[1] = true;
																						array3[2] = 170;
																						array3[1] = 10;
																						array8[2] = Lang.menu[5];
																						if (this.selectedMenu == 2)
																						{
																							Main.menuMode = 20;
																							Main.PlaySound(11, -1, -1, 1);
																						}
																					}
																					else
																					{
																						if (Main.menuMode == 22)
																						{
																							num6 = Main.numLoadPlayers;
																							num7 = Main.screenWidth / 2 - 16;
																							num8 = 210;
																							flag = true;
																							num9 = 370;
																							num = 240;
																							num3 = 60;
																							Main.loadPlayer[num6].underShirtColor = this.selColor;
																							num4 = 3;
																							array8[0] = "";
																							array8[1] = Lang.menu[42];
																							array[1] = true;
																							array3[2] = 170;
																							array3[1] = 10;
																							array8[2] = Lang.menu[5];
																							if (this.selectedMenu == 2)
																							{
																								Main.menuMode = 20;
																								Main.PlaySound(11, -1, -1, 1);
																							}
																						}
																						else
																						{
																							if (Main.menuMode == 23)
																							{
																								num6 = Main.numLoadPlayers;
																								num7 = Main.screenWidth / 2 - 16;
																								num8 = 210;
																								flag = true;
																								num9 = 370;
																								num = 240;
																								num3 = 60;
																								Main.loadPlayer[num6].pantsColor = this.selColor;
																								num4 = 3;
																								array8[0] = "";
																								array8[1] = Lang.menu[43];
																								array[1] = true;
																								array3[2] = 170;
																								array3[1] = 10;
																								array8[2] = Lang.menu[5];
																								if (this.selectedMenu == 2)
																								{
																									Main.menuMode = 20;
																									Main.PlaySound(11, -1, -1, 1);
																								}
																							}
																							else
																							{
																								if (Main.menuMode == 24)
																								{
																									num6 = Main.numLoadPlayers;
																									num7 = Main.screenWidth / 2 - 16;
																									num8 = 210;
																									flag = true;
																									num9 = 370;
																									num = 240;
																									num3 = 60;
																									Main.loadPlayer[num6].shoeColor = this.selColor;
																									num4 = 3;
																									array8[0] = "";
																									array8[1] = Lang.menu[44];
																									array[1] = true;
																									array3[2] = 170;
																									array3[1] = 10;
																									array8[2] = Lang.menu[5];
																									if (this.selectedMenu == 2)
																									{
																										Main.menuMode = 20;
																										Main.PlaySound(11, -1, -1, 1);
																									}
																								}
																								else
																								{
																									if (Main.menuMode == 3)
																									{
																										string name = Main.loadPlayer[Main.numLoadPlayers].name;
																										Main.loadPlayer[Main.numLoadPlayers].name = Main.GetInputText(Main.loadPlayer[Main.numLoadPlayers].name);
																										if (Main.loadPlayer[Main.numLoadPlayers].name.Length > Player.nameLen)
																										{
																											Main.loadPlayer[Main.numLoadPlayers].name = Main.loadPlayer[Main.numLoadPlayers].name.Substring(0, Player.nameLen);
																										}
																										if (name != Main.loadPlayer[Main.numLoadPlayers].name)
																										{
																											Main.PlaySound(12, -1, -1, 1);
																										}
																										array8[0] = Lang.menu[45];
																										array2[2] = true;
																										if (Main.loadPlayer[Main.numLoadPlayers].name != "")
																										{
																											if (Main.loadPlayer[Main.numLoadPlayers].name.Substring(0, 1) == " ")
																											{
																												Main.loadPlayer[Main.numLoadPlayers].name = "";
																											}
																											for (int n = 0; n < Main.loadPlayer[Main.numLoadPlayers].name.Length; n++)
																											{
																												if (Main.loadPlayer[Main.numLoadPlayers].name.Substring(n, 1) != " ")
																												{
																													array2[2] = false;
																												}
																											}
																										}
																										this.textBlinkerCount++;
																										if (this.textBlinkerCount >= 20)
																										{
																											if (this.textBlinkerState == 0)
																											{
																												this.textBlinkerState = 1;
																											}
																											else
																											{
																												this.textBlinkerState = 0;
																											}
																											this.textBlinkerCount = 0;
																										}
																										array8[1] = Main.loadPlayer[Main.numLoadPlayers].name;
																										if (this.textBlinkerState == 1)
																										{
																											string[] array9;
																											(array9 = array8)[1] = array9[1] + "|";
																											array4[1] = 1;
																										}
																										else
																										{
																											string[] array9;
																											(array9 = array8)[1] = array9[1] + " ";
																										}
																										array[0] = true;
																										array[1] = true;
																										array3[1] = -20;
																										array3[2] = 20;
																										array8[2] = Lang.menu[4];
																										array8[3] = Lang.menu[5];
																										num4 = 4;
																										if (this.selectedMenu == 3)
																										{
																											Main.PlaySound(11, -1, -1, 1);
																											Main.menuMode = 2;
																										}
																										if (this.selectedMenu == 2 || (!array2[2] && Main.inputTextEnter))
																										{
																											Main.loadPlayer[Main.numLoadPlayers].name.Trim();
																											Main.loadPlayerPath[Main.numLoadPlayers] = Main.nextLoadPlayer();
																											Player.SavePlayer(Main.loadPlayer[Main.numLoadPlayers], Main.loadPlayerPath[Main.numLoadPlayers]);
																											Main.LoadPlayers();
																											Main.PlaySound(10, -1, -1, 1);
																											Main.menuMode = 1;
																										}
																									}
																									else
																									{
																										if (Main.menuMode == 4)
																										{
																											num = 220;
																											num3 = 60;
																											array8[5] = Lang.menu[5];
																											for (int num12 = 0; num12 < 5; num12++)
																											{
																												if (num12 < Main.numLoadPlayers)
																												{
																													array8[num12] = Main.loadPlayer[num12].name;
																													array5[num12] = Main.loadPlayer[num12].difficulty;
																												}
																												else
																												{
																													array8[num12] = null;
																												}
																											}
																											num4 = 6;
																											if (this.focusMenu >= 0 && this.focusMenu < Main.numLoadPlayers)
																											{
																												num6 = this.focusMenu;
																												Vector2 vector2 = Main.fontDeathText.MeasureString(array8[num6]);
																												num7 = (int)((double)(Main.screenWidth / 2) + (double)vector2.X * 0.5 + 10.0);
																												num8 = num + num3 * this.focusMenu + 4;
																											}
																											if (this.selectedMenu == 5)
																											{
																												Main.PlaySound(11, -1, -1, 1);
																												Main.menuMode = 1;
																											}
																											else
																											{
																												if (this.selectedMenu >= 0)
																												{
																													this.selectedPlayer = this.selectedMenu;
																													Main.PlaySound(10, -1, -1, 1);
																													Main.menuMode = 5;
																												}
																											}
																										}
																										else
																										{
																											if (Main.menuMode == 5)
																											{
																												array8[0] = Lang.menu[46] + " " + Main.loadPlayer[this.selectedPlayer].name + "?";
																												array[0] = true;
																												array8[1] = Lang.menu[104];
																												array8[2] = Lang.menu[105];
																												num4 = 3;
																												if (this.selectedMenu == 1)
																												{
																													Main.ErasePlayer(this.selectedPlayer);
																													Main.PlaySound(10, -1, -1, 1);
																													Main.menuMode = 1;
																												}
																												else
																												{
																													if (this.selectedMenu == 2)
																													{
																														Main.PlaySound(11, -1, -1, 1);
																														Main.menuMode = 1;
																													}
																												}
																											}
																											else
																											{
																												if (Main.menuMode == 6)
																												{
																													num = 190;
																													num3 = 50;
																													array8[5] = Lang.menu[47];
																													array8[6] = Lang.menu[17];
																													if (Main.numLoadWorlds == 5)
																													{
																														array2[5] = true;
																														array8[5] = "";
																													}
																													else
																													{
																														if (Main.numLoadWorlds == 0)
																														{
																															array2[6] = true;
																															array8[6] = "";
																														}
																													}
																													array8[7] = Lang.menu[5];
																													for (int num13 = 0; num13 < 5; num13++)
																													{
																														if (num13 < Main.numLoadWorlds)
																														{
																															array8[num13] = Main.loadWorld[num13];
																														}
																														else
																														{
																															array8[num13] = null;
																														}
																													}
																													num4 = 8;
																													if (this.selectedMenu == 7)
																													{
																														if (Main.menuMultiplayer)
																														{
																															Main.menuMode = 12;
																														}
																														else
																														{
																															Main.menuMode = 1;
																														}
																														Main.PlaySound(11, -1, -1, 1);
																													}
																													else
																													{
																														if (this.selectedMenu == 5)
																														{
																															Main.PlaySound(10, -1, -1, 1);
																															Main.menuMode = 16;
																															Main.newWorldName = Lang.gen[57] + " " + (Main.numLoadWorlds + 1);
																														}
																														else
																														{
																															if (this.selectedMenu == 6)
																															{
																																Main.PlaySound(10, -1, -1, 1);
																																Main.menuMode = 8;
																															}
																															else
																															{
																																if (this.selectedMenu >= 0)
																																{
																																	if (Main.menuMultiplayer)
																																	{
																																		Main.PlaySound(10, -1, -1, 1);
																																		Main.worldPathName = Main.loadWorldPath[this.selectedMenu];
																																		Main.menuMode = 30;
																																	}
																																	else
																																	{
																																		Main.PlaySound(10, -1, -1, 1);
																																		Main.worldPathName = Main.loadWorldPath[this.selectedMenu];
																																		WorldGen.playWorld();
																																		Main.menuMode = 10;
																																	}
																																}
																															}
																														}
																													}
																												}
																												else
																												{
																													if (Main.menuMode == 7)
																													{
																														string a = Main.newWorldName;
																														Main.newWorldName = Main.GetInputText(Main.newWorldName);
																														if (Main.newWorldName.Length > 20)
																														{
																															Main.newWorldName = Main.newWorldName.Substring(0, 20);
																														}
																														if (a != Main.newWorldName)
																														{
																															Main.PlaySound(12, -1, -1, 1);
																														}
																														array8[0] = Lang.menu[48];
																														array2[2] = true;
																														if (Main.newWorldName != "")
																														{
																															if (Main.newWorldName.Substring(0, 1) == " ")
																															{
																																Main.newWorldName = "";
																															}
																															for (int num14 = 0; num14 < Main.newWorldName.Length; num14++)
																															{
																																if (Main.newWorldName != " ")
																																{
																																	array2[2] = false;
																																}
																															}
																														}
																														this.textBlinkerCount++;
																														if (this.textBlinkerCount >= 20)
																														{
																															if (this.textBlinkerState == 0)
																															{
																																this.textBlinkerState = 1;
																															}
																															else
																															{
																																this.textBlinkerState = 0;
																															}
																															this.textBlinkerCount = 0;
																														}
																														array8[1] = Main.newWorldName;
																														if (this.textBlinkerState == 1)
																														{
																															string[] array9;
																															(array9 = array8)[1] = array9[1] + "|";
																															array4[1] = 1;
																														}
																														else
																														{
																															string[] array9;
																															(array9 = array8)[1] = array9[1] + " ";
																														}
																														array[0] = true;
																														array[1] = true;
																														array3[1] = -20;
																														array3[2] = 20;
																														array8[2] = Lang.menu[4];
																														array8[3] = Lang.menu[5];
																														num4 = 4;
																														if (this.selectedMenu == 3)
																														{
																															Main.PlaySound(11, -1, -1, 1);
																															Main.menuMode = 16;
																														}
																														if (this.selectedMenu == 2 || (!array2[2] && Main.inputTextEnter))
																														{
																															Main.menuMode = 10;
																															Main.worldName = Main.newWorldName;
																															Main.worldPathName = Main.nextLoadWorld();
																															WorldGen.CreateNewWorld();
																														}
																													}
																													else
																													{
																														if (Main.menuMode == 8)
																														{
																															num = 220;
																															num3 = 60;
																															array8[5] = Lang.menu[5];
																															for (int num15 = 0; num15 < 5; num15++)
																															{
																																if (num15 < Main.numLoadWorlds)
																																{
																																	array8[num15] = Main.loadWorld[num15];
																																}
																																else
																																{
																																	array8[num15] = null;
																																}
																															}
																															num4 = 6;
																															if (this.selectedMenu == 5)
																															{
																																Main.PlaySound(11, -1, -1, 1);
																																Main.menuMode = 1;
																															}
																															else
																															{
																																if (this.selectedMenu >= 0)
																																{
																																	this.selectedWorld = this.selectedMenu;
																																	Main.PlaySound(10, -1, -1, 1);
																																	Main.menuMode = 9;
																																}
																															}
																														}
																														else
																														{
																															if (Main.menuMode == 9)
																															{
																																array8[0] = Lang.menu[46] + " " + Main.loadWorld[this.selectedWorld] + "?";
																																array[0] = true;
																																array8[1] = Lang.menu[104];
																																array8[2] = Lang.menu[105];
																																num4 = 3;
																																if (this.selectedMenu == 1)
																																{
																																	Main.EraseWorld(this.selectedWorld);
																																	Main.PlaySound(10, -1, -1, 1);
																																	Main.menuMode = 6;
																																}
																																else
																																{
																																	if (this.selectedMenu == 2)
																																	{
																																		Main.PlaySound(11, -1, -1, 1);
																																		Main.menuMode = 6;
																																	}
																																}
																															}
																															else
																															{
																																if (Main.menuMode == 1111)
																																{
																																	num = 210;
																																	num3 = 46;
																																	for (int num16 = 0; num16 < 7; num16++)
																																	{
																																		array6[num16] = 0.9f;
																																	}
																																	array3[7] = 10;
																																	num4 = 8;
																																	if (this.graphics.IsFullScreen)
																																	{
																																		array8[0] = Lang.menu[49];
																																	}
																																	else
																																	{
																																		array8[0] = Lang.menu[50];
																																	}
																																	this.bgScroll = (int)Math.Round((double)((1f - Main.caveParrallax) * 500f));
																																	array8[1] = Lang.menu[51];
																																	array8[2] = Lang.menu[52];
																																	if (Main.fixedTiming)
																																	{
																																		array8[3] = Lang.menu[53];
																																	}
																																	else
																																	{
																																		array8[3] = Lang.menu[54];
																																	}
																																	if (Lighting.lightMode == 0)
																																	{
																																		array8[4] = Lang.menu[55];
																																	}
																																	else
																																	{
																																		if (Lighting.lightMode == 1)
																																		{
																																			array8[4] = Lang.menu[56];
																																		}
																																		else
																																		{
																																			if (Lighting.lightMode == 2)
																																			{
																																				array8[4] = Lang.menu[57];
																																			}
																																			else
																																			{
																																				if (Lighting.lightMode == 3)
																																				{
																																					array8[4] = Lang.menu[58];
																																				}
																																			}
																																		}
																																	}
																																	if (Main.qaStyle == 0)
																																	{
																																		array8[5] = Lang.menu[59];
																																	}
																																	else
																																	{
																																		if (Main.qaStyle == 1)
																																		{
																																			array8[5] = Lang.menu[60];
																																		}
																																		else
																																		{
																																			if (Main.qaStyle == 2)
																																			{
																																				array8[5] = Lang.menu[61];
																																			}
																																			else
																																			{
																																				array8[5] = Lang.menu[62];
																																			}
																																		}
																																	}
																																	if (Main.owBack)
																																	{
																																		array8[6] = Lang.menu[100];
																																	}
																																	else
																																	{
																																		array8[6] = Lang.menu[101];
																																	}
																																	if (this.selectedMenu == 6)
																																	{
																																		Main.PlaySound(12, -1, -1, 1);
																																		if (Main.owBack)
																																		{
																																			Main.owBack = false;
																																		}
																																		else
																																		{
																																			Main.owBack = true;
																																		}
																																	}
																																	array8[7] = Lang.menu[5];
																																	if (this.selectedMenu == 7)
																																	{
																																		Main.PlaySound(11, -1, -1, 1);
																																		this.SaveSettings();
																																		Main.menuMode = 11;
																																	}
																																	if (this.selectedMenu == 5)
																																	{
																																		Main.PlaySound(12, -1, -1, 1);
																																		Main.qaStyle++;
																																		if (Main.qaStyle > 3)
																																		{
																																			Main.qaStyle = 0;
																																		}
																																	}
																																	if (this.selectedMenu == 4)
																																	{
																																		Main.PlaySound(12, -1, -1, 1);
																																		Lighting.lightMode++;
																																		if (Lighting.lightMode >= 4)
																																		{
																																			Lighting.lightMode = 0;
																																		}
																																	}
																																	if (this.selectedMenu == 3)
																																	{
																																		Main.PlaySound(12, -1, -1, 1);
																																		if (Main.fixedTiming)
																																		{
																																			Main.fixedTiming = false;
																																		}
																																		else
																																		{
																																			Main.fixedTiming = true;
																																		}
																																	}
																																	if (this.selectedMenu == 2)
																																	{
																																		Main.PlaySound(11, -1, -1, 1);
																																		Main.menuMode = 28;
																																	}
																																	if (this.selectedMenu == 1)
																																	{
																																		Main.PlaySound(10, -1, -1, 1);
																																		Main.menuMode = 111;
																																	}
																																	if (this.selectedMenu == 0)
																																	{
																																		this.graphics.ToggleFullScreen();
																																	}
																																}
																																else
																																{
																																	if (Main.menuMode == 11)
																																	{
																																		num = 180;
																																		num3 = 44;
																																		array3[8] = 10;
																																		num4 = 9;
																																		for (int num17 = 0; num17 < 9; num17++)
																																		{
																																			array6[num17] = 0.9f;
																																		}
																																		array8[0] = Lang.menu[63];
																																		array8[1] = Lang.menu[64];
																																		array8[2] = Lang.menu[65];
																																		array8[3] = Lang.menu[66];
																																		if (Main.autoSave)
																																		{
																																			array8[4] = Lang.menu[67];
																																		}
																																		else
																																		{
																																			array8[4] = Lang.menu[68];
																																		}
																																		if (Main.autoPause)
																																		{
																																			array8[5] = Lang.menu[69];
																																		}
																																		else
																																		{
																																			array8[5] = Lang.menu[70];
																																		}
																																		if (Main.showItemText)
																																		{
																																			array8[6] = Lang.menu[71];
																																		}
																																		else
																																		{
																																			array8[6] = Lang.menu[72];
																																		}
																																		array8[8] = Lang.menu[5];
																																		array8[7] = Lang.menu[103];
																																		if (this.selectedMenu == 7)
																																		{
																																			Main.PlaySound(10, -1, -1, 1);
																																			Main.menuMode = 1213;
																																		}
																																		if (this.selectedMenu == 8)
																																		{
																																			Main.PlaySound(11, -1, -1, 1);
																																			this.SaveSettings();
																																			Main.menuMode = 0;
																																		}
																																		if (this.selectedMenu == 6)
																																		{
																																			Main.PlaySound(12, -1, -1, 1);
																																			if (Main.showItemText)
																																			{
																																				Main.showItemText = false;
																																			}
																																			else
																																			{
																																				Main.showItemText = true;
																																			}
																																		}
																																		if (this.selectedMenu == 5)
																																		{
																																			Main.PlaySound(12, -1, -1, 1);
																																			if (Main.autoPause)
																																			{
																																				Main.autoPause = false;
																																			}
																																			else
																																			{
																																				Main.autoPause = true;
																																			}
																																		}
																																		if (this.selectedMenu == 4)
																																		{
																																			Main.PlaySound(12, -1, -1, 1);
																																			if (Main.autoSave)
																																			{
																																				Main.autoSave = false;
																																			}
																																			else
																																			{
																																				Main.autoSave = true;
																																			}
																																		}
																																		if (this.selectedMenu == 3)
																																		{
																																			Main.PlaySound(11, -1, -1, 1);
																																			Main.menuMode = 27;
																																		}
																																		if (this.selectedMenu == 2)
																																		{
																																			Main.PlaySound(11, -1, -1, 1);
																																			Main.menuMode = 26;
																																		}
																																		if (this.selectedMenu == 1)
																																		{
																																			Main.PlaySound(10, -1, -1, 1);
																																			this.selColor = Main.mouseColor;
																																			Main.menuMode = 25;
																																		}
																																		if (this.selectedMenu == 0)
																																		{
																																			Main.PlaySound(10, -1, -1, 1);
																																			Main.menuMode = 1111;
																																		}
																																	}
																																	else
																																	{
																																		if (Main.menuMode == 111)
																																		{
																																			num = 240;
																																			num3 = 60;
																																			num4 = 3;
																																			array8[0] = Lang.menu[73];
																																			array8[1] = this.graphics.PreferredBackBufferWidth + "x" + this.graphics.PreferredBackBufferHeight;
																																			array[0] = true;
																																			array3[2] = 170;
																																			array3[1] = 10;
																																			array8[2] = Lang.menu[5];
																																			if (this.selectedMenu == 1)
																																			{
																																				Main.PlaySound(12, -1, -1, 1);
																																				int num18 = 0;
																																				for (int num19 = 0; num19 < this.numDisplayModes; num19++)
																																				{
																																					if (this.displayWidth[num19] == this.graphics.PreferredBackBufferWidth && this.displayHeight[num19] == this.graphics.PreferredBackBufferHeight)
																																					{
																																						num18 = num19;
																																						break;
																																					}
																																				}
																																				num18++;
																																				if (num18 >= this.numDisplayModes)
																																				{
																																					num18 = 0;
																																				}
																																				this.graphics.PreferredBackBufferWidth = this.displayWidth[num18];
																																				this.graphics.PreferredBackBufferHeight = this.displayHeight[num18];
																																			}
																																			if (this.selectedMenu == 2)
																																			{
																																				if (this.graphics.IsFullScreen)
																																				{
																																					this.graphics.ApplyChanges();
																																				}
																																				Main.menuMode = 1111;
																																				Main.PlaySound(11, -1, -1, 1);
																																			}
																																		}
																																		else
																																		{
																																			if (Main.menuMode == 25)
																																			{
																																				flag = true;
																																				num9 = 370;
																																				num = 240;
																																				num3 = 60;
																																				Main.mouseColor = this.selColor;
																																				num4 = 3;
																																				array8[0] = "";
																																				array8[1] = Lang.menu[64];
																																				array[1] = true;
																																				array3[2] = 170;
																																				array3[1] = 10;
																																				array8[2] = Lang.menu[5];
																																				if (this.selectedMenu == 2)
																																				{
																																					Main.menuMode = 11;
																																					Main.PlaySound(11, -1, -1, 1);
																																				}
																																			}
																																			else
																																			{
																																				if (Main.menuMode == 26)
																																				{
																																					flag2 = true;
																																					num = 240;
																																					num3 = 60;
																																					num4 = 3;
																																					array8[0] = "";
																																					array8[1] = Lang.menu[65];
																																					array[1] = true;
																																					array3[2] = 170;
																																					array3[1] = 10;
																																					array8[2] = Lang.menu[5];
																																					if (this.selectedMenu == 2)
																																					{
																																						Main.menuMode = 11;
																																						Main.PlaySound(11, -1, -1, 1);
																																					}
																																				}
																																				else
																																				{
																																					if (Main.menuMode == 28)
																																					{
																																						Main.caveParrallax = 1f - (float)this.bgScroll / 500f;
																																						flag3 = true;
																																						num = 240;
																																						num3 = 60;
																																						num4 = 3;
																																						array8[0] = "";
																																						array8[1] = Lang.menu[52];
																																						array[1] = true;
																																						array3[2] = 170;
																																						array3[1] = 10;
																																						array8[2] = Lang.menu[5];
																																						if (this.selectedMenu == 2)
																																						{
																																							Main.menuMode = 1111;
																																							Main.PlaySound(11, -1, -1, 1);
																																						}
																																					}
																																					else
																																					{
																																						if (Main.menuMode == 27)
																																						{
																																							num = 176;
																																							num3 = 28;
																																							num4 = 14;
																																							string[] array10 = new string[]
																																							{
																																								Main.cUp,
																																								Main.cDown,
																																								Main.cLeft,
																																								Main.cRight,
																																								Main.cJump,
																																								Main.cThrowItem,
																																								Main.cInv,
																																								Main.cHeal,
																																								Main.cMana,
																																								Main.cBuff,
																																								Main.cHook,
																																								Main.cTorch
																																							};
																																							if (this.setKey >= 0)
																																							{
																																								array10[this.setKey] = "_";
																																							}
																																							array8[0] = Lang.menu[74] + array10[0];
																																							array8[1] = Lang.menu[75] + array10[1];
																																							array8[2] = Lang.menu[76] + array10[2];
																																							array8[3] = Lang.menu[77] + array10[3];
																																							array8[4] = Lang.menu[78] + array10[4];
																																							array8[5] = Lang.menu[79] + array10[5];
																																							array8[6] = Lang.menu[80] + array10[6];
																																							array8[7] = Lang.menu[81] + array10[7];
																																							array8[8] = Lang.menu[82] + array10[8];
																																							array8[9] = Lang.menu[83] + array10[9];
																																							array8[10] = Lang.menu[84] + array10[10];
																																							array8[11] = Lang.menu[85] + array10[11];
																																							for (int num20 = 0; num20 < 12; num20++)
																																							{
																																								array7[num20] = true;
																																								array6[num20] = 0.55f;
																																								array4[num20] = -80;
																																							}
																																							array6[12] = 0.8f;
																																							array6[13] = 0.8f;
																																							array3[12] = 6;
																																							array8[12] = Lang.menu[86];
																																							array3[13] = 16;
																																							array8[13] = Lang.menu[5];
																																							if (this.selectedMenu == 13)
																																							{
																																								Main.menuMode = 11;
																																								Main.PlaySound(11, -1, -1, 1);
																																							}
																																							else
																																							{
																																								if (this.selectedMenu == 12)
																																								{
																																									Main.cUp = "W";
																																									Main.cDown = "S";
																																									Main.cLeft = "A";
																																									Main.cRight = "D";
																																									Main.cJump = "Space";
																																									Main.cThrowItem = "T";
																																									Main.cInv = "Escape";
																																									Main.cHeal = "H";
																																									Main.cMana = "M";
																																									Main.cBuff = "B";
																																									Main.cHook = "E";
																																									Main.cTorch = "LeftShift";
																																									this.setKey = -1;
																																									Main.PlaySound(11, -1, -1, 1);
																																								}
																																								else
																																								{
																																									if (this.selectedMenu >= 0)
																																									{
																																										this.setKey = this.selectedMenu;
																																									}
																																								}
																																							}
																																							if (this.setKey >= 0)
																																							{
																																								Keys[] pressedKeys = Main.keyState.GetPressedKeys();
																																								if (pressedKeys.Length > 0)
																																								{
																																									string a2 = string.Concat(pressedKeys[0]);
																																									if (a2 != "None")
																																									{
																																										if (this.setKey == 0)
																																										{
																																											Main.cUp = a2;
																																										}
																																										if (this.setKey == 1)
																																										{
																																											Main.cDown = a2;
																																										}
																																										if (this.setKey == 2)
																																										{
																																											Main.cLeft = a2;
																																										}
																																										if (this.setKey == 3)
																																										{
																																											Main.cRight = a2;
																																										}
																																										if (this.setKey == 4)
																																										{
																																											Main.cJump = a2;
																																										}
																																										if (this.setKey == 5)
																																										{
																																											Main.cThrowItem = a2;
																																										}
																																										if (this.setKey == 6)
																																										{
																																											Main.cInv = a2;
																																										}
																																										if (this.setKey == 7)
																																										{
																																											Main.cHeal = a2;
																																										}
																																										if (this.setKey == 8)
																																										{
																																											Main.cMana = a2;
																																										}
																																										if (this.setKey == 9)
																																										{
																																											Main.cBuff = a2;
																																										}
																																										if (this.setKey == 10)
																																										{
																																											Main.cHook = a2;
																																										}
																																										if (this.setKey == 11)
																																										{
																																											Main.cTorch = a2;
																																										}
																																										this.setKey = -1;
																																									}
																																								}
																																							}
																																						}
																																						else
																																						{
																																							if (Main.menuMode == 12)
																																							{
																																								Main.menuServer = false;
																																								array8[0] = Lang.menu[87];
																																								array8[1] = Lang.menu[88];
																																								array8[2] = Lang.menu[5];
																																								if (this.selectedMenu == 0)
																																								{
																																									Main.LoadPlayers();
																																									Main.menuMultiplayer = true;
																																									Main.PlaySound(10, -1, -1, 1);
																																									Main.menuMode = 1;
																																								}
																																								else
																																								{
																																									if (this.selectedMenu == 1)
																																									{
																																										Main.LoadPlayers();
																																										Main.PlaySound(10, -1, -1, 1);
																																										Main.menuMode = 1;
																																										Main.menuMultiplayer = true;
																																										Main.menuServer = true;
																																									}
																																								}
																																								if (this.selectedMenu == 2)
																																								{
																																									Main.PlaySound(11, -1, -1, 1);
																																									Main.menuMode = 0;
																																								}
																																								num4 = 3;
																																							}
																																							else
																																							{
																																								if (Main.menuMode == 13)
																																								{
																																									string a3 = Main.getIP;
																																									Main.getIP = Main.GetInputText(Main.getIP);
																																									if (a3 != Main.getIP)
																																									{
																																										Main.PlaySound(12, -1, -1, 1);
																																									}
																																									array8[0] = Lang.menu[89];
																																									array2[9] = true;
																																									if (Main.getIP != "")
																																									{
																																										if (Main.getIP.Substring(0, 1) == " ")
																																										{
																																											Main.getIP = "";
																																										}
																																										for (int num21 = 0; num21 < Main.getIP.Length; num21++)
																																										{
																																											if (Main.getIP != " ")
																																											{
																																												array2[9] = false;
																																											}
																																										}
																																									}
																																									this.textBlinkerCount++;
																																									if (this.textBlinkerCount >= 20)
																																									{
																																										if (this.textBlinkerState == 0)
																																										{
																																											this.textBlinkerState = 1;
																																										}
																																										else
																																										{
																																											this.textBlinkerState = 0;
																																										}
																																										this.textBlinkerCount = 0;
																																									}
																																									array8[1] = Main.getIP;
																																									if (this.textBlinkerState == 1)
																																									{
																																										string[] array9;
																																										(array9 = array8)[1] = array9[1] + "|";
																																										array4[1] = 1;
																																									}
																																									else
																																									{
																																										string[] array9;
																																										(array9 = array8)[1] = array9[1] + " ";
																																									}
																																									array[0] = true;
																																									array[1] = true;
																																									array3[9] = 44;
																																									array3[10] = 64;
																																									array8[9] = Lang.menu[4];
																																									array8[10] = Lang.menu[5];
																																									num4 = 11;
																																									num = 180;
																																									num3 = 30;
																																									array3[1] = 19;
																																									for (int num22 = 2; num22 < 9; num22++)
																																									{
																																										int num23 = num22 - 2;
																																										if (Main.recentWorld[num23] != null && Main.recentWorld[num23] != "")
																																										{
																																											array8[num22] = string.Concat(new object[]
																																											{
																																												Main.recentWorld[num23],
																																												" (",
																																												Main.recentIP[num23],
																																												":",
																																												Main.recentPort[num23],
																																												")"
																																											});
																																										}
																																										else
																																										{
																																											array8[num22] = "";
																																											array[num22] = true;
																																										}
																																										array6[num22] = 0.6f;
																																										array3[num22] = 40;
																																									}
																																									if (this.selectedMenu >= 2 && this.selectedMenu < 9)
																																									{
																																										Main.autoPass = false;
																																										int num24 = this.selectedMenu - 2;
																																										Netplay.serverPort = Main.recentPort[num24];
																																										Main.getIP = Main.recentIP[num24];
																																										if (Netplay.SetIP(Main.getIP))
																																										{
																																											Main.menuMode = 10;
																																											Netplay.StartClient();
																																										}
																																										else
																																										{
																																											if (Netplay.SetIP2(Main.getIP))
																																											{
																																												Main.menuMode = 10;
																																												Netplay.StartClient();
																																											}
																																										}
																																									}
																																									if (this.selectedMenu == 10)
																																									{
																																										Main.PlaySound(11, -1, -1, 1);
																																										Main.menuMode = 1;
																																									}
																																									if (this.selectedMenu == 9 || (!array2[2] && Main.inputTextEnter))
																																									{
																																										Main.PlaySound(12, -1, -1, 1);
																																										Main.menuMode = 131;
																																										Main.clrInput();
																																									}
																																								}
																																								else
																																								{
																																									if (Main.menuMode == 131)
																																									{
																																										int num25 = 7777;
																																										string a4 = Main.getPort;
																																										Main.getPort = Main.GetInputText(Main.getPort);
																																										if (a4 != Main.getPort)
																																										{
																																											Main.PlaySound(12, -1, -1, 1);
																																										}
																																										array8[0] = Lang.menu[90];
																																										array2[2] = true;
																																										if (Main.getPort != "")
																																										{
																																											bool flag5 = false;
																																											try
																																											{
																																												num25 = Convert.ToInt32(Main.getPort);
																																												if (num25 > 0 && num25 <= 65535)
																																												{
																																													flag5 = true;
																																												}
																																											}
																																											catch
																																											{
																																											}
																																											if (flag5)
																																											{
																																												array2[2] = false;
																																											}
																																										}
																																										this.textBlinkerCount++;
																																										if (this.textBlinkerCount >= 20)
																																										{
																																											if (this.textBlinkerState == 0)
																																											{
																																												this.textBlinkerState = 1;
																																											}
																																											else
																																											{
																																												this.textBlinkerState = 0;
																																											}
																																											this.textBlinkerCount = 0;
																																										}
																																										array8[1] = Main.getPort;
																																										if (this.textBlinkerState == 1)
																																										{
																																											string[] array9;
																																											(array9 = array8)[1] = array9[1] + "|";
																																											array4[1] = 1;
																																										}
																																										else
																																										{
																																											string[] array9;
																																											(array9 = array8)[1] = array9[1] + " ";
																																										}
																																										array[0] = true;
																																										array[1] = true;
																																										array3[1] = -20;
																																										array3[2] = 20;
																																										array8[2] = Lang.menu[4];
																																										array8[3] = Lang.menu[5];
																																										num4 = 4;
																																										if (this.selectedMenu == 3)
																																										{
																																											Main.PlaySound(11, -1, -1, 1);
																																											Main.menuMode = 1;
																																										}
																																										if (this.selectedMenu == 2 || (!array2[2] && Main.inputTextEnter))
																																										{
																																											Netplay.serverPort = num25;
																																											Main.autoPass = false;
																																											if (Netplay.SetIP(Main.getIP))
																																											{
																																												Main.menuMode = 10;
																																												Netplay.StartClient();
																																											}
																																											else
																																											{
																																												if (Netplay.SetIP2(Main.getIP))
																																												{
																																													Main.menuMode = 10;
																																													Netplay.StartClient();
																																												}
																																											}
																																										}
																																									}
																																									else
																																									{
																																										if (Main.menuMode == 16)
																																										{
																																											num = 200;
																																											num3 = 60;
																																											array3[1] = 30;
																																											array3[2] = 30;
																																											array3[3] = 30;
																																											array3[4] = 70;
																																											array8[0] = Lang.menu[91];
																																											array[0] = true;
																																											array8[1] = Lang.menu[92];
																																											array8[2] = Lang.menu[93];
																																											array8[3] = Lang.menu[94];
																																											array8[4] = Lang.menu[5];
																																											num4 = 5;
																																											if (this.selectedMenu == 4)
																																											{
																																												Main.menuMode = 6;
																																												Main.PlaySound(11, -1, -1, 1);
																																											}
																																											else
																																											{
																																												if (this.selectedMenu > 0)
																																												{
																																													if (this.selectedMenu == 1)
																																													{
																																														Main.maxTilesX = 4200;
																																														Main.maxTilesY = 1200;
																																													}
																																													else
																																													{
																																														if (this.selectedMenu == 2)
																																														{
																																															Main.maxTilesX = 6400;
																																															Main.maxTilesY = 1800;
																																														}
																																														else
																																														{
																																															Main.maxTilesX = 8400;
																																															Main.maxTilesY = 2400;
																																														}
																																													}
																																													Main.clrInput();
																																													Main.menuMode = 7;
																																													Main.PlaySound(10, -1, -1, 1);
																																													WorldGen.setWorldSize();
																																												}
																																											}
																																										}
																																									}
																																								}
																																							}
																																						}
																																					}
																																				}
																																			}
																																		}
																																	}
																																}
																															}
																														}
																													}
																												}
																											}
																										}
																									}
																								}
																							}
																						}
																					}
																				}
																			}
																		}
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			IL_3B38:
			if (Main.menuMode != num5)
			{
				num4 = 0;
				for (int num26 = 0; num26 < Main.maxMenuItems; num26++)
				{
					this.menuItemScale[num26] = 0.8f;
				}
			}
			int num27 = this.focusMenu;
			this.selectedMenu = -1;
			this.selectedMenu2 = -1;
			this.focusMenu = -1;
			for (int num28 = 0; num28 < num4; num28++)
			{
				if (array8[num28] != null)
				{
					if (flag)
					{
						string text = "";
						for (int num29 = 0; num29 < 6; num29++)
						{
							int num30 = num9;
							int num31 = 370 + Main.screenWidth / 2 - 400;
							if (num29 == 0)
							{
								text = Lang.menu[95];
							}
							if (num29 == 1)
							{
								text = Lang.menu[96];
								num30 += 30;
							}
							if (num29 == 2)
							{
								text = Lang.menu[97];
								num30 += 60;
							}
							if (num29 == 3)
							{
								text = string.Concat(this.selColor.R);
								num31 += 90;
							}
							if (num29 == 4)
							{
								text = string.Concat(this.selColor.G);
								num31 += 90;
								num30 += 30;
							}
							if (num29 == 5)
							{
								text = string.Concat(this.selColor.B);
								num31 += 90;
								num30 += 60;
							}
							for (int num32 = 0; num32 < 5; num32++)
							{
								Color color4 = Color.Black;
								if (num32 == 4)
								{
									color4 = color;
									color4.R = (byte)((255 + color4.R) / 2);
									color4.G = (byte)((255 + color4.R) / 2);
									color4.B = (byte)((255 + color4.R) / 2);
								}
								int num33 = 255;
								int num34 = (int)color4.R - (255 - num33);
								if (num34 < 0)
								{
									num34 = 0;
								}
								color4 = new Color((int)((byte)num34), (int)((byte)num34), (int)((byte)num34), (int)((byte)num33));
								int num35 = 0;
								int num36 = 0;
								if (num32 == 0)
								{
									num35 = -2;
								}
								if (num32 == 1)
								{
									num35 = 2;
								}
								if (num32 == 2)
								{
									num36 = -2;
								}
								if (num32 == 3)
								{
									num36 = 2;
								}
								this.spriteBatch.DrawString(Main.fontDeathText, text, new Vector2((float)(num31 + num35), (float)(num30 + num36)), color4, 0f, default(Vector2), 0.5f, SpriteEffects.None, 0f);
							}
						}
						bool flag6 = false;
						for (int num37 = 0; num37 < 2; num37++)
						{
							for (int num38 = 0; num38 < 3; num38++)
							{
								int num39 = num9 + num38 * 30 - 12;
								int num40 = 360 + Main.screenWidth / 2 - 400;
								float scale = 0.9f;
								if (num37 == 0)
								{
									num40 -= 70;
									num39 += 2;
								}
								else
								{
									num40 -= 40;
								}
								text = "-";
								if (num37 == 1)
								{
									text = "+";
								}
								Vector2 vector3 = new Vector2(24f, 24f);
								int num41 = 142;
								if (Main.mouseX > num40 && (float)Main.mouseX < (float)num40 + vector3.X && Main.mouseY > num39 + 13 && (float)Main.mouseY < (float)(num39 + 13) + vector3.Y)
								{
									if (this.focusColor != (num37 + 1) * (num38 + 10))
									{
										Main.PlaySound(12, -1, -1, 1);
									}
									this.focusColor = (num37 + 1) * (num38 + 10);
									flag6 = true;
									num41 = 255;
									if (Main.mouseLeft)
									{
										if (this.colorDelay <= 1)
										{
											if (this.colorDelay == 0)
											{
												this.colorDelay = 40;
											}
											else
											{
												this.colorDelay = 3;
											}
											int num42 = num37;
											if (num37 == 0)
											{
												num42 = -1;
												if (this.selColor.R + this.selColor.G + this.selColor.B <= 150)
												{
													num42 = 0;
												}
											}
											if (num38 == 0 && (int)this.selColor.R + num42 >= 0 && (int)this.selColor.R + num42 <= 255)
											{
												this.selColor.R = (byte)((int)this.selColor.R + num42);
											}
											if (num38 == 1 && (int)this.selColor.G + num42 >= 0 && (int)this.selColor.G + num42 <= 255)
											{
												this.selColor.G = (byte)((int)this.selColor.G + num42);
											}
											if (num38 == 2 && (int)this.selColor.B + num42 >= 0 && (int)this.selColor.B + num42 <= 255)
											{
												this.selColor.B = (byte)((int)this.selColor.B + num42);
											}
										}
										this.colorDelay--;
									}
									else
									{
										this.colorDelay = 0;
									}
								}
								for (int num43 = 0; num43 < 5; num43++)
								{
									Color color5 = Color.Black;
									if (num43 == 4)
									{
										color5 = color;
										color5.R = (byte)((255 + color5.R) / 2);
										color5.G = (byte)((255 + color5.R) / 2);
										color5.B = (byte)((255 + color5.R) / 2);
									}
									int num44 = (int)color5.R - (255 - num41);
									if (num44 < 0)
									{
										num44 = 0;
									}
									color5 = new Color((int)((byte)num44), (int)((byte)num44), (int)((byte)num44), (int)((byte)num41));
									int num45 = 0;
									int num46 = 0;
									if (num43 == 0)
									{
										num45 = -2;
									}
									if (num43 == 1)
									{
										num45 = 2;
									}
									if (num43 == 2)
									{
										num46 = -2;
									}
									if (num43 == 3)
									{
										num46 = 2;
									}
									this.spriteBatch.DrawString(Main.fontDeathText, text, new Vector2((float)(num40 + num45), (float)(num39 + num46)), color5, 0f, default(Vector2), scale, SpriteEffects.None, 0f);
								}
							}
						}
						if (!flag6)
						{
							this.focusColor = 0;
							this.colorDelay = 0;
						}
					}
					if (flag3)
					{
						int num47 = 400;
						string text2 = "";
						for (int num48 = 0; num48 < 4; num48++)
						{
							int num49 = num47;
							int num50 = 370 + Main.screenWidth / 2 - 400;
							if (num48 == 0)
							{
								text2 = Lang.menu[52] + ": " + this.bgScroll;
							}
							for (int num51 = 0; num51 < 5; num51++)
							{
								Color color6 = Color.Black;
								if (num51 == 4)
								{
									color6 = color;
									color6.R = (byte)((255 + color6.R) / 2);
									color6.G = (byte)((255 + color6.R) / 2);
									color6.B = (byte)((255 + color6.R) / 2);
								}
								int num52 = 255;
								int num53 = (int)color6.R - (255 - num52);
								if (num53 < 0)
								{
									num53 = 0;
								}
								color6 = new Color((int)((byte)num53), (int)((byte)num53), (int)((byte)num53), (int)((byte)num52));
								int num54 = 0;
								int num55 = 0;
								if (num51 == 0)
								{
									num54 = -2;
								}
								if (num51 == 1)
								{
									num54 = 2;
								}
								if (num51 == 2)
								{
									num55 = -2;
								}
								if (num51 == 3)
								{
									num55 = 2;
								}
								this.spriteBatch.DrawString(Main.fontDeathText, text2, new Vector2((float)(num50 + num54), (float)(num49 + num55)), color6, 0f, default(Vector2), 0.5f, SpriteEffects.None, 0f);
							}
						}
						bool flag7 = false;
						for (int num56 = 0; num56 < 2; num56++)
						{
							for (int num57 = 0; num57 < 1; num57++)
							{
								int num58 = num47 + num57 * 30 - 12;
								int num59 = 360 + Main.screenWidth / 2 - 400;
								float scale2 = 0.9f;
								if (num56 == 0)
								{
									num59 -= 70;
									num58 += 2;
								}
								else
								{
									num59 -= 40;
								}
								text2 = "-";
								if (num56 == 1)
								{
									text2 = "+";
								}
								Vector2 vector4 = new Vector2(24f, 24f);
								int num60 = 142;
								if (Main.mouseX > num59 && (float)Main.mouseX < (float)num59 + vector4.X && Main.mouseY > num58 + 13 && (float)Main.mouseY < (float)(num58 + 13) + vector4.Y)
								{
									if (this.focusColor != (num56 + 1) * (num57 + 10))
									{
										Main.PlaySound(12, -1, -1, 1);
									}
									this.focusColor = (num56 + 1) * (num57 + 10);
									flag7 = true;
									num60 = 255;
									if (Main.mouseLeft)
									{
										if (this.colorDelay <= 1)
										{
											if (this.colorDelay == 0)
											{
												this.colorDelay = 40;
											}
											else
											{
												this.colorDelay = 3;
											}
											int num61 = (num56 != null) ? num56 : -1;
											if (num57 == 0)
											{
												this.bgScroll += num61;
												if (this.bgScroll > 100)
												{
													this.bgScroll = 100;
												}
												if (this.bgScroll < 0)
												{
													this.bgScroll = 0;
												}
											}
										}
										this.colorDelay--;
									}
									else
									{
										this.colorDelay = 0;
									}
								}
								for (int num62 = 0; num62 < 5; num62++)
								{
									Color color7 = Color.Black;
									if (num62 == 4)
									{
										color7 = color;
										color7.R = (byte)((255 + color7.R) / 2);
										color7.G = (byte)((255 + color7.R) / 2);
										color7.B = (byte)((255 + color7.R) / 2);
									}
									int num63 = (int)color7.R - (255 - num60);
									if (num63 < 0)
									{
										num63 = 0;
									}
									color7 = new Color((int)((byte)num63), (int)((byte)num63), (int)((byte)num63), (int)((byte)num60));
									int num64 = 0;
									int num65 = 0;
									if (num62 == 0)
									{
										num64 = -2;
									}
									if (num62 == 1)
									{
										num64 = 2;
									}
									if (num62 == 2)
									{
										num65 = -2;
									}
									if (num62 == 3)
									{
										num65 = 2;
									}
									this.spriteBatch.DrawString(Main.fontDeathText, text2, new Vector2((float)(num59 + num64), (float)(num58 + num65)), color7, 0f, default(Vector2), scale2, SpriteEffects.None, 0f);
								}
							}
						}
						if (!flag7)
						{
							this.focusColor = 0;
							this.colorDelay = 0;
						}
					}
					if (flag2)
					{
						int num66 = 400;
						string text3 = "";
						for (int num67 = 0; num67 < 4; num67++)
						{
							int num68 = num66;
							int num69 = 370 + Main.screenWidth / 2 - 400;
							if (num67 == 0)
							{
								text3 = Lang.menu[98];
							}
							if (num67 == 1)
							{
								text3 = Lang.menu[99];
								num68 += 30;
							}
							if (num67 == 2)
							{
								text3 = Math.Round((double)(Main.soundVolume * 100f)) + "%";
								num69 += 90;
							}
							if (num67 == 3)
							{
								text3 = Math.Round((double)(Main.musicVolume * 100f)) + "%";
								num69 += 90;
								num68 += 30;
							}
							for (int num70 = 0; num70 < 5; num70++)
							{
								Color color8 = Color.Black;
								if (num70 == 4)
								{
									color8 = color;
									color8.R = (byte)((255 + color8.R) / 2);
									color8.G = (byte)((255 + color8.R) / 2);
									color8.B = (byte)((255 + color8.R) / 2);
								}
								int num71 = 255;
								int num72 = (int)color8.R - (255 - num71);
								if (num72 < 0)
								{
									num72 = 0;
								}
								color8 = new Color((int)((byte)num72), (int)((byte)num72), (int)((byte)num72), (int)((byte)num71));
								int num73 = 0;
								int num74 = 0;
								if (num70 == 0)
								{
									num73 = -2;
								}
								if (num70 == 1)
								{
									num73 = 2;
								}
								if (num70 == 2)
								{
									num74 = -2;
								}
								if (num70 == 3)
								{
									num74 = 2;
								}
								this.spriteBatch.DrawString(Main.fontDeathText, text3, new Vector2((float)(num69 + num73), (float)(num68 + num74)), color8, 0f, default(Vector2), 0.5f, SpriteEffects.None, 0f);
							}
						}
						bool flag8 = false;
						for (int num75 = 0; num75 < 2; num75++)
						{
							for (int num76 = 0; num76 < 2; num76++)
							{
								int num77 = num66 + num76 * 30 - 12;
								int num78 = 360 + Main.screenWidth / 2 - 400;
								float scale3 = 0.9f;
								if (num75 == 0)
								{
									num78 -= 70;
									num77 += 2;
								}
								else
								{
									num78 -= 40;
								}
								text3 = "-";
								if (num75 == 1)
								{
									text3 = "+";
								}
								Vector2 vector5 = new Vector2(24f, 24f);
								int num79 = 142;
								if (Main.mouseX > num78 && (float)Main.mouseX < (float)num78 + vector5.X && Main.mouseY > num77 + 13 && (float)Main.mouseY < (float)(num77 + 13) + vector5.Y)
								{
									if (this.focusColor != (num75 + 1) * (num76 + 10))
									{
										Main.PlaySound(12, -1, -1, 1);
									}
									this.focusColor = (num75 + 1) * (num76 + 10);
									flag8 = true;
									num79 = 255;
									if (Main.mouseLeft)
									{
										if (this.colorDelay <= 1)
										{
											if (this.colorDelay == 0)
											{
												this.colorDelay = 40;
											}
											else
											{
												this.colorDelay = 3;
											}
											int num80 = (num75 != null) ? num75 : -1;
											if (num76 == 0)
											{
												Main.soundVolume += (float)num80 * 0.01f;
												if (Main.soundVolume > 1f)
												{
													Main.soundVolume = 1f;
												}
												if (Main.soundVolume < 0f)
												{
													Main.soundVolume = 0f;
												}
											}
											if (num76 == 1)
											{
												Main.musicVolume += (float)num80 * 0.01f;
												if (Main.musicVolume > 1f)
												{
													Main.musicVolume = 1f;
												}
												if (Main.musicVolume < 0f)
												{
													Main.musicVolume = 0f;
												}
											}
										}
										this.colorDelay--;
									}
									else
									{
										this.colorDelay = 0;
									}
								}
								for (int num81 = 0; num81 < 5; num81++)
								{
									Color color9 = Color.Black;
									if (num81 == 4)
									{
										color9 = color;
										color9.R = (byte)((255 + color9.R) / 2);
										color9.G = (byte)((255 + color9.R) / 2);
										color9.B = (byte)((255 + color9.R) / 2);
									}
									int num82 = (int)color9.R - (255 - num79);
									if (num82 < 0)
									{
										num82 = 0;
									}
									color9 = new Color((int)((byte)num82), (int)((byte)num82), (int)((byte)num82), (int)((byte)num79));
									int num83 = 0;
									int num84 = 0;
									if (num81 == 0)
									{
										num83 = -2;
									}
									if (num81 == 1)
									{
										num83 = 2;
									}
									if (num81 == 2)
									{
										num84 = -2;
									}
									if (num81 == 3)
									{
										num84 = 2;
									}
									this.spriteBatch.DrawString(Main.fontDeathText, text3, new Vector2((float)(num78 + num83), (float)(num77 + num84)), color9, 0f, default(Vector2), scale3, SpriteEffects.None, 0f);
								}
							}
						}
						if (!flag8)
						{
							this.focusColor = 0;
							this.colorDelay = 0;
						}
					}
					for (int num85 = 0; num85 < 5; num85++)
					{
						Color color10 = Color.Black;
						if (num85 == 4)
						{
							color10 = color;
							if (array5[num28] == 2)
							{
								color10 = Main.hcColor;
							}
							else
							{
								if (array5[num28] == 1)
								{
									color10 = Main.mcColor;
								}
							}
							color10.R = (byte)((255 + color10.R) / 2);
							color10.G = (byte)((255 + color10.G) / 2);
							color10.B = (byte)((255 + color10.B) / 2);
						}
						int num86 = (int)(255f * (this.menuItemScale[num28] * 2f - 1f));
						if (array[num28])
						{
							num86 = 255;
						}
						int num87 = (int)color10.R - (255 - num86);
						if (num87 < 0)
						{
							num87 = 0;
						}
						int num88 = (int)color10.G - (255 - num86);
						if (num88 < 0)
						{
							num88 = 0;
						}
						int num89 = (int)color10.B - (255 - num86);
						if (num89 < 0)
						{
							num89 = 0;
						}
						color10 = new Color((int)((byte)num87), (int)((byte)num88), (int)((byte)num89), (int)((byte)num86));
						int num90 = 0;
						int num91 = 0;
						if (num85 == 0)
						{
							num90 = -2;
						}
						if (num85 == 1)
						{
							num90 = 2;
						}
						if (num85 == 2)
						{
							num91 = -2;
						}
						if (num85 == 3)
						{
							num91 = 2;
						}
						Vector2 origin = Main.fontDeathText.MeasureString(array8[num28]);
						origin.X *= 0.5f;
						origin.Y *= 0.5f;
						float num92 = this.menuItemScale[num28];
						if (Main.menuMode == 15 && num28 == 0)
						{
							num92 *= 0.35f;
						}
						else
						{
							if (Main.netMode == 2)
							{
								num92 *= 0.5f;
							}
						}
						num92 *= array6[num28];
						if (!array7[num28])
						{
							this.spriteBatch.DrawString(Main.fontDeathText, array8[num28], new Vector2((float)(num2 + num90 + array4[num28]), (float)(num + num3 * num28 + num91) + origin.Y * array6[num28] + (float)array3[num28]), color10, 0f, origin, num92, SpriteEffects.None, 0f);
						}
						else
						{
							this.spriteBatch.DrawString(Main.fontDeathText, array8[num28], new Vector2((float)(num2 + num90 + array4[num28]), (float)(num + num3 * num28 + num91) + origin.Y * array6[num28] + (float)array3[num28]), color10, 0f, new Vector2(0f, origin.Y), num92, SpriteEffects.None, 0f);
						}
					}
					if (!array7[num28])
					{
						if ((float)Main.mouseX > (float)num2 - (float)(array8[num28].Length * 10) * array6[num28] + (float)array4[num28] && (float)Main.mouseX < (float)num2 + (float)(array8[num28].Length * 10) * array6[num28] + (float)array4[num28] && Main.mouseY > num + num3 * num28 + array3[num28] && (float)Main.mouseY < (float)(num + num3 * num28 + array3[num28]) + 50f * array6[num28] && Main.hasFocus)
						{
							this.focusMenu = num28;
							if (array[num28] || array2[num28])
							{
								this.focusMenu = -1;
							}
							else
							{
								if (num27 != this.focusMenu)
								{
									Main.PlaySound(12, -1, -1, 1);
								}
								if (Main.mouseLeftRelease && Main.mouseLeft)
								{
									this.selectedMenu = num28;
								}
								if (Main.mouseRightRelease && Main.mouseRight)
								{
									this.selectedMenu2 = num28;
								}
							}
						}
					}
					else
					{
						if (Main.mouseX > num2 + array4[num28] && (float)Main.mouseX < (float)num2 + (float)(array8[num28].Length * 20) * array6[num28] + (float)array4[num28] && Main.mouseY > num + num3 * num28 + array3[num28] && (float)Main.mouseY < (float)(num + num3 * num28 + array3[num28]) + 50f * array6[num28] && Main.hasFocus)
						{
							this.focusMenu = num28;
							if (array[num28] || array2[num28])
							{
								this.focusMenu = -1;
							}
							else
							{
								if (num27 != this.focusMenu)
								{
									Main.PlaySound(12, -1, -1, 1);
								}
								if (Main.mouseLeftRelease && Main.mouseLeft)
								{
									this.selectedMenu = num28;
								}
								if (Main.mouseRightRelease && Main.mouseRight)
								{
									this.selectedMenu2 = num28;
								}
							}
						}
					}
				}
			}
			for (int num93 = 0; num93 < Main.maxMenuItems; num93++)
			{
				if (num93 == this.focusMenu)
				{
					if (this.menuItemScale[num93] < 1f)
					{
						this.menuItemScale[num93] += 0.02f;
					}
					if (this.menuItemScale[num93] > 1f)
					{
						this.menuItemScale[num93] = 1f;
					}
				}
				else
				{
					if ((double)this.menuItemScale[num93] > 0.8)
					{
						this.menuItemScale[num93] -= 0.02f;
					}
				}
			}
			if (num6 >= 0)
			{
				Main.loadPlayer[num6].PlayerFrame();
				Main.loadPlayer[num6].position.X = (float)num7 + Main.screenPosition.X;
				Main.loadPlayer[num6].position.Y = (float)num8 + Main.screenPosition.Y;
				this.DrawPlayer(Main.loadPlayer[num6]);
			}
			for (int num94 = 0; num94 < 5; num94++)
			{
				Color color11 = Color.Black;
				if (num94 == 4)
				{
					color11 = color;
					color11.R = (byte)((255 + color11.R) / 2);
					color11.G = (byte)((255 + color11.R) / 2);
					color11.B = (byte)((255 + color11.R) / 2);
				}
				color11.A = (byte)((float)color11.A * 0.3f);
				int num95 = 0;
				int num96 = 0;
				if (num94 == 0)
				{
					num95 = -2;
				}
				if (num94 == 1)
				{
					num95 = 2;
				}
				if (num94 == 2)
				{
					num96 = -2;
				}
				if (num94 == 3)
				{
					num96 = 2;
				}
				string text4 = "Copyright © 2012 Re-Logic";
				Vector2 origin2 = Main.fontMouseText.MeasureString(text4);
				origin2.X *= 0.5f;
				origin2.Y *= 0.5f;
				this.spriteBatch.DrawString(Main.fontMouseText, text4, new Vector2((float)Main.screenWidth - origin2.X + (float)num95 - 10f, (float)Main.screenHeight - origin2.Y + (float)num96 - 2f), color11, 0f, origin2, 1f, SpriteEffects.None, 0f);
			}
			for (int num97 = 0; num97 < 5; num97++)
			{
				Color color12 = Color.Black;
				if (num97 == 4)
				{
					color12 = color;
					color12.R = (byte)((255 + color12.R) / 2);
					color12.G = (byte)((255 + color12.R) / 2);
					color12.B = (byte)((255 + color12.R) / 2);
				}
				color12.A = (byte)((float)color12.A * 0.3f);
				int num98 = 0;
				int num99 = 0;
				if (num97 == 0)
				{
					num98 = -2;
				}
				if (num97 == 1)
				{
					num98 = 2;
				}
				if (num97 == 2)
				{
					num99 = -2;
				}
				if (num97 == 3)
				{
					num99 = 2;
				}
				Vector2 origin3 = Main.fontMouseText.MeasureString(Main.versionNumber);
				origin3.X *= 0.5f;
				origin3.Y *= 0.5f;
				this.spriteBatch.DrawString(Main.fontMouseText, Main.versionNumber, new Vector2(origin3.X + (float)num98 + 10f, (float)Main.screenHeight - origin3.Y + (float)num99 - 2f), color12, 0f, origin3, 1f, SpriteEffects.None, 0f);
			}
			this.spriteBatch.Draw(Main.cursorTexture, new Vector2((float)(Main.mouseX + 1), (float)(Main.mouseY + 1)), new Rectangle?(new Rectangle(0, 0, Main.cursorTexture.Width, Main.cursorTexture.Height)), new Color((int)((float)Main.cursorColor.R * 0.2f), (int)((float)Main.cursorColor.G * 0.2f), (int)((float)Main.cursorColor.B * 0.2f), (int)((float)Main.cursorColor.A * 0.5f)), 0f, default(Vector2), Main.cursorScale * 1.1f, SpriteEffects.None, 0f);
			this.spriteBatch.Draw(Main.cursorTexture, new Vector2((float)Main.mouseX, (float)Main.mouseY), new Rectangle?(new Rectangle(0, 0, Main.cursorTexture.Width, Main.cursorTexture.Height)), Main.cursorColor, 0f, default(Vector2), Main.cursorScale, SpriteEffects.None, 0f);
			if (Main.fadeCounter > 0)
			{
				Color white = Color.White;
				Main.fadeCounter--;
				float num100 = (float)Main.fadeCounter / 75f * 255f;
				byte b2 = (byte)num100;
				white = new Color((int)b2, (int)b2, (int)b2, (int)b2);
				this.spriteBatch.Draw(Main.fadeTexture, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), white);
			}
			this.spriteBatch.End();
			if (Main.mouseLeft)
			{
				Main.mouseLeftRelease = false;
			}
			else
			{
				Main.mouseLeftRelease = true;
			}
			if (Main.mouseRight)
			{
				Main.mouseRightRelease = false;
				return;
			}
			Main.mouseRightRelease = true;
		}
		public static void CursorColor()
		{
			Main.cursorAlpha += (float)Main.cursorColorDirection * 0.015f;
			if (Main.cursorAlpha >= 1f)
			{
				Main.cursorAlpha = 1f;
				Main.cursorColorDirection = -1;
			}
			if ((double)Main.cursorAlpha <= 0.6)
			{
				Main.cursorAlpha = 0.6f;
				Main.cursorColorDirection = 1;
			}
			float num = Main.cursorAlpha * 0.3f + 0.7f;
			byte r = (byte)((float)Main.mouseColor.R * Main.cursorAlpha);
			byte g = (byte)((float)Main.mouseColor.G * Main.cursorAlpha);
			byte b = (byte)((float)Main.mouseColor.B * Main.cursorAlpha);
			byte a = (byte)(255f * num);
			Main.cursorColor = new Color((int)r, (int)g, (int)b, (int)a);
			Main.cursorScale = Main.cursorAlpha * 0.3f + 0.7f + 0.1f;
		}
		protected void DrawSplash(GameTime gameTime)
		{
			base.GraphicsDevice.Clear(Color.Black);
			base.Draw(gameTime);
			this.spriteBatch.Begin();
			this.splashCounter++;
			Color white = Color.White;
			byte b = 0;
			if (this.splashCounter <= 75)
			{
				float num = (float)this.splashCounter / 75f * 255f;
				b = (byte)num;
			}
			else
			{
				if (this.splashCounter <= 125)
				{
					b = 255;
				}
				else
				{
					if (this.splashCounter <= 200)
					{
						int num2 = 125 - this.splashCounter;
						float num3 = (float)num2 / 75f * 255f;
						b = (byte)num3;
					}
					else
					{
						Main.showSplash = false;
						Main.fadeCounter = 75;
					}
				}
			}
			white = new Color((int)b, (int)b, (int)b, (int)b);
			this.spriteBatch.Draw(Main.loTexture, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), white);
			this.spriteBatch.End();
		}
		protected void DrawBackground()
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			int num = (int)(255f * (1f - Main.gfxQuality) + 140f * Main.gfxQuality);
			int num2 = (int)(200f * (1f - Main.gfxQuality) + 40f * Main.gfxQuality);
			int num3 = 96;
			Vector2 value = new Vector2((float)Main.offScreenRange, (float)Main.offScreenRange);
			if (Main.drawToScreen)
			{
				value = default(Vector2);
			}
			float num4 = 0.9f;
			float num5 = num4;
			float num6 = num4;
			float num7 = num4;
			float num8 = 0f;
			if (Main.holyTiles > Main.evilTiles)
			{
				num8 = (float)Main.holyTiles / 800f;
			}
			else
			{
				if (Main.evilTiles > Main.holyTiles)
				{
					num8 = (float)Main.evilTiles / 800f;
				}
			}
			if (num8 > 1f)
			{
				num8 = 1f;
			}
			if (num8 < 0f)
			{
				num8 = 0f;
			}
			float num9 = (float)((double)Main.screenPosition.Y - Main.worldSurface * 16.0) / 300f;
			if (num9 < 0f)
			{
				num9 = 0f;
			}
			else
			{
				if (num9 > 1f)
				{
					num9 = 1f;
				}
			}
			float num10 = 1f * (1f - num9) + num5 * num9;
			Lighting.brightness = Lighting.defBrightness * (1f - num9) + 1f * num9;
			float num11 = (float)((double)(Main.screenPosition.Y - (float)(Main.screenHeight / 2) + 200f) - Main.rockLayer * 16.0) / 300f;
			if (num11 < 0f)
			{
				num11 = 0f;
			}
			else
			{
				if (num11 > 1f)
				{
					num11 = 1f;
				}
			}
			if (Main.evilTiles > 0)
			{
				num5 = 0.8f * num8 + num5 * (1f - num8);
				num6 = 0.75f * num8 + num6 * (1f - num8);
				num7 = 1.1f * num8 + num7 * (1f - num8);
			}
			else
			{
				if (Main.holyTiles > 0)
				{
					num5 = 1f * num8 + num5 * (1f - num8);
					num6 = 0.7f * num8 + num6 * (1f - num8);
					num7 = 0.9f * num8 + num7 * (1f - num8);
				}
			}
			num5 = 1f * (num10 - num11) + num5 * num11;
			num6 = 1f * (num10 - num11) + num6 * num11;
			num7 = 1f * (num10 - num11) + num7 * num11;
			Lighting.defBrightness = 1.2f * (1f - num11) + 1f * num11;
			this.bgParrallax = (double)Main.caveParrallax;
			this.bgStart = (int)(-Math.IEEERemainder((double)Main.screenPosition.X * this.bgParrallax, (double)num3) - (double)(num3 / 2));
			this.bgLoops = Main.screenWidth / num3 + 2;
			this.bgTop = (int)((float)((int)Main.worldSurface * 16 - Main.backgroundHeight[1]) - Main.screenPosition.Y + 16f);
			for (int i = 0; i < this.bgLoops; i++)
			{
				for (int j = 0; j < 6; j++)
				{
					float num12 = (float)this.bgStart + Main.screenPosition.X;
					num12 = -(float)Math.IEEERemainder((double)num12, 16.0);
					num12 = (float)Math.Round((double)num12);
					int num13 = (int)num12;
					if (num13 == -8)
					{
						num13 = 8;
					}
					float num14 = (float)(this.bgStart + num3 * i + j * 16 + 8);
					float num15 = (float)this.bgTop;
					Color color = Lighting.GetColor((int)((num14 + Main.screenPosition.X) / 16f), (int)((Main.screenPosition.Y + num15) / 16f));
					color.R = (byte)((float)color.R * num5);
					color.G = (byte)((float)color.G * num6);
					color.B = (byte)((float)color.B * num7);
					this.spriteBatch.Draw(Main.backgroundTexture[1], new Vector2((float)(this.bgStart + num3 * i + 16 * j + num13), (float)this.bgTop) + value, new Rectangle?(new Rectangle(16 * j + num13 + 16, 0, 16, 16)), color);
				}
			}
			double num16 = (double)(Main.maxTilesY - 230);
			double num17 = (double)((int)((num16 - Main.worldSurface) / 6.0) * 6);
			num16 = Main.worldSurface + num17 - 5.0;
			bool flag = false;
			bool flag2 = false;
			this.bgTop = (int)((float)((int)Main.worldSurface * 16) - Main.screenPosition.Y + 16f);
			if (Main.worldSurface * 16.0 <= (double)(Main.screenPosition.Y + (float)Main.screenHeight + (float)Main.offScreenRange))
			{
				this.bgParrallax = (double)Main.caveParrallax;
				this.bgStart = (int)(-Math.IEEERemainder(96.0 + (double)Main.screenPosition.X * this.bgParrallax, (double)num3) - (double)(num3 / 2)) - (int)value.X;
				this.bgLoops = (Main.screenWidth + (int)value.X * 2) / num3 + 2;
				if (Main.worldSurface * 16.0 < (double)(Main.screenPosition.Y - 16f))
				{
					this.bgStartY = (int)(Math.IEEERemainder((double)this.bgTop, (double)Main.backgroundHeight[2]) - (double)Main.backgroundHeight[2]);
					this.bgLoopsY = (Main.screenHeight - this.bgStartY + (int)value.Y * 2) / Main.backgroundHeight[2] + 1;
				}
				else
				{
					this.bgStartY = this.bgTop;
					this.bgLoopsY = (Main.screenHeight - this.bgTop + (int)value.Y * 2) / Main.backgroundHeight[2] + 1;
				}
				if (Main.rockLayer * 16.0 < (double)(Main.screenPosition.Y + 600f))
				{
					this.bgLoopsY = (int)(Main.rockLayer * 16.0 - (double)Main.screenPosition.Y + 600.0 - (double)this.bgStartY) / Main.backgroundHeight[2];
					flag2 = true;
				}
				float num18 = (float)this.bgStart + Main.screenPosition.X;
				num18 = -(float)Math.IEEERemainder((double)num18, 16.0);
				num18 = (float)Math.Round((double)num18);
				int num19 = (int)num18;
				if (num19 == -8)
				{
					num19 = 8;
				}
				for (int k = 0; k < this.bgLoops; k++)
				{
					for (int l = 0; l < this.bgLoopsY; l++)
					{
						for (int m = 0; m < 6; m++)
						{
							for (int n = 0; n < 6; n++)
							{
								float num20 = (float)(this.bgStartY + l * 96 + n * 16 + 8);
								float num21 = (float)(this.bgStart + num3 * k + m * 16 + 8);
								int num22 = (int)((num21 + Main.screenPosition.X) / 16f);
								int num23 = (int)((num20 + Main.screenPosition.Y) / 16f);
								Color color2 = Lighting.GetColor(num22, num23);
								if (Main.tile[num22, num23] == null)
								{
									Main.tile[num22, num23] = new Tile();
								}
								if (color2.R > 0 || color2.G > 0 || color2.B > 0)
								{
									if (((int)color2.R > num || (double)color2.G > (double)num * 1.1 || (double)color2.B > (double)num * 1.2) && !Main.tile[num22, num23].active)
									{
										if (Main.tile[num22, num23].wall != 0)
										{
											if (Main.tile[num22, num23].wall != 21)
											{
												goto IL_B68;
											}
										}
										try
										{
											for (int num24 = 0; num24 < 9; num24++)
											{
												int num25 = 0;
												int num26 = 0;
												int width = 4;
												int height = 4;
												Color color3 = color2;
												Color color4 = color2;
												if (num24 == 0 && !Main.tile[num22 - 1, num23 - 1].active)
												{
													color4 = Lighting.GetColor(num22 - 1, num23 - 1);
												}
												if (num24 == 1)
												{
													width = 8;
													num25 = 4;
													if (!Main.tile[num22, num23 - 1].active)
													{
														color4 = Lighting.GetColor(num22, num23 - 1);
													}
												}
												if (num24 == 2)
												{
													if (!Main.tile[num22 + 1, num23 - 1].active)
													{
														color4 = Lighting.GetColor(num22 + 1, num23 - 1);
													}
													if (Main.tile[num22 + 1, num23 - 1] == null)
													{
														Main.tile[num22 + 1, num23 - 1] = new Tile();
													}
													num25 = 12;
												}
												if (num24 == 3)
												{
													if (!Main.tile[num22 - 1, num23].active)
													{
														color4 = Lighting.GetColor(num22 - 1, num23);
													}
													height = 8;
													num26 = 4;
												}
												if (num24 == 4)
												{
													width = 8;
													height = 8;
													num25 = 4;
													num26 = 4;
												}
												if (num24 == 5)
												{
													num25 = 12;
													num26 = 4;
													height = 8;
													if (!Main.tile[num22 + 1, num23].active)
													{
														color4 = Lighting.GetColor(num22 + 1, num23);
													}
												}
												if (num24 == 6)
												{
													if (!Main.tile[num22 - 1, num23 + 1].active)
													{
														color4 = Lighting.GetColor(num22 - 1, num23 + 1);
													}
													num26 = 12;
												}
												if (num24 == 7)
												{
													width = 8;
													height = 4;
													num25 = 4;
													num26 = 12;
													if (!Main.tile[num22, num23 + 1].active)
													{
														color4 = Lighting.GetColor(num22, num23 + 1);
													}
												}
												if (num24 == 8)
												{
													if (!Main.tile[num22 + 1, num23 + 1].active)
													{
														color4 = Lighting.GetColor(num22 + 1, num23 + 1);
													}
													num25 = 12;
													num26 = 12;
												}
												color3.R = (byte)((color2.R + color4.R) / 2);
												color3.G = (byte)((color2.G + color4.G) / 2);
												color3.B = (byte)((color2.B + color4.B) / 2);
												color3.R = (byte)((float)color3.R * num5);
												color3.G = (byte)((float)color3.G * num6);
												color3.B = (byte)((float)color3.B * num7);
												this.spriteBatch.Draw(Main.backgroundTexture[2], new Vector2((float)(this.bgStart + num3 * k + 16 * m + num25 + num19), (float)(this.bgStartY + Main.backgroundHeight[2] * l + 16 * n + num26)) + value, new Rectangle?(new Rectangle(16 * m + num25 + num19 + 16, 16 * n + num26, width, height)), color3);
											}
											goto IL_EEB;
										}
										catch
										{
											color2.R = (byte)((float)color2.R * num5);
											color2.G = (byte)((float)color2.G * num6);
											color2.B = (byte)((float)color2.B * num7);
											this.spriteBatch.Draw(Main.backgroundTexture[2], new Vector2((float)(this.bgStart + num3 * k + 16 * m + num19), (float)(this.bgStartY + Main.backgroundHeight[2] * l + 16 * n)) + value, new Rectangle?(new Rectangle(16 * m + num19 + 16, 16 * n, 16, 16)), color2);
											goto IL_EEB;
										}
									}
									IL_B68:
									if ((int)color2.R > num2 || (double)color2.G > (double)num2 * 1.1 || (double)color2.B > (double)num2 * 1.2)
									{
										for (int num27 = 0; num27 < 4; num27++)
										{
											int num28 = 0;
											int num29 = 0;
											Color color5 = color2;
											Color color6 = color2;
											if (num27 == 0)
											{
												if (Lighting.Brighter(num22, num23 - 1, num22 - 1, num23))
												{
													color6 = Lighting.GetColor(num22 - 1, num23);
												}
												else
												{
													color6 = Lighting.GetColor(num22, num23 - 1);
												}
											}
											if (num27 == 1)
											{
												if (Lighting.Brighter(num22, num23 - 1, num22 + 1, num23))
												{
													color6 = Lighting.GetColor(num22 + 1, num23);
												}
												else
												{
													color6 = Lighting.GetColor(num22, num23 - 1);
												}
												num28 = 8;
											}
											if (num27 == 2)
											{
												if (Lighting.Brighter(num22, num23 + 1, num22 - 1, num23))
												{
													color6 = Lighting.GetColor(num22 - 1, num23);
												}
												else
												{
													color6 = Lighting.GetColor(num22, num23 + 1);
												}
												num29 = 8;
											}
											if (num27 == 3)
											{
												if (Lighting.Brighter(num22, num23 + 1, num22 + 1, num23))
												{
													color6 = Lighting.GetColor(num22 + 1, num23);
												}
												else
												{
													color6 = Lighting.GetColor(num22, num23 + 1);
												}
												num28 = 8;
												num29 = 8;
											}
											color5.R = (byte)((color2.R + color6.R) / 2);
											color5.G = (byte)((color2.G + color6.G) / 2);
											color5.B = (byte)((color2.B + color6.B) / 2);
											color5.R = (byte)((float)color5.R * num5);
											color5.G = (byte)((float)color5.G * num6);
											color5.B = (byte)((float)color5.B * num7);
											this.spriteBatch.Draw(Main.backgroundTexture[2], new Vector2((float)(this.bgStart + num3 * k + 16 * m + num28 + num19), (float)(this.bgStartY + Main.backgroundHeight[2] * l + 16 * n + num29)) + value, new Rectangle?(new Rectangle(16 * m + num28 + num19 + 16, 16 * n + num29, 8, 8)), color5);
										}
									}
									else
									{
										color2.R = (byte)((float)color2.R * num5);
										color2.G = (byte)((float)color2.G * num6);
										color2.B = (byte)((float)color2.B * num7);
										this.spriteBatch.Draw(Main.backgroundTexture[2], new Vector2((float)(this.bgStart + num3 * k + 16 * m + num19), (float)(this.bgStartY + Main.backgroundHeight[2] * l + 16 * n)) + value, new Rectangle?(new Rectangle(16 * m + num19 + 16, 16 * n, 16, 16)), color2);
									}
								}
								else
								{
									color2.R = (byte)((float)color2.R * num5);
									color2.G = (byte)((float)color2.G * num6);
									color2.B = (byte)((float)color2.B * num7);
									this.spriteBatch.Draw(Main.backgroundTexture[2], new Vector2((float)(this.bgStart + num3 * k + 16 * m + num19), (float)(this.bgStartY + Main.backgroundHeight[2] * l + 16 * n)) + value, new Rectangle?(new Rectangle(16 * m + num19 + 16, 16 * n, 16, 16)), color2);
								}
								IL_EEB:;
							}
						}
					}
				}
				if (flag2)
				{
					this.bgParrallax = (double)Main.caveParrallax;
					this.bgStart = (int)(-Math.IEEERemainder((double)Main.screenPosition.X * this.bgParrallax, (double)num3) - (double)(num3 / 2));
					this.bgLoops = (Main.screenWidth + (int)value.X * 2) / num3 + 2;
					this.bgTop = this.bgStartY + this.bgLoopsY * Main.backgroundHeight[2];
					if (this.bgTop > -32)
					{
						for (int num30 = 0; num30 < this.bgLoops; num30++)
						{
							for (int num31 = 0; num31 < 6; num31++)
							{
								float num32 = (float)(this.bgStart + num3 * num30 + num31 * 16 + 8);
								float num33 = (float)this.bgTop;
								Color color7 = Lighting.GetColor((int)((num32 + Main.screenPosition.X) / 16f), (int)((Main.screenPosition.Y + num33) / 16f));
								color7.R = (byte)((float)color7.R * num5);
								color7.G = (byte)((float)color7.G * num6);
								color7.B = (byte)((float)color7.B * num7);
								this.spriteBatch.Draw(Main.backgroundTexture[4], new Vector2((float)(this.bgStart + num3 * num30 + 16 * num31 + num19), (float)this.bgTop) + value, new Rectangle?(new Rectangle(16 * num31 + num19 + 16, 0, 16, 16)), color7);
							}
						}
					}
				}
			}
			this.bgTop = (int)((float)((int)Main.rockLayer * 16) - Main.screenPosition.Y + 16f + 600f - 8f);
			if (Main.rockLayer * 16.0 <= (double)(Main.screenPosition.Y + 600f))
			{
				this.bgParrallax = (double)Main.caveParrallax;
				this.bgStart = (int)(-Math.IEEERemainder(96.0 + (double)Main.screenPosition.X * this.bgParrallax, (double)num3) - (double)(num3 / 2)) - (int)value.X;
				this.bgLoops = (Main.screenWidth + (int)value.X * 2) / num3 + 2;
				if (Main.rockLayer * 16.0 + (double)Main.screenHeight < (double)(Main.screenPosition.Y - 16f))
				{
					this.bgStartY = (int)(Math.IEEERemainder((double)this.bgTop, (double)Main.backgroundHeight[3]) - (double)Main.backgroundHeight[3]);
					this.bgLoopsY = (Main.screenHeight - this.bgStartY + (int)value.Y * 2) / Main.backgroundHeight[2] + 1;
				}
				else
				{
					this.bgStartY = this.bgTop;
					this.bgLoopsY = (Main.screenHeight - this.bgTop + (int)value.Y * 2) / Main.backgroundHeight[2] + 1;
				}
				if (num16 * 16.0 < (double)(Main.screenPosition.Y + 600f))
				{
					this.bgLoopsY = (int)(num16 * 16.0 - (double)Main.screenPosition.Y + 600.0 - (double)this.bgStartY) / Main.backgroundHeight[2];
					flag = true;
				}
				float num34 = (float)this.bgStart + Main.screenPosition.X;
				num34 = -(float)Math.IEEERemainder((double)num34, 16.0);
				num34 = (float)Math.Round((double)num34);
				int num35 = (int)num34;
				if (num35 == -8)
				{
					num35 = 8;
				}
				for (int num36 = 0; num36 < this.bgLoops; num36++)
				{
					for (int num37 = 0; num37 < this.bgLoopsY; num37++)
					{
						for (int num38 = 0; num38 < 6; num38++)
						{
							for (int num39 = 0; num39 < 6; num39++)
							{
								float num40 = (float)(this.bgStartY + num37 * 96 + num39 * 16 + 8);
								float num41 = (float)(this.bgStart + num3 * num36 + num38 * 16 + 8);
								int num42 = (int)((num41 + Main.screenPosition.X) / 16f);
								int num43 = (int)((num40 + Main.screenPosition.Y) / 16f);
								Color color8 = Lighting.GetColor(num42, num43);
								if (Main.tile[num42, num43] == null)
								{
									Main.tile[num42, num43] = new Tile();
								}
								bool flag3 = false;
								if (Main.caveParrallax != 0f)
								{
									if (Main.tile[num42 - 1, num43] == null)
									{
										Main.tile[num42 - 1, num43] = new Tile();
									}
									if (Main.tile[num42 + 1, num43] == null)
									{
										Main.tile[num42 + 1, num43] = new Tile();
									}
									if (Main.tile[num42, num43].wall == 0 || Main.tile[num42, num43].wall == 21 || Main.tile[num42 - 1, num43].wall == 0 || Main.tile[num42 - 1, num43].wall == 21 || Main.tile[num42 + 1, num43].wall == 0 || Main.tile[num42 + 1, num43].wall == 21)
									{
										flag3 = true;
									}
								}
								else
								{
									if (Main.tile[num42, num43].wall == 0 || Main.tile[num42, num43].wall == 21)
									{
										flag3 = true;
									}
								}
								if ((flag3 || color8.R == 0 || color8.G == 0 || color8.B == 0) && (color8.R > 0 || color8.G > 0 || color8.B > 0) && (Main.tile[num42, num43].wall == 0 || Main.tile[num42, num43].wall == 21 || Main.caveParrallax != 0f))
								{
									if (Lighting.lightMode < 2 && color8.R < 230 && color8.G < 230 && color8.B < 230)
									{
										if (((int)color8.R > num || (double)color8.G > (double)num * 1.1 || (double)color8.B > (double)num * 1.2) && !Main.tile[num42, num43].active)
										{
											for (int num44 = 0; num44 < 9; num44++)
											{
												int num45 = 0;
												int num46 = 0;
												int width2 = 4;
												int height2 = 4;
												Color color9 = color8;
												Color color10 = color8;
												if (num44 == 0 && !Main.tile[num42 - 1, num43 - 1].active)
												{
													color10 = Lighting.GetColor(num42 - 1, num43 - 1);
												}
												if (num44 == 1)
												{
													width2 = 8;
													num45 = 4;
													if (!Main.tile[num42, num43 - 1].active)
													{
														color10 = Lighting.GetColor(num42, num43 - 1);
													}
												}
												if (num44 == 2)
												{
													if (!Main.tile[num42 + 1, num43 - 1].active)
													{
														color10 = Lighting.GetColor(num42 + 1, num43 - 1);
													}
													num45 = 12;
												}
												if (num44 == 3)
												{
													if (!Main.tile[num42 - 1, num43].active)
													{
														color10 = Lighting.GetColor(num42 - 1, num43);
													}
													height2 = 8;
													num46 = 4;
												}
												if (num44 == 4)
												{
													width2 = 8;
													height2 = 8;
													num45 = 4;
													num46 = 4;
												}
												if (num44 == 5)
												{
													num45 = 12;
													num46 = 4;
													height2 = 8;
													if (!Main.tile[num42 + 1, num43].active)
													{
														color10 = Lighting.GetColor(num42 + 1, num43);
													}
												}
												if (num44 == 6)
												{
													if (!Main.tile[num42 - 1, num43 + 1].active)
													{
														color10 = Lighting.GetColor(num42 - 1, num43 + 1);
													}
													num46 = 12;
												}
												if (num44 == 7)
												{
													width2 = 8;
													height2 = 4;
													num45 = 4;
													num46 = 12;
													if (!Main.tile[num42, num43 + 1].active)
													{
														color10 = Lighting.GetColor(num42, num43 + 1);
													}
												}
												if (num44 == 8)
												{
													if (!Main.tile[num42 + 1, num43 + 1].active)
													{
														color10 = Lighting.GetColor(num42 + 1, num43 + 1);
													}
													num45 = 12;
													num46 = 12;
												}
												color9.R = (byte)((color8.R + color10.R) / 2);
												color9.G = (byte)((color8.G + color10.G) / 2);
												color9.B = (byte)((color8.B + color10.B) / 2);
												color9.R = (byte)((float)color9.R * num5);
												color9.G = (byte)((float)color9.G * num6);
												color9.B = (byte)((float)color9.B * num7);
												this.spriteBatch.Draw(Main.backgroundTexture[3], new Vector2((float)(this.bgStart + num3 * num36 + 16 * num38 + num45 + num35), (float)(this.bgStartY + Main.backgroundHeight[2] * num37 + 16 * num39 + num46)) + value, new Rectangle?(new Rectangle(16 * num38 + num45 + num35 + 16, 16 * num39 + num46, width2, height2)), color9);
											}
										}
										else
										{
											if ((int)color8.R > num2 || (double)color8.G > (double)num2 * 1.1 || (double)color8.B > (double)num2 * 1.2)
											{
												for (int num47 = 0; num47 < 4; num47++)
												{
													int num48 = 0;
													int num49 = 0;
													Color color11 = color8;
													Color color12 = color8;
													if (num47 == 0)
													{
														if (Lighting.Brighter(num42, num43 - 1, num42 - 1, num43))
														{
															color12 = Lighting.GetColor(num42 - 1, num43);
														}
														else
														{
															color12 = Lighting.GetColor(num42, num43 - 1);
														}
													}
													if (num47 == 1)
													{
														if (Lighting.Brighter(num42, num43 - 1, num42 + 1, num43))
														{
															color12 = Lighting.GetColor(num42 + 1, num43);
														}
														else
														{
															color12 = Lighting.GetColor(num42, num43 - 1);
														}
														num48 = 8;
													}
													if (num47 == 2)
													{
														if (Lighting.Brighter(num42, num43 + 1, num42 - 1, num43))
														{
															color12 = Lighting.GetColor(num42 - 1, num43);
														}
														else
														{
															color12 = Lighting.GetColor(num42, num43 + 1);
														}
														num49 = 8;
													}
													if (num47 == 3)
													{
														if (Lighting.Brighter(num42, num43 + 1, num42 + 1, num43))
														{
															color12 = Lighting.GetColor(num42 + 1, num43);
														}
														else
														{
															color12 = Lighting.GetColor(num42, num43 + 1);
														}
														num48 = 8;
														num49 = 8;
													}
													color11.R = (byte)((color8.R + color12.R) / 2);
													color11.G = (byte)((color8.G + color12.G) / 2);
													color11.B = (byte)((color8.B + color12.B) / 2);
													color11.R = (byte)((float)color11.R * num5);
													color11.G = (byte)((float)color11.G * num6);
													color11.B = (byte)((float)color11.B * num7);
													this.spriteBatch.Draw(Main.backgroundTexture[3], new Vector2((float)(this.bgStart + num3 * num36 + 16 * num38 + num48 + num35), (float)(this.bgStartY + Main.backgroundHeight[2] * num37 + 16 * num39 + num49)) + value, new Rectangle?(new Rectangle(16 * num38 + num48 + num35 + 16, 16 * num39 + num49, 8, 8)), color11);
												}
											}
											else
											{
												color8.R = (byte)((float)color8.R * num5);
												color8.G = (byte)((float)color8.G * num6);
												color8.B = (byte)((float)color8.B * num7);
												this.spriteBatch.Draw(Main.backgroundTexture[3], new Vector2((float)(this.bgStart + num3 * num36 + 16 * num38 + num35), (float)(this.bgStartY + Main.backgroundHeight[2] * num37 + 16 * num39)) + value, new Rectangle?(new Rectangle(16 * num38 + num35 + 16, 16 * num39, 16, 16)), color8);
											}
										}
									}
									else
									{
										color8.R = (byte)((float)color8.R * num5);
										color8.G = (byte)((float)color8.G * num6);
										color8.B = (byte)((float)color8.B * num7);
										this.spriteBatch.Draw(Main.backgroundTexture[3], new Vector2((float)(this.bgStart + num3 * num36 + 16 * num38 + num35), (float)(this.bgStartY + Main.backgroundHeight[2] * num37 + 16 * num39)) + value, new Rectangle?(new Rectangle(16 * num38 + num35 + 16, 16 * num39, 16, 16)), color8);
									}
								}
							}
						}
					}
				}
				if (flag)
				{
					this.bgParrallax = (double)Main.caveParrallax;
					this.bgStart = (int)(-Math.IEEERemainder((double)Main.screenPosition.X * this.bgParrallax, (double)num3) - (double)(num3 / 2));
					this.bgLoops = Main.screenWidth / num3 + 2;
					this.bgTop = this.bgStartY + this.bgLoopsY * Main.backgroundHeight[2];
					for (int num50 = 0; num50 < this.bgLoops; num50++)
					{
						for (int num51 = 0; num51 < 6; num51++)
						{
							float num52 = (float)(this.bgStart + num3 * num50 + num51 * 16 + 8);
							float num53 = (float)this.bgTop;
							Color color13 = Lighting.GetColor((int)((num52 + Main.screenPosition.X) / 16f), (int)((Main.screenPosition.Y + num53) / 16f));
							color13.R = (byte)((float)color13.R * num5);
							color13.G = (byte)((float)color13.G * num6);
							color13.B = (byte)((float)color13.B * num7);
							this.spriteBatch.Draw(Main.backgroundTexture[6], new Vector2((float)(this.bgStart + num3 * num50 + 16 * num51 + num35), (float)this.bgTop) + value, new Rectangle?(new Rectangle(16 * num51 + num35 + 16, Main.magmaBGFrame * 16, 16, 16)), color13);
						}
					}
				}
			}
			this.bgTop = (int)((float)((int)num16 * 16) - Main.screenPosition.Y + 16f + 600f) - 8;
			if (num16 * 16.0 <= (double)(Main.screenPosition.Y + 600f))
			{
				this.bgStart = (int)(-Math.IEEERemainder(96.0 + (double)Main.screenPosition.X * this.bgParrallax, (double)num3) - (double)(num3 / 2)) - (int)value.X;
				this.bgLoops = (Main.screenWidth + (int)value.X * 2) / num3 + 2;
				if (num16 * 16.0 + (double)Main.screenHeight < (double)(Main.screenPosition.Y - 16f))
				{
					this.bgStartY = (int)(Math.IEEERemainder((double)this.bgTop, (double)Main.backgroundHeight[2]) - (double)Main.backgroundHeight[2]);
					this.bgLoopsY = (Main.screenHeight - this.bgStartY + (int)value.Y * 2) / Main.backgroundHeight[2] + 1;
				}
				else
				{
					this.bgStartY = this.bgTop;
					this.bgLoopsY = (Main.screenHeight - this.bgTop + (int)value.Y * 2) / Main.backgroundHeight[2] + 1;
				}
				num = (int)((double)num * 1.5);
				num2 = (int)((double)num2 * 1.5);
				float num54 = (float)this.bgStart + Main.screenPosition.X;
				num54 = -(float)Math.IEEERemainder((double)num54, 16.0);
				num54 = (float)Math.Round((double)num54);
				int num55 = (int)num54;
				if (num55 == -8)
				{
					num55 = 8;
				}
				for (int num56 = 0; num56 < this.bgLoops; num56++)
				{
					for (int num57 = 0; num57 < this.bgLoopsY; num57++)
					{
						for (int num58 = 0; num58 < 6; num58++)
						{
							for (int num59 = 0; num59 < 6; num59++)
							{
								float num60 = (float)(this.bgStartY + num57 * 96 + num59 * 16 + 8);
								float num61 = (float)(this.bgStart + num3 * num56 + num58 * 16 + 8);
								int num62 = (int)((num61 + Main.screenPosition.X) / 16f);
								int num63 = (int)((num60 + Main.screenPosition.Y) / 16f);
								Color color14 = Lighting.GetColor(num62, num63);
								if (Main.tile[num62, num63] == null)
								{
									Main.tile[num62, num63] = new Tile();
								}
								bool flag4 = false;
								if (Main.caveParrallax != 0f)
								{
									if (Main.tile[num62 - 1, num63] == null)
									{
										Main.tile[num62 - 1, num63] = new Tile();
									}
									if (Main.tile[num62 + 1, num63] == null)
									{
										Main.tile[num62 + 1, num63] = new Tile();
									}
									if (Main.tile[num62, num63].wall == 0 || Main.tile[num62, num63].wall == 21 || Main.tile[num62 - 1, num63].wall == 0 || Main.tile[num62 - 1, num63].wall == 21 || Main.tile[num62 + 1, num63].wall == 0 || Main.tile[num62 + 1, num63].wall == 21)
									{
										flag4 = true;
									}
								}
								else
								{
									if (Main.tile[num62, num63].wall == 0 || Main.tile[num62, num63].wall == 21)
									{
										flag4 = true;
									}
								}
								if ((flag4 || color14.R == 0 || color14.G == 0 || color14.B == 0) && (color14.R > 0 || color14.G > 0 || color14.B > 0) && (Main.tile[num62, num63].wall == 0 || Main.tile[num62, num63].wall == 21 || Main.caveParrallax != 0f))
								{
									if (Lighting.lightMode < 2 && color14.R < 230 && color14.G < 230 && color14.B < 230)
									{
										if (((int)color14.R > num || (double)color14.G > (double)num * 1.1 || (double)color14.B > (double)num * 1.2) && !Main.tile[num62, num63].active)
										{
											for (int num64 = 0; num64 < 9; num64++)
											{
												int num65 = 0;
												int num66 = 0;
												int width3 = 4;
												int height3 = 4;
												Color color15 = color14;
												Color color16 = color14;
												if (num64 == 0 && !Main.tile[num62 - 1, num63 - 1].active)
												{
													color16 = Lighting.GetColor(num62 - 1, num63 - 1);
												}
												if (num64 == 1)
												{
													width3 = 8;
													num65 = 4;
													if (!Main.tile[num62, num63 - 1].active)
													{
														color16 = Lighting.GetColor(num62, num63 - 1);
													}
												}
												if (num64 == 2)
												{
													if (!Main.tile[num62 + 1, num63 - 1].active)
													{
														color16 = Lighting.GetColor(num62 + 1, num63 - 1);
													}
													num65 = 12;
												}
												if (num64 == 3)
												{
													if (!Main.tile[num62 - 1, num63].active)
													{
														color16 = Lighting.GetColor(num62 - 1, num63);
													}
													height3 = 8;
													num66 = 4;
												}
												if (num64 == 4)
												{
													width3 = 8;
													height3 = 8;
													num65 = 4;
													num66 = 4;
												}
												if (num64 == 5)
												{
													num65 = 12;
													num66 = 4;
													height3 = 8;
													if (!Main.tile[num62 + 1, num63].active)
													{
														color16 = Lighting.GetColor(num62 + 1, num63);
													}
												}
												if (num64 == 6)
												{
													if (!Main.tile[num62 - 1, num63 + 1].active)
													{
														color16 = Lighting.GetColor(num62 - 1, num63 + 1);
													}
													num66 = 12;
												}
												if (num64 == 7)
												{
													width3 = 8;
													height3 = 4;
													num65 = 4;
													num66 = 12;
													if (!Main.tile[num62, num63 + 1].active)
													{
														color16 = Lighting.GetColor(num62, num63 + 1);
													}
												}
												if (num64 == 8)
												{
													if (!Main.tile[num62 + 1, num63 + 1].active)
													{
														color16 = Lighting.GetColor(num62 + 1, num63 + 1);
													}
													num65 = 12;
													num66 = 12;
												}
												color15.R = (byte)((color14.R + color16.R) / 2);
												color15.G = (byte)((color14.G + color16.G) / 2);
												color15.B = (byte)((color14.B + color16.B) / 2);
												color15.R = (byte)((float)color15.R * num5);
												color15.G = (byte)((float)color15.G * num6);
												color15.B = (byte)((float)color15.B * num7);
												this.spriteBatch.Draw(Main.backgroundTexture[5], new Vector2((float)(this.bgStart + num3 * num56 + 16 * num58 + num65 + num55), (float)(this.bgStartY + Main.backgroundHeight[2] * num57 + 16 * num59 + num66)) + value, new Rectangle?(new Rectangle(16 * num58 + num65 + num55 + 16, 16 * num59 + Main.backgroundHeight[2] * Main.magmaBGFrame + num66, width3, height3)), color15, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
											}
										}
										else
										{
											if ((int)color14.R > num2 || (double)color14.G > (double)num2 * 1.1 || (double)color14.B > (double)num2 * 1.2)
											{
												for (int num67 = 0; num67 < 4; num67++)
												{
													int num68 = 0;
													int num69 = 0;
													Color color17 = color14;
													Color color18 = color14;
													if (num67 == 0)
													{
														if (Lighting.Brighter(num62, num63 - 1, num62 - 1, num63))
														{
															color18 = Lighting.GetColor(num62 - 1, num63);
														}
														else
														{
															color18 = Lighting.GetColor(num62, num63 - 1);
														}
													}
													if (num67 == 1)
													{
														if (Lighting.Brighter(num62, num63 - 1, num62 + 1, num63))
														{
															color18 = Lighting.GetColor(num62 + 1, num63);
														}
														else
														{
															color18 = Lighting.GetColor(num62, num63 - 1);
														}
														num68 = 8;
													}
													if (num67 == 2)
													{
														if (Lighting.Brighter(num62, num63 + 1, num62 - 1, num63))
														{
															color18 = Lighting.GetColor(num62 - 1, num63);
														}
														else
														{
															color18 = Lighting.GetColor(num62, num63 + 1);
														}
														num69 = 8;
													}
													if (num67 == 3)
													{
														if (Lighting.Brighter(num62, num63 + 1, num62 + 1, num63))
														{
															color18 = Lighting.GetColor(num62 + 1, num63);
														}
														else
														{
															color18 = Lighting.GetColor(num62, num63 + 1);
														}
														num68 = 8;
														num69 = 8;
													}
													color17.R = (byte)((color14.R + color18.R) / 2);
													color17.G = (byte)((color14.G + color18.G) / 2);
													color17.B = (byte)((color14.B + color18.B) / 2);
													color17.R = (byte)((float)color17.R * num5);
													color17.G = (byte)((float)color17.G * num6);
													color17.B = (byte)((float)color17.B * num7);
													this.spriteBatch.Draw(Main.backgroundTexture[5], new Vector2((float)(this.bgStart + num3 * num56 + 16 * num58 + num68 + num55), (float)(this.bgStartY + Main.backgroundHeight[2] * num57 + 16 * num59 + num69)) + value, new Rectangle?(new Rectangle(16 * num58 + num68 + num55 + 16, 16 * num59 + Main.backgroundHeight[2] * Main.magmaBGFrame + num69, 8, 8)), color17, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
												}
											}
											else
											{
												color14.R = (byte)((float)color14.R * num5);
												color14.G = (byte)((float)color14.G * num6);
												color14.B = (byte)((float)color14.B * num7);
												this.spriteBatch.Draw(Main.backgroundTexture[5], new Vector2((float)(this.bgStart + num3 * num56 + 16 * num58 + num55), (float)(this.bgStartY + Main.backgroundHeight[2] * num57 + 16 * num59)) + value, new Rectangle?(new Rectangle(16 * num58 + num55 + 16, 16 * num59 + Main.backgroundHeight[2] * Main.magmaBGFrame, 16, 16)), color14, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
											}
										}
									}
									else
									{
										color14.R = (byte)((float)color14.R * num5);
										color14.G = (byte)((float)color14.G * num6);
										color14.B = (byte)((float)color14.B * num7);
										this.spriteBatch.Draw(Main.backgroundTexture[5], new Vector2((float)(this.bgStart + num3 * num56 + 16 * num58 + num55), (float)(this.bgStartY + Main.backgroundHeight[2] * num57 + 16 * num59)) + value, new Rectangle?(new Rectangle(16 * num58 + num55 + 16, 16 * num59 + Main.backgroundHeight[2] * Main.magmaBGFrame, 16, 16)), color14, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
									}
								}
							}
						}
					}
				}
			}
			Lighting.brightness = Lighting.defBrightness;
			Main.renderTimer[3] = (float)stopwatch.ElapsedMilliseconds;
		}
		protected void RenderBackground()
		{
			if (Main.drawToScreen)
			{
				return;
			}
			base.GraphicsDevice.SetRenderTarget(this.backWaterTarget);
			base.GraphicsDevice.Clear(new Color(0, 0, 0, 0));
			this.spriteBatch.Begin();
			try
			{
				this.DrawWater(true);
			}
			catch
			{
			}
			this.spriteBatch.End();
			base.GraphicsDevice.SetRenderTarget(null);
			base.GraphicsDevice.SetRenderTarget(this.backgroundTarget);
			base.GraphicsDevice.Clear(new Color(0, 0, 0, 0));
			this.spriteBatch.Begin();
			this.DrawBackground();
			this.spriteBatch.End();
			base.GraphicsDevice.SetRenderTarget(null);
		}
		protected void RenderTiles()
		{
			if (Main.drawToScreen)
			{
				return;
			}
			this.RenderBlack();
			base.GraphicsDevice.SetRenderTarget(this.tileTarget);
			base.GraphicsDevice.Clear(new Color(0, 0, 0, 0));
			this.spriteBatch.Begin();
			this.DrawTiles(true);
			this.spriteBatch.End();
			base.GraphicsDevice.SetRenderTarget(null);
		}
		protected void RenderTiles2()
		{
			if (Main.drawToScreen)
			{
				return;
			}
			base.GraphicsDevice.SetRenderTarget(this.tile2Target);
			base.GraphicsDevice.Clear(new Color(0, 0, 0, 0));
			this.spriteBatch.Begin();
			this.DrawTiles(false);
			this.spriteBatch.End();
			base.GraphicsDevice.SetRenderTarget(null);
		}
		protected void RenderWater()
		{
			if (Main.drawToScreen)
			{
				return;
			}
			base.GraphicsDevice.SetRenderTarget(this.waterTarget);
			base.GraphicsDevice.Clear(new Color(0, 0, 0, 0));
			this.spriteBatch.Begin();
			try
			{
				this.DrawWater(false);
				if (Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].mech)
				{
					this.DrawWires();
				}
			}
			catch
			{
			}
			this.spriteBatch.End();
			base.GraphicsDevice.SetRenderTarget(null);
		}
		protected bool FullTile(int x, int y)
		{
			if (Main.tile[x, y].active && Main.tileSolid[(int)Main.tile[x, y].type] && !Main.tileSolidTop[(int)Main.tile[x, y].type] && Main.tile[x, y].type != 10 && Main.tile[x, y].type != 54 && Main.tile[x, y].type != 138)
			{
				int frameX = (int)Main.tile[x, y].frameX;
				int frameY = (int)Main.tile[x, y].frameY;
				if (frameY == 18)
				{
					if (frameX >= 18 && frameX <= 54)
					{
						return true;
					}
					if (frameX >= 108 && frameX <= 144)
					{
						return true;
					}
				}
				else
				{
					if (frameY >= 90 && frameY <= 196)
					{
						if (frameX <= 70)
						{
							return true;
						}
						if (frameX >= 144 && frameX <= 232)
						{
							return true;
						}
					}
				}
			}
			return false;
		}
		protected void DrawBlack()
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			Vector2 value = new Vector2((float)Main.offScreenRange, (float)Main.offScreenRange);
			if (Main.drawToScreen)
			{
				value = default(Vector2);
			}
			int num = (int)((Main.tileColor.R + Main.tileColor.G + Main.tileColor.B) / 3);
			float num2 = (float)((double)num * 0.4) / 255f;
			if (Lighting.lightMode == 2)
			{
				num2 = (float)(Main.tileColor.R - 55) / 255f;
			}
			else
			{
				if (Lighting.lightMode == 3)
				{
					num2 = (float)(num - 55) / 255f;
				}
			}
			int num3 = (int)((Main.screenPosition.X - value.X) / 16f - 1f);
			int num4 = (int)((Main.screenPosition.X + (float)Main.screenWidth + value.X) / 16f) + 2;
			int num5 = (int)((Main.screenPosition.Y - value.Y) / 16f - 1f);
			int num6 = (int)((Main.screenPosition.Y + (float)Main.screenHeight + value.Y) / 16f) + 5;
			int num7 = Main.offScreenRange / 16;
			int num8 = Main.offScreenRange / 16;
			if (num3 - num7 < 0)
			{
				num3 = num7;
			}
			if (num4 + num7 > Main.maxTilesX)
			{
				num4 = Main.maxTilesX - num7;
			}
			if (num5 - num8 < 0)
			{
				num5 = num8;
			}
			if (num6 + num8 > Main.maxTilesY)
			{
				num6 = Main.maxTilesY - num8;
			}
			for (int i = num5 - num8; i < num6 + num8; i++)
			{
				if ((double)i <= Main.worldSurface)
				{
					for (int j = num3 - num7; j < num4 + num7; j++)
					{
						if (Main.tile[j, i] == null)
						{
							Main.tile[j, i] = new Tile();
						}
						if (Lighting.Brightness(j, i) < num2 && (Main.tile[j, i].liquid < 250 || WorldGen.SolidTile(j, i) || (Main.tile[j, i].liquid > 250 && Lighting.Brightness(j, i) == 0f)))
						{
							int num9 = j;
							j++;
							while (Main.tile[j, i] != null && Lighting.Brightness(j, i) < num2 && (Main.tile[j, i].liquid < 250 || WorldGen.SolidTile(j, i) || (Main.tile[j, i].liquid > 250 && Lighting.Brightness(j, i) == 0f)))
							{
								j++;
								if (j >= num4 + num7)
								{
									break;
								}
							}
							j--;
							int width = (j - num9 + 1) * 16;
							this.spriteBatch.Draw(Main.blackTileTexture, new Vector2((float)(num9 * 16 - (int)Main.screenPosition.X), (float)(i * 16 - (int)Main.screenPosition.Y)) + value, new Rectangle?(new Rectangle(0, 0, width, 16)), Color.Black, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
						}
					}
				}
			}
			Main.renderTimer[5] = (float)stopwatch.ElapsedMilliseconds;
		}
		protected void RenderBlack()
		{
			if (Main.drawToScreen)
			{
				return;
			}
			base.GraphicsDevice.SetRenderTarget(this.blackTarget);
			base.GraphicsDevice.DepthStencilState = new DepthStencilState
			{
				DepthBufferEnable = true
			};
			base.GraphicsDevice.Clear(new Color(0, 0, 0, 0));
			this.spriteBatch.Begin();
			this.DrawBlack();
			this.spriteBatch.End();
			base.GraphicsDevice.SetRenderTarget(null);
		}
		protected void DrawWalls()
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			int num = (int)(255f * (1f - Main.gfxQuality) + 100f * Main.gfxQuality);
			int num2 = (int)(120f * (1f - Main.gfxQuality) + 40f * Main.gfxQuality);
			Vector2 value = new Vector2((float)Main.offScreenRange, (float)Main.offScreenRange);
			if (Main.drawToScreen)
			{
				value = default(Vector2);
			}
			int num3 = (int)((Main.tileColor.R + Main.tileColor.G + Main.tileColor.B) / 3);
			float num4 = (float)((double)num3 * 0.53) / 255f;
			if (Lighting.lightMode == 2)
			{
				num4 = (float)(Main.tileColor.R - 12) / 255f;
			}
			if (Lighting.lightMode == 3)
			{
				num4 = (float)(num3 - 12) / 255f;
			}
			int num5 = (int)((Main.screenPosition.X - value.X) / 16f - 1f);
			int num6 = (int)((Main.screenPosition.X + (float)Main.screenWidth + value.X) / 16f) + 2;
			int num7 = (int)((Main.screenPosition.Y - value.Y) / 16f - 1f);
			int num8 = (int)((Main.screenPosition.Y + (float)Main.screenHeight + value.Y) / 16f) + 5;
			int num9 = Main.offScreenRange / 16;
			int num10 = Main.offScreenRange / 16;
			if (num5 - num9 < 0)
			{
				num5 = num9;
			}
			if (num6 + num9 > Main.maxTilesX)
			{
				num6 = Main.maxTilesX - num9;
			}
			if (num7 - num10 < 0)
			{
				num7 = num10;
			}
			if (num8 + num10 > Main.maxTilesY)
			{
				num8 = Main.maxTilesY - num10;
			}
			for (int i = num7 - num10; i < num8 + num10; i++)
			{
				if ((double)i <= Main.worldSurface)
				{
					for (int j = num5 - num9; j < num6 + num9; j++)
					{
						if (Main.tile[j, i] == null)
						{
							Main.tile[j, i] = new Tile();
						}
						if (Lighting.Brightness(j, i) < num4 && (Main.tile[j, i].liquid < 250 || WorldGen.SolidTile(j, i) || (Main.tile[j, i].liquid > 250 && Lighting.Brightness(j, i) == 0f)))
						{
							this.spriteBatch.Draw(Main.blackTileTexture, new Vector2((float)(j * 16 - (int)Main.screenPosition.X), (float)(i * 16 - (int)Main.screenPosition.Y)) + value, Lighting.GetBlackness(j, i));
						}
					}
				}
			}
			for (int k = num7 - num10; k < num8 + num10; k++)
			{
				for (int l = num5 - num9; l < num6 + num9; l++)
				{
					if (Main.tile[l, k] == null)
					{
						Main.tile[l, k] = new Tile();
					}
					if (Main.tile[l, k].wall > 0 && Lighting.Brightness(l, k) > 0f && !this.FullTile(l, k))
					{
						Color color = Lighting.GetColor(l, k);
						if (Lighting.lightMode < 2 && Main.tile[l, k].wall != 21 && !WorldGen.SolidTile(l, k))
						{
							if ((int)color.R > num || (double)color.G > (double)num * 1.1 || (double)color.B > (double)num * 1.2)
							{
								for (int m = 0; m < 9; m++)
								{
									int num11 = 0;
									int num12 = 0;
									int width = 12;
									int height = 12;
									Color color2 = color;
									Color color3 = color;
									if (m == 0)
									{
										color3 = Lighting.GetColor(l - 1, k - 1);
									}
									if (m == 1)
									{
										width = 8;
										num11 = 12;
										color3 = Lighting.GetColor(l, k - 1);
									}
									if (m == 2)
									{
										color3 = Lighting.GetColor(l + 1, k - 1);
										num11 = 20;
									}
									if (m == 3)
									{
										color3 = Lighting.GetColor(l - 1, k);
										height = 8;
										num12 = 12;
									}
									if (m == 4)
									{
										width = 8;
										height = 8;
										num11 = 12;
										num12 = 12;
									}
									if (m == 5)
									{
										num11 = 20;
										num12 = 12;
										height = 8;
										color3 = Lighting.GetColor(l + 1, k);
									}
									if (m == 6)
									{
										color3 = Lighting.GetColor(l - 1, k + 1);
										num12 = 20;
									}
									if (m == 7)
									{
										width = 12;
										num11 = 12;
										num12 = 20;
										color3 = Lighting.GetColor(l, k + 1);
									}
									if (m == 8)
									{
										color3 = Lighting.GetColor(l + 1, k + 1);
										num11 = 20;
										num12 = 20;
									}
									color2.R = (byte)((color.R + color3.R) / 2);
									color2.G = (byte)((color.G + color3.G) / 2);
									color2.B = (byte)((color.B + color3.B) / 2);
									this.spriteBatch.Draw(Main.wallTexture[(int)Main.tile[l, k].wall], new Vector2((float)(l * 16 - (int)Main.screenPosition.X - 8 + num11), (float)(k * 16 - (int)Main.screenPosition.Y - 8 + num12)) + value, new Rectangle?(new Rectangle((int)(Main.tile[l, k].wallFrameX * 2) + num11, (int)(Main.tile[l, k].wallFrameY * 2) + num12, width, height)), color2, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
								}
							}
							else
							{
								if ((int)color.R > num2 || (double)color.G > (double)num2 * 1.1 || (double)color.B > (double)num2 * 1.2)
								{
									for (int n = 0; n < 4; n++)
									{
										int num13 = 0;
										int num14 = 0;
										Color color4 = color;
										Color color5 = color;
										if (n == 0)
										{
											if (Lighting.Brighter(l, k - 1, l - 1, k))
											{
												color5 = Lighting.GetColor(l - 1, k);
											}
											else
											{
												color5 = Lighting.GetColor(l, k - 1);
											}
										}
										if (n == 1)
										{
											if (Lighting.Brighter(l, k - 1, l + 1, k))
											{
												color5 = Lighting.GetColor(l + 1, k);
											}
											else
											{
												color5 = Lighting.GetColor(l, k - 1);
											}
											num13 = 16;
										}
										if (n == 2)
										{
											if (Lighting.Brighter(l, k + 1, l - 1, k))
											{
												color5 = Lighting.GetColor(l - 1, k);
											}
											else
											{
												color5 = Lighting.GetColor(l, k + 1);
											}
											num14 = 16;
										}
										if (n == 3)
										{
											if (Lighting.Brighter(l, k + 1, l + 1, k))
											{
												color5 = Lighting.GetColor(l + 1, k);
											}
											else
											{
												color5 = Lighting.GetColor(l, k + 1);
											}
											num13 = 16;
											num14 = 16;
										}
										color4.R = (byte)((color.R + color5.R) / 2);
										color4.G = (byte)((color.G + color5.G) / 2);
										color4.B = (byte)((color.B + color5.B) / 2);
										this.spriteBatch.Draw(Main.wallTexture[(int)Main.tile[l, k].wall], new Vector2((float)(l * 16 - (int)Main.screenPosition.X - 8 + num13), (float)(k * 16 - (int)Main.screenPosition.Y - 8 + num14)) + value, new Rectangle?(new Rectangle((int)(Main.tile[l, k].wallFrameX * 2) + num13, (int)(Main.tile[l, k].wallFrameY * 2) + num14, 16, 16)), color4, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
									}
								}
								else
								{
									Rectangle value2 = new Rectangle((int)(Main.tile[l, k].wallFrameX * 2), (int)(Main.tile[l, k].wallFrameY * 2), 32, 32);
									this.spriteBatch.Draw(Main.wallTexture[(int)Main.tile[l, k].wall], new Vector2((float)(l * 16 - (int)Main.screenPosition.X - 8), (float)(k * 16 - (int)Main.screenPosition.Y - 8)) + value, new Rectangle?(value2), Lighting.GetColor(l, k), 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
								}
							}
						}
						else
						{
							Rectangle value2 = new Rectangle((int)(Main.tile[l, k].wallFrameX * 2), (int)(Main.tile[l, k].wallFrameY * 2), 32, 32);
							this.spriteBatch.Draw(Main.wallTexture[(int)Main.tile[l, k].wall], new Vector2((float)(l * 16 - (int)Main.screenPosition.X - 8), (float)(k * 16 - (int)Main.screenPosition.Y - 8)) + value, new Rectangle?(value2), Lighting.GetColor(l, k), 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
						}
						if ((double)color.R > (double)num2 * 0.4 || (double)color.G > (double)num2 * 0.35 || (double)color.B > (double)num2 * 0.3)
						{
							bool flag = false;
							if (Main.tile[l - 1, k].wall > 0 && Main.wallBlend[(int)Main.tile[l - 1, k].wall] != Main.wallBlend[(int)Main.tile[l, k].wall])
							{
								flag = true;
							}
							bool flag2 = false;
							if (Main.tile[l + 1, k].wall > 0 && Main.wallBlend[(int)Main.tile[l + 1, k].wall] != Main.wallBlend[(int)Main.tile[l, k].wall])
							{
								flag2 = true;
							}
							bool flag3 = false;
							if (Main.tile[l, k - 1].wall > 0 && Main.wallBlend[(int)Main.tile[l, k - 1].wall] != Main.wallBlend[(int)Main.tile[l, k].wall])
							{
								flag3 = true;
							}
							bool flag4 = false;
							if (Main.tile[l, k + 1].wall > 0 && Main.wallBlend[(int)Main.tile[l, k + 1].wall] != Main.wallBlend[(int)Main.tile[l, k].wall])
							{
								flag4 = true;
							}
							if (flag)
							{
								this.spriteBatch.Draw(Main.wallOutlineTexture, new Vector2((float)(l * 16 - (int)Main.screenPosition.X), (float)(k * 16 - (int)Main.screenPosition.Y)) + value, new Rectangle?(new Rectangle(0, 0, 2, 16)), Lighting.GetColor(l, k), 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
							}
							if (flag2)
							{
								this.spriteBatch.Draw(Main.wallOutlineTexture, new Vector2((float)(l * 16 - (int)Main.screenPosition.X + 14), (float)(k * 16 - (int)Main.screenPosition.Y)) + value, new Rectangle?(new Rectangle(14, 0, 2, 16)), Lighting.GetColor(l, k), 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
							}
							if (flag3)
							{
								this.spriteBatch.Draw(Main.wallOutlineTexture, new Vector2((float)(l * 16 - (int)Main.screenPosition.X), (float)(k * 16 - (int)Main.screenPosition.Y)) + value, new Rectangle?(new Rectangle(0, 0, 16, 2)), Lighting.GetColor(l, k), 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
							}
							if (flag4)
							{
								this.spriteBatch.Draw(Main.wallOutlineTexture, new Vector2((float)(l * 16 - (int)Main.screenPosition.X), (float)(k * 16 - (int)Main.screenPosition.Y + 14)) + value, new Rectangle?(new Rectangle(0, 14, 16, 2)), Lighting.GetColor(l, k), 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
							}
						}
					}
				}
			}
			Main.renderTimer[2] = (float)stopwatch.ElapsedMilliseconds;
		}
		protected void RenderWalls()
		{
			if (Main.drawToScreen)
			{
				return;
			}
			base.GraphicsDevice.SetRenderTarget(this.wallTarget);
			base.GraphicsDevice.DepthStencilState = new DepthStencilState
			{
				DepthBufferEnable = true
			};
			base.GraphicsDevice.Clear(new Color(0, 0, 0, 0));
			this.spriteBatch.Begin();
			this.DrawWalls();
			this.spriteBatch.End();
			base.GraphicsDevice.SetRenderTarget(null);
		}
		protected void ReleaseTargets()
		{
			try
			{
				if (!Main.dedServ)
				{
					Main.offScreenRange = 0;
					Main.targetSet = false;
					this.waterTarget.Dispose();
					this.backWaterTarget.Dispose();
					this.blackTarget.Dispose();
					this.tileTarget.Dispose();
					this.tile2Target.Dispose();
					this.wallTarget.Dispose();
					this.backgroundTarget.Dispose();
				}
			}
			catch
			{
			}
		}
		protected void InitTargets()
		{
			try
			{
				if (!Main.dedServ)
				{
					Main.offScreenRange = 192;
					Main.targetSet = true;
					if (base.GraphicsDevice.PresentationParameters.BackBufferWidth + Main.offScreenRange * 2 > 2048)
					{
						Main.offScreenRange = (2048 - base.GraphicsDevice.PresentationParameters.BackBufferWidth) / 2;
					}
					this.waterTarget = new RenderTarget2D(base.GraphicsDevice, base.GraphicsDevice.PresentationParameters.BackBufferWidth + Main.offScreenRange * 2, base.GraphicsDevice.PresentationParameters.BackBufferHeight + Main.offScreenRange * 2, false, base.GraphicsDevice.PresentationParameters.BackBufferFormat, DepthFormat.Depth24);
					this.backWaterTarget = new RenderTarget2D(base.GraphicsDevice, base.GraphicsDevice.PresentationParameters.BackBufferWidth + Main.offScreenRange * 2, base.GraphicsDevice.PresentationParameters.BackBufferHeight + Main.offScreenRange * 2, false, base.GraphicsDevice.PresentationParameters.BackBufferFormat, DepthFormat.Depth24);
					this.blackTarget = new RenderTarget2D(base.GraphicsDevice, base.GraphicsDevice.PresentationParameters.BackBufferWidth + Main.offScreenRange * 2, base.GraphicsDevice.PresentationParameters.BackBufferHeight + Main.offScreenRange * 2, false, base.GraphicsDevice.PresentationParameters.BackBufferFormat, DepthFormat.Depth24);
					this.tileTarget = new RenderTarget2D(base.GraphicsDevice, base.GraphicsDevice.PresentationParameters.BackBufferWidth + Main.offScreenRange * 2, base.GraphicsDevice.PresentationParameters.BackBufferHeight + Main.offScreenRange * 2, false, base.GraphicsDevice.PresentationParameters.BackBufferFormat, DepthFormat.Depth24);
					this.tile2Target = new RenderTarget2D(base.GraphicsDevice, base.GraphicsDevice.PresentationParameters.BackBufferWidth + Main.offScreenRange * 2, base.GraphicsDevice.PresentationParameters.BackBufferHeight + Main.offScreenRange * 2, false, base.GraphicsDevice.PresentationParameters.BackBufferFormat, DepthFormat.Depth24);
					this.wallTarget = new RenderTarget2D(base.GraphicsDevice, base.GraphicsDevice.PresentationParameters.BackBufferWidth + Main.offScreenRange * 2, base.GraphicsDevice.PresentationParameters.BackBufferHeight + Main.offScreenRange * 2, false, base.GraphicsDevice.PresentationParameters.BackBufferFormat, DepthFormat.Depth24);
					this.backgroundTarget = new RenderTarget2D(base.GraphicsDevice, base.GraphicsDevice.PresentationParameters.BackBufferWidth + Main.offScreenRange * 2, base.GraphicsDevice.PresentationParameters.BackBufferHeight + Main.offScreenRange * 2, false, base.GraphicsDevice.PresentationParameters.BackBufferFormat, DepthFormat.Depth24);
				}
			}
			catch
			{
				Lighting.lightMode = 2;
				try
				{
					this.ReleaseTargets();
				}
				catch
				{
				}
			}
		}
		protected void DrawWires()
		{
			int num = (int)(50f * (1f - Main.gfxQuality) + 2f * Main.gfxQuality);
			Vector2 value = new Vector2((float)Main.offScreenRange, (float)Main.offScreenRange);
			if (Main.drawToScreen)
			{
				value = default(Vector2);
			}
			int num2 = (int)((Main.screenPosition.X - value.X) / 16f - 1f);
			int num3 = (int)((Main.screenPosition.X + (float)Main.screenWidth + value.X) / 16f) + 2;
			int num4 = (int)((Main.screenPosition.Y - value.Y) / 16f - 1f);
			int num5 = (int)((Main.screenPosition.Y + (float)Main.screenHeight + value.Y) / 16f) + 5;
			if (num2 < 0)
			{
				num2 = 0;
			}
			if (num3 > Main.maxTilesX)
			{
				num3 = Main.maxTilesX;
			}
			if (num4 < 0)
			{
				num4 = 0;
			}
			if (num5 > Main.maxTilesY)
			{
				num5 = Main.maxTilesY;
			}
			for (int i = num4; i < num5; i++)
			{
				for (int j = num2; j < num3; j++)
				{
					if (Main.tile[j, i].wire && Lighting.Brightness(j, i) > 0f)
					{
						Rectangle value2 = new Rectangle(0, 0, 16, 16);
						bool wire = Main.tile[j, i - 1].wire;
						bool wire2 = Main.tile[j, i + 1].wire;
						bool wire3 = Main.tile[j - 1, i].wire;
						bool wire4 = Main.tile[j + 1, i].wire;
						if (wire)
						{
							if (wire2)
							{
								if (wire3)
								{
									if (wire4)
									{
										value2 = new Rectangle(18, 18, 16, 16);
									}
									else
									{
										value2 = new Rectangle(54, 0, 16, 16);
									}
								}
								else
								{
									if (wire4)
									{
										value2 = new Rectangle(36, 0, 16, 16);
									}
									else
									{
										value2 = new Rectangle(0, 0, 16, 16);
									}
								}
							}
							else
							{
								if (wire3)
								{
									if (wire4)
									{
										value2 = new Rectangle(0, 18, 16, 16);
									}
									else
									{
										value2 = new Rectangle(54, 18, 16, 16);
									}
								}
								else
								{
									if (wire4)
									{
										value2 = new Rectangle(36, 18, 16, 16);
									}
									else
									{
										value2 = new Rectangle(36, 36, 16, 16);
									}
								}
							}
						}
						else
						{
							if (wire2)
							{
								if (wire3)
								{
									if (wire4)
									{
										value2 = new Rectangle(72, 0, 16, 16);
									}
									else
									{
										value2 = new Rectangle(72, 18, 16, 16);
									}
								}
								else
								{
									if (wire4)
									{
										value2 = new Rectangle(0, 36, 16, 16);
									}
									else
									{
										value2 = new Rectangle(18, 36, 16, 16);
									}
								}
							}
							else
							{
								if (wire3)
								{
									if (wire4)
									{
										value2 = new Rectangle(18, 0, 16, 16);
									}
									else
									{
										value2 = new Rectangle(54, 36, 16, 16);
									}
								}
								else
								{
									if (wire4)
									{
										value2 = new Rectangle(72, 36, 16, 16);
									}
									else
									{
										value2 = new Rectangle(0, 54, 16, 16);
									}
								}
							}
						}
						Color color = Lighting.GetColor(j, i);
						if (Lighting.lightMode < 2 && ((int)color.R > num || (double)color.G > (double)num * 1.1 || (double)color.B > (double)num * 1.2))
						{
							for (int k = 0; k < 4; k++)
							{
								int num6 = 0;
								int num7 = 0;
								Color color2 = color;
								Color color3 = color;
								if (k == 0)
								{
									if (Lighting.Brighter(j, i - 1, j - 1, i))
									{
										color3 = Lighting.GetColor(j - 1, i);
									}
									else
									{
										color3 = Lighting.GetColor(j, i - 1);
									}
								}
								if (k == 1)
								{
									if (Lighting.Brighter(j, i - 1, j + 1, i))
									{
										color3 = Lighting.GetColor(j + 1, i);
									}
									else
									{
										color3 = Lighting.GetColor(j, i - 1);
									}
									num6 = 8;
								}
								if (k == 2)
								{
									if (Lighting.Brighter(j, i + 1, j - 1, i))
									{
										color3 = Lighting.GetColor(j - 1, i);
									}
									else
									{
										color3 = Lighting.GetColor(j, i + 1);
									}
									num7 = 8;
								}
								if (k == 3)
								{
									if (Lighting.Brighter(j, i + 1, j + 1, i))
									{
										color3 = Lighting.GetColor(j + 1, i);
									}
									else
									{
										color3 = Lighting.GetColor(j, i + 1);
									}
									num6 = 8;
									num7 = 8;
								}
								color2.R = (byte)((color.R + color3.R) / 2);
								color2.G = (byte)((color.G + color3.G) / 2);
								color2.B = (byte)((color.B + color3.B) / 2);
								this.spriteBatch.Draw(Main.wireTexture, new Vector2((float)(j * 16 - (int)Main.screenPosition.X + num6), (float)(i * 16 - (int)Main.screenPosition.Y + num7)) + value, new Rectangle?(new Rectangle(value2.X + num6, value2.Y + num7, 8, 8)), color2, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
							}
						}
						else
						{
							this.spriteBatch.Draw(Main.wireTexture, new Vector2((float)(j * 16 - (int)Main.screenPosition.X), (float)(i * 16 - (int)Main.screenPosition.Y)) + value, new Rectangle?(value2), color, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
						}
					}
				}
			}
		}
		protected override void Draw(GameTime gameTime)
		{
			if (Lighting.lightMode >= 2)
			{
				Main.drawToScreen = true;
			}
			else
			{
				Main.drawToScreen = false;
			}
			if (Main.drawToScreen && Main.targetSet)
			{
				this.ReleaseTargets();
			}
			if (!Main.drawToScreen && !Main.targetSet)
			{
				this.InitTargets();
			}
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			Main.fpsCount++;
			if (!base.IsActive)
			{
				Main.maxQ = true;
			}
			if (!Main.dedServ)
			{
				bool flag = false;
				if (Main.screenWidth != base.GraphicsDevice.Viewport.Width || Main.screenHeight != base.GraphicsDevice.Viewport.Height)
				{
					flag = true;
					if (Main.gamePaused)
					{
						Main.renderNow = true;
					}
				}
				Main.screenWidth = base.GraphicsDevice.Viewport.Width;
				Main.screenHeight = base.GraphicsDevice.Viewport.Height;
				if (Main.screenWidth > Main.maxScreenW)
				{
					Main.screenWidth = Main.maxScreenW;
					flag = true;
				}
				if (Main.screenHeight > Main.maxScreenH)
				{
					Main.screenHeight = Main.maxScreenH;
					flag = true;
				}
				if (Main.screenWidth < Main.minScreenW)
				{
					Main.screenWidth = Main.minScreenW;
					flag = true;
				}
				if (Main.screenHeight < Main.minScreenH)
				{
					Main.screenHeight = Main.minScreenH;
					flag = true;
				}
				if (flag)
				{
					this.graphics.PreferredBackBufferWidth = Main.screenWidth;
					this.graphics.PreferredBackBufferHeight = Main.screenHeight;
					this.graphics.ApplyChanges();
					if (!Main.drawToScreen)
					{
						this.InitTargets();
					}
				}
			}
			Main.CursorColor();
			Main.drawTime++;
			Main.screenLastPosition = Main.screenPosition;
			if (Main.stackSplit == 0)
			{
				Main.stackCounter = 0;
				Main.stackDelay = 7;
			}
			else
			{
				Main.stackCounter++;
				if (Main.stackCounter >= 30)
				{
					Main.stackDelay--;
					if (Main.stackDelay < 2)
					{
						Main.stackDelay = 2;
					}
					Main.stackCounter = 0;
				}
			}
			Main.mouseTextColor += (byte)Main.mouseTextColorChange;
			if (Main.mouseTextColor >= 250)
			{
				Main.mouseTextColorChange = -4;
			}
			if (Main.mouseTextColor <= 175)
			{
				Main.mouseTextColorChange = 4;
			}
			if (Main.myPlayer >= 0)
			{
				Main.player[Main.myPlayer].mouseInterface = false;
			}
			Main.toolTip = new Item();
			if (!Main.gameMenu && Main.netMode != 2)
			{
				Main.screenPosition.X = Main.player[Main.myPlayer].position.X + (float)Main.player[Main.myPlayer].width * 0.5f - (float)Main.screenWidth * 0.5f;
				Main.screenPosition.Y = Main.player[Main.myPlayer].position.Y + (float)Main.player[Main.myPlayer].height * 0.5f - (float)Main.screenHeight * 0.5f;
				Main.screenPosition.X = (float)((int)Main.screenPosition.X);
				Main.screenPosition.Y = (float)((int)Main.screenPosition.Y);
			}
			if (!Main.gameMenu && Main.netMode != 2)
			{
				if (Main.screenPosition.X < Main.leftWorld + (float)(Lighting.offScreenTiles * 16) + 16f)
				{
					Main.screenPosition.X = Main.leftWorld + (float)(Lighting.offScreenTiles * 16) + 16f;
				}
				else
				{
					if (Main.screenPosition.X + (float)Main.screenWidth > Main.rightWorld - (float)(Lighting.offScreenTiles * 16) - 32f)
					{
						Main.screenPosition.X = Main.rightWorld - (float)Main.screenWidth - (float)(Lighting.offScreenTiles * 16) - 32f;
					}
				}
				if (Main.screenPosition.Y < Main.topWorld + (float)(Lighting.offScreenTiles * 16) + 16f)
				{
					Main.screenPosition.Y = Main.topWorld + (float)(Lighting.offScreenTiles * 16) + 16f;
				}
				else
				{
					if (Main.screenPosition.Y + (float)Main.screenHeight > Main.bottomWorld - (float)(Lighting.offScreenTiles * 16) - 32f)
					{
						Main.screenPosition.Y = Main.bottomWorld - (float)Main.screenHeight - (float)(Lighting.offScreenTiles * 16) - 32f;
					}
				}
			}
			if (Main.showSplash)
			{
				this.DrawSplash(gameTime);
				return;
			}
			if (!Main.gameMenu)
			{
				if (Main.renderNow)
				{
					Main.screenLastPosition = Main.screenPosition;
					Main.renderNow = false;
					Main.renderCount = 99;
					int tempLightCount = Lighting.tempLightCount;
					this.Draw(gameTime);
					Lighting.tempLightCount = tempLightCount;
					Lighting.LightTiles(this.firstTileX, this.lastTileX, this.firstTileY, this.lastTileY);
					Lighting.LightTiles(this.firstTileX, this.lastTileX, this.firstTileY, this.lastTileY);
					this.RenderTiles();
					Main.sceneTilePos.X = Main.screenPosition.X - (float)Main.offScreenRange;
					Main.sceneTilePos.Y = Main.screenPosition.Y - (float)Main.offScreenRange;
					this.RenderBackground();
					Main.sceneBackgroundPos.X = Main.screenPosition.X - (float)Main.offScreenRange;
					Main.sceneBackgroundPos.Y = Main.screenPosition.Y - (float)Main.offScreenRange;
					this.RenderWalls();
					Main.sceneWallPos.X = Main.screenPosition.X - (float)Main.offScreenRange;
					Main.sceneWallPos.Y = Main.screenPosition.Y - (float)Main.offScreenRange;
					this.RenderTiles2();
					Main.sceneTile2Pos.X = Main.screenPosition.X - (float)Main.offScreenRange;
					Main.sceneTile2Pos.Y = Main.screenPosition.Y - (float)Main.offScreenRange;
					this.RenderWater();
					Main.sceneWaterPos.X = Main.screenPosition.X - (float)Main.offScreenRange;
					Main.sceneWaterPos.Y = Main.screenPosition.Y - (float)Main.offScreenRange;
					Main.renderCount = 99;
				}
				else
				{
					if (Main.renderCount == 3)
					{
						this.RenderTiles();
						Main.sceneTilePos.X = Main.screenPosition.X - (float)Main.offScreenRange;
						Main.sceneTilePos.Y = Main.screenPosition.Y - (float)Main.offScreenRange;
					}
					if (Main.renderCount == 2)
					{
						this.RenderBackground();
						Main.sceneBackgroundPos.X = Main.screenPosition.X - (float)Main.offScreenRange;
						Main.sceneBackgroundPos.Y = Main.screenPosition.Y - (float)Main.offScreenRange;
					}
					if (Main.renderCount == 2)
					{
						this.RenderWalls();
						Main.sceneWallPos.X = Main.screenPosition.X - (float)Main.offScreenRange;
						Main.sceneWallPos.Y = Main.screenPosition.Y - (float)Main.offScreenRange;
					}
					if (Main.renderCount == 3)
					{
						this.RenderTiles2();
						Main.sceneTile2Pos.X = Main.screenPosition.X - (float)Main.offScreenRange;
						Main.sceneTile2Pos.Y = Main.screenPosition.Y - (float)Main.offScreenRange;
					}
					if (Main.renderCount == 1)
					{
						this.RenderWater();
						Main.sceneWaterPos.X = Main.screenPosition.X - (float)Main.offScreenRange;
						Main.sceneWaterPos.Y = Main.screenPosition.Y - (float)Main.offScreenRange;
					}
				}
				if (Main.render && !Main.gameMenu)
				{
					if (Math.Abs(Main.sceneTilePos.X - (Main.screenPosition.X - (float)Main.offScreenRange)) > (float)Main.offScreenRange || Math.Abs(Main.sceneTilePos.Y - (Main.screenPosition.Y - (float)Main.offScreenRange)) > (float)Main.offScreenRange)
					{
						this.RenderTiles();
						Main.sceneTilePos.X = Main.screenPosition.X - (float)Main.offScreenRange;
						Main.sceneTilePos.Y = Main.screenPosition.Y - (float)Main.offScreenRange;
					}
					if (Math.Abs(Main.sceneTile2Pos.X - (Main.screenPosition.X - (float)Main.offScreenRange)) > (float)Main.offScreenRange || Math.Abs(Main.sceneTile2Pos.Y - (Main.screenPosition.Y - (float)Main.offScreenRange)) > (float)Main.offScreenRange)
					{
						this.RenderTiles2();
						Main.sceneTile2Pos.X = Main.screenPosition.X - (float)Main.offScreenRange;
						Main.sceneTile2Pos.Y = Main.screenPosition.Y - (float)Main.offScreenRange;
					}
					if (Math.Abs(Main.sceneBackgroundPos.X - (Main.screenPosition.X - (float)Main.offScreenRange)) > (float)Main.offScreenRange || Math.Abs(Main.sceneBackgroundPos.Y - (Main.screenPosition.Y - (float)Main.offScreenRange)) > (float)Main.offScreenRange)
					{
						this.RenderBackground();
						Main.sceneBackgroundPos.X = Main.screenPosition.X - (float)Main.offScreenRange;
						Main.sceneBackgroundPos.Y = Main.screenPosition.Y - (float)Main.offScreenRange;
					}
					if (Math.Abs(Main.sceneWallPos.X - (Main.screenPosition.X - (float)Main.offScreenRange)) > (float)Main.offScreenRange || Math.Abs(Main.sceneWallPos.Y - (Main.screenPosition.Y - (float)Main.offScreenRange)) > (float)Main.offScreenRange)
					{
						this.RenderWalls();
						Main.sceneWallPos.X = Main.screenPosition.X - (float)Main.offScreenRange;
						Main.sceneWallPos.Y = Main.screenPosition.Y - (float)Main.offScreenRange;
					}
					if (Math.Abs(Main.sceneWaterPos.X - (Main.screenPosition.X - (float)Main.offScreenRange)) > (float)Main.offScreenRange || Math.Abs(Main.sceneWaterPos.Y - (Main.screenPosition.Y - (float)Main.offScreenRange)) > (float)Main.offScreenRange)
					{
						this.RenderWater();
						Main.sceneWaterPos.X = Main.screenPosition.X - (float)Main.offScreenRange;
						Main.sceneWaterPos.Y = Main.screenPosition.Y - (float)Main.offScreenRange;
					}
				}
			}
			this.bgParrallax = 0.1;
			this.bgStart = (int)(-Math.IEEERemainder((double)Main.screenPosition.X * this.bgParrallax, (double)Main.backgroundWidth[Main.background]) - (double)(Main.backgroundWidth[Main.background] / 2));
			this.bgLoops = Main.screenWidth / Main.backgroundWidth[Main.background] + 2;
			this.bgStartY = 0;
			this.bgLoopsY = 0;
			this.bgTop = (int)((double)(-(double)Main.screenPosition.Y) / (Main.worldSurface * 16.0 - 600.0) * 200.0);
			Main.bgColor = Color.White;
			if (Main.gameMenu || Main.netMode == 2)
			{
				this.bgTop = -200;
			}
			int num = (int)(Main.time / 54000.0 * (double)(Main.screenWidth + Main.sunTexture.Width * 2)) - Main.sunTexture.Width;
			int num2 = 0;
			Color white = Color.White;
			float num3 = 1f;
			float rotation = (float)(Main.time / 54000.0) * 2f - 7.3f;
			int num4 = (int)(Main.time / 32400.0 * (double)(Main.screenWidth + Main.moonTexture.Width * 2)) - Main.moonTexture.Width;
			int num5 = 0;
			Color white2 = Color.White;
			float num6 = 1f;
			float rotation2 = (float)(Main.time / 32400.0) * 2f - 7.3f;
			if (Main.dayTime)
			{
				double num7;
				if (Main.time < 27000.0)
				{
					num7 = Math.Pow(1.0 - Main.time / 54000.0 * 2.0, 2.0);
					num2 = (int)((double)this.bgTop + num7 * 250.0 + 180.0);
				}
				else
				{
					num7 = Math.Pow((Main.time / 54000.0 - 0.5) * 2.0, 2.0);
					num2 = (int)((double)this.bgTop + num7 * 250.0 + 180.0);
				}
				num3 = (float)(1.2 - num7 * 0.4);
			}
			else
			{
				double num8;
				if (Main.time < 16200.0)
				{
					num8 = Math.Pow(1.0 - Main.time / 32400.0 * 2.0, 2.0);
					num5 = (int)((double)this.bgTop + num8 * 250.0 + 180.0);
				}
				else
				{
					num8 = Math.Pow((Main.time / 32400.0 - 0.5) * 2.0, 2.0);
					num5 = (int)((double)this.bgTop + num8 * 250.0 + 180.0);
				}
				num6 = (float)(1.2 - num8 * 0.4);
			}
			if (Main.dayTime)
			{
				if (Main.time < 13500.0)
				{
					float num9 = (float)(Main.time / 13500.0);
					white.R = (byte)(num9 * 200f + 55f);
					white.G = (byte)(num9 * 180f + 75f);
					white.B = (byte)(num9 * 250f + 5f);
					Main.bgColor.R = (byte)(num9 * 230f + 25f);
					Main.bgColor.G = (byte)(num9 * 220f + 35f);
					Main.bgColor.B = (byte)(num9 * 220f + 35f);
				}
				if (Main.time > 45900.0)
				{
					float num9 = (float)(1.0 - (Main.time / 54000.0 - 0.85) * 6.666666666666667);
					white.R = (byte)(num9 * 120f + 55f);
					white.G = (byte)(num9 * 100f + 25f);
					white.B = (byte)(num9 * 120f + 55f);
					Main.bgColor.R = (byte)(num9 * 200f + 35f);
					Main.bgColor.G = (byte)(num9 * 85f + 35f);
					Main.bgColor.B = (byte)(num9 * 135f + 35f);
				}
				else
				{
					if (Main.time > 37800.0)
					{
						float num9 = (float)(1.0 - (Main.time / 54000.0 - 0.7) * 6.666666666666667);
						white.R = (byte)(num9 * 80f + 175f);
						white.G = (byte)(num9 * 130f + 125f);
						white.B = (byte)(num9 * 100f + 155f);
						Main.bgColor.R = (byte)(num9 * 20f + 235f);
						Main.bgColor.G = (byte)(num9 * 135f + 120f);
						Main.bgColor.B = (byte)(num9 * 85f + 170f);
					}
				}
			}
			if (!Main.dayTime)
			{
				if (Main.bloodMoon)
				{
					if (Main.time < 16200.0)
					{
						float num9 = (float)(1.0 - Main.time / 16200.0);
						white2.R = (byte)(num9 * 10f + 205f);
						white2.G = (byte)(num9 * 170f + 55f);
						white2.B = (byte)(num9 * 200f + 55f);
						Main.bgColor.R = (byte)(40f - num9 * 40f + 35f);
						Main.bgColor.G = (byte)(num9 * 20f + 15f);
						Main.bgColor.B = (byte)(num9 * 20f + 15f);
					}
					else
					{
						if (Main.time >= 16200.0)
						{
							float num9 = (float)((Main.time / 32400.0 - 0.5) * 2.0);
							white2.R = (byte)(num9 * 50f + 205f);
							white2.G = (byte)(num9 * 100f + 155f);
							white2.B = (byte)(num9 * 100f + 155f);
							white2.R = (byte)(num9 * 10f + 205f);
							white2.G = (byte)(num9 * 170f + 55f);
							white2.B = (byte)(num9 * 200f + 55f);
							Main.bgColor.R = (byte)(40f - num9 * 40f + 35f);
							Main.bgColor.G = (byte)(num9 * 20f + 15f);
							Main.bgColor.B = (byte)(num9 * 20f + 15f);
						}
					}
				}
				else
				{
					if (Main.time < 16200.0)
					{
						float num9 = (float)(1.0 - Main.time / 16200.0);
						white2.R = (byte)(num9 * 10f + 205f);
						white2.G = (byte)(num9 * 70f + 155f);
						white2.B = (byte)(num9 * 100f + 155f);
						Main.bgColor.R = (byte)(num9 * 20f + 15f);
						Main.bgColor.G = (byte)(num9 * 20f + 15f);
						Main.bgColor.B = (byte)(num9 * 20f + 15f);
					}
					else
					{
						if (Main.time >= 16200.0)
						{
							float num9 = (float)((Main.time / 32400.0 - 0.5) * 2.0);
							white2.R = (byte)(num9 * 50f + 205f);
							white2.G = (byte)(num9 * 100f + 155f);
							white2.B = (byte)(num9 * 100f + 155f);
							Main.bgColor.R = (byte)(num9 * 10f + 15f);
							Main.bgColor.G = (byte)(num9 * 20f + 15f);
							Main.bgColor.B = (byte)(num9 * 20f + 15f);
						}
					}
				}
			}
			if (Main.gameMenu || Main.netMode == 2)
			{
				this.bgTop = 0;
				if (!Main.dayTime)
				{
					Main.bgColor.R = 35;
					Main.bgColor.G = 35;
					Main.bgColor.B = 35;
				}
			}
			if (Main.gameMenu)
			{
				Main.bgDelay = 1000;
				Main.evilTiles = (int)(Main.bgAlpha[1] * 500f);
			}
			if (Main.evilTiles > 0)
			{
				float num10 = (float)Main.evilTiles / 500f;
				if (num10 > 1f)
				{
					num10 = 1f;
				}
				int num11 = (int)Main.bgColor.R;
				int num12 = (int)Main.bgColor.G;
				int num13 = (int)Main.bgColor.B;
				num11 -= (int)(100f * num10 * ((float)Main.bgColor.R / 255f));
				num12 -= (int)(140f * num10 * ((float)Main.bgColor.G / 255f));
				num13 -= (int)(80f * num10 * ((float)Main.bgColor.B / 255f));
				if (num11 < 15)
				{
					num11 = 15;
				}
				if (num12 < 15)
				{
					num12 = 15;
				}
				if (num13 < 15)
				{
					num13 = 15;
				}
				Main.bgColor.R = (byte)num11;
				Main.bgColor.G = (byte)num12;
				Main.bgColor.B = (byte)num13;
				num11 = (int)white.R;
				num12 = (int)white.G;
				num13 = (int)white.B;
				num11 -= (int)(100f * num10 * ((float)white.R / 255f));
				num12 -= (int)(100f * num10 * ((float)white.G / 255f));
				num13 -= (int)(0f * num10 * ((float)white.B / 255f));
				if (num11 < 15)
				{
					num11 = 15;
				}
				if (num12 < 15)
				{
					num12 = 15;
				}
				if (num13 < 15)
				{
					num13 = 15;
				}
				white.R = (byte)num11;
				white.G = (byte)num12;
				white.B = (byte)num13;
				num11 = (int)white2.R;
				num12 = (int)white2.G;
				num13 = (int)white2.B;
				num11 -= (int)(140f * num10 * ((float)white2.R / 255f));
				num12 -= (int)(190f * num10 * ((float)white2.G / 255f));
				num13 -= (int)(170f * num10 * ((float)white2.B / 255f));
				if (num11 < 15)
				{
					num11 = 15;
				}
				if (num12 < 15)
				{
					num12 = 15;
				}
				if (num13 < 15)
				{
					num13 = 15;
				}
				white2.R = (byte)num11;
				white2.G = (byte)num12;
				white2.B = (byte)num13;
			}
			if (Main.jungleTiles > 0)
			{
				float num14 = (float)Main.jungleTiles / 200f;
				if (num14 > 1f)
				{
					num14 = 1f;
				}
				int num15 = (int)Main.bgColor.R;
				int num16 = (int)Main.bgColor.G;
				int num17 = (int)Main.bgColor.B;
				num15 -= (int)(20f * num14 * ((float)Main.bgColor.R / 255f));
				num17 -= (int)(90f * num14 * ((float)Main.bgColor.B / 255f));
				if (num16 > 255)
				{
					num16 = 255;
				}
				if (num16 < 15)
				{
					num16 = 15;
				}
				if (num15 > 255)
				{
					num15 = 255;
				}
				if (num15 < 15)
				{
					num15 = 15;
				}
				if (num17 < 15)
				{
					num17 = 15;
				}
				Main.bgColor.R = (byte)num15;
				Main.bgColor.G = (byte)num16;
				Main.bgColor.B = (byte)num17;
				num15 = (int)white.R;
				num16 = (int)white.G;
				num17 = (int)white.B;
				num15 -= (int)(30f * num14 * ((float)white.R / 255f));
				num17 -= (int)(10f * num14 * ((float)white.B / 255f));
				if (num15 < 15)
				{
					num15 = 15;
				}
				if (num16 < 15)
				{
					num16 = 15;
				}
				if (num17 < 15)
				{
					num17 = 15;
				}
				white.R = (byte)num15;
				white.G = (byte)num16;
				white.B = (byte)num17;
				num15 = (int)white2.R;
				num16 = (int)white2.G;
				num17 = (int)white2.B;
				num16 -= (int)(140f * num14 * ((float)white2.R / 255f));
				num15 -= (int)(170f * num14 * ((float)white2.G / 255f));
				num17 -= (int)(190f * num14 * ((float)white2.B / 255f));
				if (num15 < 15)
				{
					num15 = 15;
				}
				if (num16 < 15)
				{
					num16 = 15;
				}
				if (num17 < 15)
				{
					num17 = 15;
				}
				white2.R = (byte)num15;
				white2.G = (byte)num16;
				white2.B = (byte)num17;
			}
			if (Main.bgColor.R < 15)
			{
				Main.bgColor.R = 15;
			}
			if (Main.bgColor.G < 15)
			{
				Main.bgColor.G = 15;
			}
			if (Main.bgColor.B < 15)
			{
				Main.bgColor.B = 15;
			}
			if (Main.bloodMoon)
			{
				if (Main.bgColor.R < 25)
				{
					Main.bgColor.R = 25;
				}
				if (Main.bgColor.G < 25)
				{
					Main.bgColor.G = 25;
				}
				if (Main.bgColor.B < 25)
				{
					Main.bgColor.B = 25;
				}
			}
			Main.tileColor.A = 255;
			Main.tileColor.R = (byte)((Main.bgColor.R + Main.bgColor.B + Main.bgColor.G) / 3);
			Main.tileColor.G = (byte)((Main.bgColor.R + Main.bgColor.B + Main.bgColor.G) / 3);
			Main.tileColor.B = (byte)((Main.bgColor.R + Main.bgColor.B + Main.bgColor.G) / 3);
			Main.tileColor.R = (byte)((Main.bgColor.R + Main.bgColor.G + Main.bgColor.B + Main.bgColor.R * 7) / 10);
			Main.tileColor.G = (byte)((Main.bgColor.R + Main.bgColor.G + Main.bgColor.B + Main.bgColor.G * 7) / 10);
			Main.tileColor.B = (byte)((Main.bgColor.R + Main.bgColor.G + Main.bgColor.B + Main.bgColor.B * 7) / 10);
			if (Main.tileColor.R >= 255 && Main.tileColor.G >= 255)
			{
				byte arg_1A48_0 = Main.tileColor.B;
			}
			float num18 = (float)(Main.maxTilesX / 4200);
			num18 *= num18;
			float num19 = (float)((double)((Main.screenPosition.Y + (float)(Main.screenHeight / 2)) / 16f - (65f + 10f * num18)) / (Main.worldSurface / 5.0));
			if (num19 < 0f)
			{
				num19 = 0f;
			}
			if (num19 > 1f)
			{
				num19 = 1f;
			}
			if (Main.gameMenu)
			{
				num19 = 1f;
			}
			Main.bgColor.R = (byte)((float)Main.bgColor.R * num19);
			Main.bgColor.G = (byte)((float)Main.bgColor.G * num19);
			Main.bgColor.B = (byte)((float)Main.bgColor.B * num19);
			base.GraphicsDevice.Clear(Color.Black);
			base.Draw(gameTime);
			this.spriteBatch.Begin();
			if ((double)Main.screenPosition.Y < Main.worldSurface * 16.0 + 16.0)
			{
				for (int i = 0; i < this.bgLoops; i++)
				{
					this.spriteBatch.Draw(Main.backgroundTexture[Main.background], new Rectangle(this.bgStart + Main.backgroundWidth[Main.background] * i, this.bgTop, Main.backgroundWidth[Main.background], Main.backgroundHeight[Main.background]), Main.bgColor);
				}
			}
			if ((double)Main.screenPosition.Y < Main.worldSurface * 16.0 + 16.0 && 255 - Main.bgColor.R - 100 > 0 && Main.netMode != 2)
			{
				for (int j = 0; j < Main.numStars; j++)
				{
					Color color = default(Color);
					float num20 = (float)Main.evilTiles / 500f;
					if (num20 > 1f)
					{
						num20 = 1f;
					}
					num20 = 1f - num20 * 0.5f;
					if (Main.evilTiles <= 0)
					{
						num20 = 1f;
					}
					int num21 = (int)((float)(255 - Main.bgColor.R - 100) * Main.star[j].twinkle * num20);
					int num22 = (int)((float)(255 - Main.bgColor.G - 100) * Main.star[j].twinkle * num20);
					int num23 = (int)((float)(255 - Main.bgColor.B - 100) * Main.star[j].twinkle * num20);
					if (num21 < 0)
					{
						num21 = 0;
					}
					if (num22 < 0)
					{
						num22 = 0;
					}
					if (num23 < 0)
					{
						num23 = 0;
					}
					color.R = (byte)num21;
					color.G = (byte)((float)num22 * num20);
					color.B = (byte)((float)num23 * num20);
					float num24 = Main.star[j].position.X * ((float)Main.screenWidth / 800f);
					float num25 = Main.star[j].position.Y * ((float)Main.screenHeight / 600f);
					this.spriteBatch.Draw(Main.starTexture[Main.star[j].type], new Vector2(num24 + (float)Main.starTexture[Main.star[j].type].Width * 0.5f, num25 + (float)Main.starTexture[Main.star[j].type].Height * 0.5f + (float)this.bgTop), new Rectangle?(new Rectangle(0, 0, Main.starTexture[Main.star[j].type].Width, Main.starTexture[Main.star[j].type].Height)), color, Main.star[j].rotation, new Vector2((float)Main.starTexture[Main.star[j].type].Width * 0.5f, (float)Main.starTexture[Main.star[j].type].Height * 0.5f), Main.star[j].scale * Main.star[j].twinkle, SpriteEffects.None, 0f);
				}
			}
			if ((double)(Main.screenPosition.Y / 16f) < Main.worldSurface + 2.0)
			{
				if (Main.dayTime)
				{
					num3 *= 1.1f;
					if (!Main.gameMenu && Main.player[Main.myPlayer].head == 12)
					{
						this.spriteBatch.Draw(Main.sun2Texture, new Vector2((float)num, (float)(num2 + (int)Main.sunModY)), new Rectangle?(new Rectangle(0, 0, Main.sunTexture.Width, Main.sunTexture.Height)), white, rotation, new Vector2((float)(Main.sunTexture.Width / 2), (float)(Main.sunTexture.Height / 2)), num3, SpriteEffects.None, 0f);
					}
					else
					{
						this.spriteBatch.Draw(Main.sunTexture, new Vector2((float)num, (float)(num2 + (int)Main.sunModY)), new Rectangle?(new Rectangle(0, 0, Main.sunTexture.Width, Main.sunTexture.Height)), white, rotation, new Vector2((float)(Main.sunTexture.Width / 2), (float)(Main.sunTexture.Height / 2)), num3, SpriteEffects.None, 0f);
					}
				}
				if (!Main.dayTime)
				{
					this.spriteBatch.Draw(Main.moonTexture, new Vector2((float)num4, (float)(num5 + (int)Main.moonModY)), new Rectangle?(new Rectangle(0, Main.moonTexture.Width * Main.moonPhase, Main.moonTexture.Width, Main.moonTexture.Width)), white2, rotation2, new Vector2((float)(Main.moonTexture.Width / 2), (float)(Main.moonTexture.Width / 2)), num6, SpriteEffects.None, 0f);
				}
			}
			Rectangle value;
			if (Main.dayTime)
			{
				value = new Rectangle((int)((double)num - (double)Main.sunTexture.Width * 0.5 * (double)num3), (int)((double)num2 - (double)Main.sunTexture.Height * 0.5 * (double)num3 + (double)Main.sunModY), (int)((float)Main.sunTexture.Width * num3), (int)((float)Main.sunTexture.Width * num3));
			}
			else
			{
				value = new Rectangle((int)((double)num4 - (double)Main.moonTexture.Width * 0.5 * (double)num6), (int)((double)num5 - (double)Main.moonTexture.Width * 0.5 * (double)num6 + (double)Main.moonModY), (int)((float)Main.moonTexture.Width * num6), (int)((float)Main.moonTexture.Width * num6));
			}
			Rectangle rectangle = new Rectangle(Main.mouseX, Main.mouseY, 1, 1);
			Main.sunModY = (short)((double)Main.sunModY * 0.999);
			Main.moonModY = (short)((double)Main.moonModY * 0.999);
			if (Main.gameMenu && Main.netMode != 1)
			{
				if (Main.mouseLeft && Main.hasFocus)
				{
					if (rectangle.Intersects(value) || Main.grabSky)
					{
						if (Main.dayTime)
						{
							Main.time = 54000.0 * (double)((float)(Main.mouseX + Main.sunTexture.Width) / ((float)Main.screenWidth + (float)(Main.sunTexture.Width * 2)));
							Main.sunModY = (short)(Main.mouseY - num2);
							if (Main.time > 53990.0)
							{
								Main.time = 53990.0;
							}
						}
						else
						{
							Main.time = 32400.0 * (double)((float)(Main.mouseX + Main.moonTexture.Width) / ((float)Main.screenWidth + (float)(Main.moonTexture.Width * 2)));
							Main.moonModY = (short)(Main.mouseY - num5);
							if (Main.time > 32390.0)
							{
								Main.time = 32390.0;
							}
						}
						if (Main.time < 10.0)
						{
							Main.time = 10.0;
						}
						if (Main.netMode != 0)
						{
							NetMessage.SendData(18, -1, -1, "", 0, 0f, 0f, 0f, 0);
						}
						Main.grabSky = true;
					}
				}
				else
				{
					Main.grabSky = false;
				}
			}
			float num26 = (float)(Main.screenHeight - 600);
			this.bgTop = (int)((double)(-(double)Main.screenPosition.Y + num26 / 2f) / (Main.worldSurface * 16.0) * 1200.0 + 1190.0);
			float num27 = (float)(this.bgTop - 50);
			if (Main.resetClouds)
			{
				Cloud.resetClouds();
				Main.resetClouds = false;
			}
			if (base.IsActive || Main.netMode != 0)
			{
				Main.windSpeedSpeed += (float)Main.rand.Next(-10, 11) * 0.0001f;
				if (!Main.dayTime)
				{
					Main.windSpeedSpeed += (float)Main.rand.Next(-10, 11) * 0.0002f;
				}
				if ((double)Main.windSpeedSpeed < -0.002)
				{
					Main.windSpeedSpeed = -0.002f;
				}
				if ((double)Main.windSpeedSpeed > 0.002)
				{
					Main.windSpeedSpeed = 0.002f;
				}
				Main.windSpeed += Main.windSpeedSpeed;
				if ((double)Main.windSpeed < -0.3)
				{
					Main.windSpeed = -0.3f;
				}
				if ((double)Main.windSpeed > 0.3)
				{
					Main.windSpeed = 0.3f;
				}
				Main.numClouds += Main.rand.Next(-1, 2);
				if (Main.numClouds < 0)
				{
					Main.numClouds = 0;
				}
				if (Main.numClouds > Main.cloudLimit)
				{
					Main.numClouds = Main.cloudLimit;
				}
			}
			if ((double)Main.screenPosition.Y < Main.worldSurface * 16.0 + 16.0)
			{
				for (int k = 0; k < 100; k++)
				{
					if (Main.cloud[k].active && Main.cloud[k].scale < 1f)
					{
						Color color2 = Main.cloud[k].cloudColor(Main.bgColor);
						if (num19 < 1f)
						{
							color2.R = (byte)((float)color2.R * num19);
							color2.G = (byte)((float)color2.G * num19);
							color2.B = (byte)((float)color2.B * num19);
							color2.A = (byte)((float)color2.A * num19);
						}
						float num28 = Main.cloud[k].position.Y * ((float)Main.screenHeight / 600f);
						float num29 = (float)((double)(Main.screenPosition.Y / 16f - 24f) / Main.worldSurface);
						if (num29 < 0f)
						{
							num29 = 0f;
						}
						if (num29 > 1f)
						{
						}
						if (Main.gameMenu)
						{
						}
						this.spriteBatch.Draw(Main.cloudTexture[Main.cloud[k].type], new Vector2(Main.cloud[k].position.X + (float)Main.cloudTexture[Main.cloud[k].type].Width * 0.5f, num28 + (float)Main.cloudTexture[Main.cloud[k].type].Height * 0.5f + num27), new Rectangle?(new Rectangle(0, 0, Main.cloudTexture[Main.cloud[k].type].Width, Main.cloudTexture[Main.cloud[k].type].Height)), color2, Main.cloud[k].rotation, new Vector2((float)Main.cloudTexture[Main.cloud[k].type].Width * 0.5f, (float)Main.cloudTexture[Main.cloud[k].type].Height * 0.5f), Main.cloud[k].scale, SpriteEffects.None, 0f);
					}
				}
			}
			num19 = 1f;
			float num30 = 1f;
			num30 *= 2f;
			this.bgParrallax = 0.15;
			int num31 = (int)((float)Main.backgroundWidth[7] * num30);
			Color color3 = Main.bgColor;
			Color color4 = color3;
			if (num19 < 1f)
			{
				color3.R = (byte)((float)color3.R * num19);
				color3.G = (byte)((float)color3.G * num19);
				color3.B = (byte)((float)color3.B * num19);
				color3.A = (byte)((float)color3.A * num19);
			}
			this.bgTop = (int)((double)(-(double)Main.screenPosition.Y + num26 / 2f) / (Main.worldSurface * 16.0) * 1300.0 + 1090.0);
			if (Main.owBack)
			{
				this.bgStart = (int)(-Math.IEEERemainder((double)Main.screenPosition.X * this.bgParrallax, (double)num31) - (double)(num31 / 2));
				this.bgLoops = Main.screenWidth / num31 + 2;
				if (Main.gameMenu)
				{
					this.bgTop = 100;
				}
				if ((double)Main.screenPosition.Y < Main.worldSurface * 16.0 + 16.0)
				{
					color3 = color4;
					color3.R = (byte)((float)color3.R * Main.bgAlpha2[0]);
					color3.G = (byte)((float)color3.G * Main.bgAlpha2[0]);
					color3.B = (byte)((float)color3.B * Main.bgAlpha2[0]);
					color3.A = (byte)((float)color3.A * Main.bgAlpha2[0]);
					if (Main.bgAlpha2[0] > 0f)
					{
						for (int l = 0; l < this.bgLoops; l++)
						{
							this.spriteBatch.Draw(Main.backgroundTexture[7], new Vector2((float)(this.bgStart + num31 * l), (float)this.bgTop), new Rectangle?(new Rectangle(0, 0, Main.backgroundWidth[7], Main.backgroundHeight[7])), color3, 0f, default(Vector2), num30, SpriteEffects.None, 0f);
						}
					}
					color3 = color4;
					color3.R = (byte)((float)color3.R * Main.bgAlpha2[1]);
					color3.G = (byte)((float)color3.G * Main.bgAlpha2[1]);
					color3.B = (byte)((float)color3.B * Main.bgAlpha2[1]);
					color3.A = (byte)((float)color3.A * Main.bgAlpha2[1]);
					if (Main.bgAlpha2[1] > 0f)
					{
						for (int m = 0; m < this.bgLoops; m++)
						{
							this.spriteBatch.Draw(Main.backgroundTexture[23], new Vector2((float)(this.bgStart + num31 * m), (float)this.bgTop), new Rectangle?(new Rectangle(0, 0, Main.backgroundWidth[7], Main.backgroundHeight[7])), color3, 0f, default(Vector2), num30, SpriteEffects.None, 0f);
						}
					}
					color3 = color4;
					color3.R = (byte)((float)color3.R * Main.bgAlpha2[2]);
					color3.G = (byte)((float)color3.G * Main.bgAlpha2[2]);
					color3.B = (byte)((float)color3.B * Main.bgAlpha2[2]);
					color3.A = (byte)((float)color3.A * Main.bgAlpha2[2]);
					if (Main.bgAlpha2[2] > 0f)
					{
						for (int n = 0; n < this.bgLoops; n++)
						{
							this.spriteBatch.Draw(Main.backgroundTexture[24], new Vector2((float)(this.bgStart + num31 * n), (float)this.bgTop), new Rectangle?(new Rectangle(0, 0, Main.backgroundWidth[7], Main.backgroundHeight[7])), color3, 0f, default(Vector2), num30, SpriteEffects.None, 0f);
						}
					}
				}
			}
			num27 = (float)(this.bgTop - 50);
			if ((double)Main.screenPosition.Y < Main.worldSurface * 16.0 + 16.0)
			{
				for (int num32 = 0; num32 < 100; num32++)
				{
					if (Main.cloud[num32].active && (double)Main.cloud[num32].scale < 1.15 && Main.cloud[num32].scale >= 1f)
					{
						Color color5 = Main.cloud[num32].cloudColor(Main.bgColor);
						if (num19 < 1f)
						{
							color5.R = (byte)((float)color5.R * num19);
							color5.G = (byte)((float)color5.G * num19);
							color5.B = (byte)((float)color5.B * num19);
							color5.A = (byte)((float)color5.A * num19);
						}
						float num33 = Main.cloud[num32].position.Y * ((float)Main.screenHeight / 600f);
						float num34 = (float)((double)(Main.screenPosition.Y / 16f - 24f) / Main.worldSurface);
						if (num34 < 0f)
						{
							num34 = 0f;
						}
						if (num34 > 1f)
						{
						}
						if (Main.gameMenu)
						{
						}
						this.spriteBatch.Draw(Main.cloudTexture[Main.cloud[num32].type], new Vector2(Main.cloud[num32].position.X + (float)Main.cloudTexture[Main.cloud[num32].type].Width * 0.5f, num33 + (float)Main.cloudTexture[Main.cloud[num32].type].Height * 0.5f + num27), new Rectangle?(new Rectangle(0, 0, Main.cloudTexture[Main.cloud[num32].type].Width, Main.cloudTexture[Main.cloud[num32].type].Height)), color5, Main.cloud[num32].rotation, new Vector2((float)Main.cloudTexture[Main.cloud[num32].type].Width * 0.5f, (float)Main.cloudTexture[Main.cloud[num32].type].Height * 0.5f), Main.cloud[num32].scale, SpriteEffects.None, 0f);
					}
				}
			}
			if (Main.holyTiles > 0 && Main.owBack)
			{
				this.bgParrallax = 0.17;
				num30 = 1.1f;
				num30 *= 2f;
				num31 = (int)((double)(3500f * num30) * 1.05);
				this.bgStart = (int)(-Math.IEEERemainder((double)Main.screenPosition.X * this.bgParrallax, (double)num31) - (double)(num31 / 2));
				this.bgLoops = Main.screenWidth / num31 + 2;
				this.bgTop = (int)((double)(-(double)Main.screenPosition.Y + num26 / 2f) / (Main.worldSurface * 16.0) * 1400.0 + 900.0);
				if (Main.gameMenu)
				{
					this.bgTop = 230;
					this.bgStart -= 500;
				}
				Color color6 = color4;
				float num35 = (float)Main.holyTiles / 400f;
				if (num35 > 0.5f)
				{
					num35 = 0.5f;
				}
				color6.R = (byte)((float)color6.R * num35);
				color6.G = (byte)((float)color6.G * num35);
				color6.B = (byte)((float)color6.B * num35);
				color6.A = (byte)((float)color6.A * num35 * 0.8f);
				if ((double)Main.screenPosition.Y < Main.worldSurface * 16.0 + 16.0)
				{
					for (int num36 = 0; num36 < this.bgLoops; num36++)
					{
						this.spriteBatch.Draw(Main.backgroundTexture[18], new Vector2((float)(this.bgStart + num31 * num36), (float)this.bgTop), new Rectangle?(new Rectangle(0, 0, Main.backgroundWidth[18], Main.backgroundHeight[18])), color6, 0f, default(Vector2), num30, SpriteEffects.None, 0f);
						this.spriteBatch.Draw(Main.backgroundTexture[19], new Vector2((float)(this.bgStart + num31 * num36 + 1700), (float)(this.bgTop + 100)), new Rectangle?(new Rectangle(0, 0, Main.backgroundWidth[19], Main.backgroundHeight[19])), color6, 0f, default(Vector2), num30 * 0.9f, SpriteEffects.None, 0f);
					}
				}
			}
			this.bgParrallax = 0.2;
			num30 = 1.15f;
			num30 *= 2f;
			num31 = (int)((float)Main.backgroundWidth[7] * num30);
			this.bgStart = (int)(-Math.IEEERemainder((double)Main.screenPosition.X * this.bgParrallax, (double)num31) - (double)(num31 / 2));
			this.bgLoops = Main.screenWidth / num31 + 2;
			this.bgTop = (int)((double)(-(double)Main.screenPosition.Y + num26 / 2f) / (Main.worldSurface * 16.0) * 1400.0 + 1260.0);
			if (Main.owBack)
			{
				if (Main.gameMenu)
				{
					this.bgTop = 230;
					this.bgStart -= 500;
				}
				if ((double)Main.screenPosition.Y < Main.worldSurface * 16.0 + 16.0)
				{
					color3 = color4;
					color3.R = (byte)((float)color3.R * Main.bgAlpha2[0]);
					color3.G = (byte)((float)color3.G * Main.bgAlpha2[0]);
					color3.B = (byte)((float)color3.B * Main.bgAlpha2[0]);
					color3.A = (byte)((float)color3.A * Main.bgAlpha2[0]);
					if (Main.bgAlpha2[0] > 0f)
					{
						for (int num37 = 0; num37 < this.bgLoops; num37++)
						{
							this.spriteBatch.Draw(Main.backgroundTexture[8], new Vector2((float)(this.bgStart + num31 * num37), (float)this.bgTop), new Rectangle?(new Rectangle(0, 0, Main.backgroundWidth[7], Main.backgroundHeight[7])), color3, 0f, default(Vector2), num30, SpriteEffects.None, 0f);
						}
					}
					color3 = color4;
					color3.R = (byte)((float)color3.R * Main.bgAlpha2[1]);
					color3.G = (byte)((float)color3.G * Main.bgAlpha2[1]);
					color3.B = (byte)((float)color3.B * Main.bgAlpha2[1]);
					color3.A = (byte)((float)color3.A * Main.bgAlpha2[1]);
					if (Main.bgAlpha2[1] > 0f)
					{
						for (int num38 = 0; num38 < this.bgLoops; num38++)
						{
							this.spriteBatch.Draw(Main.backgroundTexture[22], new Vector2((float)(this.bgStart + num31 * num38), (float)this.bgTop), new Rectangle?(new Rectangle(0, 0, Main.backgroundWidth[7], Main.backgroundHeight[7])), color3, 0f, default(Vector2), num30, SpriteEffects.None, 0f);
						}
					}
					color3 = color4;
					color3.R = (byte)((float)color3.R * Main.bgAlpha2[2]);
					color3.G = (byte)((float)color3.G * Main.bgAlpha2[2]);
					color3.B = (byte)((float)color3.B * Main.bgAlpha2[2]);
					color3.A = (byte)((float)color3.A * Main.bgAlpha2[2]);
					if (Main.bgAlpha2[2] > 0f)
					{
						for (int num39 = 0; num39 < this.bgLoops; num39++)
						{
							this.spriteBatch.Draw(Main.backgroundTexture[25], new Vector2((float)(this.bgStart + num31 * num39), (float)this.bgTop), new Rectangle?(new Rectangle(0, 0, Main.backgroundWidth[7], Main.backgroundHeight[7])), color3, 0f, default(Vector2), num30, SpriteEffects.None, 0f);
						}
					}
					color3 = color4;
					color3.R = (byte)((float)color3.R * Main.bgAlpha2[3]);
					color3.G = (byte)((float)color3.G * Main.bgAlpha2[3]);
					color3.B = (byte)((float)color3.B * Main.bgAlpha2[3]);
					color3.A = (byte)((float)color3.A * Main.bgAlpha2[3]);
					if (Main.bgAlpha2[3] > 0f)
					{
						for (int num40 = 0; num40 < this.bgLoops; num40++)
						{
							this.spriteBatch.Draw(Main.backgroundTexture[28], new Vector2((float)(this.bgStart + num31 * num40), (float)this.bgTop), new Rectangle?(new Rectangle(0, 0, Main.backgroundWidth[7], Main.backgroundHeight[7])), color3, 0f, default(Vector2), num30, SpriteEffects.None, 0f);
						}
					}
				}
			}
			num27 = (float)this.bgTop * 1.01f - 150f;
			if ((double)Main.screenPosition.Y < Main.worldSurface * 16.0 + 16.0)
			{
				for (int num41 = 0; num41 < 100; num41++)
				{
					if (Main.cloud[num41].active && Main.cloud[num41].scale > num30)
					{
						Color color7 = Main.cloud[num41].cloudColor(Main.bgColor);
						if (num19 < 1f)
						{
							color7.R = (byte)((float)color7.R * num19);
							color7.G = (byte)((float)color7.G * num19);
							color7.B = (byte)((float)color7.B * num19);
							color7.A = (byte)((float)color7.A * num19);
						}
						float num42 = Main.cloud[num41].position.Y * ((float)Main.screenHeight / 600f);
						float num43 = (float)((double)(Main.screenPosition.Y / 16f - 24f) / Main.worldSurface);
						if (num43 < 0f)
						{
							num43 = 0f;
						}
						if (num43 > 1f)
						{
						}
						if (Main.gameMenu)
						{
						}
						this.spriteBatch.Draw(Main.cloudTexture[Main.cloud[num41].type], new Vector2(Main.cloud[num41].position.X + (float)Main.cloudTexture[Main.cloud[num41].type].Width * 0.5f, num42 + (float)Main.cloudTexture[Main.cloud[num41].type].Height * 0.5f + num27), new Rectangle?(new Rectangle(0, 0, Main.cloudTexture[Main.cloud[num41].type].Width, Main.cloudTexture[Main.cloud[num41].type].Height)), color7, Main.cloud[num41].rotation, new Vector2((float)Main.cloudTexture[Main.cloud[num41].type].Width * 0.5f, (float)Main.cloudTexture[Main.cloud[num41].type].Height * 0.5f), Main.cloud[num41].scale, SpriteEffects.None, 0f);
					}
				}
			}
			int num44 = Main.bgStyle;
			int num45 = (int)((Main.screenPosition.X + (float)(Main.screenWidth / 2)) / 16f);
			if (num45 < 380 || num45 > Main.maxTilesX - 380)
			{
				num44 = 4;
			}
			else
			{
				if (Main.sandTiles > 1000)
				{
					if (Main.player[Main.myPlayer].zoneEvil)
					{
						num44 = 5;
					}
					else
					{
						if (Main.player[Main.myPlayer].zoneHoly)
						{
							num44 = 5;
						}
						else
						{
							num44 = 2;
						}
					}
				}
				else
				{
					if (Main.player[Main.myPlayer].zoneHoly)
					{
						num44 = 6;
					}
					else
					{
						if (Main.player[Main.myPlayer].zoneEvil)
						{
							num44 = 1;
						}
						else
						{
							if (Main.player[Main.myPlayer].zoneJungle)
							{
								num44 = 3;
							}
							else
							{
								num44 = 0;
							}
						}
					}
				}
			}
			float num46 = 0.05f;
			int num47 = 30;
			if (num44 == 0)
			{
				num47 = 120;
			}
			if (Main.bgDelay < 0)
			{
				Main.bgDelay++;
			}
			else
			{
				if (num44 != Main.bgStyle)
				{
					Main.bgDelay++;
					if (Main.bgDelay > num47)
					{
						Main.bgDelay = -60;
						Main.bgStyle = num44;
						if (num44 == 0)
						{
							Main.bgDelay = 0;
						}
					}
				}
				else
				{
					if (Main.bgDelay > 0)
					{
						Main.bgDelay--;
					}
				}
			}
			if (Main.gameMenu)
			{
				num46 = 0.02f;
				if (!Main.dayTime)
				{
					Main.bgStyle = 1;
				}
				else
				{
					Main.bgStyle = 0;
				}
				num44 = Main.bgStyle;
			}
			if (Main.quickBG > 0)
			{
				Main.quickBG--;
				Main.bgStyle = num44;
				num46 = 1f;
			}
			if (Main.bgStyle == 2)
			{
				Main.bgAlpha2[0] -= num46;
				if (Main.bgAlpha2[0] < 0f)
				{
					Main.bgAlpha2[0] = 0f;
				}
				Main.bgAlpha2[1] += num46;
				if (Main.bgAlpha2[1] > 1f)
				{
					Main.bgAlpha2[1] = 1f;
				}
				Main.bgAlpha2[2] -= num46;
				if (Main.bgAlpha2[2] < 0f)
				{
					Main.bgAlpha2[2] = 0f;
				}
				Main.bgAlpha2[3] -= num46;
				if (Main.bgAlpha2[3] < 0f)
				{
					Main.bgAlpha2[3] = 0f;
				}
			}
			else
			{
				if (Main.bgStyle == 5 || Main.bgStyle == 1 || Main.bgStyle == 6)
				{
					Main.bgAlpha2[0] -= num46;
					if (Main.bgAlpha2[0] < 0f)
					{
						Main.bgAlpha2[0] = 0f;
					}
					Main.bgAlpha2[1] -= num46;
					if (Main.bgAlpha2[1] < 0f)
					{
						Main.bgAlpha2[1] = 0f;
					}
					Main.bgAlpha2[2] += num46;
					if (Main.bgAlpha2[2] > 1f)
					{
						Main.bgAlpha2[2] = 1f;
					}
					Main.bgAlpha2[3] -= num46;
					if (Main.bgAlpha2[3] < 0f)
					{
						Main.bgAlpha2[3] = 0f;
					}
				}
				else
				{
					if (Main.bgStyle == 4)
					{
						Main.bgAlpha2[0] -= num46;
						if (Main.bgAlpha2[0] < 0f)
						{
							Main.bgAlpha2[0] = 0f;
						}
						Main.bgAlpha2[1] -= num46;
						if (Main.bgAlpha2[1] < 0f)
						{
							Main.bgAlpha2[1] = 0f;
						}
						Main.bgAlpha2[2] -= num46;
						if (Main.bgAlpha2[2] < 0f)
						{
							Main.bgAlpha2[2] = 0f;
						}
						Main.bgAlpha2[3] += num46;
						if (Main.bgAlpha2[3] > 1f)
						{
							Main.bgAlpha2[3] = 1f;
						}
					}
					else
					{
						Main.bgAlpha2[0] += num46;
						if (Main.bgAlpha2[0] > 1f)
						{
							Main.bgAlpha2[0] = 1f;
						}
						Main.bgAlpha2[1] -= num46;
						if (Main.bgAlpha2[1] < 0f)
						{
							Main.bgAlpha2[1] = 0f;
						}
						Main.bgAlpha2[2] -= num46;
						if (Main.bgAlpha2[2] < 0f)
						{
							Main.bgAlpha2[2] = 0f;
						}
						Main.bgAlpha2[3] -= num46;
						if (Main.bgAlpha2[3] < 0f)
						{
							Main.bgAlpha2[3] = 0f;
						}
					}
				}
			}
			for (int num48 = 0; num48 < 7; num48++)
			{
				if (Main.bgStyle == num48)
				{
					Main.bgAlpha[num48] += num46;
					if (Main.bgAlpha[num48] > 1f)
					{
						Main.bgAlpha[num48] = 1f;
					}
				}
				else
				{
					Main.bgAlpha[num48] -= num46;
					if (Main.bgAlpha[num48] < 0f)
					{
						Main.bgAlpha[num48] = 0f;
					}
				}
				if (Main.owBack)
				{
					color3 = color4;
					color3.R = (byte)((float)color3.R * Main.bgAlpha[num48]);
					color3.G = (byte)((float)color3.G * Main.bgAlpha[num48]);
					color3.B = (byte)((float)color3.B * Main.bgAlpha[num48]);
					color3.A = (byte)((float)color3.A * Main.bgAlpha[num48]);
					if (Main.bgAlpha[num48] > 0f && num48 == 3)
					{
						num30 = 1.25f;
						num30 *= 2f;
						num31 = (int)((float)Main.backgroundWidth[8] * num30);
						this.bgParrallax = 0.4;
						this.bgStart = (int)(-Math.IEEERemainder((double)Main.screenPosition.X * this.bgParrallax, (double)num31) - (double)(num31 / 2));
						this.bgTop = (int)((double)(-(double)Main.screenPosition.Y + num26 / 2f) / (Main.worldSurface * 16.0) * 1800.0 + 1660.0);
						if (Main.gameMenu)
						{
							this.bgTop = 320;
						}
						this.bgLoops = Main.screenWidth / num31 + 2;
						if ((double)Main.screenPosition.Y < Main.worldSurface * 16.0 + 16.0)
						{
							for (int num49 = 0; num49 < this.bgLoops; num49++)
							{
								this.spriteBatch.Draw(Main.backgroundTexture[15], new Vector2((float)(this.bgStart + num31 * num49), (float)this.bgTop), new Rectangle?(new Rectangle(0, 0, Main.backgroundWidth[8], Main.backgroundHeight[8])), color3, 0f, default(Vector2), num30, SpriteEffects.None, 0f);
							}
						}
						num30 = 1.31f;
						num30 *= 2f;
						num31 = (int)((float)Main.backgroundWidth[8] * num30);
						this.bgParrallax = 0.43;
						this.bgStart = (int)(-Math.IEEERemainder((double)Main.screenPosition.X * this.bgParrallax, (double)num31) - (double)(num31 / 2));
						this.bgTop = (int)((double)(-(double)Main.screenPosition.Y + num26 / 2f) / (Main.worldSurface * 16.0) * 1950.0 + 1840.0);
						if (Main.gameMenu)
						{
							this.bgTop = 400;
							this.bgStart -= 80;
						}
						this.bgLoops = Main.screenWidth / num31 + 2;
						if ((double)Main.screenPosition.Y < Main.worldSurface * 16.0 + 16.0)
						{
							for (int num50 = 0; num50 < this.bgLoops; num50++)
							{
								this.spriteBatch.Draw(Main.backgroundTexture[16], new Vector2((float)(this.bgStart + num31 * num50), (float)this.bgTop), new Rectangle?(new Rectangle(0, 0, Main.backgroundWidth[8], Main.backgroundHeight[8])), color3, 0f, default(Vector2), num30, SpriteEffects.None, 0f);
							}
						}
						num30 = 1.34f;
						num30 *= 2f;
						num31 = (int)((float)Main.backgroundWidth[8] * num30);
						this.bgParrallax = 0.49;
						this.bgStart = (int)(-Math.IEEERemainder((double)Main.screenPosition.X * this.bgParrallax, (double)num31) - (double)(num31 / 2));
						this.bgTop = (int)((double)(-(double)Main.screenPosition.Y + num26 / 2f) / (Main.worldSurface * 16.0) * 2100.0 + 2060.0);
						if (Main.gameMenu)
						{
							this.bgTop = 480;
							this.bgStart -= 120;
						}
						this.bgLoops = Main.screenWidth / num31 + 2;
						if ((double)Main.screenPosition.Y < Main.worldSurface * 16.0 + 16.0)
						{
							for (int num51 = 0; num51 < this.bgLoops; num51++)
							{
								this.spriteBatch.Draw(Main.backgroundTexture[17], new Vector2((float)(this.bgStart + num31 * num51), (float)this.bgTop), new Rectangle?(new Rectangle(0, 0, Main.backgroundWidth[8], Main.backgroundHeight[8])), color3, 0f, default(Vector2), num30, SpriteEffects.None, 0f);
							}
						}
					}
					if (Main.bgAlpha[num48] > 0f && num48 == 2)
					{
						num30 = 1.25f;
						num30 *= 2f;
						num31 = (int)((float)Main.backgroundWidth[8] * num30);
						this.bgParrallax = 0.37;
						this.bgStart = (int)(-Math.IEEERemainder((double)Main.screenPosition.X * this.bgParrallax, (double)num31) - (double)(num31 / 2));
						this.bgTop = (int)((double)(-(double)Main.screenPosition.Y + num26 / 2f) / (Main.worldSurface * 16.0) * 1800.0 + 1750.0);
						if (Main.gameMenu)
						{
							this.bgTop = 320;
						}
						this.bgLoops = Main.screenWidth / num31 + 2;
						if ((double)Main.screenPosition.Y < Main.worldSurface * 16.0 + 16.0)
						{
							for (int num52 = 0; num52 < this.bgLoops; num52++)
							{
								this.spriteBatch.Draw(Main.backgroundTexture[21], new Vector2((float)(this.bgStart + num31 * num52), (float)this.bgTop), new Rectangle?(new Rectangle(0, 0, Main.backgroundWidth[8], Main.backgroundHeight[20])), color3, 0f, default(Vector2), num30, SpriteEffects.None, 0f);
							}
						}
						num30 = 1.34f;
						num30 *= 2f;
						num31 = (int)((float)Main.backgroundWidth[8] * num30);
						this.bgParrallax = 0.49;
						this.bgStart = (int)(-Math.IEEERemainder((double)Main.screenPosition.X * this.bgParrallax, (double)num31) - (double)(num31 / 2));
						this.bgTop = (int)((double)(-(double)Main.screenPosition.Y + num26 / 2f) / (Main.worldSurface * 16.0) * 2100.0 + 2150.0);
						if (Main.gameMenu)
						{
							this.bgTop = 480;
							this.bgStart -= 120;
						}
						this.bgLoops = Main.screenWidth / num31 + 2;
						if ((double)Main.screenPosition.Y < Main.worldSurface * 16.0 + 16.0)
						{
							for (int num53 = 0; num53 < this.bgLoops; num53++)
							{
								this.spriteBatch.Draw(Main.backgroundTexture[20], new Vector2((float)(this.bgStart + num31 * num53), (float)this.bgTop), new Rectangle?(new Rectangle(0, 0, Main.backgroundWidth[8], Main.backgroundHeight[20])), color3, 0f, default(Vector2), num30, SpriteEffects.None, 0f);
							}
						}
					}
					if (Main.bgAlpha[num48] > 0f && num48 == 5)
					{
						num30 = 1.25f;
						num30 *= 2f;
						num31 = (int)((float)Main.backgroundWidth[8] * num30);
						this.bgParrallax = 0.37;
						this.bgStart = (int)(-Math.IEEERemainder((double)Main.screenPosition.X * this.bgParrallax, (double)num31) - (double)(num31 / 2));
						this.bgTop = (int)((double)(-(double)Main.screenPosition.Y + num26 / 2f) / (Main.worldSurface * 16.0) * 1800.0 + 1750.0);
						if (Main.gameMenu)
						{
							this.bgTop = 320;
						}
						this.bgLoops = Main.screenWidth / num31 + 2;
						if ((double)Main.screenPosition.Y < Main.worldSurface * 16.0 + 16.0)
						{
							for (int num54 = 0; num54 < this.bgLoops; num54++)
							{
								this.spriteBatch.Draw(Main.backgroundTexture[26], new Vector2((float)(this.bgStart + num31 * num54), (float)this.bgTop), new Rectangle?(new Rectangle(0, 0, Main.backgroundWidth[8], Main.backgroundHeight[20])), color3, 0f, default(Vector2), num30, SpriteEffects.None, 0f);
							}
						}
						num30 = 1.34f;
						num30 *= 2f;
						num31 = (int)((float)Main.backgroundWidth[8] * num30);
						this.bgParrallax = 0.49;
						this.bgStart = (int)(-Math.IEEERemainder((double)Main.screenPosition.X * this.bgParrallax, (double)num31) - (double)(num31 / 2));
						this.bgTop = (int)((double)(-(double)Main.screenPosition.Y + num26 / 2f) / (Main.worldSurface * 16.0) * 2100.0 + 2150.0);
						if (Main.gameMenu)
						{
							this.bgTop = 480;
							this.bgStart -= 120;
						}
						this.bgLoops = Main.screenWidth / num31 + 2;
						if ((double)Main.screenPosition.Y < Main.worldSurface * 16.0 + 16.0)
						{
							for (int num55 = 0; num55 < this.bgLoops; num55++)
							{
								this.spriteBatch.Draw(Main.backgroundTexture[27], new Vector2((float)(this.bgStart + num31 * num55), (float)this.bgTop), new Rectangle?(new Rectangle(0, 0, Main.backgroundWidth[8], Main.backgroundHeight[20])), color3, 0f, default(Vector2), num30, SpriteEffects.None, 0f);
							}
						}
					}
					if (Main.bgAlpha[num48] > 0f && num48 == 1)
					{
						num30 = 1.25f;
						num30 *= 2f;
						num31 = (int)((float)Main.backgroundWidth[8] * num30);
						this.bgParrallax = 0.4;
						this.bgStart = (int)(-Math.IEEERemainder((double)Main.screenPosition.X * this.bgParrallax, (double)num31) - (double)(num31 / 2));
						this.bgTop = (int)((double)(-(double)Main.screenPosition.Y + num26 / 2f) / (Main.worldSurface * 16.0) * 1800.0 + 1500.0);
						if (Main.gameMenu)
						{
							this.bgTop = 320;
						}
						this.bgLoops = Main.screenWidth / num31 + 2;
						if ((double)Main.screenPosition.Y < Main.worldSurface * 16.0 + 16.0)
						{
							for (int num56 = 0; num56 < this.bgLoops; num56++)
							{
								this.spriteBatch.Draw(Main.backgroundTexture[12], new Vector2((float)(this.bgStart + num31 * num56), (float)this.bgTop), new Rectangle?(new Rectangle(0, 0, Main.backgroundWidth[8], Main.backgroundHeight[8])), color3, 0f, default(Vector2), num30, SpriteEffects.None, 0f);
							}
						}
						num30 = 1.31f;
						num30 *= 2f;
						num31 = (int)((float)Main.backgroundWidth[8] * num30);
						this.bgParrallax = 0.43;
						this.bgStart = (int)(-Math.IEEERemainder((double)Main.screenPosition.X * this.bgParrallax, (double)num31) - (double)(num31 / 2));
						this.bgTop = (int)((double)(-(double)Main.screenPosition.Y + num26 / 2f) / (Main.worldSurface * 16.0) * 1950.0 + 1750.0);
						if (Main.gameMenu)
						{
							this.bgTop = 400;
							this.bgStart -= 80;
						}
						this.bgLoops = Main.screenWidth / num31 + 2;
						if ((double)Main.screenPosition.Y < Main.worldSurface * 16.0 + 16.0)
						{
							for (int num57 = 0; num57 < this.bgLoops; num57++)
							{
								this.spriteBatch.Draw(Main.backgroundTexture[13], new Vector2((float)(this.bgStart + num31 * num57), (float)this.bgTop), new Rectangle?(new Rectangle(0, 0, Main.backgroundWidth[8], Main.backgroundHeight[8])), color3, 0f, default(Vector2), num30, SpriteEffects.None, 0f);
							}
						}
						num30 = 1.34f;
						num30 *= 2f;
						num31 = (int)((float)Main.backgroundWidth[8] * num30);
						this.bgParrallax = 0.49;
						this.bgStart = (int)(-Math.IEEERemainder((double)Main.screenPosition.X * this.bgParrallax, (double)num31) - (double)(num31 / 2));
						this.bgTop = (int)((double)(-(double)Main.screenPosition.Y + num26 / 2f) / (Main.worldSurface * 16.0) * 2100.0 + 2000.0);
						if (Main.gameMenu)
						{
							this.bgTop = 480;
							this.bgStart -= 120;
						}
						this.bgLoops = Main.screenWidth / num31 + 2;
						if ((double)Main.screenPosition.Y < Main.worldSurface * 16.0 + 16.0)
						{
							for (int num58 = 0; num58 < this.bgLoops; num58++)
							{
								this.spriteBatch.Draw(Main.backgroundTexture[14], new Vector2((float)(this.bgStart + num31 * num58), (float)this.bgTop), new Rectangle?(new Rectangle(0, 0, Main.backgroundWidth[8], Main.backgroundHeight[8])), color3, 0f, default(Vector2), num30, SpriteEffects.None, 0f);
							}
						}
					}
					if (Main.bgAlpha[num48] > 0f && num48 == 6)
					{
						num30 = 1.25f;
						num30 *= 2f;
						num31 = (int)((float)Main.backgroundWidth[8] * num30);
						this.bgParrallax = 0.4;
						this.bgStart = (int)(-Math.IEEERemainder((double)Main.screenPosition.X * this.bgParrallax, (double)num31) - (double)(num31 / 2));
						this.bgTop = (int)((double)(-(double)Main.screenPosition.Y + num26 / 2f) / (Main.worldSurface * 16.0) * 1800.0 + 1500.0);
						if (Main.gameMenu)
						{
							this.bgTop = 320;
						}
						this.bgLoops = Main.screenWidth / num31 + 2;
						if ((double)Main.screenPosition.Y < Main.worldSurface * 16.0 + 16.0)
						{
							for (int num59 = 0; num59 < this.bgLoops; num59++)
							{
								this.spriteBatch.Draw(Main.backgroundTexture[29], new Vector2((float)(this.bgStart + num31 * num59), (float)this.bgTop), new Rectangle?(new Rectangle(0, 0, Main.backgroundWidth[8], Main.backgroundHeight[8])), color3, 0f, default(Vector2), num30, SpriteEffects.None, 0f);
							}
						}
						num30 = 1.31f;
						num30 *= 2f;
						num31 = (int)((float)Main.backgroundWidth[8] * num30);
						this.bgParrallax = 0.43;
						this.bgStart = (int)(-Math.IEEERemainder((double)Main.screenPosition.X * this.bgParrallax, (double)num31) - (double)(num31 / 2));
						this.bgTop = (int)((double)(-(double)Main.screenPosition.Y + num26 / 2f) / (Main.worldSurface * 16.0) * 1950.0 + 1750.0);
						if (Main.gameMenu)
						{
							this.bgTop = 400;
							this.bgStart -= 80;
						}
						this.bgLoops = Main.screenWidth / num31 + 2;
						if ((double)Main.screenPosition.Y < Main.worldSurface * 16.0 + 16.0)
						{
							for (int num60 = 0; num60 < this.bgLoops; num60++)
							{
								this.spriteBatch.Draw(Main.backgroundTexture[30], new Vector2((float)(this.bgStart + num31 * num60), (float)this.bgTop), new Rectangle?(new Rectangle(0, 0, Main.backgroundWidth[8], Main.backgroundHeight[8])), color3, 0f, default(Vector2), num30, SpriteEffects.None, 0f);
							}
						}
						num30 = 1.34f;
						num30 *= 2f;
						num31 = (int)((float)Main.backgroundWidth[8] * num30);
						this.bgParrallax = 0.49;
						this.bgStart = (int)(-Math.IEEERemainder((double)Main.screenPosition.X * this.bgParrallax, (double)num31) - (double)(num31 / 2));
						this.bgTop = (int)((double)(-(double)Main.screenPosition.Y + num26 / 2f) / (Main.worldSurface * 16.0) * 2100.0 + 2000.0);
						if (Main.gameMenu)
						{
							this.bgTop = 480;
							this.bgStart -= 120;
						}
						this.bgLoops = Main.screenWidth / num31 + 2;
						if ((double)Main.screenPosition.Y < Main.worldSurface * 16.0 + 16.0)
						{
							for (int num61 = 0; num61 < this.bgLoops; num61++)
							{
								this.spriteBatch.Draw(Main.backgroundTexture[31], new Vector2((float)(this.bgStart + num31 * num61), (float)this.bgTop), new Rectangle?(new Rectangle(0, 0, Main.backgroundWidth[8], Main.backgroundHeight[8])), color3, 0f, default(Vector2), num30, SpriteEffects.None, 0f);
							}
						}
					}
					if (Main.bgAlpha[num48] > 0f && num48 == 0)
					{
						num30 = 1.25f;
						num30 *= 2f;
						num31 = (int)((float)Main.backgroundWidth[8] * num30);
						this.bgParrallax = 0.4;
						this.bgStart = (int)(-Math.IEEERemainder((double)Main.screenPosition.X * this.bgParrallax, (double)num31) - (double)(num31 / 2));
						this.bgTop = (int)((double)(-(double)Main.screenPosition.Y + num26 / 2f) / (Main.worldSurface * 16.0) * 1800.0 + 1500.0);
						if (Main.gameMenu)
						{
							this.bgTop = 320;
						}
						this.bgLoops = Main.screenWidth / num31 + 2;
						if ((double)Main.screenPosition.Y < Main.worldSurface * 16.0 + 16.0)
						{
							for (int num62 = 0; num62 < this.bgLoops; num62++)
							{
								this.spriteBatch.Draw(Main.backgroundTexture[9], new Vector2((float)(this.bgStart + num31 * num62), (float)this.bgTop), new Rectangle?(new Rectangle(0, 0, Main.backgroundWidth[8], Main.backgroundHeight[8])), color3, 0f, default(Vector2), num30, SpriteEffects.None, 0f);
							}
						}
						num30 = 1.31f;
						num30 *= 2f;
						num31 = (int)((float)Main.backgroundWidth[8] * num30);
						this.bgParrallax = 0.43;
						this.bgStart = (int)(-Math.IEEERemainder((double)Main.screenPosition.X * this.bgParrallax, (double)num31) - (double)(num31 / 2));
						this.bgTop = (int)((double)(-(double)Main.screenPosition.Y + num26 / 2f) / (Main.worldSurface * 16.0) * 1950.0 + 1750.0);
						if (Main.gameMenu)
						{
							this.bgTop = 400;
							this.bgStart -= 80;
						}
						this.bgLoops = Main.screenWidth / num31 + 2;
						if ((double)Main.screenPosition.Y < Main.worldSurface * 16.0 + 16.0)
						{
							for (int num63 = 0; num63 < this.bgLoops; num63++)
							{
								this.spriteBatch.Draw(Main.backgroundTexture[10], new Vector2((float)(this.bgStart + num31 * num63), (float)this.bgTop), new Rectangle?(new Rectangle(0, 0, Main.backgroundWidth[8], Main.backgroundHeight[8])), color3, 0f, default(Vector2), num30, SpriteEffects.None, 0f);
							}
						}
						num30 = 1.34f;
						num30 *= 2f;
						num31 = (int)((float)Main.backgroundWidth[8] * num30);
						this.bgParrallax = 0.49;
						this.bgStart = (int)(-Math.IEEERemainder((double)Main.screenPosition.X * this.bgParrallax, (double)num31) - (double)(num31 / 2));
						this.bgTop = (int)((double)(-(double)Main.screenPosition.Y + num26 / 2f) / (Main.worldSurface * 16.0) * 2100.0 + 2000.0);
						if (Main.gameMenu)
						{
							this.bgTop = 480;
							this.bgStart -= 120;
						}
						this.bgLoops = Main.screenWidth / num31 + 2;
						if ((double)Main.screenPosition.Y < Main.worldSurface * 16.0 + 16.0)
						{
							for (int num64 = 0; num64 < this.bgLoops; num64++)
							{
								this.spriteBatch.Draw(Main.backgroundTexture[11], new Vector2((float)(this.bgStart + num31 * num64), (float)this.bgTop), new Rectangle?(new Rectangle(0, 0, Main.backgroundWidth[8], Main.backgroundHeight[8])), color3, 0f, default(Vector2), num30, SpriteEffects.None, 0f);
							}
						}
					}
				}
			}
			if (Main.gameMenu || Main.netMode == 2)
			{
				this.DrawMenu();
				return;
			}
			this.firstTileX = (int)(Main.screenPosition.X / 16f - 1f);
			this.lastTileX = (int)((Main.screenPosition.X + (float)Main.screenWidth) / 16f) + 2;
			this.firstTileY = (int)(Main.screenPosition.Y / 16f - 1f);
			this.lastTileY = (int)((Main.screenPosition.Y + (float)Main.screenHeight) / 16f) + 2;
			if (this.firstTileX < 0)
			{
				this.firstTileX = 0;
			}
			if (this.lastTileX > Main.maxTilesX)
			{
				this.lastTileX = Main.maxTilesX;
			}
			if (this.firstTileY < 0)
			{
				this.firstTileY = 0;
			}
			if (this.lastTileY > Main.maxTilesY)
			{
				this.lastTileY = Main.maxTilesY;
			}
			if (!Main.drawSkip)
			{
				Lighting.LightTiles(this.firstTileX, this.lastTileX, this.firstTileY, this.lastTileY);
			}
			Color arg_53D7_0 = Color.White;
			if (Main.drawToScreen)
			{
				this.DrawWater(true);
			}
			else
			{
				this.spriteBatch.Draw(this.backWaterTarget, Main.sceneBackgroundPos - Main.screenPosition, Color.White);
			}
			float x = (Main.sceneBackgroundPos.X - Main.screenPosition.X + (float)Main.offScreenRange) * Main.caveParrallax - (float)Main.offScreenRange;
			if (Main.drawToScreen)
			{
				this.DrawBackground();
			}
			else
			{
				this.spriteBatch.Draw(this.backgroundTarget, new Vector2(x, Main.sceneBackgroundPos.Y - Main.screenPosition.Y), Color.White);
			}
			Main.magmaBGFrameCounter++;
			if (Main.magmaBGFrameCounter >= 8)
			{
				Main.magmaBGFrameCounter = 0;
				Main.magmaBGFrame++;
				if (Main.magmaBGFrame >= 3)
				{
					Main.magmaBGFrame = 0;
				}
			}
			try
			{
				if (Main.drawToScreen)
				{
					this.DrawBlack();
					this.DrawWalls();
				}
				else
				{
					this.spriteBatch.Draw(this.blackTarget, Main.sceneTilePos - Main.screenPosition, Color.White);
					this.spriteBatch.Draw(this.wallTarget, Main.sceneWallPos - Main.screenPosition, Color.White);
				}
				this.DrawWoF();
				if (Main.player[Main.myPlayer].detectCreature)
				{
					if (Main.drawToScreen)
					{
						this.DrawTiles(false);
						this.DrawTiles(true);
					}
					else
					{
						this.spriteBatch.Draw(this.tile2Target, Main.sceneTile2Pos - Main.screenPosition, Color.White);
						this.spriteBatch.Draw(this.tileTarget, Main.sceneTilePos - Main.screenPosition, Color.White);
					}
					this.DrawGore();
					this.DrawNPCs(true);
					this.DrawNPCs(false);
				}
				else
				{
					if (Main.drawToScreen)
					{
						this.DrawTiles(false);
						this.DrawNPCs(true);
						this.DrawTiles(true);
					}
					else
					{
						this.spriteBatch.Draw(this.tile2Target, Main.sceneTile2Pos - Main.screenPosition, Color.White);
						this.DrawNPCs(true);
						this.spriteBatch.Draw(this.tileTarget, Main.sceneTilePos - Main.screenPosition, Color.White);
					}
					this.DrawGore();
					this.DrawNPCs(false);
				}
			}
			catch
			{
			}
			for (int num65 = 0; num65 < 1000; num65++)
			{
				if (Main.projectile[num65].active && Main.projectile[num65].type > 0 && !Main.projectile[num65].hide)
				{
					this.DrawProj(num65);
				}
			}
			for (int num66 = 0; num66 < 255; num66++)
			{
				if (Main.player[num66].active)
				{
					if (Main.player[num66].ghost)
					{
						Vector2 position = Main.player[num66].position;
						Main.player[num66].position = Main.player[num66].shadowPos[0];
						Main.player[num66].shadow = 0.5f;
						this.DrawGhost(Main.player[num66]);
						Main.player[num66].position = Main.player[num66].shadowPos[1];
						Main.player[num66].shadow = 0.7f;
						this.DrawGhost(Main.player[num66]);
						Main.player[num66].position = Main.player[num66].shadowPos[2];
						Main.player[num66].shadow = 0.9f;
						this.DrawGhost(Main.player[num66]);
						Main.player[num66].position = position;
						Main.player[num66].shadow = 0f;
						this.DrawGhost(Main.player[num66]);
					}
					else
					{
						bool flag2 = false;
						bool flag3 = false;
						if (Main.player[num66].head == 5 && Main.player[num66].body == 5 && Main.player[num66].legs == 5)
						{
							flag2 = true;
						}
						if (Main.player[num66].head == 7 && Main.player[num66].body == 7 && Main.player[num66].legs == 7)
						{
							flag2 = true;
						}
						if (Main.player[num66].head == 22 && Main.player[num66].body == 14 && Main.player[num66].legs == 14)
						{
							flag2 = true;
						}
						if (Main.player[num66].body == 17 && Main.player[num66].legs == 16 && (Main.player[num66].head == 29 || Main.player[num66].head == 30 || Main.player[num66].head == 31))
						{
							flag2 = true;
						}
						if (Main.player[num66].body == 19 && Main.player[num66].legs == 18 && (Main.player[num66].head == 35 || Main.player[num66].head == 36 || Main.player[num66].head == 37))
						{
							flag3 = true;
						}
						if (Main.player[num66].body == 24 && Main.player[num66].legs == 23 && (Main.player[num66].head == 41 || Main.player[num66].head == 42 || Main.player[num66].head == 43))
						{
							flag3 = true;
							flag2 = true;
						}
						if (flag3)
						{
							Vector2 position2 = Main.player[num66].position;
							if (!Main.gamePaused)
							{
								Main.player[num66].ghostFade += Main.player[num66].ghostDir * 0.075f;
							}
							if ((double)Main.player[num66].ghostFade < 0.1)
							{
								Main.player[num66].ghostDir = 1f;
								Main.player[num66].ghostFade = 0.1f;
							}
							if ((double)Main.player[num66].ghostFade > 0.9)
							{
								Main.player[num66].ghostDir = -1f;
								Main.player[num66].ghostFade = 0.9f;
							}
							Main.player[num66].position.X = position2.X - Main.player[num66].ghostFade * 5f;
							Main.player[num66].shadow = Main.player[num66].ghostFade;
							this.DrawPlayer(Main.player[num66]);
							Main.player[num66].position.X = position2.X + Main.player[num66].ghostFade * 5f;
							Main.player[num66].shadow = Main.player[num66].ghostFade;
							this.DrawPlayer(Main.player[num66]);
							Main.player[num66].position = position2;
							Main.player[num66].position.Y = position2.Y - Main.player[num66].ghostFade * 5f;
							Main.player[num66].shadow = Main.player[num66].ghostFade;
							this.DrawPlayer(Main.player[num66]);
							Main.player[num66].position.Y = position2.Y + Main.player[num66].ghostFade * 5f;
							Main.player[num66].shadow = Main.player[num66].ghostFade;
							this.DrawPlayer(Main.player[num66]);
							Main.player[num66].position = position2;
							Main.player[num66].shadow = 0f;
						}
						if (flag2)
						{
							Vector2 position3 = Main.player[num66].position;
							Main.player[num66].position = Main.player[num66].shadowPos[0];
							Main.player[num66].shadow = 0.5f;
							this.DrawPlayer(Main.player[num66]);
							Main.player[num66].position = Main.player[num66].shadowPos[1];
							Main.player[num66].shadow = 0.7f;
							this.DrawPlayer(Main.player[num66]);
							Main.player[num66].position = Main.player[num66].shadowPos[2];
							Main.player[num66].shadow = 0.9f;
							this.DrawPlayer(Main.player[num66]);
							Main.player[num66].position = position3;
							Main.player[num66].shadow = 0f;
						}
						this.DrawPlayer(Main.player[num66]);
					}
				}
			}
			if (!Main.gamePaused)
			{
				Main.essScale += (float)Main.essDir * 0.01f;
				if (Main.essScale > 1f)
				{
					Main.essDir = -1;
					Main.essScale = 1f;
				}
				if ((double)Main.essScale < 0.7)
				{
					Main.essDir = 1;
					Main.essScale = 0.7f;
				}
			}
			for (int num67 = 0; num67 < 200; num67++)
			{
				if (Main.item[num67].active && Main.item[num67].type > 0)
				{
					int arg_5D71_0 = (int)((double)Main.item[num67].position.X + (double)Main.item[num67].width * 0.5) / 16;
					int arg_5D77_0 = Lighting.offScreenTiles;
					int arg_5DA8_0 = (int)((double)Main.item[num67].position.Y + (double)Main.item[num67].height * 0.5) / 16;
					int arg_5DAE_0 = Lighting.offScreenTiles;
					Color color8 = Lighting.GetColor((int)((double)Main.item[num67].position.X + (double)Main.item[num67].width * 0.5) / 16, (int)((double)Main.item[num67].position.Y + (double)Main.item[num67].height * 0.5) / 16);
					if (!Main.gamePaused && base.IsActive && ((Main.item[num67].type >= 71 && Main.item[num67].type <= 74) || Main.item[num67].type == 58 || Main.item[num67].type == 109) && color8.R > 60)
					{
						float num68 = (float)Main.rand.Next(500) - (Math.Abs(Main.item[num67].velocity.X) + Math.Abs(Main.item[num67].velocity.Y)) * 10f;
						if (num68 < (float)(color8.R / 50))
						{
							int num69 = Dust.NewDust(Main.item[num67].position, Main.item[num67].width, Main.item[num67].height, 43, 0f, 0f, 254, default(Color), 0.5f);
							Main.dust[num69].velocity *= 0f;
						}
					}
					float rotation3 = Main.item[num67].velocity.X * 0.2f;
					float num70 = 1f;
					Color alpha = Main.item[num67].GetAlpha(color8);
					if (Main.item[num67].type == 520 || Main.item[num67].type == 521 || Main.item[num67].type == 547 || Main.item[num67].type == 548 || Main.item[num67].type == 549)
					{
						num70 = Main.essScale;
						alpha.R = (byte)((float)alpha.R * num70);
						alpha.G = (byte)((float)alpha.G * num70);
						alpha.B = (byte)((float)alpha.B * num70);
						alpha.A = (byte)((float)alpha.A * num70);
					}
					else
					{
						if (Main.item[num67].type == 58 || Main.item[num67].type == 184)
						{
							num70 = Main.essScale * 0.25f + 0.75f;
							alpha.R = (byte)((float)alpha.R * num70);
							alpha.G = (byte)((float)alpha.G * num70);
							alpha.B = (byte)((float)alpha.B * num70);
							alpha.A = (byte)((float)alpha.A * num70);
						}
					}
					float num71 = (float)(Main.item[num67].height - Main.itemTexture[Main.item[num67].type].Height);
					float num72 = (float)(Main.item[num67].width / 2 - Main.itemTexture[Main.item[num67].type].Width / 2);
					this.spriteBatch.Draw(Main.itemTexture[Main.item[num67].type], new Vector2(Main.item[num67].position.X - Main.screenPosition.X + (float)(Main.itemTexture[Main.item[num67].type].Width / 2) + num72, Main.item[num67].position.Y - Main.screenPosition.Y + (float)(Main.itemTexture[Main.item[num67].type].Height / 2) + num71 + 2f), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[Main.item[num67].type].Width, Main.itemTexture[Main.item[num67].type].Height)), alpha, rotation3, new Vector2((float)(Main.itemTexture[Main.item[num67].type].Width / 2), (float)(Main.itemTexture[Main.item[num67].type].Height / 2)), num70, SpriteEffects.None, 0f);
					if (Main.item[num67].color != default(Color))
					{
						this.spriteBatch.Draw(Main.itemTexture[Main.item[num67].type], new Vector2(Main.item[num67].position.X - Main.screenPosition.X + (float)(Main.itemTexture[Main.item[num67].type].Width / 2) + num72, Main.item[num67].position.Y - Main.screenPosition.Y + (float)(Main.itemTexture[Main.item[num67].type].Height / 2) + num71 + 2f), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[Main.item[num67].type].Width, Main.itemTexture[Main.item[num67].type].Height)), Main.item[num67].GetColor(color8), rotation3, new Vector2((float)(Main.itemTexture[Main.item[num67].type].Width / 2), (float)(Main.itemTexture[Main.item[num67].type].Height / 2)), num70, SpriteEffects.None, 0f);
					}
				}
			}
			Rectangle value2 = new Rectangle((int)Main.screenPosition.X - 500, (int)Main.screenPosition.Y - 50, Main.screenWidth + 1000, Main.screenHeight + 100);
			for (int num73 = 0; num73 < Main.numDust; num73++)
			{
				if (Main.dust[num73].active)
				{
					if (new Rectangle((int)Main.dust[num73].position.X, (int)Main.dust[num73].position.Y, 4, 4).Intersects(value2))
					{
						Color color9 = Lighting.GetColor((int)((double)Main.dust[num73].position.X + 4.0) / 16, (int)((double)Main.dust[num73].position.Y + 4.0) / 16);
						if (Main.dust[num73].type == 6 || Main.dust[num73].type == 15 || Main.dust[num73].noLight || (Main.dust[num73].type >= 59 && Main.dust[num73].type <= 64))
						{
							color9 = Color.White;
						}
						color9 = Main.dust[num73].GetAlpha(color9);
						this.spriteBatch.Draw(Main.dustTexture, Main.dust[num73].position - Main.screenPosition, new Rectangle?(Main.dust[num73].frame), color9, Main.dust[num73].rotation, new Vector2(4f, 4f), Main.dust[num73].scale, SpriteEffects.None, 0f);
						if (Main.dust[num73].color != default(Color))
						{
							this.spriteBatch.Draw(Main.dustTexture, Main.dust[num73].position - Main.screenPosition, new Rectangle?(Main.dust[num73].frame), Main.dust[num73].GetColor(color9), Main.dust[num73].rotation, new Vector2(4f, 4f), Main.dust[num73].scale, SpriteEffects.None, 0f);
						}
						if (color9 == Color.Black)
						{
							Main.dust[num73].active = false;
						}
					}
					else
					{
						Main.dust[num73].active = false;
					}
				}
			}
			if (Main.drawToScreen)
			{
				this.DrawWater(false);
				if (Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].mech)
				{
					this.DrawWires();
				}
			}
			else
			{
				this.spriteBatch.Draw(this.waterTarget, Main.sceneWaterPos - Main.screenPosition, Color.White);
			}
			if (!Main.hideUI)
			{
				for (int num74 = 0; num74 < 255; num74++)
				{
					if (Main.player[num74].active && Main.player[num74].chatShowTime > 0 && num74 != Main.myPlayer && !Main.player[num74].dead)
					{
						Vector2 vector = Main.fontMouseText.MeasureString(Main.player[num74].chatText);
						Vector2 vector2;
						vector2.X = Main.player[num74].position.X + (float)(Main.player[num74].width / 2) - vector.X / 2f;
						vector2.Y = Main.player[num74].position.Y - vector.Y - 2f;
						for (int num75 = 0; num75 < 5; num75++)
						{
							int num76 = 0;
							int num77 = 0;
							Color black = Color.Black;
							if (num75 == 0)
							{
								num76 = -2;
							}
							if (num75 == 1)
							{
								num76 = 2;
							}
							if (num75 == 2)
							{
								num77 = -2;
							}
							if (num75 == 3)
							{
								num77 = 2;
							}
							if (num75 == 4)
							{
								black = new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor);
							}
							this.spriteBatch.DrawString(Main.fontMouseText, Main.player[num74].chatText, new Vector2(vector2.X + (float)num76 - Main.screenPosition.X, vector2.Y + (float)num77 - Main.screenPosition.Y), black, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
						}
					}
				}
				for (int num78 = 0; num78 < 100; num78++)
				{
					if (Main.combatText[num78].active)
					{
						int num79 = 0;
						if (Main.combatText[num78].crit)
						{
							num79 = 1;
						}
						Vector2 vector3 = Main.fontCombatText[num79].MeasureString(Main.combatText[num78].text);
						Vector2 origin = new Vector2(vector3.X * 0.5f, vector3.Y * 0.5f);
						float arg_689B_0 = Main.combatText[num78].scale;
						float num80 = (float)Main.combatText[num78].color.R;
						float num81 = (float)Main.combatText[num78].color.G;
						float num82 = (float)Main.combatText[num78].color.B;
						float num83 = (float)Main.combatText[num78].color.A;
						num80 *= Main.combatText[num78].scale * Main.combatText[num78].alpha * 0.3f;
						num82 *= Main.combatText[num78].scale * Main.combatText[num78].alpha * 0.3f;
						num81 *= Main.combatText[num78].scale * Main.combatText[num78].alpha * 0.3f;
						num83 *= Main.combatText[num78].scale * Main.combatText[num78].alpha;
						Color color10 = new Color((int)num80, (int)num81, (int)num82, (int)num83);
						for (int num84 = 0; num84 < 5; num84++)
						{
							int num85 = 0;
							int num86 = 0;
							if (num84 == 0)
							{
								num85--;
							}
							else
							{
								if (num84 == 1)
								{
									num85++;
								}
								else
								{
									if (num84 == 2)
									{
										num86--;
									}
									else
									{
										if (num84 == 3)
										{
											num86++;
										}
										else
										{
											num80 = (float)Main.combatText[num78].color.R * Main.combatText[num78].scale * Main.combatText[num78].alpha;
											num82 = (float)Main.combatText[num78].color.B * Main.combatText[num78].scale * Main.combatText[num78].alpha;
											num81 = (float)Main.combatText[num78].color.G * Main.combatText[num78].scale * Main.combatText[num78].alpha;
											num83 = (float)Main.combatText[num78].color.A * Main.combatText[num78].scale * Main.combatText[num78].alpha;
											color10 = new Color((int)num80, (int)num81, (int)num82, (int)num83);
										}
									}
								}
							}
							this.spriteBatch.DrawString(Main.fontCombatText[num79], Main.combatText[num78].text, new Vector2(Main.combatText[num78].position.X - Main.screenPosition.X + (float)num85 + origin.X, Main.combatText[num78].position.Y - Main.screenPosition.Y + (float)num86 + origin.Y), color10, Main.combatText[num78].rotation, origin, Main.combatText[num78].scale, SpriteEffects.None, 0f);
						}
					}
				}
				for (int num87 = 0; num87 < 20; num87++)
				{
					if (Main.itemText[num87].active)
					{
						string text = Main.itemText[num87].name;
						if (Main.itemText[num87].stack > 1)
						{
							text = string.Concat(new object[]
							{
								text,
								" (",
								Main.itemText[num87].stack,
								")"
							});
						}
						Vector2 vector4 = Main.fontMouseText.MeasureString(text);
						Vector2 origin2 = new Vector2(vector4.X * 0.5f, vector4.Y * 0.5f);
						float arg_6C25_0 = Main.itemText[num87].scale;
						float num88 = (float)Main.itemText[num87].color.R;
						float num89 = (float)Main.itemText[num87].color.G;
						float num90 = (float)Main.itemText[num87].color.B;
						float num91 = (float)Main.itemText[num87].color.A;
						num88 *= Main.itemText[num87].scale * Main.itemText[num87].alpha * 0.3f;
						num90 *= Main.itemText[num87].scale * Main.itemText[num87].alpha * 0.3f;
						num89 *= Main.itemText[num87].scale * Main.itemText[num87].alpha * 0.3f;
						num91 *= Main.itemText[num87].scale * Main.itemText[num87].alpha;
						Color color11 = new Color((int)num88, (int)num89, (int)num90, (int)num91);
						for (int num92 = 0; num92 < 5; num92++)
						{
							int num93 = 0;
							int num94 = 0;
							if (num92 == 0)
							{
								num93 -= 2;
							}
							else
							{
								if (num92 == 1)
								{
									num93 += 2;
								}
								else
								{
									if (num92 == 2)
									{
										num94 -= 2;
									}
									else
									{
										if (num92 == 3)
										{
											num94 += 2;
										}
										else
										{
											num88 = (float)Main.itemText[num87].color.R * Main.itemText[num87].scale * Main.itemText[num87].alpha;
											num90 = (float)Main.itemText[num87].color.B * Main.itemText[num87].scale * Main.itemText[num87].alpha;
											num89 = (float)Main.itemText[num87].color.G * Main.itemText[num87].scale * Main.itemText[num87].alpha;
											num91 = (float)Main.itemText[num87].color.A * Main.itemText[num87].scale * Main.itemText[num87].alpha;
											color11 = new Color((int)num88, (int)num89, (int)num90, (int)num91);
										}
									}
								}
							}
							if (num92 < 4)
							{
								num91 = (float)Main.itemText[num87].color.A * Main.itemText[num87].scale * Main.itemText[num87].alpha;
								color11 = new Color(0, 0, 0, (int)num91);
							}
							this.spriteBatch.DrawString(Main.fontMouseText, text, new Vector2(Main.itemText[num87].position.X - Main.screenPosition.X + (float)num93 + origin2.X, Main.itemText[num87].position.Y - Main.screenPosition.Y + (float)num94 + origin2.Y), color11, Main.itemText[num87].rotation, origin2, Main.itemText[num87].scale, SpriteEffects.None, 0f);
						}
					}
				}
				if (Main.netMode == 1 && Netplay.clientSock.statusText != "" && Netplay.clientSock.statusText != null)
				{
					string text2 = string.Concat(new object[]
					{
						Netplay.clientSock.statusText,
						": ",
						(int)((float)Netplay.clientSock.statusCount / (float)Netplay.clientSock.statusMax * 100f),
						"%"
					});
					this.spriteBatch.DrawString(Main.fontMouseText, text2, new Vector2(628f - Main.fontMouseText.MeasureString(text2).X * 0.5f + (float)(Main.screenWidth - 800), 84f), new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
				}
				this.DrawFPS();
				this.DrawInterface();
			}
			else
			{
				Main.maxQ = true;
			}
			this.spriteBatch.End();
			if (Main.mouseLeft)
			{
				Main.mouseLeftRelease = false;
			}
			else
			{
				Main.mouseLeftRelease = true;
			}
			if (Main.mouseRight)
			{
				Main.mouseRightRelease = false;
			}
			else
			{
				Main.mouseRightRelease = true;
			}
			if (Main.mouseState.RightButton != ButtonState.Pressed)
			{
				Main.stackSplit = 0;
			}
			if (Main.stackSplit > 0)
			{
				Main.stackSplit--;
			}
			if (Main.renderCount < 10)
			{
				Main.drawTimer[Main.renderCount] = (float)stopwatch.ElapsedMilliseconds;
				if (Main.drawTimerMaxDelay[Main.renderCount] > 0f)
				{
					Main.drawTimerMaxDelay[Main.renderCount] -= 1f;
				}
				else
				{
					Main.drawTimerMax[Main.renderCount] = 0f;
				}
				if (Main.drawTimer[Main.renderCount] > Main.drawTimerMax[Main.renderCount])
				{
					Main.drawTimerMax[Main.renderCount] = Main.drawTimer[Main.renderCount];
					Main.drawTimerMaxDelay[Main.renderCount] = 100f;
				}
			}
		}
		private static void UpdateInvasion()
		{
			if (Main.invasionType > 0)
			{
				if (Main.invasionSize <= 0)
				{
					if (Main.invasionType == 1)
					{
						NPC.downedGoblins = true;
						if (Main.netMode == 2)
						{
							NetMessage.SendData(7, -1, -1, "", 0, 0f, 0f, 0f, 0);
						}
					}
					else
					{
						if (Main.invasionType == 2)
						{
							NPC.downedFrost = true;
						}
					}
					Main.InvasionWarning();
					Main.invasionType = 0;
					Main.invasionDelay = 7;
				}
				if (Main.invasionX == (double)Main.spawnTileX)
				{
					return;
				}
				float num = 1f;
				if (Main.invasionX > (double)Main.spawnTileX)
				{
					Main.invasionX -= (double)num;
					if (Main.invasionX <= (double)Main.spawnTileX)
					{
						Main.invasionX = (double)Main.spawnTileX;
						Main.InvasionWarning();
					}
					else
					{
						Main.invasionWarn--;
					}
				}
				else
				{
					if (Main.invasionX < (double)Main.spawnTileX)
					{
						Main.invasionX += (double)num;
						if (Main.invasionX >= (double)Main.spawnTileX)
						{
							Main.invasionX = (double)Main.spawnTileX;
							Main.InvasionWarning();
						}
						else
						{
							Main.invasionWarn--;
						}
					}
				}
				if (Main.invasionWarn <= 0)
				{
					Main.invasionWarn = 3600;
					Main.InvasionWarning();
				}
			}
		}
		private static void InvasionWarning()
		{
			string text;
			if (Main.invasionSize <= 0)
			{
				if (Main.invasionType == 2)
				{
					text = Lang.misc[4];
				}
				else
				{
					text = Lang.misc[0];
				}
			}
			else
			{
				if (Main.invasionX < (double)Main.spawnTileX)
				{
					if (Main.invasionType == 2)
					{
						text = Lang.misc[5];
					}
					else
					{
						text = Lang.misc[1];
					}
				}
				else
				{
					if (Main.invasionX > (double)Main.spawnTileX)
					{
						if (Main.invasionType == 2)
						{
							text = Lang.misc[6];
						}
						else
						{
							text = Lang.misc[2];
						}
					}
					else
					{
						if (Main.invasionType == 2)
						{
							text = Lang.misc[7];
						}
						else
						{
							text = Lang.misc[3];
						}
					}
				}
			}
			if (Main.netMode == 0)
			{
				Main.NewText(text, 175, 75, 255);
				return;
			}
			if (Main.netMode == 2)
			{
				NetMessage.SendData(25, -1, -1, text, 255, 175f, 75f, 255f, 0);
			}
		}
		public static void StartInvasion(int type = 1)
		{
			if (Main.invasionType == 0 && Main.invasionDelay == 0)
			{
				int num = 0;
				for (int i = 0; i < 255; i++)
				{
					if (Main.player[i].active && Main.player[i].statLifeMax >= 200)
					{
						num++;
					}
				}
				if (num > 0)
				{
					Main.invasionType = type;
					Main.invasionSize = 80 + 40 * num;
					Main.invasionWarn = 0;
					if (Main.rand.Next(2) == 0)
					{
						Main.invasionX = 0.0;
						return;
					}
					Main.invasionX = (double)Main.maxTilesX;
				}
			}
		}
		private static void UpdateClient()
		{
			if (Main.myPlayer == 255)
			{
				Netplay.disconnect = true;
			}
			Main.netPlayCounter++;
			if (Main.netPlayCounter > 3600)
			{
				Main.netPlayCounter = 0;
			}
			if (Math.IEEERemainder((double)Main.netPlayCounter, 300.0) == 0.0)
			{
				NetMessage.SendData(13, -1, -1, "", Main.myPlayer, 0f, 0f, 0f, 0);
				NetMessage.SendData(36, -1, -1, "", Main.myPlayer, 0f, 0f, 0f, 0);
			}
			if (Math.IEEERemainder((double)Main.netPlayCounter, 600.0) == 0.0)
			{
				NetMessage.SendData(16, -1, -1, "", Main.myPlayer, 0f, 0f, 0f, 0);
				NetMessage.SendData(40, -1, -1, "", Main.myPlayer, 0f, 0f, 0f, 0);
			}
			if (Netplay.clientSock.active)
			{
				Netplay.clientSock.timeOut++;
				if (!Main.stopTimeOuts && Netplay.clientSock.timeOut > 60 * Main.timeOut)
				{
					Main.statusText = Lang.inter[43];
					Netplay.disconnect = true;
				}
			}
			for (int i = 0; i < 200; i++)
			{
				if (Main.item[i].active && Main.item[i].owner == Main.myPlayer)
				{
					Main.item[i].FindOwner(i);
				}
			}
		}
		private static void UpdateServer()
		{
			Main.netPlayCounter++;
			if (Main.netPlayCounter > 3600)
			{
				NetMessage.SendData(7, -1, -1, "", 0, 0f, 0f, 0f, 0);
				NetMessage.syncPlayers();
				Main.netPlayCounter = 0;
			}
			for (int i = 0; i < Main.maxNetPlayers; i++)
			{
				if (Main.player[i].active && Netplay.serverSock[i].active)
				{
					Netplay.serverSock[i].SpamUpdate();
				}
			}
			if (Math.IEEERemainder((double)Main.netPlayCounter, 900.0) == 0.0)
			{
				bool flag = true;
				int num = Main.lastItemUpdate;
				int num2 = 0;
				while (flag)
				{
					num++;
					if (num >= 200)
					{
						num = 0;
					}
					num2++;
					if (!Main.item[num].active || Main.item[num].owner == 255)
					{
						NetMessage.SendData(21, -1, -1, "", num, 0f, 0f, 0f, 0);
					}
					if (num2 >= Main.maxItemUpdates || num == Main.lastItemUpdate)
					{
						flag = false;
					}
				}
				Main.lastItemUpdate = num;
			}
			for (int j = 0; j < 200; j++)
			{
				if (Main.item[j].active && (Main.item[j].owner == 255 || !Main.player[Main.item[j].owner].active))
				{
					Main.item[j].FindOwner(j);
				}
			}
			for (int k = 0; k < 255; k++)
			{
				if (Netplay.serverSock[k].active)
				{
					Netplay.serverSock[k].timeOut++;
					if (!Main.stopTimeOuts && Netplay.serverSock[k].timeOut > 60 * Main.timeOut)
					{
						Netplay.serverSock[k].kill = true;
					}
				}
				if (Main.player[k].active)
				{
					int sectionX = Netplay.GetSectionX((int)(Main.player[k].position.X / 16f));
					int sectionY = Netplay.GetSectionY((int)(Main.player[k].position.Y / 16f));
					int num3 = 0;
					for (int l = sectionX - 1; l < sectionX + 2; l++)
					{
						for (int m = sectionY - 1; m < sectionY + 2; m++)
						{
							if (l >= 0 && l < Main.maxSectionsX && m >= 0 && m < Main.maxSectionsY && !Netplay.serverSock[k].tileSection[l, m])
							{
								num3++;
							}
						}
					}
					if (num3 > 0)
					{
						int num4 = num3 * 150;
						NetMessage.SendData(9, k, -1, "Receiving tile data", num4, 0f, 0f, 0f, 0);
						Netplay.serverSock[k].statusText2 = "is receiving tile data";
						Netplay.serverSock[k].statusMax += num4;
						for (int n = sectionX - 1; n < sectionX + 2; n++)
						{
							for (int num5 = sectionY - 1; num5 < sectionY + 2; num5++)
							{
								if (n >= 0 && n < Main.maxSectionsX && num5 >= 0 && num5 < Main.maxSectionsY && !Netplay.serverSock[k].tileSection[n, num5])
								{
									NetMessage.SendSection(k, n, num5);
									NetMessage.SendData(11, k, -1, "", n, (float)num5, (float)n, (float)num5, 0);
								}
							}
						}
					}
				}
			}
		}
		public static void NewText(string newText, byte R = 255, byte G = 255, byte B = 255)
		{
			for (int i = Main.numChatLines - 1; i > 0; i--)
			{
				Main.chatLine[i].text = Main.chatLine[i - 1].text;
				Main.chatLine[i].showTime = Main.chatLine[i - 1].showTime;
				Main.chatLine[i].color = Main.chatLine[i - 1].color;
			}
			if (R == 0 && G == 0 && B == 0)
			{
				Main.chatLine[0].color = Color.White;
			}
			else
			{
				Main.chatLine[0].color = new Color((int)R, (int)G, (int)B);
			}
			Main.chatLine[0].text = newText;
			Main.chatLine[0].showTime = Main.chatLength;
			Main.PlaySound(12, -1, -1, 1);
		}
		private static void UpdateTime()
		{
			Main.time += (double)Main.dayRate;
			if (!Main.dayTime)
			{
				if (WorldGen.spawnEye && Main.netMode != 1 && Main.time > 4860.0)
				{
					for (int i = 0; i < 255; i++)
					{
						if (Main.player[i].active && !Main.player[i].dead && (double)Main.player[i].position.Y < Main.worldSurface * 16.0)
						{
							NPC.SpawnOnPlayer(i, 4);
							WorldGen.spawnEye = false;
							break;
						}
					}
				}
				if (Main.time > 32400.0)
				{
					Main.checkXMas();
					if (Main.invasionDelay > 0)
					{
						Main.invasionDelay--;
					}
					WorldGen.spawnNPC = 0;
					Main.checkForSpawns = 0;
					Main.time = 0.0;
					Main.bloodMoon = false;
					Main.dayTime = true;
					Main.moonPhase++;
					if (Main.moonPhase >= 8)
					{
						Main.moonPhase = 0;
					}
					if (Main.netMode == 2)
					{
						NetMessage.SendData(7, -1, -1, "", 0, 0f, 0f, 0f, 0);
						WorldGen.saveAndPlay();
					}
					if (Main.netMode != 1 && WorldGen.shadowOrbSmashed)
					{
						if (!NPC.downedGoblins)
						{
							if (Main.rand.Next(3) == 0)
							{
								Main.StartInvasion(1);
							}
						}
						else
						{
							if (Main.rand.Next(15) == 0)
							{
								Main.StartInvasion(1);
							}
						}
					}
				}
				if (Main.time > 16200.0 && WorldGen.spawnMeteor)
				{
					WorldGen.spawnMeteor = false;
					WorldGen.dropMeteor();
					return;
				}
			}
			else
			{
				Main.bloodMoon = false;
				if (Main.time > 54000.0)
				{
					WorldGen.spawnNPC = 0;
					Main.checkForSpawns = 0;
					if (Main.rand.Next(50) == 0 && Main.netMode != 1 && WorldGen.shadowOrbSmashed)
					{
						WorldGen.spawnMeteor = true;
					}
					if (!NPC.downedBoss1 && Main.netMode != 1)
					{
						bool flag = false;
						for (int j = 0; j < 255; j++)
						{
							if (Main.player[j].active && Main.player[j].statLifeMax >= 200 && Main.player[j].statDefense > 10)
							{
								flag = true;
								break;
							}
						}
						if (flag && Main.rand.Next(3) == 0)
						{
							int num = 0;
							for (int k = 0; k < 200; k++)
							{
								if (Main.npc[k].active && Main.npc[k].townNPC)
								{
									num++;
								}
							}
							if (num >= 4)
							{
								WorldGen.spawnEye = true;
								if (Main.netMode == 0)
								{
									Main.NewText(Lang.misc[9], 50, 255, 130);
								}
								else
								{
									if (Main.netMode == 2)
									{
										NetMessage.SendData(25, -1, -1, Lang.misc[9], 255, 50f, 255f, 130f, 0);
									}
								}
							}
						}
					}
					if (!WorldGen.spawnEye && Main.moonPhase != 4 && Main.rand.Next(9) == 0 && Main.netMode != 1)
					{
						for (int l = 0; l < 255; l++)
						{
							if (Main.player[l].active && Main.player[l].statLifeMax > 120)
							{
								Main.bloodMoon = true;
								break;
							}
						}
						if (Main.bloodMoon)
						{
							if (Main.netMode == 0)
							{
								Main.NewText(Lang.misc[8], 50, 255, 130);
							}
							else
							{
								if (Main.netMode == 2)
								{
									NetMessage.SendData(25, -1, -1, Lang.misc[8], 255, 50f, 255f, 130f, 0);
								}
							}
						}
					}
					Main.time = 0.0;
					Main.dayTime = false;
					if (Main.netMode == 2)
					{
						NetMessage.SendData(7, -1, -1, "", 0, 0f, 0f, 0f, 0);
					}
				}
				if (Main.netMode != 1)
				{
					Main.checkForSpawns++;
					if (Main.checkForSpawns >= 7200)
					{
						int num2 = 0;
						for (int m = 0; m < 255; m++)
						{
							if (Main.player[m].active)
							{
								num2++;
							}
						}
						Main.checkForSpawns = 0;
						WorldGen.spawnNPC = 0;
						int num3 = 0;
						int num4 = 0;
						int num5 = 0;
						int num6 = 0;
						int num7 = 0;
						int num8 = 0;
						int num9 = 0;
						int num10 = 0;
						int num11 = 0;
						int num12 = 0;
						int num13 = 0;
						int num14 = 0;
						int num15 = 0;
						for (int n = 0; n < 200; n++)
						{
							if (Main.npc[n].active && Main.npc[n].townNPC)
							{
								if (Main.npc[n].type != 37 && !Main.npc[n].homeless)
								{
									WorldGen.QuickFindHome(n);
								}
								if (Main.npc[n].type == 37)
								{
									num8++;
								}
								if (Main.npc[n].type == 17)
								{
									num3++;
								}
								if (Main.npc[n].type == 18)
								{
									num4++;
								}
								if (Main.npc[n].type == 19)
								{
									num6++;
								}
								if (Main.npc[n].type == 20)
								{
									num5++;
								}
								if (Main.npc[n].type == 22)
								{
									num7++;
								}
								if (Main.npc[n].type == 38)
								{
									num9++;
								}
								if (Main.npc[n].type == 54)
								{
									num10++;
								}
								if (Main.npc[n].type == 107)
								{
									num12++;
								}
								if (Main.npc[n].type == 108)
								{
									num11++;
								}
								if (Main.npc[n].type == 124)
								{
									num13++;
								}
								if (Main.npc[n].type == 142)
								{
									num14++;
								}
								num15++;
							}
						}
						if (WorldGen.spawnNPC == 0)
						{
							int num16 = 0;
							bool flag2 = false;
							int num17 = 0;
							bool flag3 = false;
							bool flag4 = false;
							for (int num18 = 0; num18 < 255; num18++)
							{
								if (Main.player[num18].active)
								{
									for (int num19 = 0; num19 < 48; num19++)
									{
										if (Main.player[num18].inventory[num19] != null & Main.player[num18].inventory[num19].stack > 0)
										{
											if (Main.player[num18].inventory[num19].type == 71)
											{
												num16 += Main.player[num18].inventory[num19].stack;
											}
											if (Main.player[num18].inventory[num19].type == 72)
											{
												num16 += Main.player[num18].inventory[num19].stack * 100;
											}
											if (Main.player[num18].inventory[num19].type == 73)
											{
												num16 += Main.player[num18].inventory[num19].stack * 10000;
											}
											if (Main.player[num18].inventory[num19].type == 74)
											{
												num16 += Main.player[num18].inventory[num19].stack * 1000000;
											}
											if (Main.player[num18].inventory[num19].ammo == 14 || Main.player[num18].inventory[num19].useAmmo == 14)
											{
												flag3 = true;
											}
											if (Main.player[num18].inventory[num19].type == 166 || Main.player[num18].inventory[num19].type == 167 || Main.player[num18].inventory[num19].type == 168 || Main.player[num18].inventory[num19].type == 235)
											{
												flag4 = true;
											}
										}
									}
									int num20 = Main.player[num18].statLifeMax / 20;
									if (num20 > 5)
									{
										flag2 = true;
									}
									num17 += num20;
								}
							}
							if (!NPC.downedBoss3 && num8 == 0)
							{
								int num21 = NPC.NewNPC(Main.dungeonX * 16 + 8, Main.dungeonY * 16, 37, 0);
								Main.npc[num21].homeless = false;
								Main.npc[num21].homeTileX = Main.dungeonX;
								Main.npc[num21].homeTileY = Main.dungeonY;
							}
							if (WorldGen.spawnNPC == 0 && num7 < 1)
							{
								WorldGen.spawnNPC = 22;
							}
							if (WorldGen.spawnNPC == 0 && (double)num16 > 5000.0 && num3 < 1)
							{
								WorldGen.spawnNPC = 17;
							}
							if (WorldGen.spawnNPC == 0 && flag2 && num4 < 1)
							{
								WorldGen.spawnNPC = 18;
							}
							if (WorldGen.spawnNPC == 0 && flag3 && num6 < 1)
							{
								WorldGen.spawnNPC = 19;
							}
							if (WorldGen.spawnNPC == 0 && (NPC.downedBoss1 || NPC.downedBoss2 || NPC.downedBoss3) && num5 < 1)
							{
								WorldGen.spawnNPC = 20;
							}
							if (WorldGen.spawnNPC == 0 && flag4 && num3 > 0 && num9 < 1)
							{
								WorldGen.spawnNPC = 38;
							}
							if (WorldGen.spawnNPC == 0 && NPC.downedBoss3 && num10 < 1)
							{
								WorldGen.spawnNPC = 54;
							}
							if (WorldGen.spawnNPC == 0 && NPC.savedGoblin && num12 < 1)
							{
								WorldGen.spawnNPC = 107;
							}
							if (WorldGen.spawnNPC == 0 && NPC.savedWizard && num11 < 1)
							{
								WorldGen.spawnNPC = 108;
							}
							if (WorldGen.spawnNPC == 0 && NPC.savedMech && num13 < 1)
							{
								WorldGen.spawnNPC = 124;
							}
							if (WorldGen.spawnNPC == 0 && NPC.downedFrost && num14 < 1 && Main.xMas)
							{
								WorldGen.spawnNPC = 142;
							}
						}
					}
				}
			}
		}
		public static int DamageVar(float dmg)
		{
			float num = dmg * (1f + (float)Main.rand.Next(-15, 16) * 0.01f);
			return (int)Math.Round((double)num);
		}
		public static double CalculateDamage(int Damage, int Defense)
		{
			double num = (double)Damage - (double)Defense * 0.5;
			if (num < 1.0)
			{
				num = 1.0;
			}
			return num;
		}
		public static void PlaySound(int type, int x = -1, int y = -1, int Style = 1)
		{
			int num = Style;
			try
			{
				if (!Main.dedServ)
				{
					if (Main.soundVolume != 0f)
					{
						bool flag = false;
						float num2 = 1f;
						float num3 = 0f;
						if (x == -1 || y == -1)
						{
							flag = true;
						}
						else
						{
							if (WorldGen.gen)
							{
								return;
							}
							if (Main.netMode == 2)
							{
								return;
							}
							Rectangle value = new Rectangle((int)(Main.screenPosition.X - (float)(Main.screenWidth * 2)), (int)(Main.screenPosition.Y - (float)(Main.screenHeight * 2)), Main.screenWidth * 5, Main.screenHeight * 5);
							Rectangle rectangle = new Rectangle(x, y, 1, 1);
							Vector2 vector = new Vector2(Main.screenPosition.X + (float)Main.screenWidth * 0.5f, Main.screenPosition.Y + (float)Main.screenHeight * 0.5f);
							if (rectangle.Intersects(value))
							{
								flag = true;
							}
							if (flag)
							{
								num3 = ((float)x - vector.X) / ((float)Main.screenWidth * 0.5f);
								float num4 = Math.Abs((float)x - vector.X);
								float num5 = Math.Abs((float)y - vector.Y);
								float num6 = (float)Math.Sqrt((double)(num4 * num4 + num5 * num5));
								num2 = 1f - num6 / ((float)Main.screenWidth * 1.5f);
							}
						}
						if (num3 < -1f)
						{
							num3 = -1f;
						}
						if (num3 > 1f)
						{
							num3 = 1f;
						}
						if (num2 > 1f)
						{
							num2 = 1f;
						}
						if (num2 > 0f)
						{
							if (flag)
							{
								num2 *= Main.soundVolume;
								if (type == 0)
								{
									int num7 = Main.rand.Next(3);
									Main.soundInstanceDig[num7].Stop();
									Main.soundInstanceDig[num7] = Main.soundDig[num7].CreateInstance();
									Main.soundInstanceDig[num7].Volume = num2;
									Main.soundInstanceDig[num7].Pan = num3;
									Main.soundInstanceDig[num7].Pitch = (float)Main.rand.Next(-10, 11) * 0.01f;
									Main.soundInstanceDig[num7].Play();
								}
								else
								{
									if (type == 1)
									{
										int num8 = Main.rand.Next(3);
										Main.soundInstancePlayerHit[num8].Stop();
										Main.soundInstancePlayerHit[num8] = Main.soundPlayerHit[num8].CreateInstance();
										Main.soundInstancePlayerHit[num8].Volume = num2;
										Main.soundInstancePlayerHit[num8].Pan = num3;
										Main.soundInstancePlayerHit[num8].Play();
									}
									else
									{
										if (type == 2)
										{
											if (num == 1)
											{
												int num9 = Main.rand.Next(3);
												if (num9 == 1)
												{
													num = 18;
												}
												if (num9 == 2)
												{
													num = 19;
												}
											}
											if (num != 9 && num != 10 && num != 24 && num != 26 && num != 34)
											{
												Main.soundInstanceItem[num].Stop();
											}
											Main.soundInstanceItem[num] = Main.soundItem[num].CreateInstance();
											Main.soundInstanceItem[num].Volume = num2;
											Main.soundInstanceItem[num].Pan = num3;
											Main.soundInstanceItem[num].Pitch = (float)Main.rand.Next(-6, 7) * 0.01f;
											if (num == 26 || num == 35)
											{
												Main.soundInstanceItem[num].Volume = num2 * 0.75f;
												Main.soundInstanceItem[num].Pitch = Main.harpNote;
											}
											Main.soundInstanceItem[num].Play();
										}
										else
										{
											if (type == 3)
											{
												Main.soundInstanceNPCHit[num].Stop();
												Main.soundInstanceNPCHit[num] = Main.soundNPCHit[num].CreateInstance();
												Main.soundInstanceNPCHit[num].Volume = num2;
												Main.soundInstanceNPCHit[num].Pan = num3;
												Main.soundInstanceNPCHit[num].Pitch = (float)Main.rand.Next(-10, 11) * 0.01f;
												Main.soundInstanceNPCHit[num].Play();
											}
											else
											{
												if (type == 4)
												{
													if (num != 10 || Main.soundInstanceNPCKilled[num].State != SoundState.Playing)
													{
														Main.soundInstanceNPCKilled[num] = Main.soundNPCKilled[num].CreateInstance();
														Main.soundInstanceNPCKilled[num].Volume = num2;
														Main.soundInstanceNPCKilled[num].Pan = num3;
														Main.soundInstanceNPCKilled[num].Pitch = (float)Main.rand.Next(-10, 11) * 0.01f;
														Main.soundInstanceNPCKilled[num].Play();
													}
												}
												else
												{
													if (type == 5)
													{
														Main.soundInstancePlayerKilled.Stop();
														Main.soundInstancePlayerKilled = Main.soundPlayerKilled.CreateInstance();
														Main.soundInstancePlayerKilled.Volume = num2;
														Main.soundInstancePlayerKilled.Pan = num3;
														Main.soundInstancePlayerKilled.Play();
													}
													else
													{
														if (type == 6)
														{
															Main.soundInstanceGrass.Stop();
															Main.soundInstanceGrass = Main.soundGrass.CreateInstance();
															Main.soundInstanceGrass.Volume = num2;
															Main.soundInstanceGrass.Pan = num3;
															Main.soundInstanceGrass.Pitch = (float)Main.rand.Next(-30, 31) * 0.01f;
															Main.soundInstanceGrass.Play();
														}
														else
														{
															if (type == 7)
															{
																Main.soundInstanceGrab.Stop();
																Main.soundInstanceGrab = Main.soundGrab.CreateInstance();
																Main.soundInstanceGrab.Volume = num2;
																Main.soundInstanceGrab.Pan = num3;
																Main.soundInstanceGrab.Pitch = (float)Main.rand.Next(-10, 11) * 0.01f;
																Main.soundInstanceGrab.Play();
															}
															else
															{
																if (type == 8)
																{
																	Main.soundInstanceDoorOpen.Stop();
																	Main.soundInstanceDoorOpen = Main.soundDoorOpen.CreateInstance();
																	Main.soundInstanceDoorOpen.Volume = num2;
																	Main.soundInstanceDoorOpen.Pan = num3;
																	Main.soundInstanceDoorOpen.Pitch = (float)Main.rand.Next(-20, 21) * 0.01f;
																	Main.soundInstanceDoorOpen.Play();
																}
																else
																{
																	if (type == 9)
																	{
																		Main.soundInstanceDoorClosed.Stop();
																		Main.soundInstanceDoorClosed = Main.soundDoorClosed.CreateInstance();
																		Main.soundInstanceDoorClosed.Volume = num2;
																		Main.soundInstanceDoorClosed.Pan = num3;
																		Main.soundInstanceDoorOpen.Pitch = (float)Main.rand.Next(-20, 21) * 0.01f;
																		Main.soundInstanceDoorClosed.Play();
																	}
																	else
																	{
																		if (type == 10)
																		{
																			Main.soundInstanceMenuOpen.Stop();
																			Main.soundInstanceMenuOpen = Main.soundMenuOpen.CreateInstance();
																			Main.soundInstanceMenuOpen.Volume = num2;
																			Main.soundInstanceMenuOpen.Pan = num3;
																			Main.soundInstanceMenuOpen.Play();
																		}
																		else
																		{
																			if (type == 11)
																			{
																				Main.soundInstanceMenuClose.Stop();
																				Main.soundInstanceMenuClose = Main.soundMenuClose.CreateInstance();
																				Main.soundInstanceMenuClose.Volume = num2;
																				Main.soundInstanceMenuClose.Pan = num3;
																				Main.soundInstanceMenuClose.Play();
																			}
																			else
																			{
																				if (type == 12)
																				{
																					Main.soundInstanceMenuTick.Stop();
																					Main.soundInstanceMenuTick = Main.soundMenuTick.CreateInstance();
																					Main.soundInstanceMenuTick.Volume = num2;
																					Main.soundInstanceMenuTick.Pan = num3;
																					Main.soundInstanceMenuTick.Play();
																				}
																				else
																				{
																					if (type == 13)
																					{
																						Main.soundInstanceShatter.Stop();
																						Main.soundInstanceShatter = Main.soundShatter.CreateInstance();
																						Main.soundInstanceShatter.Volume = num2;
																						Main.soundInstanceShatter.Pan = num3;
																						Main.soundInstanceShatter.Play();
																					}
																					else
																					{
																						if (type == 14)
																						{
																							int num10 = Main.rand.Next(3);
																							Main.soundInstanceZombie[num10] = Main.soundZombie[num10].CreateInstance();
																							Main.soundInstanceZombie[num10].Volume = num2 * 0.4f;
																							Main.soundInstanceZombie[num10].Pan = num3;
																							Main.soundInstanceZombie[num10].Play();
																						}
																						else
																						{
																							if (type == 15)
																							{
																								if (Main.soundInstanceRoar[num].State == SoundState.Stopped)
																								{
																									Main.soundInstanceRoar[num] = Main.soundRoar[num].CreateInstance();
																									Main.soundInstanceRoar[num].Volume = num2;
																									Main.soundInstanceRoar[num].Pan = num3;
																									Main.soundInstanceRoar[num].Play();
																								}
																							}
																							else
																							{
																								if (type == 16)
																								{
																									Main.soundInstanceDoubleJump.Stop();
																									Main.soundInstanceDoubleJump = Main.soundDoubleJump.CreateInstance();
																									Main.soundInstanceDoubleJump.Volume = num2;
																									Main.soundInstanceDoubleJump.Pan = num3;
																									Main.soundInstanceDoubleJump.Pitch = (float)Main.rand.Next(-10, 11) * 0.01f;
																									Main.soundInstanceDoubleJump.Play();
																								}
																								else
																								{
																									if (type == 17)
																									{
																										Main.soundInstanceRun.Stop();
																										Main.soundInstanceRun = Main.soundRun.CreateInstance();
																										Main.soundInstanceRun.Volume = num2;
																										Main.soundInstanceRun.Pan = num3;
																										Main.soundInstanceRun.Pitch = (float)Main.rand.Next(-10, 11) * 0.01f;
																										Main.soundInstanceRun.Play();
																									}
																									else
																									{
																										if (type == 18)
																										{
																											Main.soundInstanceCoins = Main.soundCoins.CreateInstance();
																											Main.soundInstanceCoins.Volume = num2;
																											Main.soundInstanceCoins.Pan = num3;
																											Main.soundInstanceCoins.Play();
																										}
																										else
																										{
																											if (type == 19)
																											{
																												if (Main.soundInstanceSplash[num].State == SoundState.Stopped)
																												{
																													Main.soundInstanceSplash[num] = Main.soundSplash[num].CreateInstance();
																													Main.soundInstanceSplash[num].Volume = num2;
																													Main.soundInstanceSplash[num].Pan = num3;
																													Main.soundInstanceSplash[num].Pitch = (float)Main.rand.Next(-10, 11) * 0.01f;
																													Main.soundInstanceSplash[num].Play();
																												}
																											}
																											else
																											{
																												if (type == 20)
																												{
																													int num11 = Main.rand.Next(3);
																													Main.soundInstanceFemaleHit[num11].Stop();
																													Main.soundInstanceFemaleHit[num11] = Main.soundFemaleHit[num11].CreateInstance();
																													Main.soundInstanceFemaleHit[num11].Volume = num2;
																													Main.soundInstanceFemaleHit[num11].Pan = num3;
																													Main.soundInstanceFemaleHit[num11].Play();
																												}
																												else
																												{
																													if (type == 21)
																													{
																														int num12 = Main.rand.Next(3);
																														Main.soundInstanceTink[num12].Stop();
																														Main.soundInstanceTink[num12] = Main.soundTink[num12].CreateInstance();
																														Main.soundInstanceTink[num12].Volume = num2;
																														Main.soundInstanceTink[num12].Pan = num3;
																														Main.soundInstanceTink[num12].Play();
																													}
																													else
																													{
																														if (type == 22)
																														{
																															Main.soundInstanceUnlock.Stop();
																															Main.soundInstanceUnlock = Main.soundUnlock.CreateInstance();
																															Main.soundInstanceUnlock.Volume = num2;
																															Main.soundInstanceUnlock.Pan = num3;
																															Main.soundInstanceUnlock.Play();
																														}
																														else
																														{
																															if (type == 23)
																															{
																																Main.soundInstanceDrown.Stop();
																																Main.soundInstanceDrown = Main.soundDrown.CreateInstance();
																																Main.soundInstanceDrown.Volume = num2;
																																Main.soundInstanceDrown.Pan = num3;
																																Main.soundInstanceDrown.Play();
																															}
																															else
																															{
																																if (type == 24)
																																{
																																	Main.soundInstanceChat = Main.soundChat.CreateInstance();
																																	Main.soundInstanceChat.Volume = num2;
																																	Main.soundInstanceChat.Pan = num3;
																																	Main.soundInstanceChat.Play();
																																}
																																else
																																{
																																	if (type == 25)
																																	{
																																		Main.soundInstanceMaxMana = Main.soundMaxMana.CreateInstance();
																																		Main.soundInstanceMaxMana.Volume = num2;
																																		Main.soundInstanceMaxMana.Pan = num3;
																																		Main.soundInstanceMaxMana.Play();
																																	}
																																	else
																																	{
																																		if (type == 26)
																																		{
																																			int num13 = Main.rand.Next(3, 5);
																																			Main.soundInstanceZombie[num13] = Main.soundZombie[num13].CreateInstance();
																																			Main.soundInstanceZombie[num13].Volume = num2 * 0.9f;
																																			Main.soundInstanceZombie[num13].Pan = num3;
																																			Main.soundInstanceSplash[num].Pitch = (float)Main.rand.Next(-10, 11) * 0.01f;
																																			Main.soundInstanceZombie[num13].Play();
																																		}
																																		else
																																		{
																																			if (type == 27)
																																			{
																																				if (Main.soundInstancePixie.State == SoundState.Playing)
																																				{
																																					Main.soundInstancePixie.Volume = num2;
																																					Main.soundInstancePixie.Pan = num3;
																																					Main.soundInstancePixie.Pitch = (float)Main.rand.Next(-10, 11) * 0.01f;
																																				}
																																				else
																																				{
																																					Main.soundInstancePixie.Stop();
																																					Main.soundInstancePixie = Main.soundPixie.CreateInstance();
																																					Main.soundInstancePixie.Volume = num2;
																																					Main.soundInstancePixie.Pan = num3;
																																					Main.soundInstancePixie.Pitch = (float)Main.rand.Next(-10, 11) * 0.01f;
																																					Main.soundInstancePixie.Play();
																																				}
																																			}
																																			else
																																			{
																																				if (type == 28)
																																				{
																																					if (Main.soundInstanceMech[num].State != SoundState.Playing)
																																					{
																																						Main.soundInstanceMech[num] = Main.soundMech[num].CreateInstance();
																																						Main.soundInstanceMech[num].Volume = num2;
																																						Main.soundInstanceMech[num].Pan = num3;
																																						Main.soundInstanceMech[num].Pitch = (float)Main.rand.Next(-10, 11) * 0.01f;
																																						Main.soundInstanceMech[num].Play();
																																					}
																																				}
																																			}
																																		}
																																	}
																																}
																															}
																														}
																													}
																												}
																											}
																										}
																									}
																								}
																							}
																						}
																					}
																				}
																			}
																		}
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			catch
			{
			}
		}
	}
}
