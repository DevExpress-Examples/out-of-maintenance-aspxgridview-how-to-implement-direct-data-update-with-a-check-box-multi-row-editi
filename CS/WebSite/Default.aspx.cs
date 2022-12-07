#region Using
using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.SessionState;
using System.ComponentModel;
#endregion
public partial class BatchUpdate : System.Web.UI.Page {
    protected void Page_Load(object sender, EventArgs e) {
        //Populate grid with data on the first load
        if(!IsPostBack && !IsCallback) {
            InvoiceItemsProvider provider = new InvoiceItemsProvider();
            provider.Populate();
            provider.Populate();
        }
    }
    protected void grid_CustomDataCallback(object sender, DevExpress.Web.ASPxGridViewCustomDataCallbackEventArgs e) {
        string[] res = e.Parameters.Split(':');
        if(res.Length != 2) return;
        int id = -1;
        bool value = false;
        if(!int.TryParse(res[0], out id)) return;
        if(!bool.TryParse(res[1], out value)) return;
        UpdateItem(id, value);
    }
    void UpdateItem(int id, bool value) {
        //Update your data here
        InvoiceItemsProvider provider = new InvoiceItemsProvider();
        InvoiceItem item = provider.GetItemById(id);
        if(item != null) {
            item.IsPaid = value;
        }
    }
    protected string GetCheckBoxChecked(object value) {
        return (bool)value ? "checked" : "";
    }
}

