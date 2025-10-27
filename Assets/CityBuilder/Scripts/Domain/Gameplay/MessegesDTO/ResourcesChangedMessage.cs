namespace CityBuilder.Domain.Gameplay.MessagesDTO
{
    public class ResourcesChangedMessage
    {
        public readonly int NewGold;
        
        public ResourcesChangedMessage(int newGold)
        {
            NewGold = newGold;
        }
    }
}