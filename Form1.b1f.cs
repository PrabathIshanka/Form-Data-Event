using System;
using System.Collections.Generic;
using System.Xml;
using SAPbouiCOM;
using SAPbouiCOM.Framework;

namespace fromdataEvent
{
    [FormAttribute("fromdataEvent.Form1", "Form1.b1f")]
    class Form1 : UserFormBase
    {
        SAPbobsCOM.Company ocompany;
        SAPbobsCOM.Recordset orecset;
        SAPbouiCOM.Form oForm;
        string _formUID = "";

        public Form1()
        {
            ocompany = (SAPbobsCOM.Company)SAPbouiCOM.Framework.Application.SBO_Application.Company.GetDICompany();
            orecset = (SAPbobsCOM.Recordset)ocompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);

            SAPbouiCOM.Framework.Application.SBO_Application.FormDataEvent += new SAPbouiCOM._IApplicationEvents_FormDataEventEventHandler(ref FormDataEvent);
        }

        private void FormDataEvent(ref BusinessObjectInfo BusinessObjectInfo, out bool BubbleEvent)
        {
            
            BubbleEvent = true;

            if (!BusinessObjectInfo.BeforeAction &&  BusinessObjectInfo.FormUID == _formUID && BusinessObjectInfo.ActionSuccess  && BusinessObjectInfo.EventType == SAPbouiCOM.BoEventTypes.et_FORM_DATA_ADD)
            {
                string Query = "Select \"U_Test\",\"Code\",\"Name\" From \"@DATAEVENT\" ";
                orecset.DoQuery(Query);
                if (orecset.RecordCount > 0)
                {
                    for (int i = 0; orecset.RecordCount > i; i++)
                    {
                        Matrix1.AddRow();
                        ((SAPbouiCOM.EditText)Matrix1.Columns.Item("Code").Cells.Item(i + 1).Specific).Value = orecset.Fields.Item("Code").Value.ToString();
                        ((SAPbouiCOM.EditText)Matrix1.Columns.Item("Name").Cells.Item(i + 1).Specific).Value = orecset.Fields.Item("Name").Value.ToString();
                      
                        ((SAPbouiCOM.EditText)Matrix1.Columns.Item("U_Test").Cells.Item(i + 1).Specific).Value = orecset.Fields.Item("U_Test").Value.ToString();
                       

                        orecset.MoveNext();
                    }

                    Matrix1.FlushToDataSource();
                }

            }
      
        }
        

        /// <summary>
        /// Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {
            this.EditText0 = ((SAPbouiCOM.EditText)(this.GetItem("Item_0").Specific));
            this.EditText0.KeyDownAfter += new SAPbouiCOM._IEditTextEvents_KeyDownAfterEventHandler(this.EditText0_KeyDownAfter);
            this.EditText1 = ((SAPbouiCOM.EditText)(this.GetItem("Item_1").Specific));
            this.Button0 = ((SAPbouiCOM.Button)(this.GetItem("1").Specific));
            this.Matrix1 = ((SAPbouiCOM.Matrix)(this.GetItem("Item_4").Specific));
            this.EditText2 = ((SAPbouiCOM.EditText)(this.GetItem("Item_5").Specific));
            this.EditText3 = ((SAPbouiCOM.EditText)(this.GetItem("Item_6").Specific));
            this.OnCustomInitialize();

        }

        /// <summary>
        /// Initialize form event. Called by framework before form creation.
        /// </summary>
        public override void OnInitializeFormEvents()
        {
            this.LoadAfter += new LoadAfterHandler(this.Form_LoadAfter);

        }

        private SAPbouiCOM.EditText EditText0;

        private void OnCustomInitialize()
        {

        }

        private SAPbouiCOM.EditText EditText1;
        private SAPbouiCOM.Button Button0;

        private void EditText0_KeyDownAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            //throw new System.NotImplementedException();

        }

        private void Form_LoadAfter(SAPbouiCOM.SBOItemEventArg pVal)
        {
            // throw new System.NotImplementedException();
            oForm = SAPbouiCOM.Framework.Application.SBO_Application.Forms.Item(pVal.FormUID);
            _formUID = pVal.FormUID;
        }

        private Matrix Matrix1;
        private EditText EditText2;
        private EditText EditText3;
    }
}