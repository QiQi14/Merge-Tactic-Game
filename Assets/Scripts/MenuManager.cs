using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject menuContainer;
    [SerializeField]
    private GameObject levelContainer;
    [SerializeField] 
    private GameObject volumnContainer;

    [SerializeField]
    private Slider bgmSlider;

    private void Awake()
    {
        GameManager.OnGameStateChanged += OnGameStateChanged;
    }

    private void OnGameStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.Menu:
                {
                    menuContainer.SetActive(true);
                    levelContainer.SetActive(false);
                    volumnContainer.SetActive(false);
                    break;
                }
            case GameState.GameLevel:
                {
                    levelContainer.SetActive(true);
                    menuContainer.SetActive(false);
                    volumnContainer.SetActive(false);
                    break;
                }
            case GameState.Volumn:
                {
                    volumnContainer.SetActive(true);
                    menuContainer.SetActive(false);
                    levelContainer.SetActive(false);
                    break;
                }
            default: break;
        }
        //menuContainer.SetActive(state == GameState.Menu);
        //levelContainer.SetActive(state == GameState.GameLevel);
        //volumnContainer.SetActive(state == GameState.Volumn);
    }
    
    public void BGMVolumn()
    {
        AudioManager.instance.BGMVolumn(bgmSlider.value);
    }
}
