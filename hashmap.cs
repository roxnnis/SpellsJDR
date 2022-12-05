namespace Projet
{
	public class HM
	{
		/// <summary> Liste des types d'arguments valides </summary>
		public enum Arg {
			Valeur,Cible,Puissance,Temps,Addon,Chance,Forme,Distance,Taille,Longueur,Nombre,Propagation,MC,MM,ME
		};

		/// <summary> Liste des mots clés principaux valides </summary>
		public enum Cle {
			Constante,
			Eau, Feu, Foudre, Glace, Soin, Terre, Vent,
			Analyse, Armure, Esprit, Perméable, Vie_Pondéré,
			Brûle, Poison, Saigne, Soin_Statut, Son,
			Contact, Entité, Objet, Projectile, Rayon, Soi, Zone,
			Boule, Cage, Fleur, Flèche, Lame, Lance, Lierre, Ligne,
			Aura, Passif
		};

		/// <summary> Classe contenant les coûts fixes et variables d'un mot clé </summary>
		public class Cout{
			public record Fixe(int calcul,int memoire, int energie){
				internal int mc = calcul;
				internal int mm = memoire;
				internal int me = energie;
			};

			/// <summary> Coûts fixes du mot clé </summary>
			public Fixe cf;

			/// <summary> Coûts variables du mot clé </summary>
			public string cv;

			public Cout(int mc, int mm, int me, string coutsvariables){
				cf = new(mc,mm,me);
				cv = coutsvariables;
			}
		}

		/// <summary> Dictionnaire contenant les arguments possibles pour un mot clé donné </summary>
		public static Dictionary<Cle, Arg[]> ecriture = new Dictionary<Cle, Arg[]>(){
			// ================= Arguments numériques =================
			{Cle.Constante, new Arg[1]{Arg.Valeur}},
			// ================== Éléments  naturels ==================
			{Cle.Eau, new Arg[4]{Arg.Cible, Arg.Puissance, Arg.Temps, Arg.Addon}},
			{Cle.Feu, new Arg[4]{Arg.Cible, Arg.Puissance, Arg.Temps, Arg.Addon}},
			{Cle.Foudre, new Arg[4]{Arg.Cible, Arg.Puissance, Arg.Temps, Arg.Addon}},
			{Cle.Glace, new Arg[4]{Arg.Cible, Arg.Puissance, Arg.Temps, Arg.Addon}},
			{Cle.Soin, new Arg[4]{Arg.Cible, Arg.Puissance, Arg.Temps, Arg.Addon}},
			{Cle.Terre, new Arg[4]{Arg.Cible, Arg.Puissance, Arg.Temps, Arg.Addon}},
			{Cle.Vent, new Arg[4]{Arg.Cible, Arg.Puissance, Arg.Temps, Arg.Addon}}, // Not Implemented Yet
			// =================== Éléments neutres ===================
			{Cle.Analyse, new Arg[2]{Arg.Cible, Arg.Addon}},
			{Cle.Armure, new Arg[4]{Arg.Cible, Arg.Puissance, Arg.Temps, Arg.Addon}},
			{Cle.Esprit, new Arg[4]{Arg.Cible, Arg.Puissance, Arg.Temps, Arg.Addon}},
			{Cle.Perméable, new Arg[4]{Arg.Cible, Arg.Puissance, Arg.Temps, Arg.Addon}},
			{Cle.Vie_Pondéré, new Arg[4]{Arg.Cible, Arg.Puissance, Arg.Temps, Arg.Addon}},
			// ====================== Affliction ======================
			{Cle.Brûle, new Arg[4]{Arg.Cible, Arg.Puissance, Arg.Chance, Arg.Addon}},
			{Cle.Poison, new Arg[4]{Arg.Cible, Arg.Puissance, Arg.Chance, Arg.Addon}}, // Not Implemented Yet
			{Cle.Saigne, new Arg[4]{Arg.Cible, Arg.Puissance, Arg.Chance, Arg.Addon}}, // Not Implemented Yet
			{Cle.Soin_Statut, new Arg[3]{Arg.Cible, Arg.Chance, Arg.Addon}},
			{Cle.Son, new Arg[4]{Arg.Cible, Arg.Puissance, Arg.Chance, Arg.Addon}},
			// ======================== Cibles ========================
			{Cle.Contact, new Arg[1]{Arg.Cible}},
			{Cle.Entité, new Arg[0]},
			{Cle.Objet, new Arg[2]{Arg.Forme, Arg.Distance}},
			{Cle.Projectile, new Arg[2]{Arg.Forme, Arg.Distance}},
			{Cle.Rayon, new Arg[2]{Arg.Longueur, Arg.Taille}},
			{Cle.Soi, new Arg[0]},
			{Cle.Zone, new Arg[2]{Arg.Taille, Arg.Distance}},
			// ======================== Formes ========================
			{Cle.Boule, new Arg[2]{Arg.Taille, Arg.Nombre}},
			{Cle.Cage, new Arg[1]{Arg.Taille}},
			{Cle.Fleur, new Arg[2]{Arg.Taille, Arg.Nombre}},
			{Cle.Flèche, new Arg[2]{Arg.Taille, Arg.Nombre}},
			{Cle.Lame, new Arg[2]{Arg.Taille, Arg.Nombre}},
			{Cle.Lance, new Arg[2]{Arg.Taille, Arg.Nombre}},
			{Cle.Lierre, new Arg[2]{Arg.Taille, Arg.Propagation}},
			{Cle.Ligne, new Arg[0]},
			// ========================= Tour =========================
			{Cle.Aura, new Arg[0]},
			{Cle.Passif, new Arg[0]}
		};

		/// <summary> Dictionnaire contenant la description des coûts fixes et variables pour un mot clé donné </summary>
		public static Dictionary<Cle, Cout> costTable = new Dictionary<Cle, Cout>(){
			// ================= Arguments numériques =================
			{Cle.Constante,   new Cout(0,0,0,  "MM{Valeur/5}")},
			// ================== Éléments  naturels ==================
			{Cle.Eau,         new Cout(0,0,0,  "MC{Puissance/4} MM{Puissance/5} ME{Puissance/3+Temps/4}")},
			{Cle.Feu,         new Cout(1,1,0,  "MC{Puissance/2} ME{Temps/5}")},
			{Cle.Foudre,      new Cout(0,1,2,  "MC{Puissance/5} ME{Puissance+Temps/5+Addon}")},
			{Cle.Glace,       new Cout(1,4,0,  "MC{Puissance/4} MM{Puissance/3+Temps/3} ME{Temps/4}")},
			{Cle.Soin,        new Cout(0,1,0,  "MC{Puissance/8+Puissance/5+1-0^Puissance} ME{Puissance+Temps}")},
			{Cle.Terre,       new Cout(0,2,1,  "MC{Puissance/3} MM{Puissance/5+Temps/4} ME{Puissance/4*(Temps+1)}")},
			{Cle.Vent,        new Cout(0,0,0,  "NIY")}, // Not Implemented Yet
			// =================== Éléments neutres ===================
			{Cle.Analyse,     new Cout(0,2,1,  "")},
			{Cle.Armure,      new Cout(1,2,0,  "MC{Puissance/4} ME{Temps/6}")},
			{Cle.Esprit,      new Cout(1,2,0,  "MC{Puissance/4} ME{Temps/6}")},
			{Cle.Perméable,   new Cout(0,0,0,  "MC{Puissance^3} MM{Puissance*2+Addon} ME{MM*2}")},
			{Cle.Vie_Pondéré, new Cout(2,10,0, "ME{Puissance+Temps}")},
			// ====================== Affliction ======================
			{Cle.Brûle,       new Cout(1,1,0,  "MC{Puissance/4+Chance} ME{Puissance/2}")},
			{Cle.Poison,      new Cout(0,0,0,  "NIY")}, // Not Implemented Yet
			{Cle.Saigne,      new Cout(0,0,0,  "NIY")}, // Not Implemented Yet
			{Cle.Soin_Statut, new Cout(1,2,0,  "MC{Chance/2} ME{Chance*2}")},
			{Cle.Son,         new Cout(1,2,0,  "MC{Puissance+Chance} ME{Chance/3}")},
			// ======================== Cibles ========================
			{Cle.Contact,     new Cout(1,0,1,  "")},
			{Cle.Entité,      new Cout(0,1,0,  "")},
			{Cle.Objet,       new Cout(1,1,0,  "ME{Distance/3}")},
			{Cle.Projectile,  new Cout(1,1,0,  "ME{Distance/3}")},
			{Cle.Rayon,       new Cout(2,0,1,  "MC{Longueur/10} ME{Taille/4}")},
			{Cle.Soi,         new Cout(0,1,0,  "")},
			{Cle.Zone,        new Cout(0,1,0,  "MC{Taille/2+Distance/3} ME{Puissance/5*Taille}")},
			// ======================== Formes ========================
			{Cle.Boule,       new Cout(0,1,-1, "MC{Nombre*Taille/5} ME{Nombre}")},
			{Cle.Cage,        new Cout(0,4,2,  "MC{Taille/4}")},
			{Cle.Fleur,       new Cout(0,1,0,  "MC{Nombre*Taille/3} ME{2*(Nombre-1)}")},
			{Cle.Flèche,      new Cout(0,2,0,  "MC{Nombre*Taille/4} ME{2*(Nombre-1)}")},
			{Cle.Lame,        new Cout(0,4,0,  "MC{Nombre*Taille/3} ME{3*(Nombre-1)}")},
			{Cle.Lance,       new Cout(0,1,0,  "MC{Nombre*Taille/3} ME{3*(Nombre-1)}")},
			{Cle.Lierre,      new Cout(0,1,0,  "MC{Taille/2} MM{Propagation/2} ME{Propagation/2}")},
			{Cle.Ligne,       new Cout(1,1,0,  "")},
			// ========================= Tour =========================
			{Cle.Aura,        new Cout(1,2,0,  "ME{Puissance/2}")},
			{Cle.Passif,      new Cout(2,2,1,  "ME{Puissance/2}")}
		};

		/// <summary> Fonction qui sert à afficher les éléments du dictionnaire ecriture </summary>
		public static void afficherEcriture() {
			foreach (KeyValuePair<Cle, Arg[]> mot in ecriture)
			{
				Console.Write(mot.Key.ToString() + " : ");
				for(int i = 0; i < mot.Value.Length; i++){
					Console.Write(mot.Value[i].ToString()); if(i != mot.Value.Length - 1) Console.Write(" | ");
				}
				Console.WriteLine(); Console.WriteLine();
			}
		}
		/// <summary> Fonction qui sert à afficher les éléments du dictionnaire costTable </summary>
		public static void afficherCostTable() {
			foreach (KeyValuePair<Cle, Cout> cout in costTable)
			{
				Console.WriteLine("> " + cout.Key.ToString());
				Console.WriteLine("Coûts fixes :     [MC = " + cout.Value.cf.mc + "] [MM = " + cout.Value.cf.mm + "] [ME = " + cout.Value.cf.me + "]");
				Console.WriteLine("Coûts variables : " + cout.Value.cv);
				Console.WriteLine();
			}
		}

		/** <summary> Obtenir la clé principale du sort </summary>
			<param name="s"> L'écriture du sort </param>
			<returns> La clé correspondante </returns>
		*/
		//public static Cle getClePrincipal(string s){}

		/** <summary> Fonction importante : Calcul du coût du sort </summary>
			<param name="s"> L'écriture du sort </param>
		*/
		public static void Main(string s) {
			Cle mp = Cle.Contact;
			// = getClePrincipal(s); // Récupère le mot principal

			// Vérifie si le mot principal existe dans la liste.
			if(!ecriture.ContainsKey(mp)) throw new Exception("Cle non supporté.");

			// Obtention des Arguments valides
			ecriture.TryGetValue(mp, out var result);

			if(result != null)
				foreach(Arg elem in result){
				switch(elem){
					// Arguments obligatoires
					case Arg.Valeur :
					case Arg.Cible :
					case Arg.Puissance :
					case Arg.Chance :
					case Arg.Forme :
					case Arg.Taille :
					case Arg.Longueur :
					// Arguments optionnels
					case Arg.Temps :
					case Arg.Addon :
					case Arg.Distance :
					case Arg.Nombre :
					case Arg.Propagation :
					// Arguments spéciaux
					case Arg.MC :
					case Arg.MM :
					case Arg.ME :
					default:
						throw new Exception("Argument non reconnu. L'avez-vous ajouté à l'énumération \"Arg\" ?");
				}
			} else {
				throw new Exception("Cle principal non reconnu. L'avez-vous ajouté à l'énumération \"Cle\" ?");
			}
		}
	}
}