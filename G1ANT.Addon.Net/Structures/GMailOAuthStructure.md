# mail
This structure describes OAuth authentication details for GMail authorization. 
ClientID and Client Secret can be created in Google Developer Console https://console.developers.google.com  

| Field | Type| Description |
| -------- |------ | ---- |
|`username`|text| GMail account username you want to access to |
|`clientid`|text| ClientId of OAuth Google app |
|`secret`|text| Client secret of OAuth Google app |


## Example

♥auth = ⟦gmailoauth⟧
♥auth⟦username⟧ = ‴test@gmail.com‴
♥auth⟦clientid⟧ = ‴27dsfer6118958-sh7q75shka63ajcsqhb05nfo917q306t.apps.googleusercontent.com‴
♥auth⟦secret⟧ = ‴qXH_Z16kcfeloin8ssvcXmpjzmn‴

imap.openex host ‴imap.gmail.com‴ port 993 usessl true ignorecertificateerrors true authentication ♥auth
