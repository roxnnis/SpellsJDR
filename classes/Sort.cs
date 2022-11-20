// Librairies
using com.Roxnnis.JDR.Statistique;
using com.Roxnnis.JDR.Ecriture;

// Classe
namespace com.Roxnnis.JDR.Sort
{
	/// <summary>
	/// Classe contenant un sort du JDR
	/// </summary>
	public class Sort
	{
		/// <summary>
		/// Nom du sort
		/// </summary>
		private string nom;
		public string GS_Nom {
			get {return nom;}
			set {nom = value;}
		}

		/// <summary>
		/// Statistiques du sort<br/>
		/// Contient MC, MM, ME et Puissance
		/// </summary>
		private Statistique statistique;
		public string GS_Statistique {
			get {return statistique;}
			set {statistique = value;}
		}

		/// <summary>
		/// Écriture d'un sort
		/// </summary>
		private Ecriture ecriture;
		public string GS_Ecriture {
			get {return ecriture;}
			set {ecriture = value;}
		}

		/// <summary>
		/// Description du sort
		/// </summary>
		private string description;
		public string GS_Description {
			get {return description;}
			set {description = value;}
		}

		/// <summary>
		/// Constructeur principal de la classe Sort
		/// </summary>
		/// <param name="n">Nom du sort</param>
		/// <param name="s">Valeurs des statistiques<br/>
		/// - ME (Masse énergétique)<br/>
		///- ME (Masse énergétique)<br/>- ME (Masse énergétique)<br/></param>
		/// <param name="e"></param>
		/// <param name="d"></param>
		public Sort(string n, Statistique s, Ecriture e, string d) {
			this(n,s,e);
			GS_Description = d;
		}
		public Sort(string n, Statistique s, Ecriture e) {
			GS_Nom = n;
			GS_Statistique = s;
			GS_Ecriture = e;
			GS_Description = "Sort sans description.";
		}
	}
}