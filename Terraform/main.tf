terraform {
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "=3.56.0"
    }
  }

  backend "azurerm" {
    storage_account_name = "stwebsitesprod001"
    resource_group_name  = "rg-websites-prod-001"
    container_name       = "statefile"
    key                  = "terraform.tfstate"
  }

}

provider "azurerm" {
  subscription_id = var.subscription_id
  client_id       = var.client_id
  client_secret   = var.client_secret
  tenant_id       = var.tenant_id
  features {}
  skip_provider_registration = true
}

module "Monitor" {
  source = "./Modules/Monitor"

}




