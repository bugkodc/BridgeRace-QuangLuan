using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayUI : UICanvas
{
    [SerializeField] GameObject settingBtn;
    private void Update() // fix late
    {
        CheckGameStateFinish();

        CheckGameStatePlay();
    }
    public void CheckGameStateFinish()
    {
        if (GameManagerr.Instance.IsState(EGameState.Finish))
        {
            Hide();
        }
    }
    public void CheckGameStatePlay()
    {
        if (GameManagerr.Instance.IsState(EGameState.GamePlay))
        {
            Show();
        }
    }
    public void SettingBtn()
    {
        GameManagerr.Instance.ChangeState(EGameState.Pause);
        UIManager.Instance.OpenUI<Setting>();
        Close();
    }

    public void Hide()
    {
        settingBtn.SetActive(false);
    }

    public void Show()
    {
        settingBtn.SetActive(true);
    }
}
