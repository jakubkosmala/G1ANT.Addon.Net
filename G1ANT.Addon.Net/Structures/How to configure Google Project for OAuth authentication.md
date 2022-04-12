## How to configure Google Project for OAuth authentication


 1. Open **[Google Cloud Console](https://console.cloud.google.com/)** and click on **Select a project**.  
   ![](https://lh5.googleusercontent.com/eOr7CVsZALY3TnLx6Fzp69wWzpLCH2SoO8ha-0vgNo0UWMW0g-VTccx0ROz2o-bcs3yw_VK6A4ym5Y-nY4OvU3H2b4CqDrGS1Y4-FROCiBwMyuhwgHUU4S1ivPWSI2mAcjnZw2o)

2. In the opened modal dialog select an organization where you want to create the project and click**NEW PROJECT**.

3. Fill the form:

   1. **Project name** of your application
   2. **Organization** where the project will be created**
   3. **Location**
   4. Click **CREATE**

2. Select the created project and click on**APIs & Services**.

3. We need to configure “OAuth consent screen” before we will be able to generate an API key.  
   Select **OAuth consent screen** menu from the left panel and choose user type for the application

   1. Internal - when you want to authorize only users belong to the organization
   2. External - when you want to authorize any user

4. Fill in the form with required informations:

   1. **App name** which will be presented to the users
   2. **User support email**
   3. **Developer contact information**
   4. Fill other fields as desired
   5. Click **SAVE AND CONTINUE**

5. Leave **Scopes** step as it is, click **SAVE AND CONTINUE**.

6. If you selected **External** user type you need to define Test users for your app, otherwise just back to the dashboard.

7. Select the Credentials menu, click **CREATE CREDENTIALS** button and choose **OAuth client ID**.  
   ![](https://lh6.googleusercontent.com/31T_92YsFUJz2WYyAdaUQhnGClWbO8At1nORzLCnvjdBUIsE6r5IoJGmDGb5E2n3csC1w_RBN_YNYrW1IA-i17kRrcuyNa4QSIp_w2Icd_MfTKFZL4oqYFF-fPb0CSXcvnKQIUE)

8. Select **Desktop app** as an application type, type **Name** for your application and click **CREATE**.

9. Generated **Client ID** and **Client Secret** values are needed to authenticate **imap/smtp** **G1ANT** commands connection.
