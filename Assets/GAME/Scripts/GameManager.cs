using UnityEngine;
using System;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    float deltaTime = 0.0f;


    [SerializeField] Transform player;
    [SerializeField] PlayerData _playerData;


    [Header("-----UIManager-----")]
    [SerializeField] Text textScore;
    [SerializeField] Text textFPS;
    [SerializeField] Text textMessage;

    public int Score => _playerData.currentScore;

    public int HightScore => _playerData.hightScore;

    private void OnEnable()
    {
        ObserverManager.AddListener(ObserverKey.addScore,AddScore);
    }
    private void OnDisable()
    {
        ObserverManager.RemoveListener(ObserverKey.addScore, AddScore);
    }

    private void Start()
    {
        LoadDataPlayer();
        player.position = this._playerData.positionPlayer;
    }

    private void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        this._playerData.positionPlayer = player.position;
    }
    public void AddScore(params object[] datas)
    {
        this._playerData.currentScore += (int)datas[0];
        this.textScore.text = "Score: " + this._playerData.currentScore.ToString();
        if (this._playerData.currentScore > this._playerData.hightScore)
        {
            this._playerData.hightScore = this._playerData.currentScore;

        }
        SaveDataPlayer();
    }

    public void AddEnemyDied()
    {
        this._playerData.numberEnemyDie++;
    }

    private void OnDestroy()
    {
        SaveDataPlayer() ;
    }

    public void LoadDataPlayer()
    {
        string dataLoader =  PlayerPrefs.GetString(typeof(PlayerData).ToString(),"{}");
        if (dataLoader != null)
        {
            this._playerData = JsonUtility.FromJson<PlayerData>(dataLoader);
        }
        this._playerData.currentScore = 0;
    }

    public void SaveDataPlayer()
    {
        PlayerPrefs.SetString(typeof(PlayerData).ToString(), JsonUtility.ToJson(this._playerData));
    }

    public void ResetDataPlayer()
    {
        PlayerData playerData = new PlayerData();
    }

    /////////////////////////////////////// Hien thi FPS//////////////////////////////////////
    void OnGUI()
    {
        float fps = 1.0f / deltaTime;
        string text = string.Format("FPS: {0:0.}", fps);
        this.textFPS.text = text;
    }
}
[System.Serializable]
public class PlayerData
{
    public string playerName;
    public int hightScore;
    public int currentScore;
    public int numberEnemyDie;
    public Vector3 positionPlayer;

    public PlayerData()
    {
        this.playerName = "JoJo";
        this.currentScore = 0;
        this.numberEnemyDie = 0;
        this.positionPlayer = new Vector3(-9f,-0.5f,0f);
    }

    public PlayerData(string playerName, int currentScore, int numberEnemyDie, Vector3 pos)
    {
        this.playerName = playerName;
        this.currentScore = currentScore;
        this.numberEnemyDie = numberEnemyDie;
        this.positionPlayer = pos;
    }
}
