//Generic method.
//expecting
//<subject>subject</subject>
//<body>body text – could be html</body>
//<from>email address of who to send from = User if send from Logged On User</from>
//<to>list of email addresses, comma separated</to>
//<attachments>list of files, comma separated fully qualified file names</attachments>
Innovator inn = this.getInnovator();
 
Item smtpVar = this.newItem("Variable","get");
smtpVar.setProperty("name","smtp server");
smtpVar.setAttribute("select","value");
smtpVar = smtpVar.apply();
 
string smtpSrv = smtpVar.getProperty("value","");
if ( smtpSrv == "" ){
    return  inn.newError("No SMTP Server set for emails with attachements. See 'smtp server' Variable under Administration.");        
}
 
 
string subject = this.getProperty("subject","");
string body = this.getProperty("body","");
string from = this.getProperty("from","user");
string toList = this.getProperty("to","");
string attachmentsList = this.getProperty("attachments","");
 
//string[] to = toList.Split(new char[] {','});
string[] attachments = attachmentsList.Split(new char[] {','});
 
 
 
//System.Diagnostics.Debugger.Break();
System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
 
Item usr = this.newItem("User","get");
usr.setID(inn.getUserID());
usr.setAttribute("select","email");
usr = usr.apply();
if ( usr.isEmpty() ){
       return inn.newError("Unable to retrieve logged on user credentials: " + usr.getErrorDetail());
}
 
System.Net.Mail.MailAddress fromAdr = new System.Net.Mail.MailAddress(from);
 
message.From = fromAdr;
message.To.Add(toList);
message.Subject = subject;
message.Body = body;
message.IsBodyHtml = true;
 
for ( int i = 0; i < attachments.Length; i++){
    System.Net.Mail.Attachment attachment = new
	System.Net.Mail.Attachment(attachments[i]);
    System.Net.Mime.ContentDisposition disp = attachment.ContentDisposition;
    disp.CreationDate = System.IO.File.GetCreationTime(attachments[i]);
    disp.ModificationDate = System.IO.File.GetLastWriteTime(attachments[i]);
    disp.ReadDate = System.IO.File.GetLastAccessTime(attachments[i]);

   message.Attachments.Add(attachment);
}
//System.Diagnostics.Debugger.Break();
try{
System.Net.Mail.SmtpClient client = new
System.Net.Mail.SmtpClient(smtpSrv);
 
	client.Send(message);
} catch (Exception e){
    message.Attachments.Dispose();
    return inn.newError("Exception: " + e.Message);
}
 
message.Attachments.Dispose();
return this;