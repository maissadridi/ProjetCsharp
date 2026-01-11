using Domain;

public static class BonusStrategies
{
    public static BonusStrategy DefaultBonus = (obj, pointsBase) =>
    {
        double pts = pointsBase;

        if (obj.Etat == EtatObjet.Neuf) pts *= 1.10;
        if (obj.TypeObjet == TypeObjet.Livre) pts *= 1.20;

        return (int)Math.Round(pts);
    };
}
