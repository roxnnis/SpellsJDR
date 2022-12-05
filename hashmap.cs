namespace Projet
{
	public class HM
	{
		/// <summary> Liste des types d'arguments valides </summary>
		public enum Arg {
			Valeur,Cible,Puissance,Temps,Addon,Chance,Forme,Distance,Taille,Longueur,Nombre,Propagation,MC,MM,ME
		};

		/// <summary> Liste des mots clés principaux valides </summary>
		public enum Mot {
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
			public record fixe(int calcul,int memoire, int energie){
				internal int mc = calcul;
				internal int mm = memoire;
				internal int me = energie;
			};

			/// <summary> Coûts fixes du mot clé </summary>
			public fixe cf;

			/// <summary> Coûts variables du mot clé </summary>
			public string cv;

			public Cout(int mc, int mm, int me, string coutsvariables){
				cf = new(mc,mm,me);
				cv = coutsvariables;
			}
		}

		/// <summary> Dictionnaire contenant les arguments possibles pour un mot clé donné </summary>
		public static Dictionary<Mot, Arg[]> ecriture = new Dictionary<Mot, Arg[]>(){
			// ================= Arguments numériques =================
			{Mot.Constante, new Arg[1]{Arg.Valeur}},
			// ================== Éléments  naturels ==================
			{Mot.Eau, new Arg[4]{Arg.Cible, Arg.Puissance, Arg.Temps, Arg.Addon}},
			{Mot.Feu, new Arg[4]{Arg.Cible, Arg.Puissance, Arg.Temps, Arg.Addon}},
			{Mot.Foudre, new Arg[4]{Arg.Cible, Arg.Puissance, Arg.Temps, Arg.Addon}},
			{Mot.Glace, new Arg[4]{Arg.Cible, Arg.Puissance, Arg.Temps, Arg.Addon}},
			{Mot.Soin, new Arg[4]{Arg.Cible, Arg.Puissance, Arg.Temps, Arg.Addon}},
			{Mot.Terre, new Arg[4]{Arg.Cible, Arg.Puissance, Arg.Temps, Arg.Addon}},
			{Mot.Vent, new Arg[4]{Arg.Cible, Arg.Puissance, Arg.Temps, Arg.Addon}}, // Not Implemented Yet
			// =================== Éléments neutres ===================
			{Mot.Analyse, new Arg[2]{Arg.Cible, Arg.Addon}},
			{Mot.Armure, new Arg[4]{Arg.Cible, Arg.Puissance, Arg.Temps, Arg.Addon}},
			{Mot.Esprit, new Arg[4]{Arg.Cible, Arg.Puissance, Arg.Temps, Arg.Addon}},
			{Mot.Perméable, new Arg[4]{Arg.Cible, Arg.Puissance, Arg.Temps, Arg.Addon}},
			{Mot.Vie_Pondéré, new Arg[4]{Arg.Cible, Arg.Puissance, Arg.Temps, Arg.Addon}},
			// ====================== Affliction ======================
			{Mot.Brûle, new Arg[4]{Arg.Cible, Arg.Puissance, Arg.Chance, Arg.Addon}},
			{Mot.Poison, new Arg[4]{Arg.Cible, Arg.Puissance, Arg.Chance, Arg.Addon}}, // Not Implemented Yet
			{Mot.Saigne, new Arg[4]{Arg.Cible, Arg.Puissance, Arg.Chance, Arg.Addon}}, // Not Implemented Yet
			{Mot.Soin_Statut, new Arg[3]{Arg.Cible, Arg.Chance, Arg.Addon}},
			{Mot.Son, new Arg[4]{Arg.Cible, Arg.Puissance, Arg.Chance, Arg.Addon}},
			// ======================== Cibles ========================
			{Mot.Contact, new Arg[1]{Arg.Cible}},
			{Mot.Entité, new Arg[0]},
			{Mot.Objet, new Arg[2]{Arg.Forme, Arg.Distance}},
			{Mot.Projectile, new Arg[2]{Arg.Forme, Arg.Distance}},
			{Mot.Rayon, new Arg[2]{Arg.Longueur, Arg.Taille}},
			{Mot.Soi, new Arg[0]},
			{Mot.Zone, new Arg[2]{Arg.Taille, Arg.Distance}},
			// ======================== Formes ========================
			{Mot.Boule, new Arg[2]{Arg.Taille, Arg.Nombre}},
			{Mot.Cage, new Arg[1]{Arg.Taille}},
			{Mot.Fleur, new Arg[2]{Arg.Taille, Arg.Nombre}},
			{Mot.Flèche, new Arg[2]{Arg.Taille, Arg.Nombre}},
			{Mot.Lame, new Arg[2]{Arg.Taille, Arg.Nombre}},
			{Mot.Lance, new Arg[2]{Arg.Taille, Arg.Nombre}},
			{Mot.Lierre, new Arg[2]{Arg.Taille, Arg.Propagation}},
			{Mot.Ligne, new Arg[0]},
			// ========================= Tour =========================
			{Mot.Aura, new Arg[0]},
			{Mot.Passif, new Arg[0]}
		};

		/// <summary> Dictionnaire contenant la description des coûts fixes et variables pour un mot clé donné </summary>
		public static Dictionary<Mot, Cout> costTable = new Dictionary<Mot, Cout>(){
			// ================= Arguments numériques =================
			{Mot.Constante,   new Cout(0,0,0, "MM{Valeur/5}")},
			// ================== Éléments  naturels ==================
			{Mot.Eau,         new Cout(0,0,0,  "MC{Puissance/4} MM{Puissance/5} ME{Puissance/3+Temps/4}")},
			{Mot.Feu,         new Cout(1,1,0,  "MC{Puissance/2} ME{Temps/5}")},
			{Mot.Foudre,      new Cout(0,1,2,  "MC{Puissance/5} ME{Puissance+Temps/5+Addon}")},
			{Mot.Glace,       new Cout(1,4,0,  "MC{Puissance/4} MM{Puissance/3+Temps/3} ME{Temps/4}")},
			{Mot.Soin,        new Cout(0,1,0,  "MC{Puissance/8+Puissance/5+1-0^Puissance} ME{Puissance+Temps}")},
			{Mot.Terre,       new Cout(0,2,1,  "MC{Puissance/3} MM{Puissance/5+Temps/4} ME{Puissance/4*(Temps+1)}")},
			{Mot.Vent,        new Cout(0,0,0,  "NIY")}, // Not Implemented Yet
			// =================== Éléments neutres ===================
			{Mot.Analyse,     new Cout(0,2,1,  "")},
			{Mot.Armure,      new Cout(1,2,0,  "MC{Puissance/4} ME{Temps/6}")},
			{Mot.Esprit,      new Cout(1,2,0,  "MC{Puissance/4} ME{Temps/6}")},
			{Mot.Perméable,   new Cout(0,0,0,  "MC{Puissance^3} MM{Puissance*2+Addon} ME{MM*2}")},
			{Mot.Vie_Pondéré, new Cout(2,10,0, "ME{Puissance+Temps}")},
			// ====================== Affliction ======================
			{Mot.Brûle,       new Cout(1,1,0,  "MC{Puissance/4+Chance} ME{Puissance/2}")},
			{Mot.Poison,      new Cout(0,0,0,  "NIY")}, // Not Implemented Yet
			{Mot.Saigne,      new Cout(0,0,0,  "NIY")}, // Not Implemented Yet
			{Mot.Soin_Statut, new Cout(1,2,0,  "MC{Chance/2} ME{Chance*2}")},
			{Mot.Son,         new Cout(1,2,0,  "MC{Puissance+Chance} ME{Chance/3}")},
			// ======================== Cibles ========================
			{Mot.Contact,     new Cout(1,0,1,  "")},
			{Mot.Entité,      new Cout(0,1,0,  "")},
			{Mot.Objet,       new Cout(1,1,0,  "ME{Distance/3}")},
			{Mot.Projectile,  new Cout(1,1,0,  "ME{Distance/3}")},
			{Mot.Rayon,       new Cout(2,0,1,  "MC{Longueur/10} ME{Taille/4}")},
			{Mot.Soi,         new Cout(0,1,0,  "")},
			{Mot.Zone,        new Cout(0,1,0,  "MC{Taille/2+Distance/3} ME{Puissance/5*Taille}")},
			// ======================== Formes ========================
			{Mot.Boule,       new Cout(0,1,-1, "MC{Nombre*Taille/5} ME{Nombre}")},
			{Mot.Cage,        new Cout(0,4,2,  "MC{Taille/4}")},
			{Mot.Fleur,       new Cout(0,1,0,  "MC{Nombre*Taille/3} ME{2*(Nombre-1)}")},
			{Mot.Flèche,      new Cout(0,2,0,  "MC{Nombre*Taille/4} ME{2*(Nombre-1)}")},
			{Mot.Lame,        new Cout(0,4,0,  "MC{Nombre*Taille/3} ME{3*(Nombre-1)}")},
			{Mot.Lance,       new Cout(0,1,0,  "MC{Nombre*Taille/3} ME{3*(Nombre-1)}")},
			{Mot.Lierre,      new Cout(0,1,0,  "MC{Taille/2} MM{Propagation/2} ME{Propagation/2}")},
			{Mot.Ligne,       new Cout(1,1,0,  "")},
			// ========================= Tour =========================
			{Mot.Aura,        new Cout(1,2,0,  "ME{Puissance/2}")},
			{Mot.Passif,      new Cout(2,2,1,  "ME{Puissance/2}")}
		};

		/// <summary> Fonction qui sert à afficher les éléments du dictionnaire ecriture </summary>
		public static void afficherEcriture() {
			foreach (KeyValuePair<Mot, Arg[]> mot in ecriture)
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
			foreach (KeyValuePair<Mot, Cout> cout in costTable)
			{
				Console.WriteLine("> " + cout.Key.ToString());
				Console.WriteLine("Coûts fixes :     [MC = " + cout.Value.cf.mc + "] [MM = " + cout.Value.cf.mm + "] [ME = " + cout.Value.cf.me + "]");
				Console.WriteLine("Coûts variables : " + cout.Value.cv);
				Console.WriteLine();
			}
		}

		public static Mot getMotPrincipal(string s){return Mot.Analyse;}
		public static void Main(string s) {
			Mot mp = getMotPrincipal(s); // Récupère le mot principal

			// Vérifie si le mot principal existe dans la liste.
			if(!ecriture.ContainsKey(mp)) throw new Exception("Mot non supporté.");

			// Obtention 
			ecriture.TryGetValue(mp, out var result);
			if(result != null)
				foreach(Arg elem in result){
				switch(elem){
					case Arg.Valeur :
					case Arg.Cible :
					case Arg.Puissance :
					case Arg.Temps :
					case Arg.Addon :
					case Arg.Chance :
					case Arg.Forme :
					case Arg.Distance :
					case Arg.Taille :
					case Arg.Longueur :
					case Arg.Nombre :
					case Arg.Propagation :
					case Arg.MC :
					case Arg.MM :
					case Arg.ME :
					default:
						throw new Exception("Argument non reconnu. L'avez-vous ajouté à l'énumération \"Arg\" ?");
				}
			} else {
				throw new Exception("Mot principal non reconnu. L'avez-vous ajouté à l'énumération \"Mot\" ?");
			}
		}
	}
}