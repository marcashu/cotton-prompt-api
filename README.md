## How to Setup

### Prerequisites

- Install [Visual Studio](https://visualstudio.microsoft.com/)
- Download **appsettings.Development.json** file from the email and paste it inside the **./CottonPrompt.Api** folder

### Add your IP Address to gain access on the Dev Database

(This needs to be done once only)

1. Go to the [azure portal](https://portal.azure.com/).
2. Login using your Cotton Prompt account
3. Go to **SQL databases**
4. Then click on **cottonprompt-dev (cottonpromptdev/cottonprompt-dev)**
5. Then click on **Set server firewall**
6. On the **Public access** tab and under **Firewall rules**, click on **+ Add your client IPv4 address**
7. Edit the **Start IPv4 address** to end with **.0**
8. Edit the **End IPv4 address** to end with **.255**
9. Click **Save**

### Build the app

(This needs to be done once only)

1. Open **./CottonPromptApi.sln** file using **Visual Studio**
2. Right click on the **CottonPrompt.Api** project
3. Then click on **Build**

### Run the app

(This needs to be done everytime)

1. Run the Frontend App (follow the README file from the other repository)
2. Open **./CottonPromptApi.sln** file using **Visual Studio**
3. Run the **CottonPrompt.Api** project

## How to deploy changes to production

1. Run the command `git checkout master` to switch to the `master` (production) branch
2. Run the command `git merge dev` to merge the changes from `dev` to `master` branch
3. Run the command `git push` to push the changes to the `master` branch.

This will trigger a GitHub Action that will automatically deploy the changes to the cloud. The progress of the GitHub Action can be monitored on the **Actions** tab in the GitHub repo.

If the GitHub Action job fails, the usual suspect is there's not enough storage in the App Service file system. To fix this:

1. Go to the [azure portal](https://portal.azure.com/).
2. Login using your Cotton Prompt account
3. Go to **App Services**
4. Click on **cottonpromptapp-prod**
5. Under **Development Tools**, click on **SSH**
6. Click on **Go ->**
7. This will open a new tab, wait for the terminal to load
8. Run the command `cd site/wwwroot/.next/cache`
9. Then run the command `rm -rf images`, this will clear up the cached images
10. Wait for the command to finish
11. Then go back to the **Actions** tab in the GitHub repo, and rerun the failed job

### Updating the Database

If the update contains database changes, you can get the Production database connection string/credentials here:

1. Go to the [azure portal](https://portal.azure.com/).
2. Login using your Cotton Prompt account
3. Go to **App Services**
4. Click on **cottonpromptapi-prod**
5. Under **Settings**, click on **Environment variables**
6. Then click on the **Connection strings** tab
7. Then click on **Show value** button in the **DefaultConnection**
