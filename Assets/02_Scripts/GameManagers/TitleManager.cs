using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    [SerializeField] private Button startBtn;
    [SerializeField] private Button exitBtn;
    private void Start()
    {
        startBtn.onClick.AddListener(() =>SceneManager.LoadScene("SJHLobbyTestScene"));
        exitBtn.onClick.AddListener(() => Application.Quit());
    }
}
