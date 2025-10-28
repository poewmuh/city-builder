using System;
using UnityEngine;
using UnityEngine.UIElements;
using UniRx;

namespace CityBuilder.Presentation.Gameplay.Views
{
    public class GameplayUIView : MonoBehaviour
    {
        [SerializeField] private UIDocument uiDocument;
        private VisualElement _root;
        private Label _goldLabel;

        private Button _houseButton;
        private Button _farmButton;
        private Button _mineButton;
        
        private readonly Subject<Unit> _houseButtonClicked = new();
        private readonly Subject<Unit> _farmButtonClicked = new();
        private readonly Subject<Unit> _mineButtonClicked = new();
        
        public IObservable<Unit> OnHouseButtonClicked => _houseButtonClicked;
        public IObservable<Unit> OnFarmButtonClicked => _farmButtonClicked;
        public IObservable<Unit> OnMineButtonClicked => _mineButtonClicked;
        
        private int _currentGold;
        
        public void Start()
        {
            _root = uiDocument.rootVisualElement;
            _goldLabel = _root.Q<Label>("gold-label");

            _houseButton = _root.Q<Button>("house-button");
            _farmButton = _root.Q<Button>("farm-button");
            _mineButton = _root.Q<Button>("mine-button");
            
            _houseButton.clicked += () => _houseButtonClicked.OnNext(Unit.Default);
            _farmButton.clicked += () => _farmButtonClicked.OnNext(Unit.Default);
            _mineButton.clicked += () => _mineButtonClicked.OnNext(Unit.Default);
        }
        
        public void UpdateGold(int goldAmount)
        {
            _currentGold = goldAmount;
            
            if (_goldLabel != null)
                _goldLabel.text = _currentGold.ToString();
        }

        private void OnDestroy()
        {
            _houseButtonClicked?.OnCompleted();
            _farmButtonClicked?.OnCompleted();
            _mineButtonClicked?.OnCompleted();
        }
    }
}