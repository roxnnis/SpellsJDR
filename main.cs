namespace Projet
{
	public class M
	{
		private Mot mot = new Mot();
		public static void Main()
		{
			// Demande du sort
			Console.WriteLine("Écrivez votre sort : ");
			var sort = Console.ReadLine();
			while (sort == "" || sort == null)
			{
				Console.WriteLine("Veuillez entrer un sort : ");
				sort = Console.ReadLine();
			}
			Console.WriteLine();

			// Récupération des composants
			afficher(calculCout(sort));
		}

		/**	<summary> Sépare chaque mot du sort en blocs contenant le mot et ses arguments</summary>
			<param name="e">L'écriture du sort</param>
			<returns>La liste des mots-clés avec leurs arguments composant le sort</returns>
		*/
		public static string[] flatSpell(string e)
		{
			Dictionary<int, string> words = new Dictionary<int, string>() { { 0, "" } }; // Mots trouvés
			List<int> writable = new List<int>() { 0 }; // Indice des mots où l'on peut écrire (Non fermé par une ')')

			for (int i = 0; i < e.Length; i++)
			{
				bool addWritable = false;
				switch (e[i])
				{
					// Cas : Ajout d'un argument
					case ',':
						writable.RemoveAt(writable.Count - 1); // Dernier mot de la liste interdit à l'écriture
						goto LPAREN;

					// Cas : Déclaration d'arguments
					case '(':
					LPAREN:
						words.Add(words.Count, "");
						addWritable = true;
						goto default;

					// Cas : Fermeture d'arguments
					case ')':
						writable.RemoveAt(writable.Count - 1); // Dernier mot de la liste interdit à l'écriture
						goto default;

					// Cas par défaut : Ecriture d'une lettre
					default:
						foreach (KeyValuePair<int, string> word in words)
						{
							if (writable.Contains(word.Key) && !((words[word.Key] == "") && e[i] == ' ')) // Vérifie si le mot peut être complété (Droit d'écriture)
								words[word.Key] += e[i]; // Ajoute la lettre à la suite
						}
						if (addWritable) writable.Add(words.Count - 1);
						break;
				}

			}
			string[] res = new string[words.Count]; // Résultat de la fonction
			foreach (KeyValuePair<int, string> word in words)
			{
				res[word.Key] = word.Value;
			}
			return res;
		}

		/**	<summary> Obtenir le mot-clé principal du sort </summary>
			<param name="s"> L'écriture du sort </param>
			<returns> Le mot-clé principal </returns>
		*/
		public static string getMotPrincipal(string s){
			string res = ""; int i = 0;
			while (s.Length > i && s[i] != '(') res += s[i++];
			return res;
		}

		/**	<summary> Obtenir les arguments du mot-clé principal </summary>
			<param name="s"> L'écriture du sort</param>
			<returns>La liste d'arguments du mot-clé principal</returns>
		*/
		public static string[] getArguments(string s){
			string[] e = flatSpell(s);
			List<string> list = new List<string>(); // Résultat sous forme de liste
			for (int i = 1; i < e.Length; i++)
			{
				string current = e[i];
				list.Add(current);
				for (int j = 0; j < current.Length; j++)
				{
					if (current[j] == '(' || current[j] == ',')
					{
						i++;
					}
				}
				
			}
			string[] res = list.ToArray();
			return res;
		}
		public static void afficher<T>(T[] list) where T : IComparable<T>{
			// Personnel : Afficher le coût d'un sort
			if(list.GetType() == System.Type.GetType("System.Byte") && list.Length == 3){
				Console.WriteLine("MC : " + list[0]);
				Console.WriteLine("MM : " + list[1]);
				Console.WriteLine("ME : " + list[2]);
			} 
			// Afficher les autres tableaux
			else foreach (T elem in list) {
				Console.WriteLine(elem);
			}
		}
		public static string[] ToLower(string[] a){
			List<string> res = new List<string>();
			foreach(string elem in a){
				res.Add(elem.ToLower());
			}
			return res.ToArray();
		}
		public static byte[] Somme(byte[] a, byte[] b){
			byte[] res = new byte[a.Length];
			for (byte r = 0; r < a.Length; r++)
			{
				res[r] = (byte) (a[r] + b[r]);
			}
			return res;
		}
		public static byte constValue(string c){
			return byte.Parse(c.Split(" ",2)[1]);
		}
		public static byte selectCible(string cible){
			if(cible.StartsWith("contact(")) return 1;
			if(cible == "entite") return 2;
			if(cible.StartsWith("objet(")) return 3;
			if(cible.StartsWith("projectile(")) return 4;
			if(cible == "soi") return 5;
			if(cible.StartsWith("zone(")) return 6;
			else return 0;
		}
		/** <summary> Coût en mémoire des constantes LIBRES </summary>
			<param name="args"> Arguments du sort </param>
		*/
		public static byte coutMemoireConst(string[] args){
			byte res = 0;
			foreach(string argument in args){
				if(argument.StartsWith("constante")){
					if(constValue(argument)< 0) throw new Exception("NegativeConstant");
					res += (byte) (constValue(argument) / 5);
				}
			}
			return res;
		}
		public static byte[] calculCout(string s){
			string motPrincipal = getMotPrincipal(s).ToLower();
			string[] arguments = ToLower(getArguments(s));

			int nbArgs = arguments.Count();
			byte[] res = new byte[3]{0, 0, 0};

			// Coût en mémoire des constantes LIBRES (N'est pas enclavé par un mot clé)
			res[1] = coutMemoireConst(arguments);

			// Cout element
			switch (motPrincipal)
			{
				// ======== Soin
				case "soin":
					if(nbArgs < 2) goto Error; // Manque des arguments
					if(constValue(arguments[1]) > 10) goto Error;
					else goto Addon;
				// ======== Eau
				case "eau":
					// Nombre d'arguments incorrect ?
					if(nbArgs < 2 || nbArgs > 4) goto Error;

					// Prix du mot-clé
					res = Somme(res, Mot.Eau(constValue(arguments[1])));

					// Temps non vide
					if(nbArgs > 2) {
						if(arguments[2].StartsWith("constante")) res = Somme(res, new byte[3]{0,0,(byte) (constValue(arguments[2])/4)});
						else if(arguments[2] == "aura") res = Somme(res, Mot.Aura(arguments[1]));
						else if(arguments[2] == "passif") res = Somme(res, Mot.Passif(arguments[1]));
						else goto Error;
					}

					// Mot clé supplémentaire ?
					goto Addon;
				// ======== Feu
				case "feu":
					if(nbArgs < 2) goto Error; // Manque des arguments
					goto Addon;
				// ======== Foudre
				case "foudre":
					if(nbArgs < 2) goto Error; // Manque des arguments
					goto Addon;
				// ======== Glace
				case "glace":
					if(nbArgs < 2) goto Error; // Manque des arguments
					goto Addon;
				
				// ======== Son
				case "son": break;
				// ======== Analyse
				case "analyse": break;
				// ======== Armure
				case "armure": break;
				// ======== Esprit
				case "esprit": break;
				// ======== Perméable
				case "permeable": break;
				// ======== Vie Pondéré
				case "viepondere": break;
				// ======== Brule
				case "brule": break;

				default:
					Error: // ERREUR !
					Console.WriteLine("Le sort \"" + s + "\" n'a pas été compris.");
					throw new Exception("Unhandled Spell");

					// ======== Addon ?
					Addon:
					if(nbArgs > 3) {
						res = Somme(res, calculCout(arguments[3]));
					}
					break;
			}
			
			// Cout Cible
			res = Somme(res, coutCible(arguments[0], (byte) constValue(arguments[1])));

			return res;
		}

		public static byte[] coutCible(string cible, byte puissance){
			byte[] res = new byte[3]{0,0,0};
			byte indexCible = selectCible(cible);

			switch(indexCible){
				case 1: // Contact
					res = Somme(res, Mot.Contact(cible, puissance));
					break;
				case 2: // Entité
					res = Somme(res, Mot.Entite());
					break;
				case 3: // Objet
					res = Somme(res, Mot.Objet(cible));
					break;
				case 4: // Projectile
					res = Somme(res, Mot.Projectile(cible));
					break;
				case 5: // Soi
					res = Somme(res, Mot.Soi());
					break;
				case 6: // Zone
					res = Somme(res, Mot.Zone(cible, puissance));
					break;
				default:
					throw new Exception("CibleUnknown");
			}
			return res;
		}
	}
}