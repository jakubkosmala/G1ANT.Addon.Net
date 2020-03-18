# mail
This structure stores current information about a mail message, which was downloaded with the [`mail.imap`](https://manual.g1ant.com/link/G1ANT.Addon.Net/G1ANT.Addon.Net/Commands/MailImapCommand.md) command. The mail structure contains several fields such as:

| Field | Type| Description |
| -------- |------ | ---- |
|`id`|text| Identification number of the message |
|`from`|text| Sender of the message |
|`to`|text| Recipient of the message |
|`cc`|text| CC recipient of the message |
|`bcc`|text| BCC recipient of the message |
|`subject`|text| Subject of the message |
|`content`|text| Content of the message |
|`date`|text| Date of the message |
|`priority`|integer| Priority of the message |
|`attachments`|list| Returns a list of elements of the [attachment](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Language/Structures/AttachmentStructure.md) structure allowing to manage attachments |
|`isreply`|bool| Is the message a reply |

## Example
This example shows how to receive messages using the `mail.imap` command and iterate through all of them. As a result, the message body should be displayed in a dialog box.

```G1ANT
♥yesterday = ⟦date⟧13.01.2018
mail.imap host imap.gmail.com port 993 login mail@gmail.com password p@$$w0rD sincedate ♥yesterday onlyunreadmessages true markallmessagesasread false result ♥result 

foreach ♥element in ♥result
   dialog ♥element⟦content⟧
end
```

> **Note:** Host, login and password values are of course just examples. You have to provide your real mail server credentials for the command to work.