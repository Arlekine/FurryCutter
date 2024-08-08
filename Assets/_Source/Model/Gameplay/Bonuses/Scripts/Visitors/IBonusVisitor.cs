namespace FurryCutter.Bonuses
{
    public interface IBonusVisitor
    {
        void Visit(IBonusPlacer bonusPlacer);
        void Visit(StarLinesCutBonusPlacer bonusPlacer);
        void Visit(LinesCutBonusPlacer bonusPlacer);
        void Visit(TemporalFieldBonusPlacer bonusPlacer);
    }
}