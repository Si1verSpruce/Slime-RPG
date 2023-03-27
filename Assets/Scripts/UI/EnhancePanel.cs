using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnhancePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _lvl;
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private TextMeshProUGUI _value;
    [SerializeField] private Player _player;
    [SerializeField] private Button _enhance;
    [SerializeField] private StatType _statType;

    private void OnEnable()
    {
        StartCoroutine(UpdateDataEndFrame());
        _enhance.onClick.AddListener(Enhance);
    }

    private void OnDisable()
    {
        _enhance.onClick.RemoveListener(Enhance);
    }

    private void Enhance()
    {
        _player.EnhanceStat(_statType);
        UpdateData(_player.GetStatData(_statType));
    }

    private void UpdateData(StatData data)
    {
        _lvl.text = "lvl " + " " + data.Level.ToString();
        _priceText.text = data.EnhancePrice.ToString();
        _value.text = data.Value.ToString();
    }

    private IEnumerator UpdateDataEndFrame()
    {
        yield return new WaitForEndOfFrame();

        UpdateData(_player.GetStatData(_statType));
    }
}
