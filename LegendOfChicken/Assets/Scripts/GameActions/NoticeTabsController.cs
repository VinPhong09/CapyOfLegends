using UnityEngine;
using UnityEngine.UI;

public class NoticeTabsController : MonoBehaviour
{
    public static NoticeTabsController Instance;

    [Header("Victory")]
    [SerializeField] private GameObject _victoryTab;

    public void OnEnable()
    {
        Instance = this;
    }

    public void OnVictoryTab()
    {
        _victoryTab.SetActive(true);
    }
}
