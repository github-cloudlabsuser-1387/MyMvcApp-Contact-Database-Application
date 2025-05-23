# MyMvcApp-Contact-Database-Application

This guide explains how to deploy the MyMvcApp CRUD Application to Azure using an ARM template and automate deployments with a GitHub Actions workflow.

---

## 1. Deploying with ARM Template

The repository contains two files for Azure Resource Manager (ARM) deployment:

- **deploy.json**: The ARM template that defines the Azure App Service Plan and Web App resources.
- **deploy.parameters.json**: The parameters file for the ARM template.

### Deploy via Azure CLI

```sh
az deployment group create \
  --resource-group <your-resource-group> \
  --template-file deploy.json \
  --parameters @deploy.parameters.json
```

Replace `<your-resource-group>` with your Azure resource group name.

---

## 2. GitHub Actions Deployment Pipeline

A GitHub Actions workflow is included to automate build and deployment to Azure App Service.

- **.github/workflows/azure-webapp-deploy.yml**: The workflow file that builds and deploys your app on every push to the `master` branch.

### Setup Steps

1. **Configure Azure Publish Profile Secret**
   - In the Azure Portal, go to your Web App > Deployment Center > Get Publish Profile.
   - In your GitHub repository, go to Settings > Secrets and variables > Actions > New repository secret.
   - Name the secret `AZUREAPPSERVICE_PUBLISHPROFILE` and paste the contents of the publish profile.

2. **Workflow Trigger**
   - The workflow runs automatically on every push to the `master` branch.

---

## 3. Project Structure

```
MyMvcApp-Contact-Database-Application/
│
├── deploy.json
├── deploy.parameters.json
├── .github/
│   └── workflows/
│       └── azure-webapp-deploy.yml
```
