using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using UnityEngine.Events;

/// <summary>
/// you need the build for 64 bits and in player settings selec .net without subset
/// </summary>
public class EmailManager : MonoBehaviour {


    [Header("Datos Correo")]

    public string emailAddres = "eventos@cinecolor.com.co";
    public string emailName = "Guardianes de la Galaxia";
    public string smtpServer;
    public int port;
    public string userHost;
    public string password;

    public UnityEvent onMailFinished;

    [TextArea()]
    public string subject = "TU DISEÑO HUAWEI";
    [TextArea()]
    public string body = "Gracias por participar en nuestras actividades, puedes compartir tu diseño en redes sociales :)";
    


   // public string mailAddres = ;

    public Text StatusText;


    public bool useTroniksServer = true;
    public void SendEmail()
    {
        StartCoroutine("SendMailCoroutine");
    }

    public Attachment MakeAttachment()
    {
        Attachment inline = new Attachment(GameInfoManager.photoPath);
        inline.ContentDisposition.Inline = true;
        inline.ContentType.MediaType = "image/jpg";
        inline.ContentType.Name = "Foto.jpg";

        return inline;
    }

    public IEnumerator SendMailCoroutine()
    {
        StatusText.text = "Enviando Correo...";
        Debug.Log("Send Email Coroutine");
        yield return new WaitForSecondsRealtime(0.1f);

        MailMessage mail = new MailMessage();

        try
        {
            //ComposeMail(mail);

            if (useTroniksServer)
            {
                ComposeMail(mail, "eventos@troniks.me", "RETO HUAWEI LASER Y9", GameInfoManager.playerEmail);
                SendSmtp(mail);
            }else
            {
                ComposeMail(mail, emailAddres,emailName ,GameInfoManager.playerEmail);
                SendSmtp(mail, "smtp.office365.com",587, "eventos@cinecolor.com.co", "C1n3c0l0rEC");
            }

            StatusText.text = "¡Correo Enviado!";
            

        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
          StatusText.text = "Error al Enviar tu foto :(";
        }
        onMailFinished.Invoke();    }

    private void ComposeMail(MailMessage mail, string mailAddres = "eventos@troniks.me", string nameMail = "Huawei Y9", string mailTo = "eventos@troniks.me")
    {

        mail.From = new MailAddress(mailAddres , nameMail);
        mail.To.Add(mailTo);
        mail.Subject = subject;
        mail.Body = body;
        mail.Attachments.Add(MakeAttachment());
        Debug.Log("total Attachments : " + mail.Attachments.Count);
    }

    private static void SendSmtp(MailMessage mail, string smtpClient = "smtp.webfaction.com",int port = 587, string userName = "eventostroniks", string pass = "3V3NT052018")
    {
        
        SmtpClient smtpServer = new SmtpClient(smtpClient);
        smtpServer.Port = port;
        smtpServer.Credentials = new System.Net.NetworkCredential(userName, pass) as ICredentialsByHost;
        smtpServer.EnableSsl = true;
        ServicePointManager.ServerCertificateValidationCallback =
            delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
            { return true; };
        smtpServer.Send(mail);
    }

    void OnGUI()
    {

    }
}
