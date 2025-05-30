﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Helios.GUI {
    public class WheelMenuController : MonoBehaviour {
        private const int FULL_CIRCLE = 360;

        [Header("References")]
        [SerializeField] private Image[] _imgRewards;
        [SerializeField] private CurrencySO[] _currencyRewards;
        [SerializeField] private TextMeshProUGUI[] _amountRewards;
        [SerializeField] private int[] _amounts;
        [SerializeField] private GameObject _goRewardPopup;
        [SerializeField] private Image _imgRewardIcon;
        [SerializeField] private TextMeshProUGUI _rewardAmount;
        [SerializeField] private Animator _animator;
        [SerializeField] private Image _imgFocusLine;
        [SerializeField] private Button _btnTapToClose;
        [SerializeField] private Button TurnButton;
        [SerializeField] private GameObject Circle;           // Rotatable Object with rewards

        [Header("Config params")]
        [SerializeField] private int _nbSpinTime = 5;
        [SerializeField] private int _nbAnimationTime = 3;
        [SerializeField] private List<AnimationCurve> animationCurves;

        private bool spinning;
        private float anglePerItem;
        private int itemNumber;

        private void Awake() {
            TurnButton.onClick.AddListener(RewardedTurnWheel);
            for (int i = 0; i < _currencyRewards.Length; i++){
                _imgRewards[i].sprite = _currencyRewards[i]._currencyIconImage;
            }
            for (int i = 0; i < _amounts.Length; i++){
                _amountRewards[i].text = _amounts[i].ToString();
            }
        }

        private IEnumerator ShowReward(int index) {
            yield return new WaitForSeconds(0.4f);
            TurnButton.interactable = true;
            _animator.SetTrigger("Released");
            _imgFocusLine.gameObject.SetActive(true);
            _goRewardPopup.SetActive(true);
            _imgRewardIcon.sprite = _imgRewards[index].sprite;
            _rewardAmount.text = _amountRewards[index].text;
        }


        public void ClaimReward(){
            CurrencyView.Instance.CreditCurrency(_currencyRewards[itemNumber], _amounts[itemNumber]);
        }

        void Start() {
            spinning = false;
            anglePerItem = FULL_CIRCLE / _imgRewards.Length;
        }

        public void RewardedTurnWheel()
        {
            AdmodManager.Instance.ShowRewardedAd(TurnWheel);
        }
        public void TurnWheel() {
            
            if(!spinning) {
                //UI handle
                _animator.SetTrigger("Pressed");
                _btnTapToClose.interactable = false;
                _imgFocusLine.gameObject.SetActive(false);

                itemNumber = Random.Range(0, _imgRewards.Length);
                float maxAngle = _nbSpinTime * FULL_CIRCLE + (itemNumber * anglePerItem);
                StartCoroutine(SpinTheWheel(_nbAnimationTime, maxAngle));
            }
        }

        IEnumerator SpinTheWheel(float time, float maxAngle) {
            spinning = true;
            TurnButton.interactable = false;

            float timer = 0.0f;
            float startAngle = Circle.transform.eulerAngles.z;
            maxAngle -= startAngle;

            int animationCurveNumber = Random.Range(0, animationCurves.Count);
            //Debug.Log("Animation Curve No. : " + animationCurveNumber);

            while(timer < time) {
                //to calculate rotation
                float angle = maxAngle * animationCurves[animationCurveNumber].Evaluate(timer / time);
                Circle.transform.eulerAngles = new Vector3(0.0f, 0.0f, angle + startAngle);
                timer += Time.deltaTime;
                yield return 0;
            }

            Circle.transform.eulerAngles = new Vector3(0.0f, 0.0f, maxAngle + startAngle);
            spinning = false;
            _btnTapToClose.interactable = true;
            StartCoroutine(ShowReward(itemNumber));
        }
    }
}
