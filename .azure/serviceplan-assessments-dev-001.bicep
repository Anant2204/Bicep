param location string 

param appServicePlanName string 

param sku string 

param tags string 

resource serviceplanassessmentsdev001 'Microsoft.Web/serverfarms@2023-01-01' = {
  name: appServicePlanName
  kind:'Windows'
  location: location
  tags:{
    '${tags}': tags
  }

  properties: {
    reserved: false    
  }
  sku: {
    name: sku
  }
 
}

output serverFarm string = serviceplanassessmentsdev001.id
