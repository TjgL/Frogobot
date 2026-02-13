namespace Frogobot.Core.Utils;

// TODO: refactor in data oriented config
public static class RandomResponses
{
	private static readonly string[] SlimeResponses =
	[
		"{0} a mentionné *Possesslime* ! {1}\nIl a seulement **{2} Possess-Points**.",
		"Salution {0} ! Savais tu que *Possesslime* est un jeu où le protagoniste est un Slime ? Voila ton point {1} ({2} au total).",
		"*Possesslime* ? Le fameux jeu où des slimes peuvent prendre le contrôle d'autres créatures ? **J-A-D-O-R-E** ! Tiens voila un {1} pour l'avoir mentionné ({2} au total).",
		"Oh non {0} as mentionné le jeu *Possesslime* ! Tu as maintenant {1} Possess-Points (et {2} au total)!",
		"Je suis tout à fait d'accord avec {0} ! Tien c'est cadeau : {1} ({2} au total)",
		"Je ne suis qu'un robot, mais là {0} je suis pas d'accord avec ce que tu dit sur *Possesslime* ! Pour la peine : {1} ({2} au total)",
		"Quelle opinion désastreuse que tu viens de dire {0}. Pour la peine prend ça {1} ({2} au total).",
		"Hmm actually 🤓, dans *Possesslime* {0}, tu aurais pas gagné de {1}, et encore moins {2} !",
		"{0} vient d'invoquer *Possesslime*… et ça mérite clairement un {1}. Total : {2}.",
		"Alerte slime /!\\ {0} a prononcé *Possesslime* ! Je lui octroie donc un {1} (ça en fait {2}).",
		"Serait-ce *Possesslime* que j'ai lu ?! {0}, prends ton {1} et file : {2} points au compteur.",
		"C'est officiel : {0} a relancé le débat *Possesslime*. Verdict : {1}. Total actuel : {2}.",
		"Le slime intérieur de {0} s'est réveillé. Voilà ta nouvelle possession {1}, ça en fait déjà {2} !",
		"{0}, ton obsession pour *Possesslime* est pour le moins… terrifiant. Cadeau : {1} (total : {2}).",
		"Ok {0}, tu l'as encore mentionné. Tu sais que t'est vraiment collant avec ce sujet ? Allez prend ta médaille {1}, ça en fera {2}.",
		"Breaking news : {0} a lâché un gros *Possesslime*. Pour la peine il reçoit 1 {1}. Total : {2}.",
		"Je note dans mes registres : {0} a *encore* parlé de *Possesslime*. Voilà un (1) {1}. Nouveau total : {2}.",
		"{0} a ouvert la boîte de Pandore version slime. Prends ton {1} (total {2}).",
		"C'est vraiment une secte votre truc de slime là… {0} a dit *Possesslime*. Voilà {1}. Total : {2}.",
		"Analyse terminée : message hautement slime-compatible. Coupable : {0}. Gain : {1}. Total : {2}.",
		"{0} a appuyé sur le bouton *Possesslime*… encore. Voilà {1} (total {2}).",
		"Je valide la référence, {0}. Récompense : {1}. Compteur : {2}.",
		"Oh non ! {0} d'où sort tu ce *Posseslime* ?! Pas de ton... j'espère... parce que {1} en plus ça en fait un total de {2} à l'intérieur",
		"{0} collectionne les posseslimes {1} comme les MST franchement psartek ça fait en {2}",
	];
	
	/// <summary> Get a random response from the list of Possesslime responses. </summary>
	/// <param name="args"> User, emoji, total points </param>
	/// <returns> A random response from the list of Possesslime responses. </returns>
	public static string GetRandomPossesslimeResponse(params object[] args)
	{
		if (args.Length != 3)
			throw new ArgumentException("Invalid number of arguments. Expected 3: user, emoji, total points");
		
		var response = SlimeResponses[new Random().Next(0, SlimeResponses.Length)];
		return string.Format(response, args);
	}
}
