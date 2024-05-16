param location string 

param appName string 

param rgEnvironment string 

param InstrumentationKey string

param Connectionstring string

param serverFarm string


resource apiassessmentsdev001 'Microsoft.Web/sites@2022-09-01' = {
  name: appName
  location: location
  kind:'Windows'
  properties: {
    serverFarmId: serverFarm
    siteConfig: {
      windowsFxVersion:'Dotnetcore'
      appSettings: [
        {
          name: 'APPINSIGHTS_INSTRUMENTATIONKEY'
          value: InstrumentationKey
        }
        {
          name: 'APPLICATIONINSIGHTS_CONNECTION_STRING'
          value: Connectionstring
        }
        {
          name: 'ASPNETCORE_ENVIRONMENT'
          value: rgEnvironment
        }
      ]
    }
  }
}

