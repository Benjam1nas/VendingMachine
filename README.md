Prielaidos: 
1. Visur naudojama int tipo kintamieji. Daroma prielaida, kad skaičiai bus sveikieji ir kaina nebus 2,99 eur ar pnš.
2. Naudojama lokali duomenų bazė.
3. Metodai aprašyti taikantis prie FE. Daroma prielaida, kad kai kurie metodai yra iškviečiami mygtukų paspaudimų arba background servisų (jie nėra implementuoti). Metodai automatiškai patys nepasileidžia.
5. Daroma prielaida, kad turime tik vieną prekių automatą, kitų atveju, reikėtų papildomo metodo, kuris užildytu arba visus automatus iškart, arba pasirinktus aparatus su tais pačiais parametrais.

Dėl laiko stokos neatlikti šie darbai:
1. Error handling'as: Galima apsirašyti middleware, kuris būtų globalus arba bent jau turėti try catch'us.
2. Loggingas: Užduotyje taip pat nedaromas logging'as, kuris didelėj sistemoj turėtų būti.
3. Servisai: Nepilnai išpildytas CRUD'as. Pvz.: Item ir Coin turi tik Create metodą. 
4. Base response: Nepadarytas standartinis grąžinimas į FE, friedly message ir pnš.
5. Nenaudojami cancelation token'ai.
