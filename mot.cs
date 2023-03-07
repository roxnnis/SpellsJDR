namespace Projet
{
	public class Mot
	{
		// --------------------------------------------------------------------------------
		// ----------------------------------- Éléments -----------------------------------
		// --------------------------------------------------------------------------------

		// ================================================================================
		// EAU
		// ================================================================================
		public static byte[] Eau(byte puissance)
		{
			// Coût
			byte[] res = new byte[3] { 0, 0, 0 };
			res[0] = (byte)(puissance / 4);
			res[1] = (byte)(puissance / 5);
			res[2] = (byte)(puissance / 3);
			return res;
		}
		public static byte[] Eau(byte puissance, string temps)
		{
			byte[] res = new byte[3] { 0, 0, 0 };
			res = Eau(puissance);

			// Temps
			byte indexTemps = M.selectTemps(temps);
			switch (indexTemps)
			{
				case 1:
					res[2] += (byte)(M.constValue(temps) / 4);
					break;
				case 2:
					res = M.Somme(res, Aura(puissance));
					break;
				case 3:
					res = M.Somme(res, Passif(puissance));
					break;
				default:
					throw new Exception("TempsException : La variable de temps n'a pas été comprise.");
			}
			return res;
		}

		// ================================================================================
		// FEU
		// ================================================================================
		public static byte[] Feu(byte puissance)
		{
			// Coût
			byte[] res = new byte[3] { 1, 1, 0 };
			res[0] += (byte)(puissance / 2);
			return res;
		}
		public static byte[] Feu(byte puissance, string temps)
		{
			byte[] res = Feu(puissance);

			// Temps
			byte indexTemps = M.selectTemps(temps);
			switch (indexTemps)
			{
				case 1:
					res[2] += (byte)(M.constValue(temps) / 5);
					break;
				case 2:
					res = M.Somme(res, Aura(puissance));
					break;
				case 3:
					res = M.Somme(res, Passif(puissance));
					break;
				default:
					throw new Exception("TempsException : La variable de temps n'a pas été comprise.");
			}
			return res;
		}
		// ================================================================================
		// FOUDRE
		// ================================================================================
		public static byte[] Foudre(byte puissance, byte nbAddons)
		{
			byte[] res = new byte[3] { 0, 1, 2 };
			res[0] = (byte) (puissance / 5);
			res[2] += (byte) (puissance + nbAddons);
			return res;
		}
		public static byte[] Foudre(byte puissance, byte nbAddons, string temps)
		{
			byte[] res = Foudre(puissance, nbAddons);

			// Temps
			byte indexTemps = M.selectTemps(temps);
			switch (indexTemps)
			{
				case 1:
					res[2] += (byte)(M.constValue(temps) / 4);
					break;
				case 2:
					res = M.Somme(res, Aura(puissance));
					break;
				case 3:
					res = M.Somme(res, Passif(puissance));
					break;
				default:
					throw new Exception("TempsException : La variable de temps n'a pas été comprise.");
			}
			return res;
		}
		// ================================================================================
		// GLACE
		// ================================================================================
		public static byte[] Glace(byte puissance)
		{
			// Coût
			byte[] res = new byte[3] { 1, 4, 0 };
			res[0] += (byte)(puissance / 4);
			res[1] += (byte)(puissance / 3);
			return res;
		}
		public static byte[] Glace(byte puissance, string temps)
		{
			// Coût
			byte[] res = Glace(puissance);

			if (temps.StartsWith("constante"))
			{
				res[1] += (byte)(M.constValue(temps) / 3);
				res[2] += (byte)(M.constValue(temps) / 4);
			}
			else if (temps == "aura") res = M.Somme(res, Aura(puissance));
			else if (temps == "passif") res = M.Somme(res, Passif(puissance));
			else throw new Exception("TempsException : La variable de temps n'a pas été comprise.");
			return res;
		}
		// ================================================================================
		// SOIN
		// ================================================================================
		public static byte[] Soin(byte puissance)
		{
			byte[] res = new byte[3] { 0, 1, 0 };
			res[0] = (byte)(puissance < 1 ? 0 : puissance < 5 ? 1 : puissance < 8 ? 2 : 3);
			res[2] = (byte)puissance;
			return res;
		}
		public static byte[] Soin(byte puissance, string temps)
		{
			byte[] res = Soin(puissance);

			byte indexTemps = M.selectTemps(temps);
			switch (indexTemps)
			{
				case 1:
					res[2] += (byte)(M.constValue(temps));
					break;
				case 2:
					res = M.Somme(res, Aura(puissance));
					break;
				case 3:
					res = M.Somme(res, Passif(puissance));
					break;
				default:
					throw new Exception("TempsException : La variable de temps n'a pas été comprise.");
			}
			return res;
		}
		// ================================================================================
		// TERRE
		// ================================================================================
		public static byte[] Terre(byte puissance)
		{
			byte[] res = new byte[3] { 0, 2, 1 };
			res[0] = (byte)(puissance / 3);
			res[1] += (byte)(puissance / 5);
			res[2] += (byte)(puissance / 4);
			return res;
		}
		public static byte[] Terre(byte puissance, string temps)
		{
			byte[] res = Terre(puissance);

			byte indexTemps = M.selectTemps(temps);
			switch (indexTemps)
			{
				case 1:
					res[1] += (byte)(M.constValue(temps) /4);
					res[2] += (byte)((puissance /4) * (M.constValue(temps)));
					break;
				case 2:
					res = M.Somme(res, Aura(puissance));
					break;
				case 3:
					res = M.Somme(res, Passif(puissance));
					break;
				default:
					throw new Exception("TempsException : La variable de temps n'a pas été comprise.");
			}
			return res;
		}
		// ================================================================================
		// VENT
		// ================================================================================
		public static byte[] Vent(byte puissance) {
			byte[] res = new byte[3] { 1, 1, 0 };
			res[0] += (byte) (puissance/2);
			res[2] += (byte) (puissance/3);
			return res;
		}
		public static byte[] Vent(byte puissance, string temps) {
			byte[] res = Vent(puissance);

			byte indexTemps = M.selectTemps(temps);
			switch (indexTemps)
			{
				case 1:
					res[0] += (byte) (M.constValue(temps)/3);
					res[2] = (byte) ((puissance + M.constValue(temps))/3);
					break;
				case 2:
					res = M.Somme(res, Aura(puissance));
					break;
				case 3:
					res = M.Somme(res, Passif(puissance));
					break;
				default:
					throw new Exception("TempsException : La variable de temps n'a pas été comprise.");
			}
			return res;
		}

		// --------------------------------------------------------------------------------
		// ------------------------------------ Neutre ------------------------------------
		// --------------------------------------------------------------------------------

		// ================================================================================
		// ANALYSE
		// ================================================================================
		public static byte[] Analyse() { return new byte[3] { 0, 2, 1 }; }
		// ================================================================================
		// ARMURE & ESPRIT
		// ================================================================================
		public static byte[] Armuresprit(byte puissance)
		{
			byte[] res = new byte[3] { 1, 2, 0 };
			res[0] += (byte)(puissance / 4);
			return res;
		}
		public static byte[] Armuresprit(byte puissance, string temps)
		{
			byte[] res = Armuresprit(puissance);

			byte indexTemps = M.selectTemps(temps);
			switch (indexTemps)
			{
				case 1:
					res[2] = (byte)(M.constValue(temps) / 6);
					break;
				case 2:
					res = M.Somme(res, Aura(puissance));
					break;
				case 3:
					res = M.Somme(res, Passif(puissance));
					break;
				default:
					throw new Exception("TempsException : La variable de temps n'a pas été comprise.");
			}
			return res;
		}
		// ================================================================================
		// Perméable
		// ================================================================================
		public static byte[] Permeable(byte puissance, byte nbAddons)
		{
			byte[] res = new byte[3] { 0, 0, 0 };
			res[0] = (byte)(Math.Pow(puissance, 3));
			res[1] = (byte)(puissance * 2 + nbAddons);
			res[2] = (byte)(res[1] * 2);
			return res;
		}
		// ================================================================================
		// VIE PONDÉRÉ
		// ================================================================================
		public static byte[] ViePondere(byte puissance)
		{
			Console.WriteLine("Attention : Le coût en MC est lié à la statistique de vie de la cible. |Puissance - pv:Cible| /2 est à ajouter pour calculer le coût réel.");
			byte[] res = new byte[3] { 2, 10, 0 };
			res[2] = puissance;
			return res;
		}
		public static byte[] ViePondere(byte puissance, string temps)
		{
			byte[] res = ViePondere(puissance);

			byte indexTemps = M.selectTemps(temps);
			switch (indexTemps)
			{
				case 1:
					res[2] += (byte)(M.constValue(temps));
					break;
				case 2:
					res = M.Somme(res, Aura(puissance));
					break;
				case 3:
					res = M.Somme(res, Passif(puissance));
					break;
				default:
					throw new Exception("TempsException : La variable de temps n'a pas été comprise.");
			}
			return res;
		}

		// --------------------------------------------------------------------------------
		// ---------------------------------- Affliction ----------------------------------
		// --------------------------------------------------------------------------------

		// ================================================================================
		// BRULE
		// ================================================================================
		public static byte[] Brule(byte puissance, byte chance)
		{
			byte[] res = new byte[3] { 1, 1, 0 };
			res[0] += (byte)(puissance / 4 + chance);
			res[2] += (byte)(puissance / 2);
			return res;
		}
		// ================================================================================
		// PARALYSE
		// ================================================================================
		public static byte[] Paralyse(byte puissance, byte chance)
		{
			byte[] res = new byte[3] {0,1,3};
			res[0] = (byte) (chance/3);
			res[2] += (byte) (puissance + chance/3);
			return res;
		}
		// ================================================================================
		// SAIGNE
		// ================================================================================
		public static byte[] Saigne(byte puissance, byte chance)
		{
			byte[] res = new byte[3] {1,3,0};
			res[0] += (byte) (chance/2 + puissance/4);
			res[2] += (byte) (puissance/2);
			return res;
		}
		// ================================================================================
		// SOIN STATUT
		// ================================================================================
		public static byte[] SoinStatut(byte chance){
			byte[] res = new byte[3]{1,2,0};
			res[0] += (byte) (chance/2);
			res[2] = (byte) (chance*2);
			return res;
		}
		// ================================================================================
		// SON
		// ================================================================================
		public static byte[] Son(byte puissance, byte chance)
		{
			byte[] res = new byte[3] { 1, 2, 0 };
			res[0] += (byte) (puissance + chance);
			res[2] += (byte)(chance / 3);
			return res;
		}

		// --------------------------------------------------------------------------------
		// --------------------------------- Multi-Cibles ---------------------------------
		// --------------------------------------------------------------------------------
		// ================================================================================
		// LUMIÈRE
		// ================================================================================
		public static byte[] Lumiere(byte puissance)
		{
			byte[] res = new byte[3] { 0, 0, 0 };
			res[0] = (byte)(puissance / 3);
			res[1] = (byte)(puissance / 6);
			res[2] = (byte)(puissance / 2);
			return res;
		}
		public static byte[] Lumiere(byte puissance, string temps)
		{
			byte[] res = Lumiere(puissance);

			byte indexTemps = M.selectTemps(temps);
			switch (indexTemps)
			{
				case 1:
					res[1] += (byte)(M.constValue(temps) / 5);
					res[2] = (byte)((puissance + M.constValue(temps)) / 2);
					break;
				case 2:
					res = M.Somme(res, Aura(puissance));
					break;
				case 3:
					res = M.Somme(res, Passif(puissance));
					break;
				default:
					throw new Exception("TempsException : La variable de temps n'a pas été comprise.");
			}
			return res;
		}
		public static byte[] Lumiere(byte puissance, string temps, byte nbAddons)
		{
			byte[] res = Lumiere(puissance, temps);
			res[2] += nbAddons;
			return res;
		}
		

		// --------------------------------------------------------------------------------
		// ------------------------------------ Cibles ------------------------------------
		// --------------------------------------------------------------------------------
		public static byte[] Contact(string cible, byte puissance)
		{
			string[] args = M.getArguments(cible);

			// Nombre d'arguments incorrect ?
			if (args.Length != 1) throw new Exception("ArgumentException : Contact ne contient pas le bon nombre d'arguments.");

			// Contact-ception ?
			if (args[0].StartsWith("contact")) throw new Exception("ContactException : Interdit de mettre un contact dans un contact.");

			// Calcul
			byte[] res = new byte[3] { 1, 0, 1 };
			res = M.Somme(res, M.coutCible(args[0], puissance));

			return res;
		}
		public static byte[] Entite() { return new byte[3] { 0, 1, 0 }; }
		public static byte[] Objet(string cible)
		{
			string[] args = M.getArguments(cible);

			// Nombre d'arguments incorrect ?
			if (args.Length < 1 || args.Length > 2) throw new Exception("ArgumentException : Objet ne contient pas le bon nombre d'arguments.");

			// Calcul
			byte[] res = new byte[3] { 1, 1, 0 };

			// Coût en mémoire des constantes dans les arguments de la cible
			res[1] += M.coutMemoireConst(args);

			if (args.Length == 2) res[2] = (byte)(M.constValue(args[1]) / 3);
			res = M.Somme(res, M.coutForme(args[0]));
			return res;
		}
		public static byte[] Projectile(string cible)
		{
			string[] args = M.getArguments(cible);

			// Nombre d'arguments incorrect ?
			if (args.Length < 1 || args.Length > 2) throw new Exception("ArgumentException : Objet ne contient pas le bon nombre d'arguments.");

			// Calcul
			byte[] res = new byte[3] { 1, 1, 0 };

			// Coût en mémoire des constantes dans les arguments de la cible
			res[1] += M.coutMemoireConst(args);

			if (args.Length == 2) res[2] = (byte)(M.constValue(args[1]) / 3);
			res = M.Somme(res, M.coutForme(args[0]));

			return res;
		}
		public static byte[] Rayon(string cible)
		{
			string[] args = M.getArguments(cible);

			// Nombre d'arguments incorrect ?
			if (args.Length < 1 || args.Length > 2) throw new Exception("ArgumentException : Rayon ne contient pas le bon nombre d'arguments.");
			byte[] res = new byte[3] { 2, 0, 1 };

			// Coût en mémoire des constantes dans les arguments de la cible
			res[1] += M.coutMemoireConst(args);

			// Coûts
			if (args.Length == 2) res[0] += (byte)(M.constValue(args[0]) / 10);
			res[2] += (byte)(M.constValue(args[1]) / 4);
			return res;
		}
		public static byte[] Soi() { return new byte[3] { 0, 1, 0 }; }
		public static byte[] Zone(string cible, byte puissance)
		{
			string[] args = M.getArguments(cible);

			// Nombre d'arguments incorrect ?
			if (args.Length < 1 || args.Length > 2) throw new Exception("ArgumentException : Zone ne contient pas le bon nombre d'arguments.");
			// Zone <= 5 ?
			if (M.constValue(args[0]) > 5) throw new Exception("ZoneTooLargeException : La taille max d'une zone est de 5");
			byte[] res = new byte[3] { 0, 1, 0 };

			// Coût en mémoire des constantes dans les arguments de la cible
			res[1] += M.coutMemoireConst(args);

			// Coûts
			res[0] = (byte)(M.constValue(args[0]) / 2 + (args.Length == 2 ? M.constValue(args[1]) / 3 : 0));
			res[2] = (byte)(M.constValue(args[0]) * (puissance / 5));
			return res;
		}

		// --------------------------------------------------------------------------------
		// ------------------------------------ Formes ------------------------------------
		// --------------------------------------------------------------------------------
		public static byte[] Boule(string forme)
		{
			string[] args = M.getArguments(forme);

			// Nombre d'arguments incorrect ?
			if (args.Length < 1 || args.Length > 2) throw new Exception("ArgumentException : Boule ne contient pas le bon nombre d'arguments.");

			// Calcul
			byte[] res = new byte[3] { 0, 1, 0 };

			// Coût en mémoire des constantes dans les arguments de la forme
			res[1] += M.coutMemoireConst(args);

			// Coût
			byte nb = (byte)(args.Length > 1 ? M.constValue(args[1]) : 1);
			res[0] = (byte)(nb * M.constValue(args[0]) / 5);
			res[2] = (byte)(nb - 1);

			return res;
		}
		public static byte[] Cage(string forme)
		{
			string[] args = M.getArguments(forme);

			// Nombre d'arguments incorrect ?
			if (args.Length != 1) throw new Exception("ArgumentException : Cage ne contient pas le bon nombre d'arguments.");

			// Calcul
			byte[] res = new byte[3] { 0, 4, 2 };

			// Coût en mémoire des constantes dans les arguments de la forme
			res[1] += M.coutMemoireConst(args);

			// Coût
			res[0] = (byte)(M.constValue(args[0]) / 4);

			return res;
		}
		public static byte[] Fleur(string forme)
		{
			string[] args = M.getArguments(forme);

			// Nombre d'arguments incorrect ?
			if (args.Length < 1 || args.Length > 2) throw new Exception("ArgumentException : Fleur ne contient pas le bon nombre d'arguments.");

			// Calcul
			byte[] res = new byte[3] { 0, 1, 0 };

			// Coût en mémoire des constantes dans les arguments de la forme
			res[1] += M.coutMemoireConst(args);

			// Coût
			byte nb = (byte)(args.Length > 1 ? M.constValue(args[1]) : 1);
			res[0] = (byte)(nb * M.constValue(args[0]) / 3);
			res[2] = (byte)(2 * (nb - 1));

			return res;
		}
		public static byte[] Fleche(string forme)
		{
			string[] args = M.getArguments(forme);

			// Nombre d'arguments incorrect ?
			if (args.Length < 1 || args.Length > 2) throw new Exception("ArgumentException : Flèche ne contient pas le bon nombre d'arguments.");

			// Calcul
			byte[] res = new byte[3] { 0, 2, 0 };

			// Coût en mémoire des constantes dans les arguments de la forme
			res[1] += M.coutMemoireConst(args);

			// Coût
			byte nb = (byte)(args.Length > 1 ? M.constValue(args[1]) : 1);
			res[0] = (byte)(nb * M.constValue(args[0]) / 4);
			res[2] = (byte)(2 * (nb - 1));
			return res;
		}
		public static byte[] Lame(string forme)
		{
			string[] args = M.getArguments(forme);

			// Nombre d'arguments incorrect ?
			if (args.Length < 1 || args.Length > 2) throw new Exception("ArgumentException : Lame ne contient pas le bon nombre d'arguments.");

			// Calcul
			byte[] res = new byte[3] { 0, 4, 0 };

			// Coût en mémoire des constantes dans les arguments de la forme
			res[1] += M.coutMemoireConst(args);

			// Coût
			byte nb = (byte)(args.Length > 1 ? M.constValue(args[1]) : 1);
			res[0] = (byte)(nb * M.constValue(args[0]) / 3);
			res[2] = (byte)(3 * (nb - 1));
			return res;
		}
		public static byte[] Lance(string forme)
		{
			string[] args = M.getArguments(forme);

			// Nombre d'arguments incorrect ?
			if (args.Length < 1 || args.Length > 2) throw new Exception("ArgumentException : Lance ne contient pas le bon nombre d'arguments.");

			// Calcul
			byte[] res = new byte[3] { 0, 1, 0 };

			// Coût en mémoire des constantes dans les arguments de la forme
			res[1] += M.coutMemoireConst(args);

			// Coût
			byte nb = (byte)(args.Length > 1 ? M.constValue(args[1]) : 1);
			res[0] = (byte)(nb * M.constValue(args[0]) / 3);
			res[2] = (byte)(3 * (nb - 1));
			return res;
		}
		public static byte[] Lierre(string forme)
		{
			string[] args = M.getArguments(forme);

			// Nombre d'arguments incorrect ?
			if (args.Length < 1 || args.Length > 2) throw new Exception("ArgumentException : Lierre ne contient pas le bon nombre d'arguments.");

			// Calcul
			byte[] res = new byte[3] { 0, 1, 0 };

			// Coût en mémoire des constantes dans les arguments de la forme
			res[1] += M.coutMemoireConst(args);

			// Coût
			res[0] = (byte)(M.constValue(args[0]) / 2);
			res[1] += (byte)(args.Length > 1 ? M.constValue(args[1]) / 2 : 0);
			res[2] = (byte)(args.Length > 1 ? M.constValue(args[1]) / 2 : 0);
			return res;
		}
		public static byte[] Ligne() { return new byte[3] { 1, 1, 0 }; }

		// --------------------------------------------------------------------------------
		// ----------------------------------- Le Temps -----------------------------------
		// --------------------------------------------------------------------------------
		public static byte[] Aura(byte puissance)
		{
			return new byte[3] { 1, 2, (byte)(puissance / 2) };
		}
		public static byte[] Passif(byte puissance)
		{
			return new byte[3] { 2, 2, (byte)(puissance / 2 + 1) };
		}
	}
}