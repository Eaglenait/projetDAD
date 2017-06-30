using System;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Net.Mail;
using System.Net;

namespace BrutusService
{
    class mailGen
    {

        const string virginie = "chivini@hotmail.fr";

        /// <summary>
        /// default constructor for testing purpose
        /// </summary>
        public mailGen()
        {
            /*
             * Donner les bonnes valeurs des informations
             * Donner adresse mail du receiver (normalement nom de l'utilsiateur)
             */
            Console.WriteLine("Starting mail gen");
            string msg = "voilai^shstdhsdfwmic  un petit message";
            string keyValue = "v3er2vre4r51er8";
            int rateValue = 80;
            string receiver = "chivini@hotmail.fr";

            //Créer le PDF
            makePdf(msg,keyValue,rateValue,receiver);
        }

        /// <summary>
        /// Constructor to send an email
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="key"></param>
        /// <param name="fiability"></param>
        /// <param name="email"></param>
        public mailGen(string msg, string key, double fiability, string email)
        {
            fiability *= 100;
            fiability = Math.Truncate(100 * fiability) / fiability;
            makePdf(msg, key, fiability, virginie);
        }

        //Créer le PDF
        static void makePdf(string msg, string cle, double taux, string destinataire)
        {
            Console.WriteLine("Making PDF");

            //Créer un document
            String outputFile = Path.Combine("C:/Users/Thor/Desktop/", "FichierDechiffre.pdf");
            FileStream fs = new FileStream(outputFile, FileMode.Create, FileAccess.Write, FileShare.None);
            Document doc = new Document();
            PdfWriter writer = PdfWriter.GetInstance(doc, fs);

            Console.WriteLine("Mail instance created");

            //Ouvre le document
            doc.Open();

            //Prépare les fonts utilisées
            var titleFont = FontFactory.GetFont(FontFactory.TIMES_BOLD, 25);
            var boldFont = FontFactory.GetFont(FontFactory.TIMES_BOLD, 12);
            var normalFont = FontFactory.GetFont(FontFactory.TIMES, 12);

            //Ajoute les composants
            //Titre
            Paragraph title = new Paragraph(new Phrase(new Chunk("Détails du fichier déchiffré", titleFont)));
            title.Alignment = Element.ALIGN_CENTER;
            title.SpacingAfter = 60;
            doc.Add(title);

            //Message 
            Paragraph msgDechiffre = new Paragraph();
            msgDechiffre.Add(new Phrase(new Chunk("Message déchiffré : \n", boldFont)));
            msgDechiffre.Add(new Phrase(new Chunk(msg, normalFont)));
            msgDechiffre.SpacingAfter = 10;
            doc.Add(msgDechiffre);

            //Clé
            Paragraph key = new Paragraph();
            key.Add(new Phrase(new Chunk("Clé utilisée : \n", boldFont)));
            key.Add(new Phrase(new Chunk(cle, normalFont)));
            key.SpacingAfter = 10;
            doc.Add(key);

            //Taux
            Paragraph rate = new Paragraph();
            rate.Add(new Phrase(new Chunk("Taux de confiance : \n", boldFont)));
            rate.Add(new Phrase(new Chunk(taux + "%", normalFont)));
            rate.SpacingAfter = 10;
            doc.Add(rate);

            //Ferme le document
            doc.Close();

            Console.WriteLine("PDF generated");


            //Envoit l'mail à l'utilsateur
            sendMail(destinataire);
        }

        //Envoi l'email
        static void sendMail(string destinataire)
        {
            Console.WriteLine("Sending mail");

            try
            {

                Console.WriteLine("trying");

                /*
                * METTRE LE BON CHEMIN A PARTIR DE TON PC
                */
                string path = "C:/Users/Thor/Desktop/FichierDechiffre.pdf";

                //Créer un mail
                MailMessage mail = new MailMessage();

                //Rempli les champs du mail
                mail.To.Add(destinataire);
                mail.Subject = "Message décrypté";
                mail.Body = "Bonjour, \n\nNous avons le plaisir de vous informer que nous avons réussi à décrypter le message.\n" + 
                    "Ci-joint les informations relatives à celui-ci. \n\nCordialement,\nL'équipe de déchiffrement";
                mail.From = new MailAddress("no-replay@mon-site.fr", "Decrypt message team");

                //Donne la pièce jointe
                Attachment attachment = new Attachment(path);
                attachment.Name = "FichierDéchiffré.pdf";
                mail.Attachments.Add(attachment);

                //Configure le smtp
                SmtpClient smtpServer = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 25,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("projetGenPDF@gmail.com", "DAD123JEE*")
                };

                Console.WriteLine("Before sending mail");

                //Envoie l'eamil
                smtpServer.Send(mail);
                Console.WriteLine("After sending mail");

                Console.WriteLine("Message envoyé");
            }
            catch (Exception e)
            {
                Console.WriteLine("Bite");
                Console.WriteLine(e.Message);
            }
        }
    }
}
