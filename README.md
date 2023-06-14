# NBPapi
To run project, run this command in project directory:
```dotnet run```
<br>
<br>
### First opreration example:

https://localhost:7279/api/Currency/GetByDate/usd/2016-04-04

**result:**

average for usd at 2016-04-04 : 3.7254
<br>
<br>

### Second operation example:

https://localhost:7279/api/Currency/GetMinAndMax/usd/4

**result:**

min : 4.1649 max : 4.2024
<br>
<br>


### Third operation example:

https://localhost:7279/api/Currency/DiffrenceAskSell/usd/4

**result:**

[0.0842,
0.084,
0.0842,
0.0834]
