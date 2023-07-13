using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LifeUIController : MonoBehaviour
{
    [SerializeField] private Slider _lifeSlider;
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _losePanel;
    [SerializeField] private Button _restartButton;

    private void Awake()
    {
        _restartButton.onClick.AddListener(() =>
        {
            //Time.timeScale = 1;
            SceneManager.LoadScene("SampleScene");
        });
    }

    private void Update()
    {
        _lifeSlider.value = _player.Life;

        if(_player.Life <= 0)
        {
            //Time.timeScale = 0;
            _losePanel.SetActive(true);
        }
    }
}
