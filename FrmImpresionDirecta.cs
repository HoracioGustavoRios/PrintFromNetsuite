using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Windows.Forms;

namespace NETSUITE_PRINT
{
    public partial class FrmImpresionDirecta : Form
    {

        public string v_tipoDocumento = "";
        public string v_DocumentoId = "";
        public string v_Subsidiary = "";
        public string v_BankId = "";
        public Boolean v_Paquete = false;


        TableLogOnInfo tliCurrent = new TableLogOnInfo();
        ConnectionInfo crConnectionInfo = new ConnectionInfo();

        public FrmImpresionDirecta()
        {
            InitializeComponent();
        }

        private void FrmImpresionDirecta_Shown(object sender, EventArgs e)
        {

            crConnectionInfo.ServerName = "{Poner SeverName}";
            crConnectionInfo.DatabaseName = "{Poner DataBase}";
            crConnectionInfo.UserID = "sa";
            crConnectionInfo.Password = "{Poner Contraseña}";


            string oldDatabaseName = "";


            //*************************************** FACTURACION ************************************************************
            if (this.v_Subsidiary == "Styba") { 

                    //**********************  CCF **********************************************************            
                if (this.v_tipoDocumento == "Comprobante Crédito Fiscal") { oldDatabaseName = ST_CCF.Database.Tables[0].LogOnInfo.ConnectionInfo.DatabaseName; };
                    if (v_tipoDocumento == "Comprobante Crédito Fiscal")
                    {
                        foreach (Table tbCurrent in ST_CCF.Database.Tables)
                        {
                            tliCurrent = tbCurrent.LogOnInfo;
                            tliCurrent.ConnectionInfo = crConnectionInfo;
                            tbCurrent.ApplyLogOnInfo(tliCurrent);
                            tbCurrent.LogOnInfo.ConnectionInfo.Password = "{Poner Contraseña}";
                            tbCurrent.Location = tbCurrent.Location.Replace(oldDatabaseName, "{Poner BaseDeDatos}");
                        }
                    }

                    if (v_tipoDocumento == "Comprobante Crédito Fiscal" )
                    {
                        ST_CCF.SetParameterValue(0, this.v_DocumentoId);
                        ST_CCF.PrintOptions.PrinterName = "EPSON LX";
                        ST_CCF.PrintOptions.PaperSize = PaperSize.DefaultPaperSize;
                        ST_CCF.PrintToPrinter(1, false, 0, 0);                
                    }
                    //**********************  CCF **********************************************************            

                    //**********************  FACTURA **********************************************************            
                    if (this.v_tipoDocumento == "Factura Fiscal") { oldDatabaseName = ST_FAC.Database.Tables[0].LogOnInfo.ConnectionInfo.DatabaseName; };
                    if (v_tipoDocumento == "Factura Fiscal")
                    {
                        foreach (Table tbCurrent in ST_FAC.Database.Tables)
                        {
                            tliCurrent = tbCurrent.LogOnInfo;
                            tliCurrent.ConnectionInfo = crConnectionInfo;
                            tbCurrent.ApplyLogOnInfo(tliCurrent);
                            tbCurrent.LogOnInfo.ConnectionInfo.Password = "{Poner Contraseña}";
                            tbCurrent.Location = tbCurrent.Location.Replace(oldDatabaseName, "{Poner DataBase}");
                        }
                    }

                    if (v_tipoDocumento == "Factura Fiscal")
                    {
                        ST_FAC.SetParameterValue(0, this.v_DocumentoId);
                        ST_FAC.PrintOptions.PrinterName = "EPSON LX";
                        ST_FAC.PrintOptions.PaperSize = PaperSize.DefaultPaperSize;
                        ST_FAC.PrintToPrinter(1, false, 0, 0);
                    }
                //**********************  FACTURA **********************************************************  


                    //**********************  FACTURA EXPORTACIÓN **********************************************************            
                    if (this.v_tipoDocumento == "Factura Exportación") { oldDatabaseName = ST_EXP.Database.Tables[0].LogOnInfo.ConnectionInfo.DatabaseName; };
                    if (v_tipoDocumento == "Factura Exportación")
                    {
                        foreach (Table tbCurrent in ST_EXP.Database.Tables)
                        {
                            tliCurrent = tbCurrent.LogOnInfo;
                            tliCurrent.ConnectionInfo = crConnectionInfo;
                            tbCurrent.ApplyLogOnInfo(tliCurrent);
                            tbCurrent.LogOnInfo.ConnectionInfo.Password = "{Poner Contraseña}";
                            tbCurrent.Location = tbCurrent.Location.Replace(oldDatabaseName, "{Poner DataBase}");
                        }
                    }

                    if (v_tipoDocumento == "Factura Exportación")
                    {
                        ST_EXP.SetParameterValue(0, this.v_DocumentoId);
                        ST_EXP.PrintOptions.PrinterName = "EPSON LX";
                        ST_EXP.PrintOptions.PaperSize = PaperSize.DefaultPaperSize;
                        ST_EXP.PrintToPrinter(1, false, 0, 0);
                    }
                    //**********************  FACTURA EXPORTACIÓN **********************************************************            


                    //**********************  NOTAS CREDITO **********************************************************            
                    if (this.v_tipoDocumento == "Nota de Crédito") { oldDatabaseName = ST_NC.Database.Tables[0].LogOnInfo.ConnectionInfo.DatabaseName; };
                    if (v_tipoDocumento == "Nota de Crédito")
                    {
                        foreach (Table tbCurrent in ST_NC.Database.Tables)
                        {
                            tliCurrent = tbCurrent.LogOnInfo;
                            tliCurrent.ConnectionInfo = crConnectionInfo;
                            tbCurrent.ApplyLogOnInfo(tliCurrent);
                            tbCurrent.LogOnInfo.ConnectionInfo.Password = "{Poner Contraseña}";
                            tbCurrent.Location = tbCurrent.Location.Replace(oldDatabaseName, "{Poner DataBase}");
                        }
                    }

                    if (v_tipoDocumento == "Nota de Crédito")
                    {
                        ST_NC.SetParameterValue(0, this.v_DocumentoId);
                        ST_NC.PrintOptions.PrinterName = "EPSON LX";
                        ST_NC.PrintOptions.PaperSize = PaperSize.DefaultPaperSize;
                        ST_NC.PrintToPrinter(1, false, 0, 0);
                    }
                //**********************  NOTAS CREDITO **********************************************************

            
            }

            if (this.v_Subsidiary == "Viva Outdoor")
            {
                if (this.v_Paquete == false) {

                    //**********************  CCF **********************************************************            
                    if (this.v_tipoDocumento == "Comprobante Crédito Fiscal") { oldDatabaseName = VO_CCF.Database.Tables[0].LogOnInfo.ConnectionInfo.DatabaseName; };
                    if (v_tipoDocumento == "Comprobante Crédito Fiscal")
                    {
                        foreach (Table tbCurrent in VO_CCF.Database.Tables)
                        {
                            tliCurrent = tbCurrent.LogOnInfo;
                            tliCurrent.ConnectionInfo = crConnectionInfo;
                            tbCurrent.ApplyLogOnInfo(tliCurrent);
                            tbCurrent.LogOnInfo.ConnectionInfo.Password = "{Poner Contraseña}";
                            tbCurrent.Location = tbCurrent.Location.Replace(oldDatabaseName, "{Poner DataBase}");
                        }
                    }

                    if (v_tipoDocumento == "Comprobante Crédito Fiscal")
                    {
                        VO_CCF.SetParameterValue(0, this.v_DocumentoId);
                        VO_CCF.PrintOptions.PrinterName = "EPSON LX";
                        VO_CCF.PrintOptions.PaperSize = PaperSize.DefaultPaperSize;
                        VO_CCF.PrintToPrinter(1, false, 0, 0);
                    }
                    //**********************  CCF **********************************************************   
                   

                    //**********************  FACTURA **********************************************************            
                    if (this.v_tipoDocumento == "Factura Fiscal") { oldDatabaseName = VO_FAC.Database.Tables[0].LogOnInfo.ConnectionInfo.DatabaseName; };
                    if (v_tipoDocumento == "Factura Fiscal")
                    {
                        foreach (Table tbCurrent in VO_FAC.Database.Tables)
                        {
                            tliCurrent = tbCurrent.LogOnInfo;
                            tliCurrent.ConnectionInfo = crConnectionInfo;
                            tbCurrent.ApplyLogOnInfo(tliCurrent);
                            tbCurrent.LogOnInfo.ConnectionInfo.Password = "{Poner Contraseña}";
                            tbCurrent.Location = tbCurrent.Location.Replace(oldDatabaseName, "{Poner DataBase}");
                        }
                    }

                    if (v_tipoDocumento == "Factura Fiscal")
                    {
                        VO_FAC.SetParameterValue(0, this.v_DocumentoId);
                        VO_FAC.PrintOptions.PrinterName = "EPSON LX";
                        VO_FAC.PrintOptions.PaperSize = PaperSize.DefaultPaperSize;
                        VO_FAC.PrintToPrinter(1, false, 0, 0);
                    }
                    //**********************  FACTURA ********************************************************** 


                    //**********************  FACTURA EXPORTACIÓN **********************************************************            
                    if (this.v_tipoDocumento == "Factura Exportación") { oldDatabaseName = VO_EXP.Database.Tables[0].LogOnInfo.ConnectionInfo.DatabaseName; };
                    if (v_tipoDocumento == "Factura Exportación")
                    {
                        foreach (Table tbCurrent in VO_EXP.Database.Tables)
                        {
                            tliCurrent = tbCurrent.LogOnInfo;
                            tliCurrent.ConnectionInfo = crConnectionInfo;
                            tbCurrent.ApplyLogOnInfo(tliCurrent);
                            tbCurrent.LogOnInfo.ConnectionInfo.Password = "{Poner Contraseña}";
                            tbCurrent.Location = tbCurrent.Location.Replace(oldDatabaseName, "{Poner DataBase}");
                        }
                    }

                    if (v_tipoDocumento == "Factura Exportación")
                    {
                        VO_EXP.SetParameterValue(0, this.v_DocumentoId);
                        VO_EXP.PrintOptions.PrinterName = "EPSON LX";
                        VO_EXP.PrintOptions.PaperSize = PaperSize.DefaultPaperSize;
                        VO_EXP.PrintToPrinter(1, false, 0, 0);
                    }
                    //**********************  FACTURA EXPORTACIÓN **********************************************************            



                    //**********************  NOTAS CREDITO **********************************************************            
                    if (this.v_tipoDocumento == "Nota de Crédito") { oldDatabaseName = VO_NC.Database.Tables[0].LogOnInfo.ConnectionInfo.DatabaseName; };
                    if (v_tipoDocumento == "Nota de Crédito")
                    {
                        foreach (Table tbCurrent in VO_NC.Database.Tables)
                        {
                            tliCurrent = tbCurrent.LogOnInfo;
                            tliCurrent.ConnectionInfo = crConnectionInfo;
                            tbCurrent.ApplyLogOnInfo(tliCurrent);
                            tbCurrent.LogOnInfo.ConnectionInfo.Password = "{Poner Contraseña}";
                            tbCurrent.Location = tbCurrent.Location.Replace(oldDatabaseName, "{Poner DataBase}");
                        }
                    }

                    if (v_tipoDocumento == "Nota de Crédito")
                    {
                        VO_NC.SetParameterValue(0, this.v_DocumentoId);
                        VO_NC.PrintOptions.PrinterName = "EPSON LX";
                        VO_NC.PrintOptions.PaperSize = PaperSize.DefaultPaperSize;
                        VO_NC.PrintToPrinter(1, false, 0, 0);
                    }
                    //**********************  NOTAS CREDITO **********************************************************


                
                }

                if (this.v_Paquete == true)
                {

                    //**********************  CCF **********************************************************            
                    if (this.v_tipoDocumento == "Comprobante Crédito Fiscal") { oldDatabaseName = VO_CCF_PAQUETE.Database.Tables[0].LogOnInfo.ConnectionInfo.DatabaseName; };
                    if (v_tipoDocumento == "Comprobante Crédito Fiscal")
                    {
                        foreach (Table tbCurrent in VO_CCF_PAQUETE.Database.Tables)
                        {
                            tliCurrent = tbCurrent.LogOnInfo;
                            tliCurrent.ConnectionInfo = crConnectionInfo;
                            tbCurrent.ApplyLogOnInfo(tliCurrent);
                            tbCurrent.LogOnInfo.ConnectionInfo.Password = "{Poner Contraseña}";
                            tbCurrent.Location = tbCurrent.Location.Replace(oldDatabaseName, "{Poner DataBase}");
                        }
                    }

                    if (v_tipoDocumento == "Comprobante Crédito Fiscal")
                    {
                        VO_CCF_PAQUETE.SetParameterValue(0, this.v_DocumentoId);
                        VO_CCF_PAQUETE.PrintOptions.PrinterName = "EPSON LX";
                        VO_CCF_PAQUETE.PrintOptions.PaperSize = PaperSize.DefaultPaperSize;
                        VO_CCF_PAQUETE.PrintToPrinter(1, false, 0, 0);
                    }
                    //**********************  CCF **********************************************************                      


                    //**********************  FACTURA **********************************************************            
                    if (this.v_tipoDocumento == "Factura Fiscal") { oldDatabaseName = VO_FAC_PAQUETE.Database.Tables[0].LogOnInfo.ConnectionInfo.DatabaseName; };
                    if (v_tipoDocumento == "Factura Fiscal")
                    {
                        foreach (Table tbCurrent in VO_FAC_PAQUETE.Database.Tables)
                        {
                            tliCurrent = tbCurrent.LogOnInfo;
                            tliCurrent.ConnectionInfo = crConnectionInfo;
                            tbCurrent.ApplyLogOnInfo(tliCurrent);
                            tbCurrent.LogOnInfo.ConnectionInfo.Password = "{Poner Contraseña}";
                            tbCurrent.Location = tbCurrent.Location.Replace(oldDatabaseName, "{Poner DataBase}");
                        }
                    }

                    if (v_tipoDocumento == "Factura Fiscal")
                    {
                        VO_FAC_PAQUETE.SetParameterValue(0, this.v_DocumentoId);
                        VO_FAC_PAQUETE.PrintOptions.PrinterName = "EPSON LX";
                        VO_FAC_PAQUETE.PrintOptions.PaperSize = PaperSize.DefaultPaperSize;
                        VO_FAC_PAQUETE.PrintToPrinter(1, false, 0, 0);
                    }
                    //**********************  FACTURA ********************************************************** 


                    //**********************  FACTURA EXPORTACIÓN **********************************************************            
                    if (this.v_tipoDocumento == "Factura Exportación") { oldDatabaseName = VO_EXP_PAQUETE.Database.Tables[0].LogOnInfo.ConnectionInfo.DatabaseName; };
                    if (v_tipoDocumento == "Factura Exportación")
                    {
                        foreach (Table tbCurrent in VO_EXP_PAQUETE.Database.Tables)
                        {
                            tliCurrent = tbCurrent.LogOnInfo;
                            tliCurrent.ConnectionInfo = crConnectionInfo;
                            tbCurrent.ApplyLogOnInfo(tliCurrent);
                            tbCurrent.LogOnInfo.ConnectionInfo.Password = "{Poner Contraseña}";
                            tbCurrent.Location = tbCurrent.Location.Replace(oldDatabaseName, "{Poner DataBase}");
                        }
                    }

                    if (v_tipoDocumento == "Factura Exportación")
                    {
                        VO_EXP_PAQUETE.SetParameterValue(0, this.v_DocumentoId);
                        VO_EXP_PAQUETE.PrintOptions.PrinterName = "EPSON LX";
                        VO_EXP_PAQUETE.PrintOptions.PaperSize = PaperSize.DefaultPaperSize;
                        VO_EXP_PAQUETE.PrintToPrinter(1, false, 0, 0);
                    }
                    //**********************  FACTURA EXPORTACIÓN **********************************************************    


                    //**********************  NOTAS CREDITO **********************************************************            
                    if (this.v_tipoDocumento == "Nota de Crédito") { oldDatabaseName = VO_NC_PAQUETE.Database.Tables[0].LogOnInfo.ConnectionInfo.DatabaseName; };
                    if (v_tipoDocumento == "Nota de Crédito")
                    {
                        foreach (Table tbCurrent in VO_NC_PAQUETE.Database.Tables)
                        {
                            tliCurrent = tbCurrent.LogOnInfo;
                            tliCurrent.ConnectionInfo = crConnectionInfo;
                            tbCurrent.ApplyLogOnInfo(tliCurrent);
                            tbCurrent.LogOnInfo.ConnectionInfo.Password = "{Poner Contraseña}";
                            tbCurrent.Location = tbCurrent.Location.Replace(oldDatabaseName, "{Poner DataBase}");
                        }
                    }

                    if (v_tipoDocumento == "Nota de Crédito")
                    {
                        VO_NC_PAQUETE.SetParameterValue(0, this.v_DocumentoId);
                        VO_NC_PAQUETE.PrintOptions.PrinterName = "EPSON LX";
                        VO_NC_PAQUETE.PrintOptions.PaperSize = PaperSize.DefaultPaperSize;
                        VO_NC_PAQUETE.PrintToPrinter(1, false, 0, 0);
                    }
                    //**********************  NOTAS CREDITO **********************************************************



                }

            }
             
            //*************************************** FACTURACION ************************************************************




            //*************************************** CHEQUES ************************************************************

            if (this.v_tipoDocumento == "Cheque" & this.v_BankId == "CH-BAC-BA") { oldDatabaseName = CH_BAC_BA.Database.Tables[0].LogOnInfo.ConnectionInfo.DatabaseName; };
            if (v_tipoDocumento == "Cheque" & this.v_BankId == "CH-BAC-BA")
            {
                foreach (Table tbCurrent in CH_BAC_BA.Database.Tables)
                {
                    tliCurrent = tbCurrent.LogOnInfo;
                    tliCurrent.ConnectionInfo = crConnectionInfo;
                    tbCurrent.ApplyLogOnInfo(tliCurrent);
                    tbCurrent.LogOnInfo.ConnectionInfo.Password = "{Poner Contraseña}";
                    tbCurrent.Location = tbCurrent.Location.Replace(oldDatabaseName, "{Poner DataBase}");
                }
            }

            if (v_tipoDocumento == "Cheque" & this.v_BankId == "CH-BAC-BA")
            {
                CH_BAC_BA.SetParameterValue(0, this.v_DocumentoId);
                CH_BAC_BA.PrintOptions.PrinterName = "EPSON LX";
                CH_BAC_BA.PrintOptions.PaperSize = PaperSize.DefaultPaperSize;
                CH_BAC_BA.PrintToPrinter(1, false, 0, 0);
            }





            if (this.v_tipoDocumento == "Cheque" & this.v_BankId == "CH-BI-BDAV-BH") { oldDatabaseName = CH_BI_BDAV_BH.Database.Tables[0].LogOnInfo.ConnectionInfo.DatabaseName; };
            if (v_tipoDocumento == "Cheque" & this.v_BankId == "CH-BI-BDAV-BH")
            {
                foreach (Table tbCurrent in CH_BI_BDAV_BH.Database.Tables)
                {
                    tliCurrent = tbCurrent.LogOnInfo;
                    tliCurrent.ConnectionInfo = crConnectionInfo;
                    tbCurrent.ApplyLogOnInfo(tliCurrent);
                    tbCurrent.LogOnInfo.ConnectionInfo.Password = "{Poner Contraseña}";
                    tbCurrent.Location = tbCurrent.Location.Replace(oldDatabaseName, "{Poner DataBase}");
                }
            }

            if (v_tipoDocumento == "Cheque" & this.v_BankId == "CH-BI-BDAV-BH")
            {
                CH_BI_BDAV_BH.SetParameterValue(0, this.v_DocumentoId);
                CH_BI_BDAV_BH.PrintOptions.PrinterName = "EPSON LX";
                CH_BI_BDAV_BH.PrintOptions.PaperSize = PaperSize.DefaultPaperSize;
                CH_BI_BDAV_BH.PrintToPrinter(1, false, 0, 0);
            }


            if (this.v_tipoDocumento == "Cheque" & this.v_BankId == "CH-CITI-CUS") { oldDatabaseName = CH_CITI_CUS.Database.Tables[0].LogOnInfo.ConnectionInfo.DatabaseName; };
            if (v_tipoDocumento == "Cheque" & this.v_BankId == "CH-CITI-CUS")
            {
                foreach (Table tbCurrent in CH_CITI_CUS.Database.Tables)
                {
                    tliCurrent = tbCurrent.LogOnInfo;
                    tliCurrent.ConnectionInfo = crConnectionInfo;
                    tbCurrent.ApplyLogOnInfo(tliCurrent);
                    tbCurrent.LogOnInfo.ConnectionInfo.Password = "{Poner Contraseña}";
                    tbCurrent.Location = tbCurrent.Location.Replace(oldDatabaseName, "{Poner DataBase}");
                }
            }

            if (v_tipoDocumento == "Cheque" & this.v_BankId == "CH-CITI-CUS")
            {
                CH_CITI_CUS.SetParameterValue(0, this.v_DocumentoId);
                CH_CITI_CUS.PrintOptions.PrinterName = "EPSON LX";
                CH_CITI_CUS.PrintOptions.PaperSize = PaperSize.DefaultPaperSize;
                CH_CITI_CUS.PrintToPrinter(1, false, 0, 0);
            }


            //*************************************** CHEQUES ************************************************************








            //*************************************** VENDOR PAYMENT *****************************************************
            if (this.v_tipoDocumento == "VendorPayment" & this.v_BankId == "CH-CITI-CUS") { oldDatabaseName = PP_CITI_CUS.Database.Tables[0].LogOnInfo.ConnectionInfo.DatabaseName; };
            if (v_tipoDocumento == "VendorPayment" & this.v_BankId == "CH-CITI-CUS")
            {
                foreach (Table tbCurrent in PP_CITI_CUS.Database.Tables)
                {
                    tliCurrent = tbCurrent.LogOnInfo;
                    tliCurrent.ConnectionInfo = crConnectionInfo;
                    tbCurrent.ApplyLogOnInfo(tliCurrent);
                    tbCurrent.LogOnInfo.ConnectionInfo.Password = "{Poner Contraseña}";
                    tbCurrent.Location = tbCurrent.Location.Replace(oldDatabaseName, "{Poner DataBase}");
                }
            }

            if (v_tipoDocumento == "VendorPayment" & this.v_BankId == "CH-CITI-CUS")
            {
                PP_CITI_CUS.SetParameterValue(0, this.v_DocumentoId);
                PP_CITI_CUS.PrintOptions.PrinterName = "EPSON LX";
                PP_CITI_CUS.PrintOptions.PaperSize = PaperSize.DefaultPaperSize;
                PP_CITI_CUS.PrintToPrinter(1, false, 0, 0);
            }


            if (this.v_tipoDocumento == "VendorPayment" & this.v_BankId == "CH-BAC-BA") { oldDatabaseName = PP_BAC_BA.Database.Tables[0].LogOnInfo.ConnectionInfo.DatabaseName; };
            if (v_tipoDocumento == "VendorPayment" & this.v_BankId == "CH-BAC-BA")
            {
                foreach (Table tbCurrent in PP_BAC_BA.Database.Tables)
                {
                    tliCurrent = tbCurrent.LogOnInfo;
                    tliCurrent.ConnectionInfo = crConnectionInfo;
                    tbCurrent.ApplyLogOnInfo(tliCurrent);
                    tbCurrent.LogOnInfo.ConnectionInfo.Password = "{Poner Contraseña}";
                    tbCurrent.Location = tbCurrent.Location.Replace(oldDatabaseName, "{Poner DataBase}");
                }
            }

            if (v_tipoDocumento == "VendorPayment" & this.v_BankId == "CH-BAC-BA")
            {
                PP_BAC_BA.SetParameterValue(0, this.v_DocumentoId);
                PP_BAC_BA.PrintOptions.PrinterName = "EPSON LX";
                PP_BAC_BA.PrintOptions.PaperSize = PaperSize.DefaultPaperSize;
                PP_BAC_BA.PrintToPrinter(1, false, 0, 0);
            }


            if (this.v_tipoDocumento == "VendorPayment" & this.v_BankId == "CH-BI-BDAV-BH") { oldDatabaseName = PP_BI_BDAV_BH.Database.Tables[0].LogOnInfo.ConnectionInfo.DatabaseName; };
            if (v_tipoDocumento == "VendorPayment" & this.v_BankId == "CH-BI-BDAV-BH")
            {
                foreach (Table tbCurrent in PP_BI_BDAV_BH.Database.Tables)
                {
                    tliCurrent = tbCurrent.LogOnInfo;
                    tliCurrent.ConnectionInfo = crConnectionInfo;
                    tbCurrent.ApplyLogOnInfo(tliCurrent);
                    tbCurrent.LogOnInfo.ConnectionInfo.Password = "{Poner Contraseña}";
                    tbCurrent.Location = tbCurrent.Location.Replace(oldDatabaseName, "{Poner DataBase}");
                }
            }

            if (v_tipoDocumento == "VendorPayment" & this.v_BankId == "CH-BI-BDAV-BH")
            {
                PP_BI_BDAV_BH.SetParameterValue(0, this.v_DocumentoId);
                PP_BI_BDAV_BH.PrintOptions.PrinterName = "EPSON LX";
                PP_BI_BDAV_BH.PrintOptions.PaperSize = PaperSize.DefaultPaperSize;
                PP_BI_BDAV_BH.PrintToPrinter(1, false, 0, 0);
            }

            //*************************************** VENDOR PAYMENT *****************************************************
            this.Close();
        }



    }

}
