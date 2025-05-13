using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Energy : Singleton<Energy>
{
    [SerializeField] private float _maxEnergy = 10f;
    [SerializeField] private float _rechargeTime = 2f;

    private Slider _energySlider;
    private Animator _energyAnimator;
    private float _currentEnergy;

    const string ENERGY_SLIDER_TEXT = "Energy Slider";
    const string ENERGY_ANIMATOR_TEXT = "Energy Animator";
    const string _onMaxTrigger = "EnergyMax";

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        _currentEnergy = _maxEnergy;

        UpdateEnergySlider();
    }

    public void RestoreEnergy()
    {
        if (_currentEnergy < _maxEnergy)
        {
            _currentEnergy++;           
            UpdateEnergySlider();
            CheckIfEnergyMax();
        }
    }

    public bool TryUseEnergy(float energyAmount)
    {
        if (_currentEnergy < energyAmount)
        {
            return false;
        }
        _currentEnergy -= energyAmount;
        UpdateEnergySlider();
        return true;
    }

    private IEnumerator RestoreEnergyRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(_rechargeTime);
            RestoreEnergy();
        }
    }


    private void CheckIfEnergyMax()
    {
        if (_currentEnergy >= _maxEnergy)
        {
            _energyAnimator.SetTrigger(_onMaxTrigger);
        }
    }

    private void UpdateEnergySlider()
    {
        if (_energySlider == null)
        {
            _energySlider = GameObject.Find(ENERGY_SLIDER_TEXT).GetComponent<Slider>();
            _energyAnimator = GameObject.Find(ENERGY_ANIMATOR_TEXT).GetComponent<Animator>();
        }

        _energySlider.maxValue = _maxEnergy;
        _energySlider.value = _currentEnergy;

        if (_currentEnergy < _maxEnergy)
        {
            StopAllCoroutines();
            StartCoroutine(RestoreEnergyRoutine());
        }
    }
}
