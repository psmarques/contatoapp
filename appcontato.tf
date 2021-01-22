provider "azurerm" {

        features {}
}

resource "azurerm_resource_group" "rg1" {
        name = "rg-contatoapp-terraform"
        location = "East US"
}

resource "azurerm_application_insights" "appinsights1" {
        name    = "appinsights_contatoapp_tf"
        resource_group_name = azurerm_resource_group.rg1.name
        location = azurerm_resource_group.rg1.location
        application_type = "web"
}

resource "azurerm_kubernetes_cluster" "clust1" {
  name                = "contatoapp-aks1"
  location            = azurerm_resource_group.rg1.location
  resource_group_name = azurerm_resource_group.rg1.name
  dns_prefix          = "contatoappaks1"

  default_node_pool {
    name       = "default"
    node_count = 1
    vm_size    = "Standard_D2_v2"
  }

  identity {
    type = "SystemAssigned"
  }

  tags = {
    Environment = "Production"
  }
}

output "client_certificate" {
  value = azurerm_kubernetes_cluster.clust1.kube_config.0.client_certificate
}

output "kube_config" {
  value = azurerm_kubernetes_cluster.clust1.kube_config_raw
}