// Infrastructure as Code for the BP Calculator (Azure App Service, Linux)
// Creates: App Service plan + web app (production) + staging slot (blue/green)

@description('Name of the web app (must be globally unique)')
param appName string

@description('Azure region for all resources')
param location string = resourceGroup().location

@description('App Service plan tier (P0v3 supports deployment slots)')
param skuName string = 'P0v3'

@description('Name of the existing Application Insights resource (created once, kept when the environment is stopped)')
param appInsightsName string = 'appi-${appName}'

// existing Application Insights resource: only its connection string is read here
resource appInsights 'Microsoft.Insights/components@2020-02-02' existing = {
  name: appInsightsName
} 

resource plan 'Microsoft.Web/serverfarms@2024-11-01' = {
  name: 'asp-${appName}'
  location: location
  kind: 'linux'
  sku: {
    name: skuName
  }
  properties: {
    reserved: true // required for Linux plans
  }
}

// same settings for production and staging (these settings are not swapped)
var siteConfig = {
  linuxFxVersion: 'DOTNETCORE|10.0'
  alwaysOn: true
  minTlsVersion: '1.2'
  ftpsState: 'Disabled'

  appSettings: [
    {
      name: 'ASPNETCORE_FORWARDEDHEADERS_ENABLED'
      value: 'true'
    }
    
    {
      name: 'APPLICATIONINSIGHTS_CONNECTION_STRING' // telemetry is sent to this Application Insights resource
      value: appInsights.properties.ConnectionString
    }
  ]
}

resource webApp 'Microsoft.Web/sites@2024-11-01' = {
  name: appName
  location: location
  kind: 'app,linux'
  properties: {
    serverFarmId: plan.id
    httpsOnly: true
    clientAffinityEnabled: false // no ARRAffinity cookies
    siteConfig: siteConfig
  }
}

resource stagingSlot 'Microsoft.Web/sites/slots@2024-11-01' = {
  parent: webApp
  name: 'staging'
  location: location
  kind: 'app,linux'
  properties: {
    serverFarmId: plan.id
    httpsOnly: true
    clientAffinityEnabled: false // no ARRAffinity cookies
    siteConfig: siteConfig
  }
}

output productionUrl string = 'https://${webApp.properties.defaultHostName}'
output stagingUrl string = 'https://${stagingSlot.properties.defaultHostName}'
