param location string 

param tags string 

param applicationInsightsName string 

param Application_Type string 

param Flow_Type string 

param keyVaultName string 

param skuName string 

param tenantId string 

param storageName string 

param storageSkuName string 

param appServicePlanName string 

param sku string 

param appName string 

param rgEnvironment string 

module appiassessmentsdev001 'appi-assessments-dev-001.bicep' = {
  name: 'applicationInsightsName'
  params:{
    location: location
    tags: tags
    applicationInsightsName: applicationInsightsName
    Application_Type: Application_Type
    Flow_Type: Flow_Type

  }
}

module kvassessmentsdev001 'kv-assessments-dev-001.bicep'= {
   name:'keyVaultName'
   params:{
    location: location
    keyVaultName: keyVaultName
    skuName: skuName
    tenantId: tenantId

   }
}

module stoassessmentsdev001 'st-assessments-dev-001.bicep' = {
  name:'storagename'
  params:{
    tags: tags
    location: location
    storageName: storageName
    storageSkuName: storageSkuName
  }
}

module serviceplanassessmentsdev001 'serviceplan-assessments-dev-001.bicep' = {
  name: 'appservicename'
  params: {
    appServicePlanName: appServicePlanName
    sku: sku
    tags: tags
    location: location
    
  }
}

module apiassessmentsdev001 'api-assessments-dev-001.bicep' = {
  name: 'app'
  params:{
    location: location
    appName: appName
    rgEnvironment: rgEnvironment
    InstrumentationKey:appiassessmentsdev001.outputs.InstrumentationKey
    Connectionstring:appiassessmentsdev001.outputs.Connectionstring
    serverFarm: serviceplanassessmentsdev001.outputs.serverFarm  

  }
}
  



  



