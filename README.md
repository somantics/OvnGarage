Det här projektet använder sig av ett CLI bibliotek som jag skrev förra veckan, repo finns här: https://github.com/somantics/ConsoleMenu

CLI och datastrukturen initializeras i Program.cs, sen tar cli över. 

### Garage
Huvudklass för datastrukturen. Den har ingen kunskap eller direkt kontakt med ui lagret.

### UIHandler
"Middleware" klass som pratar med CLI bliblioteket och garaget och översätter mellan dem.
Det är här menyvalen specificeras. 

### Vehicle
Abstrakt bas klass för alla fordon. Alla ärver direkt av Vehicle för att hålla det enkelt. 

Varje enskilt vehicle har ett eget ljud (testa honk i menyn), samt en egen default mängd hjul. Hoppas det är nog. 
