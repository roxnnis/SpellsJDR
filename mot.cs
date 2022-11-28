namespace Projet
{
	public class Mot
	{
		// Éléments
		public static byte[] Eau(byte puissance)
		{
			// Coût
			byte[] res = new byte[3]{0,0,0};
			res[0] = (byte)(puissance / 4);
			res[1] = (byte)(puissance / 5);
			res[2] = (byte)(puissance / 3);
			return res;
		}
		public static byte[] Eau(byte puissance, string temps)
		{
			byte[] res = new byte[3]{0,0,0};
			res = Eau(puissance);

			if(temps.StartsWith("constante"))
				res[2] += (byte) (M.constValue(temps)/4);
			else if(temps == "aura") res = M.Somme(res, Aura(puissance));
			else if(temps == "passif") res = M.Somme(res, Passif(puissance));
			else throw new Exception("TempsException : La variable de temps n'a pas été comprise.");
			return res;
		}

		public static byte[] Feu(string cible, byte puissance) { return new byte[3]; }
		public static byte[] Feu(string cible, byte puissance, string temps) { return new byte[3]; }
		public static byte[] Feu(string cible, byte puissance, string temps, string addon) { return new byte[3]; }
		public static byte[] Foudre(string cible, byte puissance) { return new byte[3]; }
		public static byte[] Foudre(string cible, byte puissance, string temps) { return new byte[3]; }
		public static byte[] Foudre(string cible, byte puissance, string temps, string addon) { return new byte[3]; }
		public static byte[] Glace(byte puissance) {
			// Coût
			byte[] res = new byte[3]{0,0,0};
			res[0] = (byte) (puissance / 4 + 1);
			res[1] = (byte) (puissance / 3 + 4);
			return res;
		}
		public static byte[] Glace(byte puissance, string temps) {
			// Coût
			byte[] res = new byte[3]{0,0,0};
			res = Glace(puissance);

			if(temps.StartsWith("constante")) {
				res[1] += (byte) (M.constValue(temps)/3);
				res[2] += (byte) (M.constValue(temps)/4);
			}
			else if(temps == "aura") res = M.Somme(res, Aura(puissance));
			else if(temps == "passif") res = M.Somme(res, Passif(puissance));
			else throw new Exception("TempsException : La variable de temps n'a pas été comprise.");
			return res;
		}
		public static byte[] Soin(string cible, byte puissance) { return new byte[3]; }
		public static byte[] Soin(string cible, byte puissance, string temps) { return new byte[3]; }
		public static byte[] Soin(string cible, byte puissance, string temps, string addon) { return new byte[3]; }
		public static byte[] Son(string cible, byte puissance) { return new byte[3]; }
		public static byte[] Son(string cible, byte puissance, byte chance) { return new byte[3]; }
		public static byte[] Son(string cible, byte puissance, byte chance, string addon) { return new byte[3]; }
		//public static byte[] Terre() { return new byte[3]; }
		//public static byte[] Vent() { return new byte[3]; }

		// Neutres
		public static byte[] Analyse() { return new byte[3]; }
		public static byte[] Armure() { return new byte[3]; }
		public static byte[] Esprit() { return new byte[3]; }
		public static byte[] Permeable() { return new byte[3]; }
		public static byte[] ViePondere() { return new byte[3]; }

		// Afflictions
		public static byte[] Brule() { return new byte[3]; }

		// Cible
		public static byte[] Contact(string cible, byte puissance) {
			string[] args = M.getArguments(cible);

			// Nombre d'arguments incorrect ?
			if(args.Length != 1) throw new Exception("ArgumentException : Contact ne contient pas le bon nombre d'arguments.");

			// Contact-ception ?
			if(args[0].StartsWith("contact")) throw new Exception("ContactException : Interdit de mettre un contact dans un contact.");

			// Calcul
			byte[] res = new byte[3]{1,0,1};
			res = M.Somme(res, M.coutCible(args[0], puissance));

			return res;
		}
		public static byte[] Entite() { return new byte[3] { 0, 1, 0 }; } // OK
		public static byte[] Objet(string cible) { return new byte[3]; }
		public static byte[] Projectile(string cible) { return new byte[3]; }
		public static byte[] Rayon(string cible) { // OK
			string[] args = M.getArguments(cible);

			// Nombre d'arguments incorrect ?
			if(args.Length < 1 || args.Length > 2) throw new Exception("ArgumentException : Rayon ne contient pas le bon nombre d'arguments.");
			byte[] res = new byte[3]{2,0,1};

			// Coût en mémoire des constantes dans les arguments de la cible
			res[1] += M.coutMemoireConst(args);

			// Coûts
			if(args.Length == 2)
				res[0] += (byte) (M.constValue(args[1])/10);
			res[2] += (byte) (M.constValue(args[0])/4);
			return res;
		}
		public static byte[] Soi() { return new byte[3] { 0, 1, 0 }; } // OK
		public static byte[] Zone(string cible, byte puissance) { // OK
			string[] args = M.getArguments(cible);

			// Nombre d'arguments incorrect ?
			if(args.Length < 1 || args.Length > 2) throw new Exception("ArgumentException : Zone ne contient pas le bon nombre d'arguments.");
			// Zone <= 5 ?
			if(M.constValue(args[0]) > 5) throw new Exception("ZoneTooLargeException : La taille max d'une zone est de 5");
			byte[] res = new byte[3]{0,1,0};

			// Coût en mémoire des constantes dans les arguments de la cible
			res[1] += M.coutMemoireConst(args);

			// Coûts
			res[0] = (byte) (M.constValue(args[0])/2 + (args.Length == 2 ? M.constValue(args[1])/3 : 0));
			res[2] = (byte) (M.constValue(args[0]) * (puissance / 5));
			return res;
		}

		// Forme
		public static byte[] Boule(string cible) { // OK
			string[] args = M.getArguments(cible);

			// Nombre d'arguments incorrect ?
			if(args.Length < 1 || args.Length > 2) throw new Exception("ArgumentException : Boule ne contient pas le bon nombre d'arguments.");

			// Calcul
			byte[] res = new byte[3]{0,1,0};

			// Coût en mémoire des constantes dans les arguments de la cible
			res[1] += M.coutMemoireConst(args);

			// Coût
			byte nb = (byte) (args.Length > 2 ? M.constValue(args[1]) : 1);
			res[0] = (byte) (nb * M.constValue(args[0]) / 5);
			res[2] = (byte) (nb - 1);

			return res;
		}
		public static byte[] Cage(string cible) { return new byte[3]; }
		public static byte[] Fleur(string cible) { return new byte[3]; }
		public static byte[] Fleche(string cible){ return new byte[3]; }
		public static byte[] Lame(string cible) { return new byte[3]; }
		public static byte[] Lance(string cible) { return new byte[3]; }
		public static byte[] Lierre(string cible) { return new byte[3]; }
		public static byte[] Ligne() { return new byte[3] { 1, 1, 0 }; } // OK
		
		

		// Temps

		public static byte[] Aura(byte puissance) {
			return new byte[3]{1,2,(byte)(puissance/2)};
		}
		public static byte[] Passif(byte puissance) { 
			return new byte[3]{2,2,(byte)(puissance/2 + 1)};
		}
	}
}