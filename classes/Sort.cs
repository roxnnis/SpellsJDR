using static Statistique;
using static Ecriture;

/// <summary>
/// Classe contenant un sort du JDR
/// </summary>
public class Sort
{
	/// <summary>
	/// Nom du sort
	/// </summary>
	private string nom;

	/// <summary>
	/// Statistiques du sort<br/>
	/// Contient MC, MM et ME
	/// </summary>
	private Statistique statistique = new Statistique(6,6,0);

	/// <summary>
	/// Écriture d'un sort
	/// </summary>
	private Ecriture ecriture;

	/// <summary>
	/// Description du sort
	/// </summary>
	private string description;

	/// <summary>
	/// Constructeur principal de la classe Sort
	/// </summary>
	/// <param name="n">Nom du sort</param>
	/// <param name="s">Valeurs des statistiques<br/>
	/// - ME (Masse énergétique)<br/>
	///- ME (Masse énergétique)<br/>- ME (Masse énergétique)<br/></param>
	/// <param name="e"></param>
	/// <param name="d"></param>
	public Sort(string n, Ecriture e, string d) : this(n,e)
	{
		description = d;
	}
	public Sort(string n, Ecriture e)
	{
		nom = n;
		ecriture = e;
		description = "Sort sans description.";
	}

	public override string ToString()
	{
		return	"Sort : " + nom + "\n" +
				"MC : " + statistique.calcul + " || MM : " + statistique.memoire + " || ME : " + statistique.energetique + "\n" +
				"Ecriture : " + ecriture + "\n\n" +
				"Description :\n" + description;
				//base.ToString();
	}

}