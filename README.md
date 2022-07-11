Követelmények: Visual Studio 2022, Adatbazis mód esetén: SQL Server 2016+

* Első indításnál:
	- SelfCheckoutMachine.WebApi-t be kell állítani StartUp Projectnek a Visual Studio-ban
	- SelfCheckoutMachine.WebApi/appsettings.json-ben UseInMemoryDatabase = true esetén nincs további teendő, ilyenkor memóriában lesz az adatbázis a könnyebb tesztelés érdekében, de ez mindig üresen fog indulni, mehet a futtatás
	- UseInMemoryDatabase=false esetén: SelfCheckoutMachine.WebApi/appsettings.json-ban a 'ConnectionString' át kell írni a megfelelő értékre
	- UseInMemoryDatabase=false esetén: Package Manager console-ban Default projectnek be kell állítani: SelfCheckoutMachine.DataAccess
	- UseInMemoryDatabase=false esetén: Package Manager console-ban futtatni a következő parancsot: EntityFrameworkCore\Update-Database - ez létrehozza az adatbázisunkat
	- Kestrelen (alapbeállítás) futtassuk az alkalmazásunkat

Az alkalamzás tartalmaz swagger felületet így ott könnyen ki lehet próbálni a funkciókat.

Végpontok:
- GET https://localhost:7180/api/v1/Stock
- POST https://localhost:7180/api/v1/Stock
- POST https://localhost:7180/api/v1/Checkout
- GET https://localhost:7180/api/v1/BlockedBills
- POST https://localhost:7180/api/v1/CheckoutInEur

Az EUR checkoutnál a könnyebség kedvéért mi adjuk be a váltószámot (exchangeRate) ami azt jellemzi 1 EUR hány HUF.
A EUR megoldást nagyon leegyszerűsítettem a háttérben a gépünk egy valutaváltógéppel van összekötve, 
akinek mindig van elég forintja ahhoz hogy a legoptimálisabb címletekben váltson nekünk 0 haszonnal :D,
ezután az itt visszakapott címleteket tekintjük alapból beadott címleteknek, és innen már az alap megoldás működik.
A felhasználó forintban kap vissza.

Ha esetleg valami nem működne vagy kérdés lenne elérhető vagyok bármikor.

Azért használtam CQRS patternt az alkalmazáshoz, mert mostanában nagyon megtetszett és nagyon könnyen implementáltható .NET 6 alatt, 
és bár nem lett volna szükséges ennyire széttagolni ezt az egyszerű alkalmazást, be akartam mutatni hogyan is dolgozom.
