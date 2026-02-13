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
		"{0} s'est fait posseslimer avec succès, il est tout posseslimé par {1} posseslime posseslimant les {2} posseslime déjà posseslimés !",
		"{0} a récupéré son due qui lui est dut : {1} possess-point pour ses {2} posseslime.",
		"My friend, {0}, you have met a terrible, terrible demise. But, uh, y'know, I-I don't feel too bad about it. After all, if... if it weren't from me, it would've just been from someone else, y'know? I guess what I'm trying to say is, life... life goes on. W-well, from—for everyone else, life goes on. Not... not for you. You're... you're {1} posseslimed {1}. But that's neither here nor there. It reminds me of one summer day in the park. I was having just a delightful picnic with my good friend Frankulin. And I said to him, I said, \"Frankulin, I... I have a story.\" And he said to me, \"What's the significance of the story?\" And... I said to him, \"Frankulin, not every story has to have significance, y'know? Sometimes, a... y'know, sometimes, a story's just a story. You try to read into every little thing, and find meaning in everything anyone says, you'll just drive yourself crazy. Had a friend do it once. Wasn't pretty. We talked about it for {2} years. And then not only that, but... you'll likely end up believing something you shouldn't believe, thinking something you shouldn't think, o-o-or assuming something you shouldn't assume. Y'know? Sometimes,\" I said, \"A story is-is just a story, so just be quiet for one second of your life and eat your sandwich, okay?\" Of course, it was only then I'd realized I'd made sandwiches, and... poor Frankulin was having such difficulty eating it! Frogs have those clumsy hands, y'know? Actually, I-I suppose that's the problem. They don't have hands at all, do they? They're f—they're all feet! And I-I couldn't imagine someone asking me to eat a sandwich with my feet.",
		"Oh non ! {0} d'où sort tu ce *Posseslime* ?! Pas de ton... j'espère... parce que {1} en plus ça en fait un total de {2} à l'intérieur",
		"{0} collectionne les posseslimes {1} comme les MST franchement psartek ça fait en {2}",
		"{0} à ouvert un portail vers la dimension malade et tordue du Sébastien Maléfique. Il s'en sort tout juste avec un posseslime {1}. En voilà {2} dans sa besace",
		"{0} à l'honneur de recevoir un posseslime {1} de la part de l'**UNIQUE** Sébastien Pautot en personne ! Le voilà en possession de {2} posseslime jusqu'à aujourd'hui",
		"Tu étais l'Élu, c'était toi {0}. La prophétie voulait que tu détruises les slimes pas que tu les possèdes! Tu devais amener l'équilibre dans le slime, pas te l'accaparer et en faire le posseslime {1}! Nous étions comme des frères. Je t'aimais {0} en plus tu en as {2} maintenant c'est bien trop pour ce monde",
		"Nous vivons dans un slime{1} et en plus {0} en détiens {2}",
		"{0} arrète d'être un PNJ steuplait ! T'as mentionné posslime{1} {2} fois !",
		"Posseslime{1} à infiltré les serveurs de MyGES et a accordé un posseslime à {0}. Ca lui en fait {2}, sacré moyenne !!"
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
