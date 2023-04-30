using NETSUITE_PRINT.netsuite.webservices;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace NETSUITE_PRINT
{
    public partial class FrmPrincipal : Form
    {

        static bool _isAuthenticated;
        public SqlConnection connection = new SqlConnection("Data Source={ServerName};Initial Catalog={DataBase};User ID=sa;Password={Contraseña}");
        static NetSuiteService _service;
        

        public FrmPrincipal()
        {
            InitializeComponent();
            this.Text = Application.ProductVersion.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {



            if (this.TxtInternalId.Text == "")
            {
                MessageBox.Show("Debe Colocar el Interal ID");
                return;
            }

            if (this.CbxType.Text == "")
            {
                MessageBox.Show("Debe Colocar el Tipo de documento");
                return;
            }



            if (this.CbxType.Text == "FACTURACION")
            {
                Facturacion();

            }

            if (this.CbxType.Text == "CHEQUES")
            {
                Cheques();
            }

            if (this.CbxType.Text == "PAGO_PROVEEDOR")
            {
                VendorPayment();
            }

             if (this.CbxType.Text == "NOTAS_CREDITO")
            {
                Notas_Credito();
            }
        


            
        }

        private ApplicationInfo CreateApplicationId()
        {
            ApplicationInfo applicationInfo = new ApplicationInfo();
            applicationInfo.applicationId = "{applicationId}";
            return applicationInfo;
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {

            _service = new NetSuiteService();
            _service.applicationInfo = CreateApplicationId();
            _service.CookieContainer = new CookieContainer();
            _service.Timeout = 1000 * 60 * 60 * 100;
            _service.searchPreferences = new SearchPreferences() { bodyFieldsOnly = false };

            Passport passport = new Passport();
            passport.account = "{AccountId}";
            passport.email = "{Email}";
            passport.password = "{Password}";



            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;                        
           _service.Url = "https://4861091.suitetalk.api.netsuite.com/services/NetSuitePort_2018_2";            
            _service.passport = passport;            
            
            SessionResponse response = _service.login(passport);
            
            Status status = response.status;
            _isAuthenticated = status.isSuccess;

            this.BtnImprimir.Enabled = true;
            label4.Text = "Estado: Conectado";
            label4.ForeColor = Color.Green;

        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            _service.logout();
            label4.Text = "Estado: No Conectado";
            label4.ForeColor = Color.Red;//this.BtnImprimir.Enabled = false;
        }

        public void Cheques() 
        {


            string BankId="";
            string entity="";
            string tranDate = "";//default(DateTime); 
            string tranId="";
            string memo="";
            string subsidiary="";
            float userTotal = 0;

            string account="";
            float amount =0;
            float grossAmt = 0;

            if (_isAuthenticated != true) { return; };


            RecordRef invoiceRef = new RecordRef
            {
                type = RecordType.check,
                typeSpecified = true,
                internalId = this.TxtInternalId.Text,

            };


            ReadResponse readResponse = _service.get(invoiceRef);
            if (readResponse.status.isSuccess)
            {

                FileStream fs = System.IO.File.Create("Cheque.xml");
                XmlSerializer writer = new XmlSerializer(typeof(ReadResponse));

                writer.Serialize(fs, readResponse);
                fs.Close();

                string content;
                using (StreamReader reader = new StreamReader("Cheque.xml", Encoding.UTF8))
                {
                    content = reader.ReadToEnd();
                }
                content = content.Replace("q1:", "");
                System.IO.File.WriteAllText("Cheque.xml", content, Encoding.UTF8);
            }

                XmlDocument myXmlDocument = new XmlDocument();
                myXmlDocument.Load("Cheque.xml");
                XmlNodeList Encabezado = myXmlDocument.DocumentElement.SelectNodes("/ReadResponse");
                XmlNodeList Documento = Encabezado.Item(0).LastChild.ChildNodes;
                XmlNodeList ListadoExpenses = null;
                XmlNodeList ListadoItems = null;


                if (connection.State != ConnectionState.Open) { connection.Open(); };

                string CmdDelete="DELETE FROM [dbo].[Check] WHERE internalId="+TxtInternalId.Text;
                SqlCommand commandDelete = new SqlCommand(CmdDelete, connection);
                commandDelete.ExecuteNonQuery();

                string CmdDeleteDetails = "DELETE FROM [dbo].[CheckDetalle] WHERE internalId=" + TxtInternalId.Text;
                SqlCommand commandDeleteDetail = new SqlCommand(CmdDeleteDetails, connection);
                commandDeleteDetail.ExecuteNonQuery();




                foreach (XmlNode node in Documento)
                {

                    if (node.Name == "entity") { entity = node.InnerText; };
                    if (node.Name == "tranDate") { tranDate = node.InnerText.Substring(0,10); };
                    if (node.Name == "tranId") { tranId = node.InnerText; };
                    if (node.Name == "subsidiary") { subsidiary = node.InnerText; };
                    if (node.Name == "account") { BankId = node.InnerText; };
                    if (node.Name == "memo") { memo = node.InnerText; };
                    if (node.Name == "userTotal") { userTotal = float.Parse(node.InnerText); };

                   
                    if (node.Name == "expenseList"){ListadoExpenses = node.ChildNodes;};
                    if (node.Name == "itemList") { ListadoItems = node.ChildNodes; };
                                
                }

                string CmdInsert = "INSERT INTO [dbo].[Check]"
                + "([internalId]"
                + ",[BankId]"
                + ",[entity]"
                + ",[tranDate]"
                + ",[tranId]"
                + ",[memo]"
                + ",[subsidiary]"
                + ",[userTotal])"
                + " VALUES(" + this.TxtInternalId.Text + ",'" + BankId + "','" + entity + "',convert(varchar, '" + tranDate + "', 23)" + ",'" + tranId + "','" + memo + "','" + subsidiary + "'," + userTotal.ToString() + ")";


                SqlCommand command = new SqlCommand(CmdInsert, connection);
                command.ExecuteNonQuery();

            if  (ListadoExpenses !=null ){
            
                foreach (XmlNode nodes in ListadoExpenses)
                {
                account = nodes.ChildNodes[1].InnerText;
                amount = float.Parse(nodes.ChildNodes[2].InnerText);
                grossAmt =0;


                string CmdInsertDetail = "INSERT INTO [dbo].[CheckDetalle]"
                + "([internalId]"
                + ",[account]"
                + ",[amount]"
                + ",[grossAmt])"
                + " VALUES(" + this.TxtInternalId.Text + ",'" + account + "'," + amount.ToString() + "," + grossAmt + ")";

                SqlCommand commandDetail = new SqlCommand(CmdInsertDetail, connection);
                commandDetail.ExecuteNonQuery();
                }

            } else{

                foreach (XmlNode nodes in ListadoItems)
                {
                account = nodes.ChildNodes[4].InnerText;
                amount = float.Parse(nodes.ChildNodes[7].InnerText);
                grossAmt = float.Parse(nodes.ChildNodes[7].InnerText);


                string CmdInsertDetail = "INSERT INTO [dbo].[CheckDetalle]"
                + "([internalId]"
                + ",[account]"
                + ",[amount]"
                + ",[grossAmt])"
                + " VALUES(" + this.TxtInternalId.Text + ",'" + account + "'," + amount.ToString() + "," + grossAmt + ")";

                SqlCommand commandDetail = new SqlCommand(CmdInsertDetail, connection);
                commandDetail.ExecuteNonQuery();
                }
            
            
            
            
            }



                if (connection.State == ConnectionState.Open) { connection.Close(); };

                FrmImpresionDirecta f = new FrmImpresionDirecta();
                f.v_tipoDocumento = "Cheque";
                f.v_DocumentoId = this.TxtInternalId.Text;
                f.v_Subsidiary = subsidiary;
                f.v_BankId = this.CbxFormatoCheque.Text;
                f.ShowDialog(this);
        
        }

        public void VendorPayment()
        {


                string BankId="";
                string entity="";
                string tranDate="";
                string tranId="";
                string memo="";
                string subsidiary="";
                float total = 0;

                string doc = "";
                string type = "";
                string refNum = "";
                float totalDetail = 0;
                float amount = 0;
                bool apply = false;


                if (_isAuthenticated != true) { return; };


                RecordRef invoiceRef = new RecordRef
                {
                    type = RecordType.vendorPayment,
                    typeSpecified = true,
                    internalId = this.TxtInternalId.Text,

                };


                ReadResponse readResponse = _service.get(invoiceRef);
                if (readResponse.status.isSuccess)
                {

                    FileStream fs = System.IO.File.Create("VendorPayment.xml");
                    XmlSerializer writer = new XmlSerializer(typeof(ReadResponse));

                    writer.Serialize(fs, readResponse);
                    fs.Close();

                    string content;
                    using (StreamReader reader = new StreamReader("VendorPayment.xml", Encoding.UTF8))
                    {
                        content = reader.ReadToEnd();
                    }
                    content = content.Replace("q1:", "");
                    System.IO.File.WriteAllText("VendorPayment.xml", content, Encoding.UTF8);
                }

            XmlDocument myXmlDocument = new XmlDocument();
            myXmlDocument.Load("VendorPayment.xml");
            XmlNodeList Encabezado = myXmlDocument.DocumentElement.SelectNodes("/ReadResponse");
            XmlNodeList Documento = Encabezado.Item(0).LastChild.ChildNodes;
            XmlNodeList ListadoApply = null;

            if (connection.State != ConnectionState.Open) { connection.Open(); };

            string CmdDelete = "DELETE FROM [dbo].[VendorPayment] WHERE internalId=" + TxtInternalId.Text;
            SqlCommand commandDelete = new SqlCommand(CmdDelete, connection);
            commandDelete.ExecuteNonQuery();

            string CmdDeleteDetails = "DELETE FROM [dbo].[VendorPaymentDetalle] WHERE internalId=" + TxtInternalId.Text;
            SqlCommand commandDeleteDetail = new SqlCommand(CmdDeleteDetails, connection);
            commandDeleteDetail.ExecuteNonQuery();


            foreach (XmlNode node in Documento)
            {

                if (node.Name == "entity") { entity = node.InnerText; };
                if (node.Name == "BankId") { BankId = node.InnerText; };                
                //if (node.Name == "tranDate") { tranDate = node.InnerText; };
                if (node.Name == "tranDate") { tranDate = node.InnerText.Substring(0, 10); };
                if (node.Name == "tranId") { tranId = node.InnerText; };
                if (node.Name == "subsidiary") { subsidiary = node.InnerText; };
                if (node.Name == "account") { BankId = node.InnerText; };
                if (node.Name == "memo") { memo = node.InnerText; };
                if (node.Name == "total") { total = float.Parse(node.InnerText); };


                if (node.Name == "applyList")
                {
                    ListadoApply = node.ChildNodes;
                }

            }


            string CmdInsert = "INSERT INTO [dbo].[VendorPayment]"
            + "([internalId]"
            + ",[BankId]"
            + ",[entity]"
            + ",[tranDate]"
            + ",[tranId]"
            + ",[memo]"
            + ",[subsidiary]"
            + ",[total])VALUES(" + this.TxtInternalId.Text + ",'" + BankId + "','" + entity + "',convert(varchar,'" + tranDate.ToString()+ "',23),'" + tranId + "','" + memo + "','" + subsidiary + "'," + total.ToString() + ")";

            SqlCommand command = new SqlCommand(CmdInsert, connection);
            command.ExecuteNonQuery();

            foreach (XmlNode XmlNodeList in ListadoApply)
            {

                    apply = bool.Parse(XmlNodeList.ChildNodes.Item(0).InnerText);

                    

                if (apply==true){

                    doc = XmlNodeList.ChildNodes.Item(1).InnerText;
                    type = XmlNodeList.ChildNodes.Item(4).InnerText;
                    refNum = XmlNodeList.ChildNodes.Item(5).InnerText;
                    totalDetail = float.Parse(XmlNodeList.ChildNodes.Item(6).InnerText);

                    try
                    { amount = float.Parse(XmlNodeList.ChildNodes.Item(9).InnerText); }
                    catch
                    {
                        amount = float.Parse(XmlNodeList.ChildNodes.Item(8).InnerText);
                    } 

                    
                    string CmdInsertDetail = "INSERT INTO [dbo].[VendorPaymentDetalle]"
                    + "([internalId]"
                    + ",[doc]"
                    + ",[type]"
                    + ",[refNum]"
                    + ",[total]"
                    + ",[amount])VALUES(" + this.TxtInternalId.Text + ",'" + doc + "','" + type + "','" + refNum + "'," + total.ToString() + "," + amount.ToString() + ")";
                    SqlCommand commandDetail = new SqlCommand(CmdInsertDetail, connection);
                    commandDetail.ExecuteNonQuery();

                }
                        

                }


                if (connection.State == ConnectionState.Open) { connection.Close(); };

                FrmImpresionDirecta f = new FrmImpresionDirecta();
                f.v_tipoDocumento = "VendorPayment";
                f.v_DocumentoId = this.TxtInternalId.Text;
                f.v_Subsidiary = subsidiary;
                f.v_BankId = this.CbxFormatoCheque.Text;
                f.ShowDialog(this);

            }
      
        public  void Facturacion()
        {

            string entity = "";
            string tranDate = "";
            string tranId = "";
            string terms = "";
            string subsidiary = "";
            string salesRep = "";
            string memo = "";
            string addr3 = "";
            string city = "";
            string addr1 = "";
            string custbody_cust_nitcliente = "";
            string custbody_cust_giro = "";
            string custbody_cust_nrc = "";
            string custbody_cust_tipodocumento = "";
            string custbodycustbodycustbody_cust_paquete = "";
            string custbody_cust_numerocontrato = "";
            string custbodycustbody_cust_numeroop = "";
            string message = "";
            float total = 0;
            float subTotal = 0;
            float taxTotal = 0;
            string DocRef = "";

            string name = "";
            string description = "";
            float amount = 0;
            int quantity = 0;
            float rate = 0;
            float grossAmt = 0;
            float tax1Amt = 0;
            float taxRate1 = 0;
            string taxCode = "";






            try
            {


                if (_isAuthenticated != true) { return; };


               

                RecordRef invoiceRef = new RecordRef
                {
                    type = RecordType.invoice,
                    typeSpecified = true,
                    internalId = this.TxtInternalId.Text,
                };


                ReadResponse readResponse = _service.get(invoiceRef);
                if (readResponse.status.isSuccess)
                {

                    FileStream fs = System.IO.File.Create("Facturacion.xml");
                    XmlSerializer writer = new XmlSerializer(typeof(ReadResponse));

                    writer.Serialize(fs, readResponse);
                    fs.Close();

                    string content;
                    using (StreamReader reader = new StreamReader("Facturacion.xml", Encoding.UTF8))
                    {
                        content = reader.ReadToEnd();
                    }
                    content = content.Replace("q1:", "");
                    System.IO.File.WriteAllText("Facturacion.xml", content, Encoding.UTF8);
                }



                XmlDocument myXmlDocument = new XmlDocument();
                myXmlDocument.Load("Facturacion.xml");
                XmlNodeList Encabezado = myXmlDocument.DocumentElement.SelectNodes("/ReadResponse");
                XmlNodeList Documento = Encabezado.Item(0).LastChild.ChildNodes;
                XmlNodeList CampoPersonalizados = null;
                XmlNodeList Direcciones = null;
                XmlNodeList ListadoArticulos = null;
                foreach (XmlNode node in Documento)
                {


                    if (node.Name == "entity") { entity = node.InnerText; };
                    if (node.Name == "tranDate") { tranDate = node.InnerText; };
                    if (node.Name == "tranId") { tranId = node.InnerText; };
                    if (node.Name == "terms") { terms = node.InnerText; };
                    if (node.Name == "subsidiary") { subsidiary = node.InnerText; };
                    if (node.Name == "salesRep") { salesRep = node.InnerText; };
                    if (node.Name == "message") { message = node.InnerText; };
                    if (node.Name == "total") { total = float.Parse(node.InnerText); };
                    if (node.Name == "subTotal") { subTotal = float.Parse(node.InnerText); };
                    if (node.Name == "taxTotal") { taxTotal = float.Parse(node.InnerText); };
                    if (node.Name == "otherRefNum") {DocRef = node.InnerText;};


                    


                    if (node.Name == "billingAddress")
                    {
                        Direcciones = node.ChildNodes;
                        foreach (XmlNode nodes in Direcciones)
                        {
                            if (nodes.Name == "memo") { memo = nodes.InnerText; };
                            if (nodes.Name == "addr3") { addr3 = nodes.InnerText; };
                            if (nodes.Name == "city") { city = nodes.InnerText; };
                            if (nodes.Name == "addr1") { addr1 = nodes.InnerText; };
                        }


                    }


                    if (node.Name == "itemList")
                    {
                        ListadoArticulos = node.ChildNodes;
                    }


                    if (node.Name == "customFieldList")
                    {
                        CampoPersonalizados = node.ChildNodes;
                        foreach (XmlNode nodes in CampoPersonalizados)
                        {
                            if (nodes.Attributes.GetNamedItem("scriptId").Value == "custbody_cust_giro") { custbody_cust_giro = nodes.InnerText; };
                            if (nodes.Attributes.GetNamedItem("scriptId").Value == "custbody_cust_nitcliente") { custbody_cust_nitcliente = nodes.InnerText; };
                            if (nodes.Attributes.GetNamedItem("scriptId").Value == "custbody_cust_nrc") { custbody_cust_nrc = nodes.InnerText; };

                            if (nodes.Attributes.GetNamedItem("scriptId").Value == "custbody_cust_tipodocumento") { custbody_cust_tipodocumento = nodes.InnerText; };
                            if (nodes.Attributes.GetNamedItem("scriptId").Value == "custbodycustbodycustbody_cust_paquete") { custbodycustbodycustbody_cust_paquete = nodes.InnerText; };
                            if (nodes.Attributes.GetNamedItem("scriptId").Value == "custbody_cust_numerocontrato") { custbody_cust_numerocontrato = nodes.InnerText; };
                            if (nodes.Attributes.GetNamedItem("scriptId").Value == "custbodycustbody_cust_numeroop") { custbodycustbody_cust_numeroop = nodes.InnerText; };
                        }

                    }

                } //******************************************


                


                if (connection.State != ConnectionState.Open) { connection.Open(); };



                

                string CmdDelete = "DELETE FROM [dbo].[Invoices] WHERE internalId=" + TxtInternalId.Text;
                SqlCommand commandDelete = new SqlCommand(CmdDelete, connection);
                commandDelete.ExecuteNonQuery();

                string CmdDeleteDetails = "DELETE FROM [dbo].[InvoicesDetalle] WHERE internalId=" + TxtInternalId.Text;
                SqlCommand commandDeleteDetail = new SqlCommand(CmdDeleteDetails, connection);
                commandDeleteDetail.ExecuteNonQuery();

                string CmdInsert = "INSERT INTO [dbo].[Invoices]"
                 + "([internalId]"
                 + ",[entity]"
                 + ",[tranDate]"
                 + ",[tranId]"
                 + ",[terms]"
                 + ",[subsidiary]"
                 + ",[salesRep]"
                 + ",[memo]"
                 + ",[addr3]"
                 + ",[city]"
                 + ",[addr1]"
                 + ",[custbody_cust_nitcliente]"
                 + ",[custbody_cust_giro]"
                 + ",[custbody_cust_nrc]"
                 + ",[custbody_cust_tipodocumento]"
                 + ",[custbodycustbodycustbody_cust_paquete]"
                 + ",[custbody_cust_numerocontrato]"
                 + ",[custbodycustbody_cust_numeroop]"
                 + ",[message]"
                 + ",[total]"
                 + ",[subTotal]"
                 + ",[taxTotal]"
                 + ",[DocRef])"
                 + "VALUES('" + this.TxtInternalId.Text + "','" + entity + "','" + tranDate.Substring(0, 10) + "','" + tranId + "','" + terms + "','" + subsidiary + "','"
                 + salesRep + "','" + memo + "','" + addr3 + "','" + city + "','" + addr1 + "','" + custbody_cust_nitcliente + "','" + custbody_cust_giro + "','"
                 + custbody_cust_nrc + "','" + custbody_cust_tipodocumento + "','" + custbodycustbodycustbody_cust_paquete + "','" + custbody_cust_numerocontrato + "','"
                 + custbodycustbody_cust_numeroop + "','" + message.Replace("'","") + "'," + total.ToString() + "," + subTotal.ToString() + "," + taxTotal + ",'"+ DocRef + "')";

                SqlCommand command = new SqlCommand(CmdInsert, connection);
                command.ExecuteNonQuery();

                foreach (XmlNode nodesHeader in ListadoArticulos)
                {

                    foreach (XmlNode nodes in nodesHeader.ChildNodes)
                    {

                        if (nodes.Name == "item")
                        {
                            name = nodes.ChildNodes[0].InnerText;
                        };
                        if (nodes.Name == "description") { description = nodes.InnerText; };
                        if (nodes.Name == "amount") { amount = float.Parse(nodes.InnerText); };
                        if (nodes.Name == "quantity") { quantity = int.Parse(nodes.InnerText); };
                        if (nodes.Name == "rate") { rate = float.Parse(nodes.InnerText); };
                        if (nodes.Name == "grossAmt") { grossAmt = float.Parse(nodes.InnerText); };
                        if (nodes.Name == "tax1Amt") { tax1Amt = float.Parse(nodes.InnerText); };
                        if (nodes.Name == "taxRate1") { taxRate1 = float.Parse(nodes.InnerText); };

                        if (nodes.Name == "taxCode")
                        {
                            taxCode = nodes.ChildNodes[0].InnerText;
                        };
                        

                    }

                    string CmdInsertDetail = "INSERT INTO [dbo].[InvoicesDetalle]"
                    + "([internalId]"
                    + ",[name]"
                    + ",[description]"
                    + ",[amount]"
                    + ",[quantity]"
                    + ",[rate]"
                    + ",[grossAmt]"
                    + ",[tax1Amt]"
                    + ",[taxRate1]"
                    + ",taxCode) VALUES(" + this.TxtInternalId.Text + ",'" + name + "','" + description + "'," + amount.ToString() + "," + quantity.ToString() + "," + rate.ToString() + "," + grossAmt.ToString() + "," + tax1Amt.ToString() + "," + taxRate1.ToString() + ",'"+taxCode+"')";


                    SqlCommand commandDetail = new SqlCommand(CmdInsertDetail, connection);
                    commandDetail.ExecuteNonQuery();
                }

                if (connection.State == ConnectionState.Open) { connection.Close(); };

                FrmImpresionDirecta f = new FrmImpresionDirecta();
                f.v_tipoDocumento = custbody_cust_tipodocumento;
                f.v_DocumentoId = this.TxtInternalId.Text;
                f.v_Subsidiary = subsidiary;
                f.v_Paquete = Boolean.Parse( custbodycustbodycustbody_cust_paquete);
                f.ShowDialog(this);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }

        public void Notas_Credito()
        {

            string entity = "";
            string tranDate = "";
            string tranId = "";
            string terms = "";
            string subsidiary = "";
            string salesRep = "";
            string memo = "";
            string addr3 = "";
            string city = "";
            string addr1 = "";
            string custbody_cust_nitcliente = "";
            string custbody_cust_giro = "";
            string custbody_cust_nrc = "";
            string custbody_cust_tipodocumento = "";
            string custbodycustbodycustbody_cust_paquete = "";
            string custbody_cust_numerocontrato = "";
            string custbodycustbody_cust_numeroop = "";
            string message = "";
            float total = 0;
            float subTotal = 0;
            float taxTotal = 0;

            string name = "";
            string description = "";
            float amount = 0;
            int quantity = 0;
            float rate = 0;
            float grossAmt = 0;
            float tax1Amt = 0;
            float taxRate1 = 0;
            string taxCode = "";


            try
            {


                if (_isAuthenticated != true) { return; };




                RecordRef invoiceRef = new RecordRef
                {
                    type = RecordType.creditMemo,
                    typeSpecified = true,
                    internalId = this.TxtInternalId.Text,

                };


                ReadResponse readResponse = _service.get(invoiceRef);
                if (readResponse.status.isSuccess)
                {

                    FileStream fs = System.IO.File.Create("NotaCredito.xml");
                    XmlSerializer writer = new XmlSerializer(typeof(ReadResponse));

                    writer.Serialize(fs, readResponse);
                    fs.Close();

                    string content;
                    using (StreamReader reader = new StreamReader("NotaCredito.xml", Encoding.UTF8))
                    {
                        content = reader.ReadToEnd();
                    }
                    content = content.Replace("q1:", "");
                    System.IO.File.WriteAllText("NotaCredito.xml", content, Encoding.UTF8);
                }



                XmlDocument myXmlDocument = new XmlDocument();
                myXmlDocument.Load("NotaCredito.xml");
                XmlNodeList Encabezado = myXmlDocument.DocumentElement.SelectNodes("/ReadResponse");
                XmlNodeList Documento = Encabezado.Item(0).LastChild.ChildNodes;
                XmlNodeList CampoPersonalizados = null;
                XmlNodeList Direcciones = null;
                XmlNodeList ListadoArticulos = null;
                foreach (XmlNode node in Documento)
                {


                    if (node.Name == "entity") { entity = node.InnerText; };
                    if (node.Name == "tranDate") { tranDate = node.InnerText; };
                    if (node.Name == "tranId") { tranId = node.InnerText; };
                    if (node.Name == "terms") { terms = node.InnerText; };
                    if (node.Name == "subsidiary") { subsidiary = node.InnerText; };
                    if (node.Name == "salesRep") { salesRep = node.InnerText; };
                    if (node.Name == "message") { message = node.InnerText; };
                    if (node.Name == "total") { total = float.Parse(node.InnerText); };
                    if (node.Name == "subTotal") { subTotal = float.Parse(node.InnerText); };
                    if (node.Name == "taxTotal") { taxTotal = float.Parse(node.InnerText); };




                    if (node.Name == "billingAddress")
                    {
                        Direcciones = node.ChildNodes;
                        foreach (XmlNode nodes in Direcciones)
                        {
                            if (nodes.Name == "memo") { memo = nodes.InnerText; };
                            if (nodes.Name == "addr3") { addr3 = nodes.InnerText; };
                            if (nodes.Name == "city") { city = nodes.InnerText; };
                            if (nodes.Name == "addr1") { addr1 = nodes.InnerText; };
                        }


                    }


                    if (node.Name == "itemList")
                    {
                        ListadoArticulos = node.ChildNodes;
                    }


                    if (node.Name == "customFieldList")
                    {
                        CampoPersonalizados = node.ChildNodes;
                        foreach (XmlNode nodes in CampoPersonalizados)
                        {
                            if (nodes.Attributes.GetNamedItem("scriptId").Value == "custbody_cust_giro") { custbody_cust_giro = nodes.InnerText; };
                            if (nodes.Attributes.GetNamedItem("scriptId").Value == "custbody_cust_nitcliente") { custbody_cust_nitcliente = nodes.InnerText; };
                            if (nodes.Attributes.GetNamedItem("scriptId").Value == "custbody_cust_nrc") { custbody_cust_nrc = nodes.InnerText; };

                            if (nodes.Attributes.GetNamedItem("scriptId").Value == "custbody_cust_tipodocumento") { custbody_cust_tipodocumento = nodes.InnerText; };
                            if (nodes.Attributes.GetNamedItem("scriptId").Value == "custbodycustbodycustbody_cust_paquete") { custbodycustbodycustbody_cust_paquete = nodes.InnerText; };
                            if (nodes.Attributes.GetNamedItem("scriptId").Value == "custbody_cust_numerocontrato") { custbody_cust_numerocontrato = nodes.InnerText; };
                            if (nodes.Attributes.GetNamedItem("scriptId").Value == "custbodycustbody_cust_numeroop") { custbodycustbody_cust_numeroop = nodes.InnerText; };
                        }

                    }

                } //******************************************



                custbody_cust_tipodocumento = "Nota de Crédito";

                if (connection.State != ConnectionState.Open) { connection.Open(); };





                string CmdDelete = "DELETE FROM [dbo].[CreditMemo] WHERE internalId=" + TxtInternalId.Text;
                SqlCommand commandDelete = new SqlCommand(CmdDelete, connection);
                commandDelete.ExecuteNonQuery();

                string CmdDeleteDetails = "DELETE FROM [dbo].[CreditMemoDetalle] WHERE internalId=" + TxtInternalId.Text;
                SqlCommand commandDeleteDetail = new SqlCommand(CmdDeleteDetails, connection);
                commandDeleteDetail.ExecuteNonQuery();

                string CmdInsert = "INSERT INTO [dbo].[CreditMemo]"
                 + "([internalId]"
                 + ",[entity]"
                 + ",[tranDate]"
                 + ",[tranId]"
                 + ",[terms]"
                 + ",[subsidiary]"
                 + ",[salesRep]"
                 + ",[memo]"
                 + ",[addr3]"
                 + ",[city]"
                 + ",[addr1]"
                 + ",[custbody_cust_nitcliente]"
                 + ",[custbody_cust_giro]"
                 + ",[custbody_cust_nrc]"
                 + ",[custbody_cust_tipodocumento]"
                 + ",[custbodycustbodycustbody_cust_paquete]"
                 + ",[custbody_cust_numerocontrato]"
                 + ",[custbodycustbody_cust_numeroop]"
                 + ",[message]"
                 + ",[total]"
                 + ",[subTotal]"
                 + ",[taxTotal])"
                 + "VALUES('" + this.TxtInternalId.Text + "','" + entity + "','" + tranDate.Substring(0, 10) + "','" + tranId + "','" + terms + "','" + subsidiary + "','"
                 + salesRep + "','" + memo + "','" + addr3 + "','" + city + "','" + addr1 + "','" + custbody_cust_nitcliente + "','" + custbody_cust_giro + "','"
                 + custbody_cust_nrc + "','" + custbody_cust_tipodocumento + "','" + custbodycustbodycustbody_cust_paquete + "','" + custbody_cust_numerocontrato + "','"
                 + custbodycustbody_cust_numeroop + "','" + message + "'," + total.ToString() + "," + subTotal.ToString() + "," + taxTotal + ")";

                SqlCommand command = new SqlCommand(CmdInsert, connection);
                command.ExecuteNonQuery();


                foreach (XmlNode nodesHeader in ListadoArticulos)
                {

                    foreach (XmlNode nodes in nodesHeader.ChildNodes)
                    {

                        if (nodes.Name == "item")
                        {
                            name = nodes.ChildNodes[0].InnerText;
                        };
                        if (nodes.Name == "description") { description = nodes.InnerText; };
                        if (nodes.Name == "amount") { amount = float.Parse(nodes.InnerText); };
                        if (nodes.Name == "quantity") { quantity = int.Parse(nodes.InnerText); };
                        if (nodes.Name == "rate") { rate = float.Parse(nodes.InnerText); };
                        if (nodes.Name == "grossAmt") { grossAmt = float.Parse(nodes.InnerText); };
                        if (nodes.Name == "tax1Amt") { tax1Amt = float.Parse(nodes.InnerText); };
                        if (nodes.Name == "taxRate1") { taxRate1 = float.Parse(nodes.InnerText); };

                        if (nodes.Name == "taxCode")
                        {
                            taxCode = nodes.ChildNodes[0].InnerText;
                        };


                    }

                    string CmdInsertDetail = "INSERT INTO [dbo].[CreditMemoDetalle]"
                    + "([internalId]"
                    + ",[name]"
                    + ",[description]"
                    + ",[amount]"
                    + ",[quantity]"
                    + ",[rate]"
                    + ",[grossAmt]"
                    + ",[tax1Amt]"
                    + ",[taxRate1]"
                    + ",taxCode) VALUES(" + this.TxtInternalId.Text + ",'" + name + "','" + description + "'," + amount.ToString() + "," + quantity.ToString() + "," + rate.ToString() + "," + grossAmt.ToString() + "," + tax1Amt.ToString() + "," + taxRate1.ToString() + ",'" + taxCode + "')";


                    SqlCommand commandDetail = new SqlCommand(CmdInsertDetail, connection);
                    commandDetail.ExecuteNonQuery();
                }

                if (connection.State == ConnectionState.Open) { connection.Close(); };


                FrmImpresionDirecta f = new FrmImpresionDirecta();
                f.v_tipoDocumento = custbody_cust_tipodocumento;
                f.v_DocumentoId = this.TxtInternalId.Text;
                f.v_Subsidiary = subsidiary;
                f.ShowDialog(this);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

    }
}
