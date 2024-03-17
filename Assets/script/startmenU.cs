using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YG;

public class startmenU : MonoBehaviour
{
    public TMP_Text easyrecord;
    public TMP_Text normalrecord;
    public TMP_Text hardrecord;
    // Подписываемся на событие GetDataEvent в OnEnable
    private void OnEnable() => YandexGame.GetDataEvent += GetLoad;

    // Отписываемся от события GetDataEvent в OnDisable
    private void OnDisable() => YandexGame.GetDataEvent -= GetLoad;

    private void Start()
    {
        // Проверяем запустился ли плагин
        if (YandexGame.SDKEnabled == true)
        {
            // Если запустился, то выполняем Ваш метод для загрузки
            GetLoad();

            // Если плагин еще не прогрузился, то метод не выполнится в методе Start,
            // но он запустится при вызове события GetDataEvent, после прогрузки плагина
        }
    }

    // Ваш метод для загрузки, который будет запускаться в старте
    public void GetLoad()
    {
        easyrecord.text = YandexGame.savesData.easyrecords.ToString();
        normalrecord.text = YandexGame.savesData.normalrecords.ToString(); 
        hardrecord.text     =   YandexGame.savesData .hardrecords.ToString();
    }

}
