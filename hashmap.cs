namespace Projet
{
	public class HM
	{
		/// <summary> Liste des types d'arguments valides </summary>
		public enum Arg {
			Valeur,Cible,Puissance,Temps,Addon,Chance,Forme,Distance,Taille,Longueur,Nombre,Propagation,MC,MM,ME
		};

		/// <summary> Liste des arguments numériques valides </summary>
		public enum Num {
			Constante
		}

		/// <summary> Liste des mots clés principaux valides </summary>
		public enum Cle {
			Eau, Feu, Foudre, Glace, Soin, Terre, Vent,
			Analyse, Armure, Esprit, Perméable, Vie_Pondéré,
			Brûle, Poison, Saigne, Soin_Statut, Son,
		};

		/// <summary> Liste des cibles valides </summary>
		public enum Cible {
			Contact, Entité, Objet, Projectile, Rayon, Soi, Zone
		}

		/// <summary> Liste des mots formes valides </summary>
		public enum Forme {
			Boule, Cage, Fleur, Flèche, Lame, Lance, Lierre, Ligne,
		}

		/// <summary> Liste des arguments de temps valides </summary>
		public enum Temps {
			Aura, Constante, Passif
		}

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
		public static Dictionary<Enum, Arg[]> ListeArguments = new Dictionary<Enum, Arg[]>(){
			// ================= Arguments numériques =================
			{Num.Constante,    new Arg[1]{Arg.Valeur}},
			// ================== Éléments  naturels ==================
			{Cle.Eau,          new Arg[4]{Arg.Cible,    Arg.Puissance, Arg.Temps,  Arg.Addon}},
			{Cle.Feu,          new Arg[4]{Arg.Cible,    Arg.Puissance, Arg.Temps,  Arg.Addon}},
			{Cle.Foudre,       new Arg[4]{Arg.Cible,    Arg.Puissance, Arg.Temps,  Arg.Addon}},
			{Cle.Glace,        new Arg[4]{Arg.Cible,    Arg.Puissance, Arg.Temps,  Arg.Addon}},
			{Cle.Soin,         new Arg[4]{Arg.Cible,    Arg.Puissance, Arg.Temps,  Arg.Addon}},
			{Cle.Terre,        new Arg[4]{Arg.Cible,    Arg.Puissance, Arg.Temps,  Arg.Addon}},
			{Cle.Vent,         new Arg[4]{Arg.Cible,    Arg.Puissance, Arg.Temps,  Arg.Addon}}, // Not Implemented Yet
			// =================== Éléments neutres ===================
			{Cle.Analyse,      new Arg[2]{Arg.Cible,    Arg.Addon                           }},
			{Cle.Armure,       new Arg[4]{Arg.Cible,    Arg.Puissance, Arg.Temps,  Arg.Addon}},
			{Cle.Esprit,       new Arg[4]{Arg.Cible,    Arg.Puissance, Arg.Temps,  Arg.Addon}},
			{Cle.Perméable,    new Arg[4]{Arg.Cible,    Arg.Puissance, Arg.Temps,  Arg.Addon}},
			{Cle.Vie_Pondéré,  new Arg[4]{Arg.Cible,    Arg.Puissance, Arg.Temps,  Arg.Addon}},
			// ====================== Affliction ======================
			{Cle.Brûle,        new Arg[4]{Arg.Cible,    Arg.Puissance, Arg.Chance, Arg.Addon}},
			{Cle.Poison,       new Arg[4]{Arg.Cible,    Arg.Puissance, Arg.Chance, Arg.Addon}}, // Not Implemented Yet
			{Cle.Saigne,       new Arg[4]{Arg.Cible,    Arg.Puissance, Arg.Chance, Arg.Addon}}, // Not Implemented Yet
			{Cle.Soin_Statut,  new Arg[3]{Arg.Cible,    Arg.Chance,    Arg.Addon            }},
			{Cle.Son,          new Arg[4]{Arg.Cible,    Arg.Puissance, Arg.Chance, Arg.Addon}},
			// ======================== Cibles ========================
			{Cible.Contact,    new Arg[1]{Arg.Cible                                         }},
			{Cible.Entité,     new Arg[0]                                                    },
			{Cible.Objet,      new Arg[2]{Arg.Forme,    Arg.Distance                        }},
			{Cible.Projectile, new Arg[2]{Arg.Forme,    Arg.Distance                        }},
			{Cible.Rayon,      new Arg[2]{Arg.Longueur, Arg.Taille                          }},
			{Cible.Soi,        new Arg[0]                                                    },
			{Cible.Zone,       new Arg[2]{Arg.Taille,   Arg.Distance                        }},
			// ======================== Formes ========================
			{Forme.Boule,      new Arg[2]{Arg.Taille,   Arg.Nombre                          }},
			{Forme.Cage,       new Arg[1]{Arg.Taille                                        }},
			{Forme.Fleur,      new Arg[2]{Arg.Taille,   Arg.Nombre                          }},
			{Forme.Flèche,     new Arg[2]{Arg.Taille,   Arg.Nombre                          }},
			{Forme.Lame,       new Arg[2]{Arg.Taille,   Arg.Nombre                          }},
			{Forme.Lance,      new Arg[2]{Arg.Taille,   Arg.Nombre                          }},
			{Forme.Lierre,     new Arg[2]{Arg.Taille,   Arg.Propagation                     }},
			{Forme.Ligne,      new Arg[0]                                                    },
			// ========================= Tour =========================
			{Temps.Aura,       new Arg[0]                                                    },
			{Temps.Constante,  new Arg[1]{Arg.Valeur                                        }},
			{Temps.Passif,     new Arg[0]                                                    }
		};

		/// <summary> Dictionnaire contenant la description des coûts fixes et variables pour un mot clé donné </summary>
		public static Dictionary<Enum, Cout> costTable = new Dictionary<Enum, Cout>(){
			// ================= Arguments numériques =================
			{Num.Constante,    new Cout(0,0,0,  "MM{Valeur/5}")},
			{Temps.Constante,  new Cout(0,0,0,  "MM{Valeur/5}")},
			// ================== Éléments  naturels ==================
			{Cle.Eau,          new Cout(0,0,0,  "MC{Puissance/4} MM{Puissance/5} ME{Puissance/3+Temps/4}")},
			{Cle.Feu,          new Cout(1,1,0,  "MC{Puissance/2} ME{Temps/5}")},
			{Cle.Foudre,       new Cout(0,1,2,  "MC{Puissance/5} ME{Puissance+Temps/5+Addon}")},
			{Cle.Glace,        new Cout(1,4,0,  "MC{Puissance/4} MM{Puissance/3+Temps/3} ME{Temps/4}")},
			{Cle.Soin,         new Cout(0,1,0,  "MC{Puissance/8+Puissance/5+1-0^Puissance} ME{Puissance+Temps}")},
			{Cle.Terre,        new Cout(0,2,1,  "MC{Puissance/3} MM{Puissance/5+Temps/4} ME{Puissance/4*(Temps+1)}")},
			{Cle.Vent,         new Cout(0,0,0,  "NIY")}, // Not Implemented Yet
			// =================== Éléments neutres ===================
			{Cle.Analyse,      new Cout(0,2,1,  "")},
			{Cle.Armure,       new Cout(1,2,0,  "MC{Puissance/4} ME{Temps/6}")},
			{Cle.Esprit,       new Cout(1,2,0,  "MC{Puissance/4} ME{Temps/6}")},
			{Cle.Perméable,    new Cout(0,0,0,  "MC{Puissance^3} MM{Puissance*2+Addon} ME{MM*2}")},
			{Cle.Vie_Pondéré,  new Cout(2,10,0, "ME{Puissance+Temps}")},
			// ====================== Affliction ======================
			{Cle.Brûle,        new Cout(1,1,0,  "MC{Puissance/4+Chance} ME{Puissance/2}")},
			{Cle.Poison,       new Cout(0,0,0,  "NIY")}, // Not Implemented Yet
			{Cle.Saigne,       new Cout(0,0,0,  "NIY")}, // Not Implemented Yet
			{Cle.Soin_Statut,  new Cout(1,2,0,  "MC{Chance/2} ME{Chance*2}")},
			{Cle.Son,          new Cout(1,2,0,  "MC{Puissance+Chance} ME{Chance/3}")},
			// ======================== Cibles ========================
			{Cible.Contact,    new Cout(1,0,1,  "")},
			{Cible.Entité,     new Cout(0,1,0,  "")},
			{Cible.Objet,      new Cout(1,1,0,  "ME{Distance/3}")},
			{Cible.Projectile, new Cout(1,1,0,  "ME{Distance/3}")},
			{Cible.Rayon,      new Cout(2,0,1,  "MC{Longueur/10} ME{Taille/4}")},
			{Cible.Soi,        new Cout(0,1,0,  "")},
			{Cible.Zone,       new Cout(0,1,0,  "MC{Taille/2+Distance/3} ME{Puissance/5*Taille}")},
			// ======================== Formes ========================
			{Forme.Boule,      new Cout(0,1,-1, "MC{Nombre*Taille/5} ME{Nombre}")},
			{Forme.Cage,       new Cout(0,4,2,  "MC{Taille/4}")},
			{Forme.Fleur,      new Cout(0,1,0,  "MC{Nombre*Taille/3} ME{2*(Nombre-1)}")},
			{Forme.Flèche,     new Cout(0,2,0,  "MC{Nombre*Taille/4} ME{2*(Nombre-1)}")},
			{Forme.Lame,       new Cout(0,4,0,  "MC{Nombre*Taille/3} ME{3*(Nombre-1)}")},
			{Forme.Lance,      new Cout(0,1,0,  "MC{Nombre*Taille/3} ME{3*(Nombre-1)}")},
			{Forme.Lierre,     new Cout(0,1,0,  "MC{Taille/2} MM{Propagation/2} ME{Propagation/2}")},
			{Forme.Ligne,      new Cout(1,1,0,  "")},
			// ========================= Tour =========================
			{Temps.Aura,       new Cout(1,2,0,  "ME{Puissance/2}")},
			{Temps.Passif,     new Cout(2,2,1,  "ME{Puissance/2}")}
		};

		/// <summary> Fonction qui sert à afficher les éléments du dictionnaire ListeCle </summary>
		public static void afficherEcriture() {
			foreach (KeyValuePair<Enum, Arg[]> mot in ListeArguments)
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
			foreach (KeyValuePair<Enum, Cout> cout in costTable)
			{
				Console.WriteLine("> " + cout.Key.ToString());
				Console.WriteLine("Coûts fixes :     [MC = " + cout.Value.cf.mc + "] [MM = " + cout.Value.cf.mm + "] [ME = " + cout.Value.cf.me + "]");
				Console.WriteLine("Coûts variables : " + cout.Value.cv);
				Console.WriteLine();
			}
		}

		/** <summary> Fonction qui gère les impulsions au clavier de l'utilisateur pour un sort </summary>
			<returns> L'écriture du sort envoyée par l'utilisateur
		*/
		public static string Spell(){
			Console.WriteLine();
			Console.WriteLine("Veuillez entrer un sort : ");
			var sort = Console.ReadLine();
			while (sort == "" || sort == null)
			{
				Console.WriteLine();
				Console.WriteLine("Veuillez entrer un sort : ");
				sort = Console.ReadLine();
			}
			Console.WriteLine();
			return sort;
		}

		/** <summary> Obtenir la clé principale du sort </summary>
			<param name="s"> L'écriture du sort </param>
			<returns> La clé correspondante </returns>
			<exception cref="ArgumentOutOfRangeException"> Le mot clé principal n'est pas reconnu </exception>
		*/
		public static Cle getClePrincipal(string s){
			string res = "";
			if (s.Length == 0) throw new ArgumentException("Le sort est vide.","s");
			for(int i = 0; i < s.Length; i++){
				if(s[i] == '(') break;
				else res += s[i];
			}
			try {
				Cle key = (Cle) Enum.Parse(typeof(Cle),res);
				return key;
			} catch (Exception e) {
				throw new ArgumentOutOfRangeException(e.Message);
			}
		}

		/// <summary> Extrait d'un sort ses arguments </summary>
		public static string[] extraireArguments(string sort){
			if(sort.Length != 0){
				List<string> res = new List<string>();
				int i = 0;
				int j = 0;
				int layer = -1;
				string spaceBuffer = "";
				res.Add("");
				while(i < sort.Length && sort[i] != '(' ) i++;
				for(; i < sort.Length;){
					while(i < sort.Length && !new char[3]{'(',',',')'}.Contains(sort[i]))
						if(sort[i] == ' '){
							spaceBuffer += sort[i++];
						} else {
							if(res[j] != "")
								res[j] += spaceBuffer;
							res[j] += sort[i++];
							spaceBuffer = "";
						}

					switch(sort[i++]){
						case '(':
							if(layer > -1) res[j] += '(';
							layer++;
							break;
						// -------------------
						case ',':
							if(layer == 0){
								res.Add("");
								j++;
							} else {
								res[j] += ',';
								while(i < sort.Length && sort[i] == ' ') i++;
							}
							break;
						// -------------------
						case ')':
							if(layer > 0){
								res[j] += ')';
								layer--;
							}
							break;
					}
					spaceBuffer = "";
				}
				return res.ToArray();
			} else {
				return new string[0];
			}
		}

		/// <summary> Récupère la liste des arguments par rapport à un mot </summary>
		public static Arg[] getListArguments(Enum c){
			ListeArguments.TryGetValue(c, out var result);
			if(result == null) throw new ArgumentNullException("La liste d'argument est vide.","result");
			return result;
		}

		/// <summary> Décompose un sort et le valide </summary>
		public static bool TOBECONTINUED(string sort){
			
			
			// On récupère la clé principale
			Cle mP = getClePrincipal(sort);
			// On récupère ses arguments
			Arg[] prms = getListArguments(mP);
			// On décompose le sort
			string[] dcpst = extraireArguments(sort);

			if(!valideSort(mP,prms,dcpst)) throw new Exception("Sort invalide.");
			return true;
		}

		public static bool constanteTrouve(string motTest){
			foreach(var enumItem in (Num[]) Enum.GetValues(typeof(Num))){
				ListeArguments.TryGetValue(enumItem, out var res);
				if(res == null) throw new ArgumentNullException();
				if(res.Length > 0){
					if(motTest.ToLower().StartsWith(enumItem.ToString().ToLower())) return true;
				} else {
					if(motTest.ToLower() == enumItem.ToString().ToLower()) return true;
				}
			}
			return false;
		}

		/// <summary> Valide un sort </summary>
		public static bool valideSort(Cle motPrincipal, Arg[] parametres, string[] decomposition){
			bool noArgumentsLeft = false;
			foreach (var item in parametres){
				if(decomposition.Length <= 0) noArgumentsLeft = true;
				switch(item){
					// Arguments obligatoires
					case Arg.Valeur :
					case Arg.Puissance :
					case Arg.Chance :
					case Arg.Taille :
					case Arg.Longueur :
						if(noArgumentsLeft) throw new ArgumentNullException();
						if(!constanteTrouve(decomposition[0])) return false;
						break;
					case Arg.Cible :
						if(noArgumentsLeft) throw new ArgumentNullException();
						 return false;
					case Arg.Forme :
						if(noArgumentsLeft) throw new ArgumentNullException();
						 return false;
					// Arguments optionnels
					case Arg.Distance :
					case Arg.Nombre :
					case Arg.Propagation :
						if(noArgumentsLeft) throw new ArgumentNullException();
						if(!constanteTrouve(decomposition[0])) return false;
						break;
					case Arg.Temps :

					case Arg.Addon :
					// Arguments spéciaux
					case Arg.MC :
					case Arg.MM :
					case Arg.ME :
						break;
					default:
						throw new Exception("Argument non reconnu. L'avez-vous ajouté à l'énumération \"Arg\" ?");
				}
			}
			return true;
		}

		/** <summary> Fonction importante : Calcul du coût du sort </summary>
			<param name="s"> L'écriture du sort </param>
		*/
		public static void Main() {
			// Récupération du sort
			string s = Spell();

			// Récupération du mot clé principal
			Cle mp = getClePrincipal(s);

			// Obtention des Arguments valides
			ListeArguments.TryGetValue(mp, out var result);

			// Extraire les arguments
			foreach (var item in extraireArguments(s))
			{
				Console.WriteLine(item);
			}
		}
	}
}