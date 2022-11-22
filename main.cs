using static Sort;
using static Mot;
public class MainClass
{
	public static void Main()
	{
		Sort s = new Sort("Test", new Ecriture(Element.EAU,Cible.SOI,Numerique.CONSTANTE), "OuiOuiOui !");
		Console.WriteLine(s);
	}
}
