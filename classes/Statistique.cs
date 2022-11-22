/// <summary>
/// Classe contenant les statistiques d'un sort
/// </summary>
public class Statistique
{
	/// <summary>
	/// Masse de calcul nécessaire pour lancer le sort
	/// </summary>
	public int calcul;

	/// <summary>
	/// Masse mémoire nécessaire pour enregistrer le sort
	/// </summary>
	public int memoire;

	/// <summary>
	/// Masse énergetique nécessaire supplémentaire pour lancer le sort
	/// </summary>
	public int energetique;

	public Statistique(int mc, int mm, int me){
		calcul = mc;
		memoire = mm;
		energetique = me;
	}
}