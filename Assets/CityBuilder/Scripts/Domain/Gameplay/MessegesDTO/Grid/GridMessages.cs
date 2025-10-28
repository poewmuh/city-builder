using CityBuilder.Domain.Gameplay.Models;

namespace CityBuilder.Domain.Gameplay.MessagesDTO.Grid
{
    public readonly struct GridHoveredMessage
    {
        public readonly GridPosition Position;
        public readonly bool IsValid;

        public GridHoveredMessage(GridPosition position, bool isValid)
        {
            Position = position;
            IsValid = isValid;
        }
    }
    
    public readonly struct GridClickedMessage
    {
        public readonly GridPosition Position;

        public GridClickedMessage(GridPosition position)
        {
            Position = position;
        }
    }
}
