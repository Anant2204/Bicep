param location string 

param tags string 

param applicationInsightsName string 

param Application_Type string 

param Flow_Type string 

resource workmicroservicesdev001 'Microsoft.OperationalInsights/workspaces@2022-10-01' existing = {
  name:'work-microservices-dev-001'
  scope: resourceGroup('f777258d-fdd8-4efd-81be-8c383ae0b600','rg-microservices-dev-001')
}

resource appiassessmentsdev001 'Microsoft.Insights/components@2020-02-02' = {
  name: applicationInsightsName
  location: location
  tags:{
    '${tags}': tags
  }
  kind:'web'
  properties: {
    Application_Type: Application_Type
    Flow_Type: Flow_Type
    WorkspaceResourceId: workmicroservicesdev001.id
    
  }
}

output InstrumentationKey string  = appiassessmentsdev001.properties.InstrumentationKey
output Connectionstring string = appiassessmentsdev001.properties.ConnectionString
