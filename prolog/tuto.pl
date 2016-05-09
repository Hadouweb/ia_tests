plusGros('rhinocéros', 'cheval').
plusGros('cheval', 'chien').
plusGros('chien', 'chat').
plusGros('chat', 'hamster').

estPlusGros(X, Y) :-
	plusGros(X, Y).

estPlusGros(X, Z) :-
	plusGros(X, Y),
	estPlusGros(Y, Z).

phrase(_) :-
	estPlusGros(X, Y),
	\+plusGros(X, Y),
	write('Le '), write(X), write(' est plus gros que le '), write(Y), nl.

phrases :-
	findall(X, phrase(X), _).