# Currency Tracker

## Description
This API allows you to fetch the exchange rate (buy and sell rates) for a specified currency on a given date. The data is retrieved from the National Bank of Poland (NBP) API.

## Installation guide

1. **Clone the repository**
```bash
git clone <repository-url>
cd <repository-folder>
```
2. **Update appsettings.json with proper connection string**

```json
{
  "ConnectionString": "PLACEHOLDER_FOR_CONNECTION_STRING"
}
```
3. **Run the Application**

    Open a terminal in the repository folder and start the application
```bash
dotnet run
```
4. **Access the API**

   Once the application is running, the terminal will display the URL where the API is hosted, for example:
```bash
Now listening on: http://localhost:5150
```

### Usage

To fetch exchange rates for a specific currency and date, send a GET request to:

```text
http://localhost:<port>/api/currency/rate/{currency}/{date}
```
`<port>` : Replace with the actual port displayed in the terminal after running the application (step 4)

`{currency}`: Currency code (e.g., USD, EUR). Case-sensitive.

`{date}`: The date in `yyyy-MM-dd` format.


#### Example request
```bash
curl http://localhost:5150/api/currency/rate/USD/2024-11-05
```
#### Response example
```json
{
  "currency": "USD",
  "date": "2024-11-05T00:00:00",
  "buyRate": 4.1234,
  "sellRate": 4.5678
}
```