
public class MainClass
{
	public static void Main()
	{
		// Demande du sort
		Console.WriteLine("Écrivez votre sort : ");
		var sort = Console.ReadLine();
		while (sort == "" || sort == null) {
			Console.WriteLine("Veuillez entrer un sort : ");
			sort = Console.ReadLine();
		}

		// Récupération des composants
		string[] fullWrite = flatSpell(sort);
		string[] arguments = getArguments(sort);
		Console.WriteLine("Full Write :\n"); afficher(fullWrite);
		Console.WriteLine("Mot clé principal : " + getMotPrincipal(sort));
		Console.WriteLine("Arguments :"); afficher(arguments);
	}

	/**	<summary> Sépare chaque mot du sort en blocs contenant le mot et ses arguments</summary>
		<param name="e">L'écriture du sort</param>
		<returns>La liste des mots-clés avec leurs arguments composant le sort</returns>
	*/
	public static string[] flatSpell(string e){
		Dictionary<int,string> words = new Dictionary<int, string>(){{0,""}}; // Mots trouvés
		List<int> writable = new List<int>(){0}; // Indice des mots où l'on peut écrire (Non fermé par une ')')

		for(int i = 0; i < e.Length; i++){
			bool addWritable = false;
			switch(e[i]){
				// Cas : Ajout d'un argument
				case ',': 
					writable.RemoveAt(writable.Count - 1); // Dernier mot de la liste interdit à l'écriture
					goto LPAREN;
				
				// Cas : Déclaration d'arguments
				case '(':
					LPAREN:
					words.Add(words.Count,"");
					addWritable = true;
					goto default;
				
				// Cas : Fermeture d'arguments
				case ')':
					writable.RemoveAt(writable.Count - 1); // Dernier mot de la liste interdit à l'écriture
					goto default;
				
				// Cas par défaut : Ecriture d'une lettre
				default:
					foreach(KeyValuePair<int,string> word in words){
						if(writable.Contains(word.Key) && !(words[word.Key] == "" && e[i] == ' ')) // Vérifie si le mot peut être complété (Droit d'écriture)
						words[word.Key] += e[i]; // Ajoute la lettre à la suite
					}
					if(addWritable) writable.Add(words.Count-1);
					break;
			}
			
		}
		string[] res = new string[words.Count + 1]; // Résultat de la fonction
		foreach(KeyValuePair<int,string> word in words){
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
		while(s.Length > i && s[i] != '(') res += s[i++];
		return res;
	}

	/**	<summary> Obtenir les arguments du mot-clé principal </summary>
		<param name="s"> L'écriture du sort</param>
		<returns>La liste d'arguments du mot-clé principal</returns>
	*/
	public static string[] getArguments(string s){
		string[] e = flatSpell(s);
		List<string> list = new List<string>(); // Résultat sous forme de liste
		for(int i = 1; i < e.Length - 1; i++){
			string current = e[i];
			list.Add(current);
			for(int j = 0; j < current.Length; j++){
				if(current[j] == '(' || current[j] == ','){
					i++;
				}
			}
		}
		string[] res = list.ToArray();
		return res;
	}
	public static void afficher(string[] list){
		foreach(string elem in list){
			Console.WriteLine(elem);
		}
	}

	public static int[] Somme(int[] a, int[] b){
		if(a.Length != b.Length) throw new Exception();
		int[] res = new int[a.Length];
		for(int r = 0; r< a.Length; r++){
			res[r] = a[r] + b[r];
		}
		return res;
	}
	public static int[] calculCout(string s){
		string motPrincipal = getMotPrincipal(s);
		return new int[0];
	}
}