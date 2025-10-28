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

        //Create Building
        private Button _houseButton;
        private Button _farmButton;
        private Button _mineButton;
        
        //Selected Building
        private VisualElement _buildingInfoPanel;
        private Button _upgradeButton;
        private Button _moveButton;
        private Button _deleteButton;
        
        private readonly Subject<Unit> _houseButtonClicked = new();
        private readonly Subject<Unit> _farmButtonClicked = new();
        private readonly Subject<Unit> _mineButtonClicked = new();
        
        private readonly Subject<Unit> _upgradeButtonClicked = new();
        private readonly Subject<Unit> _moveButtonClicked = new();
        private readonly Subject<Unit> _deleteButtonClicked = new();
        
        public IObservable<Unit> OnHouseButtonClicked => _houseButtonClicked;
        public IObservable<Unit> OnFarmButtonClicked => _farmButtonClicked;
        public IObservable<Unit> OnMineButtonClicked => _mineButtonClicked;
        
        public IObservable<Unit> OnUpgradeButtonClicked => _upgradeButtonClicked;
        public IObservable<Unit> OnMoveButtonClicked => _moveButtonClicked;
        public IObservable<Unit> OnDeleteButtonClicked => _deleteButtonClicked;
        
        private int _currentGold;
        
        public void Start()
        {
            _root = uiDocument.rootVisualElement;
            _goldLabel = _root.Q<Label>("gold-label");

            _houseButton = _root.Q<Button>("house-button");
            _farmButton = _root.Q<Button>("farm-button");
            _mineButton = _root.Q<Button>("mine-button");
            
            _buildingInfoPanel = _root.Q<VisualElement>("building-info-panel");
            _upgradeButton = _root.Q<Button>("upgrade-button");
            _moveButton = _root.Q<Button>("move-button");
            _deleteButton = _root.Q<Button>("delete-button");
            
            _houseButton.clicked += () => _houseButtonClicked.OnNext(Unit.Default);
            _farmButton.clicked += () => _farmButtonClicked.OnNext(Unit.Default);
            _mineButton.clicked += () => _mineButtonClicked.OnNext(Unit.Default);
            
            _upgradeButton.clicked += () => _upgradeButtonClicked.OnNext(Unit.Default);
            _moveButton.clicked += () => _moveButtonClicked.OnNext(Unit.Default);
            _deleteButton.clicked += () => _deleteButtonClicked.OnNext(Unit.Default);
            
            HideBuildingInfo();
        }
        
        public void UpdateGold(int goldAmount)
        {
            _currentGold = goldAmount;
            
            if (_goldLabel != null)
                _goldLabel.text = _currentGold.ToString();
        }
        
        public void ShowBuildingInfo(bool canUpgrade)
        {
            _buildingInfoPanel.SetEnabled(true);

            _upgradeButton.SetEnabled(canUpgrade);
            _upgradeButton.text = canUpgrade ? "Upgrade" : "Max Level";
        }

        public void HideBuildingInfo()
        {
            _buildingInfoPanel.SetEnabled(false);
        }

        private void OnDestroy()
        {
            _houseButtonClicked?.OnCompleted();
            _farmButtonClicked?.OnCompleted();
            _mineButtonClicked?.OnCompleted();
            
            _upgradeButtonClicked?.OnCompleted();
            _moveButtonClicked?.OnCompleted();
            _deleteButtonClicked?.OnCompleted();
        }
    }
}