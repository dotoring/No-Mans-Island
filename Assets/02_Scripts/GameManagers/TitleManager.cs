using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    [SerializeField] private Button startBtn;
    [SerializeField] private Button exitBtn;
    private void Start()
    {
        startBtn.onClick.AddListener(() =>SceneManager.LoadScene("2_LobbyScene"));
        exitBtn.onClick.AddListener(() => Application.Quit());
    }
}
