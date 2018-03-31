using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : Singleton<CameraManager> {

    Camera _cameras;

    Vector3 _defaultCameraPos;
    float _defaultCameraSize;

    GameObject[] _players;
    GameObject _rightPlayer;
    GameObject _leftPlayer;
    GameObject _topPlayer;
    GameObject _bottomPlayer;

    float _movemntSmooth = 4;

    float _cameraOffset = 2;
    float _cameraZ = -10;

    float _shakeDuration = 1f;
    float _lastShake = 0;
    float _shakeAmplitude = 0.7f;
    float _shakeSpeedMultiplicator = 100f;

    bool _shake = true;


    // Use this for initialization
    void Start () {
        _cameras = this.GetComponent<Camera>();
        _defaultCameraPos = this.transform.position;
        _defaultCameraSize = _cameras.orthographicSize;
	}

    void ResetCameraPos()
    {
        this.transform.position = Vector3.Lerp(this.transform.position, _defaultCameraPos, Time.deltaTime * _movemntSmooth);
        _cameras.orthographicSize = Mathf.Lerp(_cameras.orthographicSize, _defaultCameraSize, Time.deltaTime * _movemntSmooth);
    }
	
	// Update is called once per frame
	void Update () {

	    _players = GameObject.FindGameObjectsWithTag(Constants.PLAYERS_TAG);
        if(GameManager.Instance.gameStatus == GameManager.GameState.running && PlayerManager.Instance.players.Length > 1)
        {
            ComputeCameraMovement();
        }
        else if(GameManager.Instance.gameStatus == GameManager.GameState.finished) {
            ResetCameraPos();
        }
    }
    void ComputeCameraMovement()
    {
        _rightPlayer = _players[1];
        _leftPlayer = _players[1];
        _topPlayer = _players[1];
        _bottomPlayer = _players[1];

        foreach (GameObject player in _players)
        {
            if (_rightPlayer.transform.position.x <= player.transform.position.x)
            {
                _rightPlayer = player;
            }
            else if (_leftPlayer.transform.position.x >= player.transform.position.x)
            {
                _leftPlayer = player;
            }

            if (_bottomPlayer.transform.position.y >= player.transform.position.y)
            {
                _bottomPlayer = player;
            }
            else if (_topPlayer.transform.position.y <= player.transform.position.y)
            {
                _topPlayer = player;
            }
        }
        /* 
            Pour calculer la distance que la caméra doit prendre (verticale et horizontale) , on doit calculer
            la distance horizontal des deux joueurs, et la distance verticale des deux joueurs.
            CàD la distance entre (joueur1.x , 0) & (joueur2.x, 0) , (joueur1.y,0) & (joueur2.y, 0)
            Cela reviens a calculer : |joueur1.x  - joueur2.x| , |joueur1.y - joueur2.y|
            On adapte la caméra en fonction de la distance la plus grande
            */
        float distance = Math.Abs(_rightPlayer.transform.position.x - _leftPlayer.transform.position.x);
        float distanceHeight = Math.Abs(_topPlayer.transform.position.y - _bottomPlayer.transform.position.y);

        float cameraSize = 0;
        if (ConvertWidthPixelToUnite(distance) > ConvertHeightPixelToUnite(distanceHeight))
        {
            cameraSize = ConvertWidthPixelToUnite(distance);
        }
        else
        {
            cameraSize = ConvertHeightPixelToUnite(distanceHeight);
        }
        _cameras.orthographicSize = Mathf.Lerp(_cameras.orthographicSize, cameraSize, Time.deltaTime * _movemntSmooth);

        Vector3 middlePosition = ComputeMiddlePosition(_rightPlayer.transform, _leftPlayer.transform, _topPlayer.transform, _bottomPlayer.transform);

        this.transform.position = Vector3.Lerp(this.transform.position,middlePosition,Time.deltaTime * _movemntSmooth);
    }
    Vector3 ComputeMiddlePosition(Transform right, Transform left, Transform top, Transform bottom)
    {
        float middlePlayerX = (right.transform.position.x + left.transform.position.x) / 2;
        float middlePlayerY = (top.transform.position.y + bottom.transform.position.y) / 2;

        return new Vector3(middlePlayerX, middlePlayerY, _cameraZ);
    }
    float ConvertWidthPixelToUnite(float distance)
    {
        float toUnite = distance / (this._cameras.aspect * 2);
        toUnite += _cameraOffset;

        if (toUnite < 5)
        {
            return 5;
        }
        else
        {
            return toUnite;
        }
    }

    float ConvertHeightPixelToUnite(float distance)
    {
        float toUnite;
        toUnite = distance / 2;
        toUnite += _cameraOffset;
        if (toUnite < 5)
        {
            return 5;
        }
        else
        {
            return toUnite;
        }
    }

    public void Shake()
    {
        _lastShake = Time.time;
        StartCoroutine(ShakeCoroutine());
    }

    IEnumerator ShakeCoroutine()
    {
        while(Time.time < _shakeDuration + _lastShake)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, this.transform.position + UnityEngine.Random.insideUnitSphere * _shakeAmplitude, Time.deltaTime*_shakeSpeedMultiplicator);
            yield return null;
        }
    }
}
