using CityBuilder.Domain.Gameplay.Models.Buildings.Base;
using UnityEngine;

namespace CityBuilder.Domain.Gameplay.Models
{
    public class PlayerResources
    {
        public int Gold { get; private set;  }
        
        public PlayerResources(int startGold = 100)
        {
            Gold = startGold;
        }
        
        public void AddGold(int amount)
        {
            if (amount < 0)
            {
                Debug.LogError("[PlayerResources.AddGold] Cannot add negative amount of gold.");
                return;
            }
            
            Gold += amount;
        }
        
        public bool TrySpendGold(Cost cost)
        {
            if (Gold < cost.Gold)
                return false;

            Gold -= cost.Gold;
            return true;
        }
    }
}
