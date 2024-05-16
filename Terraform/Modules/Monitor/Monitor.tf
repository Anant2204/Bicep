resource "azurerm_log_analytics_workspace" "log-wwwaacnorg-dev-001" {
  name                = "log-wwwaacnorg-dev-001"
  location            = var.location
  resource_group_name = var.resource_group_name
  sku                 = "PerGB2018"
  retention_in_days   = 30

}

# Resource - Application Insights 

resource "azurerm_application_insights" "application-wwwaacnorg-dev-001" {
  name                = "application-wwwaacnorg-dev-001"
  location            = var.location
  resource_group_name = var.resource_group_name
  workspace_id        = azurerm_log_analytics_workspace.log-wwwaacnorg-dev-001.id
  application_type    = "web"
}


output "instrumentation_key" {
  value     = azurerm_application_insights.application-wwwaacnorg-dev-001.instrumentation_key
  sensitive = "true"
}

output "app_id" {
  value = azurerm_application_insights.application-wwwaacnorg-dev-001.app_id
}


# Resource - Monitor action group 

resource "azurerm_monitor_action_group" "actiongrp-www-aacn-support-dev-001" {
  name                = "actiongrp-www-aacn-support-dev-001"
  resource_group_name = var.resource_group_name
  short_name          = "agwwwaacnorg"
  email_receiver {
    name          = "team-architecture-alerts"
    email_address = "df6936d9.aacn.org@amer.teams.ms"
  }
  
}

# Resource - Monitor scheduled query rules alert

resource "azurerm_monitor_scheduled_query_rules_alert" "logalert-wwwaacnorg-response-001" {
  name                = "logalert-wwwaacnorg-response-001"
  location            = var.location
  resource_group_name = var.resource_group_name

  action {
    action_group           = [azurerm_monitor_action_group.actiongrp-www-aacn-support-dev-001.id]
    email_subject          = "team-architecture-alerts"
    custom_webhook_payload = "{}"
  }
  data_source_id = azurerm_application_insights.application-wwwaacnorg-dev-001.id
  description    = "Alert when total results cross threshold"
  enabled        = true
  # Count all requests with server error result code grouped into 5-minute bins
  query       = <<-QUERY
   let timeGrain=5m;
   let total_count=toscalar(requests
    | summarize count());
   let slow_count=toscalar(requests
    | where client_Type != 'Browser'
    | summarize avg(duration) by bin(timestamp, timeGrain)
    | where avg_duration > 3000
    | summarize count());
   let diff=toscalar((todouble(slow_count) / todouble(total_count)) * 100);
   print diff, total_count, slow_count
  | where total_count > 100
  QUERY
  severity    = 2
  frequency   = 5
  time_window = 30
  trigger {
    operator  = "GreaterThan"
    threshold = 0
  }
  tags = {
    foo = "bar"
  }
}

