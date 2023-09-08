using UnityEngine;
using TMPro;

public class LastUpdate : MonoBehaviour
{
    [SerializeField]
    TMP_Text buildDateText;

    [SerializeField]
    BuildDate buildDate;

    private void Awake()
    {
        buildDateText.text = buildDate.s_BuildDate;
    }
}
