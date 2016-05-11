%************** Rules **************
% Triangle
nom(triangle) :-
	ordre(3).

nom(triangleIsocele) :-
	nom(triangle),
	cotesEgaux(2).

nom(triangleRectangle) :-
	nom(triangle),
	angleDroit(oui).

nom(triangleRectangleIsoecele) :-
	nom(triangleIsocele),
	nom(triangleRectangle).

nom(triangleEquilatrel) :-
	nom(triangle),
	cotesEgaux(3).

% Quadrilateres
nom(quadrilateres) :-
	ordre(4).

nom(trapeze) :-
	nom(quadrilateres),
	cotesParalleles(2).

nom(parallelogramme) :-
	nom(quadrilatere),
	cotesParalleles(4).

nom(rectangle) :-
	nom(parallelogramme),
	angleDroit(oui).

nom(losange) :-
	nom(parallelogramme),
	cotesEgaux(4).

nom(carre) :-
	nom(losange),
	nom(rectangle).

memory(Pred, X).

ask(Pred, _, X) :-
	memory(Pred, X).

ask(Pred, _, _) :-
	memory(Pred, _),
	!,
	fail.

ask(Pred, Question, X) :-
	write(Question),
	read(Y),
	asserta(memory(Pred, Y)),
	X == Y.

cotesEgaux(X) :-
	ask(cotesEgaux, 'Combien la figure a-t-elle de côtés égaux ? ', X).

angleDroit(X) :-
	ask(angleDroit, 'La figure possède-t-elle des angles droits (oui, non) ? ', X).

cotesParalleles(X) :-
	ask(cotesParalleles, 'Combien la figure a-t-elle de côtés parallèles (0, 2 ou 4) ? ', X).

ordre(X) :-
	ask(ordre, 'Combien de côtés ? ', X).

solve :-
	retractall(memory(_, _)),
	findall(X, nom(X), R),
	write(R).